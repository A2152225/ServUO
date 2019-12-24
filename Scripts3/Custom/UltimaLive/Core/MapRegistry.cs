/* Copyright(c) 2016 UltimaLive
 * 
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the
 * "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to
 * permit persons to whom the Software is furnished to do so, subject to
 * the following conditions:
 *
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
 * CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
*/

using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;

namespace UltimaLive
{
  public class MapRegistry
  {
    public struct MapDefinition
    {
      public int FileIndex;
      public Point2D Dimensions;
      public Point2D WrapAroundDimensions;
      public MapDefinition(int index, Point2D dimension, Point2D wraparound)
      {
        FileIndex = index;
        Dimensions = dimension;
        WrapAroundDimensions = wraparound;
      }
    }

    private static Dictionary<int, MapDefinition> m_Definitions = new Dictionary<int, MapDefinition>();
    public static Dictionary<int, MapDefinition> Definitions
    {
      get { return m_Definitions; }
    }
    private static Dictionary<int, List<int>> m_MapAssociations = new Dictionary<int, List<int>>();
    public static Dictionary<int, List<int>> MapAssociations
    {
      get { return m_MapAssociations; }
    }


    public static void AddMapDefinition(int index, int associated, Point2D dimensions, Point2D wrapDimensions)
    {
      if (!m_Definitions.ContainsKey(index))
      {
        m_Definitions.Add(index, new MapDefinition(associated, dimensions, wrapDimensions));
        if (m_MapAssociations.ContainsKey(associated))
        {
          m_MapAssociations[associated].Add(index);
        }
        else
        {
          m_MapAssociations[associated] = new List<int>();
          m_MapAssociations[associated].Add(index);
        }
      }
    }

    public static void Configure()
    {
      AddMapDefinition(0, 0, new Point2D(7168, 4096), new Point2D(5120, 4096)); //felucca
      AddMapDefinition(1, 1, new Point2D(7168, 4096), new Point2D(5120, 4096)); //trammel
      AddMapDefinition(2, 2, new Point2D(2304, 1600), new Point2D(2304, 1600)); //Ilshenar
      AddMapDefinition(3, 3, new Point2D(2560, 2048), new Point2D(2560, 2048)); //Malas
      AddMapDefinition(4, 4, new Point2D(1448, 1448), new Point2D(1448, 1448)); //Tokuno
      AddMapDefinition(5, 5, new Point2D(1280, 4096), new Point2D(1280, 4096)); //TerMur
	  AddMapDefinition(32, 32, new Point2D(7168, 4096), new Point2D(5120, 4096));//Essex 
	  
	  
	  //start hard
			AddMapDefinition(40, 40,  new Point2D(7168, 4096), new Point2D(5120, 4096)); //felucca
			AddMapDefinition(41, 41, new Point2D(7168, 4096), new Point2D(5120, 4096)); //trammel
			AddMapDefinition(42, 42,  new Point2D(2304, 1600), new Point2D(2304, 1600)); //Ilshenar
			AddMapDefinition(43, 43,  new Point2D(2560, 2048), new Point2D(2560, 2048)); //Malas
			AddMapDefinition(44, 44, new Point2D(1448, 1448), new Point2D(1448, 1448)); //Tokuno
			AddMapDefinition(45, 45, new Point2D(1280, 4096), new Point2D(1280, 4096)); //TerMur
			AddMapDefinition(46, 46, new Point2D(7168, 4096), new Point2D(5120, 4096));//Essex 
			
			//start Expert
			AddMapDefinition(50, 50, new Point2D(7168, 4096), new Point2D(5120, 4096)); //felucca
			AddMapDefinition(51, 51, new Point2D(7168, 4096), new Point2D(5120, 4096)); //trammel
			AddMapDefinition(52, 52,  new Point2D(2304, 1600), new Point2D(2304, 1600)); //Ilshenar
			AddMapDefinition(53, 53, new Point2D(2560, 2048), new Point2D(2560, 2048)); //Malas
			AddMapDefinition(54, 54,  new Point2D(1448, 1448), new Point2D(1448, 1448)); //Tokuno
			AddMapDefinition(55, 55, new Point2D(1280, 4096), new Point2D(1280, 4096)); //TerMur
			AddMapDefinition(56, 56, new Point2D(7168, 4096), new Point2D(5120, 4096));//Essex 
			
			
			
			//start Master
			AddMapDefinition(60, 60, new Point2D(7168, 4096), new Point2D(5120, 4096)); //felucca
			AddMapDefinition(61, 61, new Point2D(7168, 4096), new Point2D(5120, 4096)); //trammel
			AddMapDefinition(62, 62,  new Point2D(2304, 1600), new Point2D(2304, 1600)); //Ilshenar
			AddMapDefinition(63, 63, new Point2D(2560, 2048), new Point2D(2560, 2048)); //Malas
			AddMapDefinition(64, 64,  new Point2D(1448, 1448), new Point2D(1448, 1448)); //Tokuno
			AddMapDefinition(65, 65, new Point2D(1280, 4096), new Point2D(1280, 4096)); //TerMur
			AddMapDefinition(66, 66, new Point2D(7168, 4096), new Point2D(5120, 4096));//Essex 
			
			
			
			
			//start T1
			AddMapDefinition(70, 70, new Point2D(7168, 4096), new Point2D(5120, 4096)); //felucca
			AddMapDefinition(71, 71, new Point2D(7168, 4096), new Point2D(5120, 4096)); //trammel
			AddMapDefinition(72, 72,  new Point2D(2304, 1600), new Point2D(2304, 1600)); //Ilshenar
			AddMapDefinition(73, 73, new Point2D(2560, 2048), new Point2D(2560, 2048)); //Malas
			AddMapDefinition(74, 74,  new Point2D(1448, 1448), new Point2D(1448, 1448)); //Tokuno
			AddMapDefinition(75, 75, new Point2D(1280, 4096), new Point2D(1280, 4096)); //TerMur
			AddMapDefinition(76, 76, new Point2D(7168, 4096), new Point2D(5120, 4096));//Essex 
			
			
			
			
			//start T2
			AddMapDefinition(80, 80, new Point2D(7168, 4096), new Point2D(5120, 4096)); //felucca
			AddMapDefinition(81, 81, new Point2D(7168, 4096), new Point2D(5120, 4096)); //trammel
			AddMapDefinition(82, 82,  new Point2D(2304, 1600), new Point2D(2304, 1600)); //Ilshenar
			AddMapDefinition(83, 83, new Point2D(2560, 2048), new Point2D(2560, 2048)); //Malas
			AddMapDefinition(84, 84,  new Point2D(1448, 1448), new Point2D(1448, 1448)); //Tokuno
			AddMapDefinition(85, 85, new Point2D(1280, 4096), new Point2D(1280, 4096)); //TerMur
			AddMapDefinition(86, 86, new Point2D(7168, 4096), new Point2D(5120, 4096));//Essex 
			
			
			
			//start T3
			AddMapDefinition(90, 90, new Point2D(7168, 4096), new Point2D(5120, 4096)); //felucca
			AddMapDefinition(91, 91, new Point2D(7168, 4096), new Point2D(5120, 4096)); //trammel
			AddMapDefinition(92, 92,  new Point2D(2304, 1600), new Point2D(2304, 1600)); //Ilshenar
			AddMapDefinition(93, 93, new Point2D(2560, 2048), new Point2D(2560, 2048)); //Malas
			AddMapDefinition(94, 94,  new Point2D(1448, 1448), new Point2D(1448, 1448)); //Tokuno
			AddMapDefinition(95, 95, new Point2D(1280, 4096), new Point2D(1280, 4096)); //TerMur
			AddMapDefinition(96, 96, new Point2D(7168, 4096), new Point2D(5120, 4096));//Essex 
			
			
			
			//start T4
			AddMapDefinition(100, 100, new Point2D(7168, 4096), new Point2D(5120, 4096)); //felucca
			AddMapDefinition(101, 101, new Point2D(7168, 4096), new Point2D(5120, 4096)); //trammel
			AddMapDefinition(102, 102,  new Point2D(2304, 1600), new Point2D(2304, 1600)); //Ilshenar
			AddMapDefinition(103, 103, new Point2D(2560, 2048), new Point2D(2560, 2048)); //Malas
			AddMapDefinition(104, 104,  new Point2D(1448, 1448), new Point2D(1448, 1448)); //Tokuno
			AddMapDefinition(105, 105, new Point2D(1280, 4096), new Point2D(1280, 4096)); //TerMur
			AddMapDefinition(106, 106, new Point2D(7168, 4096), new Point2D(5120, 4096));//Essex 
			
			
			//start T5
			AddMapDefinition(110, 110,  new Point2D(7168, 4096), new Point2D(5120, 4096)); //felucca
			AddMapDefinition(111, 111, new Point2D(7168, 4096), new Point2D(5120, 4096)); //trammel
			AddMapDefinition(112, 112,  new Point2D(2304, 1600), new Point2D(2304, 1600)); //Ilshenar
			AddMapDefinition(113, 113, new Point2D(2560, 2048), new Point2D(2560, 2048)); //Malas
			AddMapDefinition(114, 114,  new Point2D(1448, 1448), new Point2D(1448, 1448)); //Tokuno
			AddMapDefinition(115, 115, new Point2D(1280, 4096), new Point2D(1280, 4096)); //TerMur
			AddMapDefinition(116, 116, new Point2D(7168, 4096), new Point2D(5120, 4096));//Essex 
			
			
			//start T6
			AddMapDefinition(120, 120,  new Point2D(7168, 4096), new Point2D(5120, 4096)); //felucca
			AddMapDefinition(121, 121, new Point2D(7168, 4096), new Point2D(5120, 4096)); //trammel
			AddMapDefinition(122, 122,  new Point2D(2304, 1600), new Point2D(2304, 1600)); //Ilshenar
			AddMapDefinition(123, 123, new Point2D(2560, 2048), new Point2D(2560, 2048)); //Malas
			AddMapDefinition(124, 124,  new Point2D(1448, 1448), new Point2D(1448, 1448)); //Tokuno
			AddMapDefinition(125, 125, new Point2D(1280, 4096), new Point2D(1280, 4096)); //TerMur
			AddMapDefinition(126, 126, new Point2D(7168, 4096), new Point2D(5120, 4096));//Essex 
			
			
	  
	/*  AddMapDefinition(33, 33, new Point2D(7168, 4096), new Point2D(5120, 4096)); //felucca
      AddMapDefinition(34, 34, new Point2D(7168, 4096), new Point2D(5120, 4096)); //trammel
      AddMapDefinition(35, 35, new Point2D(2304, 1600), new Point2D(2304, 1600)); //Ilshenar
      AddMapDefinition(36, 36, new Point2D(2560, 2048), new Point2D(2560, 2048)); //Malas
      AddMapDefinition(37, 37, new Point2D(1448, 1448), new Point2D(1448, 1448)); //Tokuno
      AddMapDefinition(38, 38, new Point2D(1280, 4096), new Point2D(1280, 4096)); //TerMur
	  */
      //those are sample maps that use same original map...
      //AddMapDefinition(32, 0, new Point2D(7168, 4096), new Point2D(5120, 4096));
      //AddMapDefinition(33, 0, new Point2D(7168, 4096), new Point2D(5120, 4096));
      //AddMapDefinition(34, 1, new Point2D(7168, 4096), new Point2D(5120, 4096));

      EventSink.ServerList += new ServerListEventHandler(EventSink_OnServerList);
      EventSink.Login += new LoginEventHandler(EventSink_Login);
    }

    private static void EventSink_OnServerList(ServerListEventArgs args)
    {
      args.State.Send(new UltimaLive.Network.LoginComplete());
      args.State.Send(new UltimaLive.Network.MapDefinitions());
    }

    private static void EventSink_Login(LoginEventArgs args)
    {
      args.Mobile.Send(new UltimaLive.Network.QueryClientHash(args.Mobile));
    }
  }
}