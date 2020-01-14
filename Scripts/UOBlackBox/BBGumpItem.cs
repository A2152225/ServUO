 //UO Black Box - By GoldDraco13
//1.0.0.93

using Server.Mobiles;

namespace Server.UOBlackBox
{
    class BBGumpItem : Item
    {
        private int GumpID;
        private int GumpX;
        private int GumpY;
        private int GumpW;
        private int GumpH;

        private string GumpText;
        private int GumpHue;
        private string GumpType;

        [Constructable]
        public BBGumpItem(int id, int x, int y, int w, int h, string text, int hue, string type) : base(0x2F5A)
        {
            Name = "UO Black Box Test Gump";
            Visible = true;

            GumpID = id;
            GumpX = x;
            GumpY = y;
            GumpW = w;
            GumpH = h;

            GumpText = text;
            GumpHue = hue;
            GumpType = type;
        }

        public BBGumpItem(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            PlayerMobile pm = from as PlayerMobile;

            if (pm == null)
                return;

            pm.SendGump(new BBGump(pm, GumpID, GumpX, GumpY, GumpW, GumpH, GumpText, GumpHue, GumpType));
            Delete();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); //version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
