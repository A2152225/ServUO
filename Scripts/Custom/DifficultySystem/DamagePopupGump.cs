using System;
using System.Collections.Generic;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;

namespace Server.Mobiles
{
    public class DamagePopupGump : Gump
    {
        private const int MaxEntries = 4;
        private const int EntryDurationMs = 2500;
        private const int EntrySpacing = 22; // Vertical space between entries
        private const int LabelHue = 33; // Red

        private readonly PlayerMobile _player;
        private readonly bool _editMode;
        private int _x, _y;

        // Per-player damage queue
        private static readonly Dictionary<NetState, Queue<DamageEntry>> _entries = new Dictionary<NetState, Queue<DamageEntry>>();

        public class DamageEntry
        {
            public int Amount;
            public DateTime Timestamp;
            public DamageEntry(int amount) { Amount = amount; Timestamp = DateTime.UtcNow; }
        }

        // Normal display mode
        public DamagePopupGump(List<DamageEntry> entries, int x, int y)
            : this(entries, x, y, null, false)
        {
        }

        // Edit mode constructor
        public DamagePopupGump(List<DamageEntry> entries, int x, int y, PlayerMobile player, bool editMode)
            : base(x, y)
        {
            _player = player;
            _editMode = editMode;
            _x = x;
            _y = y;
            Dragable = editMode; // Only draggable in edit mode

            Closable = editMode;
            Disposable = true;
            
            Resizable = false;

            if (editMode)
            {
                AddBackground(-10, -10, 120, 60, 9270);
                AddLabel(10, 0, 1152, "Drag to move");
                AddButton(10, 30, 247, 248, 1, GumpButtonType.Reply, 0); // Save
                AddLabel(45, 32, 0, "Save");

                // Show a sample number at the exact position where damage will appear
                AddLabel(0, 60, LabelHue, "1,234,435"); // Example damage number
            }

            int i = 0;
            foreach (var entry in entries)
            {
                AddLabel(0, 60 + i * EntrySpacing, LabelHue, entry.Amount.ToString("N0"));
                i++;
            }
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (!_editMode || _player == null)
                return;

            if (info.ButtonID == 1)
            {
                // Save the current gump position
                _player.DamagePopupX = X;
                _player.DamagePopupY = Y;
                _player.SendMessage("Damage popup position saved.");
            }
        }
        public override void OnServerClose(NetState owner)
        {
            if (_editMode && _player != null)
            {
                _player.DamagePopupX = X;
                _player.DamagePopupY = Y;
                _player.SendMessage("Damage popup position saved.");
            }
            base.OnServerClose(owner);
        }
        public static void Show(Mobile m, int damage)
        {
            NetState ns = m.NetState;
            if (ns == null)
                return;

            if (!_entries.TryGetValue(ns, out var queue))
                _entries[ns] = queue = new Queue<DamageEntry>();

            // Remove expired entries
            while (queue.Count > 0 && (DateTime.UtcNow - queue.Peek().Timestamp).TotalMilliseconds > EntryDurationMs)
                queue.Dequeue();

            // Add new entry
            queue.Enqueue(new DamageEntry(damage));

            // Remove oldest if over max
            while (queue.Count > MaxEntries)
                queue.Dequeue();

            // Remove any existing popup
            m.CloseGump(typeof(DamagePopupGump));

            // Get player-specific position, or prompt to set
            int x, y;
            if (m is PlayerMobile pm)
            {
                if (pm.DamagePopupX == -1 || pm.DamagePopupY == -1)
                {
                    // Show the gump in edit mode for initial setup
                    pm.SendGump(new DamagePopupGump(new List<DamageEntry>(), 370, 180, pm, true));
                    return;
                }
                x = pm.DamagePopupX;
                y = pm.DamagePopupY;
            }
            else
            {
                x = (800 / 2) - 30;
                y = (600 / 2) - 120;
            }

            m.SendGump(new DamagePopupGump(new List<DamageEntry>(queue), x, y));

            // Schedule removal of the oldest entry after EntryDurationMs
            Timer.DelayCall(TimeSpan.FromMilliseconds(EntryDurationMs), () =>
            {
                if (_entries.TryGetValue(ns, out var q) && q.Count > 0)
                {
                    q.Dequeue();
                    m.CloseGump(typeof(DamagePopupGump));
                    if (q.Count > 0)
                        m.SendGump(new DamagePopupGump(new List<DamageEntry>(q), x, y));
                }
            });
        }

        public bool HandlesOnClick => false;
        public bool HandlesOnDoubleClick => false;
    }

    
}
