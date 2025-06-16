 //UO Black Box - By GoldDraco13
//1.0.0.99

using System;
using System.IO;
using System.Text;
using System.Threading;
using Server.Commands;
using Server.Mobiles;

namespace Server.UOBlackBox
{
    class BBControlBox : Item
    {
        private FileSystemWatcher FSW = null;
        private readonly string DataFileLoc = @"C:\UOBlackBox\UOBlackBoxServer\CMD"; 
        private PlayerMobile PM;

        [Constructable]
        public BBControlBox() : base(0x9F64)
        {
            Name = "UO Black Box - OFF";
            Hue = 1175;
        }

        public BBControlBox(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!(from is PlayerMobile pm))
            {
                return;
            }
            else
            {
                PM = pm;

                if (IsChildOf(pm.Backpack))
                {
                    if (FSW == null)
                    {
                        pm.SendMessage("Started - [NEW] UO Black Box File Watcher!");

                        FSW = new FileSystemWatcher(DataFileLoc, pm.Name + "BBCMD.BlackCmd")
                        {
                            EnableRaisingEvents = true,
                            IncludeSubdirectories = false
                        };

                        FSW.NotifyFilter = NotifyFilters.Size;
                        FSW.Changed += Fsw_Changed;

                        Movable = false;

                        Name = "UO Black Box - On";
                        Hue = 1153;
                    }
                    else
                    {
                        if (FSW.EnableRaisingEvents == false)
                        {
                            pm.SendMessage("Started - UO Black Box File Watcher!");

                            FSW.EnableRaisingEvents = true;

                            Movable = false;

                            Name = "UO Black Box - On";
                            Hue = 1153;
                        }
                        else
                        {
                            pm.SendMessage("Stopped - UO Black Box File Watcher!");

                            FSW.EnableRaisingEvents = false;

                            Movable = true;

                            Name = "UO Black Box - Off";
                            Hue = 1175;
                        }
                    }
                }
                else
                {
                    pm.SendMessage("This needs to be in your backpack in order to use!");

                    Movable = true;

                    Name = "UO Black Box - Off";
                    Hue = 1175;
                }
            }
        }

        public override void OnDelete()
        {
            if (FSW != null)
            {
                FSW.Dispose();
            }
            base.OnDelete();
        }

        private int firecount; //To Catch Second File Watcher Stream

        private void Fsw_Changed(object sender, FileSystemEventArgs e)
        {
            string file = e.FullPath;

            if (PM != null)
            {
                firecount++;

                if (firecount == 1)
                {
                    Hue = 1152;

                    RunCommand(file);

                    Thread.Sleep(250);

                    Hue = 1153;
                }
                else
                {
                    firecount = 0;
                }
            }
            else
            {
                FSW.EnableRaisingEvents = false;

                firecount = 0;
            }
        }

        private void RunCommand(string file)
        {
            FileInfo FI = new FileInfo(file);

            bool IsLocked = IsFileLocked(FI);

            while (IsLocked)
            {
                IsLocked = IsFileLocked(FI);
            }

            StreamReader sr = new StreamReader(file);

            string cmdDATA = sr.ReadLine();

            sr.Close();

            if (cmdDATA != "")
            {
                string[] getData = cmdDATA.Split(':');

                string name = getData[0];
                string cmd = getData[1];

                if (PM.Name == name)
                {
                    if (getData[1].Contains("$"))
                    {
                        BBCmdTimer timer = new BBCmdTimer(PM, getData[1]);
                        timer.Start();
                    }
                    else if (getData[1].Contains("*"))
                    {
                        BBCmdTimer timer = new BBCmdTimer(PM, "*");
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
                            CommandSystem.Handle(PM, (CommandSystem.Prefix + sb.ToString().TrimEnd(' ')), Network.MessageType.Regular);
                        }
                        else
                        {
                            string CMD = ("self set map " + GetVal[0] + " x " + GetVal[1] + " y " + GetVal[2] + " z " + landZ);
                            CommandSystem.Handle(PM, (CommandSystem.Prefix + CMD), Network.MessageType.Regular);
                        }
                        BBCmdTimer timer = new BBCmdTimer(PM, "Finished");
                        timer.Start();
                    }
                    else
                    {
                        if (cmd.Contains(PM.Name))
                        {
                            string[] newVal = cmd.Split(':');

                            cmd = newVal[0];

                            CommandSystem.Handle(PM, (CommandSystem.Prefix + cmd), Network.MessageType.Regular);
                        }
                        else
                        {
                            CommandSystem.Handle(PM, (CommandSystem.Prefix + cmd), Network.MessageType.Regular);
                        }

                        if (!cmd.Contains("BBGetItemID"))
                        {
                            if (!cmd.Contains("BBGetHue"))
                            {
                                if (!cmd.Contains("BBSaveBuild"))
                                {
                                    BBCmdTimer timer = new BBCmdTimer(PM, "Finished");
                                    timer.Start();
                                }
                            }
                        }
                    }
                }
            }
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

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); //version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            Movable = true;

            Name = "UO Black Box - Off";
            Hue = 1175;
        }
    }
}
