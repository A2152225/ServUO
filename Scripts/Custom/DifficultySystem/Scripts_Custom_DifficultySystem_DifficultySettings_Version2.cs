using System;
using System.Collections.Generic;
using System.IO;
using Server;
using Server.Mobiles;

namespace Server
{
    // A static class for centralized access to difficulty settings
    public static class DifficultySettings
    {
        // Central storage for player difficulty settings
        private static Dictionary<Mobile, int> _playerDifficulties = new Dictionary<Mobile, int>();
        private static string SavePath = Path.Combine("Saves", "DifficultySystem", "Settings.bin");
        
        // Initialize the system
        public static void Initialize()
        {
            // Load saved settings
            LoadSettings();
            
            // Save settings on world save
            EventSink.WorldSave += new WorldSaveEventHandler(OnWorldSave);
            
            Console.WriteLine("Difficulty system initialized.");
        }
        
        // Get a player's difficulty setting (1 = normal)
        public static int GetPlayerDifficulty(Mobile m)
        {
            if (_playerDifficulties.TryGetValue(m, out int level))
                return level;
                
            // Default to normal difficulty (1)
            return 1;
        }
        
        // Set a player's difficulty level
        public static void SetPlayerDifficulty(Mobile m, int level)
        {
            // Validate difficulty level (1-12)
            if (level < 1) level = 1;
            if (level > 12) level = 12;
            
            _playerDifficulties[m] = level;
            
            // Tell the player about the impact of their choice
            double healthMultiplier = Math.Pow(2, level - 1);
            double rewardMultiplier = CalculateRewardMultiplier(level);
            
            m.SendMessage("Difficulty set to {0}.", level);
            m.SendMessage("Enemies will appear to have {0}x health.", healthMultiplier);
            m.SendMessage("Rewards will be increased by approximately {0}%.", ((rewardMultiplier - 1) * 100).ToString("F0"));
        }
        
        // Helper method to calculate reward multiplier based on difficulty
        public static double CalculateRewardMultiplier(int difficultyLevel)
        {
            // Progressive reward scaling
            if (difficultyLevel <= 1)
                return 1.0;
                
            return 1.0 + ((difficultyLevel - 1) * 0.25);
        }
        
        // Helper method to get reward multiplier for a player
        public static double GetRewardMultiplier(Mobile player)
        {
            int difficulty = GetPlayerDifficulty(player);
            return CalculateRewardMultiplier(difficulty);
        }
        
        // Helper method to calculate health multiplier
        public static double GetHealthMultiplier(int difficultyLevel)
        {
             double multiplier = Math.Pow(2, difficultyLevel - 1);
    //Console.WriteLine("Difficulty: " + difficultyLevel + ", Health Multiplier: " + multiplier);
    return multiplier;
        }
        
        // Save settings to disk
        private static void OnWorldSave(WorldSaveEventArgs e)
        {
            SaveSettings();
        }
        
        private static void SaveSettings()
        {
            try
            {
                // Ensure directory exists
                string directory = Path.Combine("Saves", "DifficultySystem");
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                    
                using (FileStream fs = new FileStream(SavePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    BinaryWriter writer = new BinaryWriter(fs);
                    
                    writer.Write(_playerDifficulties.Count);
                    
                    foreach (KeyValuePair<Mobile, int> kvp in _playerDifficulties)
                    {
                        writer.Write(kvp.Key.Serial.Value);
                        writer.Write(kvp.Value);
                    }
                    
                    writer.Close();
                }
                
                Console.WriteLine("Difficulty system settings saved.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving difficulty settings: " + ex.Message);
            }
        }
        
        private static void LoadSettings()
        {
            if (!File.Exists(SavePath))
                return;
                
            try
            {
                using (FileStream fs = new FileStream(SavePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    BinaryReader reader = new BinaryReader(fs);
                    
                    int count = reader.ReadInt32();
                    
                    for (int i = 0; i < count; i++)
                    {
                        int serialValue = reader.ReadInt32();
                        int difficultyLevel = reader.ReadInt32();
                        
                        Mobile m = World.FindMobile((Serial)serialValue);
                        
                        if (m != null)
                            _playerDifficulties[m] = difficultyLevel;
                    }
                    
                    reader.Close();
                }
                
                Console.WriteLine("Difficulty system settings loaded for {0} players.", _playerDifficulties.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading difficulty settings: " + ex.Message);
            }
        }

        // Move ContributionTracker inside DifficultySettings as a nested class
        public static class ContributionTracker
        {
            // Dictionary to track contributions: <Mobile creature, Dictionary<Mobile player, ContributionData>>
            private static Dictionary<Mobile, Dictionary<Mobile, ContributionData>> _contributions = new Dictionary<Mobile, Dictionary<Mobile, ContributionData>>();
            
            // Track contribution data
            public class ContributionData
            {
                public int DamageContribution { get; set; } = 0;
                public int UtilityContribution { get; set; } = 0;
                
                public int TotalContribution => DamageContribution + UtilityContribution;
                
                // Calculate score, with difficulty factored in
                public double GetContributionScore(Mobile player)
                {
                    return TotalContribution * DifficultySettings.GetRewardMultiplier(player);
                }
                
                public ContributionData()
                {
                }
            }
            
            // Add damage contribution
            public static void AddDamageContribution(Mobile creature, Mobile source, int amount)
            {
                // Get actual player
                Mobile player = GetActualPlayer(source);
                if (player == null)
                    return;
                    
                AddContribution(creature, player, amount, 0);
            }
            
            // Add utility contribution (skills, healing, etc.)
            public static void AddUtilityContribution(Mobile creature, Mobile source, int amount)
            {
                // Get actual player
                Mobile player = GetActualPlayer(source);
                if (player == null)
                    return;
                    
                AddContribution(creature, player, 0, amount);
            }
            
            // Core contribution tracking
            private static void AddContribution(Mobile creature, Mobile player, int damageAmount, int utilityAmount)
            {
                if (creature == null || player == null)
                    return;
                    
                // Initialize dictionaries if needed
                if (!_contributions.ContainsKey(creature))
                    _contributions[creature] = new Dictionary<Mobile, ContributionData>();
                    
                if (!_contributions[creature].ContainsKey(player))
                    _contributions[creature][player] = new ContributionData();
                    
                // Add contributions
                _contributions[creature][player].DamageContribution += damageAmount;
                _contributions[creature][player].UtilityContribution += utilityAmount;
            }
            
            // Get all contributions for a creature
            public static Dictionary<Mobile, ContributionData> GetContributions(Mobile creature)
            {
                if (creature == null || !_contributions.ContainsKey(creature))
                    return new Dictionary<Mobile, ContributionData>();
                    
                return _contributions[creature];
            }
            
            // Clean up tracking data when creature dies
            public static void CleanupCreature(Mobile creature)
            {
                if (creature != null && _contributions.ContainsKey(creature))
                    _contributions.Remove(creature);
            }
            
            // Helper to get actual player from a damage source
            public static Mobile GetActualPlayer(Mobile source)
            {
                if (source == null)
                    return null;
                    
                // Direct player
                if (source is PlayerMobile)
                    return source;
                    
                // Controlled creature
                if (source is BaseCreature bc)
                {
                    if (bc.Controlled && bc.ControlMaster is PlayerMobile)
                        return bc.ControlMaster;
                        
                    if (bc.Summoned && bc.SummonMaster is PlayerMobile)
                        return bc.SummonMaster;
                }
                
                return null;
            }
        }
    }
}