using System;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{
public class SingingCrystalBall : Item
{
private bool m_Squelch;

[CommandProperty(AccessLevel.Seer)]
        public bool SoundSquelch
        {
            get { return m_Squelch; }
            set { m_Squelch = value; }
			        
        }

[Constructable]
public SingingCrystalBall() : base( 0xE2E )
{
Weight = 2.0;
Name = "a Singing Crystal Ball";
SoundSquelch = false;
}

public SingingCrystalBall( Serial serial ) : base( serial )
{
}

public override bool HandlesOnMovement{ get{ return true; } }

public override void OnMovement( Mobile m, Point3D oldLocation )
{
if ( Utility.InRange( Location, m.Location, 4 ) )
{
//m.PlaySound ( Utility.Random( 1237 ) );
Effects.PlaySound( m.Location, m.Map, Utility.Random( 1647 ));
}
}


 public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( !m_Squelch )
				list.Add( 1060742 ); // active
			else
				list.Add( 1060743 ); // inactive
		}
		

public override void OnDoubleClick( Mobile from )
{

if ( from.InRange( GetWorldLocation(), 4 ) )
{

SoundSquelch = !SoundSquelch;

InvalidateProperties();
}
else
from.SendLocalizedMessage( 500446 ); // That is too far away.
}

public override void Serialize( GenericWriter writer )
{
base.Serialize( writer );
writer.Write( (int) 0 ); // version

writer.Write( (bool)m_Squelch );
}

public override void Deserialize( GenericReader reader )
{
base.Deserialize( reader );
int version = reader.ReadInt();

m_Squelch = reader.ReadBool();
}

}
}