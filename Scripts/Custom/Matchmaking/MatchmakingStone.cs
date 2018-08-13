#region References

using Tresni;

#endregion

namespace Server.Items
{
    [Flipable(3804, 3803)]
    public class MatchmakingStone : Item
    {
        public override string DefaultName
        {
            get { return "Party Matchmaking System"; }
        }

        [Constructable]
        public MatchmakingStone() : base(3804) // looks like a faction signup stone
        {
            Movable = false;
            Hue = 74;
        }

        public override void OnDoubleClick(Mobile from)
        {
            from.CloseGump(typeof(MatchmakingGump));
            from.SendGump(new MatchmakingGump(from, 0));   
            
            from.SendMessage("Welcome to the party matchmaking system.  You can also access this by using the command [matchmaking at any time.");  
        }

        public MatchmakingStone(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}