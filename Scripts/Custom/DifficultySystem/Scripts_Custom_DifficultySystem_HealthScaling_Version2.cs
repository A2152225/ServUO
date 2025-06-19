using System;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Mobiles;

namespace Server
{
    public class HealthScaling
    {
        // Track processed packets to avoid duplicates
        private static Dictionary<int, DateTime> _processedPackets = new Dictionary<int, DateTime>();
        
        public static void Initialize()
        {
            // Hook into mobile events
            EventSink.Movement += new MovementEventHandler(OnMovement);
            EventSink.Login += new LoginEventHandler(OnLogin);
            
            // Clean up processed packets periodically
            Timer.DelayCall(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5), CleanupProcessedPackets);
        }
        
        private static void OnMovement(MovementEventArgs e)
        {
            Mobile m = e.Mobile;
            
            if (m == null || !(m is PlayerMobile))
                return;
                
            // When player moves, send updated health for nearby creatures
            UpdateNearbyCreatureHealth(m);
        }
        
        private static void OnLogin(LoginEventArgs e)
        {
            Mobile m = e.Mobile;
            
            if (m == null)
                return;
                
            // When player logs in, send them the difficulty command info
            m.SendMessage(0x35, "Type [difficulty to adjust your game's difficulty level.");
            
            // Schedule health update for nearby creatures
            Timer.DelayCall(TimeSpan.FromSeconds(2), () => UpdateNearbyCreatureHealth(m));
        }
        
        private static void UpdateNearbyCreatureHealth(Mobile player)
        {
            if (player == null || player.NetState == null)
                return;
                
            int difficultyLevel = DifficultySettings.GetPlayerDifficulty(player);
            
            if (difficultyLevel <= 1) // No scaling needed
                return;
                
            // Find nearby creatures
            foreach (Mobile m in player.GetMobilesInRange(18))
            {
                if (m is BaseCreature && m != player && m.Alive)
                {
                    // Calculate scaled health
                    double healthMultiplier = DifficultySettings.GetHealthMultiplier(difficultyLevel);
                    
                    // Calculate scaled values
                    int scaledMaxHits = (int)(m.HitsMax * healthMultiplier);
                    double ratio = m.HitsMax > 0 ? (double)m.Hits / m.HitsMax : 0;
                    int scaledCurrentHits = Math.Max(1, (int)(scaledMaxHits * ratio));
                    
                    // Send custom health update
                    SendCustomHealthUpdate(player.NetState, m, scaledCurrentHits, scaledMaxHits);
                }
            }
        }
        
        private static void SendCustomHealthUpdate(NetState ns, Mobile creature, int currentHits, int maxHits)
        {
            if (ns == null)
                return;
                
            // Create packet ID (combination of time and serial to avoid duplicates)
            int packetId = creature.Serial.Value ^ (int)(DateTime.UtcNow.Ticks % int.MaxValue);
            
            // Check if we've sent this recently
            if (_processedPackets.ContainsKey(packetId) && 
                DateTime.UtcNow - _processedPackets[packetId] < TimeSpan.FromSeconds(1))
                return;
                
            _processedPackets[packetId] = DateTime.UtcNow;
            
            // Send health update packet
            ns.Send(new HealthUpdatePacket(creature, currentHits, maxHits));
        }
        
        private static void CleanupProcessedPackets()
        {
            // Remove entries older than 5 minutes
            DateTime cutoff = DateTime.UtcNow - TimeSpan.FromMinutes(5);
            List<int> keysToRemove = new List<int>();
            
            foreach (var kvp in _processedPackets)
            {
                if (kvp.Value < cutoff)
                    keysToRemove.Add(kvp.Key);
            }
            
            foreach (int key in keysToRemove)
            {
                _processedPackets.Remove(key);
            }
        }
    }
    
    public class HealthUpdatePacket : Packet
    {
        public HealthUpdatePacket(Mobile m, int currentHits, int maxHits) : base(0xA1, 9)
        {
            m_Stream.Write(m.Serial);
            m_Stream.Write((short)currentHits);  
            m_Stream.Write((short)maxHits);
        }
    }
}