using System;
using System.Reflection;
using Server;
using Server.Mobiles;
using Server.Network;

namespace Server.Custom.DifficultySystem
{
    public class DamageScalingSystem
    {
        public static void Initialize()
        {
            // Hook into damage display events
            EventSink.AnimateRequest += OnAnimateRequest;
            EventSink.Mobile_GetDamageOverlay += OnGetDamageOverlay;
        }
        
        private static void OnAnimateRequest(AnimateRequestEventArgs e)
        {
            // This handles damage animation requests
            if (e.Animation >= 10 && e.Animation <= 19) // Damage animations
            {
                // Not necessary to modify, as we handle overlay text instead
            }
        }
        
        private static void OnGetDamageOverlay(Mobile.GetDamageOverlayEventArgs e)
        {
            // This event fires when damage numbers are shown floating above a mobile
            Mobile target = e.Mobile;
            int damage = e.Damage;
            
            if (target == null || damage <= 0)
                return;
                
            // Check if target is a creature
            if (target is BaseCreature)
            {
                // Find any nearby players
                foreach (Mobile m in target.GetMobilesInRange(18))
                {
                    if (m is PlayerMobile && m.NetState != null)
                    {
                        PlayerMobile pm = m as PlayerMobile;
                        int difficultyLevel = DifficultySystem.GetPlayerDifficulty(pm);
                        
                        if (difficultyLevel > 1)
                        {
                            // Scale the damage display for this player (but not the actual damage)
                            double healthMultiplier = Math.Pow(2, difficultyLevel - 1);
                            
                            // Show scaled damage numbers - this makes player's attacks seem proportionally powerful
                            int scaledDamage = (int)(damage * healthMultiplier);
                            
                            // Send a custom damage number to this player
                            pm.NetState.Send(new DamagePacket(target, scaledDamage));
                            
                            // Prevent default damage number from being sent to this player
                            e.Handled = true;
                        }
                    }
                }
            }
        }
    }
    
    // Custom packet for damage number display
    public sealed class DamagePacket : Packet
    {
        public DamagePacket(Mobile m, int amount) : base(0x0B, 7)
        {
            m_Stream.Write(m.Serial);
            m_Stream.Write((short)amount);
        }
    }
}