using System;
using Server;
using Server.Commands;

namespace Server.Commands
{
    public static class HealthBarModeCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("HealthBarMode", AccessLevel.GameMaster, OnHealthBarMode);
            CommandSystem.Register("HBMode", AccessLevel.GameMaster, OnHealthBarMode);
        }

        [Usage("HealthBarMode [real|hit|periodic|0|1|2]")]
        [Description("Sets the global health-bar presentation for difficulty scaling.")]
        private static void OnHealthBarMode(CommandEventArgs e)
        {
            if (e.Length == 0)
            {
                e.Mobile.SendMessage(0x55, $"HealthBarMode is currently: {Server.DifficultySettings.HealthBarPresentation}");
                e.Mobile.SendMessage(0x55, "Usage: [HealthBarMode real|hit|periodic (or 0|1|2)");
                return;
            }

            var arg = e.GetString(0).Trim().ToLowerInvariant();
            DifficultySettings.HealthBarMode mode;

            switch (arg)
            {
                case "0":
                case "real":
                case "off":
                case "engine":
                    mode = DifficultySettings.HealthBarMode.Real;
                    break;
                case "1":
                case "hit":
                case "perhit":
                case "onhit":
                    mode = DifficultySettings.HealthBarMode.PerceivedOnHit;
                    break;
                case "2":
                case "periodic":
                case "refresh":
                case "both":
                case "onhit+periodic":
                    mode = DifficultySettings.HealthBarMode.PerceivedOnHitAndPeriodic;
                    break;
                default:
                    e.Mobile.SendMessage(0x22, "Invalid value. Use: real|hit|periodic or 0|1|2.");
                    return;
            }

            DifficultySettings.HealthBarPresentation = mode;
            e.Mobile.SendMessage(0x55, $"HealthBarMode set to: {mode}");

            // Optional: nudge nearby bars to refresh immediately
            foreach (Mobile m in e.Mobile.GetMobilesInRange(18))
            {
                if (m is Server.Mobiles.BaseCreature bc && !bc.Controlled && !bc.Summoned)
                {
                    Timer.DelayCall(TimeSpan.Zero, () =>
                    {
                        var mi = typeof(Server.Mobiles.BaseCreature).GetMethod(
                            "UpdatePerceivedHealthForNearbyPlayers",
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        mi?.Invoke(bc, null);
                    });
                }
            }
        }
    }
}
