using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Regions;
using Server.Mobiles;
using Server.Network;

namespace Server.Multis
{
    public class PreviewHouse : BaseMulti
    {
        private readonly List<PreviewMarkerItem> _markers = new List<PreviewMarkerItem>();

        // Cloth visuals
        private const int RingItemID = 0x1767; // cloth
        private const int FillItemID = 0x1766; // cloth variant
        private const int HueValid = 0;        // default gray
        private const int HueInvalid = 33;     // red

        private Timer _timer;

        // Footprint bounds (multi-relative)
        private int _minX, _maxX, _minY, _maxY;

        // Spawn control
        private bool _markersSpawned;

        // Expose validity for UI logic
        public bool CenterAreaValid { get; private set; }
        public bool SurroundingRingValid { get; private set; }

        // Track invalid tiles for the most recent validation
        private HashSet<Point2D> _invalidCenterTiles = new HashSet<Point2D>();
        private HashSet<Point2D> _invalidRingTiles = new HashSet<Point2D>();

        // Viewer tracking: when someone opens the preview gump we attach them here.
        // The preview will persist while at least one attached viewer is present and within range;
        // otherwise it will expire according to the lifetime timer logic.
        private readonly HashSet<Mobile> _viewers = new HashSet<Mobile>();

        public int PreviewWidth => (_maxX - _minX + 1);
        public int PreviewHeight => (_maxY - _minY + 1);

        public PreviewHouse(int multiID) : base(multiID)
        {
            var mcl = Components;
            _minX = mcl.Min.X;
            _maxX = mcl.Max.X;
            _minY = mcl.Min.Y;
            _maxY = mcl.Max.Y;

            _markersSpawned = false;
            CenterAreaValid = false;
            SurroundingRingValid = false;

            // Safety: defer marker spawn slightly; by then MoveToWorld should be done.
            Timer.DelayCall(TimeSpan.FromMilliseconds(50), () =>
            {
                if (!Deleted)
                {
                    EnsureMarkersSpawned();
                }
            });

            // Start periodic watcher (every 5 seconds)
            _timer = new PreviewLifetimeTimer(this);
            _timer.Start();
        }

        public PreviewHouse(Serial serial) : base(serial)
        {
        }

        public override void OnDelete()
        {
            base.OnDelete();
            CleanupMarkers();
            _timer?.Stop();
            _timer = null;

            // Clear viewer references
            _viewers.Clear();
        }

        public override void OnLocationChange(Point3D oldLocation)
        {
            base.OnLocationChange(oldLocation);

            // If we've just been moved into the world, spawn markers now
            EnsureMarkersSpawned();

            if (_markers.Count == 0)
                return;

            int dx = X - oldLocation.X;
            int dy = Y - oldLocation.Y;
            int dz = Z - oldLocation.Z;

            for (int i = 0; i < _markers.Count; ++i)
            {
                var item = _markers[i];
                item.MoveToWorld(new Point3D(item.X + dx, item.Y + dy, item.Z + dz), Map);
            }

            RevalidateAndColorize();
        }

        private void EnsureMarkersSpawned()
        {
            if (_markersSpawned || Map == null || Map == Map.Internal)
                return;

            SpawnMarkers();
            _markersSpawned = true;
            RevalidateAndColorize();
        }

        private void SpawnMarkers()
        {
            CleanupMarkers();

            // Center fill markers (rectangle)
            for (int rx = _minX; rx <= _maxX; rx++)
            {
                for (int ry = _minY; ry <= _maxY; ry++)
                {
                    PlaceMarker(FillItemID, rx, ry);
                }
            }

            // Exactly one ring around the rectangle (1-tile out)
            int minX = _minX - 1;
            int maxX = _maxX + 1;
            int minY = _minY - 1;
            int maxY = _maxY + 1;

            for (int rx = minX; rx <= maxX; rx++)
            {
                PlaceMarker(RingItemID, rx, minY);
                PlaceMarker(RingItemID, rx, maxY);
            }
            for (int ry = minY + 1; ry <= maxY - 1; ry++)
            {
                PlaceMarker(RingItemID, minX, ry);
                PlaceMarker(RingItemID, maxX, ry);
            }
        }

        private void PlaceMarker(int itemID, int relX, int relY)
        {
            int wx = X + relX;
            int wy = Y + relY;
            int wz = GetGroundZ(wx, wy);

            var item = new PreviewMarkerItem(itemID) { Hue = HueValid };
            item.MoveToWorld(new Point3D(wx, wy, wz), Map);
            _markers.Add(item);
        }

        private int GetGroundZ(int x, int y)
        {
            try { return Map?.GetAverageZ(x, y) ?? Z; }
            catch { return Z; }
        }

        private void CleanupMarkers()
        {
            for (int i = 0; i < _markers.Count; i++)
            {
                try { _markers[i].Delete(); } catch { }
            }
            _markers.Clear();
        }

        private void ColorizeAll(int hue)
        {
            for (int i = 0; i < _markers.Count; i++)
            {
                try { _markers[i].Hue = hue; } catch { }
            }
        }

        public bool RevalidateAndColorize()
        {
            // IMPORTANT: This function updates marker colors and validity flags only.
            // It DOES NOT delete this preview if invalid. Deletion is controlled by viewer actions
            // (closing/canceling the gump) or by the lifetime timer when viewer(s) disconnect / move away.
            if (Map == null || Map == Map.Internal)
            {
                ColorizeAll(HueInvalid);
                CenterAreaValid = false;
                SurroundingRingValid = false;
                _invalidCenterTiles = new HashSet<Point2D>();
                _invalidRingTiles = new HashSet<Point2D>();
                return false;
            }

            var invalid = new HashSet<(int x, int y)>();
            var centerInvalid = new HashSet<Point2D>();
            var ringInvalid = new HashSet<Point2D>();

            bool centerHasInvalid = false;
            bool ringHasInvalid = false;

            // Center rectangle
            for (int rx = _minX; rx <= _maxX; rx++)
            {
                for (int ry = _minY; ry <= _maxY; ry++)
                {
                    int x = X + rx, y = Y + ry;
                    if (!TileIsPlaceable(x, y))
                    {
                        invalid.Add((x, y));
                        centerInvalid.Add(new Point2D(x, y));
                        centerHasInvalid = true;
                    }
                }
            }

            // One ring
            int rMinX = _minX - 1;
            int rMaxX = _maxX + 1;
            int rMinY = _minY - 1;
            int rMaxY = _maxY + 1;

            for (int rx = rMinX; rx <= rMaxX; rx++)
            {
                int xTop = X + rx, yTop = Y + rMinY;
                int xBot = X + rx, yBot = Y + rMaxY;

                if (!TileIsRingOK(xTop, yTop)) { invalid.Add((xTop, yTop)); ringInvalid.Add(new Point2D(xTop, yTop)); ringHasInvalid = true; }
                if (!TileIsRingOK(xBot, yBot)) { invalid.Add((xBot, yBot)); ringInvalid.Add(new Point2D(xBot, yBot)); ringHasInvalid = true; }
            }
            for (int ry = rMinY + 1; ry <= rMaxY - 1; ry++)
            {
                int xL = X + rMinX, yL = Y + ry;
                int xR = X + rMaxX, yR = Y + ry;

                if (!TileIsRingOK(xL, yL)) { invalid.Add((xL, yL)); ringInvalid.Add(new Point2D(xL, yL)); ringHasInvalid = true; }
                if (!TileIsRingOK(xR, yR)) { invalid.Add((xR, yR)); ringInvalid.Add(new Point2D(xR, yR)); ringHasInvalid = true; }
            }

            // Color markers
            foreach (var item in _markers)
            {
                try
                {
                    var p = item.Location;
                    item.Hue = invalid.Contains((p.X, p.Y)) ? HueInvalid : HueValid;
                }
                catch { }
            }

            CenterAreaValid = !centerHasInvalid;
            SurroundingRingValid = !ringHasInvalid;

            _invalidCenterTiles = centerInvalid;
            _invalidRingTiles = ringInvalid;

            // Return center validity (UI decides the label)
            return CenterAreaValid;
        }

        // Full validity: center + ring must be valid
        public bool FootprintIsValidAt(Point3D center)
        {
            if (Map == null || Map == Map.Internal)
                return false;

            // center
            for (int rx = _minX; rx <= _maxX; rx++)
                for (int ry = _minY; ry <= _maxY; ry++)
                    if (!TileIsPlaceable(center.X + rx, center.Y + ry))
                        return false;

            // ring
            int rMinX = _minX - 1;
            int rMaxX = _maxX + 1;
            int rMinY = _minY - 1;
            int rMaxY = _maxY + 1;

            for (int rx = rMinX; rx <= rMaxX; rx++)
            {
                if (!TileIsRingOK(center.X + rx, center.Y + rMinY)) return false;
                if (!TileIsRingOK(center.X + rx, center.Y + rMaxY)) return false;
            }
            for (int ry = rMinY + 1; ry <= rMaxY - 1; ry++)
            {
                if (!TileIsRingOK(center.X + rMinX, center.Y + ry)) return false;
                if (!TileIsRingOK(center.X + rMaxX, center.Y + ry)) return false;
            }

            return true;
        }

        // For the gump’s dot-panel logic
        public bool IsWorldPointInFootprint(int x, int y)
        {
            return x >= (X + _minX) && x <= (X + _maxX) && y >= (Y + _minY) && y <= (Y + _maxY);
        }

        public bool IsWorldPointInRing(int x, int y)
        {
            int rMinX = X + (_minX - 1);
            int rMaxX = X + (_maxX + 1);
            int rMinY = Y + (_minY - 1);
            int rMaxY = Y + (_maxY + 1);

            bool onHorizontal = (y == rMinY || y == rMaxY) && x >= rMinX && x <= rMaxX;
            bool onVertical = (x == rMinX || x == rMaxX) && y >= rMinY && y <= rMaxY;

            // Exclude the inner rectangle itself
            if (IsWorldPointInFootprint(x, y))
                return false;

            return onHorizontal || onVertical;
        }

        public bool IsCenterTileInvalid(int x, int y)
        {
            return _invalidCenterTiles.Contains(new Point2D(x, y));
        }

        public bool IsRingTileInvalid(int x, int y)
        {
            return _invalidRingTiles.Contains(new Point2D(x, y));
        }

        /// <summary>
        /// Attach a viewer (mobile) to this preview. Multiple viewers can watch a single preview;
        /// the preview will persist while at least one attached viewer is valid (connected, same map, and within range).
        /// </summary>
        public void AttachViewer(Mobile m)
        {
            if (m == null || m.Deleted)
                return;

            lock (_viewers)
            {
                _viewers.Add(m);
            }
        }

        /// <summary>
        /// Detach a previously attached viewer.
        /// </summary>
        public void DetachViewer(Mobile m)
        {
            if (m == null)
                return;

            lock (_viewers)
            {
                _viewers.Remove(m);
            }
        }

        /// <summary>
        /// Helper: checks whether at least one attached viewer is still present & within limits.
        /// </summary>
        private bool HasAnyValidViewer()
        {
            lock (_viewers)
            {
                if (_viewers.Count == 0)
                    return false;

                foreach (var v in _viewers)
                {
                    if (v == null) continue;
                    if (v.Deleted) continue;
                    if (v.NetState == null) continue;
                    if (v.Map != this.Map) continue;

                    int dx = Math.Abs(v.X - this.X);
                    int dy = Math.Abs(v.Y - this.Y);

                    if (dx <= 30 && dy <= 30)
                        return true;
                }
            }

            return false;
        }

        private bool TileIsPlaceable(int x, int y)
        {
            if (Map == null || Map == Map.Internal)
                return false;

            int z = GetGroundZ(x, y);
            var p = new Point3D(x, y, z);

            // Region restrictions
            Region r = Region.Find(p, Map);
            if (r != null)
            {
                if (r.IsPartOf("NoHousing") || r.IsPartOf("HouseRegion"))
                    return false;
            }

            // Land tile blockers (e.g., water/impassable)
            try
            {
                int landId = Map.Tiles.GetLandTile(x, y).ID;
                var landFlags = TileData.LandTable[landId].Flags;

                if ((landFlags & TileFlag.Impassable) != 0 || (landFlags & TileFlag.Wet) != 0)
                    return false;
            }
            catch { }

            // Static tile blockers (trees, rocks, etc.)
            try
            {
                var statics = Map.Tiles.GetStaticTiles(x, y);
                for (int i = 0; i < statics.Length; i++)
                {
                    int sid = statics[i].ID & 0x3FFF;
                    var sflags = TileData.ItemTable[sid].Flags;

                    if ((sflags & TileFlag.Impassable) != 0 ||
                        (sflags & TileFlag.Wet) != 0 ||
                        (sflags & TileFlag.Foliage) != 0)
                        return false;
                }
            }
            catch { }

            // Blockers (ignore our own preview multi and markers)
            try
            {
                foreach (Item item in Map.GetItemsInRange(p, 0))
                {
                    if (item.Deleted || !item.Visible)
                        continue;

                    if (ReferenceEquals(item, this))
                        continue;

                    if (PreviewMarkerItem.IsPreviewMarker(item))
                        continue;

                    bool impassable = (item.ItemData.Flags & TileFlag.Impassable) != 0;

                    if (item is BaseHouse || item is BaseMulti || item is AddonComponent || impassable)
                        return false;
                }
            }
            catch { return false; }

            // Nearby house 1-tile border
            if (BaseHouse.FindHouseAt(p, Map, 1) != null)
                return false;

            // Final Map.CanFit is deferred to HousePlacement.Check during Accept.
            return true;
        }

        private bool TileIsRingOK(int x, int y)
        {
            int z = GetGroundZ(x, y);
            var p = new Point3D(x, y, z);

            if (BaseHouse.FindHouseAt(p, Map, 0) != null)
                return false;

            // Land/static blockers color the ring too
            try
            {
                int landId = Map.Tiles.GetLandTile(x, y).ID;
                var landFlags = TileData.LandTable[landId].Flags;
                if ((landFlags & TileFlag.Impassable) != 0 || (landFlags & TileFlag.Wet) != 0)
                    return false;
            }
            catch { }

            try
            {
                var statics = Map.Tiles.GetStaticTiles(x, y);
                for (int i = 0; i < statics.Length; i++)
                {
                    int sid = statics[i].ID & 0x3FFF;
                    var sflags = TileData.ItemTable[sid].Flags;
                    if ((sflags & TileFlag.Impassable) != 0 ||
                        (sflags & TileFlag.Wet) != 0 ||
                        (sflags & TileFlag.Foliage) != 0)
                        return false;
                }
            }
            catch { }

            try
            {
                foreach (Item item in Map.GetItemsInRange(p, 0))
                {
                    if (item.Deleted || !item.Visible)
                        continue;

                    if (ReferenceEquals(item, this))
                        continue;

                    if (PreviewMarkerItem.IsPreviewMarker(item))
                        continue;

                    bool impassable = (item.ItemData.Flags & TileFlag.Impassable) != 0;
                    if (item is BaseHouse || item is BaseMulti || impassable)
                        return false;
                }
            }
            catch { return false; }

            return true;
        }

        private class PreviewLifetimeTimer : Timer
        {
            private readonly PreviewHouse _house;

            public PreviewLifetimeTimer(PreviewHouse house) : base(TimeSpan.FromSeconds(5.0), TimeSpan.FromSeconds(5.0))
            {
                _house = house;
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                if (_house == null || _house.Deleted)
                {
                    Stop();
                    return;
                }

                try
                {
                    // If any attached viewer is valid (connected, same map, within range), keep the preview.
                    if (_house.HasAnyValidViewer())
                        return;

                    // No valid viewers attached: do nothing (we want previews to persist until explicitly cancelled or until
                    // a previously attached viewer becomes disconnected/out-of-range — in that case the viewer removal path will
                    // cause the preview to be deleted). To be safe, we do not auto-delete previews just because no viewers are attached,
                    // as previews may be spawned programmatically or by staff for inspection.

                    // However, we also handle the case where a viewer was attached previously but now all attached viewers are gone/invalid.
                    // In that scenario, delete the preview so abandoned previews don't linger indefinitely.
                    //
                    // Decision: delete when there are attached viewers but none remain valid. If there were never any attached viewers, keep it.
                    bool hadAnyAttached = false;
                    lock (_house._viewers)
                    {
                        hadAnyAttached = _house._viewers.Count > 0;
                    }

                    if (hadAnyAttached && !_house.HasAnyValidViewer())
                    {
                        // All attached viewers are now invalid/disconnected/out-of-range -> delete preview
                        _house.Delete();
                        return;
                    }

                    // Otherwise (no attached viewers ever) do nothing.
                }
                catch
                {
                    try { _house.Delete(); } catch { }
                }
            }
        }
    }
}