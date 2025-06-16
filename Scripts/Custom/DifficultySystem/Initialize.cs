using Server;
using Server.Commands;

namespace Server.Systems.Difficulty
{
    public class DifficultySystemInit
    {
        public static void Initialize()
        {
            DifficultyCommands.Initialize();
            DifficultyAdminCommands.Initialize();
            DifficultyTracker.Initialize();
        }
    }
}