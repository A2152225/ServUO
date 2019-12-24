using System;
using System.Collections;
using Server;
using Server.Misc;
using Server.Mobiles;
using Server.Gumps;
using Server.ContextMenus;
using Server.Network;
using Server.Targeting;
using Server.Regions;
using Server.Accounting;

namespace Server.Items
{
	[Flipable( 0x29CE, 0x29CF )]
	public class Flying_Carpet : Item
	{
		private Item m_Shadow;
        private bool m_HasShadow;
		private bool m_Rollable;
		private bool m_Rolled;
		private int m_ManaCost;
		private bool m_MovableWhileUnrolled;
		private bool m_AllowHiddenFlyers;
		private bool m_AllowMountedFlyers;
		private int  m_MaxAltitude;
		private int  m_MinAltitude;
		
        [CommandProperty(AccessLevel.GameMaster)]
        public Item Shadow
        {
            get { return m_Shadow; }
            set { m_Shadow = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool HasShadow
        {
            get { return m_HasShadow; }
            set { m_HasShadow = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int MaxAltitude
        {
            get { return m_MaxAltitude; }
            set { m_MaxAltitude = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int MinAltitude
        {
            get { return m_MinAltitude; }
            set { m_MinAltitude = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Rollable
        {
            get { return m_Rollable; }
            set { m_Rollable = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Rolled
        {
            get { return m_Rolled; }
            set { m_Rolled = value; }
        }
		
        [CommandProperty(AccessLevel.GameMaster)]
        public int ManaCost
        {
            get { return m_ManaCost; }
            set { m_ManaCost = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool MovableWhileUnrolled
        {
            get { return m_MovableWhileUnrolled; }
            set { m_MovableWhileUnrolled = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool AllowHiddenFlyers
        {
            get { return m_AllowHiddenFlyers; }
            set { m_AllowHiddenFlyers = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool AllowMountedFlyers
        {
            get { return m_AllowMountedFlyers; }
            set { m_AllowMountedFlyers = value; }
        }

		[Constructable]
		public Flying_Carpet() : base( 0x29CE )
		{
			Name = "a carpet";
			Hue = 733;
			Weight = 5.0;
			m_Rolled=true;
			m_Rollable=true;
			m_ManaCost=0;
			m_MovableWhileUnrolled=false;
			m_MaxAltitude=120;
			m_MinAltitude=-86;
			m_AllowHiddenFlyers=true;
			m_AllowMountedFlyers=true;
			
			if(m_Rolled)
    			ItemID=0xA58;
			
			m_Shadow=null;
			m_HasShadow=true;
		}

		public Flying_Carpet( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( this, 1 ) )
			{
				from.SendMessage("Not from here.");
				return;
			}
			// standing on it, toggle fly gump
			else if(from.InRange(this,0) && !Rolled && (!from.Mounted || AllowMountedFlyers))
			{   
			    if ( ((PlayerMobile)from).m_Volotimer != null )
			    {
				    ((PlayerMobile)from).m_Volotimer.Stop();
				    ((PlayerMobile)from).m_Volotimer = null;
				    ((PlayerMobile)from).CloseGump( typeof( FlyingCarpetgump ) );
				    return; 
			    }
    	        from.SendGump( new FlyingCarpetgump( this, from , 0 ) );
			}
			else if(from.Mounted && !AllowMountedFlyers)
			    from.SendMessage("Not while mounted!");
			// standing near, toggle rolled
			else if(from.InRange(this,1))
			{
			    Rolled=!Rolled;
			    if(!Rolled) ItemID=0x29ce;
			    else ItemID=0xa58;
			    
			    if(Rolled) Movable=true;
			    else Movable=false;
			    
			    if(Rolled && from.HasGump( typeof( FlyingCarpetgump ) ))
			    {
				    if ( ((PlayerMobile)from).m_Volotimer != null )
				    {
					    ((PlayerMobile)from).m_Volotimer.Stop();
					    ((PlayerMobile)from).m_Volotimer = null;
					    return; 
				    }
				    ((PlayerMobile)from).CloseGump( typeof( FlyingCarpetgump ) );
			    }
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
			writer.Write(m_Rollable);
			writer.Write(m_Rolled);
			writer.Write(m_ManaCost);
			writer.Write(m_MovableWhileUnrolled);
			writer.Write(m_AllowHiddenFlyers);
			writer.Write(m_MaxAltitude);
			writer.Write(m_MinAltitude);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
			
			m_Rollable=reader.ReadBool();
			m_Rolled=reader.ReadBool();
			m_ManaCost=reader.ReadInt();
			m_MovableWhileUnrolled=reader.ReadBool();
			m_AllowHiddenFlyers=reader.ReadBool();
			m_MaxAltitude=reader.ReadInt();
			m_MinAltitude=reader.ReadInt();
			
			if(m_Rolled && m_Rollable)
			{
			    ItemID=0xa58;
			    Movable=true;
			}
			else
			{
			    ItemID=0x29ce;
			    if(m_MovableWhileUnrolled)
			        Movable=true;
			    else
			        Movable=false;
			}
		}

        public override TimeSpan DecayTime 
        { 
            get 
            { 
                return TimeSpan.FromMinutes( 30.0 ); 
            } 
        } 
	}
}