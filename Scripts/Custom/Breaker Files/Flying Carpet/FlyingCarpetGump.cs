using System;
using Server;
using Server.Items;
using Server.Multis;
using Server.Network;
using Server.Mobiles;
using Server.ContextMenus;
using System.Collections;
using Server.Misc;


namespace Server.Gumps
{
	public class FlyingCarpetgump : Gump
	{
		private Mobile m_Owner;
		public Mobile Owner
		{ 
			get{ return m_Owner; } 
			set{ m_Owner = value; } 
		}

		private Item i_item;
	
		public Item i
		{ 
			get{ return i_item; } 
			set{ i_item = value; } 
		}


		public FlyingCarpetgump(Item i, Mobile owner, int page) : base( 20, 20 )
		{
			owner.CloseGump( typeof( FlyingCarpetgump ) );

			int gumpX = 0; int gumpY = 0;

			m_Owner = owner;

			i_item = i;
			((PlayerMobile)owner).m_CarpetItem=i;

			Closable = true;
			Disposable = false;
			Dragable = true;
			Resizable = false;

			AddPage( 0 );

			AddPage( 1 ); 

			gumpX = 10; gumpY = 10;
			AddImage( gumpX, gumpY, 0x1392 );
                       
            if ( page != 1 ) 
		    {
			    gumpX = 118; gumpY = 53;
			    AddButton( gumpX, gumpY, 0x1195, 0x1195, 1, GumpButtonType.Reply, 0);
            }
            
            if ( page != 2 ) 
		    {
			    gumpX = 114; gumpY = 117;
			    AddButton( gumpX, gumpY, 0x1197, 0x1197, 2, GumpButtonType.Reply, 0);
            }
            
            if ( page != 3 ) 
		    {
			    gumpX = 54; gumpY = 119;
			    AddButton( gumpX, gumpY, 0x1199, 0x1199, 3, GumpButtonType.Reply, 0);
            }
            
            if ( page != 4 ) 
		    {
			    gumpX = 54; gumpY = 52;
			    AddButton( gumpX, gumpY, 0x119B, 0x119B, 4, GumpButtonType.Reply, 0);
            }
            
            if ( page != 5 ) 
		    {
			    gumpX = 101; gumpY = 89;
			    AddButton( gumpX, gumpY, 0x26AC, 0x26AE, 5, GumpButtonType.Reply, 0);
            }
                        
            if ( page != 6 )
		    {
			    gumpX = 102; gumpY = 112;
			    AddButton( gumpX, gumpY, 0x26B2, 0x26B4, 6, GumpButtonType.Reply, 0);
            }

            if ( page == 1 )              
		    {
			    gumpX = 118; gumpY = 53;
			    AddButton( gumpX, gumpY, 0x1195, 0x1195, 7, GumpButtonType.Reply, 0);

			    gumpX = 118; gumpY = 53;
			    AddImage( gumpX, gumpY, 0x1195, 69 );
            }
            else if ( page == 2 )              
		    {
    			gumpX = 114; gumpY = 117;
    			AddButton( gumpX, gumpY, 0x1197, 0x1197, 8, GumpButtonType.Reply, 0);
    
    			gumpX = 114; gumpY = 117;
    			AddImage( gumpX, gumpY, 0x1197, 69 );
            }
            else if ( page == 3 )             
            {
	    		gumpX = 54; gumpY = 119;
		    	AddButton( gumpX, gumpY, 0x1199, 0x1199, 9, GumpButtonType.Reply, 0);

			    gumpX = 54; gumpY = 119;
			    AddImage( gumpX, gumpY, 0x1199, 69 );
            }
            else if ( page == 4 )             
		    {
			    gumpX = 54; gumpY = 52;
			    AddButton( gumpX, gumpY, 0x119B, 0x119B, 10, GumpButtonType.Reply, 0);

			    gumpX = 54; gumpY = 52;
			    AddImage( gumpX, gumpY, 0x119B, 69 );
            }
            else if ( page == 5 )            
		    {
			    gumpX = 101; gumpY = 89;
			    AddButton( gumpX, gumpY, 0x26AC, 0x26AE, 11, GumpButtonType.Reply, 0);

			    gumpX = 101; gumpY = 89;
    			AddImage( gumpX, gumpY, 0x26AC, 69 );
            }
            else if ( page == 6 )           
	        {
		    	gumpX = 102; gumpY = 112;
			    AddButton( gumpX, gumpY, 0x26B2, 0x26B4, 12, GumpButtonType.Reply, 0);

			    gumpX = 102; gumpY = 112;
			    AddImage( gumpX, gumpY, 0x26B2, 69 );
            }
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

            PlayerMobile pm = (PlayerMobile)from;

			if ( i_item == null)
			{
				pm.CloseGump( typeof( FlyingCarpetgump ) );
				pm.m_Volotimer.Stop();
				return;
			}

			LandTile landTile = pm.Map.Tiles.GetLandTile(pm.X, pm.Y );

          	bool landcheck = ( landTile.Z <= ( pm.Z -1 )  );

          	if (landcheck )
              	animatevolo(from,i_item);

			switch( info.ButtonID )
			{
              	case 0:
    			break;

				case 1:
                    from.Direction = Direction.North;
				    i_item.ItemID = 0x29CF;
                    from.SendGump(new FlyingCarpetgump( i_item, from , 1) ); 
 			       	if ( pm.m_Volotimer != null )
                        pm.m_Volotimer.Stop();
                   	pm.m_Volotimer = new Volotimer( i_item, pm, 1 );  
                   	pm.m_Volotimer.Start();     
				break;

				case 2:
                    from.Direction = Direction.East;
    				i_item.ItemID = 0x29CE;
                    from.SendGump(new FlyingCarpetgump( i_item, from , 2) ); 
 			       	if ( pm.m_Volotimer != null )
                       	pm.m_Volotimer.Stop();
                   	pm.m_Volotimer = new Volotimer( i_item, pm, 2 );  
                   	pm.m_Volotimer.Start();     
				break;

				case 3:
                    from.Direction = Direction.South;
        			i_item.ItemID = 0x29CF;
                    from.SendGump(new FlyingCarpetgump( i_item, from , 3) ); 
 			       	if ( pm.m_Volotimer != null )
                      	pm.m_Volotimer.Stop();
                   	pm.m_Volotimer = new Volotimer( i_item, pm, 3 );  
                   	pm.m_Volotimer.Start();     
				break;

				case 4:
                    from.Direction = Direction.West;
 				    i_item.ItemID = 0x29CE;
                    from.SendGump(new FlyingCarpetgump( i_item, from , 4) ); 
 			       	if ( pm.m_Volotimer != null )
                       	pm.m_Volotimer.Stop();
                   	pm.m_Volotimer = new Volotimer( i_item, pm, 4 );  
                   	pm.m_Volotimer.Start();     
				break;

				case 5:
                    from.SendGump(new FlyingCarpetgump( i_item, from , 5) ); 
 			       	if ( pm.m_Volotimer != null )
                      	pm.m_Volotimer.Stop();
                   	pm.m_Volotimer = new Volotimer( i_item, pm, 5 );  
                   	pm.m_Volotimer.Start();     
				break;

				case 6:
                    from.SendGump(new FlyingCarpetgump( i_item, from , 6) );                          
 			       	if ( pm.m_Volotimer != null )
                      	pm.m_Volotimer.Stop();
                   	pm.m_Volotimer = new Volotimer( i_item, pm, 6 );  
                   	pm.m_Volotimer.Start();     
				break;

				case 7:
			        if ( pm.m_Volotimer != null )
                        pm.m_Volotimer.Stop();
                    if ( landcheck )
			        {
                    	pm.m_Volotimer = new Volotimer( i_item, pm, 0 );
                    	pm.m_Volotimer.Start();
                    } 
                    else
                        pm.m_Volotimer = null;
                    from.SendGump(new FlyingCarpetgump( i_item, from , 0) );  
				break;

				case 8:
			        if ( pm.m_Volotimer != null )
                        pm.m_Volotimer.Stop();
                    if ( landcheck )
			        {
                    	pm.m_Volotimer = new Volotimer( i_item, pm, 0 );
                    	pm.m_Volotimer.Start();
                    } 
                    else
                        pm.m_Volotimer = null;
                    from.SendGump(new FlyingCarpetgump( i_item, from , 0) );  
				break;

				case 9:
			        if ( pm.m_Volotimer != null )
                        pm.m_Volotimer.Stop();
                    if ( landcheck )
			        {
                    	pm.m_Volotimer = new Volotimer( i_item, pm, 0 );
                    	pm.m_Volotimer.Start();
                    } 
                    else
                        pm.m_Volotimer = null;
                    from.SendGump(new FlyingCarpetgump( i_item, from , 0) );  
				break;

				case 10:
			        if ( pm.m_Volotimer != null )
                        pm.m_Volotimer.Stop();
                    if ( landcheck )
			        {
                    	pm.m_Volotimer = new Volotimer( i_item, pm, 0 );
                    	pm.m_Volotimer.Start();
                    } 
                    else
                        pm.m_Volotimer = null;
                    from.SendGump(new FlyingCarpetgump( i_item, from , 0) );  
				break;

				case 11:
			        if ( pm.m_Volotimer != null )
                        pm.m_Volotimer.Stop();
                    if ( landcheck )
			        {
                    	pm.m_Volotimer = new Volotimer( i_item, pm, 0 );
                    	pm.m_Volotimer.Start();
                    } 
                    else
                        pm.m_Volotimer = null;
                    from.SendGump(new FlyingCarpetgump( i_item, from , 0) );  
				break;

				case 12:
			        if ( pm.m_Volotimer != null )
                        pm.m_Volotimer.Stop();
                    if ( landcheck )
			        {
                    	pm.m_Volotimer = new Volotimer( i_item, pm, 0 );
                    	pm.m_Volotimer.Start();
                    } 
                    else
                        pm.m_Volotimer = null;
                    from.SendGump(new FlyingCarpetgump( i_item, from , 0) );  
				break;
	        }
		}

		public static void animatevolo( Mobile m, Item i_item )
		{
            if(!(i_item is Flying_Carpet)) return;
            
            Flying_Carpet fc=(Flying_Carpet)i_item;
            
            // this is called at a regular interval while mobile is flying - do stuff to him/her here!
            if(!fc.AllowHiddenFlyers && m.Hidden)
            {
                m.Hidden=false;
      		    m.SendMessage(46,"You have been revealed!");

            }
            
            m.Mana-=fc.ManaCost;
            
            //m.PlaySound( 0x109 );
			//m.FixedParticles(0x376A, 1, 31, 9961, 1160, 0, EffectLayer.Waist );
           	//m.FixedParticles( 0x37C4, 1, 31, 9502, 43, 2, EffectLayer.Waist );
		}

		public static bool checkmap( Mobile m,Item item )
		{
			/*if(item==null || !(item is FlyingCarpet)) return true;
			
			if ( m.Z <= ((FlyingCarpet)item).MaxAltitude )
			    return true;

            if ( m.Z >= ((FlyingCarpet)item).MaxAltitude )
			    return true;*/

			if ( m.Z <= -86 )
			    return true;

            if ( m.Z >= 120 )
			    return true;

			return false;
		}

		private class Volotimer : Timer 
        { 
            private PlayerMobile m_Mobile; 
			private Item i_item;
            private int d;
			private int anim = 0;
			private Item m_Shadow;

           	public Volotimer( Item i, PlayerMobile m, int seq ) : base( TimeSpan.FromSeconds( 0.2 ) )
           	{
				i_item = i;
           		m_Mobile = m;
           		d = seq;
           	} 

           	protected override void OnTick() 
        	{
        		bool flycheck = false;               
        		bool flycheck2 = false;
			
           		LandTile under = m_Mobile.Map.Tiles.GetLandTile( m_Mobile.X, m_Mobile.Y );
                StaticTile[] statsUnder = m_Mobile.Map.Tiles.GetStaticTiles((m_Mobile.X), (m_Mobile.Y), true);

           		int underZ=under.Z;
           		
           		if(m_Mobile.Mana<=0 && ((Flying_Carpet)i_item).ManaCost>0)
           		{
           		    // hit the down button, we're outta gas
           		    d=6;
           		}
           		
                for ( int i = 0; i < statsUnder.Length; ++i )
                {
                    StaticTile tile = statsUnder[i];
                    if ( tile.Z >= underZ)
                        underZ=tile.Z;
                }
           		
           		if(underZ>=m_Mobile.Z-2)
           		{
           		    m_Mobile.CantWalk=false;
           		    if(((Flying_Carpet)i_item).Shadow!=null)
           		        ((Flying_Carpet)i_item).Shadow.Delete();
           		}
           		else
           		    m_Mobile.CantWalk=true;
           		
           		if(!m_Mobile.InRange(i_item,0))
           		{
				    if ( ((PlayerMobile)m_Mobile).m_Volotimer != null )
				    {
					    ((PlayerMobile)m_Mobile).m_Volotimer.Stop();
					    ((PlayerMobile)m_Mobile).m_Volotimer = null;
                    }
    			    ((PlayerMobile)m_Mobile).CloseGump( typeof( FlyingCarpetgump ) );
         			Stop();
         			return;
           		}
		        
		        // shadow
		        if(underZ<i_item.Z-1)
		        {
		            Flying_Carpet fc=(Flying_Carpet)i_item;
		            
		            if(fc.Shadow!=null)
		                fc.Shadow.Delete();
		                
		            if(fc.HasShadow)
		            {
		                fc.Shadow=new Item(fc.ItemID);
		                fc.Shadow.Hue=16385;  //try 2000 if that does not work - ethy mount transparent hue 16385
		                fc.Shadow.Movable=false;
		            
		                fc.Shadow.MoveToWorld(new Point3D(fc.Location.X,fc.Location.Y,underZ-1),fc.Map);
		            }
		        }
		        
		        if ( !m_Mobile.Alive || m_Mobile.NetState == null || i_item == null )
        		{
        			Stop();
        			
        			if(m_Mobile!=null) m_Mobile.CantWalk=false;
        		}
         		else if (d == 0)
         		{
                    LandTile landTile = m_Mobile.Map.Tiles.GetLandTile(m_Mobile.X, m_Mobile.Y);

            		if ( landTile.Z >= ( m_Mobile.Z -1 )  )
                       	flycheck = true;

             		if ( flycheck )
             		{
             			m_Mobile.CloseGump( typeof( FlyingCarpetgump ) );
             			m_Mobile.SendGump(new FlyingCarpetgump( i_item, m_Mobile , 0) ); 
             			Stop();
             		}
               		else
             		{
                		if ( anim <= 0)
				        {
					        animatevolo(m_Mobile,i_item);
					        anim = 2;
				        }
				        else
				        {
					        anim -= 1;
				        }
		                Start();
 			        }
				}           
                else if ( d == 1) 
                {
                    StaticTile[] tiles = m_Mobile.Map.Tiles.GetStaticTiles((m_Mobile.X), (m_Mobile.Y - 1), true);
                    LandTile landTile = m_Mobile.Map.Tiles.GetLandTile(m_Mobile.X, m_Mobile.Y);

                    for ( int i = 0; i < tiles.Length; ++i )
                    {
                        StaticTile tile = tiles[i];
                        if ( tile.Z >= ( m_Mobile.Z -1 ))
                            flycheck = true;
                    }
                    
                    if ( landTile.Z >= ( m_Mobile.Z -1 )  )
                    {
                        flycheck = true;
                        flycheck2 = true;
                    }
                    
                    if ( flycheck )
                    {
                        m_Mobile.CloseGump( typeof( FlyingCarpetgump ) );
                        m_Mobile.SendGump(new FlyingCarpetgump( i_item, m_Mobile , 0) ); 
                        
                        if (flycheck2)
                            Stop();
                        else
                        {
                        	d = 0;
                        	Start();
                        }
                    }
                    else
                    {
                        m_Mobile.Direction|=Direction.Running;
                        m_Mobile.Y -= 1;
                        i_item.Y -= 1;
                        Start(); 

                        if ( anim <= 0)
                        {
                            animatevolo(m_Mobile,i_item);
                            anim = 2;
                        }
                        else
                        {
                            anim -= 1;
                        }
                    }
                }
                else if ( d == 2)
                {
                    StaticTile[] tiles = m_Mobile.Map.Tiles.GetStaticTiles((m_Mobile.X + 1), (m_Mobile.Y), true);
                    LandTile landTile = m_Mobile.Map.Tiles.GetLandTile(m_Mobile.X, m_Mobile.Y);
                    
                    for ( int i = 0; i < tiles.Length; ++i )
                    {
                        StaticTile tile = tiles[i];
                        if ( tile.Z >= ( m_Mobile.Z -1 ))
                            flycheck = true;
                    }
                    
                    if ( landTile.Z >= ( m_Mobile.Z -1 )  )
                    {
                        flycheck = true;
                        flycheck2 = true;
                    }

                    if ( flycheck )
                    {
                        m_Mobile.CloseGump( typeof( FlyingCarpetgump ) );
                        m_Mobile.SendGump(new FlyingCarpetgump( i_item, m_Mobile , 0) ); 
                        
                        if (flycheck2)
                            Stop();
                        else
                        {
                        	d = 0;
                        	Start();
                        }
                    }
                    else
                    {
                        if ( anim <= 0)
                        {
                            animatevolo(m_Mobile,i_item);
                            anim = 2;
                        }
                        else
                        {
                            anim -= 1;
                        }

                        m_Mobile.Direction |= Direction.Running;
                        m_Mobile.X += 1;
                        i_item.X += 1;
                        Start(); 
                    }
                }
                else if ( d == 3)
                {
                    StaticTile[] tiles = m_Mobile.Map.Tiles.GetStaticTiles((m_Mobile.X), (m_Mobile.Y + 1), true);
                    LandTile landTile = m_Mobile.Map.Tiles.GetLandTile(m_Mobile.X, m_Mobile.Y);
                    
                    for ( int i = 0; i < tiles.Length; ++i )
                    {
                        StaticTile tile = tiles[i];
                        if ( tile.Z >= ( m_Mobile.Z -1 ))
                            flycheck = true;
                    }
                    
                    if ( landTile.Z >= ( m_Mobile.Z -1 )  )
                    {
                        flycheck = true;
                        flycheck2 = true;
                    }
                
                    if ( flycheck )
                    {
                        m_Mobile.CloseGump( typeof( FlyingCarpetgump ) );
                        m_Mobile.SendGump(new FlyingCarpetgump( i_item, m_Mobile , 0) ); 

                        if (flycheck2)
                            Stop();
                        else
                        {
                        	d = 0;
                        	Start();
                        }
                    }
                    else
                    {
                        if ( anim <= 0)
                        {
                            animatevolo(m_Mobile,i_item);
                            anim = 2;
                        }
                        else
                        {
                            anim -= 1;
                        }

                        m_Mobile.Direction |= Direction.Running;
                        m_Mobile.Y += 1;
                        i_item.Y += 1;
                        Start(); 
                    }
                }
                else if ( d == 4)
                {
                    StaticTile[] tiles = m_Mobile.Map.Tiles.GetStaticTiles((m_Mobile.X - 1), (m_Mobile.Y), true);
                    LandTile landTile = m_Mobile.Map.Tiles.GetLandTile(m_Mobile.X, m_Mobile.Y);

                    for ( int i = 0; i < tiles.Length; ++i )
                    {
                        StaticTile tile = tiles[i];
                        if ( tile.Z >= ( m_Mobile.Z -1 ))
                            flycheck = true;
                    }

                    if ( landTile.Z >= ( m_Mobile.Z -1 )  )
                    {
                        flycheck = true;
                        flycheck2 = true;
                    }

                    if ( flycheck )
                    {
                        m_Mobile.CloseGump( typeof( FlyingCarpetgump ) );
                        m_Mobile.SendGump(new FlyingCarpetgump( i_item, m_Mobile , 0) ); 
                        
                        if (flycheck2)
                            Stop();
                        else
                        {
                        	d = 0;
                        	Start();
                        }
                    }
                    else
                    {
                        if ( anim <= 0)
                        {
                            animatevolo(m_Mobile,i_item);
                            anim = 2;
                        }
                        else
                        {
                            anim -= 1;
                        }

                        m_Mobile.Direction |= Direction.Running;
                        m_Mobile.X -= 1;
                        i_item.X -= 1;
                        Start(); 
                    }
                }
                else if ( d == 5) 
                {
                    StaticTile[] tiles = m_Mobile.Map.Tiles.GetStaticTiles((m_Mobile.X), (m_Mobile.Y), true);
                    LandTile landTile = m_Mobile.Map.Tiles.GetLandTile(m_Mobile.X, m_Mobile.Y);

                    for ( int i = 0; i < tiles.Length; ++i )
                    {
                        StaticTile tile = tiles[i];
                        if ( tile.Z >= ( m_Mobile.Z +1 ) )
                            flycheck = true;
                    }

                    if ( landTile.Z >= ( m_Mobile.Z +1 )  )
                        flycheck = true;

                    if ( flycheck || checkmap( m_Mobile,i_item ) )
                    {
                        m_Mobile.CloseGump( typeof( FlyingCarpetgump ) );
                        m_Mobile.SendGump(new FlyingCarpetgump( i_item, m_Mobile , 0) ); 
                        Start();
                    }
                    else
                    {
                        if ( anim <= 0)
                        {
                            animatevolo(m_Mobile,i_item);
                            anim = 2;
                        }
                        else
                        {
                            anim -= 1;
                        }	
                        
                        m_Mobile.Direction |= Direction.Running;
                        m_Mobile.Z += 1;
                        i_item.Z += 1;
                        Start(); 
                    }
                }
                else if ( d == 6) 
                {
                    StaticTile[] tiles = m_Mobile.Map.Tiles.GetStaticTiles((m_Mobile.X), (m_Mobile.Y), true);
                    LandTile landTile = m_Mobile.Map.Tiles.GetLandTile(m_Mobile.X, m_Mobile.Y);

                    for ( int i = 0; i < tiles.Length; ++i )
                    {
                        StaticTile tile = tiles[i];
                        if ( tile.Z >= ( m_Mobile.Z -1 ))
                            flycheck = true;
                    }

                    if ( landTile.Z >= ( m_Mobile.Z -1 )  )
                        flycheck = true;

                    if ( flycheck )
                    {
                        m_Mobile.CloseGump( typeof( FlyingCarpetgump ) );
                        m_Mobile.SendGump(new FlyingCarpetgump( i_item, m_Mobile , 0) );
                        Stop();
                    }
                    else
                    {
                        if ( anim <= 0)
                        {
                            animatevolo(m_Mobile,i_item);
                            anim = 2;
                        }
                        else
                        {
                            anim -= 1;
                        }

                        m_Mobile.Direction |= Direction.Running;
                        m_Mobile.Z -= 1;
                        i_item.Z -= 1;
                        Start(); 
                    }
                }
                else
                {
                    Stop();
                    if(m_Mobile!=null) m_Mobile.CantWalk=false;
                } 
            } 
        } 
    }
}
