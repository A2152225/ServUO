 //UO Black Box - By GoldDraco13
//1.0.0.93

using Server.Commands;
using Server.Commands.Generic;

namespace Server.UOBlackBox
{
    public class BBTargetCMD : TargetCommands
    {
        public new static void Initialize()
        {
            Register(new BBGetItemIDCommand());
            Register(new BBGetHueCommand());
        }
    }

    public class BBGetItemIDCommand : BaseCommand
    {
        public BBGetItemIDCommand()
        {
            AccessLevel = AccessLevel.Administrator;
            Supports = CommandSupport.All;
            Commands = new[] { "BBGetItemID" };
            ObjectTypes = ObjectTypes.All;
            Usage = "BBGetItemID < propertyName > ";
            Description = "Gets ItemID for Black Box.";
        }

        public override void Execute(CommandEventArgs e, object obj)
        {
            if (e.Length >= 1)
            {
                for (var i = 0; i < e.Length; ++i)
                {
                    var result = Properties.GetValue(e.Mobile, obj, e.GetString(i));

                    if (result == "Property not found." || result == "Property is write only." || result.StartsWith("Getting this property"))
                    {
                        LogFailure(result);
                    }
                    else
                    {
                        AddResponse(result);

                        if (result.Contains("("))
                        {
                            string[] getVal = result.TrimEnd(')').Split('(');

                            string nameID = getVal[1];

                            BlackBoxSender.SendBBCMD(nameID, "0");
                        }
                    }
                }
            }
            else
            {
                LogFailure("Format: Get <propertyName>");
            }
        }
    }

    public class BBGetHueCommand : BaseCommand
    {
        public BBGetHueCommand()
        {
            AccessLevel = AccessLevel.Administrator;
            Supports = CommandSupport.All;
            Commands = new[] { "BBGetHue" };
            ObjectTypes = ObjectTypes.All;
            Usage = "BBGetHue <propertyName>";
            Description = "Gets Hue for Black Box.";
        }

        public override void Execute(CommandEventArgs e, object obj)
        {
            if (e.Length >= 1)
            {
                for (var i = 0; i < e.Length; ++i)
                {
                    var result = Properties.GetValue(e.Mobile, obj, e.GetString(i));

                    if (result == "Property not found." || result == "Property is write only." || result.StartsWith("Getting this property"))
                    {
                        LogFailure(result);
                    }
                    else
                    {
                        AddResponse(result);

                        if (result.Contains(" = "))
                        {
                            string[] getVal = result.Split('(');
                            string[] getHue = getVal[0].Split('=');
                            string hue = "0";

                            if (getHue[1] != null)
                            {
                                hue = getHue[1].Trim(' ');
                            }

                            BlackBoxSender.SendBBCMD("0", hue);
                        }
                    }
                }
            }
            else
            {
                LogFailure("Format: Get <propertyName>");
            }
        }
    }
}
