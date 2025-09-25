using System;
using Server;
using Server.Items;

namespace Server.Multis
{
    // Ephemeral marker used for placement preview.
    public class PreviewMarkerItem : Item
    {
        public static bool IsPreviewMarker(Item item) => item is PreviewMarkerItem;

        [Constructable]
        public PreviewMarkerItem(int itemID) : base(itemID)
        {
            Movable = false;
            Visible = true;
            Hue = 0; // gray/default
            Name = "House Preview";
            LootType = LootType.Blessed;
        }

        public PreviewMarkerItem(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int _ = reader.ReadInt();

            // Safety: auto-delete if somehow persisted
            Timer.DelayCall(TimeSpan.FromSeconds(1.0), () =>
            {
                try { Delete(); } catch { }
            });
        }
    }
}