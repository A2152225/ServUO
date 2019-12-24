/*
 * Crée par SharpDevelop.
 * Gargouille
 * Date: 23/10/2014
 */

using System;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Targeting;
using Server.Commands;
using Server.Commands.Generic;

namespace UltimaLive
{
	public class ULMassLandID
	{
		public static void Initialize()
		{
			Live.Register("AreaLandID", AccessLevel.GameMaster, new CommandEventHandler(AreaLandIDCommand));
			Live.Register("CircularLandID", AccessLevel.GameMaster, new CommandEventHandler(CircularLandIDCommand));
		}
		
		public static void AreaLandIDCommand(CommandEventArgs e)
		{
			if (e.Length != 1)
			{
				e.Mobile.SendMessage("You must specify the Item ID");
				
				return;
			}
			
			BoundingBoxPicker.Begin(e.Mobile, new BoundingBoxCallback(OnAreaTargeted),e);
		}
		
		public static void OnAreaTargeted( Mobile from, Map map, Point3D start, Point3D end, object state)
		{
			CommandEventArgs e = (CommandEventArgs)state;
			
			if(e!=null)
			{
				int	id = e.GetInt32(0);
				
				if(id>0)
				{
					MapOperationSeries moveSeries = new MapOperationSeries(null, from.Map.MapID);

					bool first = true;
					
					for( int x = start.X; x <= end.X; ++x )
					{
						for( int y = start.Y; y <= end.Y; ++y )
						{
							if (first)
							{
								moveSeries = new MapOperationSeries(new SetLandID(x, y, from.Map.MapID, id), from.Map.MapID);
								first = false;
							}
							else
							{
								moveSeries.Add(new SetLandID(x, y, from.Map.MapID, id));
							}
						}
					}
					
					moveSeries.DoOperation();
				}
			}
		}
		
		public static void CircularLandIDCommand(CommandEventArgs e)
		{
			if (e.Length != 2)
			{
				e.Mobile.SendMessage("You must specify a radius and an itemID.");
				return;
			}
			int radius = e.GetInt32(0);
			int id = e.GetInt32(1);

			if (radius > 0)
			{
				e.Mobile.Target = new CircularSetLandTarget(radius, id);
			}
		}
		
		private class CircularSetLandTarget : Private_BaseLandRadialTarget
		{
			private int m_Radius;
			private int m_Id;
			public CircularSetLandTarget(int radius, int id)
				: base(1, radius, 0)
			{
				m_Radius = radius;
				m_Id = id;
			}

			protected override void OnTarget(Mobile from, object o)
			{
				if (base.SetupTarget(from, o))
				{
					List<Point2D> circle = UltimaLiveUtility.rasterFilledCircle(new Point2D(m_Location.X, m_Location.Y), m_Radius);

					MapOperationSeries moveSeries = new MapOperationSeries(null, from.Map.MapID);

					bool first = true;
					foreach (Point2D p in circle)
					{
						if (first)
						{
							moveSeries = new MapOperationSeries(new SetLandID(p.X, p.Y, from.Map.MapID, m_Id), from.Map.MapID);
							first = false;
						}
						else
						{
							moveSeries.Add(new SetLandID(p.X, p.Y, from.Map.MapID, m_Id));
						}
					}

					moveSeries.DoOperation();
				}
			}
		}
		
		private class Private_BaseLandRadialTarget : Live.RadialTarget
		{
			protected IPoint3D m_Location;
			public Private_BaseLandRadialTarget(int TType, int Radius, int Height)
				: base(TType, Radius, Height)
			{
			}

			protected override void OnTarget(Mobile from, object o)
			{
			}

			protected bool SetupTarget(Mobile from, object o)
			{
				if (!BaseCommand.IsAccessible(from, o))
				{
					from.SendMessage("That is not accessible.");
					return false;
				}

				if (!(o is IPoint3D))
					return false;
				m_Location = (IPoint3D)o;

				return true;
			}
		}

	}
}