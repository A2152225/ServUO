 //UO Black Box - By GoldDraco13
//1.0.0.99

using System;
using Server.Mobiles;

namespace Server.UOBlackBox
{
    public class BBCmdTimer : Timer
    {
        private PlayerMobile PM;
        private string cmdType;

        public BBCmdTimer(PlayerMobile pm, string type) : base(TimeSpan.FromMilliseconds(250))
        {
            PM = pm;
            cmdType = type;
        }

        protected override void OnTick()
        {
            Stop();

            if (cmdType.Contains("*"))
            {
                BlackBoxSender.SendBBCMD(PM.Map.Name, PM.X.ToString(), PM.Y.ToString(), PM.Z.ToString(), PM);
            }
            else if (cmdType.Contains("$"))
            {
                string getMap = cmdType.TrimStart('$');
                BlackBoxSender.SendBBCMD(getMap, PM);
            }
            else
            {
                if (cmdType.Contains("Finished"))
                {
                    BlackBoxSender.SendBBCMD(PM);
                }
            }
        }
    }
}
