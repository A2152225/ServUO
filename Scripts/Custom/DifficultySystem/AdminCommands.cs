using System;
using System.Collections.Generic;
using Server;
using Server.Commands;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Commands
{
    public class DifficultyAdminCommands
    {
        public static void Initialize()
        {
            CommandSystem.Register("ViewDifficulty", AccessLevel.GameMaster, new CommandEventHandler(ViewDifficulty_OnCommand));
            CommandSystem.Register("SetPlayerDifficulty", AccessLevel.GameMaster, new CommandEventHandler(SetPlayerDifficulty_OnCommand));
            CommandSystem.Register("DifficultyStats", AccessLevel.Administrator, new CommandEventHandler(DifficultyStats_OnCommand));
            CommandSystem.Register("CleanupDifficulty", AccessLevel.Administrator, new CommandEventHandler(CleanupDifficulty_OnCommand));
        }

        [Usage("ViewDifficulty")]
        [Description("View the difficulty level of a targeted player.")]
        public static void ViewDifficulty_OnCommand(CommandEventArgs e)
        {
            e.Mobile.Target = new ViewDifficultyTarget();
        }

        [Usage("SetPlayerDifficulty <difficulty>")]
        [Description("Set the difficulty level of a targeted player.")]
        public static void SetPlayerDifficulty_OnCommand(CommandEventArgs e)
        {
            if (e.Arguments.Length != 1)
            {
                e.Mobile.SendMessage("Usage: SetPlayerDifficulty <1-12>");
                return;
            }

            if (!int.TryParse(e.Arguments[0], out int difficulty) || difficulty < 1 || difficulty > 12)
            {
                e.Mobile.SendMessage("Difficulty must be between 1 and 12.");
                return;
            }

            e.Mobile.Target = new SetDifficultyTarget(difficulty);
        }

        [Usage("DifficultyStats")]
        [Description("Shows statistics about the difficulty system.")]
        public static void DifficultyStats_OnCommand(CommandEventArgs e)
        {
            var difficulties = Server.Systems.Difficulty.DifficultyTracker.GetAllPlayerDifficulties();
            Dictionary<int, int> counts = new Dictionary<int, int>();
            
            for (int i = 1; i <= 12; i++)
                counts[i] = 0;
                
            foreach (var kvp in difficulties)
            {
                if (kvp.Key is PlayerMobile && !kvp.Key.Deleted)
                    counts[kvp.Value]++;
            }
            
            e.Mobile.SendMessage("=== Difficulty System Statistics ===");
            for (int i = 1; i <= 12; i++)
            {
                e.Mobile.SendMessage($"Difficulty {i}: {counts[i]} players");
            }
        }

        [Usage("CleanupDifficulty")]
        [Description("Manually cleanup old difficulty contributions.")]
        public static void CleanupDifficulty_OnCommand(CommandEventArgs e)
        {
            // Force cleanup
            Server.Systems.Difficulty.DifficultyTracker.CleanupOldContributions();
            e.Mobile.SendMessage("Difficulty system cleanup completed.");
        }

        private class ViewDifficultyTarget : Target
        {
            public ViewDifficultyTarget() : base(-1, false, TargetFlags.None) { }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is PlayerMobile pm)
                {
                    int difficulty = Server.Systems.Difficulty.DifficultyTracker.GetPlayerDifficulty(pm);
                    from.SendMessage($"{pm.Name} has difficulty level: {difficulty}");
                }
                else
                {
                    from.SendMessage("That is not a player.");
                }
            }
        }

        private class SetDifficultyTarget : Target
        {
            private int m_Difficulty;

            public SetDifficultyTarget(int difficulty) : base(-1, false, TargetFlags.None)
            {
                m_Difficulty = difficulty;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is PlayerMobile pm)
                {
                    Server.Systems.Difficulty.DifficultyTracker.SetPlayerDifficulty(pm, m_Difficulty);
                    from.SendMessage($"{pm.Name}'s difficulty set to {m_Difficulty}.");
                    pm.SendMessage($"Your difficulty has been set to {m_Difficulty} by {from.Name}.");
                }
                else
                {
                    from.SendMessage("That is not a player.");
                }
            }
        }
    }
}