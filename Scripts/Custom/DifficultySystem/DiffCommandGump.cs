using System;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.Gumps
{
    public class DifficultySelectionGump : Gump
    {
        private const int GumpID = 0x2422;
        
        public DifficultySelectionGump(Mobile user) : base(50, 50)
        {
            user.CloseGump(typeof(DifficultySelectionGump));
            
            int currentDifficulty = Server.Systems.Difficulty.DifficultyTracker.GetPlayerDifficulty(user);
            
            AddPage(0);
            AddBackground(0, 0, 400, 500, GumpID);
            AddHtml(20, 20, 360, 40, "<center><h2>Difficulty Selection</h2></center>", false, false);
            
            AddHtml(20, 70, 360, 80, 
                "<center>Higher difficulty provides better rewards but makes creatures tougher. " +
                "Changing difficulty will clear your current damage contributions!</center>", 
                false, false);
                
            AddHtml(20, 160, 360, 20, $"<center>Current Difficulty: <b>{currentDifficulty}</b></center>", false, false);
            
            // Difficulty buttons
            for (int i = 1; i <= 12; i++)
            {
                int x = 30 + ((i - 1) % 3) * 110;
                int y = 200 + ((i - 1) / 3) * 50;
                
                int hue = (i == currentDifficulty) ? 68 : 0; // Green if current
                
                AddButton(x, y, 0xFA5, 0xFA7, i, GumpButtonType.Reply, 0);
                AddLabel(x + 30, y + 5, hue, $"Level {i}");
                
                // Add difficulty description
                string desc = GetDifficultyDescription(i);
                AddLabel(x - 10, y + 25, 0, desc);
            }
            
            AddButton(150, 450, 0xFB7, 0xFB9, 0, GumpButtonType.Reply, 0); // Cancel
            AddLabel(185, 453, 0, "Cancel");
        }
        
        private string GetDifficultyDescription(int difficulty)
        {
            switch (difficulty)
            {
                case 1: return "Normal";
                case 2: return "Easy+";
                case 3: return "Medium";
                case 4: return "Medium+";
                case 5: return "Hard";
                case 6: return "Hard+";
                case 7: return "Very Hard";
                case 8: return "Extreme";
                case 9: return "Nightmare";
                case 10: return "Hell";
                case 11: return "Insane";
                case 12: return "IMPOSSIBLE";
                default: return "Unknown";
            }
        }
        
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            
            if (info.ButtonID >= 1 && info.ButtonID <= 12)
            {
                int newDifficulty = info.ButtonID;
                int currentDifficulty = Server.Systems.Difficulty.DifficultyTracker.GetPlayerDifficulty(from);
                
                if (newDifficulty != currentDifficulty)
                {
                    from.SendGump(new ConfirmDifficultyGump(newDifficulty));
                }
                else
                {
                    from.SendMessage("You are already at that difficulty level.");
                }
            }
        }
    }
    
    public class ConfirmDifficultyGump : Gump
    {
        private int m_NewDifficulty;
        
        public ConfirmDifficultyGump(int newDifficulty) : base(100, 100)
        {
            m_NewDifficulty = newDifficulty;
            
            AddPage(0);
            AddBackground(0, 0, 350, 200, 0x2422);
            AddHtml(20, 20, 310, 40, "<center><h3>Confirm Difficulty Change</h3></center>", false, false);
            
            AddHtml(20, 70, 310, 60, 
                $"<center>Are you sure you want to change to difficulty <b>{newDifficulty}</b>?<br>" +
                "This will clear all your current damage contributions!</center>", 
                false, false);
            
            AddButton(50, 150, 0xFA5, 0xFA7, 1, GumpButtonType.Reply, 0);  // Yes
            AddLabel(85, 153, 68, "Yes, Change It");
            
            AddButton(200, 150, 0xFA5, 0xFA7, 0, GumpButtonType.Reply, 0); // No
            AddLabel(235, 153, 38, "Cancel");
        }
        
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            
            if (info.ButtonID == 1) // Yes
            {
                Server.Systems.Difficulty.DifficultyTracker.SetPlayerDifficulty(from, m_NewDifficulty);
                from.SendMessage(68, $"Difficulty changed to {m_NewDifficulty}!");
            }
            else
            {
                from.SendMessage("Difficulty change cancelled.");
            }
        }
    }
}