using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Gumps;

namespace Server.Items
{
	[FlipableAttribute( 0x13B9, 0x13Ba )]
	public class Pathfinder : BaseSword
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.CrushingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }

		public override int AosStrengthReq{ get{ return 40; } }
		public override int AosMinDamage{ get{ return 18; } }
		public override int AosMaxDamage{ get{ return 22; } }
		public override int AosSpeed{ get{ return 32; } }

		public override int OldStrengthReq{ get{ return 40; } }
		public override int OldMinDamage{ get{ return 6; } }
		public override int OldMaxDamage{ get{ return 34; } }
		public override int OldSpeed{ get{ return 30; } }

		public override int DefHitSound{ get{ return 0x237; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		private Gems m_Gems;

		[CommandProperty( AccessLevel.GameMaster )]
		public Gems Gems{ get{ if ( m_Gems == null )return m_Gems = new Gems( this );return m_Gems; } set{} }

		[Constructable]
		public Pathfinder() : base( 0x13B9 )
		{
			Weight = 2.0;
			Name = "Pathfinder";
			Hue = 1160;
			LootType = LootType.Blessed;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( Parent != from )
			{
				from.SendMessage( "Pathfinder must be equiped to use it." );
				return;
			}

			from.CloseGump( typeof( PathfinderWhatGump ) );
			from.CloseGump( typeof( PathfinderWhoGump ) );
			from.SendGump( new PathfinderWhatGump( from, Gems ) );
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060659, "Power\t{0}", Gems.GetPower() );
		}

		public Pathfinder( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
              		m_Gems.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_Gems = new Gems();
              		m_Gems.Deserialize( reader );
		}
	}
	public class PathfinderWhatGump : Gump
	{
		private Mobile m_From;
		private Gems m_Gems;

		public PathfinderWhatGump( Mobile from, Gems gems ) : base( 20, 30 )
		{
			m_From = from;
			m_Gems = gems;

			AddPage( 0 );

			AddBackground( 0, 0, 440, 135, 5054 );

			AddBackground( 10, 10, 420, 75, 2620 );
			AddBackground( 10, 85, 420, 25, 3000 );

			AddItem( 20, 20, 9682 );
			AddButton( 20, 110, 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 20, 90, 100, 20, 1018087, false, false ); // Animals

			AddItem( 120, 20, 9607 );
			AddButton( 120, 110, 4005, 4007, 2, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 120, 90, 100, 20, 1018088, false, false ); // Monsters

			AddItem( 220, 20, 8454 );
			AddButton( 220, 110, 4005, 4007, 3, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 220, 90, 100, 20, 1018089, false, false ); // Human NPCs

			AddItem( 320, 20, 8455 );
			AddButton( 320, 110, 4005, 4007, 4, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 320, 90, 100, 20, 1018090, false, false ); // Players
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			if ( info.ButtonID >= 1 && info.ButtonID <= 4 )
				m_From.SendGump( new PathfinderSetRangeGump( m_From, info.ButtonID-1, m_Gems ) );
		}
	}
	public class PathfinderSetRangeGump : Gump
	{
		private Mobile m_From;
		private Gems m_Gems;
		private int m_Which;

		public PathfinderSetRangeGump( Mobile from, int which, Gems gems ) : base( 20, 30 )
		{
			m_From = from;
			m_Gems = gems;
			m_Which = which;

			AddPage( 0 );

			AddBackground( 0, 0, 200, 135, 5054 );

			AddBackground( 10, 10, 180, 110, 3000 );

			AddHtml( 20, 60, 110, 20, "Power/Range: 5", false, false );
			AddHtml( 20, 60, 110, 20, "Range:", false, false );
		  	AddTextEntry( 20, 60, 110, 15, 33, 0, "0" );

			AddButton( 20, 110, 4005, 4007, 0, GumpButtonType.Reply, 0 );
			AddHtml( 20, 90, 100, 20, "Cancel", false, false );

			AddButton( 120, 110, 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddHtml( 120, 90, 100, 20, "Search", false, false );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			int range = 0;

			string[] tr = new string[ 1 ];
			foreach( TextRelay t in info.TextEntries )
			{
				tr[ t.EntryID ] = t.Text;
			}
			try { range = (int) uint.Parse( tr[ 0 ] ); } 
			catch {}

			if ( info.ButtonID == 1 )
			{
				if ( range*5 <= m_Gems.GetPower() )
					PathfinderWhoGump.DisplayTo( m_From, m_Which, m_Gems, range );
				else
					m_From.SendMessage( "Pathfinder does not have enough power to do this." );
			}
		}
	}

	public delegate bool PathfinderTypeDelegate( Mobile m );

	public class PathfinderWhoGump : Gump
	{
		private Mobile m_From;
		private int m_Range;

		private static PathfinderTypeDelegate[] m_Delegates = new PathfinderTypeDelegate[]
			{
				new PathfinderTypeDelegate( IsAnimal ),
				new PathfinderTypeDelegate( IsMonster ),
				new PathfinderTypeDelegate( IsHumanNPC ),
				new PathfinderTypeDelegate( IsPlayer )
			};

		private class InternalSorter : IComparer
		{
			private Mobile m_From;

			public InternalSorter( Mobile from )
			{
				m_From = from;
			}

			public int Compare( object x, object y )
			{
				if ( x == null && y == null )
					return 0;
				else if ( x == null )
					return -1;
				else if ( y == null )
					return 1;

				Mobile a = x as Mobile;
				Mobile b = y as Mobile;

				if ( a == null || b == null )
					throw new ArgumentException();

				return m_From.GetDistanceToSqrt( a ).CompareTo( m_From.GetDistanceToSqrt( b ) );
			}
		}

		public static void DisplayTo( Mobile from, int type, Gems gems, int range )
		{
			Map map = from.Map;

			if ( map == null || range == 0 )
				return;

			PathfinderTypeDelegate check = m_Delegates[type];

			ArrayList list = new ArrayList();

			foreach ( Mobile m in from.GetMobilesInRange( range ) )
			{
				// Ghosts can no longer be tracked 
				if ( m != from && (!Core.AOS || m.Alive) && (!m.Hidden || m.AccessLevel == AccessLevel.Player || from.AccessLevel > m.AccessLevel) && check( m ) )
					list.Add( m );
			}

			if ( list.Count > 0 )
			{
				list.Sort( new InternalSorter( from ) );

				gems.ConsumePower( range*5 );

				from.SendGump( new PathfinderWhoGump( from, list, range ) );
				from.SendLocalizedMessage( 1018093 ); // Select the one you would like to track.
			}
			else
			{
				if ( type == 0 )
					from.SendLocalizedMessage( 502991 ); // You see no evidence of animals in the area.
				else if ( type == 1 )
					from.SendLocalizedMessage( 502993 ); // You see no evidence of creatures in the area.
				else
					from.SendLocalizedMessage( 502995 ); // You see no evidence of people in the area.
			}
		}

		private static bool IsAnimal( Mobile m )
		{
			return ( !m.Player && m.Body.IsAnimal );
		}

		private static bool IsMonster( Mobile m )
		{
			return ( !m.Player && m.Body.IsMonster );
		}

		private static bool IsHumanNPC( Mobile m )
		{
			return ( !m.Player && m.Body.IsHuman );
		}

		private static bool IsPlayer( Mobile m )
		{
			return m.Player;
		}

		private ArrayList m_List;

		private PathfinderWhoGump( Mobile from, ArrayList list, int range ) : base( 20, 30 )
		{
			m_From = from;
			m_List = list;
			m_Range = range;

			AddPage( 0 );

			AddBackground( 0, 0, 440, 155, 5054 );

			AddBackground( 10, 10, 420, 75, 2620 );
			AddBackground( 10, 85, 420, 45, 3000 );

			if ( list.Count > 4 )
			{
				AddBackground( 0, 155, 440, 155, 5054 );

				AddBackground( 10, 165, 420, 75, 2620 );
				AddBackground( 10, 240, 420, 45, 3000 );

				if ( list.Count > 8 )
				{
					AddBackground( 0, 310, 440, 155, 5054 );

					AddBackground( 10, 320, 420, 75, 2620 );
					AddBackground( 10, 395, 420, 45, 3000 );
				}
			}

			for ( int i = 0; i < list.Count && i < 12; ++i )
			{
				Mobile m = (Mobile)list[i];

				AddItem( 20 + ((i % 4) * 100), 20 + ((i / 4) * 155), ShrinkTable.Lookup( m ) );
				AddButton( 20 + ((i % 4) * 100), 130 + ((i / 4) * 155), 4005, 4007, i + 1, GumpButtonType.Reply, 0 );

				if ( m.Name != null )
					AddHtml( 20 + ((i % 4) * 100), 90 + ((i / 4) * 155), 90, 40, m.Name, false, false );
			}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			int index = info.ButtonID - 1;

			if ( index >= 0 && index < m_List.Count && index < 12 )
			{
				Mobile m = (Mobile)m_List[index];

				m_From.QuestArrow = new PathfinderArrow( m_From, m, m_Range * 2 );
			}
		}
	}

	public class PathfinderArrow : QuestArrow
	{
		private Mobile m_From;
		private Timer m_Timer;

		public PathfinderArrow( Mobile from, Mobile target, int range ) : base( from, target )
		{
			m_From = from;
			m_Timer = new PathfinderTimer( from, target, range, this );
			m_Timer.Start();
		}

		public override void OnClick( bool rightClick )
		{
			if ( rightClick )
			{
				m_From = null;

				Stop();
			}
		}

		public override void OnStop()
		{
			m_Timer.Stop();

			if ( m_From != null )
				m_From.SendLocalizedMessage( 503177 ); // You have lost your quarry.
		}
	}

	public class PathfinderTimer : Timer
	{
		private Mobile m_From, m_Target;
		private int m_Range;
		private int m_LastX, m_LastY;
		private QuestArrow m_Arrow;

		public PathfinderTimer( Mobile from, Mobile target, int range, QuestArrow arrow ) : base( TimeSpan.FromSeconds( 0.25 ), TimeSpan.FromSeconds( 2.5 ) )
		{
			m_From = from;
			m_Target = target;
			m_Range = range;

			m_Arrow = arrow;
		}

		protected override void OnTick()
		{
			if ( !m_Arrow.Running )
			{
				Stop();
				return;
			}
			else if ( m_From.NetState == null || m_From.Deleted || m_Target.Deleted || m_From.Map != m_Target.Map || !m_From.InRange( m_Target, m_Range ) )
			{
				m_From.Send( new CancelArrow() );
				m_From.SendLocalizedMessage( 503177 ); // You have lost your quarry.

				Stop();
				return;
			}

			if ( m_LastX != m_Target.X || m_LastY != m_Target.Y )
			{
				m_LastX = m_Target.X;
				m_LastY = m_Target.Y;

				m_Arrow.Update( m_LastX, m_LastY );
			}
		}
	}
}