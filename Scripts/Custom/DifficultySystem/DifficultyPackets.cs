using Server;
using Server.Network;
using Server.Systems.Difficulty;

namespace Server.Network
{
    public sealed class DifficultyHealthPacket : Packet
    {
        public DifficultyHealthPacket(Mobile mobile, Mobile viewer) : base(0x17)
        {
            EnsureCapacity(12);
            
            int perceivedHealth = DifficultyTracker.GetPerceivedHealth(mobile, viewer);
            int perceivedMaxHealth = DifficultyTracker.GetPerceivedMaxHealth(mobile, viewer);
            
            m_Stream.Write((int)mobile.Serial);
            m_Stream.Write((short)perceivedMaxHealth);
            m_Stream.Write((short)perceivedHealth);
        }
    }

    public sealed class DifficultyMobileStatus : Packet
    {
        public DifficultyMobileStatus(Mobile mobile, Mobile viewer) : base(0x11)
        {
            string name = mobile.Name;
            if (string.IsNullOrEmpty(name))
                name = "";

            EnsureCapacity(43);

            m_Stream.Write((int)mobile.Serial);
            m_Stream.WriteAsciiFixed(name, 30);

            int perceivedHealth = DifficultyTracker.GetPerceivedHealth(mobile, viewer);
            int perceivedMaxHealth = DifficultyTracker.GetPerceivedMaxHealth(mobile, viewer);

            m_Stream.Write((short)perceivedHealth);
            m_Stream.Write((short)perceivedMaxHealth);

            m_Stream.Write((byte)(mobile.CanBeRenamedBy(viewer) ? 1 : 0));
            m_Stream.Write((byte)0);
        }
    }
}