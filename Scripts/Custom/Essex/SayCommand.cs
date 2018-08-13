using System;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;
using Server.Items;
using Server.Commands;

namespace Server.Scripts.Commands
{
	public class CustomCmdHandlers
	{
		public static void Initialize()
		{
			Register( "Say", AccessLevel.Developer, new CommandEventHandler( Say_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
			CommandSystem.Register( command, access, handler );
		}
		

		[Usage( "Say <text>" )]
		[Description( "Displays <text> above a mobile or item." )]
		public static void Say_OnCommand( CommandEventArgs e )
		{
			string toSay = e.ArgString.Trim();

			if ( toSay.Length > 0 )
				e.Mobile.Target = new SayTarget( toSay, false );
			else
				e.Mobile.SendMessage( "Format: Say \"<text>\"" );
		}

		private class SayTarget : Target
		{
			private string m_toSay;
			private bool targetItem;

			public SayTarget( string say, bool item ) : base( -1, false, TargetFlags.None )
			{
				m_toSay = say;
				targetItem = item;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Mobile )
				{
					Mobile targ = (Mobile)targeted;
					if ( from != targ && from.AccessLevel > targ.AccessLevel )
					{
						CommandLogging.WriteLine( from, "{0} {1} forcing speech on {2}", from.AccessLevel, CommandLogging.Format( from ), CommandLogging.Format( targ ) );
						targ.Say( m_toSay );
					}
				}
				else if ( targeted is Item )
				{
					Item targ = (Item)targeted;
					targ.PublicOverheadMessage( MessageType.Regular, Utility.RandomDyedHue(), false, m_toSay);
				}
				else 
					from.SendMessage( "Invaild Target Type" );
			}
		}
	}
}