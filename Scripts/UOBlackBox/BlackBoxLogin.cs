 //UO Black Box - By GoldDraco13
//1.0.0.93

using System;
using Server.Mobiles;
using System.Collections.Generic;

namespace Server.UOBlackBox
{
    public class BlackBoxLogin
    {
        public static void Initialize()
        {
            EventSink.Login += new LoginEventHandler(OnLogin);
        }

        private static void OnLogin(LoginEventArgs e)
        {
            foreach (Item item in World.Items.Values)
            {
                if (item.Name != null)
                {
                    if (item.Name.Contains("Black Box Moongate") || item.Name.Contains("Black Box SoulStone"))
                    {
                        BlackBoxTravel BBT = item as BlackBoxTravel;

                        if (BBT.CheckCleanUp)
                        {
                            item.Delete();
                        }
                        else
                        {
                            BBT.CheckCleanUp = true;
                        }
                    }

                    if (item.Name.Contains("Black Box Portal"))
                    {
                        BlackBoxTravelEnd BBT = item as BlackBoxTravelEnd;

                        if (BBT.CheckCleanUp)
                        {
                            item.Delete();
                        }
                        else
                        {
                            BBT.CheckCleanUp = true;
                        }
                    }
                }
            }

            List<PlayerMobile> pmList = new List<PlayerMobile>();

            foreach (Mobile mob  in World.Mobiles.Values)
            {
                if (mob == mob as PlayerMobile)
                {
                    PlayerMobile pm = mob as PlayerMobile;
                    if (pm != null)
                    {
                        if (mob.Map == Map.Internal && pm.Name == "")
                            pmList.Add(pm);
                    }
                }
            }

            int cnt = 0;

            foreach (PlayerMobile playerMobile in pmList)
            {
                playerMobile.Delete();
                cnt++;
            }

            if (cnt > 0)
            {
                Utility.WriteConsoleColor(ConsoleColor.Red, "UOBlackBox: " + cnt + " Phantoms Removed! - [(Empty PlayerMobile) Clean Up]");
            }
            else
            {
                Utility.WriteConsoleColor(ConsoleColor.Green, "UOBlackBox: " + cnt + " Phantoms Found! - [(Empty PlayerMobile) Clean Up]");
            }

            if (BlackBoxListener.IsStarted == false)
                BlackBoxListener.StartChatWatcher();
        }
    }
}
