using System;
using System.Collections.Generic;
using System.Reflection;
using Server;
using Server.Mobiles;
using Server.Network;

namespace Server.Custom.DifficultySystem
{
    public class DamageScalingSystem
    {
        // Track recent damage received by mobs for scaling
        private static Dictionary<Mobile, List<DamageRecord>> _recentDamage = new Dictionary<Mobile, List<DamageRecord>>();
        
        public static void Initialize()
        {
            // Start a timer to process damage displays
            Timer.DelayCall(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(0.1), ProcessDamageDisplays);
            
            // Clean up old entries periodically
            Timer.DelayCall(TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1), CleanupOldRecords);
            
            // Try to hook into AOS damage method using reflection
            TryHookAOSDamage();
			    // Add a hook for DamageScaling
    EventSink.Combat += new CombatEventHandler(OnCombat);
        }
		
		private static void OnCombat(CombatEventArgs e)
{
    Mobile attacker = e.Attacker;
    Mobile defender = e.Defender;
    
    if (attacker is PlayerMobile && defender is BaseCreature)
    {
        int difficultyLevel = GetPlayerDifficulty(attacker);
        
        if (difficultyLevel > 1)
        {
            // Record this for damage scaling
            int damage = e.Damage;
            DamageScalingSystem.RecordDamage(attacker, defender, damage);
        }
    }
}
        
        private static void TryHookAOSDamage()
        {
            try
            {
                Type aosType = Type.GetType("Server.AOS");
                if (aosType == null) 
                    aosType = Type.GetType("Server.AOS, Server");
                
                if (aosType != null)
                {
                    // Try different methods that might handle damage
                    MethodInfo[] methods = aosType.GetMethods(BindingFlags.Public | BindingFlags.Static);
                    
                    foreach (MethodInfo method in methods)
                    {
                        if ((method.Name == "Damage" || method.Name.Contains("Damage")) && 
                            method.GetParameters().Length >= 2)
                        {
                            Console.WriteLine("Found potential damage method: " + method.Name);
                            // We'll use this information later 
                        }
                    }
                }
                
                Console.WriteLine("Damage scaling system initialized");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error setting up damage scaling: " + ex.Message);
            }
        }
        
        // Method to record damage done to creatures
        public static void RecordDamage(Mobile attacker, Mobile defender, int damage)
        {
            if (attacker == null || defender == null || damage <= 0)
                return;
                
            // Only track player damage to creatures
            if (attacker is PlayerMobile && defender is BaseCreature)
            {
                int difficultyLevel = DifficultySystem.GetPlayerDifficulty(attacker);
                
                if (difficultyLevel > 1)
                {
                    // Track this damage for later display scaling
                    if (!_recentDamage.ContainsKey(defender))
                        _recentDamage[defender] = new List<DamageRecord>();
                        
                    _recentDamage[defender].Add(new DamageRecord(attacker, damage, difficultyLevel, DateTime.UtcNow));
                    
                    // Queue this for damage display scaling
                    double healthMultiplier = Math.Pow(2, difficultyLevel - 1);
                    int scaledDamage = (int)(damage * healthMultiplier);
                    
                    // Send a custom damage number to this player
                    attacker.NetState.Send(new ScaledDamagePacket(defender, scaledDamage));
                }
            }
        }
        
        private static void ProcessDamageDisplays()
        {
            // This is a placeholder for the timer - we're using direct calls in RecordDamage instead
        }
        
        private static void CleanupOldRecords()
        {
            // Remove records older than 1 minute
            DateTime cutoff = DateTime.UtcNow - TimeSpan.FromMinutes(1);
            List<Mobile> keysToRemove = new List<Mobile>();
            
            foreach (var kvp in _recentDamage)
            {
                kvp.Value.RemoveAll(dr => dr.Time < cutoff);
                
                if (kvp.Value.Count == 0)
                    keysToRemove.Add(kvp.Key);
            }
            
            foreach (Mobile m in keysToRemove)
            {
                _recentDamage.Remove(m);
            }
        }
        
        private class DamageRecord
        {
            public Mobile Attacker { get; private set; }
            public int Damage { get; private set; }
            public int DifficultyLevel { get; private set; }
            public DateTime Time { get; private set; }
            
            public DamageRecord(Mobile attacker, int damage, int difficultyLevel, DateTime time)
            {
                Attacker = attacker;
                Damage = damage;
                DifficultyLevel = difficultyLevel;
                Time = time;
            }
        }
    }
    
    // Custom packet for scaled damage display
    public sealed class ScaledDamagePacket : Packet
    {
        public ScaledDamagePacket(Mobile m, int amount) : base(0x0B, 7)
        {
            m_Stream.Write(m.Serial);
            m_Stream.Write((short)amount);
        }
    }
}