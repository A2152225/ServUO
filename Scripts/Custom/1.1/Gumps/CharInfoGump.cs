using System; 
using System.Net; 
using System.Collections;
using Server; 
using Server.Accounting; 
using Server.Gumps; 
using Server.Items; 
using Server.Mobiles; 
using Server.Network; 
using Server.Misc; 

namespace Server.Gumps 
{ 
	public class CharInfoGump : Gump 
	{
		public CharInfoGump( Mobile owner ) : base( 50,50 ) 
		{ 
         		PlayerMobile pm = owner as PlayerMobile;

			AddPage( 1 ); 
			AddBackground( 0, 0, 622, 220, 5054 ); 

			AddBackground( 8, 28, 270, 92, 3000 ); 
			AddBackground( 8, 136, 270, 20, 3000 ); 
			AddBackground( 8, 156, 270, 20, 3000 ); 
			AddBackground( 286, 28, 135, 92, 3000 ); 

			AddLabel( 294, 36, 0, "You have "+ pm.SP +"." ); 
			AddLabel( 294, 50, 0, "specialization points." );
			AddLabel( 294, 64, 0, "Current Str: "+ owner.RawStr );
			AddLabel( 294, 78, 0, "Current Dex: "+ owner.RawDex );
			AddLabel( 294, 92, 0, "Current Int: "+ owner.RawInt );
			AddButton( 314, 136, 4005, 4007, 1, GumpButtonType.Reply, 0 ); 
			AddLabel( 346, 139, 32, "Strength" );
			AddButton( 314, 161, 4005, 4007, 2, GumpButtonType.Reply, 0 ); 
			AddLabel( 346, 164, 32, "Dexterity" );
			AddButton( 314, 186, 4005, 4007, 3, GumpButtonType.Reply, 0 ); 
			AddLabel( 346, 189, 32, "Intellegence" );

			AddButton( 440, 28, 4005, 4007, 4, GumpButtonType.Reply, 0 ); 
			AddLabel( 470, 28, 32, "Skills" );

			AddButton( 440, 48, 4005, 4007, 8, GumpButtonType.Reply, 0 );
			if ( pm.HorizontalExpBar )
				AddLabel( 470, 48, 32, "Horizontal Exp Bar" );
			else
				AddLabel( 470, 48, 32, "Vertical Exp Bar" );

			AddButton( 440, 108, 4005, 4007, 6, GumpButtonType.Reply, 0 );
			if ( pm.ShowExpBar )
				AddLabel( 470, 108, 32, "Showing Exp Bar" );
			else
				AddLabel( 470, 108, 32, "Not Showing Exp Bar" );

		/*	if ( pm.IllnessRace == IllnessRace.Vampire )
			{
				AddButton( 440, 128, 4005, 4007, 9, GumpButtonType.Reply, 0 );
			//	if ( pm.HorizontalBloodBar )
				//	AddLabel( 470, 128, 32, "Horizontal Blood Bar" );
				else
					AddLabel( 470, 128, 32, "Vertical Blood Bar" );

				AddButton( 440, 188, 4005, 4007, 7, GumpButtonType.Reply, 0 );
				if ( pm.ShowBloodBar )
					AddLabel( 470, 188, 32, "Showing Blood Bar" );
				else
					AddLabel( 470, 188, 32, "Not Showing Blood Bar" );
			}
*/
			AddLabel( 50, 8, 33, ServerList.ServerName +" Level System" ); 

			AddLabel( 16, 36, 0, "Level: "+ pm.LvL );
			AddLabel( 16, 52, 0, "Playtime: " + pm.GameTime.ToString(@"d\ \d\a\y\s\ h\ \h\o\u\r\s\.") );

			AddLabel( 16, 136, 0, "You have "+ pm.EXP +" experience points." ); 

			int Level = pm.LvL;
			long LastLevel = pm.LastLevelExp;
			long ExpRequired =   ((long)(LastLevel*1.2)) ; //(long)(((Math.Pow((Level*1.5), 1.5)*1.5)+20)*Experience.AvgMonsterExp)+LastLevel;
			if (ExpRequired == 0)
				ExpRequired = 100;
			if (LastLevel == 0)
				ExpRequired = 100;
			if (pm.LvL == 1 || pm.PrestigeLvl == 1)
						ExpRequired = 220;
					
			if ( pm.LvL < pm.MaxLvl ) // Changed from 200 to 500 March 20th 2006 -{Rahvin the Evil}  //Changed from 500 to 2000 7-12-07 {RE}
			{
				AddLabel( 16, 156, 0, "EXP til next level: " + (ExpRequired - pm.EXP) ); 
			}
			else
			{
				AddLabel( 16, 156, 0, "EXP til next Paragon level: " + (ExpRequired - pm.EXP) );    // "You have no more levels to gain." ); 
			}

			//AddLabel( 80, 183, 906, "Created by Akara Essex Dev. Team" ); //not stealing credit

			AddButton( 8, 180, 4005, 4007, 0, GumpButtonType.Reply, 0 ); 
			AddLabel( 40, 183, 33, "Close" ); 
		} 

		public override void OnResponse( NetState state, RelayInfo info )
		{ 
			Mobile from = state.Mobile; 
         		PlayerMobile pm = from as PlayerMobile;

			switch ( info.ButtonID ) 
			{ 
				case 0:
				{
					from.SendMessage( "Closed." ); 
					break; 
				} 
				case 1:
				{
					if ( pm.SP > 0 )
					{ 
						if ( !SkillCheck.CanRaise( from, SkillCheck.Stat.Str ) )
						{
							from.SendMessage( "You can't gain anymore strength." );
						}
						else if ( from.RawStatTotal >= from.StatCap )
						{
							from.SendMessage( "You can't gain anymore stats." );
						}
						else
						{
							pm.SP -= 1;
							from.RawStr += 1;
							from.SendMessage( "You have used 1 specialization point and you gain 1 strength." );
							from.SendGump( new CharInfoGump( from ) ); 
							from.Send( new PlaySound( 323, from.Location ) );
						}
					}
					else
					{
						from.SendMessage( "You do not have enough specialization points for that." );
						from.SendGump( new CharInfoGump( from ) ); 
					} 
					break;
				}
				case 2:
				{
					if ( pm.SP > 0 )
					{ 
						if ( !SkillCheck.CanRaise( from, SkillCheck.Stat.Dex ) )
						{
							from.SendMessage( "You can't gain anymore dexterity." );
						}
						else if ( from.RawStatTotal >= from.StatCap )
						{
							from.SendMessage( "You can't gain anymore stats." );
						}
						else
						{
							pm.SP -= 1;
							from.RawDex += 1;
							from.SendMessage( "You have used 1 specialization point and you gain 1 dexterity." );
							from.SendGump( new CharInfoGump( from ) ); 
							from.Send( new PlaySound( 323, from.Location ) );
						}
					}
					else
					{
						from.SendMessage( "You do not have enough specialization points for that." );
						from.SendGump( new CharInfoGump( from ) ); 
					} 
					break;
				}
				case 3:
				{
					if ( pm.SP > 0 )
					{ 
						if ( !SkillCheck.CanRaise( from, SkillCheck.Stat.Int ) )
						{
							from.SendMessage( "You can't gain anymore intellegence." );
						}
						else if ( from.RawStatTotal >= from.StatCap )
						{
							from.SendMessage( "You can't gain anymore stats." );
						}
						else
						{
							pm.SP -= 1;
							from.RawInt += 1;
							from.SendMessage( "You have used 1 specialization point and you gain 1 intellegence." );
							from.SendGump( new CharInfoGump( from ) ); 
							from.Send( new PlaySound( 323, from.Location ) );
						}
					}
					else
					{
						from.SendMessage( "You do not have enough specialization points for that." );
						from.SendGump( new CharInfoGump( from ) ); 
					}
					break; 
				}
				case 4:
				{
					from.SendGump( new RaceSkillGump( from, from ) );
					break; 
				}
				case 6:
				{
					if ( pm.ShowExpBar )
					{
						pm.ShowExpBar = false;
					}
					else
					{
						pm.ShowExpBar = true;
					}

					from.SendGump( new CharInfoGump( from ) );

					if ( pm.ShowExpBar )
						from.SendGump( new ExpBarGump( pm ) );
					else
						from.CloseGump( typeof( ExpBarGump ) );
					break;
				}
				case 7:
				{
			/*		if ( pm.ShowBloodBar )
					{
						pm.ShowBloodBar = false;
					}
					else
					{
						pm.ShowBloodBar = true;
					}
					

					from.SendGump( new CharInfoGump( from ) );

					if ( pm.IllnessRace == IllnessRace.Vampire )
						if ( pm.ShowBloodBar )
							from.SendGump( new BloodBarGump( pm ) );
						else
							from.CloseGump( typeof( BloodBarGump ) );
*/					break;
				}
				case 8:
				{
					if ( pm.HorizontalExpBar )
					{
						pm.HorizontalExpBar = false;
					}
					else
					{
						pm.HorizontalExpBar = true;
					}

					from.SendGump( new CharInfoGump( from ) );

					if ( pm.ShowExpBar )
						from.SendGump( new ExpBarGump( pm ) );
					break;
				}
				case 9:
				{
				/*	if ( pm.HorizontalBloodBar )
					{
						pm.HorizontalBloodBar = false;
					}
					else
					{
						pm.HorizontalBloodBar = true;
					}

					from.SendGump( new CharInfoGump( from ) );

					if ( pm.IllnessRace == IllnessRace.Vampire )
						if ( pm.ShowBloodBar )
							from.SendGump( new BloodBarGump( pm ) );
				*/	break;
				}
			} 
		} 
	}
	public class RaceSkillGump : Gump
	{
		public static bool OldStyle = PropsConfig.OldStyle;

		public static int GumpOffsetX = PropsConfig.GumpOffsetX;
		public static int GumpOffsetY = PropsConfig.GumpOffsetY;

		public static int TextHue = PropsConfig.TextHue;
		public static int TextOffsetX = PropsConfig.TextOffsetX;

		public static int OffsetGumpID = PropsConfig.OffsetGumpID;
		public static int HeaderGumpID = PropsConfig.HeaderGumpID;
		public static int  EntryGumpID = PropsConfig.EntryGumpID;
		public static int   BackGumpID = PropsConfig.BackGumpID;
		public static int    SetGumpID = PropsConfig.SetGumpID;

		public static int SetWidth = PropsConfig.SetWidth;
		public static int SetOffsetX = PropsConfig.SetOffsetX, SetOffsetY = PropsConfig.SetOffsetY;
		public static int SetButtonID1 = PropsConfig.SetButtonID1;
		public static int SetButtonID2 = PropsConfig.SetButtonID2;

		public static int PrevWidth = PropsConfig.PrevWidth;
		public static int PrevOffsetX = PropsConfig.PrevOffsetX, PrevOffsetY = PropsConfig.PrevOffsetY;
		public static int PrevButtonID1 = PropsConfig.PrevButtonID1;
		public static int PrevButtonID2 = PropsConfig.PrevButtonID2;

		public static int NextWidth = PropsConfig.NextWidth;
		public static int NextOffsetX = PropsConfig.NextOffsetX, NextOffsetY = PropsConfig.NextOffsetY;
		public static int NextButtonID1 = PropsConfig.NextButtonID1;
		public static int NextButtonID2 = PropsConfig.NextButtonID2;

		public static int OffsetSize = PropsConfig.OffsetSize;

		public static int EntryHeight = PropsConfig.EntryHeight;
		public static int BorderSize = PropsConfig.BorderSize;

		private static bool PrevLabel = OldStyle, NextLabel = OldStyle;

		private static int PrevLabelOffsetX = PrevWidth + 1;
		private static int PrevLabelOffsetY = 0;

		private static int NextLabelOffsetX = -29;
		private static int NextLabelOffsetY = 0;

		private static int NameWidth = 107;
		private static int ValueWidth = 128;

		private static int EntryCount = 15;

		private static int TypeWidth = NameWidth + OffsetSize + ValueWidth;

		private static int TotalWidth = OffsetSize + NameWidth + OffsetSize + ValueWidth + OffsetSize + SetWidth + OffsetSize;
		private static int TotalHeight = OffsetSize + ((EntryHeight + OffsetSize) * (EntryCount + 1));

		private static int BackWidth = BorderSize + TotalWidth + BorderSize;
		private static int BackHeight = BorderSize + TotalHeight + BorderSize;

		private static int IndentWidth = 12;

		private Mobile m_From;
		private Mobile m_Target;

		private RaceSkillsGumpGroup[] m_Groups;
		private RaceSkillsGumpGroup m_Selected;

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			PlayerMobile pm = (PlayerMobile)m_From;

			int buttonID = info.ButtonID - 1;

			int index = buttonID / 3;
			int type = buttonID % 3;

			switch ( type )
			{
				case 0:
				{
					if ( index >= 0 && index < m_Groups.Length )
					{
						RaceSkillsGumpGroup newSelection = m_Groups[index];

						if ( m_Selected != newSelection )
							m_From.SendGump( new RaceSkillGump( m_From, m_Target, newSelection ) );
						else
							m_From.SendGump( new RaceSkillGump( m_From, m_Target, null ) );
					}

					break;
				}
				case 1:
				{
					if ( m_Selected != null && index >= 0 && index < m_Selected.Skills.Length )
					{
						Skill sk = m_Target.Skills[m_Selected.Skills[index]];

						if ( sk != null )
						{
							if ( ( m_From.SkillsTotal + 0.5 ) > m_From.SkillsCap )
							{ 
								m_From.SendMessage( "You cannot exceed the skill cap." ); 
								m_From.SendGump( new RaceSkillGump( m_From, m_Target, m_Selected ) );
								return; 
							} 
							else if (sk.Base + 0.1 <= sk.Cap)
							{ 
								if ( pm.SP > 0 )
								{
									pm.SP -= 1;
									sk.Base += 0.5; 
									if ( sk.Base > sk.Cap )
										sk.Base = sk.Cap;
								}
								else
									m_From.SendMessage( "You do not have enough specialization points." );
							} 
							else 
								m_From.SendMessage("You cannot exceed any skill over it's cap."); 
						}
						m_From.SendGump( new RaceSkillGump( m_From, m_Target, m_Selected ) );
					}

					break;
				}
			}
		}

		public int GetButtonID( int type, int index )
		{
			return 1 + (index * 3) + type;
		}

		public RaceSkillGump( Mobile from, Mobile target ) : this( from, target, null )
		{
		}

		public RaceSkillGump( Mobile from, Mobile target, RaceSkillsGumpGroup selected ) : base( GumpOffsetX, GumpOffsetY )
		{
			m_From = from;
			m_Target = target;

			m_Groups = RaceSkillsGumpGroup.Groups;
			m_Selected = selected;

			int count = m_Groups.Length;

			if ( selected != null )
				count += selected.Skills.Length;

			int totalHeight = OffsetSize + ((EntryHeight + OffsetSize) * (count + 1));

			AddPage( 0 );

			AddBackground( 0, 0, BackWidth, BorderSize + totalHeight + BorderSize, BackGumpID );
			AddImageTiled( BorderSize, BorderSize, TotalWidth - (OldStyle ? SetWidth + OffsetSize : 0), totalHeight, OffsetGumpID );

			int x = BorderSize + OffsetSize;
			int y = BorderSize + OffsetSize;

			int emptyWidth = TotalWidth - PrevWidth - NextWidth - (OffsetSize * 4) - (OldStyle ? SetWidth + OffsetSize : 0);

			if ( OldStyle )
				AddImageTiled( x, y, TotalWidth - (OffsetSize * 3) - SetWidth, EntryHeight, HeaderGumpID );
			else
				AddImageTiled( x, y, PrevWidth, EntryHeight, HeaderGumpID );

			x += PrevWidth + OffsetSize;

			if ( !OldStyle )
				AddImageTiled( x - (OldStyle ? OffsetSize : 0), y, emptyWidth + (OldStyle ? OffsetSize * 2 : 0), EntryHeight, HeaderGumpID );

			x += emptyWidth + OffsetSize;

			if ( !OldStyle )
				AddImageTiled( x, y, NextWidth, EntryHeight, HeaderGumpID );

			for ( int i = 0; i < m_Groups.Length; ++i )
			{
				x = BorderSize + OffsetSize;
				y += EntryHeight + OffsetSize;

				RaceSkillsGumpGroup group = m_Groups[i];

				AddImageTiled( x, y, PrevWidth, EntryHeight, HeaderGumpID );

				if ( group == selected )
					AddButton( x + PrevOffsetX, y + PrevOffsetY, 0x15E2, 0x15E6, GetButtonID( 0, i ), GumpButtonType.Reply, 0 );
				else
					AddButton( x + PrevOffsetX, y + PrevOffsetY, 0x15E1, 0x15E5, GetButtonID( 0, i ), GumpButtonType.Reply, 0 );

				x += PrevWidth + OffsetSize;

				x -= (OldStyle ? OffsetSize : 0);

				AddImageTiled( x, y, emptyWidth + (OldStyle ? OffsetSize * 2 : 0), EntryHeight, EntryGumpID );
				AddLabel( x + TextOffsetX, y, TextHue, group.Name );

				x += emptyWidth + (OldStyle ? OffsetSize * 2 : 0);
				x += OffsetSize;

				if ( SetGumpID != 0 )
					AddImageTiled( x, y, SetWidth, EntryHeight, SetGumpID );

				if ( group == selected )
				{
					int indentMaskX = BorderSize;
					int indentMaskY = y + EntryHeight + OffsetSize;

					for ( int j = 0; j < group.Skills.Length; ++j )
					{
						Skill sk = target.Skills[group.Skills[j]];

						x = BorderSize + OffsetSize;
						y += EntryHeight + OffsetSize;

						x += OffsetSize;
						x += IndentWidth;

						AddImageTiled( x, y, PrevWidth, EntryHeight, HeaderGumpID );

						AddButton( x + PrevOffsetX, y + PrevOffsetY, 0x15E1, 0x15E5, GetButtonID( 1, j ), GumpButtonType.Reply, 0 );

						x += PrevWidth + OffsetSize;

						x -= (OldStyle ? OffsetSize : 0);

						AddImageTiled( x, y, emptyWidth + (OldStyle ? OffsetSize * 2 : 0) - OffsetSize - IndentWidth, EntryHeight, EntryGumpID );
						AddLabel( x + TextOffsetX, y, TextHue, sk == null ? "(null)" : sk.Name );

						x += emptyWidth + (OldStyle ? OffsetSize * 2 : 0) - OffsetSize - IndentWidth;
						x += OffsetSize;

						if ( SetGumpID != 0 )
							AddImageTiled( x, y, SetWidth, EntryHeight, SetGumpID );

						if ( sk != null )
						{
							y += 1;
							x -= OffsetSize;
							x -= 1;
							x -= 50;

							AddImageTiled( x, y, 50, EntryHeight - 2, OffsetGumpID );

							x += 1;
							y += 1;

							AddImageTiled( x, y, 48, EntryHeight - 4, EntryGumpID );

							AddLabelCropped( x + TextOffsetX, y - 1, 48 - TextOffsetX, EntryHeight - 3, TextHue, sk.Base.ToString( "F1" ) );

							y -= 2;
						}
					}

					AddImageTiled( indentMaskX, indentMaskY, IndentWidth + OffsetSize, (group.Skills.Length * (EntryHeight + OffsetSize)) - (i < (m_Groups.Length - 1) ? OffsetSize : 0), BackGumpID + 4 );
				}
			}
		}
	}
	public class RaceSkillsGumpGroup
	{
		private string m_Name;
		private SkillName[] m_Skills;

		public string Name{ get{ return m_Name; } }
		public SkillName[] Skills{ get{ return m_Skills; } }

		public RaceSkillsGumpGroup( string name, SkillName[] skills )
		{
			m_Name = name;
			m_Skills = skills;

			Array.Sort( m_Skills, new SkillNameComparer() );
		}

		private class SkillNameComparer : IComparer
		{
			public SkillNameComparer()
			{
			}

			public int Compare( object x, object y )
			{
				SkillName a = (SkillName)x;
				SkillName b = (SkillName)y;

				string aName = SkillInfo.Table[(int)a].Name;
				string bName = SkillInfo.Table[(int)b].Name;

				return aName.CompareTo( bName );
			}
		}

		private static RaceSkillsGumpGroup[] m_Groups = new RaceSkillsGumpGroup[]
			{
				new RaceSkillsGumpGroup( "Crafting", new SkillName[]         //Re Enabled Crafting Skill Group 2-26-06 {RE} 
				{
					SkillName.Alchemy,
					SkillName.Blacksmith,
					SkillName.Cartography,
					SkillName.Carpentry,
					SkillName.Cooking,
					SkillName.Fletching,
					SkillName.Inscribe,
					SkillName.Tailoring,
					SkillName.Tinkering,
					SkillName.Imbuing
				} ),
				new RaceSkillsGumpGroup( "Bardic", new SkillName[]
				{
					SkillName.Discordance,
					SkillName.Musicianship,
					SkillName.Peacemaking,
					SkillName.Provocation
				} ),
				new RaceSkillsGumpGroup( "Magical", new SkillName[]
				{
					SkillName.Chivalry,
					SkillName.EvalInt,
					SkillName.Magery,
					SkillName.MagicResist,
					SkillName.Meditation,
					SkillName.Necromancy,
					SkillName.SpiritSpeak,
					SkillName.Spellweaving,
					SkillName.Mysticism
				} ),
				new RaceSkillsGumpGroup( "Miscellaneous", new SkillName[]
				{
					SkillName.Camping,
					SkillName.Fishing,
					SkillName.Focus,
					SkillName.Healing,
					SkillName.Herding,
					SkillName.Lockpicking,
					SkillName.Lumberjacking,
					SkillName.Mining,
					SkillName.Snooping,
					SkillName.Veterinary
				} ),
				new RaceSkillsGumpGroup( "Combat Ratings", new SkillName[]
				{
					SkillName.Archery,
					SkillName.Fencing,
					SkillName.Macing,
					SkillName.Parry,
					SkillName.Swords,
					SkillName.Tactics,
					SkillName.Wrestling,
					SkillName.Ninjitsu,
					SkillName.Bushido,
					SkillName.Throwing
				} ),
				new RaceSkillsGumpGroup( "Actions", new SkillName[]
				{
					SkillName.AnimalTaming,
					SkillName.Begging,
					SkillName.DetectHidden,
					SkillName.Hiding,
					SkillName.RemoveTrap,
					SkillName.Poisoning,
					SkillName.Stealing,
					SkillName.Stealth,
					SkillName.Tracking
				} ),
				new RaceSkillsGumpGroup( "Lore & Knowledge", new SkillName[]
				{
					SkillName.Anatomy,
					SkillName.AnimalLore,
					SkillName.ArmsLore,
					SkillName.Forensics,
					SkillName.ItemID,
					SkillName.TasteID
				} )
			};

		public static RaceSkillsGumpGroup[] Groups
		{
			get{ return m_Groups; }
		}
	}
} 
