/***************************************************************************
 *                               GumpTooltip.cs
 *                            -------------------
 *   begin                : May 1, 2002
 *   copyright            : (C) The RunUO Software Team
 *   email                : info@runuo.com
 *
 *   $Id$
 *
 ***************************************************************************/

/***************************************************************************
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 2 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/

using System;
using Server.Network;

namespace Server.Gumps
{
	public class GumpTooltip : GumpEntry
	{
		private int m_Number;
		private string m_Text;
        public GumpTooltip( int number )
		{
			m_Number = number;
		}
	public GumpTooltip( string text )
		{
			m_Text = text;
		}

        public int Number
		{
			get
			{
				return m_Number;
			}
			set
			{
				Delta( ref m_Number, value );
				
			}
		}
		public string Text
		{
			get
			{
				return m_Text;
			}
			set
			{
				//m_Text = Text;//value;
				Console.WriteLine("{0} mtext, {1} Text " ,m_Text, Text );
				Delta( ref m_Text, value );
				Console.WriteLine("{0} mtext, {1} After Delta Text " ,m_Text, Text );
			}
		}


        public override string Compile()
		{
			if (m_Text != null)
				return string.Format("{{ tooltip {0} }}", m_Text);
			else 
				return string.Format("{{ tooltip {0} }}", m_Number);
				
        }

		private static byte[] m_LayoutName = Gump.StringToBuffer( "tooltip" );

		public override void AppendTo( IGumpWriter disp )
		{
			Console.WriteLine("{0}",disp );
			disp.AppendLayout( m_LayoutName );
			Console.WriteLine("{0}",m_LayoutName );
			Console.WriteLine("Mtext {0}  mnumber {1}",m_Text,m_Number );
			if (m_Text != null)
			disp.AppendLayout( m_Text );
			else		
			disp.AppendLayout( m_Number );
        }
	}
}