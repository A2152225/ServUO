using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{ 
   	public class EXPBall : Item 
   	{ 
                private int m_EXPBonus;

                [CommandProperty( AccessLevel.Seer )] 
                public int EXPBonus
                { 
                        get { return m_EXPBonus; } 
                        set { m_EXPBonus = value; InvalidateProperties(); } 
                } 
                
      		[Constructable] 
      		public EXPBall() : base( 0x1869 )
      		{ 
  			Name = "An EXP Ball";
  			//Movable = false;
			//Visible = false;
			m_EXPBonus = 500;
  			LootType = LootType.Blessed;
     		} 

		public EXPBall( Serial serial ) : base( serial ) 
      		{ 
      		} 

		public override void OnDoubleClick( Mobile from )
		{
			if ( from is PlayerMobile )
			{
				PlayerMobile pm = (PlayerMobile)from;
				pm.EXP += m_EXPBonus;
				Consume();
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			//list.Add( string.Format( "{0}", Name ));
			//base.GetProperties( list );

			//list.Add( string.Format( "{0}", Name ));
			list.Add( string.Format( "An Exp Ball: {0}", m_EXPBonus ));
		}

      		public override void Serialize( GenericWriter writer ) 
      		{ 
         		base.Serialize( writer ); 

         		writer.Write( (int) 0 ); // version 
                        	writer.Write( (int) m_EXPBonus ); 
      		} 

      		public override void Deserialize( GenericReader reader ) 
      		{ 
         		base.Deserialize( reader ); 

         		int version = reader.ReadInt(); 
			switch ( version )
			{
				case 0:
				{
					m_EXPBonus = (int)reader.ReadInt();

					break;
				}
			}
     		}
	}
}
