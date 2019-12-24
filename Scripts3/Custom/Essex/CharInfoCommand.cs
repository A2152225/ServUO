using System; 
using System.Collections; 
using Server; 
using Servers; 
using Server.Network; 
using Server.Mobiles; 
using Server.Gumps; 
using Server.Commands;


namespace Servers
{ 
	public class CharInfo
	{ 
		public static void Initialize()
		{ 
			CommandSystem.Register( "CharInfo", AccessLevel.Player, new CommandEventHandler( CharInfo_OnCommand ) );    
		} 

		private static void CharInfo_OnCommand( CommandEventArgs args ) 
		{ 
			Mobile m = args.Mobile; 
			PlayerMobile from = m as PlayerMobile;

			if( from != null ) 
			{  
				from.SendGump( new CharInfoGump( from ) ); 
			}
		}  
	} 
} 
