using System;
using Server;
using Server.Mobiles;
using Server.Gumps;
using Server.Commands;

namespace Server
{
    public class DifficultyCommands
    {
        public static void Initialize()
        {
            // Register the difficulty command
            CommandSystem.Register("Difficulty", AccessLevel.Player, new CommandEventHandler(OnCommand));
        }
        
        [Usage("Difficulty")]
        [Description("Opens the difficulty selection gump.")]
        public static void OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendGump(new DifficultyGump(e.Mobile));
        }
    }
    
    public class DifficultyGump : Gump
    {
        public DifficultyGump(Mobile player) : base(50, 50)
        {
            int currentLevel = DifficultySettings.GetPlayerDifficulty(player);
            
            AddPage(0);
            AddBackground(0, 0, 300, 410, 9270);
            AddAlphaRegion(10, 10, 280, 390);
            
            AddLabel(20, 20, 32, "Select Your Difficulty Level");
            AddLabel(20, 40, 0, "Level 1 is baseline. Each level doubles");
            AddLabel(20, 55, 0, "enemy health but increases rewards.");
            
            // Create buttons for all 12 difficulty levels
            for (int i = 1; i <= 12; i++)
            {
                int y = 70 + ((i - 1) * 25);
                AddButton(20, y, 4005, 4007, i, GumpButtonType.Reply, 0);
                
                double healthMult = DifficultySettings.GetHealthMultiplier(i);
                double rewardMult = DifficultySettings.CalculateRewardMultiplier(i);
                
                string label = String.Format("{0} - {1}x Health, +{2}% Rewards", 
                    i, healthMult, ((rewardMult - 1) * 100).ToString("F0"));
                
                AddLabel(55, y, currentLevel == i ? 33 : 0, label);
            }
        }
        
        public override void OnResponse(Network.NetState sender, RelayInfo info)
        {
            Mobile m = sender.Mobile;
            
            if (info.ButtonID >= 1 && info.ButtonID <= 12)
            {
                int newLevel = info.ButtonID;
                DifficultySettings.SetPlayerDifficulty(m, newLevel);
            }
        }
    }
}