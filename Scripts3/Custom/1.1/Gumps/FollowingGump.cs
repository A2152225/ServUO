using System; 
using System.Net; 
using Server; 
using Server.Accounting; 
using Server.Gumps; 
using Server.Items; 
using Server.Mobiles; 
using Server.Network; 
using Server.Misc; 

namespace Server.Gumps 
{ 
	public class FollowingGump : Gump
	{
		private Mobile m_Mobile;
		//private OverallRaceStone m_Stone;

		public FollowingGump( Mobile mobile/*, OverallRaceStone stone*/ ) : base( 25, 50 )
		{
			Closable = false; 
			Dragable = false; 
			mobile.Frozen = true; 

			m_Mobile = mobile;
			//m_Stone = stone;

			AddPage( 0 );

			AddBackground( 25, 10, 420, 200, 5054 );

			AddImageTiled( 33, 20, 401, 181, 2624 );
			AddAlphaRegion( 33, 20, 401, 181 );

			AddLabel( 125, 148, 1152, "Pick a path to follow." );

			AddButton( 43, 50, 4005, 4007, 0, GumpButtonType.Reply, 0 );
			AddLabel( 73, 50, 1152, "Good" );
			AddButton( 43, 75, 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddLabel( 73, 75, 1152, "Evil" );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile; 
			PlayerMobile pm = (PlayerMobile)from;

			if ( from == null )
				return;

			if ( info.ButtonID == 0 )
			{
				pm.Following = Following.Good;
				from.Frozen = false;
			}
			if ( info.ButtonID == 1 )
			{
				pm.Following = Following.Evil;
				from.Frozen = false;
			}
		}
	}
} 
