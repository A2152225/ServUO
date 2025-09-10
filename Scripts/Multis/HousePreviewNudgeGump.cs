using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Items; // Needed for Item, BaseMulti, AddonComponent

namespace Server.Multis
{
    public class HousePreviewNudgeGump : Gump
    {
        // Dot hues (tweak if your shard palette differs)
        private const int HueGreen = 63;    // preview footprint tile valid / ring valid
        private const int HueRed = 33;      // preview footprint or ring tile invalid
        private const int HueBlue = 2;      // user requested hue 2 for blue dots
        // HueText is only used for single-label color (non-HTML); most text below is HTML-styled now.
        private const int HueText = 0;

        // Larger spacing for readability
        private const int DotSpacing = 8;   // pixels per tile

        private readonly WarningGumpCallback _callback;
        private readonly object _state;
        private readonly PreviewHouse _prev;
        private readonly Mobile _viewer;

        // Optional, kept for signature compatibility
        private readonly Func<Point3D, bool> _placementIsValidAt;

        // Computed panel size
        private readonly int _scanRadius;   // tiles from center to scan in each direction
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

            // Make the grid more readable: include at least the footprint + ring + one more tile
            int maxDim = Math.Max(_prev.PreviewWidth, _prev.PreviewHeight);
            _scanRadius = Math.Max(22, maxDim + 2); // slightly larger to ensure context

            _panelW = (2 * _scanRadius + 1) * DotSpacing;
            _panelH = _panelW; // square grid
            _leftPaneW = 240;  // increase left pane to fit readable text and larger buttons

            // Add some extra margins so nothing clips
            int gumpW = _leftPaneW + _panelW + 96;
            int gumpH = Math.Max(460, _panelH + 180);

            Closable = true;   // allow closing via the gump X - closing will delete the preview
            Dragable = true;

            AddPage(0);

            // Background: lighter backdrop and add a semi-transparent overlay for readability
            AddBackground(0, 0, gumpW, gumpH, 9270);
            AddAlphaRegion(8, 8, gumpW - 16, gumpH - 16); // subtle translucent inset

            // Attach the viewer so the preview lifetime can be monitored
            _prev.AttachViewer(_viewer);

            _prev.RevalidateAndColorize();

            // Label: Valid only when center AND ring are valid
            bool showValid = _prev.CenterAreaValid && _prev.SurroundingRingValid;
            string statusHtmlColor = showValid ? "#008000" : "#A00000"; // green/red
            string statusText = showValid ? "Placement: Valid" : "Placement: Invalid";

            // Big, HTML-styled heading (larger font, colored)
            AddHtml(16, 12, _leftPaneW - 32, 30,
                $"<CENTER><BASEFONT COLOR=\"{statusHtmlColor}\"><BIG><B>{statusText}</B></BIG></BASEFONT></CENTER>", false, false);

            // Make the subsequent text lighter (white) so it reads on the translucent background
            if (!showValid && _viewer != null && _viewer.AccessLevel >= AccessLevel.GameMaster)
            {
                AddHtml(16, 46, _leftPaneW - 32, 20, "<BASEFONT COLOR=\"#FFFFFF\">(Staff) You can still proceed to place.</BASEFONT>", false, false);
            }

            // Info lines with larger spacing and HTML formatting for clarity (use white text for readability)
            AddHtml(16, 70, _leftPaneW - 32, 22, $"<BASEFONT COLOR=\"#FFFFFF\"><B>Center:</B> ({_prev.X}, {_prev.Y})</BASEFONT>", false, false);
            AddHtml(16, 96, _leftPaneW - 32, 22, $"<BASEFONT COLOR=\"#FFFFFF\"><B>Size:</B> {_prev.PreviewWidth} x {_prev.PreviewHeight}</BASEFONT>", false, false);
            AddHtml(16, 122, _leftPaneW - 32, 48,
                "<BASEFONT COLOR=\"#FFFFFF\">Legend: <B>Green</B> = footprint/ring OK, <B>Red</B> = invalid, <B>Blue</B> = nearby terrain OK</BASEFONT>",
                false, false);

            // Nudge controls (bigger buttons and spaced)
            int bx = 18, by = 190, bw = 52, bh = 30, gap = 8;

            AddButton(bx, by, 4014, 4016, 101, GumpButtonType.Reply, 0); AddHtml(bx + 6, by + 6, bw - 12, bh - 12, "<BASEFONT COLOR=\"#FFFFFF\">NW</BASEFONT>", false, false);
            AddButton(bx + (bw + gap), by, 4014, 4016, 102, GumpButtonType.Reply, 0); AddHtml(bx + (bw + gap) + 6, by + 6, bw - 12, bh - 12, "<BASEFONT COLOR=\"#FFFFFF\">N</BASEFONT>", false, false);
            AddButton(bx + 2 * (bw + gap), by, 4014, 4016, 103, GumpButtonType.Reply, 0); AddHtml(bx + 2 * (bw + gap) + 6, by + 6, bw - 12, bh - 12, "<BASEFONT COLOR=\"#FFFFFF\">NE</BASEFONT>", false, false);

            AddButton(bx, by + (bh + gap), 4014, 4016, 104, GumpButtonType.Reply, 0); AddHtml(bx + 6, by + (bh + gap) + 6, bw - 12, bh - 12, "<BASEFONT COLOR=\"#FFFFFF\">W</BASEFONT>", false, false);
            AddHtml(bx + (bw + gap), by + (bh + gap) + 6, bw - 12, bh - 12, "<BASEFONT COLOR=\"#FFFFFF\">·</BASEFONT>", false, false);
            AddButton(bx + 2 * (bw + gap), by + (bh + gap), 4014, 4016, 105, GumpButtonType.Reply, 0); AddHtml(bx + 2 * (bw + gap) + 6, by + (bh + gap) + 6, bw - 12, bh - 12, "<BASEFONT COLOR=\"#FFFFFF\">E</BASEFONT>", false, false);

            AddButton(bx, by + 2 * (bh + gap), 4014, 4016, 106, GumpButtonType.Reply, 0); AddHtml(bx + 6, by + 2 * (bh + gap) + 6, bw - 12, bh - 12, "<BASEFONT COLOR=\"#FFFFFF\">SW</BASEFONT>", false, false);
            AddButton(bx + (bw + gap), by + 2 * (bh + gap), 4014, 4016, 107, GumpButtonType.Reply, 0); AddHtml(bx + (bw + gap) + 6, by + 2 * (bh + gap) + 6, bw - 12, bh - 12, "<BASEFONT COLOR=\"#FFFFFF\">S</BASEFONT>", false, false);
            AddButton(bx + 2 * (bw + gap), by + 2 * (bh + gap), 4014, 4016, 108, GumpButtonType.Reply, 0); AddHtml(bx + 2 * (bw + gap) + 6, by + 2 * (bh + gap) + 6, bw - 12, bh - 12, "<BASEFONT COLOR=\"#FFFFFF\">SE</BASEFONT>", false, false);

            // Dot panel on the right (with translucent overlay for readability)
            int panelX = _leftPaneW + 20;
            int panelY = 18;

            // Slightly larger panel background and alpha region so dots and labels are easy to see
            AddBackground(panelX - 12, panelY - 12, _panelW + 24, _panelH + 24, 9270);
            AddAlphaRegion(panelX - 10, panelY - 10, _panelW + 20, _panelH + 20);

            AddHtml(panelX, panelY - 24, 300, 20, "<BASEFONT COLOR=\"#FFFFFF\"><B>Nearby placements</B></BASEFONT>", false, false);

            RenderDotPanel(panelX, panelY, _panelW, _panelH);

            // Legend below the panel with clearer spacing (keep colored labels; descriptive text white)
            int legendY = panelY + _panelH + 8;
            AddHtml(panelX, legendY, 320, 22, $"<BASEFONT COLOR=\"#008000\"><B>Green:</B></BASEFONT> <BASEFONT COLOR=\"#FFFFFF\"> footprint/ring OK</BASEFONT>", false, false);
            AddHtml(panelX + 220, legendY, 320, 22, $"<BASEFONT COLOR=\"#5678FF\"><B>Blue:</B></BASEFONT> <BASEFONT COLOR=\"#FFFFFF\"> terrain clear</BASEFONT>", false, false);
            AddHtml(panelX, legendY + 24, 320, 22, $"<BASEFONT COLOR=\"#A00000\"><B>Red:</B></BASEFONT> <BASEFONT COLOR=\"#FFFFFF\"> invalid tile</BASEFONT>", false, false);

            // Accept / Cancel: keep them visible and inside gump bounds near the left pane bottom
            int bottomY = gumpH - 80;
            if (bottomY < by + 3 * (bh + gap) + 20)
                bottomY = by + 3 * (bh + gap) + 28;

            int acceptX = 18;
            int cancelX = acceptX + 160;

            AddButton(acceptX, bottomY, 4005, 4007, 201, GumpButtonType.Reply, 0); AddHtml(acceptX + 46, bottomY + 4, 100, 20, "<BASEFONT COLOR=\"#008000\"><B>Accept</B></BASEFONT>", false, false);
            AddButton(cancelX, bottomY, 4017, 4019, 202, GumpButtonType.Reply, 0); AddHtml(cancelX + 46, bottomY + 4, 100, 20, "<BASEFONT COLOR=\"#A00000\"><B>Cancel</B></BASEFONT>", false, false);
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
                        // Footprint tiles: green if valid, red if invalid
                        bool invalid = _prev.IsCenterTileInvalid(wx, wy);
                        int hue = invalid ? HueRed : HueGreen;
                        AddLabelCropped(px, py, DotSpacing + 2, DotSpacing + 2, hue, "•");
                    }
                    else if (_prev.IsWorldPointInRing(wx, wy))
                    {
                        // Ring tiles: green if valid, red if invalid (user requested ring show green when OK)
                        if (_prev.IsRingTileInvalid(wx, wy))
                        {
                            AddLabelCropped(px, py, DotSpacing + 2, DotSpacing + 2, HueRed, "•");
                        }
                        else
                        {
                            // Show ring-valid as green (not blue)
                            AddLabelCropped(px, py, DotSpacing + 2, DotSpacing + 2, HueGreen, "•");
                        }
                    }
                    else
                    {
                        // Outside ring: blue if terrain clear (hue 2), blank if blocked
                        if (IsTerrainClear(map, wx, wy))
                        {
                            AddLabelCropped(px, py, DotSpacing + 2, DotSpacing + 2, HueBlue, "•");
                        }
                        // else blank
                    }
                }
            }

            // Mark current center
            int centerPx = x + _scanRadius * DotSpacing;
            int centerPy = y + _scanRadius * DotSpacing;
            //AddHtml(centerPx - 10, centerPy - 20, 60, 18, "<BASEFONT COLOR=\"#FFFFFF\"><B>Center</B></BASEFONT>", false, false);
            AddLabelCropped(centerPx, centerPy - 6, DotSpacing + 2, DotSpacing + 2, HueText, "X");
        }

        private static bool IsTerrainClear(Map map, int x, int y)
        {
            if (map == null || map == Map.Internal)
                return false;

            try
            {
                // Land flags
                int landId = map.Tiles.GetLandTile(x, y).ID;
                var landFlags = TileData.LandTable[landId].Flags;
                if ((landFlags & TileFlag.Impassable) != 0 || (landFlags & TileFlag.Wet) != 0)
                    return false;
            }
            catch { }

            try
            {
                // Statics
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
                // Items/Multis
                int z = 0, avg = 0, top = 0;
                map.GetAverageZ(x, y, ref z, ref avg, ref top);
                var p = new Point3D(x, y, avg);

                foreach (Item item in map.GetItemsInRange(p, 0))
                {
                    if (item.Deleted || !item.Visible)
                        continue;

                    // Ignore preview markers
                    if (item is PreviewMarkerItem)
                        continue;

                    bool impassable = (item.ItemData.Flags & TileFlag.Impassable) != 0;

                    // IMPORTANT: BaseMulti is in Server.Items; BaseHouse is in Server.Multis
                    if (impassable || item is BaseHouse || item is BaseMulti || item is AddonComponent)
                        return false;
                }
            }
            catch { return false; }

            return true;
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            var from = sender.Mobile;
            if (_prev == null || _prev.Deleted)
                return;

            // If ButtonID == 0 the player closed the gump via the X (or otherwise)
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
    // Ensure preview still exists and perform a final validity check before allowing placement
    if (_prev == null || _prev.Deleted)
    {
        from.SendMessage("Preview no longer available.");
        return;
    }

    // Use the house's FootprintIsValidAt to validate center + ring (and ring extensions)
    if (!_prev.FootprintIsValidAt(new Point3D(_prev.X, _prev.Y, _prev.Z)))
    {
        from.SendMessage("Cannot accept: the placement is not yet valid. Move/adjust the preview until all required tiles are clear.");
        // Refresh gump so player can nudge and try again
        from.CloseGump<HousePreviewNudgeGump>();
        from.SendGump(new HousePreviewNudgeGump(_callback, _state, _prev, _viewer, _placementIsValidAt));
        return;
    }

    // Proceed with the normal accept flow (show confirmation warning)
    from.SendGump(new WarningGump(1060635, 30720, 1049583, 32512, 420, 280, _callback, _state));
    return;
}
                case 202:
                {
                    // Cancel: delete the preview and close
                    try { _prev.Delete(); } catch { }
                    return;
                }
            }

            _prev.RevalidateAndColorize();
            from.CloseGump<HousePreviewNudgeGump>();
            // keep signature compatibility
            from.SendGump(new HousePreviewNudgeGump(_callback, _state, _prev, _viewer, _placementIsValidAt));
        }
    }
}