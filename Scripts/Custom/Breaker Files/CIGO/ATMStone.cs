using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server.Mobiles;
//designed by Burner(aka BObby Schaffer)
namespace Server.Items

{
	public class ATMStone : Item
	{
	int m_Interval;
	[CommandProperty( AccessLevel.Seer )]
	public int Interval{get{return m_Interval;}set{m_Interval = value;}}
	
		[Constructable]
		public ATMStone() : base( 0xED4 )
		{
			Movable = false;
			Hue = 0x2D1;
			Name = "an ATM Stone";
			Interval = 5000;
		}

		public override void OnDoubleClick( Mobile from )
		{
		
			
				
			Container bank = from.BankBox;
		
BankCheck ATMCheck = new BankCheck( Interval );
				if ( bank != null && Banker.Withdraw( from, Interval  ))
{
				from.AddToBackpack( ATMCheck );
from.SendMessage( "A check for {0} gold has been withdrawn from your bank box and placed in your backpack", Interval );
}
else
{
from.SendMessage( "You do not have enough Gold in your bank box, please add more gold before trying again!" );
}			
						}

		public ATMStone( Serial serial ) : base( serial )
		{

		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
			
				writer.Write( (int)m_Interval );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_Interval = reader.ReadInt(); 
		}
	}
}