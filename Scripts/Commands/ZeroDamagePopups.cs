using Server;
using Server.Commands;
using Server.Mobiles;

namespace Server.Commands
{
    public static class ZeroDamagePopupsCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("ZeroDamagePopups", AccessLevel.Player, OnCommand);
        }

        private static void OnCommand(CommandEventArgs e)
        {
            if (e.Mobile is PlayerMobile pm)
            {
                if (e.Length == 0)
                {
                    pm.SendMessage(38, $"Zero-damage popups currently {(pm.ShowZeroDamagePopups ? "ON" : "OFF")}.");
                    pm.SendMessage(38, "Usage: [ZeroDamagePopups on|off");
                    return;
                }

                var arg = e.GetString(0).ToLowerInvariant();
                if (arg == "on" || arg == "true")
                {
                    pm.ShowZeroDamagePopups = true;
                    pm.SendMessage(38, "Zero-damage popups turned ON.");
                }
                else if (arg == "off" || arg == "false")
                {
                    pm.ShowZeroDamagePopups = false;
                    pm.SendMessage(38, "Zero-damage popups turned OFF.");
                }
                else
                {
                    pm.SendMessage(38, "Usage: [ZeroDamagePopups on|off");
                }
            }
        }
    }
}   
