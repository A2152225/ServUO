using System;
using Server;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Mobiles
{
    public class ExpBarGump : Gump
    {
        public ExpBarGump(PlayerMobile m) : base(20, 20)
        {
            Dragable = true;
            Closable = false;

            PlayerMobile from = m;
            from.CloseGump(typeof(ExpBarGump));

            int Level = from.LvL;
            long LastLevel = from.LastLevelExp;
            long ExpRequired = ((long)(Experience.GetNextLevelXP(from)));

            if (ExpRequired == 0 || (LastLevel == 0))
            {
                ExpRequired = 100;

                if (from.HorizontalExpBar == false)
                {
                    AddImage(0, 0, 9741);
                    AddImageTiled(0, 0, 8, 96 - (int)(decimal.Divide(1, 100) * 96), 9740);
                }
                else
                {
                    AddImage(0, 0, 9750);
                    AddImageTiled(0, 0, (int)(decimal.Divide(1, 100) * 96), 8, 9751);
                }
            }
            else if (from.HorizontalExpBar == false)
            {
                AddImage(0, 0, 9741);
                AddImageTiled(0, 0, 8, 96 - (int)(decimal.Divide(from.EXP, Experience.GetNextLevelXP(from)) * 96), 9740);
            }
            else
            {
                AddImage(0, 0, 9750);
                AddImageTiled(0, 0, (int)(decimal.Divide(from.EXP, Experience.GetNextLevelXP(from)) * 96), 8, 9751);
            }
        }
    }
}