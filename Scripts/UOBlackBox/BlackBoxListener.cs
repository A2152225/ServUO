 //UO Black Box - By GoldDraco13
//1.0.0.93

using Server.Commands;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Server.UOBlackBox
{
    public class BBCmdTimer : Timer
    {
        PlayerMobile PM;
        private string cmdType;
        private bool IsFirst = true;

        public BBCmdTimer(PlayerMobile pm, string type) : base(TimeSpan.FromMilliseconds(250))
        {
            PM = pm;
            cmdType = type;
        }

        protected override void OnTick()
        {
            Stop();

            if (IsFirst)
            {
                IsFirst = false;

                if(cmdType.Contains("*"))
                {
                    BlackBoxSender.SendBBCMD(PM.Map.Name, PM.X.ToString(), PM.Y.ToString(), PM.Z.ToString(), PM);
                }
                else if(cmdType.Contains("$"))
                {
                    string getMap = cmdType.TrimStart('$');
                    BlackBoxSender.SendBBCMD(getMap, PM);
                }
            }
        }
    }

    public static class BlackBoxListener
    {
        private static bool IsRunning;

        public static bool IsStarted = false;

        private static readonly string BaseDir = @"C:\UOBlackBox";

        private static readonly string DataFileLoc = BaseDir + @"\CMD";

        private static readonly string DataFile = DataFileLoc + @"\BBCMD.BlackCmd";

        public static void StartChatWatcher()
        {
            if (Directory.Exists(DataFileLoc))
            {
                WatchBBCMD();
            }
            else
            {
                Directory.CreateDirectory(DataFileLoc);

                File.Create(DataFile);

                WatchBBCMD();
            }
            IsStarted = true;
            IsRunning = false;
        }

        private static void WatchBBCMD()
        {
            FileSystemWatcher FSW = new FileSystemWatcher(DataFileLoc, "BBCMD.BlackCmd")
            {
                EnableRaisingEvents = true,
                IncludeSubdirectories = false
            };

            FSW.NotifyFilter = NotifyFilters.Size;
            FSW.Changed += Fsw_Changed;
        }

        private static int firecount;
        private static void Fsw_Changed(object sender, FileSystemEventArgs e)
        {
            firecount++;

            if (firecount == 1)
            {
                if(!IsRunning)
                {
                    IsRunning = true;
                    RunCommand();
                }
            }
            else
            {
                firecount = 0;
            }
        }

        private static void RunCommand()
        {

            FileInfo FI = new FileInfo(DataFile);

            bool IsLocked = IsFileLocked(FI);

            while (IsLocked)
            {
                IsLocked = IsFileLocked(FI);
            }

            try
            {
                StreamReader sr = new StreamReader(DataFile);

                if (sr != null)
                {
                    string cmdDATA = sr.ReadLine();
                    sr.Close();

                    if (cmdDATA != "")
                    {
                        string[] getData = cmdDATA.Split(':');

                        string name = getData[0];
                        string cmd = getData[1];

                        List<Mobile> players = new List<Mobile>();

                        foreach (Mobile mob in World.Mobiles.Values)
                        {
                            if (mob == mob as PlayerMobile)
                            {
                                players.Add(mob);
                            }
                        }

                        if(players.Count > 0)
                        {
                            PlayerMobile pm;

                            foreach (PlayerMobile player in players)
                            {
                                if (player.Name == name)
                                {
                                    pm = player;

                                    if (pm.Name == name)
                                    {
                                        if (getData[1].Contains("$"))
                                        {
                                            BBCmdTimer timer = new BBCmdTimer(pm, getData[1]);
                                            timer.Start();
                                        }
                                        else if (getData[1].Contains("*"))
                                        {
                                            BBCmdTimer timer = new BBCmdTimer(pm, "*");
                                            timer.Start();
                                        }
                                        else if (cmd.Contains(";"))
                                        {
                                            string[] GetVal = cmd.Split(';');

                                            int landZ = 0;

                                            if (GetVal[0] == "Felucca")
                                            {
                                                LandTile landTile = Map.Felucca.Tiles.GetLandTile(Convert.ToInt32(GetVal[1]), Convert.ToInt32(GetVal[2]));
                                                landZ = (landTile.Z + 1);
                                            }
                                            if (GetVal[0] == "Trammel")
                                            {
                                                LandTile landTile = Map.Trammel.Tiles.GetLandTile(Convert.ToInt32(GetVal[1]), Convert.ToInt32(GetVal[2]));
                                                landZ = (landTile.Z + 1);
                                            }
                                            if (GetVal[0] == "Ilshenar")
                                            {
                                                LandTile landTile = Map.Ilshenar.Tiles.GetLandTile(Convert.ToInt32(GetVal[1]), Convert.ToInt32(GetVal[2]));
                                                landZ = (landTile.Z + 1);
                                            }
                                            if (GetVal[0] == "Malas")
                                            {
                                                LandTile landTile = Map.Malas.Tiles.GetLandTile(Convert.ToInt32(GetVal[1]), Convert.ToInt32(GetVal[2]));
                                                landZ = (landTile.Z + 1);
                                            }
                                            if (GetVal[0] == "Tokuno")
                                            {
                                                LandTile landTile = Map.Tokuno.Tiles.GetLandTile(Convert.ToInt32(GetVal[1]), Convert.ToInt32(GetVal[2]));
                                                landZ = (landTile.Z + 1);
                                            }
                                            if (GetVal[0] == "TerMur")
                                            {
                                                LandTile landTile = Map.TerMur.Tiles.GetLandTile(Convert.ToInt32(GetVal[1]), Convert.ToInt32(GetVal[2]));
                                                landZ = (landTile.Z + 1);
                                            }

                                            if (GetVal.Length > 3)
                                            {
                                                StringBuilder sb = new StringBuilder();

                                                sb.Append("add BlackBoxTravel ");

                                                foreach (var value in GetVal)
                                                {
                                                    sb.Append(value + " ");

                                                    if (GetVal[2] == value)
                                                    {
                                                        sb.Append(landZ + " ");
                                                    }
                                                }
                                                CommandSystem.Handle(pm, (CommandSystem.Prefix + sb.ToString().TrimEnd(' ')), Network.MessageType.Regular);
                                            }
                                            else
                                            {
                                                string CMD = ("self set map " + GetVal[0] + " x " + GetVal[1] + " y " + GetVal[2] + " z " + landZ);
                                                CommandSystem.Handle(pm, (CommandSystem.Prefix + CMD), Network.MessageType.Regular);
                                            }
                                        }
                                        else
                                        {
                                            if (cmd.Contains(pm.Name))
                                            {
                                                string[] newVal = cmd.Split(':');

                                                cmd = newVal[0];

                                                CommandSystem.Handle(pm, (CommandSystem.Prefix + cmd), Network.MessageType.Regular);
                                            }
                                            else
                                            {
                                                CommandSystem.Handle(pm, (CommandSystem.Prefix + cmd), Network.MessageType.Regular);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                //Add Warning Message?
            }
            IsRunning = false;
        }

        private static bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }

            return false;
        }
    }
}
