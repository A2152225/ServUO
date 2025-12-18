using System;
using Server.Commands.Generic;

namespace Server.Commands
{
    public class ConsoleEchoCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("ConsoleEcho", AccessLevel.Administrator, new CommandEventHandler(ConsoleEcho_OnCommand));
        }

        [Usage("ConsoleEcho")]
        [Description("Toggles whether command outputs are echoed to the console. When enabled, outputs from commands like [Area get] will be displayed in the server console.")]
        private static void ConsoleEcho_OnCommand(CommandEventArgs e)
        {
            // Toggle the console echo flag
            BaseCommand.ConsoleEcho = !BaseCommand.ConsoleEcho;

            // Inform the user of the current state
            if (BaseCommand.ConsoleEcho)
            {
                e.Mobile.SendMessage(0x35, "Command outputs will now be echoed to the console.");
                Console.WriteLine("[ConsoleEcho] {0} enabled console echo for command outputs.", e.Mobile.Name);
            }
            else
            {
                e.Mobile.SendMessage(0x23, "Command outputs will no longer be echoed to the console.");
                Console.WriteLine("[ConsoleEcho] {0} disabled console echo for command outputs.", e.Mobile.Name);
            }
        }
    }
}
