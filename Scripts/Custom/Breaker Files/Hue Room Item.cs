using Server.Targeting; 
using System; 
using Server; 
using Server.Gumps; 
using Server.Network; 
using Server.Menus; 
using Server.Menus.Questions; 
using Server.Mobiles; 
using System.Collections; 

namespace Server.Items 
{ 
   	public class HueRoomItem : Item 
   	{ 
		private DateTime _LastUse;
   
      		[Constructable] 
      		public HueRoomItem() : base( 7939 ) 
      		{ 
         		Weight = 0.0;  
         		Movable = false;
				Hue = 0;
         		Name="Hue Room Item"; 
          	} 
				[Constructable] 
				public HueRoomItem(int hue) : base( 7939 ) 
      		{ 
         		Weight = 0.0;  
         		Movable = false;
				Hue = hue;
         		Name = hue.ToString(); 
          	} 

      		public HueRoomItem( Serial serial ) : base( serial ) 
      		{ 
          
      
      		} 
      		public override void OnDoubleClick( Mobile from ) 
     	 	{ 

		
			
				from.SendMessage( "What do you wish to preview?" ); 
           			from.Target = new HueRoomItemTarget(this);
		    
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
			 
			private class HueRoomItemTarget: Target 
      		{ 
         		     private readonly HueRoomItem m_Item;
            public HueRoomItemTarget(HueRoomItem item)
                : base(20, true, TargetFlags.None)
            {
                m_Item = item;
            }
          
         		protected override void OnTarget( Mobile from, object target ) 
         		{ 
				if ( target is Item)
				{
					Item m_Item2 = (Item)target;
						
						  if (m_Item._LastUse + TimeSpan.FromSeconds(10) < DateTime.UtcNow)
            {
                m_Item.ItemID = m_Item2.ItemID;
				m_Item._LastUse = DateTime.UtcNow;
                Timer.DelayCall(TimeSpan.FromSeconds(10), () =>
                    {
                        m_Item.ItemID = 7939;

                        Timer.DelayCall(TimeSpan.FromSeconds(1), () =>
                            {
                               // ItemID = 7939;
                            });
                    });

                
            }
            else
            {
                from.SendLocalizedMessage(501789); // You must wait before trying again.
				m_Item.ItemID = 7939;
				m_Item._LastUse = (DateTime.UtcNow - TimeSpan.FromSeconds(-10));
            }
			}else 
				{
					from.SendMessage("Please Target an item to preview");
				}
			
          			
  
            	}
         	}

        	

		

		
      	 
   	} 
} 
