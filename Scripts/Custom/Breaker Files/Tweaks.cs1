using System;
using System.Collections;
using System.IO;
using Server;
using Server.Commands;
using Server.Scripts.Commands;
using System.Runtime.InteropServices;
using Server.Spells;
using Server.Mobiles;
using Server.Items;
using Server.Misc;


namespace Server.Misc
{

public class Tweaks
{

private static int m_LMCP = 75;
private static int m_VHeal = 100;
private static int m_PvPSDI = 200;
private static int m_XPMod = 2;
private static int m_CXPMod = 1;

private static int m_TramMapXPMod = 4;
private static int m_FelMapXPMod = 4;
private static int m_IlshMapXPMod = 4;
private static int m_MalMapXPMod = 4;
private static int m_TokMapXPMod = 4;
private static int m_TerMapXPMod = 0;

		public static void Initialize()
		{
		CommandSystem.Register( "VampAoeHeal", AccessLevel.Developer, new CommandEventHandler( VampAoeHeal_OnCommand ) );
			CommandSystem.Register( "LMCP", AccessLevel.Developer, new CommandEventHandler( LMCP_OnCommand ) );
			CommandSystem.Register( "PVPSDI", AccessLevel.Developer, new CommandEventHandler( PvPSDI_OnCommand ) );
			CommandSystem.Register( "XPMod", AccessLevel.Developer, new CommandEventHandler( XPMod_OnCommand ) );
			CommandSystem.Register( "CXPMod", AccessLevel.Developer, new CommandEventHandler( CXPMod_OnCommand ) );
			CommandSystem.Register( "MapXPMod", AccessLevel.Developer, new CommandEventHandler( MapXPMod_OnCommand ) );
		}
		
		public static int GetMapXPMod(Map map)
		{
			
		
		if ( map ==  Map.Trammel)
			{
			return m_TramMapXPMod;
						 
			}
		if ( map ==  Map.Felucca)
			{
			return m_FelMapXPMod;
			 
			}
		if ( map == Map.Ilshenar)
			{
			return m_IlshMapXPMod;
			 
			}
		if ( map ==  Map.Malas)
			{
			return m_MalMapXPMod;
			 
			}
		if ( map == Map.Tokuno)
			{
			return m_TokMapXPMod;
			 
			}
		if ( map == Map.TerMur)
			{
			return m_TerMapXPMod;
			 
			}
		return m_TramMapXPMod;
		}
		
		public static void  SetMapXPMod(Map map, int val)
	{
		
			
		if ( map == Map.Trammel )
			{
			m_TramMapXPMod = val;
			
			}
		if ( map == Map.Felucca)
			{
			 m_FelMapXPMod = val;
			}
		if ( map == Map.Ilshenar)
			{
			 m_IlshMapXPMod = val;
			}
		if ( map ==  Map.Malas)
			{
			 m_MalMapXPMod = val;
			}
		if ( map ==  Map.Tokuno)
			{
			 m_TokMapXPMod = val;
			}
		if ( map ==  Map.TerMur)
			{
			 m_TerMapXPMod = val;
			}
		
	
		}
		
		
		
		
		
	
		
		
		
		public static int LMCP
		{
			get{ return m_LMCP; }
			set{ m_LMCP = value; }
		}

		[Usage( "LMCP (int)" )]
		[Description( "Changes the LMC Cap." )]
		public static void LMCP_OnCommand( CommandEventArgs e )
		{
			if ( e.Length == 1 )
			{
				double testit = e.GetInt32(0);
				
				
				if ( testit < 0 || testit > 100)
				{
				e.Mobile.SendMessage( "Please Choose a number between 0 and 100." );
				
				}
				else
				{
				try {
				m_LMCP = ((int)testit);
				if (m_LMCP == 0){
				e.Mobile.SendMessage( "Lower Mana Cost Cap has been removed");
				}else{
				e.Mobile.SendMessage( "Lower Mana Cost has been capped at {0}.", m_LMCP  );
				}}
catch {
m_LMCP = m_LMCP;
				e.Mobile.SendMessage( "Lower Mana Cost Cap remained at {0}.",m_LMCP  );
}
				
				
				}
			}
			else
			{
				e.Mobile.SendMessage( "Format: LMCP (int)" );
				if (m_LMCP > 0)
				e.Mobile.SendMessage( "LMC cap is currently: {0}.", m_LMCP );
				if (m_LMCP == 0)
				e.Mobile.SendMessage( "Lower Mana Cost Cap is not in effect ");
				
			}
		}
		
		/////////////
	
		
			
		public static int VampAoeHeal
		{
			get{ return m_VHeal; }
			set{ m_VHeal = value; }
		}

		[Usage( "VampAoeHeal (int)" )]
		[Description( "Changes the Vamp Embrace Area damage heal scaling. 1 = normal" )]
		public static void VampAoeHeal_OnCommand( CommandEventArgs e )
		{
			if ( e.Length == 1 )
			{
				double testit = e.GetInt32(0);
				
				
				if ( testit < 0 || testit > 300)
				{
				e.Mobile.SendMessage( "Please Choose a number between 0 and 300." );
				
				}
				else
				{
				try {
				m_VHeal = ((int)testit);
				if (m_VHeal == 0){
				e.Mobile.SendMessage( "Vampiric Embrace Has Been Turned OFF");
				}else{
				e.Mobile.SendMessage( "Vampiric Embrace has been capped at {0}.", ((m_VHeal*20) /100 ));
				}}
			catch {
				m_VHeal = m_VHeal;
				e.Mobile.SendMessage( "Vampiric Embrace Cap remained at {0}.",m_VHeal  );
}
				
				
				}
			}
			else
			{
				e.Mobile.SendMessage( "Format: VampAoeHeal (int)" );
				if (m_VHeal > 0)
				e.Mobile.SendMessage( "Vampiric Embrace Heal is currently: {0}. or {1}", ((m_VHeal * 20)/ 100 ),m_VHeal );
				if (m_VHeal == 0)
				e.Mobile.SendMessage( "Vampiric Embrace Heal is not in effect ");
				
			}
		}
		
		
		///////////////////////////
		
		
		
		
			
		public static int PvPSDI
		{
			get{ return m_PvPSDI; }
			set{ m_PvPSDI = value; }
		}

		[Usage( "PvPSDI (int)" )]
		[Description( "Changes the PvP Spell Damage Increase scaling. 0 = normal" )]
		public static void PvPSDI_OnCommand( CommandEventArgs e )
		{
			if ( e.Length == 1 )
			{
				double testit = e.GetInt32(0);
				
				
				if ( testit < 0 || testit > 1000)
				{
				e.Mobile.SendMessage( "Please Choose a number between 0 and 1000." );
				
				}
				else
				{
				try {
				m_PvPSDI = ((int)testit);
				if (m_PvPSDI == 0){
				e.Mobile.SendMessage( "PvP SDI Has Been Turned OFF");
				}else{
				e.Mobile.SendMessage( "PvP SDI has been capped at {0}.", (m_PvPSDI));
				}}
			catch {
				m_PvPSDI = m_PvPSDI;
				e.Mobile.SendMessage( "PvP SDI Cap remained at {0}.",m_PvPSDI  );
}
				
				
				}
			}
			else
			{
				e.Mobile.SendMessage( "Format: PvPSDI (int)" );
				if (m_PvPSDI > 0)
				e.Mobile.SendMessage( "PvP SDI is currently: {0}.", m_PvPSDI );
				if (m_PvPSDI == 0)
				e.Mobile.SendMessage( "PvP SDI is not in effect ");
				
			}
		}
		
		
		
		
		
		
		//////////////////////////
		
			public static int XPMod
		{
			get{ return m_XPMod; }
			set{ m_XPMod = value; }
		}

		[Usage( "XPMod (int)" )]
		[Description( "Changes the Experience Modifier. 1 = normal" )]
		public static void XPMod_OnCommand( CommandEventArgs e )
		{
			if ( e.Length == 1 )
			{
				double testit = e.GetInt32(0);
				
				
				if ( testit < 0 || testit > 10000)
				{
				e.Mobile.SendMessage( "Please Choose a number between 0 and 10000." );
				
				}
				else
				{
				try {
				m_XPMod = ((int)testit);
				if (m_XPMod == 1){
				e.Mobile.SendMessage( "Extra Experience Has Been Turned OFF");
				}else{
				e.Mobile.SendMessage( "Experience has been modified by {0}.", (m_XPMod));
				}}
			catch {
				m_XPMod = m_XPMod;
				e.Mobile.SendMessage( "Experience Modifier remained at {0}.",m_XPMod  );
}
				
				
				}
			}
			else
			{
				e.Mobile.SendMessage( "Format: XPMod (int)" );
				if (m_XPMod >= 1)
				e.Mobile.SendMessage( "Experience is currently multiplied by: {0}.", m_XPMod );
				if (m_XPMod == 0)
				e.Mobile.SendMessage( "Experience  is not being awarded.");
				
			}
		}
		
		
		//////////////////////////
		
		
			public static int CXPMod
		{
			get{ return m_CXPMod; }
			set{ m_CXPMod = value; }
		}

		[Usage( "CXPMod (int)" )]
		[Description( "Changes the Experience Percent for champ spawns." )]
		public static void CXPMod_OnCommand( CommandEventArgs e )
		{
			if ( e.Length == 1 )
			{
				double testit = e.GetInt32(0);
				
				
				if ( testit < 0 || testit > 10000)
				{
				e.Mobile.SendMessage( "Please Choose a number between 0 and 10000." );
				
				}
				else
				{
				try {
				m_CXPMod = ((int)testit);
				if (m_CXPMod == 1){
				e.Mobile.SendMessage( "Experience at 1%, 0 to disable");
				}else{
				e.Mobile.SendMessage( "Experience has been modified by {0}.", (m_CXPMod));
				}}
			catch {
				m_CXPMod = m_CXPMod;
				e.Mobile.SendMessage( "Experience Modifier remained at {0}.",m_CXPMod  );
}
				
				
				}
			}
			else
			{
				e.Mobile.SendMessage( "Format: CXPMod (int)" );
				if (m_CXPMod >= 1)
				e.Mobile.SendMessage( "Experience is currently modified by: {0}.", m_CXPMod );
				if (m_CXPMod == 0)
				e.Mobile.SendMessage( "Experience  is not being awarded.");
				
			}
		}
		//MapMod
		[Usage( "MapXPMod (int)" )]
		[Description( "Changes the Experience modifer for all Maps but TerMur. Adds this value to them." )]
		public static void MapXPMod_OnCommand( CommandEventArgs e )
		{
		Map cmdMap = null;
			if ( e.Length == 2 )
			{
				double testit2 = e.GetInt32(0);
				string testmap = e.GetString(1);
				testmap.ToLower();
				
				
				if (testmap == "felucca" || testmap == "fel" || testmap == "0")
				{
				cmdMap = Map.Felucca;
			
				}
				if (testmap == "trammel" || testmap == "tram" || testmap == "1")
				{
				cmdMap = Map.Trammel;
			
				}
				if (testmap == "ilsh" || testmap == "ilshenar" || testmap ==  "2")
				{
				cmdMap = Map.Ilshenar;
			
				}
				if (testmap ==  "malas" || testmap ==  "mal" || testmap ==  "3")
				{
				cmdMap = Map.Malas; 
			
				}
				if (testmap ==  "tokuno" || testmap ==  "tok" || testmap ==  "4")
				{
				cmdMap = Map.Tokuno;
			
				}
				if (testmap ==  "termur" || testmap ==  "ter mur" || testmap ==  "ter" || testmap ==  "5")
				{
				cmdMap = Map.TerMur;
			
				}
				if (testmap == "6" || testmap == "7")
				e.Mobile.SendMessage("There are only 6 maps, 0=fel, 1=tram, 2=ilsh, 3=malas, 4=tokuno, 5=TerMur");
				
				
				
				if ( testit2 < 0 || testit2 > 10000)
				{
				e.Mobile.SendMessage( "Please Choose a number between 0 and 10000." );
				
				}
				else
				{
				try {
				if (cmdMap != null)			{	
				SetMapXPMod(cmdMap, ((int)testit2));
				e.Mobile.SendMessage("Modifier for map {0} has been set to {1}",cmdMap,testit2);
				}
				//m_MapXPMod = ((int)testit2);
				/*if (m_MapXPMod == 1){
				e.Mobile.SendMessage( "Experience mod 2 at 1, 0 to disable");
				}else{
				e.Mobile.SendMessage( "Experience Modifer has been increased to {0}. for All Maps except Ter Mur", (m_MapXPMod));
				}*/}
			catch {
				//m_MapXPMod = m_MapXPMod;
				e.Mobile.SendMessage("Error, Aborting Action"); //"Experience Modifier remained at {0}.",m_MapXPMod  );
				Console.WriteLine("ERROR in MAP SET TWEAK");
}
				
				
				}
			}
			else
			{
				e.Mobile.SendMessage( "Format: MapXPMod (int) (map)" );
				//if (m_MapXPMod == 0)
				e.Mobile.SendMessage( "Current Map Modifier Values: Felucca: {0} -  Trammel: {1} - Ilshenar: {2} - Malas: {3} - Tokuno: {4} - Ter Mur: {5} -- Global Modifier is: {6} (added to Map Modifiers)", m_FelMapXPMod, m_TramMapXPMod, m_IlshMapXPMod, m_MalMapXPMod, m_TokMapXPMod, m_TerMapXPMod, m_XPMod);
				
			}
		}
		
		
		
		
		
		///////////////////////////
	}	
}