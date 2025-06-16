 //UO Black Box - By GoldDraco13
//1.0.0.99

using System.IO;
using System.Text;
using Server.Mobiles;

namespace Server.UOBlackBox
{
    class BlackBoxSender
    {
        private static readonly string BaseDir = @"C:\UOBlackBox\UOBlackBoxServer";

        private static readonly string DataFileLoc = BaseDir + @"\DATA";

        private static string DataFile;

        public static void SendBBCMD(string itemID, string hue, PlayerMobile pm)
        {
            DataFile = DataFileLoc + @"\" + pm.Name + "BBDATA.BlackCmd";

            if (Directory.Exists(DataFileLoc))
            {
                WriteCommand(itemID, hue, pm);
            }
            else
            {
                Directory.CreateDirectory(DataFileLoc);

                WriteCommand(itemID, hue, pm);
            }
        }

        public static void SendBBCMD(string map, string x, string y, string z, PlayerMobile pm)
        {
            DataFile = DataFileLoc + @"\" + pm.Name + "BBDATA.BlackCmd";

            if (Directory.Exists(DataFileLoc))
            {
                WriteCommand(map, x, y, z, pm);
            }
            else
            {
                Directory.CreateDirectory(DataFileLoc);

                WriteCommand(map, x, y, z, pm);
            }
        }

        public static void SendBBCMD(string map, PlayerMobile pm)
        {
            DataFile = DataFileLoc + @"\" + pm.Name + "BBDATA.BlackCmd";

            if (Directory.Exists(DataFileLoc))
            {
                WriteCommand(map, pm);
            }
            else
            {
                Directory.CreateDirectory(DataFileLoc);

                WriteCommand(map, pm);
            }
        }

        public static void SendBBCMD(PlayerMobile pm)
        {
            DataFile = DataFileLoc + @"\" + pm.Name + "BBDATA.BlackCmd";

            if (Directory.Exists(DataFileLoc))
            {
                WriteCommand(pm);
            }
            else
            {
                Directory.CreateDirectory(DataFileLoc);

                WriteCommand(pm);
            }
        }

        public static void SendBBCMD(string build, Mobile from)
        {
            PlayerMobile pm = from as PlayerMobile;

            DataFile = DataFileLoc + @"\" + pm.Name + "BBDATA.BlackCmd";

            if (Directory.Exists(DataFileLoc))
            {
                WriteCommand(pm, build);
            }
            else
            {
                Directory.CreateDirectory(DataFileLoc);

                WriteCommand(pm, build);
            }
        }

        private static void WriteCommand(string itemID, string hue, PlayerMobile pm)
        {
            if (Directory.Exists(DataFileLoc))
            {
                StreamWriter sw = new StreamWriter(DataFile, false);

                sw.WriteLine((itemID + ":" + hue));

                sw.Close();

                if (hue != "0")
                {
                    pm.SendMessage("Hue = " + hue + " ...Sending!");
                }
                else
                {
                    pm.SendMessage("ItemID = " + itemID + " ...Sending!");
                }
            }
            else
            {
                //Add Warning Message?
            }
        }

        private static void WriteCommand(string map, string x, string y, string z, PlayerMobile pm)
        {
            if (Directory.Exists(DataFileLoc))
            {
                StreamWriter sw = new StreamWriter(DataFile, false);

                sw.WriteLine(("*" + map + ":" + x + ":" + y + ":" + z));

                sw.Close();

                pm.SendMessage(pm.Map.Name + " : " + x + " : " + y + " : " + z + " - Marked ...Sending!");
            }
            else
            {
                //Add Warning Message?
            }
        }

        private static void WriteCommand(string map, PlayerMobile player)
        {
            if (Directory.Exists(DataFileLoc))
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(("$" + player.Name + ":" + player.X + ":" + player.Y + ";"));

                int count = 0;

                foreach (Mobile mob in World.Mobiles.Values)
                {
                    PlayerMobile pm = mob as PlayerMobile;

                    if (pm != null && pm.Name != player.Name)
                    {
                        if (map == pm.Map.Name)
                        {
                            sb.Append(("$" + pm.Name + ":" + pm.X + ":" + pm.Y + ";"));

                            count++;
                        }
                    }
                }
                StreamWriter sw = new StreamWriter(DataFile, false);

                sw.WriteLine(sb);

                sw.Close();

                player.SendMessage("Found " + count + " Players in " + map + " ...Sending!");
            }
            else
            {
                //Add Warning Message?
            }
        }

        private static void WriteCommand(PlayerMobile pm)
        {
            if (Directory.Exists(DataFileLoc))
            {
                StreamWriter sw = new StreamWriter(DataFile, false);

                sw.WriteLine("Finished");

                sw.Close();

                pm.SendMessage("...");
            }
            else
            {
                //Add Warning Message?
            }
        }

        private static void WriteCommand(PlayerMobile pm, string build)
        {
            if (Directory.Exists(DataFileLoc))
            {
                StreamWriter sw = new StreamWriter(DataFile, false);

                sw.WriteLine(build);

                sw.Close();

                pm.SendMessage(pm.Name + " : Save Blueprint ...Sending!");
            }
            else
            {
                //Add Warning Message?
            }
        }
    }
}
