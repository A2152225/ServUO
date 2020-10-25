 //UO Black Box - By GoldDraco13
//1.0.0.99
using Server.Gumps;
using Server.Mobiles;
using Server.Network;

namespace Server.UOBlackBox
{
    class BBGump : Gump
    {
        private readonly PlayerMobile player;

        public BBGump(PlayerMobile pm, int GumpID, int BBX, int BBY, int BBW, int BBH, string BBText, int BBHue, string BBtype) : base(0, 0)
        {
            player = pm;

            AddPage(0);

            if (BBtype == "Background")
                AddBackground(BBX, BBY, BBW, BBH, GumpID);
            else
                AddBackground((BBX - 25), (BBY - 25), (BBW + 50), (BBH + 50), 83);

            if (BBtype == "Alpha Region")
                AddAlphaRegion(BBX, BBY, BBW, BBH);

            if (BBtype == "Image")
                AddImage(BBX, BBY, GumpID);
            if (BBtype == "Image Tiled")
                AddImageTiled(BBX, BBY, BBW, BBH, GumpID);

            if (BBtype == "Label")
                AddLabel(BBX, BBY, BBHue, BBText);
            if (BBtype == "Label Cropped")
                AddLabelCropped(BBX, BBY, BBW, BBH, BBHue, BBText);

            if (BBtype == "TextEntry")
                AddTextEntry(BBX, BBY, BBW, BBH, BBHue, 0, BBText, BBText.Length);
            if (BBtype == "Html")
                AddHtml(BBX, BBY, BBW, BBH, BBText, false, false);

            if (BBtype == "Item")
                AddItem(BBX, BBY, GumpID, BBHue);
            if (BBtype == "Button")
                AddButton(BBX, BBY, GumpID, (GumpID++), 0, GumpButtonType.Reply, 0); //GumpButtonType.Page

            if (BBtype == "Radio")
                AddRadio(BBX, BBY, GumpID, (GumpID++), true, 0);
            if (BBtype == "Check")
                AddCheck(BBX, BBY, GumpID, (GumpID++), true, 0);
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            switch (info.ButtonID)
            {
                case 0:
                {
                      player.SendMessage("It Works!");
                      break;
                }
                default:
                {
                      break;
                }
            }
        }
    }
}
