using System;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items
{
    public class ParagonVase : Item
    {
		public override bool IsArtifact { get { return true; } }
        [Constructable]
        public ParagonVase()
            : base(0x0B48)
        {
        }

        public ParagonVase(Serial serial)
            : base(serial)
        {
        }
		
		       public override void OnDoubleClick(Mobile from)
        {
			if (from is PlayerMobile){
			PlayerMobile pm = (PlayerMobile)from;
			from.SendGump( new ParagonGump( pm ) );
			}
        }
		
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}