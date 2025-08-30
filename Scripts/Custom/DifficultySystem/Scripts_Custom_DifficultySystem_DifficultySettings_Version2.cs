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
        // Health-bar presentation toggle
        public enum HealthBarMode
        {
            Real = 0,                    // no perceived health; use engine Hits/HitsMax only
            PerceivedOnHit = 1,          // push perceived health once per hit
            PerceivedOnHitAndPeriodic = 2// push per-hit + periodic refresh while damaged
        }

        public static HealthBarMode HealthBarPresentation { get; set; } = HealthBarMode.PerceivedOnHit;

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
            return 1;
        }

        // Set a player's difficulty level
        public static void SetPlayerDifficulty(Mobile m, int level)
        {
            if (level < 1) level = 1;
            if (level > 12) level = 12;

            _playerDifficulties[m] = level;

            double healthMultiplier = Math.Pow(2, level - 1);
            double rewardMultiplier = CalculateRewardMultiplier(level);

            m.SendMessage("Difficulty set to {0}.", level);
            m.SendMessage("Enemies will appear to have {0}x health.", healthMultiplier);
            m.SendMessage("Rewards will be increased by approximately {0}%.", ((rewardMultiplier - 1) * 100).ToString("F0"));
        }

        // Helper method to calculate reward multiplier based on difficulty
        public static double CalculateRewardMultiplier(int difficultyLevel)
        {
            if (difficultyLevel <= 1)
                return 1.0;
            return 1.0 + ((difficultyLevel - 1) * 25);
        }

        public static double GetRewardMultiplier(Mobile player)
        {
            int difficulty = GetPlayerDifficulty(player);
            return CalculateRewardMultiplier(difficulty);
        }

        public static double GetHealthMultiplier(int difficultyLevel)
        {
            double multiplier = Math.Pow(2, difficultyLevel - 1);
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
                string directory = Path.Combine("Saves", "DifficultySystem");
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                using (FileStream fs = new FileStream(SavePath, FileMode.Create, FileAccess.Write, FileShare.None))
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    // Player difficulties
                    writer.Write(_playerDifficulties.Count);
                    foreach (KeyValuePair<Mobile, int> kvp in _playerDifficulties)
                    {
                        writer.Write(kvp.Key.Serial.Value);
                        writer.Write(kvp.Value);
                    }

                    // NEW: persist health-bar presentation mode (backward-compatible append)
                    writer.Write((int)HealthBarPresentation);
                }

                Console.WriteLine("Difficulty system settings saved. Mode={0}", HealthBarPresentation);
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
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    int count = reader.ReadInt32();

                    // Player difficulties (best-effort; skips entries for non-loaded mobiles)
                    for (int i = 0; i < count; i++)
                    {
                        int serialValue = reader.ReadInt32();
                        int difficultyLevel = reader.ReadInt32();

                        Mobile m = World.FindMobile((Serial)serialValue);
                        if (m != null)
                            _playerDifficulties[m] = difficultyLevel;
                    }

                    // NEW: optional tail read for health-bar mode (backward compatible)
                    if (fs.Position + sizeof(int) <= fs.Length)
                    {
                        int mode = reader.ReadInt32();
                        if (Enum.IsDefined(typeof(HealthBarMode), mode))
                            HealthBarPresentation = (HealthBarMode)mode;
                    }
                }

                Console.WriteLine("Difficulty system settings loaded for {0} players. Mode={1}", _playerDifficulties.Count, HealthBarPresentation);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading difficulty settings: " + ex.Message);
            }
        }

        // Move ContributionTracker inside DifficultySettings as a nested class
        public static class ContributionTracker
        {
            private static Dictionary<Mobile, Dictionary<Mobile, ContributionData>> _contributions = new Dictionary<Mobile, Dictionary<Mobile, ContributionData>>();

            public class ContributionData
            {
                public int DamageContribution { get; set; } = 0;
                public int UtilityContribution { get; set; } = 0;

                public int TotalContribution => DamageContribution + UtilityContribution;

                public double GetContributionScore(Mobile player)
                {
                    return TotalContribution * DifficultySettings.GetRewardMultiplier(player);
                }
            }

            public static void AddDamageContribution(Mobile creature, Mobile source, int amount)
            {
                Mobile player = GetActualPlayer(source);
                if (player == null) return;
                AddContribution(creature, player, amount, 0);
            }

            public static void AddUtilityContribution(Mobile creature, Mobile source, int amount)
            {
                Mobile player = GetActualPlayer(source);
                if (player == null) return;
                AddContribution(creature, player, 0, amount);
            }

            private static void AddContribution(Mobile creature, Mobile player, int damageAmount, int utilityAmount)
            {
                if (creature == null || player == null) return;
                if (!_contributions.ContainsKey(creature))
                    _contributions[creature] = new Dictionary<Mobile, ContributionData>();
                if (!_contributions[creature].ContainsKey(player))
                    _contributions[creature][player] = new ContributionData();

                _contributions[creature][player].DamageContribution += damageAmount;
                _contributions[creature][player].UtilityContribution += utilityAmount;
            }

            public static Dictionary<Mobile, ContributionData> GetContributions(Mobile creature)
            {
                if (creature == null || !_contributions.ContainsKey(creature))
                    return new Dictionary<Mobile, ContributionData>();
                return _contributions[creature];
            }

            public static void CleanupCreature(Mobile creature)
            {
                if (creature != null && _contributions.ContainsKey(creature))
                    _contributions.Remove(creature);
            }

            public static Mobile GetActualPlayer(Mobile source)
            {
                if (source == null) return null;
                if (source is PlayerMobile) return source;
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

        public static void ApplyScaledDamage(BaseCreature creature, Mobile player, double scaledDamage)
        {
            if (creature == null || player == null)
                return;

            creature.AddFractionalDamage(player, scaledDamage);

            double fractional = creature.GetFractionalDamage(player);
            if (fractional >= 1.0)
            {
                int wholeDamage = (int)fractional;
                creature.Hits -= wholeDamage;
                creature._fractionalDamage[player] -= wholeDamage;
            }
        }
    }
}
