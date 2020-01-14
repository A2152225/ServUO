using System;
using System.Text;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Commands
{
	public class GetKillCount
	{

		public static void Initialize()
		{
			CommandSystem.Register("GetKillCount", AccessLevel.Player, new CommandEventHandler(GenericCommand_OnCommand));
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

				string name = String.Empty;
				string typename = targeted.GetType().Name;
				string article = "a";
				string type1 = targeted.GetType().ToString();


				if (typename != null && typename.Length > 0)
				{
					if ("aeiouy".IndexOf(typename.ToLower()[0]) >= 0)
					{
						article = "an";
					}
				}
				if (targeted is Item) 
				{
				from.SendMessage("Please Target a mobile instead.");
			
				//return false;
				}
					if(targeted is Mobile)
					{
						name = ((Mobile)targeted).Name;
					
				if (name != String.Empty && name != null)
				{
					from.SendMessage("That is {0} {1} named '{2}' you have killed {3} of them", article, typename, name, ((PlayerMobile)from).GetKCount(type1));
				}
				else
				{
					from.SendMessage("That is {0} {1} with no name, you have killed {3} of them ", article, typename, ((PlayerMobile)from).GetKCount(type1));
				}
					}
			}
		}

		[Usage( "GetKillCount" )]
		public static void GenericCommand_OnCommand( CommandEventArgs e )
		{
			if(e == null || e.Mobile == null) return;

			e.Mobile.Target = new GetKillCountTarget();
		}
	}
}
