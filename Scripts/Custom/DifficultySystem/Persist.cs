using System;
using System.Collections.Generic;
using System.IO;
using Server;
using Server.Mobiles;

namespace Server.Systems.Difficulty
{
    public static class DifficultyPersistence
    {
        private static string SavePath = Path.Combine(Core.BaseDirectory, "Saves", "DifficultySystem");
        private static string DifficultyFile = Path.Combine(SavePath, "PlayerDifficulties.xml");
        
        public static void Initialize()
        {
            EventSink.WorldSave += OnWorldSave;
            EventSink.WorldLoad += OnWorldLoad;
        }

        private static void OnWorldSave(WorldSaveEventArgs e)
        {
            Persistence.Serialize(
                DifficultyFile,
                SavePlayerDifficulties
            );
        }

        private static void OnWorldLoad()
        {
            Persistence.Deserialize(
                DifficultyFile,
                LoadPlayerDifficulties
            );
        }

        private static void SavePlayerDifficulties(GenericWriter writer)
        {
            var difficulties = DifficultyTracker.GetAllPlayerDifficulties();
            
            writer.Write((int)1); // Version
            writer.Write((int)difficulties.Count);
            
            foreach (var kvp in difficulties)
            {
                writer.Write((Mobile)kvp.Key);
                writer.Write((int)kvp.Value);
            }
        }

        private static void LoadPlayerDifficulties(GenericReader reader)
        {
            int version = reader.ReadInt();
            int count = reader.ReadInt();
            
            for (int i = 0; i < count; i++)
            {
                Mobile player = reader.ReadMobile();
                int difficulty = reader.ReadInt();
                
                if (player != null && !player.Deleted)
                {
                    DifficultyTracker.SetPlayerDifficultyInternal(player, difficulty);
                }
            }
        }
    }

    // Enhanced DifficultyTracker with persistence support
    public static partial class DifficultyTracker
    {
        // Initialize the difficulty system
        public static void Initialize()
        {
            Timer.DelayCall(TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(5.0), CleanupOldContributions);
            DifficultyPersistence.Initialize();
        }

        // Enhanced cleanup for disconnected players
        public static void OnPlayerDisconnected(Mobile player)
        {
            // Keep difficulty setting but clean up active contributions after delay
            Timer.DelayCall(TimeSpan.FromMinutes(5.0), () => CleanupDisconnectedPlayer(player));
        }

        private static void CleanupDisconnectedPlayer(Mobile player)
        {
            if (player == null || player.NetState != null) // Player reconnected
                return;

            // Clean up contributions for disconnected player
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
    }
}