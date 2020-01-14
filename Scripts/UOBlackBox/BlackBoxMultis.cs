 //UO Black Box - By GoldDraco13
//1.0.0.93
using Server.Items;

namespace Server.UOBlackBox
{
    public class BlackBoxMultis : BaseMulti
    {
        [Constructable]
        public BlackBoxMultis(int itemVal) : base(itemVal)
        {
            Name = ("BB - " + Name);
            Visible = true;
        }

        public BlackBoxMultis(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
