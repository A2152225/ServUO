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

        // Extended invalidity (adjacency + foreign house buffer)
        private HashSet<Point2D> _invalidExtendedTiles = new HashSet<Point2D>();

        // Track invalid tiles for the most recent validation
        private HashSet<Point2D> _invalidCenterTiles = new HashSet<Point2D>();
        private HashSet<Point2D> _invalidRingTiles = new HashSet<Point2D>();

        // Viewer tracking
        private readonly HashSet<Mobile> _viewers = new HashSet<Mobile>();

        public int PreviewWidth => (_maxX - _minX + 1);
        public int PreviewHeight => (_maxY - _minY + 1);

        // NEW: Owner for account-based adjacency & foreign buffer exceptions
        public Mobile Owner { get; }

        // Foreign house buffer distance must match HousePlacement (Chebyshev)
        private const int ForeignHouseBuffer = 3;

        public PreviewHouse(int multiID, Mobile owner) : this(multiID)
        {
            Owner = owner;
        }

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

            // Defer marker spawn slightly; by then MoveToWorld should be done.
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
            _viewers.Clear();
        }

        public override void OnLocationChange(Point3D oldLocation)
        {
            base.OnLocationChange(oldLocation);

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

            // Center fill markers
            for (int rx = _minX; rx <= _maxX; rx++)
            {
                for (int ry = _minY; ry <= _maxY; ry++)
                {
                    PlaceMarker(FillItemID, rx, ry);
                }
            }

            // One ring
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

        // Re-validates footprint + ring and computes extended invalid tiles (adjacency + foreign house buffer)
        public bool RevalidateAndColorize()
        {
            if (Map == null || Map == Map.Internal)
            {
                ColorizeAll(HueInvalid);
                CenterAreaValid = false;
                SurroundingRingValid = false;
                _invalidCenterTiles = new HashSet<Point2D>();
                _invalidRingTiles = new HashSet<Point2D>();
                _invalidExtendedTiles = new HashSet<Point2D>();
                return false;
            }

            var centerInvalid = new HashSet<Point2D>();
            var ringInvalid = new HashSet<Point2D>();
            var extendedInvalid = new HashSet<Point2D>();

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
                        centerInvalid.Add(new Point2D(x, y));
                        centerHasInvalid = true;
                    }
                }
            }

            // Ring
            int rMinX = _minX - 1;
            int rMaxX = _maxX + 1;
            int rMinY = _minY - 1;
            int rMaxY = _maxY + 1;

            for (int rx = rMinX; rx <= rMaxX; rx++)
            {
                int xTop = X + rx; int yTop = Y + rMinY;
                int xBot = X + rx; int yBot = Y + rMaxY;

                if (!TileIsRingOK(xTop, yTop)) { ringInvalid.Add(new Point2D(xTop, yTop)); ringHasInvalid = true; }
                if (!TileIsRingOK(xBot, yBot)) { ringInvalid.Add(new Point2D(xBot, yBot)); ringHasInvalid = true; }
            }
            for (int ry = rMinY + 1; ry <= rMaxY - 1; ry++)
            {
                int xL = X + rMinX; int yL = Y + ry;
                int xR = X + rMaxX; int yR = Y + ry;

                if (!TileIsRingOK(xL, yL)) { ringInvalid.Add(new Point2D(xL, yL)); ringHasInvalid = true; }
                if (!TileIsRingOK(xR, yR)) { ringInvalid.Add(new Point2D(xR, yR)); ringHasInvalid = true; }
            }

            // Extended: adjacency to unwalkable for any invalid tile (center or ring) OR within foreign house buffer
            HashSet<Point2D> allStructureTiles = GetFootprintAndRingTiles();

            // First: adjacency to impassable tiles near the structure footprint/ring
            foreach (var t in allStructureTiles)
            {
                if (IsUnwalkable(Map, t.X, t.Y))
                {
                    extendedInvalid.Add(t);
                    AddAdjacent8(t, extendedInvalid);
                }
            }

            // Then add adjacency around tiles already invalid (center/ring) if they represent blockers
            foreach (var t in centerInvalid)
                AddAdjacent8(t, extendedInvalid);
            foreach (var t in ringInvalid)
                AddAdjacent8(t, extendedInvalid);

            // Foreign house buffer (Chebyshev) excluding same-account houses
            foreach (var t in allStructureTiles)
            {
                if (ForeignHouseViolationAt(t.X, t.Y))
                {
                    extendedInvalid.Add(t);
                    AddAdjacent8(t, extendedInvalid);
                }
            }

            // Color footprint + ring markers
            foreach (var marker in _markers)
            {
                try
                {
                    var p = marker.Location;
                    Point2D pt = new Point2D(p.X, p.Y);
                    if (centerInvalid.Contains(pt) || ringInvalid.Contains(pt))
                        marker.Hue = HueInvalid;
                    else
                        marker.Hue = HueValid;
                }
                catch { }
            }

            CenterAreaValid = !centerHasInvalid;
            SurroundingRingValid = !ringHasInvalid;

            _invalidCenterTiles = centerInvalid;
            _invalidRingTiles = ringInvalid;
            _invalidExtendedTiles = extendedInvalid;

            return CenterAreaValid;
        }

        // Full validity (center + ring + no extended invalid inside structural area)
        public bool FootprintIsValidAt(Point3D center)
        {
            if (Map == null || Map == Map.Internal)
                return false;

            // Re-run base logic using existing sets (already recalculated on moves)
            // If any center/ring invalid -> false
            if (!CenterAreaValid || !SurroundingRingValid)
                return false;

            // If any extended invalid tile overlaps the footprint or ring, treat invalid
            foreach (var t in _invalidExtendedTiles)
            {
                if (IsWorldPointInFootprint(t.X, t.Y) || IsWorldPointInRing(t.X, t.Y))
                    return false;
            }

            return true;
        }

        public bool HasExtendedInvalids() => _invalidExtendedTiles.Count > 0;

        // Footprint test
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

            if (IsWorldPointInFootprint(x, y))
                return false;

            return onHorizontal || onVertical;
        }

        public bool IsCenterTileInvalid(int x, int y) => _invalidCenterTiles.Contains(new Point2D(x, y));
        public bool IsRingTileInvalid(int x, int y) => _invalidRingTiles.Contains(new Point2D(x, y));

        // Red outside ring (extended)
        public bool IsWorldPointHardInvalid(int x, int y) => _invalidExtendedTiles.Contains(new Point2D(x, y));

        public void AttachViewer(Mobile m)
        {
            if (m == null || m.Deleted)
                return;

            lock (_viewers)
            {
                _viewers.Add(m);
            }
        }

        public void DetachViewer(Mobile m)
        {
            if (m == null)
                return;

            lock (_viewers)
            {
                _viewers.Remove(m);
            }
        }

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

        // --- Internal validation helpers ---

        private bool TileIsPlaceable(int x, int y)
        {
            if (Map == null || Map == Map.Internal)
                return false;

            int z = GetGroundZ(x, y);
            var p = new Point3D(x, y, z);

            // Region housing permission
            Region r = Region.Find(p, Map);
            if (r != null && !r.AllowHousing(Owner ?? (Mobile)null, p))
                return false;

            // Land
            try
            {
                int landId = Map.Tiles.GetLandTile(x, y).ID;
                var landFlags = TileData.LandTable[landId].Flags;

                if ((landFlags & TileFlag.Impassable) != 0 || (landFlags & TileFlag.Wet) != 0)
                    return false;
            }
            catch { }

            // Statics
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

            // Items
            try
            {
                foreach (Item item in Map.GetItemsInRange(p, 0))
                {
                    if (item == null || item.Deleted || !item.Visible)
                        continue;

                    // Skip the preview instance and any preview marker items so the preview doesn't block itself
                    if (ReferenceEquals(item, this))
                        continue;

                    if (item is PreviewHouse) // in case preview object is present as an item
                        continue;

                    if (PreviewMarkerItem.IsPreviewMarker(item))
                        continue;

                    if (PreviewMarkerItem.IsPreviewMarker(item))
                        continue;

                    if (PreviewMarkerItem.IsPreviewMarker(item))
                        continue;

                    if (PreviewMarkerItem.IsPreviewMarker(item))
                        continue;

                    if (PreviewMarkerItem.IsPreviewMarker(item))
                        continue;

                    if (PreviewMarkerItem.IsPreviewMarker(item))
                        continue;

                    if (PreviewMarkerItem.IsPreviewMarker(item))
                        continue;

                    if (PreviewMarkerItem.IsPreviewMarker(item))
                        continue;

                    if (PreviewMarkerItem.IsPreviewMarker(item))
                        continue;

                    if (PreviewMarkerItem.IsPreviewMarker(item))
                        continue;

                    if (PreviewMarkerItem.IsPreviewMarker(item))
                        continue;

                    if (PreviewMarkerItem.IsPreviewMarker(item))
                        continue;

                    if (PreviewMarkerItem.IsPreviewMarker(item))
                        continue;

                    if (PreviewMarkerItem.IsPreviewMarker(item))
                        continue;

                    if (PreviewMarkerItem.IsPreviewMarker(item))
                        continue;

                    if (PreviewMarkerItem.IsPreviewMarker(item))
                        continue;

                    if (PreviewMarkerItem.IsPreviewMarker(item))
                        continue;

                    // Normal checks below
                    bool impassable = (item.ItemData.Flags & TileFlag.Impassable) != 0;

                    // If the item is an existing house, consult centralized helper to allow owner's overlap.
                    if (item is BaseHouse existingHouse)
                    {
                        try
                        {
                            if (HousePlacementHelper.CanOverlapHouse(existingHouse, Owner ?? (Mobile)null))
                                continue; // allowed overlap -> ignore this house
                        }
                        catch { }

                        return false;
                    }

                    // Some implementations use BaseMulti; if it's a BaseHouse underneath, allow similar handling.
                    if (item is BaseMulti multiItem)
                    {
                        if (multiItem is BaseHouse multiAsHouse)
                        {
                            try
                            {
                                if (HousePlacementHelper.CanOverlapHouse(multiAsHouse, Owner ?? (Mobile)null))
                                    continue;
                            }
                            catch { }

                            return false;
                        }
                        // If BaseMulti is not a house-type in this shard, treat it as blocking
                        return false;
                    }

                    if (item is AddonComponent)
                        return false;

                    if (impassable)
                        return false;
                }
            }
            catch { return false; }

            // Too close to existing house (1 tile) - base rule (legacy)
            try
            {
                BaseHouse near = BaseHouse.FindHouseAt(p, Map, 1);
                if (near != null && !HousePlacementHelper.CanOverlapHouse(near, Owner ?? (Mobile)null))
                    return false;
            }
            catch { }

            return true;
        }

        private bool TileIsRingOK(int x, int y)
        {
            if (Map == null || Map == Map.Internal)
                return false;

            int z = GetGroundZ(x, y);
            var p = new Point3D(x, y, z);

            // Do not allow ring to overlay existing house tile (unless allowed for owner)
            try
            {
                BaseHouse atHouse = BaseHouse.FindHouseAt(p, Map, 0);
                if (atHouse != null && !HousePlacementHelper.CanOverlapHouse(atHouse, Owner ?? (Mobile)null))
                    return false;
            }
            catch { return false; }

            // Land/static
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
                    if (item == null || item.Deleted || !item.Visible)
                        continue;

                    // Skip the preview instance and any preview marker items so the preview doesn't block itself
                    if (ReferenceEquals(item, this))
                        continue;

                    if (item is PreviewHouse)
                        continue;

                    if (PreviewMarkerItem.IsPreviewMarker(item))
                        continue;

                    bool impassable = (item.ItemData.Flags & TileFlag.Impassable) != 0;

                    if (item is BaseHouse existingHouse)
                    {
                        try
                        {
                            if (HousePlacementHelper.CanOverlapHouse(existingHouse, Owner ?? (Mobile)null))
                                continue;
                        }
                        catch { }

                        return false;
                    }

                    if (item is BaseMulti multiItem)
                    {
                        if (multiItem is BaseHouse multiAsHouse)
                        {
                            try
                            {
                                if (HousePlacementHelper.CanOverlapHouse(multiAsHouse, Owner ?? (Mobile)null))
                                    continue;
                            }
                            catch { }

                            return false;
                        }

                        return false;
                    }

                    if (impassable)
                        return false;
                }
            }
            catch { return false; }

            return true;
        }

        private bool IsUnwalkable(Map map, int x, int y)
        {
            if (map == null || map == Map.Internal)
                return true;

            try
            {
                int landId = map.Tiles.GetLandTile(x, y).ID;
                var lf = TileData.LandTable[landId].Flags;
                if ((lf & TileFlag.Impassable) != 0 || (lf & TileFlag.Wet) != 0)
                    return true;
            }
            catch { }

            try
            {
                var statics = map.Tiles.GetStaticTiles(x, y);
                for (int i = 0; i < statics.Length; i++)
                {
                    int sid = statics[i].ID & 0x3FFF;
                    var flags = TileData.ItemTable[sid].Flags;
                    if ((flags & TileFlag.Impassable) != 0 ||
                        (flags & TileFlag.Wet) != 0 ||
                        (flags & TileFlag.Foliage) != 0)
                        return true;
                }
            }
            catch { }

            try
            {
                foreach (Item item in map.GetItemsInRange(new Point3D(x, y, 0), 0))
                {
                    if (item == null || item.Deleted || !item.Visible)
                        continue;

                    // Skip preview artifacts
                    if (ReferenceEquals(item, this))
                        continue;

                    if (item is PreviewHouse)
                        continue;

                    if (PreviewMarkerItem.IsPreviewMarker(item))
                        continue;

                    if ((item.ItemData.Flags & TileFlag.Impassable) != 0)
                        return true;
                }
            }
            catch { }

            return false;
        }

        private bool ForeignHouseViolationAt(int x, int y)
        {
            if (Map == null) return false;

            for (int dx = -ForeignHouseBuffer; dx <= ForeignHouseBuffer; dx++)
            {
                for (int dy = -ForeignHouseBuffer; dy <= ForeignHouseBuffer; dy++)
                {
                    BaseHouse h = BaseHouse.FindHouseAt(new Point3D(x + dx, y + dy, 0), Map, 16);
                    if (h == null)
                        continue;

                    if (Owner != null && Owner.Account != null && h.Owner != null && h.Owner.Account == Owner.Account)
                        continue; // same account

                    // ignore preview houses if any slipped into house-finding APIs
                    if (h is PreviewHouse)
                        continue;

                    // foreign
                    return true;
                }
            }
            return false;
        }

        private void AddAdjacent8(Point2D p, HashSet<Point2D> set)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0)
                        continue;
                    set.Add(new Point2D(p.X + dx, p.Y + dy));
                }
            }
        }

        private HashSet<Point2D> GetFootprintAndRingTiles()
        {
            var set = new HashSet<Point2D>();

            for (int rx = _minX; rx <= _maxX; rx++)
                for (int ry = _minY; ry <= _maxY; ry++)
                    set.Add(new Point2D(X + rx, Y + ry));

            int rMinX = _minX - 1;
            int rMaxX = _maxX + 1;
            int rMinY = _minY - 1;
            int rMaxY = _maxY + 1;

            for (int rx = rMinX; rx <= rMaxX; rx++)
            {
                set.Add(new Point2D(X + rx, Y + rMinY));
                set.Add(new Point2D(X + rx, Y + rMaxY));
            }
            for (int ry = rMinY + 1; ry <= rMaxY - 1; ry++)
            {
                set.Add(new Point2D(X + rMinX, Y + ry));
                set.Add(new Point2D(X + rMaxX, Y + ry));
            }

            return set;
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
                    if (_house.HasAnyValidViewer())
                        return;

                    bool hadAnyAttached = false;
                    lock (_house._viewers)
                    {
                        hadAnyAttached = _house._viewers.Count > 0;
                    }

                    if (hadAnyAttached && !_house.HasAnyValidViewer())
                    {
                        _house.Delete();
                        return;
                    }
                }
                catch
                {
                    try { _house.Delete(); } catch { }
                }
            }
        }
    }
}