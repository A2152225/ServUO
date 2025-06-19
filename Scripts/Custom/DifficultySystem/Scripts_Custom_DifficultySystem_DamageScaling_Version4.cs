using System;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Commands;

namespace Server
{
    public class DamageScaling
    {
        public static void Initialize()
        {
            // Register command for testing
            CommandSystem.Register("TestDamage", AccessLevel.Player, new CommandEventHandler(OnCommand));
        }
        
        [Usage("TestDamage")]
        [Description("Shows how damage scaling works.")]
        public static void OnCommand(CommandEventArgs e)
        {
            Mobile m = e.Mobile;
            
            int difficultyLevel = DifficultySettings.GetPlayerDifficulty(m);
            double healthMultiplier = DifficultySettings.GetHealthMultiplier(difficultyLevel);
            
            m.SendMessage("Your current difficulty level is {0}.", difficultyLevel);
            m.SendMessage("Enemy health appears {0}x normal amount.", healthMultiplier);
            m.SendMessage("Your damage will appear {0}x normal amount to match.", healthMultiplier);
            
            // Demonstrate scaled damage
            int baseDamage = 50;
            int scaledDamage = (int)(baseDamage * healthMultiplier);
            
            m.SendMessage("A normal hit of {0} damage would appear as {1} damage.", 
                baseDamage, scaledDamage);
        }
        
        // Method to be called from AOS.Damage
        public static void ShowScaledDamage(Mobile attacker, Mobile defender, int damage)
        {
            if (attacker == null || defender == null || attacker.NetState == null)
                return;
            
            if (attacker is PlayerMobile && defender is BaseCreature)
            {
                int difficultyLevel = DifficultySettings.GetPlayerDifficulty(attacker);
                
                if (difficultyLevel > 1)
                {
                    double healthMultiplier = DifficultySettings.GetHealthMultiplier(difficultyLevel);
                    int scaledDamage = (int)(damage * healthMultiplier);
                    
                    // Send scaled damage
                    attacker.NetState.Send(new DamageNumberPacket(defender, scaledDamage));
                }
            }
        }
    }
    
    // Custom packet for damage display
    public sealed class DamageNumberPacket : Packet
    {
        public DamageNumberPacket(Mobile m, int amount) : base(0x0B, 7)
        {
            m_Stream.Write(m.Serial);
            m_Stream.Write((short)amount);
        }
    }
}