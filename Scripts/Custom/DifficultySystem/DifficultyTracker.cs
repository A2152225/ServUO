using System;
using System.Collections.Generic;
using System.Linq;
using Server;
using Server.Mobiles;
using Server.Network;

namespace Server.Systems.Difficulty
{
    public enum ContributionType
    {
        Damage,
        Healing,
        Tanking,
        Utility
    }

    public class DamageContribution
    {
        public Mobile Player { get; set; }
        public int TotalDamage { get; set; }
        public int TotalHealing { get; set; }
        public int TankingTime { get; set; }
        public int UtilityActions { get; set; }
        public int DifficultyLevel { get; set; }
        public DateTime LastActivity { get; set; }

        public DamageContribution(Mobile player, int difficulty)
        {
            Player = player;
            DifficultyLevel = difficulty;
            LastActivity = DateTime.UtcNow;
        }

        public double GetContributionScore()
        {
            double score = 0;
            score += TotalDamage * DifficultyLevel;
            score += TotalHealing * 0.5 * DifficultyLevel;
            score += TankingTime * 2 * DifficultyLevel;
            score += UtilityActions * 10 * DifficultyLevel;
            return score;
        }
    }

    public static partial class DifficultyTracker
    {
        private static Dictionary<Mobile, int> _playerDifficulties = new Dictionary<Mobile, int>();
        private static Dictionary<Mobile, Dictionary<Mobile, DamageContribution>> _creatureContributions = 
            new Dictionary<Mobile, Dictionary<Mobile, DamageContribution>>();

        public static int GetPlayerDifficulty(Mobile player)
        {
            return _playerDifficulties.ContainsKey(player) ? _playerDifficulties[player] : 1;
        }

        public static void SetPlayerDifficulty(Mobile player, int difficulty)
        {
            difficulty = Math.Max(1, Math.Min(12, difficulty));
            _playerDifficulties[player] = difficulty;
            ClearPlayerContributions(player);
            
            if (player.NetState != null)
            {
                player.SendMessage($"Difficulty set to {difficulty}. All damage contributions cleared.");
            }
        }

        public static void ClearPlayerContributions(Mobile player)
        {
            List<Mobile> toRemove = new List<Mobile>();
            
            foreach (var kvp in _creatureContributions)
            {
                if (kvp.Value.ContainsKey(player))
                {
                    kvp.Value.Remove(player);
                    if (kvp.Value.Count == 0)
                        toRemove.Add(kvp.Key);
                }
            }
            
            foreach (Mobile creature in toRemove)
            {
                _creatureContributions.Remove(creature);
            }
        }

        public static void AddDamageContribution(Mobile creature, Mobile attacker, int damage)
        {
            if (creature == null || attacker == null)
                return;

            if (!_creatureContributions.ContainsKey(creature))
                _creatureContributions[creature] = new Dictionary<Mobile, DamageContribution>();

            Mobile actualPlayer = GetActualPlayer(attacker);
            if (actualPlayer == null)
                return;

            int difficulty = GetPlayerDifficulty(actualPlayer);
            
            if (!_creatureContributions[creature].ContainsKey(actualPlayer))
                _creatureContributions[creature][actualPlayer] = new DamageContribution(actualPlayer, difficulty);

            var contribution = _creatureContributions[creature][actualPlayer];
            contribution.TotalDamage += damage;
            contribution.LastActivity = DateTime.UtcNow;
        }

        public static void AddHealingContribution(Mobile creature, Mobile healer, int healing)
        {
            if (creature == null || healer == null)
                return;

            if (!_creatureContributions.ContainsKey(creature))
                _creatureContributions[creature] = new Dictionary<Mobile, DamageContribution>();

            if (!_creatureContributions[creature].ContainsKey(healer))
                _creatureContributions[creature][healer] = new DamageContribution(healer, GetPlayerDifficulty(healer));

            var contribution = _creatureContributions[creature][healer];
            contribution.TotalHealing += healing;
            contribution.LastActivity = DateTime.UtcNow;
        }

        public static void AddUtilityContribution(Mobile creature, Mobile player, int utilityPoints = 10)
        {
            if (creature == null || player == null)
                return;

            if (!_creatureContributions.ContainsKey(creature))
                _creatureContributions[creature] = new Dictionary<Mobile, DamageContribution>();

            if (!_creatureContributions[creature].ContainsKey(player))
                _creatureContributions[creature][player] = new DamageContribution(player, GetPlayerDifficulty(player));

            var contribution = _creatureContributions[creature][player];
            contribution.UtilityActions += utilityPoints;
            contribution.LastActivity = DateTime.UtcNow;
        }

        public static Mobile GetActualPlayer(Mobile mobile)
        {
            if (mobile is PlayerMobile)
                return mobile;
            
            if (mobile is BaseCreature bc)
            {
                if (bc.Controlled && bc.ControlMaster is PlayerMobile)
                    return bc.ControlMaster;
                
                if (bc.Summoned && bc.SummonMaster is PlayerMobile)
                    return bc.SummonMaster;
            }
            
            return null;
        }

        public static Dictionary<Mobile, DamageContribution> GetContributions(Mobile creature)
        {
            return _creatureContributions.ContainsKey(creature) ? 
                _creatureContributions[creature] : new Dictionary<Mobile, DamageContribution>();
        }

        public static void CleanupCreature(Mobile creature)
        {
            if (_creatureContributions.ContainsKey(creature))
                _creatureContributions.Remove(creature);
        }

        public static int GetPerceivedHealth(Mobile creature, Mobile viewer)
        {
            if (creature == null || viewer == null)
                return 0;

            Mobile actualPlayer = GetActualPlayer(viewer);
            if (actualPlayer == null)
                return creature.Hits;

            int difficulty = GetPlayerDifficulty(actualPlayer);
            return creature.Hits * difficulty;
        }

        public static int GetPerceivedMaxHealth(Mobile creature, Mobile viewer)
        {
            if (creature == null || viewer == null)
                return 0;

            Mobile actualPlayer = GetActualPlayer(viewer);
            if (actualPlayer == null)
                return creature.HitsMax;

            int difficulty = GetPlayerDifficulty(actualPlayer);
            return creature.HitsMax * difficulty;
        }

        // Methods for persistence
        public static Dictionary<Mobile, int> GetAllPlayerDifficulties()
        {
            return new Dictionary<Mobile, int>(_playerDifficulties);
        }

        // Internal method that doesn't clear contributions (used for loading)
        public static void SetPlayerDifficultyInternal(Mobile player, int difficulty)
        {
            difficulty = Math.Max(1, Math.Min(12, difficulty));
            _playerDifficulties[player] = difficulty;
        }

        // Periodic cleanup of old contributions
        public static void CleanupOldContributions()
        {
            DateTime cutoff = DateTime.UtcNow.AddMinutes(-30); // 30 minutes old
            List<Mobile> creaturesToRemove = new List<Mobile>();
            
            foreach (var creatureKvp in _creatureContributions)
            {
                List<Mobile> playersToRemove = new List<Mobile>();
                
                foreach (var playerKvp in creatureKvp.Value)
                {
                    if (playerKvp.Value.LastActivity < cutoff)
                    {
                        playersToRemove.Add(playerKvp.Key);
                    }
                }
                
                foreach (Mobile player in playersToRemove)
                {
                    creatureKvp.Value.Remove(player);
                }
                
                if (creatureKvp.Value.Count == 0)
                {
                    creaturesToRemove.Add(creatureKvp.Key);
                }
            }
            
            foreach (Mobile creature in creaturesToRemove)
            {
                _creatureContributions.Remove(creature);
            }
        }
    }
}