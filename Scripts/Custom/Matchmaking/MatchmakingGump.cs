using System;
using System.Collections.Generic;
using System.Linq;
using Server;
using Server.Engines.PartySystem;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Tresdni;

namespace Tresni
{
    public class MatchmakingGump : Gump
    {
        Mobile caller;

        private int MaxPerPage = 7;
        private readonly int CurrentPage;
        private readonly List<Matchmaking> matches; 

        public MatchmakingGump(Mobile from, int page) : base(0, 0)
        {
            caller = from;
            CurrentPage = page;

            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            matches = Matchmaking.WaitingForParty;

            bool inmatchmaking = false;

            foreach (Matchmaking round in matches.Where(round => round.Player == caller))
            {
                inmatchmaking = true;
            }

            AddPage(0);
            AddBackground(0, 0, 796, 596, 9270);
            AddLabel(656, 15, 1153, @"Party Matchmaking");
            AddBackground(16, 48, 766, 35, 9200);

            // Gump Headers
            AddLabel(28, 57, 1152, @"Party Leader");
            AddLabel(178, 57, 1152, @"Party Members");
            AddLabel(365, 57, 1152, @"Type");
            AddLabel(494, 57, 1152, @"Murderers Allowed");
            AddLabel(685, 57, 1152, @"Join Party");

            AddBackground(494, 481, 287, 100, 9250);

            if (!inmatchmaking)
            {
                AddLabel(552, 497, 1153, @"Create Matchmaking Party");
                AddButton(599, 527, 1148, 1147, 999, GumpButtonType.Reply, 0);
            }
            else
            {
                AddLabel(512, 497, 1153, @"Remove Your Party From Matchmaking?");
                AddButton(599, 527, 1146, 1145, 998, GumpButtonType.Reply, 0);
            }

            bool haspages = false;

            if (matches.Count > MaxPerPage && matches.Count > CurrentPage * MaxPerPage)
            {
                AddButton(705, 446, 4005, 4007, 996, GumpButtonType.Reply, 0); //Next Page
                haspages = true;
            }

            if ((matches.Count < CurrentPage * MaxPerPage) && CurrentPage != 0)
            {
                AddButton(556, 446, 4014, 248, 997, GumpButtonType.Reply, 0); //Previous Page
                haspages = true;
            }

            if (haspages)
            {
                AddLabel(624, 448, 1153, @"Pages");
            }

            if (matches.Count <= 0)
            {
                AddLabel(33, 113, 1153, @"There are currently no Matchmaking parties.");
            }
            else
            {
                int y = 113;
                int match;

                for (match = 0; match < matches.Count; match++)
                {
                    if (match / MaxPerPage != CurrentPage)
                    {
                        continue;
                    }

                    Matchmaking party = Matchmaking.WaitingForParty[match];

                    if (party != null)
                    {
                        int members = 1;

                        Party current = Party.Get(party.Player);

                        if (current != null)
                        {
                            members = current.Members.Count;
                        }

                        AddLabel(33, y, party.Player.Kills >= 5 ? 37 : 194, String.Format("{0}", party.Player.Name));  // Name shows red if murderer, blue if not.
                        AddLabel(211, y, 1153, String.Format("{0}", members));  // Show their current party count.
                        AddLabel(367, y, 1153, String.Format("{0}", party.Type == 0 ? "PvM" : "PvP"));
                        AddLabel(540, y, party.AllowReds ? 154 : 173, String.Format("{0}", party.AllowReds ? "Yes" : "No"));  // Shows as yellow as a warning if they allow reds, green if not.
                        AddButton(705, y, 4008, 4009, match + 1, GumpButtonType.Reply, 0);
                    }                

                    y += 37;
                }
          
                /* Reference 
                AddLabel(33, 150, 0, @"Fejilian");
                AddLabel(33, 189, 0, @"Wrexsoul");       
                AddLabel(367, 150, 0, @"PvM");
                AddLabel(367, 189, 0, @"PvM");
                AddLabel(540, 150, 0, @"No");
                AddLabel(540, 189, 0, @"No");
                AddButton(705, 150, 4008, 4009, 0, GumpButtonType.Reply, 0);     
                AddButton(705, 189, 4008, 4009, 0, GumpButtonType.Reply, 0); 
                */
            }                    
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            PlayerMobile pm = from as PlayerMobile;

            if (from == null || pm == null)
                return;

            switch (info.ButtonID)
            {
                case 0:
                {
                    // Closed from right click
                    break;
                }
                case 996: //next page (if applicable)
                {
                    int nitems = 0;

                    if (matches != null)
                    {
                        nitems = matches.Count;
                    }

                    int page = CurrentPage + 1;

                    if (page > nitems / MaxPerPage)
                    {
                        page = nitems / MaxPerPage;
                    }

                    from.SendGump(new MatchmakingGump(from, page));
                    break;
                }
                case 997: //previous page (if applicable)
                {
                    int page = CurrentPage - 1;

                    if (page < 0)
                    {
                        page = 0;
                    }

                    from.SendGump(new MatchmakingGump(from, page));
                    break;
                }
                case 998:  // Leave Matchmaking
                {
                    Matchmaking.CancelWaitForParty(from);
                    break;
                }
                case 999:  // Open Gump To Join Matchmaking, with new options.
                {
                    if (pm.HasGump(typeof (MatchmakingCreateGump)))
                        pm.CloseGump(typeof (MatchmakingCreateGump));

                    if (pm.HasGump(typeof(MatchmakingGump)))
                        pm.CloseGump(typeof(MatchmakingGump));

                    pm.SendGump( new MatchmakingCreateGump(pm) );
          
                    break;
                }
                default:  // Join Party
                {
                    int partyid = info.ButtonID - 1;  // Math, so the 0 button isn't taken, we need it for right click closing.

                    if (Matchmaking.WaitingForParty.Count > partyid)
                    {
                        Matchmaking match = Matchmaking.WaitingForParty[partyid];

                        if (match == null)
                        {
                            from.SendMessage("The party was removed from Matchmaking before you could join it.");
                            return;
                        }

                        Matchmaking.AddToParty(match, from);
                    }
                    else               
                        from.SendMessage("The party was removed from Matchmaking before you could join it.");
                        
                    
                    break;
                }
            }
        }
    }

    public class MatchmakingCreateGump : Gump
    {
        Mobile caller;

        public MatchmakingCreateGump(Mobile from) : base(0, 0)
        {
            caller = from;

            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            AddPage(0);
            AddBackground(0, 0, 327, 226, 9270);
            AddLabel(78, 17, 1153, @"Create Matchmaking Party");
            AddLabel(116, 53, 1153, @"Murderers Allowed");

            AddButton(60, 174, 247, 248, 1, GumpButtonType.Reply, 0);  // Okay
            AddButton(193, 174, 242, 241, 2, GumpButtonType.Reply, 0);  // Cancel

            AddCheck(84, 53, 210, 211, false, 666); // Murderers Allowed

            // Party Type
            AddRadio(75, 130, 209, 208, false, 998);  // PvM, ticked by default.
            AddRadio(180, 130, 209, 208, false, 999);  // PvP

            AddLabel(102, 130, 1153, @"PvM");
            AddLabel(207, 130, 1153, @"PvP");
            AddLabel(119, 98, 1153, @"Party Type");

        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            PlayerMobile pm = from as PlayerMobile;          

            if (from == null || pm == null)          
                return;
            
            bool redsallowed = info.IsSwitched(666);
            int type = info.IsSwitched(998) ? 0 : 1;
        
            switch (info.ButtonID)
            {
                case 0:
                {
                    // Close on right click.
                    break;
                }
                case 1:  // Okay
                {
                    Matchmaking.WaitForParty(from, redsallowed, type);
                    if (pm.HasGump(typeof(MatchmakingCreateGump)))
                        pm.CloseGump(typeof(MatchmakingCreateGump));

                    if (pm.HasGump(typeof(MatchmakingGump)))
                        pm.CloseGump(typeof(MatchmakingGump));

                    pm.SendGump(new MatchmakingGump(pm, 0));

                    break;
                }
                case 2: // Cancel
                {
                    // Just close the gump.
                    break;
                }
            }
        }
    }
}