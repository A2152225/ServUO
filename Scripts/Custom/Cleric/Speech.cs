using System;
using System.Collections;
using Server.Mobiles;
using Server.Gumps;
using Server.Targeting;
using Server.Items;
using Server.Prompts;

namespace Server
{
	public class ClericCommands
	{
		public static void Initialize()
		{
			EventSink.Speech += new SpeechEventHandler( Speech_Event );
		}

		public static void Speech_Event( SpeechEventArgs e )
		{
			if ( e.Speech.ToLower().IndexOf( "i pray to the gods" ) != -1 )
			{
				if ( !e.Mobile.CheckAlive() )
				{
					e.Mobile.SendMessage( "You cannot pray while dead." );
				}
				else
				{
					e.Mobile.CloseGump( typeof ( ClericGump ) );
					e.Mobile.SendGump( new ClericGump( e.Mobile ) );
				}
			}
		}
	}
}	