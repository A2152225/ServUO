    //////////////////////////////////
   //			           		   //
  //      Scripted by Burner      //
 //		             	 		 //
//////////////////////////////////
using System; 
using System.Collections; 
using Server; 
using Server.Mobiles; 
using Server.Network; 
using Server.Targeting;
using Server.Commands;


namespace Server.Mobiles
{ 
	public class KPSSystem
	{ 

		public static void Initialize()
		{
			CommandSystem.Register( "KPS", AccessLevel.Player, new CommandEventHandler( KPS_OnCommand ) );    
		} 

		public static void KPS_OnCommand( CommandEventArgs args )
		{ 
			Mobile m = args.Mobile; 
			PlayerMobile from = m as PlayerMobile; 
          
			if( from != null ) 
			{  
				from.SendMessage ( "Target a Steed that you own to get their EXP amount." );
				m.Target = new InternalTarget();
			} 
		} 

		private class InternalTarget : Target
		{
			public InternalTarget() : base( 8, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object obj )
			{
				if ( !from.Alive )
				{
					from.SendMessage( "You may not do that while dead." );
				}
                           	else if ( obj is EvoSteed && obj is BaseCreature ) 
                           	{ 
					BaseCreature bc = (BaseCreature)obj;
					EvoSteed ed = (EvoSteed)obj;

					if ( ed.Controlled == true && ed.ControlMaster == from )
					{
						ed.PublicOverheadMessage( MessageType.Regular, ed.SpeechHue, true, ed.Name +" has "+ ed.KP +" kill points.", false );
					}
					else
					{
						from.SendMessage( "You do not control this Evo Steed!" );
					}
                           	} 
                           	else 
                           	{ 
                              		from.SendMessage( "That is not an Evo Steed!" );
			   	}
			}
		}
	} 
} 
