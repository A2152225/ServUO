using System;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Items;
using Server.Multis;

namespace Server.Multis
{
    public class HousePreviewNudgeGump : Gump
    {
        // Dot hues
        private const int HueGreen = 63;  // valid
        private const int HueRed = 33;    // invalid
        private const int HueBlue = 2;    // clear terrain outside ring
        private const int HueText = 0;

        private const int DotSpacing = 8;

        private readonly WarningGumpCallback _callback;
        private readonly object _state;
        private readonly PreviewHouse _prev;
        private readonly Mobile _viewer;

        private readonly Func<Point3D, bool> _placementIsValidAt;

        private readonly int _scanRadius;
        private readonly int _panelW;
        private readonly int _panelH;
        private readonly int _leftPaneW;

        public HousePreviewNudgeGump(
            WarningGumpCallback callback,
            object state,
            PreviewHouse prev,
            Mobile viewer,
            Func<Point3D, bool> placementIsValidAt = null)
            : base(30, 30)
        {
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
            _state = state;
            _prev = prev ?? throw new ArgumentNullException(nameof(prev));
            _viewer = viewer;
            _placementIsValidAt = placementIsValidAt;

            int maxDim = Math.Max(_prev.PreviewWidth, _prev.PreviewHeight);
            _scanRadius = Math.Max(22, maxDim + 2);

            _panelW = (2 * _scanRadius + 1) * DotSpacing;
            _panelH = _panelW;
            _leftPaneW = 240;

            int gumpW = _leftPaneW + _panelW + 96;
            int gumpH = Math.Max(460, _panelH + 180);

            Closable = true;
            Dragable = true;

            AddPage(0);
            AddBackground(0, 0, gumpW, gumpH, 9270);
            AddAlphaRegion(8, 8, gumpW - 16, gumpH - 16);

            _prev.AttachViewer(_viewer);
            _prev.RevalidateAndColorize();

            // Evaluate validity with owner-aware overlap allowance.
            bool showValid = IsPreviewCenterAndRingValidConsideringOwner() && !_prev.HasExtendedInvalids();
            string statusHtmlColor = showValid ? "#008000" : "#A00000";
            string statusText = showValid ? "Placement: Valid" : "Placement: Invalid";

            AddHtml(16, 12, _leftPaneW - 32, 30,
                $"<CENTER><BASEFONT COLOR=\"{statusHtmlColor}\"><BIG><B>{statusText}</B></BIG></BASEFONT></CENTER>",
                false, false);

            if (!showValid && _viewer != null && _viewer.AccessLevel >= AccessLevel.GameMaster)
            {
                AddHtml(16, 46, _leftPaneW - 32, 20,
                    "<BASEFONT COLOR=\"#FFFFFF\">(Staff) You can still proceed.</BASEFONT>",
                    false, false);
            }

            AddHtml(16, 70, _leftPaneW - 32, 22,
                $"<BASEFONT COLOR=\"#FFFFFF\"><B>Center:</B> ({_prev.X}, {_prev.Y})</BASEFONT>",
                false, false);
            AddHtml(16, 96, _leftPaneW - 32, 22,
                $"<BASEFONT COLOR=\"#FFFFFF\"><B>Size:</B> {_prev.PreviewWidth} x {_prev.PreviewHeight}</BASEFONT>",
                false, false);
            AddHtml(16, 122, _leftPaneW - 32, 48,
                "<BASEFONT COLOR=\"#FFFFFF\">Legend: <B>Green</B>=OK, <B>Red</B>=invalid (including adjacency/buffer), <B>Blue</B>=clear terrain outside ring</BASEFONT>",
                false, false);

            // Nudge controls
            int bx = 18, by = 190, bw = 52, bh = 30, gap = 8;

            // Row 1
            AddButton(bx, by, 4014, 4016, 101, GumpButtonType.Reply, 0); AddHtml(bx + 6, by + 6, bw - 12, bh - 12, "<BASEFONT COLOR=\"#FFFFFF\">NW</BASEFONT>", false, false);
            AddButton(bx + (bw + gap), by, 4014, 4016, 102, GumpButtonType.Reply, 0); AddHtml(bx + (bw + gap) + 6, by + 6, bw - 12, bh - 12, "<BASEFONT COLOR=\"#FFFFFF\">N</BASEFONT>", false, false);
            AddButton(bx + 2 * (bw + gap), by, 4014, 4016, 103, GumpButtonType.Reply, 0); AddHtml(bx + 2 * (bw + gap) + 6, by + 6, bw - 12, bh - 12, "<BASEFONT COLOR=\"#FFFFFF\">NE</BASEFONT>", false, false);

            // Row 2
            AddButton(bx, by + (bh + gap), 4014, 4016, 104, GumpButtonType.Reply, 0); AddHtml(bx + 6, by + (bh + gap) + 6, bw - 12, bh - 12, "<BASEFONT COLOR=\"#FFFFFF\">W</BASEFONT>", false, false);
            AddHtml(bx + (bw + gap), by + (bh + gap) + 6, bw - 12, bh - 12, "<BASEFONT COLOR=\"#FFFFFF\">·</BASEFONT>", false, false);
            AddButton(bx + 2 * (bw + gap), by + (bh + gap), 4014, 4016, 105, GumpButtonType.Reply, 0); AddHtml(bx + 2 * (bw + gap) + 6, by + (bh + gap) + 6, bw - 12, bh - 12, "<BASEFONT COLOR=\"#FFFFFF\">E</BASEFONT>", false, false);

            // Row 3
            AddButton(bx, by + 2 * (bh + gap), 4014, 4016, 106, GumpButtonType.Reply, 0); AddHtml(bx + 6, by + 2 * (bh + gap) + 6, bw - 12, bh - 12, "<BASEFONT COLOR=\"#FFFFFF\">SW</BASEFONT>", false, false);
            AddButton(bx + (bw + gap), by + 2 * (bh + gap), 4014, 4016, 107, GumpButtonType.Reply, 0); AddHtml(bx + (bw + gap) + 6, by + 2 * (bh + gap) + 6, bw - 12, bh - 12, "<BASEFONT COLOR=\"#FFFFFF\">S</BASEFONT>", false, false);
            AddButton(bx + 2 * (bw + gap), by + 2 * (bh + gap), 4014, 4016, 108, GumpButtonType.Reply, 0); AddHtml(bx + 2 * (bw + gap) + 6, by + 2 * (bh + gap) + 6, bw - 12, bh - 12, "<BASEFONT COLOR=\"#FFFFFF\">SE</BASEFONT>", false, false);

            // Dot panel
            int panelX = _leftPaneW + 20;
            int panelY = 18;

            AddBackground(panelX - 12, panelY - 12, _panelW + 24, _panelH + 24, 9270);
            AddAlphaRegion(panelX - 10, panelY - 10, _panelW + 20, _panelH + 20);

            AddHtml(panelX, panelY - 24, 300, 20, "<BASEFONT COLOR=\"#FFFFFF\"><B>Nearby placements</B></BASEFONT>", false, false);

            RenderDotPanel(panelX, panelY, _panelW, _panelH);

            int legendY = panelY + _panelH + 8;
            AddHtml(panelX, legendY, 320, 22, $"<BASEFONT COLOR=\"#008000\"><B>Green:</B></BASEFONT> <BASEFONT COLOR=\"#FFFFFF\">OK (footprint/ring)</BASEFONT>", false, false);
            AddHtml(panelX + 220, legendY, 320, 22, $"<BASEFONT COLOR=\"#5678FF\"><B>Blue:</B></BASEFONT> <BASEFONT COLOR=\"#FFFFFF\">clear outside ring</BASEFONT>", false, false);
            AddHtml(panelX, legendY + 24, 320, 22, $"<BASEFONT COLOR=\"#A00000\"><B>Red:</B></BASEFONT> <BASEFONT COLOR=\"#FFFFFF\">invalid (blocker / adjacency / buffer)</BASEFONT>", false, false);

            int bottomY = gumpH - 80;
            int acceptX = 18;
            int cancelX = acceptX + 160;

            AddButton(acceptX, bottomY, 4005, 4007, 201, GumpButtonType.Reply, 0);
            AddHtml(acceptX + 46, bottomY + 4, 100, 20, "<BASEFONT COLOR=\"#008000\"><B>Accept</B></BASEFONT>", false, false);
            AddButton(cancelX, bottomY, 4017, 4019, 202, GumpButtonType.Reply, 0);
            AddHtml(cancelX + 46, bottomY + 4, 100, 20, "<BASEFONT COLOR=\"#A00000\"><B>Cancel</B></BASEFONT>", false, false);
        }

        private void RenderDotPanel(int x, int y, int w, int h)
        {
            if (_prev == null || _prev.Deleted || _prev.Map == null || _prev.Map == Map.Internal)
                return;

            var map = _prev.Map;
            int cx = _prev.X;
            int cy = _prev.Y;

            for (int dy = -_scanRadius; dy <= _scanRadius; dy++)
            {
                for (int dx = -_scanRadius; dx <= _scanRadius; dx++)
                {
                    int px = x + (dx + _scanRadius) * DotSpacing;
                    int py = y + (dy + _scanRadius) * DotSpacing;

                    int wx = cx + dx;
                    int wy = cy + dy;

                    if (_prev.IsWorldPointInFootprint(wx, wy))
                    {
                        // Allow owner-aware house overlap: treat hard-invalids that are house-owned by the viewer as valid.
                        bool centerInvalid = _prev.IsCenterTileInvalid(wx, wy);
                        bool hardInvalid = _prev.IsWorldPointHardInvalid(wx, wy);

                        // If the tile is center-invalid but the only blocking thing is an overlappable house, allow it.
                        if (centerInvalid && IsHouseOverlapAllowedAt(map, wx, wy, _viewer))
                            centerInvalid = false;

                        if (hardInvalid && IsHouseOverlapAllowedAt(map, wx, wy, _viewer))
                            hardInvalid = false;

                        bool invalid = centerInvalid || hardInvalid;
                        int hue = invalid ? HueRed : HueGreen;
                        AddLabelCropped(px, py, DotSpacing + 2, DotSpacing + 2, hue, "•");
                    }
                    else if (_prev.IsWorldPointInRing(wx, wy))
                    {
                        bool ringInvalid = _prev.IsRingTileInvalid(wx, wy);
                        bool hardInvalid = _prev.IsWorldPointHardInvalid(wx, wy);

                        // Clear ringInvalid if it's only due to an overlappable house
                        if (ringInvalid && IsHouseOverlapAllowedAt(map, wx, wy, _viewer))
                            ringInvalid = false;

                        if (hardInvalid && IsHouseOverlapAllowedAt(map, wx, wy, _viewer))
                            hardInvalid = false;

                        bool invalid = ringInvalid || hardInvalid;
                        AddLabelCropped(px, py, DotSpacing + 2, DotSpacing + 2, invalid ? HueRed : HueGreen, "•");
                    }
                    else
                    {
                        if (_prev.IsWorldPointHardInvalid(wx, wy))
                        {
                            // If it's a hard invalid but due solely to a house the viewer owns, allow it visually as non-blocking.
                            if (IsHouseOverlapAllowedAt(map, wx, wy, _viewer))
                            {
                                // show as green (or maybe blue) to indicate allowed overlap; choose green for consistency.
                                AddLabelCropped(px, py, DotSpacing + 2, DotSpacing + 2, HueGreen, "•");
                            }
                            else
                            {
                                AddLabelCropped(px, py, DotSpacing + 2, DotSpacing + 2, HueRed, "•");
                            }
                        }
                        else if (IsTerrainClear(map, wx, wy))
                        {
                            AddLabelCropped(px, py, DotSpacing + 2, DotSpacing + 2, HueBlue, "•");
                        }
                    }
                }
            }

            // Mark center
            int centerPx = x + _scanRadius * DotSpacing;
            int centerPy = y + _scanRadius * DotSpacing;
            AddLabelCropped(centerPx, centerPy - 6, DotSpacing + 2, DotSpacing + 2, HueText, "X");
        }

        private static bool IsTerrainClear(Map map, int x, int y)
        {
            if (map == null || map == Map.Internal)
                return false;

            try
            {
                int landId = map.Tiles.GetLandTile(x, y).ID;
                var landFlags = TileData.LandTable[landId].Flags;
                if ((landFlags & TileFlag.Impassable) != 0 || (landFlags & TileFlag.Wet) != 0)
                    return false;
            }
            catch { }

            try
            {
                var statics = map.Tiles.GetStaticTiles(x, y);
                for (int i = 0; i < statics.Length; i++)
                {
                    int sid = statics[i].ID & TileData.MaxItemValue;
                    var flags = TileData.ItemTable[sid].Flags;

                    if ((flags & TileFlag.Impassable) != 0 ||
                        (flags & TileFlag.Wet) != 0 ||
                        (flags & TileFlag.Foliage) != 0)
                        return false;
                }
            }
            catch { }

            try
            {
                int z = 0, avg = 0, top = 0;
                map.GetAverageZ(x, y, ref z, ref avg, ref top);
                var p = new Point3D(x, y, avg);

                foreach (Item item in map.GetItemsInRange(p, 0))
                {
                    if (item.Deleted || !item.Visible)
                        continue;
                    if (item is PreviewMarkerItem)
                        continue;

                    bool impassable = (item.ItemData.Flags & TileFlag.Impassable) != 0;

                    if (impassable || item is BaseHouse || item is BaseMulti || item is AddonComponent)
                        return false;
                }
            }
            catch { return false; }

            return true;
        }

        // Scans items at (x,y) and returns the first BaseHouse/BaseMulti found, or null.
        private static BaseHouse FindHouseAtPoint(Map map, int x, int y)
        {
            if (map == null || map == Map.Internal)
                return null;

            try
            {
                int z = 0, avg = 0, top = 0;
                map.GetAverageZ(x, y, ref z, ref avg, ref top);
                var p = new Point3D(x, y, avg);

                foreach (Item item in map.GetItemsInRange(p, 0))
                {
                    if (item == null || item.Deleted)
                        continue;
                    if (item is BaseHouse house)
                        return house;
                    if (item is BaseMulti multi) // some house implementations use BaseMulti
                    {
                        // Try to cast to BaseHouse if appropriate
                        if (multi is BaseHouse asHouse)
                            return asHouse;
                    }
                }
            }
            catch { }

            return null;
        }

        // Returns true if the only blocking item at the point is a house that 'viewer' is allowed to overlap.
        private static bool IsHouseOverlapAllowedAt(Map map, int x, int y, Mobile viewer)
        {
            try
            {
                BaseHouse h = FindHouseAtPoint(map, x, y);
                if (h == null)
                    return false;

                // Use the centralized helper added in your HousingUpdate branch.
                return HousePlacementHelper.CanOverlapHouse(h, viewer);
            }
            catch
            {
                return false;
            }
        }

        // Re-evaluate center and ring area validity using the same owner-aware allowance used by RenderDotPanel.
        // This function is used to determine the overall "Placement: Valid/Invalid" status shown at the top of the gump
        // and to influence the Accept button behavior.
        private bool IsPreviewCenterAndRingValidConsideringOwner()
        {
            if (_prev == null || _prev.Deleted || _prev.Map == null || _prev.Map == Map.Internal)
                return false;

            var map = _prev.Map;
            int cx = _prev.X;
            int cy = _prev.Y;

            for (int dy = -_scanRadius; dy <= _scanRadius; dy++)
            {
                for (int dx = -_scanRadius; dx <= _scanRadius; dx++)
                {
                    int wx = cx + dx;
                    int wy = cy + dy;

                    if (_prev.IsWorldPointInFootprint(wx, wy))
                    {
                        bool centerInvalid = _prev.IsCenterTileInvalid(wx, wy);
                        bool hardInvalid = _prev.IsWorldPointHardInvalid(wx, wy);

                        // Allow owner-overlap for center tiles too if the only blocking object is an overlappable house
                        if (centerInvalid && IsHouseOverlapAllowedAt(map, wx, wy, _viewer))
                            centerInvalid = false;

                        if (hardInvalid && IsHouseOverlapAllowedAt(map, wx, wy, _viewer))
                            hardInvalid = false;

                        if (centerInvalid || hardInvalid)
                            return false;
                    }
                    else if (_prev.IsWorldPointInRing(wx, wy))
                    {
                        bool ringInvalid = _prev.IsRingTileInvalid(wx, wy);
                        bool hardInvalid = _prev.IsWorldPointHardInvalid(wx, wy);

                        // Allow owner-overlap for ring tiles as well
                        if (ringInvalid && IsHouseOverlapAllowedAt(map, wx, wy, _viewer))
                            ringInvalid = false;

                        if (hardInvalid && IsHouseOverlapAllowedAt(map, wx, wy, _viewer))
                            hardInvalid = false;

                        if (ringInvalid || hardInvalid)
                            return false;
                    }
                }
            }

            return true;
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            var from = sender.Mobile;
            if (_prev == null || _prev.Deleted)
                return;

            if (info.ButtonID == 0)
            {
                try { _prev.Delete(); } catch { }
                return;
            }

            switch (info.ButtonID)
            {
                case 101: _prev.MoveToWorld(new Point3D(_prev.X - 1, _prev.Y - 1, _prev.Z), _prev.Map); break;
                case 102: _prev.MoveToWorld(new Point3D(_prev.X + 0, _prev.Y - 1, _prev.Z), _prev.Map); break;
                case 103: _prev.MoveToWorld(new Point3D(_prev.X + 1, _prev.Y - 1, _prev.Z), _prev.Map); break;
                case 104: _prev.MoveToWorld(new Point3D(_prev.X - 1, _prev.Y + 0, _prev.Z), _prev.Map); break;
                case 105: _prev.MoveToWorld(new Point3D(_prev.X + 1, _prev.Y + 0, _prev.Z), _prev.Map); break;
                case 106: _prev.MoveToWorld(new Point3D(_prev.X - 1, _prev.Y + 1, _prev.Z), _prev.Map); break;
                case 107: _prev.MoveToWorld(new Point3D(_prev.X + 0, _prev.Y + 1, _prev.Z), _prev.Map); break;
                case 108: _prev.MoveToWorld(new Point3D(_prev.X + 1, _prev.Y + 1, _prev.Z), _prev.Map); break;

                case 201:
                    {
                        if (_prev == null || _prev.Deleted)
                        {
                            from.SendMessage("Preview no longer available.");
                            return;
                        }

                        // Use owner-aware center/ring validation when deciding whether to allow acceptance.
                        bool fullyValid = IsPreviewCenterAndRingValidConsideringOwner() && !_prev.HasExtendedInvalids();

                        if (from.AccessLevel < AccessLevel.GameMaster && !fullyValid)
                        {
                            from.SendMessage("Cannot accept: placement fails extended validity (adjacency or foreign house buffer) or center/ring contains invalid tiles.");
                            from.CloseGump<HousePreviewNudgeGump>();
                            from.SendGump(new HousePreviewNudgeGump(_callback, _state, _prev, _viewer, _placementIsValidAt));
                            return;
                        }

                        // Proceed to confirmation (staff can override)
                        from.SendGump(new WarningGump(1060635, 30720, 1049583, 32512, 420, 280, _callback, _state));
                        return;
                    }
                case 202:
                    {
                        try { _prev.Delete(); } catch { }
                        return;
                    }
            }

            _prev.RevalidateAndColorize();
            from.CloseGump<HousePreviewNudgeGump>();
            from.SendGump(new HousePreviewNudgeGump(_callback, _state, _prev, _viewer, _placementIsValidAt));
        }
    }
}