using System;
using System.Collections.Generic;
using Server;
using Server.Mobiles;

namespace Server.Systems.Difficulty
{
    // Event handlers for PlayerMobile to integrate with difficulty system
    public static class PlayerMobileDifficultyIntegration
    {
        public static void Initialize()
        {
            EventSink.Login += OnLogin;
            EventSink.Logout += OnLogout;
        }

        private static void OnLogin(LoginEventArgs e)
        {
            if (e.Mobile is PlayerMobile player)
            {
                // Ensure difficulty is properly set from saved data
                int difficulty = GetSavedDifficulty(player);
                DifficultyTracker.SetPlayerDifficultyInternal(player, difficulty);
                
                // Send welcome message about difficulty system
                Timer.DelayCall(TimeSpan.FromSeconds(2.0), () =>
                {
                    if (player.NetState != null)
                    {
                        player.SendMessage(38, $"Your difficulty level is set to {difficulty}. Use .SetDifficulty <1-12> to change it.");
                        if (difficulty == 1)
                        {
                            player.SendMessage(68, "Try increasing your difficulty for better rewards and a greater challenge!");
                        }
                    }
                });
            }
        }

        private static void OnLogout(LogoutEventArgs e)
        {
            if (e.Mobile is PlayerMobile player)
            {
                // Save current difficulty
                int difficulty = DifficultyTracker.GetPlayerDifficulty(player);
                SavePlayerDifficulty(player, difficulty);
                
                // Handle disconnection cleanup
                DifficultyTracker.OnPlayerDisconnected(player);
            }
        }

        // Simple property-based storage for difficulty (can be expanded to use files)
        private static Dictionary<Serial, int> _savedDifficulties = new Dictionary<Serial, int>();

        private static int GetSavedDifficulty(PlayerMobile player)
        {
            return _savedDifficulties.TryGetValue(player.Serial, out int difficulty) ? difficulty : 1;
        }

        private static void SavePlayerDifficulty(PlayerMobile player, int difficulty)
        {
            _savedDifficulties[player.Serial] = difficulty;
        }
    }
}