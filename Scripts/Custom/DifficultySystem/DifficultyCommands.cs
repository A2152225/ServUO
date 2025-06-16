using Server;
using Server.Commands;
using Server.Systems.Difficulty;

namespace Server.Commands
{
    public partial class DifficultyCommands
    {
        public static void Initialize()
        {
            CommandSystem.Register("SetDifficulty", AccessLevel.Player, new CommandEventHandler(SetDifficulty_OnCommand));
            CommandSystem.Register("Difficulty", AccessLevel.Player, new CommandEventHandler(Difficulty_OnCommand));
            CommandSystem.Register("DifficultyInfo", AccessLevel.Player, new CommandEventHandler(DifficultyInfo_OnCommand));
            CommandSystem.Register("DifficultyMenu", AccessLevel.Player, new CommandEventHandler(DifficultyMenu_OnCommand));
        }

        [Usage("SetDifficulty <1-12>")]
        [Description("Sets your personal difficulty level.")]
        public static void SetDifficulty_OnCommand(CommandEventArgs e)
        {
            if (e.Arguments.Length != 1)
            {
                e.Mobile.SendMessage("Usage: SetDifficulty <1-12>");
                return;
            }

            if (!int.TryParse(e.Arguments[0], out int difficulty) || difficulty < 1 || difficulty > 12)
            {
                e.Mobile.SendMessage("Difficulty must be between 1 and 12.");
                return;
            }

            DifficultyTracker.SetPlayerDifficulty(e.Mobile, difficulty);
        }

        [Usage("Difficulty")]
        [Description("Shows your current difficulty level.")]
        public static void Difficulty_OnCommand(CommandEventArgs e)
        {
            int difficulty = DifficultyTracker.GetPlayerDifficulty(e.Mobile);
            e.Mobile.SendMessage($"Your current difficulty level is: {difficulty}");
        }

        [Usage("DifficultyInfo")]
        [Description("Shows information about the difficulty system.")]
        public static void DifficultyInfo_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendMessage("=== Difficulty System ===");
            e.Mobile.SendMessage("Difficulty ranges from 1 (normal) to 12 (extreme).");
            e.Mobile.SendMessage("Higher difficulty = more creature health, less damage dealt, better rewards.");
            e.Mobile.SendMessage("Use .SetDifficulty <1-12> to change your difficulty.");
            e.Mobile.SendMessage("Changing difficulty clears your damage contributions!");
        }

        [Usage("DifficultyMenu")]
        [Aliases("DMenu")]
        [Description("Opens the difficulty selection menu.")]
        public static void DifficultyMenu_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendGump(new Server.Gumps.DifficultySelectionGump(e.Mobile));
        }
    }
}