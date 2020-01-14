  //UO Black Box - By GoldDraco13
 //1.0.0.93

  using System.IO;
  using Server.Mobiles;

  namespace Server.UOBlackBox
  {
      class BlackBoxSender
      {
          private static readonly string BaseDir = @"C:\UOBlackBox";

          private static readonly string DataFileLoc = BaseDir + @"\DATA";

          private static readonly string DataFile = DataFileLoc + @"\BBDATA.BlackCmd";

          public static void SendBBCMD(string itemID, string hue)
          {
              if (Directory.Exists(DataFileLoc))
              {
                  WriteCommand(itemID, hue);
              }
              else
              {
                  Directory.CreateDirectory(DataFileLoc);

                  WriteCommand(itemID, hue);
              }
          }

          public static void SendBBCMD(string map, string x, string y, string z, PlayerMobile pm)
          {
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

          private static void WriteCommand(string itemID, string hue)
          {
              if (Directory.Exists(DataFileLoc))
              {
                  StreamWriter sw = new StreamWriter(DataFile, false);

                  sw.WriteLine((itemID + ":" + hue));
                  sw.Close();
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

                  pm.SendMessage(pm.Map.Name + " Marked...Sending!");
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
                  StreamWriter sw = new StreamWriter(DataFile, false);

                  sw.WriteLine(("$" + player.Name + ":" + player.X + ":" + player.Y));

                  foreach (Mobile mob in World.Mobiles.Values)
                  {
                      PlayerMobile pm = mob as PlayerMobile;

                      if (pm != null)
                      {
                          if (map == pm.Map.Name)
                          {
                              sw.WriteLine(("$" + pm.Name + ":" + pm.X + ":" + pm.Y));
                          }
                      }
                  }
                  sw.Close();

                  player.SendMessage("Found Players in " + player.Map.Name + "...Finished!");
              }
              else
              {
                  //Add Warning Message?
              }
          }
      }
  }
