using System;
using System.Text;
using System.Collections;

using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;

namespace Server.Custom.Misc
{
    public class JustMissed
    {
        public JustMissed() { }

        private static bool m_Enabled = true;
        private static Hashtable m_LogoutTable;
		//private AccessLevel HighestShown = AccessLevel.GameMaster;
		private static AccessLevel m_HighestShown = AccessLevel.Seer;
        [CommandProperty( AccessLevel.Developer )]
        public static bool Enabled { get { return m_Enabled; } set { m_Enabled = value; } }
		 [CommandProperty( AccessLevel.Developer )]
		public static AccessLevel HighestShown { get { return m_HighestShown; } set { m_HighestShown = value; } }
		
        public static JustMissed Instance { get { return new JustMissed(); } }

        public static void Initialize()
        {         
            EventSink.Login += new LoginEventHandler( EventSink_Login );
            EventSink.Logout += new LogoutEventHandler( EventSink_Logout );
            CommandSystem.Register( "JMSettings", AccessLevel.GameMaster, new CommandEventHandler( JMSettings_OnCommand ) );

            m_LogoutTable = new Hashtable();            
        }

        private static void JMSettings_OnCommand( CommandEventArgs e )
        {
            Mobile m = e.Mobile;

            if( m == null )
                return;

            m.SendGump( new PropertiesGump( m, Instance ) );
        }

        private static void EventSink_Logout( LogoutEventArgs e )
        {
            if( !Enabled )
                return;

            Mobile m = e.Mobile;

            if( m == null ) 
                return;        
			
			if(m.AccessLevel > HighestShown)
			return;

            if( m_LogoutTable.ContainsKey( m.Serial ) )
                m_LogoutTable.Remove( m.Serial );

            m_LogoutTable.Add( m.Serial, new JMLogTimer( m ) );
        }

        private static void EventSink_Login( LoginEventArgs e )
        {
            if( !Enabled )
                return;

            Mobile m = e.Mobile;

            if( m == null )
                return;

            if( m_LogoutTable.ContainsKey( m.Serial ) )
                m_LogoutTable.Remove( m.Serial );

            foreach( JMLogTimer t in m_LogoutTable.Values )
            {
                if( t.Mobile == null )
                    continue;

                int minutes = t.Ticks / 60;
                m.SendMessage( 54,"You just missed {0}, they logged out {1} minute{2} ago.",
                    t.Mobile.Name,
                    ( minutes > 0 ) ? minutes.ToString() : "less than 1",
                    ( minutes > 0 ) ? "s" : "" );
            }
        }

        public static void RemoveTimer( JMLogTimer timer )
        {
            if( m_LogoutTable.ContainsKey( timer.Mobile.Serial ) )
                m_LogoutTable.Remove( timer.Mobile.Serial );
        }
		
		
		
		
		
		
		
		
    }

	
    public class JMLogTimer : Timer
    {
        private Mobile m_LoggedMobile;
        private int m_Ticks;

        public Mobile Mobile { get { return m_LoggedMobile; } }
        public int Ticks { get { return m_Ticks; } }

        public JMLogTimer( Mobile m )
            : base( TimeSpan.FromSeconds( 0.0 ), TimeSpan.FromSeconds( 1.0 ) )
        {
            m_LoggedMobile = m;
            Start();
        }

        protected override void OnTick()
        {
            if( m_Ticks > 1200 )
            {
                JustMissed.RemoveTimer( this );
                Stop();
            }

            m_Ticks++;    
        }
    }
	
	
	
	
	
	
}
