using System;
using System.Collections;
using System.Collections.Generic;
using Server.Network;

namespace Server.Items
{
	public class BaseBagofHolding : Container
	{
		public override int DefaultGumpID{ get{ return 0x3D; } }
		public override int DefaultDropSound{ get{ return 0x48; } }
		public override int DefaultMaxWeight{ get{ return 0; } } // A value of 0 signals unlimited weight lets you put bigger stacks in bag
//breaker add ^

		public override Rectangle2D Bounds
		{
			get{ return new Rectangle2D( 29, 34, 108, 94 ); }
		}
		public BaseBagofHolding() : base( 0xE76 )
		{
			Weight = 1.0;
			LootType = LootType.Cursed;
			Hue = Utility.RandomMetalHue();
		}
		
		public BaseBagofHolding( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}

		public override void UpdateTotal( Item sender, TotalType type, int delta )
		{
			base.UpdateTotal( sender,type,delta );
			if( type == TotalType.Weight )
			{
				if ( Parent is Item )
					( Parent as Item ).UpdateTotal( sender, type, delta*-1 );
				else if ( Parent is Mobile )
					( Parent as Mobile ).UpdateTotal( sender, type, delta*-1 );
			}
		}
		public override int GetTotal( TotalType type )
		{
			if( type == TotalType.Weight )
				return 0;
			else
				return base.GetTotal(type);
		}
	}
	
	public class SmallBagofHolding : BaseBagofHolding
	{
		[Constructable]
		public SmallBagofHolding()
		{
			Name = "small bag of holding";
			MaxItems = 5;
		}
		
		public SmallBagofHolding( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			MaxItems = 5;
			
			int version = reader.ReadInt();
		}
	}
	
	public class MediumBagofHolding : BaseBagofHolding
	{
		[Constructable]
		public MediumBagofHolding()
		{
			Name = "medium bag of holding";
			MaxItems = 10;
		}
		
		public MediumBagofHolding( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			MaxItems = 10;
			
			int version = reader.ReadInt();
		}
	}
	
	public class LargeBagofHolding : BaseBagofHolding
	{
		[Constructable]
		public LargeBagofHolding()
		{
			Name = "large bag of holding";
			MaxItems = 20;
		}
		
		public LargeBagofHolding( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			MaxItems = 20;
			
			int version = reader.ReadInt();
		}
	}
}
