using System;
using System.Text;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Commands
{
	public class ParagonView 
	{

		public static void Initialize()
		{
			CommandSystem.Register("Paragon", AccessLevel.Player, new CommandEventHandler(GenericCommand_OnCommand));
		}

		public class GetKillCountTarget : Target
		{

			public GetKillCountTarget()
				: base(30, true, TargetFlags.None)
			{
				CheckLOS = false;
			}
			protected override void OnTarget( Mobile from, object targeted )
			{
				if(from == null || targeted == null) return;

			
				//return false;
			
					if(targeted is PlayerMobile)
					{
					PlayerMobile pm = ((PlayerMobile)targeted);
								from.SendGump( new ParagonGump( pm ) );

				
					}
					else{from.SendMessage("Please Target a player.");return;}
			}
		}

		[Usage( "GetKillCount" )]
		public static void GenericCommand_OnCommand( CommandEventArgs e )
		{
			if(e == null || e.Mobile == null) return;
			PlayerMobile pm = (PlayerMobile) e.Mobile;
			if (e.Mobile.AccessLevel > AccessLevel.Player)
			e.Mobile.Target = new GetKillCountTarget();
			else
			pm.SendGump( new ParagonGump( pm ) );
		}
	}
}
