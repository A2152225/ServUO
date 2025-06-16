
using System;
using Server.Network;
using Server.Mobiles;
using Server.Commands;

namespace Server
{
	

	public class AnnounceLogin
	{
		public static void Initialize()
		{
			EventSink.Login += new LoginEventHandler( World_Login );
		}

		private static void World_Login( LoginEventArgs args )
		{
			foreach( Mobile m in World.Mobiles.Values )
			{
				if ( m is PlayerMobile )
				{
					PlayerMobile p = m as PlayerMobile;
					PlayerMobile p2 = args.Mobile as PlayerMobile;

					if ( p.NetState != null  )
					{
						if ( p2.AccessLevel < AccessLevel.Seer) 
							p.SendMessage(353, "{0} has logged into the world.", p2.Name);
					}
				}
			}
		}
	}

	public class AnnounceLogout
	{
		public static void Initialize()
		{
			EventSink.Logout += new LogoutEventHandler( World_Logout );
		}

		private static void World_Logout( LogoutEventArgs args )
		{
			foreach( Mobile m in World.Mobiles.Values )
			{
				if ( m is PlayerMobile )
				{
					PlayerMobile p = m as PlayerMobile;
					PlayerMobile p2 = args.Mobile as PlayerMobile;

					if ( p.NetState != null )
					{
						if ( p2.AccessLevel < AccessLevel.Seer )
							p.SendMessage(353, "{0} has logged out of the world.", p2.Name);
					}
				}
			}
		}
	}
}