using System;
using System.Text;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Commands
{
	public class CreateHueRoom
	{

		public static void Initialize()
		{
			CommandSystem.Register("CreateHueRoom", AccessLevel.Developer, new CommandEventHandler(GenericCommand_OnCommand));
		}


			public static void CreateItems( Mobile from)
			{
				
				int X = 0;
				int Y = 0;
				int Z = 0;
				
				int XS = 0;
				int YS = 0;
				int ZS = 0;
				
				int hue = 3000;
				int rowlength = 50;
				int Spacer = 2;

			
			
						X = from.X;
						Y = from.Y;
						Z = from.Z;
						XS = from.X;  //starting X
						YS = from.Y;//starting Y
						ZS = from.Z;//starting Z
			
			/*	if (targeted is LandTarget ||  targeted is Mobile || targeted is Item) 
				{
					
					
					if (targeted is LandTarget)
					{
					LandTarget LT = (LandTarget)targeted;
						X = LT.X;
						Y = LT.Y;
						Z = LT.Z;
						XS = LT.X;  //starting X
						YS = LT.Y;//starting Y
						ZS = LT.Z;//starting Z
					}
					
					if (targeted is Mobile)
					{
					Mobile MO = (Mobile)targeted;
						X = MO.X;
						Y = MO.Y;
						Z = MO.Z;
						XS = MO.X;  //starting X
						YS = MO.Y;//starting Y
						ZS = MO.Z;//starting Z
					}
					
					if (targeted is Item)
					{
					Item IT = (Item)targeted;
						X = IT.X;
						Y = IT.Y;
						Z = IT.Z;
						XS = IT.X;  //starting X
						YS = IT.Y;//starting Y
						ZS = IT.Z;//starting Z
					}
					
				*/	
					while (hue > -1)
					{
						HueRoomItem HRI = new HueRoomItem(hue);
						HRI.X = X; 
						HRI.Y = Y; 
						HRI.Z = Z;
						HRI.Map = from.Map;
						HRI.MoveToWorld(new Point3D(X, Y, Z),  from.Map); //  MoveToWorld(X,Y,Z);
						X--;
						if (X == (XS-rowlength))
						{
						X += rowlength;
						Y+=Spacer;
							}
						hue--;
					}

					
				
				
				
				
			}
		

		[Usage( "CreateHueRoom" )]
		public static void GenericCommand_OnCommand( CommandEventArgs e )
		{
			if(e == null || e.Mobile == null) return;

			CreateItems(e.Mobile);
		}
	}
}
