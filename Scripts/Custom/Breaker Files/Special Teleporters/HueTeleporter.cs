using System;
using Server;
using Server.Network;
using Server.Spells;
using System.Collections;

namespace Server.Items
{
	public class HueTeleporter : Teleporter
	{
		private string m_Substring;
		private int m_Keyword;
		private int m_Range;

		[CommandProperty( AccessLevel.GameMaster )]
		public string Substring
		{
			get{ return m_Substring; }
			set{ m_Substring = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Keyword
		{
			get{ return m_Keyword; }
			set{ m_Keyword = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Range
		{
			get{ return m_Range; }
			set{ m_Range = value; InvalidateProperties(); }
		}

		public override bool HandlesOnSpeech{ get{ return true; } }

		public override void OnSpeech( SpeechEventArgs args ) //within 15 tiles
		{
		Point3D p = Point3D.Zero ;
		Point3D BC = Point3D.Zero ;
		Map map;
		ArrayList list = new ArrayList();
		
		
			if ( !args.Handled && Active )
			{
				Mobile m = args.Mobile;

				if ( !Creatures && !m.Player )
					return;

				//if ( !m.InRange( GetWorldLocation(), m_Range ) )
				//	return;
				if( Insensitive.StartsWith( args.Speech, "hue" ) )
				{


					string[] split = args.Speech.Split( ' ' );

					if ( split.Length == 2 )
					{
						try
						{
							string name = split[0];
							int value = Convert.ToInt32( split[1] );
							//
							if (value < 0 || value > 2999)
							{
							m.SendMessage("Please Pick a number between 0 and 2999 -- {0}",value);
							//return; 
							}
							else
							{
						//	m.SendMessage("You've Picked hue {0)",value);
									
									foreach ( Item R in this.Map.GetItemsInRange(this.Location, 150 ) )
									{
									list.Add(R);
									}
									
									foreach( Item R in list)
									{
									//m.SendMessage("{0}",R.Name);
									if (value == R.Hue && R is Robe)
									{
		//	Map map = m_MapDest;

		//	if ( map == null || map == Map.Internal )
		//		map = m.Map;
		//	map = m.Map;
			 p = R.Location;
								BC = p;
			
									}
									}
				if(BC == Point3D.Zero)
				{
				m.SendMessage("{0} is a Black Hue. Please choose a different number.",value);
				args.Handled = true;
				return;
				}
							
	
			}
							
			if ( p == Point3D.Zero )
				p = m.Location;
				
				

			Server.Mobiles.BaseCreature.TeleportPets( m, p, m.Map );

			m.MoveToWorld( p, m.Map );			
				//	m.SendMessage("You moved to {0}",p);
						}
						 catch( Exception e )
						{
						m.SendMessage("You failed to move E:{0}",e);
						}
						args.Handled = true;
					}
				}
				
			}
		}

		public override bool OnMoveOver( Mobile m )
		{
			return true;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

		}

		[Constructable]
		public HueTeleporter()
		{
			m_Keyword = -1;
			m_Substring = null;
			Name = "Hue Teleporter";
		}

		public HueTeleporter( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_Substring );
			writer.Write( m_Keyword );
			writer.Write( m_Range );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Substring = reader.ReadString();
					m_Keyword = reader.ReadInt();
					m_Range = reader.ReadInt();

					break;
				}
			}
		}
	}
}