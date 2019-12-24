using System;
using System.Collections;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Regions;

namespace Server.Items
{

	
	[DispellableFieldAttribute]
	public class LvlGate : Moongate
	{
	

		private int m_Lvl;
		
	[CommandProperty( AccessLevel.Developer )]
	public int Lvl{ get{ return m_Lvl; } set{ m_Lvl = value; } }

	

		[Constructable]
		public LvlGate() : this( Point3D.Zero, null )
		{
		Dispellable = false;
			Movable = false;
			Hue = 1;
			Light = LightType.Circle300;
			Lvl = 2000;
		}

		public LvlGate( int LvL ) :  this( Point3D.Zero, null )
		{
		Lvl = LvL;
		Dispellable = false;
			Movable = false;
			Hue = 1;
			Light = LightType.Circle300;
		}
		
		public LvlGate( Point3D target, Map targetMap ) : base( target, targetMap )
		{
			Name = "Level Gate";
			Lvl = 2000;
			Dispellable = false;
			Movable = false;
			Location = target;
			Map = targetMap;
			Hue = 1;
			Light = LightType.Circle300;
		}

		
		public LvlGate( Serial serial ) : base( serial )
		{
		}


		public override void UseGate( Mobile m )
		{
			if ( m.Spell != null )
			{
				m.SendLocalizedMessage( 1049616 ); // You are too busy to do that at the moment.
			}
			else if ( ((PlayerMobile)m).LvL >= Lvl || ((PlayerMobile)m).PrestigeLvl >= 1 )
			{
				
						BaseCreature.TeleportPets( m, Target, TargetMap);

						m.Map = TargetMap;
						m.Location = Target;

						m.PlaySound( 0x1FE );
						if (((PlayerMobile)m).LvL >= Lvl)
						m.SendMessage("Your Level has granted you passage through this gate, {0}.",m.RawName);
						else
						m.SendMessage("Your Prestige has granted you passage through this gate, {0}.",m.RawName);
					
					
				
				
			}
			else
			{
				m.SendMessage( "To use this gate, you need to gain {0} more levels.",this.Lvl-((PlayerMobile)m).LvL );
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			
			writer.Write( (int)m_Lvl );

			
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Lvl = reader.ReadInt();
	
		}
	}
}