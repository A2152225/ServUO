using System;
using Server;
using System.Text;
using System.Threading;
using System.IO;
using System.Collections;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Accounting;

namespace Server.Items
{
	public class Dice1 : Item
	{
		[Constructable]
		public Dice1() : base( 0xFA7 )
		{
			Weight = 1.0;
		}

		public Dice1( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
	{
		PlayerMobile pm = from as PlayerMobile;
			if ( !from.InRange( this.GetWorldLocation(), 1 ) )
				return;
		int numa = Utility.Random(1,6);
		int numb = Utility.Random(1,6);
			this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format( "*{0} rolls {1}, {2}*", from.Name, numa, numb ) );

//	int rolled = numa + numb ;
int pntroll  =  numa + numb ;
			if (from.Virtues.Humility == 0 && pntroll != 7)//if ( ((PlayerMobile)from).PointRoll == 0 )
		{

		//((PlayerMobile)from).PointRoll = numa + numb ;
		from.Virtues.Humility = pntroll;
		//((PlayerMobile)from).CurrentDice = 1;		
		this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format( "*{0} rolls {1} as their Point Roll*", from.Name, pntroll ));//((PlayerMobile)from).PointRoll  ) );

		}
	//	else if ( ((PlayerMobile)from).PointRoll != 7 )
	//	{
	//	
	//	((PlayerMobile)from).CurrentRoll = numa + numb;
		
		
	//	} 	
		else if (from.Virtues.Humility == 0 && pntroll == 7)   //( ((PlayerMobile)from).PointRoll == 7 )
		{
		from.SendMessage("You Roll a 7 for the set point and advance automatically!");
		//((PlayerMobile)from).CurrentDice = 2;
		//((PlayerMobile)from).PointRoll = 0;
		//((PlayerMobile)from).CurrentRoll = 0;
		from.Virtues.Humility = 0;
		from.X = from.X + 3;
	
		} 	
		
		else if (from.Virtues.Humility == pntroll)//( ((PlayerMobile)from).CurrentRoll == ((PlayerMobile)from).PointRoll )
		{	
		from.SendMessage( "You roll {0} advancing you in your Lucky 7 game!", pntroll);//((PlayerMobile)from).CurrentRoll  );
		
		//((PlayerMobile)from).CurrentDice = 2;
		//((PlayerMobile)from).PointRoll = 0;
		//((PlayerMobile)from).CurrentRoll = 0;
		from.Virtues.Humility = 0;
		from.X = from.X + 3;
	
		}
		else if (pntroll == 7)//( ((PlayerMobile)from).CurrentRoll == 7 && ((PlayerMobile)from).PointRoll != 7 )
		{
		from.SendMessage( "You roll 7 ending your Lucky 7 game." );
		//((PlayerMobile)from).CurrentDice = 0;
		//((PlayerMobile)from).PointRoll = 0;
		//((PlayerMobile)from).CurrentRoll = 0;
		from.Y = from.Y + 12;
		from.Virtues.Humility = 0;
		}
		
	}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}