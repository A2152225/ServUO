using System;
using System.Collections;
using Server;
using Server.Prompts;
using Server.Mobiles;
using Server.ContextMenus;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Multis;
using Server.Regions;


namespace Server.Items
{
        [FlipableAttribute( 0xE41, 0xE40 )]
        public class ResourceBox : BaseContainer
        {
//////////WOOD//////////
                private int m_RegularLog;
                //private int m_AshLog;
                //private int m_CedarLog;
                //private int m_CherryLog;
                //private int m_MahoganyLog;
                //private int m_MapleLog;
                //private int m_PineLog;
                //private int m_SycamoreLog;
                //private int m_WalnutLog;
                //private int m_YewLog;
//////////WOOD//////////
//////////INGOT//////////
                private int m_IronIngot;
                private int m_DullCopperIngot;
                private int m_ShadowIronIngot;
                private int m_CopperIngot;
                private int m_BronzeIngot;
                private int m_GoldIngot;
                private int m_AgapiteIngot;
                private int m_VeriteIngot;
                private int m_ValoriteIngot;
//////////INGOT//////////
//////////GRANITE&SAND//////////
                private int m_Iron;
                private int m_DullCopper;
                private int m_ShadowIron;
                private int m_Copper;
                private int m_Bronze;
                private int m_Gold;
                private int m_Agapite;
                private int m_Verite;
                private int m_Valorite;
                private int m_Sand;
//////////GRANITE&SAND//////////
//////////LEATHER&CLOTH//////////
                private int m_Cloth;
                private int m_Leather;
                private int m_Spined;
                private int m_Horned;
                private int m_Barbed;
//////////LEATHER&CLOTH//////////
//////////REAGENTS//////////
                private int m_MandrakeRoot;
                private int m_Bloodmoss;
                private int m_BlackPearl;
                private int m_Nightshade;
                private int m_Ginseng;
                private int m_SpidersSilk;
                private int m_Garlic;
                private int m_SulfurousAsh;
                private int m_NoxCrystal;
                private int m_DaemonBlood;
                private int m_BatWing;
                private int m_PigIron;
                private int m_GraveDust;
                //private int m_DestroyingAngel;
                //private int m_PetrafiedWood;
                //private int m_SpringWater;
//////////REAGENTS//////////
//////////SCALES//////////
                private int m_RedScales;
                private int m_YellowScales;
                private int m_BlackScales;
                private int m_GreenScales;
                private int m_WhiteScales;
                private int m_BlueScales;
//////////SCALES//////////



//////////WOOD//////////
                [CommandProperty( AccessLevel.GameMaster )]
                public int RegularLog{ get{ return m_RegularLog; } set{ m_RegularLog = value; InvalidateProperties(); } }

                //[CommandProperty( AccessLevel.GameMaster )]
                //public int AshLog{ get{ return m_AshLog; } set{ m_AshLog = value; InvalidateProperties(); } }

                //[CommandProperty( AccessLevel.GameMaster )]
                //public int CedarLog{ get{ return m_CedarLog; } set{ m_CedarLog = value; InvalidateProperties(); } }

                //[CommandProperty( AccessLevel.GameMaster )]
                //public int CherryLog{ get{ return m_CherryLog; } set{ m_CherryLog = value; InvalidateProperties(); } }

                //[CommandProperty( AccessLevel.GameMaster )]
                //public int MahoganyLog{ get{ return m_MahoganyLog; } set{ m_MahoganyLog = value; InvalidateProperties(); } }

                //[CommandProperty( AccessLevel.GameMaster )]
                //public int MapleLog{ get{ return m_MapleLog; } set{ m_MapleLog = value; InvalidateProperties(); } }

                //[CommandProperty( AccessLevel.GameMaster )]
                //public int SycamoreLog{ get{ return m_SycamoreLog; } set{ m_SycamoreLog = value; InvalidateProperties(); } }

                //[CommandProperty( AccessLevel.GameMaster )]
                //public int PineLog{ get{ return m_PineLog; } set{ m_PineLog = value; InvalidateProperties(); } }

                //[CommandProperty( AccessLevel.GameMaster )]
                //public int WalnutLog{ get{ return m_WalnutLog; } set{ m_WalnutLog = value; InvalidateProperties(); } }

                //[CommandProperty( AccessLevel.GameMaster )]
                //public int YewLog{ get{ return m_YewLog; } set{ m_YewLog = value; InvalidateProperties(); } }
//////////WOOD//////////

//////////INGOT//////////
                [CommandProperty( AccessLevel.GameMaster )]
                public int IronIngot{ get{ return m_IronIngot; } set{ m_IronIngot = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int DullCopperIngot{ get{ return m_DullCopperIngot; } set{ m_DullCopperIngot = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int ShadowIronIngot{ get{ return m_ShadowIronIngot; } set{ m_ShadowIronIngot = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int CopperIngot{ get{ return m_CopperIngot; } set{ m_CopperIngot = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int BronzeIngot{ get{ return m_BronzeIngot; } set{ m_BronzeIngot = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int GoldIngot{ get{ return m_GoldIngot; } set{ m_GoldIngot = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int AgapiteIngot{ get{ return m_AgapiteIngot; } set{ m_AgapiteIngot = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int VeriteIngot{ get{ return m_VeriteIngot; } set{ m_VeriteIngot = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int ValoriteIngot{ get{ return m_ValoriteIngot; } set{ m_ValoriteIngot = value; InvalidateProperties(); } }
//////////INGOT//////////

//////////GRANITE&SAND//////////
                [CommandProperty( AccessLevel.GameMaster )]
                public int IronGranite{ get{ return m_Iron; } set{ m_Iron = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int DullCopperGranite{ get{ return m_DullCopper; } set{ m_DullCopper = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int ShadowIronGranite{ get{ return m_ShadowIron; } set{ m_ShadowIron = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int CopperGranite{ get{ return m_Copper; } set{ m_Copper = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int BronzeGranite{ get{ return m_Bronze; } set{ m_Bronze = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int GoldGranite{ get{ return m_Gold; } set{ m_Gold = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int AgapiteGranite{ get{ return m_Agapite; } set{ m_Agapite = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int VeriteGranite{ get{ return m_Verite; } set{ m_Verite = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int ValoriteGranite{ get{ return m_Valorite; } set{ m_Valorite = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int Sand{ get{ return m_Sand; } set{ m_Sand = value; InvalidateProperties(); } }
//////////GRANITE&SAND//////////

//////////LEATHER&CLOTH//////////
                [CommandProperty( AccessLevel.GameMaster )]
                public int Cloth{ get{ return m_Cloth; } set{ m_Cloth = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int Leather{ get{ return m_Leather; } set{ m_Leather = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int Spined{ get{ return m_Spined; } set{ m_Spined = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int Horned{ get{ return m_Horned; } set{ m_Horned = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int Barbed{ get{ return m_Barbed; } set{ m_Barbed = value; InvalidateProperties(); } }
//////////LEATHER&CLOTH//////////

//////////REAGENTS//////////
                [CommandProperty( AccessLevel.GameMaster )]
                public int MandrakeRoot{ get{ return m_MandrakeRoot; } set{ m_MandrakeRoot = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int Bloodmoss{ get{ return m_Bloodmoss; } set{ m_Bloodmoss = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int BlackPearl{ get{ return m_BlackPearl; } set{ m_BlackPearl = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int Nightshade{ get{ return m_Nightshade; } set{ m_Nightshade = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int Ginseng{ get{ return m_Ginseng; } set{ m_Ginseng = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int SpidersSilk{ get{ return m_SpidersSilk; } set{ m_SpidersSilk = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int Garlic{ get{ return m_Garlic; } set{ m_Garlic = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int SulfurousAsh{ get{ return m_SulfurousAsh; } set{ m_SulfurousAsh = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int NoxCrystal{ get{ return m_NoxCrystal; } set{ m_NoxCrystal = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int DaemonBlood{ get{ return m_DaemonBlood; } set{ m_DaemonBlood = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int BatWing{ get{ return m_BatWing; } set{ m_BatWing = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int PigIron{ get{ return m_PigIron; } set{ m_PigIron = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int GraveDust{ get{ return m_GraveDust; } set{ m_GraveDust = value; InvalidateProperties(); } }

                //[CommandProperty( AccessLevel.GameMaster )]
                //public int DestroyingAngel{ get{ return m_DestroyingAngel; } set{ m_DestroyingAngel = value; InvalidateProperties(); } }

                //[CommandProperty( AccessLevel.GameMaster )]
                //public int PetrafiedWood{ get{ return m_PetrafiedWood; } set{ m_PetrafiedWood = value; InvalidateProperties(); } }

                //[CommandProperty( AccessLevel.GameMaster )]
                //public int SpringWater{ get{ return m_SpringWater; } set{ m_SpringWater = value; InvalidateProperties(); } }
//////////REAGENTS//////////

//////////SCALES//////////
                [CommandProperty( AccessLevel.GameMaster )]
                public int RedScales{ get{ return m_RedScales; } set{ m_RedScales = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int YellowScales{ get{ return m_YellowScales; } set{ m_YellowScales = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int BlackScales{ get{ return m_BlackScales; } set{ m_BlackScales = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int GreenScales{ get{ return m_GreenScales; } set{ m_GreenScales = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int WhiteScales{ get{ return m_WhiteScales; } set{ m_WhiteScales = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int BlueScales{ get{ return m_BlueScales; } set{ m_BlueScales = value; InvalidateProperties(); } }
//////////SCALES//////////

                [Constructable]
                public ResourceBox() : base( 0xE41 )
                {
                        Movable = true;
                        Weight = 100.0;
                        Hue = 0x488;
                        Name = "Resource Box";
                }

     		public override bool OnDragDrop( Mobile from, Item dropped ) 
      		{ 
                        if ( Movable )
                        {
                                from.SendMessage( "You haven't locked it down!" );
                                return false;
                        }
                        else if ( dropped is Sand )
                        {
                        	Sand g = ( Sand ) dropped;

                                        if ( Sand <= 59999 )
                                        {
						int need = 60000 - Sand;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new Sand( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too much sand, 1 sand is returned back to you." );
							else
								from.SendMessage( "You have given the box too much sand, "+ diff +" sand are returned back to you." );


                                               		g.Delete();
                                                	Sand += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the sand to the box." );
                                               		g.Delete();
                                                	Sand += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is Log )
                        {
                        	Log g = ( Log ) dropped;

                                        if ( RegularLog <= 59999 )
                                        {
						int need = 60000 - RegularLog;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new Log( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many logs, 1 log is returned back to you." );
							else
								from.SendMessage( "You have given the box too many logs, "+ diff +" logs are returned back to you." );


                                               		g.Delete();
                                                	RegularLog += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the log to the box." );
                                               		g.Delete();
                                                	RegularLog += g.Amount;
						}
                                        }
                                return false;
                        }
/*
                        else if ( dropped is AshLog )
                        {
                        	AshLog g = ( AshLog ) dropped;

                                        if ( AshLog <= 59999 )
                                        {
						int need = 60000 - AshLog;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new AshLog( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many logs, 1 log is returned back to you." );
							else
								from.SendMessage( "You have given the box too many logs, "+ diff +" logs are returned back to you." );


                                               		g.Delete();
                                                	AshLog += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the log to the box." );
                                               		g.Delete();
                                                	AshLog += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is CherryLog )
                        {
                        	CherryLog g = ( CherryLog ) dropped;

                                        if ( CherryLog <= 59999 )
                                        {
						int need = 60000 - CherryLog;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new CherryLog( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many logs, 1 log is returned back to you." );
							else
								from.SendMessage( "You have given the box too many logs, "+ diff +" logs are returned back to you." );


                                               		g.Delete();
                                                	CherryLog += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the log to the box." );
                                               		g.Delete();
                                                	CherryLog += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is CedarLog )
                        {
                        	CedarLog g = ( CedarLog ) dropped;

                                        if ( CedarLog <= 59999 )
                                        {
						int need = 60000 - CedarLog;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new CedarLog( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many logs, 1 log is returned back to you." );
							else
								from.SendMessage( "You have given the box too many logs, "+ diff +" logs are returned back to you." );


                                               		g.Delete();
                                                	CedarLog += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the log to the box." );
                                               		g.Delete();
                                                	CedarLog += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is MahoganyLog )
                        {
                        	MahoganyLog g = ( MahoganyLog ) dropped;

                                        if ( MahoganyLog <= 59999 )
                                        {
						int need = 60000 - MahoganyLog;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new MahoganyLog( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many logs, 1 log is returned back to you." );
							else
								from.SendMessage( "You have given the box too many logs, "+ diff +" logs are returned back to you." );


                                               		g.Delete();
                                                	MahoganyLog += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the log to the box." );
                                               		g.Delete();
                                                	MahoganyLog += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is MapleLog )
                        {
                        	MapleLog g = ( MapleLog ) dropped;

                                        if ( MapleLog <= 59999 )
                                        {
						int need = 60000 - MapleLog;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new MapleLog( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many logs, 1 log is returned back to you." );
							else
								from.SendMessage( "You have given the box too many logs, "+ diff +" logs are returned back to you." );


                                               		g.Delete();
                                                	MapleLog += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the log to the box." );
                                               		g.Delete();
                                                	MapleLog += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is PineLog )
                        {
                        	PineLog g = ( PineLog ) dropped;

                                        if ( PineLog <= 59999 )
                                        {
						int need = 60000 - PineLog;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new PineLog( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many logs, 1 log is returned back to you." );
							else
								from.SendMessage( "You have given the box too many logs, "+ diff +" logs are returned back to you." );


                                               		g.Delete();
                                                	PineLog += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the log to the box." );
                                               		g.Delete();
                                                	PineLog += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is SycamoreLog )
                        {
                        	SycamoreLog g = ( SycamoreLog ) dropped;

                                        if ( SycamoreLog <= 59999 )
                                        {
						int need = 60000 - SycamoreLog;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new SycamoreLog( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many logs, 1 log is returned back to you." );
							else
								from.SendMessage( "You have given the box too many logs, "+ diff +" logs are returned back to you." );


                                               		g.Delete();
                                                	SycamoreLog += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the log to the box." );
                                               		g.Delete();
                                                	SycamoreLog += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is WalnutLog )
                        {
                        	WalnutLog g = ( WalnutLog ) dropped;

                                        if ( WalnutLog <= 59999 )
                                        {
						int need = 60000 - WalnutLog;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new WalnutLog( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many logs, 1 log is returned back to you." );
							else
								from.SendMessage( "You have given the box too many logs, "+ diff +" logs are returned back to you." );


                                               		g.Delete();
                                                	WalnutLog += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the log to the box." );
                                               		g.Delete();
                                                	WalnutLog += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is YewLog )
                        {
                        	YewLog g = ( YewLog ) dropped;

                                        if ( YewLog <= 59999 )
                                        {
						int need = 60000 - YewLog;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new YewLog( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many logs, 1 log is returned back to you." );
							else
								from.SendMessage( "You have given the box too many logs, "+ diff +" logs are returned back to you." );


                                               		g.Delete();
                                                	YewLog += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the log to the box." );
                                               		g.Delete();
                                                	YewLog += g.Amount;
						}
                                        }
                                return false;
                        }
*/
                        else if ( dropped is IronIngot )
                        {
                        	IronIngot g = ( IronIngot ) dropped;

                                        if ( IronIngot <= 59999 )
                                        {
						int need = 60000 - IronIngot;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new IronIngot( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many ingots, 1 ingot is returned back to you." );
							else
								from.SendMessage( "You have given the box too many ingots, "+ diff +" ingots are returned back to you." );


                                               		g.Delete();
                                                	IronIngot += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the ingot to the box." );
                                               		g.Delete();
                                                	IronIngot += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is DullCopperIngot )
                        {
                        	DullCopperIngot g = ( DullCopperIngot ) dropped;

                                        if ( DullCopperIngot <= 59999 )
                                        {
						int need = 60000 - DullCopperIngot;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new DullCopperIngot( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many ingots, 1 ingot is returned back to you." );
							else
								from.SendMessage( "You have given the box too many ingots, "+ diff +" ingots are returned back to you." );


                                               		g.Delete();
                                                	DullCopperIngot += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the ingot to the box." );
                                               		g.Delete();
                                                	DullCopperIngot += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is ShadowIronIngot )
                        {
                        	ShadowIronIngot g = ( ShadowIronIngot ) dropped;

                                        if ( ShadowIronIngot <= 59999 )
                                        {
						int need = 60000 - ShadowIronIngot;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new ShadowIronIngot( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many ingots, 1 ingot is returned back to you." );
							else
								from.SendMessage( "You have given the box too many ingots, "+ diff +" ingots are returned back to you." );


                                               		g.Delete();
                                                	ShadowIronIngot += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the ingot to the box." );
                                               		g.Delete();
                                                	ShadowIronIngot += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is CopperIngot )
                        {
                        	CopperIngot g = ( CopperIngot ) dropped;

                                        if ( CopperIngot <= 59999 )
                                        {
						int need = 60000 - CopperIngot;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new CopperIngot( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many ingots, 1 ingot is returned back to you." );
							else
								from.SendMessage( "You have given the box too many ingots, "+ diff +" ingots are returned back to you." );


                                               		g.Delete();
                                                	CopperIngot += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the ingot to the box." );
                                               		g.Delete();
                                                	CopperIngot += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is BronzeIngot )
                        {
                        	BronzeIngot g = ( BronzeIngot ) dropped;

                                        if ( BronzeIngot <= 59999 )
                                        {
						int need = 60000 - BronzeIngot;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new BronzeIngot( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many ingots, 1 ingot is returned back to you." );
							else
								from.SendMessage( "You have given the box too many ingots, "+ diff +" ingots are returned back to you." );


                                               		g.Delete();
                                                	BronzeIngot += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the ingot to the box." );
                                               		g.Delete();
                                                	BronzeIngot += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is GoldIngot )
                        {
                        	GoldIngot g = ( GoldIngot ) dropped;

                                        if ( GoldIngot <= 59999 )
                                        {
						int need = 60000 - GoldIngot;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new GoldIngot( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many ingots, 1 ingot is returned back to you." );
							else
								from.SendMessage( "You have given the box too many ingots, "+ diff +" ingots are returned back to you." );


                                               		g.Delete();
                                                	GoldIngot += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the ingot to the box." );
                                               		g.Delete();
                                                	GoldIngot += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is AgapiteIngot )
                        {
                        	AgapiteIngot g = ( AgapiteIngot ) dropped;

                                        if ( AgapiteIngot <= 59999 )
                                        {
						int need = 60000 - AgapiteIngot;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new AgapiteIngot( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many ingots, 1 ingot is returned back to you." );
							else
								from.SendMessage( "You have given the box too many ingots, "+ diff +" ingots are returned back to you." );


                                               		g.Delete();
                                                	AgapiteIngot += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the ingot to the box." );
                                               		g.Delete();
                                                	AgapiteIngot += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is VeriteIngot )
                        {
                        	VeriteIngot g = ( VeriteIngot ) dropped;

                                        if ( VeriteIngot <= 59999 )
                                        {
						int need = 60000 - VeriteIngot;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new VeriteIngot( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many ingots, 1 ingot is returned back to you." );
							else
								from.SendMessage( "You have given the box too many ingots, "+ diff +" ingots are returned back to you." );


                                               		g.Delete();
                                                	VeriteIngot += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the ingot to the box." );
                                               		g.Delete();
                                                	VeriteIngot += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is ValoriteIngot )
                        {
                        	ValoriteIngot g = ( ValoriteIngot ) dropped;

                                        if ( ValoriteIngot <= 59999 )
                                        {
						int need = 60000 - ValoriteIngot;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new ValoriteIngot( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many ingots, 1 ingot is returned back to you." );
							else
								from.SendMessage( "You have given the box too many ingots, "+ diff +" ingots are returned back to you." );


                                               		g.Delete();
                                                	ValoriteIngot += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the ingot to the box." );
                                               		g.Delete();
                                                	ValoriteIngot += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is Granite )
                        {
                        	Granite g = ( Granite ) dropped;

				if ( IronGranite >= 60000 )
					from.SendMessage( "That granite type is too full to add more." );
				else
				{
				IronGranite += 1;
				g.Delete();
                                from.SendMessage( "You add the granite to the box." );
				}
                                return false;
                        }
                        else if ( dropped is DullCopperGranite )
                        {
                        	DullCopperGranite g = ( DullCopperGranite ) dropped;

				if ( DullCopperGranite >= 60000 )
					from.SendMessage( "That granite type is too full to add more." );
				else
				{
				DullCopperGranite += 1;
				g.Delete();
                                from.SendMessage( "You add the granite to the box." );
				}
                                return false;
                        }
                        else if ( dropped is ShadowIronGranite )
                        {
                        	ShadowIronGranite g = ( ShadowIronGranite ) dropped;

				if ( ShadowIronGranite >= 60000 )
					from.SendMessage( "That granite type is too full to add more." );
				else
				{
				ShadowIronGranite += 1;
				g.Delete();
                                from.SendMessage( "You add the granite to the box." );
				}
                                return false;
                        }
                        else if ( dropped is CopperGranite )
                        {
                        	CopperGranite g = ( CopperGranite ) dropped;

				if ( CopperGranite >= 60000 )
					from.SendMessage( "That granite type is too full to add more." );
				else
				{
				CopperGranite += 1;
				g.Delete();
                                from.SendMessage( "You add the granite to the box." );
				}
                                return false;
                        }
                        else if ( dropped is BronzeGranite )
                        {
                        	BronzeGranite g = ( BronzeGranite ) dropped;

				if ( BronzeGranite >= 60000 )
					from.SendMessage( "That granite type is too full to add more." );
				else
				{
				BronzeGranite += 1;
				g.Delete();
                                from.SendMessage( "You add the granite to the box." );
				}
                                return false;
                        }
                        else if ( dropped is GoldGranite )
                        {
                        	GoldGranite g = ( GoldGranite ) dropped;

				if ( GoldGranite >= 60000 )
					from.SendMessage( "That granite type is too full to add more." );
				else
				{
				GoldGranite += 1;
				g.Delete();
                                from.SendMessage( "You add the granite to the box." );
				}
                                return false;
                        }
                        else if ( dropped is AgapiteGranite )
                        {
                        	AgapiteGranite g = ( AgapiteGranite ) dropped;

				if ( AgapiteGranite >= 60000 )
					from.SendMessage( "That granite type is too full to add more." );
				else
				{
				AgapiteGranite += 1;
				g.Delete();
                                from.SendMessage( "You add the granite to the box." );
				}
                                return false;
                        }
                        else if ( dropped is VeriteGranite )
                        {
                        	VeriteGranite g = ( VeriteGranite ) dropped;

				if ( VeriteGranite >= 60000 )
					from.SendMessage( "That granite type is too full to add more." );
				else
				{
				VeriteGranite += 1;
				g.Delete();
                                from.SendMessage( "You add the granite to the box." );
				}
                                return false;
                        }
                        else if ( dropped is ValoriteGranite )
                        {
                        	ValoriteGranite g = ( ValoriteGranite ) dropped;

				if ( ValoriteGranite >= 60000 )
					from.SendMessage( "That granite type is too full to add more." );
				else
				{
				ValoriteGranite += 1;
				g.Delete();
                                from.SendMessage( "You add the granite to the box." );
				}
                                return false;
                        }
                        else if ( dropped is Cloth )
                        {
                        	Cloth g = ( Cloth ) dropped;

                                        if ( Cloth <= 59999 )
                                        {
						int need = 60000 - Cloth;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new Cloth( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many cloths, 1 cloth is returned back to you." );
							else
								from.SendMessage( "You have given the box too many cloths, "+ diff +" cloths are returned back to you." );


                                               		g.Delete();
                                                	Cloth += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the cloth to the box." );
                                               		g.Delete();
                                                	Cloth += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is Leather )
                        {
                        	Leather g = ( Leather ) dropped;

                                        if ( Leather <= 59999 )
                                        {
						int need = 60000 - Leather;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new Leather( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many leather, 1 leather is returned back to you." );
							else
								from.SendMessage( "You have given the box too many leather, "+ diff +" leather are returned back to you." );


                                               		g.Delete();
                                                	Leather += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the leather to the box." );
                                               		g.Delete();
                                                	Leather += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is SpinedLeather )
                        {
                        	SpinedLeather g = ( SpinedLeather ) dropped;

                                        if ( Spined <= 59999 )
                                        {
						int need = 60000 - Spined;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new SpinedLeather( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many leather, 1 leather is returned back to you." );
							else
								from.SendMessage( "You have given the box too many leather, "+ diff +" leather are returned back to you." );


                                               		g.Delete();
                                                	Spined += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the leather to the box." );
                                               		g.Delete();
                                                	Spined += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is HornedLeather )
                        {
                        	HornedLeather g = ( HornedLeather ) dropped;

                                        if ( Horned <= 59999 )
                                        {
						int need = 60000 - Horned;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new HornedLeather( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many leather, 1 leather is returned back to you." );
							else
								from.SendMessage( "You have given the box too many leather, "+ diff +" leather are returned back to you." );


                                               		g.Delete();
                                                	Horned += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the leather to the box." );
                                               		g.Delete();
                                                	Horned += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is BarbedLeather )
                        {
                        	BarbedLeather g = ( BarbedLeather ) dropped;

                                        if ( Barbed <= 59999 )
                                        {
						int need = 60000 - Barbed;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new BarbedLeather( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many leather, 1 leather is returned back to you." );
							else
								from.SendMessage( "You have given the box too many leather, "+ diff +" leather are returned back to you." );


                                               		g.Delete();
                                                	Barbed += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the leather to the box." );
                                               		g.Delete();
                                                	Barbed += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is MandrakeRoot )
                        {
                        	MandrakeRoot g = ( MandrakeRoot ) dropped;

                                        if ( MandrakeRoot <= 59999 )
                                        {
						int need = 60000 - MandrakeRoot;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new MandrakeRoot( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagents are returned back to you." );


                                               		g.Delete();
                                                	MandrakeRoot += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the reagents to the box." );
                                               		g.Delete();
                                                	MandrakeRoot += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is Bloodmoss )
                        {
                        	Bloodmoss g = ( Bloodmoss ) dropped;

                                        if ( Bloodmoss <= 59999 )
                                        {
						int need = 60000 - Bloodmoss;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new Bloodmoss( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagents are returned back to you." );


                                               		g.Delete();
                                                	Bloodmoss += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the reagents to the box." );
                                               		g.Delete();
                                                	Bloodmoss += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is BlackPearl )
                        {
                        	BlackPearl g = ( BlackPearl ) dropped;

                                        if ( BlackPearl <= 59999 )
                                        {
						int need = 60000 - BlackPearl;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new BlackPearl( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagents are returned back to you." );


                                               		g.Delete();
                                                	BlackPearl += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the reagents to the box." );
                                               		g.Delete();
                                                	BlackPearl += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is Nightshade )
                        {
                        	Nightshade g = ( Nightshade ) dropped;

                                        if ( Nightshade <= 59999 )
                                        {
						int need = 60000 - Nightshade;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new Nightshade( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagents are returned back to you." );


                                               		g.Delete();
                                                	Nightshade += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the reagents to the box." );
                                               		g.Delete();
                                                	Nightshade += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is Ginseng )
                        {
                        	Ginseng g = ( Ginseng ) dropped;

                                        if ( Ginseng <= 59999 )
                                        {
						int need = 60000 - Ginseng;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new Ginseng( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagents are returned back to you." );


                                               		g.Delete();
                                                	Ginseng += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the reagents to the box." );
                                               		g.Delete();
                                                	Ginseng += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is Garlic )
                        {
                        	Garlic g = ( Garlic ) dropped;

                                        if ( Garlic <= 59999 )
                                        {
						int need = 60000 - Garlic;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new Garlic( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagents are returned back to you." );


                                               		g.Delete();
                                                	Garlic += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the reagents to the box." );
                                               		g.Delete();
                                                	Garlic += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is SpidersSilk )
                        {
                        	SpidersSilk g = ( SpidersSilk ) dropped;

                                        if ( SpidersSilk <= 59999 )
                                        {
						int need = 60000 - SpidersSilk;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new SpidersSilk( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagents are returned back to you." );


                                               		g.Delete();
                                                	SpidersSilk += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the reagents to the box." );
                                               		g.Delete();
                                                	SpidersSilk += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is SulfurousAsh )
                        {
                        	SulfurousAsh g = ( SulfurousAsh ) dropped;

                                        if ( SulfurousAsh <= 59999 )
                                        {
						int need = 60000 - SulfurousAsh;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new SulfurousAsh( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagents are returned back to you." );


                                               		g.Delete();
                                                	SulfurousAsh += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the reagents to the box." );
                                               		g.Delete();
                                                	SulfurousAsh += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is NoxCrystal )
                        {
                        	NoxCrystal g = ( NoxCrystal ) dropped;

                                        if ( NoxCrystal <= 59999 )
                                        {
						int need = 60000 - NoxCrystal;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new NoxCrystal( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagents are returned back to you." );


                                               		g.Delete();
                                                	NoxCrystal += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the reagents to the box." );
                                               		g.Delete();
                                                	NoxCrystal += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is PigIron )
                        {
                        	PigIron g = ( PigIron ) dropped;

                                        if ( PigIron <= 59999 )
                                        {
						int need = 60000 - PigIron;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new PigIron( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagents are returned back to you." );


                                               		g.Delete();
                                                	PigIron += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the reagents to the box." );
                                               		g.Delete();
                                                	PigIron += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is BatWing )
                        {
                        	BatWing g = ( BatWing ) dropped;

                                        if ( BatWing <= 59999 )
                                        {
						int need = 60000 - BatWing;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new BatWing( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagents are returned back to you." );


                                               		g.Delete();
                                                	BatWing += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the reagents to the box." );
                                               		g.Delete();
                                                	BatWing += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is DaemonBlood )
                        {
                        	DaemonBlood g = ( DaemonBlood ) dropped;

                                        if ( DaemonBlood <= 59999 )
                                        {
						int need = 60000 - DaemonBlood;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new DaemonBlood( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagents are returned back to you." );


                                               		g.Delete();
                                                	DaemonBlood += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the reagents to the box." );
                                               		g.Delete();
                                                	DaemonBlood += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is GraveDust )
                        {
                        	GraveDust g = ( GraveDust ) dropped;

                                        if ( GraveDust <= 59999 )
                                        {
						int need = 60000 - GraveDust;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new GraveDust( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagents are returned back to you." );


                                               		g.Delete();
                                                	GraveDust += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the reagents to the box." );
                                               		g.Delete();
                                                	GraveDust += g.Amount;
						}
                                        }
                                return false;
                        }
/*
                        else if ( dropped is DestroyingAngel )
                        {
                        	DestroyingAngel g = ( DestroyingAngel ) dropped;

                                        if ( DestroyingAngel <= 59999 )
                                        {
						int need = 60000 - DestroyingAngel;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new DestroyingAngel( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagents are returned back to you." );


                                               		g.Delete();
                                                	DestroyingAngel += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the reagents to the box." );
                                               		g.Delete();
                                                	DestroyingAngel += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is PetrafiedWood )
                        {
                        	PetrafiedWood g = ( PetrafiedWood ) dropped;

                                        if ( PetrafiedWood <= 59999 )
                                        {
						int need = 60000 - PetrafiedWood;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new PetrafiedWood( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagents are returned back to you." );


                                               		g.Delete();
                                                	PetrafiedWood += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the reagents to the box." );
                                               		g.Delete();
                                                	PetrafiedWood += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is SpringWater )
                        {
                        	SpringWater g = ( SpringWater ) dropped;

                                        if ( SpringWater <= 59999 )
                                        {
						int need = 60000 - SpringWater;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new SpringWater( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagents are returned back to you." );


                                               		g.Delete();
                                                	SpringWater += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the reagents to the box." );
                                               		g.Delete();
                                                	SpringWater += g.Amount;
						}
                                        }
                                return false;
                        }
*/
                        else if ( dropped is RedScales )
                        {
                        	RedScales g = ( RedScales ) dropped;

                                        if ( RedScales <= 59999 )
                                        {
						int need = 60000 - RedScales;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new RedScales( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many scales, 1 scale is returned back to you." );
							else
								from.SendMessage( "You have given the box too many scales, "+ diff +" scales are returned back to you." );


                                               		g.Delete();
                                                	RedScales += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the scales to the box." );
                                               		g.Delete();
                                                	RedScales += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is YellowScales )
                        {
                        	YellowScales g = ( YellowScales ) dropped;

                                        if ( YellowScales <= 59999 )
                                        {
						int need = 60000 - YellowScales;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new YellowScales( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many scales, 1 scale is returned back to you." );
							else
								from.SendMessage( "You have given the box too many scales, "+ diff +" scales are returned back to you." );


                                               		g.Delete();
                                                	YellowScales += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the scales to the box." );
                                               		g.Delete();
                                                	YellowScales += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is BlackScales )
                        {
                        	BlackScales g = ( BlackScales ) dropped;

                                        if ( BlackScales <= 59999 )
                                        {
						int need = 60000 - BlackScales;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new BlackScales( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many scales, 1 scale is returned back to you." );
							else
								from.SendMessage( "You have given the box too many scales, "+ diff +" scales are returned back to you." );


                                               		g.Delete();
                                                	BlackScales += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the scales to the box." );
                                               		g.Delete();
                                                	BlackScales += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is GreenScales )
                        {
                        	GreenScales g = ( GreenScales ) dropped;

                                        if ( GreenScales <= 59999 )
                                        {
						int need = 60000 - GreenScales;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new GreenScales( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many scales, 1 scale is returned back to you." );
							else
								from.SendMessage( "You have given the box too many scales, "+ diff +" scales are returned back to you." );


                                               		g.Delete();
                                                	GreenScales += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the scales to the box." );
                                               		g.Delete();
                                                	GreenScales += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is WhiteScales )
                        {
                        	WhiteScales g = ( WhiteScales ) dropped;

                                        if ( WhiteScales <= 59999 )
                                        {
						int need = 60000 - WhiteScales;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new WhiteScales( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many scales, 1 scale is returned back to you." );
							else
								from.SendMessage( "You have given the box too many scales, "+ diff +" scales are returned back to you." );


                                               		g.Delete();
                                                	WhiteScales += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the scales to the box." );
                                               		g.Delete();
                                                	WhiteScales += g.Amount;
						}
                                        }
                                return false;
                        }
                        else if ( dropped is BlueScales )
                        {
                        	BlueScales g = ( BlueScales ) dropped;

                                        if ( BlueScales <= 59999 )
                                        {
						int need = 60000 - BlueScales;

						if ( g.Amount >= need )
						{
							if ( g.Amount >= 60000 )
							{
							int diff = ( g.Amount - need );
							int amount = ( g.Amount - diff );
							from.AddToBackpack( new BlueScales( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many scales, 1 scale is returned back to you." );
							else
								from.SendMessage( "You have given the box too many scales, "+ diff +" scales are returned back to you." );


                                               		g.Delete();
                                                	BlueScales += amount;
							}
						}
						else
						{
                                			from.SendMessage( "You add the scales to the box." );
                                               		g.Delete();
                                                	BlueScales += g.Amount;
						}
                                        }
                                return false;
                        }
			else
			{
				from.SendMessage( "That is not a resource!" );
				return false;
			}

         		//return false; 
      		} 

                public override void OnDoubleClick( Mobile from )
                {
                        if ( Movable )
                        {
                                from.SendMessage( "You haven't locked it down!" );
                                return;
                        }
                        if ( !from.InRange( GetWorldLocation(), 2 ) )
                                from.LocalOverheadMessage( Network.MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
                        else if ( from is PlayerMobile )
                                from.SendGump( new ChooseResourceGump( (PlayerMobile)from, this ) );
                }

                public void BeginCombine( Mobile from )
                {
                                from.Target = new ResourceBoxTarget( this );
                }

                public void EndCombine( Mobile from, object o )
                {
                        if ( o is Item && ((Item)o).IsChildOf( from.Backpack ) )
                        {
                                if ( o is Sand )
                                {
                                        if ( Sand <= 59999 )
                                        {
						int need = 60000 - Sand;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60000 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new Sand( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too much sand, 1 sand is returned back to you." );
							else
								from.SendMessage( "You have given the box too much sand, "+ diff +" sand are returned back to you." );


                                               		((Item)o).Delete();
                                                	Sand += amount;
                                                	from.SendGump( new WoodBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	Sand += ((Item)o).Amount;
                                                	from.SendGump( new WoodBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is Log )
                                {
                                        if ( RegularLog <= 59999 )
                                        {
						int need = 60000 - RegularLog;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60000 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new Log( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many logs, 1 log is returned back to you." );
							else
								from.SendMessage( "You have given the box too many logs, "+ diff +" logs are returned back to you." );


                                               		((Item)o).Delete();
                                                	RegularLog += amount;
                                                	from.SendGump( new WoodBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	RegularLog += ((Item)o).Amount;
                                                	from.SendGump( new WoodBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
/*
                                if ( o is CedarLog )
                                {
                                        if ( CedarLog <= 59999 )
                                        {
						int need = 60000 - CedarLog;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60000 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new CedarLog( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many logs, 1 log is returned back to you." );
							else
								from.SendMessage( "You have given the box too many logs, "+ diff +" logs are returned back to you." );


                                               		((Item)o).Delete();
                                                	CedarLog += amount;
                                                	from.SendGump( new WoodBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	CedarLog += ((Item)o).Amount;
                                                	from.SendGump( new WoodBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is CherryLog )
                                {
                                        if ( CherryLog <= 59999 )
                                        {
						int need = 60000 - CherryLog;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60000 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new CherryLog( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many logs, 1 log is returned back to you." );
							else
								from.SendMessage( "You have given the box too many logs, "+ diff +" logs are returned back to you." );


                                               		((Item)o).Delete();
                                                	CherryLog += amount;
                                                	from.SendGump( new WoodBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	CherryLog += ((Item)o).Amount;
                                                	from.SendGump( new WoodBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is MahoganyLog )
                                {
                                        if ( MahoganyLog <= 59999 )
                                        {
						int need = 60000 - MahoganyLog;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60000 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new MahoganyLog( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many logs, 1 log is returned back to you." );
							else
								from.SendMessage( "You have given the box too many logs, "+ diff +" logs are returned back to you." );


                                               		((Item)o).Delete();
                                                	MahoganyLog += amount;
                                                	from.SendGump( new WoodBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	MahoganyLog += ((Item)o).Amount;
                                                	from.SendGump( new WoodBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is MapleLog )
                                {
                                        if ( MapleLog <= 59999 )
                                        {
						int need = 60000 - MapleLog;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60000 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new MapleLog( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many logs, 1 log is returned back to you." );
							else
								from.SendMessage( "You have given the box too many logs, "+ diff +" logs are returned back to you." );


                                               		((Item)o).Delete();
                                                	MapleLog += amount;
                                                	from.SendGump( new WoodBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	MapleLog += ((Item)o).Amount;
                                                	from.SendGump( new WoodBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is SycamoreLog )
                                {
                                        if ( SycamoreLog <= 59999 )
                                        {
						int need = 60000 - SycamoreLog;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60000 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new SycamoreLog( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many logs, 1 log is returned back to you." );
							else
								from.SendMessage( "You have given the box too many logs, "+ diff +" logs are returned back to you." );


                                               		((Item)o).Delete();
                                                	SycamoreLog += amount;
                                                	from.SendGump( new WoodBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	SycamoreLog += ((Item)o).Amount;
                                                	from.SendGump( new WoodBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is PineLog )
                                {
                                        if ( PineLog <= 59999 )
                                        {
						int need = 60000 - PineLog;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60000 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new PineLog( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many logs, 1 log is returned back to you." );
							else
								from.SendMessage( "You have given the box too many logs, "+ diff +" logs are returned back to you." );


                                               		((Item)o).Delete();
                                                	PineLog += amount;
                                                	from.SendGump( new WoodBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	PineLog += ((Item)o).Amount;
                                                	from.SendGump( new WoodBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is WalnutLog )
                                {
                                        if ( WalnutLog <= 59999 )
                                        {
						int need = 60000 - WalnutLog;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60000 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new WalnutLog( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many logs, 1 log is returned back to you." );
							else
								from.SendMessage( "You have given the box too many logs, "+ diff +" logs are returned back to you." );


                                               		((Item)o).Delete();
                                                	WalnutLog += amount;
                                                	from.SendGump( new WoodBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	WalnutLog += ((Item)o).Amount;
                                                	from.SendGump( new WoodBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is YewLog )
                                {
                                        if ( YewLog <= 59999 )
                                        {
						int need = 60000 - YewLog;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60000 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new YewLog( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many logs, 1 log is returned back to you." );
							else
								from.SendMessage( "You have given the box too many logs, "+ diff +" logs are returned back to you." );


                                               		((Item)o).Delete();
                                                	YewLog += amount;
                                                	from.SendGump( new WoodBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	YewLog += ((Item)o).Amount;
                                                	from.SendGump( new WoodBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is AshLog )
                                {
                                        if ( AshLog <= 59999 )
                                        {
						int need = 60000 - AshLog;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60000 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new AshLog( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many logs, 1 log is returned back to you." );
							else
								from.SendMessage( "You have given the box too many logs, "+ diff +" logs are returned back to you." );


                                               		((Item)o).Delete();
                                                	AshLog += amount;
                                                	from.SendGump( new WoodBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	AshLog += ((Item)o).Amount;
                                                	from.SendGump( new WoodBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
*/
                                if ( o is DullCopperIngot )
                                {
                                        if ( DullCopperIngot <= 59999 )
                                        {
						int need = 60000 - DullCopperIngot;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new DullCopperIngot( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many ingots, 1 ingot is returned back to you." );
							else
								from.SendMessage( "You have given the box too many ingots, "+ diff +" ingots are returned back to you." );


                                               		((Item)o).Delete();
                                                	DullCopperIngot += amount;
                                                	from.SendGump( new IngotBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	DullCopperIngot += ((Item)o).Amount;
                                                	from.SendGump( new IngotBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is ShadowIronIngot )
                                {
                                        if ( ShadowIronIngot <= 59999 )
                                        {
						int need = 60000 - ShadowIronIngot;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new ShadowIronIngot( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many ingots, 1 ingot is returned back to you." );
							else
								from.SendMessage( "You have given the box too many ingots, "+ diff +" ingots are returned back to you." );


                                               		((Item)o).Delete();
                                                	ShadowIronIngot += amount;
                                                	from.SendGump( new IngotBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	ShadowIronIngot += ((Item)o).Amount;
                                                	from.SendGump( new IngotBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is CopperIngot )
                                {
                                        if ( CopperIngot <= 59999 )
                                        {
						int need = 60000 - CopperIngot;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new CopperIngot( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many ingots, 1 ingot is returned back to you." );
							else
								from.SendMessage( "You have given the box too many ingots, "+ diff +" ingots are returned back to you." );


                                               		((Item)o).Delete();
                                                	CopperIngot += amount;
                                                	from.SendGump( new IngotBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	CopperIngot += ((Item)o).Amount;
                                                	from.SendGump( new IngotBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is BronzeIngot )
                                {
                                        if ( BronzeIngot <= 59999 )
                                        {
						int need = 60000 - BronzeIngot;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new BronzeIngot( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many ingots, 1 ingot is returned back to you." );
							else
								from.SendMessage( "You have given the box too many ingots, "+ diff +" ingots are returned back to you." );


                                               		((Item)o).Delete();
                                                	BronzeIngot += amount;
                                                	from.SendGump( new IngotBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	BronzeIngot += ((Item)o).Amount;
                                                	from.SendGump( new IngotBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is GoldIngot )
                                {
                                        if ( GoldIngot <= 59999 )
                                        {
						int need = 60000 - GoldIngot;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new GoldIngot( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many ingots, 1 ingot is returned back to you." );
							else
								from.SendMessage( "You have given the box too many ingots, "+ diff +" ingots are returned back to you." );


                                               		((Item)o).Delete();
                                                	GoldIngot += amount;
                                                	from.SendGump( new IngotBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	GoldIngot += ((Item)o).Amount;
                                                	from.SendGump( new IngotBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is AgapiteIngot )
                                {
                                        if ( AgapiteIngot <= 59999 )
                                        {
						int need = 60000 - AgapiteIngot;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new AgapiteIngot( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many ingots, 1 ingot is returned back to you." );
							else
								from.SendMessage( "You have given the box too many ingots, "+ diff +" ingots are returned back to you." );


                                               		((Item)o).Delete();
                                                	AgapiteIngot += amount;
                                                	from.SendGump( new IngotBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	AgapiteIngot += ((Item)o).Amount;
                                                	from.SendGump( new IngotBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is VeriteIngot )
                                {
                                        if ( VeriteIngot <= 59999 )
                                        {
						int need = 60000 - VeriteIngot;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new VeriteIngot( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many ingots, 1 ingot is returned back to you." );
							else
								from.SendMessage( "You have given the box too many ingots, "+ diff +" ingots are returned back to you." );


                                               		((Item)o).Delete();
                                                	VeriteIngot += amount;
                                                	from.SendGump( new IngotBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	VeriteIngot += ((Item)o).Amount;
                                                	from.SendGump( new IngotBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is ValoriteIngot )
                                {
                                        if ( ValoriteIngot <= 59999 )
                                        {
						int need = 60000 - ValoriteIngot;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new ValoriteIngot( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many ingots, 1 ingot is returned back to you." );
							else
								from.SendMessage( "You have given the box too many ingots, "+ diff +" ingots are returned back to you." );


                                               		((Item)o).Delete();
                                                	ValoriteIngot += amount;
                                                	from.SendGump( new IngotBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	ValoriteIngot += ((Item)o).Amount;
                                                	from.SendGump( new IngotBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is IronIngot )
                                {
                                        if ( IronIngot <= 59999 )
                                        {
						int need = 60000 - IronIngot;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new IronIngot( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many ingots, 1 ingot is returned back to you." );
							else
								from.SendMessage( "You have given the box too many ingots, "+ diff +" ingots are returned back to you." );


                                               		((Item)o).Delete();
                                                	IronIngot += amount;
                                                	from.SendGump( new IngotBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	IronIngot += ((Item)o).Amount;
                                                	from.SendGump( new IngotBoxGump1( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is DullCopperGranite )
                                {
                                        if ( DullCopperGranite >= 60000 )
                                                from.SendMessage( "That granite type is too full to add more." );
                                        else
                                        {
                                                ((Item)o).Delete();
                                                DullCopperGranite = ( DullCopperGranite + 1 );
                                                from.SendGump( new GraniteBoxGump1( (PlayerMobile)from, this ) );
                                                BeginCombine( from );
                                        }
                                }
                                if ( o is ShadowIronGranite )
                                {
                                        if ( ShadowIronGranite >= 60000 )
                                                from.SendMessage( "That granite type is too full to add more." );
                                        else
                                        {
                                                ((Item)o).Delete();
                                                ShadowIronGranite = ( ShadowIronGranite + 1 );
                                                from.SendGump( new GraniteBoxGump1( (PlayerMobile)from, this ) );
                                                BeginCombine( from );
                                        }
                                }
                                if ( o is CopperGranite )
                                {
                                        if ( CopperGranite >= 60000 )
                                                from.SendMessage( "That granite type is too full to add more." );
                                        else
                                        {
                                                ((Item)o).Delete();
                                                CopperGranite = ( CopperGranite + 1 );
                                                from.SendGump( new GraniteBoxGump1( (PlayerMobile)from, this ) );
                                                BeginCombine( from );
                                        }
                                }
                                if ( o is BronzeGranite )
                                {
                                        if ( BronzeGranite >= 60000 )
                                                from.SendMessage( "That granite type is too full to add more." );
                                        else
                                        {
                                                ((Item)o).Delete();
                                                BronzeGranite = ( BronzeGranite + 1 );
                                                from.SendGump( new GraniteBoxGump1( (PlayerMobile)from, this ) );
                                                BeginCombine( from );
                                        }
                                }
                                if ( o is GoldGranite )
                                {
                                        if ( GoldGranite >= 60000 )
                                                from.SendMessage( "That granite type is too full to add more." );
                                        else
                                        {
                                                ((Item)o).Delete();
                                                GoldGranite = ( GoldGranite + 1 );
                                                from.SendGump( new GraniteBoxGump1( (PlayerMobile)from, this ) );
                                                BeginCombine( from );
                                        }
                                }
                                if ( o is AgapiteGranite )
                                {
                                        if ( AgapiteGranite >= 60000 )
                                                from.SendMessage( "That granite type is too full to add more." );
                                        else
                                        {
                                                ((Item)o).Delete();
                                                AgapiteGranite = ( AgapiteGranite + 1 );
                                                from.SendGump( new GraniteBoxGump1( (PlayerMobile)from, this ) );
                                                BeginCombine( from );
                                        }
                                }
                                if ( o is VeriteGranite )
                                {
                                        if ( VeriteGranite >= 60000 )
                                                from.SendMessage( "That granite type is too full to add more." );
                                        else
                                        {
                                                ((Item)o).Delete();
                                                VeriteGranite = ( VeriteGranite + 1 );
                                                from.SendGump( new GraniteBoxGump1( (PlayerMobile)from, this ) );
                                                BeginCombine( from );
                                        }
                                }
                                if ( o is ValoriteGranite )
                                {
                                        if ( ValoriteGranite >= 60000 )
                                                from.SendMessage( "That granite type is too full to add more." );
                                        else
                                        {
                                                ((Item)o).Delete();
                                                ValoriteGranite = ( ValoriteGranite + 1 );
                                                from.SendGump( new GraniteBoxGump1( (PlayerMobile)from, this ) );
                                                BeginCombine( from );
                                        }
                                }
                                if ( o is Granite )
                                {
                                        if ( IronGranite >= 60000 )
                                                from.SendMessage( "That granite type is too full to add more." );
                                        else
                                        {
                                                ((Item)o).Delete();
                                                IronGranite = ( IronGranite + 1 );
                                                from.SendGump( new GraniteBoxGump1( (PlayerMobile)from, this ) );
                                                BeginCombine( from );
                                        }
                                }
                                if ( o is Cloth )
                                {
                                        if ( Cloth <= 59999 )
                                        {
						int need = 60000 - Cloth;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new Cloth( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many cloths, 1 cloth is returned back to you." );
							else
								from.SendMessage( "You have given the box too many cloths, "+ diff +" cloths are returned back to you." );


                                               		((Item)o).Delete();
                                                	Cloth += amount;
                                                	from.SendGump( new ClothANDLeatherBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	Cloth += ((Item)o).Amount;
                                                	from.SendGump( new ClothANDLeatherBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is Leather )
                                {
                                        if ( Leather <= 59999 )
                                        {
						int need = 60000 - Leather;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new Leather( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many leather, 1 leather is returned back to you." );
							else
								from.SendMessage( "You have given the box too many leather, "+ diff +" leather are returned back to you." );


                                               		((Item)o).Delete();
                                                	Leather += amount;
                                                	from.SendGump( new ClothANDLeatherBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	Leather += ((Item)o).Amount;
                                                	from.SendGump( new ClothANDLeatherBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is SpinedLeather )
                                {
                                        if ( Spined <= 59999 )
                                        {
						int need = 60000 - Spined;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new SpinedLeather( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many leather, 1 leather is returned back to you." );
							else
								from.SendMessage( "You have given the box too many leather, "+ diff +" leather are returned back to you." );


                                               		((Item)o).Delete();
                                                	Spined += amount;
                                                	from.SendGump( new ClothANDLeatherBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	Spined += ((Item)o).Amount;
                                                	from.SendGump( new ClothANDLeatherBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is HornedLeather )
                                {
                                        if ( Horned <= 59999 )
                                        {
						int need = 60000 - Horned;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new HornedLeather( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many leather, 1 leather is returned back to you." );
							else
								from.SendMessage( "You have given the box too many leather, "+ diff +" leather are returned back to you." );


                                               		((Item)o).Delete();
                                                	Horned += amount;
                                                	from.SendGump( new ClothANDLeatherBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	Horned += ((Item)o).Amount;
                                                	from.SendGump( new ClothANDLeatherBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is BarbedLeather )
                                {
                                        if ( Barbed <= 59999 )
                                        {
						int need = 60000 - Barbed;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new BarbedLeather( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many leather, 1 leather is returned back to you." );
							else
								from.SendMessage( "You have given the box too many leather, "+ diff +" leather are returned back to you." );


                                               		((Item)o).Delete();
                                                	Barbed += amount;
                                                	from.SendGump( new ClothANDLeatherBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	Barbed += ((Item)o).Amount;
                                                	from.SendGump( new ClothANDLeatherBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is MandrakeRoot )
                                {
                                        if ( MandrakeRoot <= 59999 )
                                        {
						int need = 60000 - MandrakeRoot;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new MandrakeRoot( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagent are returned back to you." );


                                               		((Item)o).Delete();
                                                	MandrakeRoot += amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	MandrakeRoot += ((Item)o).Amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is Bloodmoss )
                                {
                                        if ( Bloodmoss <= 59999 )
                                        {
						int need = 60000 - Bloodmoss;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new Bloodmoss( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagent are returned back to you." );


                                               		((Item)o).Delete();
                                                	Bloodmoss += amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	Bloodmoss += ((Item)o).Amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is BlackPearl )
                                {
                                        if ( BlackPearl <= 59999 )
                                        {
						int need = 60000 - BlackPearl;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new BlackPearl( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagent are returned back to you." );


                                               		((Item)o).Delete();
                                                	BlackPearl += amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	BlackPearl += ((Item)o).Amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is Nightshade )
                                {
                                        if ( Nightshade <= 59999 )
                                        {
						int need = 60000 - Nightshade;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new Nightshade( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagent are returned back to you." );


                                               		((Item)o).Delete();
                                                	Nightshade += amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	Nightshade += ((Item)o).Amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is Ginseng )
                                {
                                        if ( Ginseng <= 59999 )
                                        {
						int need = 60000 - Ginseng;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new Ginseng( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagent are returned back to you." );


                                               		((Item)o).Delete();
                                                	Ginseng += amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	Ginseng += ((Item)o).Amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is Garlic )
                                {
                                        if ( Garlic <= 59999 )
                                        {
						int need = 60000 - Garlic;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new Garlic( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagent are returned back to you." );


                                               		((Item)o).Delete();
                                                	Garlic += amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	Garlic += ((Item)o).Amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is SpidersSilk )
                                {
                                        if ( SpidersSilk <= 59999 )
                                        {
						int need = 60000 - SpidersSilk;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new SpidersSilk( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagent are returned back to you." );


                                               		((Item)o).Delete();
                                                	SpidersSilk += amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	SpidersSilk += ((Item)o).Amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is SulfurousAsh )
                                {
                                        if ( SulfurousAsh <= 59999 )
                                        {
						int need = 60000 - SulfurousAsh;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new SulfurousAsh( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagent are returned back to you." );


                                               		((Item)o).Delete();
                                                	SulfurousAsh += amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	SulfurousAsh += ((Item)o).Amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is BatWing )
                                {
                                        if ( BatWing <= 59999 )
                                        {
						int need = 60000 - BatWing;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new BatWing( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagent are returned back to you." );


                                               		((Item)o).Delete();
                                                	BatWing += amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	BatWing += ((Item)o).Amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is DaemonBlood )
                                {
                                        if ( DaemonBlood <= 59999 )
                                        {
						int need = 60000 - DaemonBlood;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new DaemonBlood( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagent are returned back to you." );


                                               		((Item)o).Delete();
                                                	DaemonBlood += amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	DaemonBlood += ((Item)o).Amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is GraveDust )
                                {
                                        if ( GraveDust <= 59999 )
                                        {
						int need = 60000 - GraveDust;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new GraveDust( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagent are returned back to you." );


                                               		((Item)o).Delete();
                                                	GraveDust += amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	GraveDust += ((Item)o).Amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is NoxCrystal )
                                {
                                        if ( NoxCrystal <= 59999 )
                                        {
						int need = 60000 - NoxCrystal;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new NoxCrystal( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagent are returned back to you." );


                                               		((Item)o).Delete();
                                                	NoxCrystal += amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	NoxCrystal += ((Item)o).Amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is PigIron )
                                {
                                        if ( PigIron <= 59999 )
                                        {
						int need = 60000 - PigIron;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new PigIron( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagent are returned back to you." );


                                               		((Item)o).Delete();
                                                	PigIron += amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	PigIron += ((Item)o).Amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
/*
                                if ( o is DestroyingAngel )
                                {
                                        if ( DestroyingAngel <= 59999 )
                                        {
						int need = 60000 - DestroyingAngel;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new DestroyingAngel( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagent are returned back to you." );


                                               		((Item)o).Delete();
                                                	DestroyingAngel += amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	DestroyingAngel += ((Item)o).Amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is PetrafiedWood )
                                {
                                        if ( PetrafiedWood <= 59999 )
                                        {
						int need = 60000 - PetrafiedWood;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new PetrafiedWood( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagent are returned back to you." );


                                               		((Item)o).Delete();
                                                	PetrafiedWood += amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	PetrafiedWood += ((Item)o).Amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is SpringWater )
                                {
                                        if ( SpringWater <= 59999 )
                                        {
						int need = 60000 - SpringWater;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new SpringWater( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many reagents, 1 reagent is returned back to you." );
							else
								from.SendMessage( "You have given the box too many reagents, "+ diff +" reagent are returned back to you." );


                                               		((Item)o).Delete();
                                                	SpringWater += amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	SpringWater += ((Item)o).Amount;
                                                	from.SendGump( new ReagentBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
*/
                                if ( o is RedScales )
                                {
                                        if ( RedScales <= 59999 )
                                        {
						int need = 60000 - RedScales;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new RedScales( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many scales, 1 scale is returned back to you." );
							else
								from.SendMessage( "You have given the box too many scales, "+ diff +" scales are returned back to you." );


                                               		((Item)o).Delete();
                                                	RedScales += amount;
                                                	from.SendGump( new ScalesBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	RedScales += ((Item)o).Amount;
                                                	from.SendGump( new ScalesBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is YellowScales )
                                {
                                        if ( YellowScales <= 59999 )
                                        {
						int need = 60000 - YellowScales;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new YellowScales( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many scales, 1 scale is returned back to you." );
							else
								from.SendMessage( "You have given the box too many scales, "+ diff +" scales are returned back to you." );


                                               		((Item)o).Delete();
                                                	YellowScales += amount;
                                                	from.SendGump( new ScalesBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	YellowScales += ((Item)o).Amount;
                                                	from.SendGump( new ScalesBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is BlackScales )
                                {
                                        if ( BlackScales <= 59999 )
                                        {
						int need = 60000 - BlackScales;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new BlackScales( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many scales, 1 scale is returned back to you." );
							else
								from.SendMessage( "You have given the box too many scales, "+ diff +" scales are returned back to you." );


                                               		((Item)o).Delete();
                                                	BlackScales += amount;
                                                	from.SendGump( new ScalesBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	BlackScales += ((Item)o).Amount;
                                                	from.SendGump( new ScalesBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is GreenScales )
                                {
                                        if ( GreenScales <= 59999 )
                                        {
						int need = 60000 - GreenScales;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new GreenScales( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many scales, 1 scale is returned back to you." );
							else
								from.SendMessage( "You have given the box too many scales, "+ diff +" scales are returned back to you." );


                                               		((Item)o).Delete();
                                                	GreenScales += amount;
                                                	from.SendGump( new ScalesBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	GreenScales += ((Item)o).Amount;
                                                	from.SendGump( new ScalesBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is WhiteScales )
                                {
                                        if ( WhiteScales <= 59999 )
                                        {
						int need = 60000 - WhiteScales;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new WhiteScales( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many scales, 1 scale is returned back to you." );
							else
								from.SendMessage( "You have given the box too many scales, "+ diff +" scales are returned back to you." );


                                               		((Item)o).Delete();
                                                	WhiteScales += amount;
                                                	from.SendGump( new ScalesBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	WhiteScales += ((Item)o).Amount;
                                                	from.SendGump( new ScalesBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                                if ( o is BlueScales )
                                {
                                        if ( BlueScales <= 59999 )
                                        {
						int need = 60000 - BlueScales;

						if ( ((Item)o).Amount >= need )
						{
							if ( ((Item)o).Amount >= 60001 )
							{
							int diff = ( ((Item)o).Amount - need );
							int amount = ( ((Item)o).Amount - diff );
							from.AddToBackpack( new BlueScales( diff ) );

							if ( diff == 1 )
								from.SendMessage( "You have given the box too many scales, 1 scale is returned back to you." );
							else
								from.SendMessage( "You have given the box too many scales, "+ diff +" scales are returned back to you." );


                                               		((Item)o).Delete();
                                                	BlueScales += amount;
                                                	from.SendGump( new ScalesBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
							}
						}
						else
						{
                                               		((Item)o).Delete();
                                                	BlueScales += ((Item)o).Amount;
                                                	from.SendGump( new ScalesBoxGump( (PlayerMobile)from, this ) );
                                                	BeginCombine( from );
						}
                                        }
                                }
                        }
                        else
                        {
                                from.SendLocalizedMessage( 1045158 ); // You must have the item in your backpack to target it.
                        }
                }

                public ResourceBox( Serial serial ) : base( serial )
                {
                }

                                public override void Serialize( GenericWriter writer ) 
		                { 
		                        base.Serialize( writer ); 
		
		                        writer.Write( (int) m_Sand); 
		                        writer.Write( (int) m_RegularLog); 
		                        //writer.Write( (int) m_AshLog); 
		                        //writer.Write( (int) m_CedarLog); 
		                        //writer.Write( (int) m_CherryLog); 
		                        //writer.Write( (int) m_MahoganyLog); 
		                        //writer.Write( (int) m_MapleLog); 
		                        //writer.Write( (int) m_SycamoreLog); 
		                        //writer.Write( (int) m_PineLog); 
		                        //writer.Write( (int) m_WalnutLog); 
		                        //writer.Write( (int) m_YewLog);
		                        writer.Write( (int) m_IronIngot); 
		                        writer.Write( (int) m_DullCopperIngot); 
		                        writer.Write( (int) m_ShadowIronIngot); 
		                        writer.Write( (int) m_CopperIngot); 
		                        writer.Write( (int) m_BronzeIngot); 
		                        writer.Write( (int) m_GoldIngot); 
		                        writer.Write( (int) m_AgapiteIngot); 
		                        writer.Write( (int) m_VeriteIngot); 
		                        writer.Write( (int) m_ValoriteIngot);
		                        writer.Write( (int) m_Iron); 
		                        writer.Write( (int) m_DullCopper); 
		                        writer.Write( (int) m_ShadowIron); 
		                        writer.Write( (int) m_Copper); 
		                        writer.Write( (int) m_Bronze); 
		                        writer.Write( (int) m_Gold); 
		                        writer.Write( (int) m_Agapite); 
		                        writer.Write( (int) m_Verite); 
		                        writer.Write( (int) m_Valorite);
		                        writer.Write( (int) m_Cloth); 
		                        writer.Write( (int) m_Leather); 
		                        writer.Write( (int) m_Spined); 
		                        writer.Write( (int) m_Horned); 
		                        writer.Write( (int) m_Barbed);
		                        writer.Write( (int) m_MandrakeRoot); 
		                        writer.Write( (int) m_Bloodmoss);
		                        writer.Write( (int) m_BlackPearl); 
		                        writer.Write( (int) m_Nightshade); 
		                        writer.Write( (int) m_Ginseng); 
		                        writer.Write( (int) m_Garlic); 
		                        writer.Write( (int) m_SpidersSilk); 
		                        writer.Write( (int) m_SulfurousAsh); 
		                        writer.Write( (int) m_NoxCrystal); 
		                        writer.Write( (int) m_PigIron); 
		                        writer.Write( (int) m_DaemonBlood);
		                        writer.Write( (int) m_GraveDust); 
		                        writer.Write( (int) m_BatWing); 
		                        //writer.Write( (int) m_DestroyingAngel); 
		                        //writer.Write( (int) m_PetrafiedWood); 
		                        //writer.Write( (int) m_SpringWater);
		                        writer.Write( (int) m_RedScales); 
		                        writer.Write( (int) m_YellowScales); 
		                        writer.Write( (int) m_BlackScales); 
		                        writer.Write( (int) m_GreenScales); 
		                        writer.Write( (int) m_WhiteScales);
		                        writer.Write( (int) m_BlueScales);
		                } 
		
		                public override void Deserialize( GenericReader reader ) 
		                { 
		                        base.Deserialize( reader ); 
		
		                        m_Sand = reader.ReadInt(); 
		                        m_RegularLog = reader.ReadInt(); 
		                        //m_AshLog = reader.ReadInt(); 
		                        //m_CedarLog = reader.ReadInt(); 
		                        //m_CherryLog = reader.ReadInt(); 
		                        //m_MahoganyLog = reader.ReadInt(); 
		                        //m_MapleLog = reader.ReadInt(); 
		                        //m_SycamoreLog = reader.ReadInt(); 
		                        //m_PineLog = reader.ReadInt(); 
		                        //m_WalnutLog = reader.ReadInt(); 
		                        //m_YewLog = reader.ReadInt();
		                        m_IronIngot = reader.ReadInt(); 
		                        m_DullCopperIngot = reader.ReadInt(); 
		                        m_ShadowIronIngot = reader.ReadInt(); 
		                        m_CopperIngot = reader.ReadInt(); 
		                        m_BronzeIngot = reader.ReadInt(); 
		                        m_GoldIngot = reader.ReadInt(); 
		                        m_AgapiteIngot = reader.ReadInt(); 
		                        m_VeriteIngot = reader.ReadInt(); 
		                        m_ValoriteIngot = reader.ReadInt();
		                        m_Iron = reader.ReadInt(); 
		                        m_DullCopper = reader.ReadInt(); 
		                        m_ShadowIron = reader.ReadInt(); 
		                        m_Copper = reader.ReadInt(); 
		                        m_Bronze = reader.ReadInt(); 
		                        m_Gold = reader.ReadInt(); 
		                        m_Agapite = reader.ReadInt(); 
		                        m_Verite = reader.ReadInt(); 
		                        m_Valorite = reader.ReadInt();
		                        m_Cloth = reader.ReadInt(); 
		                        m_Leather = reader.ReadInt(); 
		                        m_Spined = reader.ReadInt(); 
		                        m_Horned = reader.ReadInt(); 
		                        m_Barbed = reader.ReadInt();
		                        m_MandrakeRoot = reader.ReadInt(); 
		                        m_Bloodmoss = reader.ReadInt();
		                        m_BlackPearl = reader.ReadInt(); 
		                        m_Nightshade = reader.ReadInt(); 
		                        m_Ginseng = reader.ReadInt(); 
		                        m_Garlic = reader.ReadInt(); 
		                        m_SpidersSilk = reader.ReadInt(); 
		                        m_SulfurousAsh = reader.ReadInt(); 
		                        m_NoxCrystal = reader.ReadInt(); 
		                        m_PigIron = reader.ReadInt(); 
		                        m_DaemonBlood = reader.ReadInt();
		                        m_GraveDust = reader.ReadInt(); 
		                        m_BatWing = reader.ReadInt(); 
		                        //m_DestroyingAngel = reader.ReadInt(); 
		                        //m_PetrafiedWood = reader.ReadInt(); 
		                        //m_SpringWater = reader.ReadInt();
		                        m_RedScales = reader.ReadInt();
		                        m_YellowScales = reader.ReadInt(); 
		                        m_BlackScales = reader.ReadInt(); 
		                        m_GreenScales = reader.ReadInt(); 
		                        m_WhiteScales = reader.ReadInt(); 
		                        m_BlueScales = reader.ReadInt();
		                } 
		        } 
		} 
namespace Server.Items
{
        public class ChooseResourceGump : Gump
        {
                private PlayerMobile m_From;
                private ResourceBox m_Box;

                public ChooseResourceGump( PlayerMobile from, ResourceBox box ) : base( 25, 25 )
                {
                        m_From = from;
                        m_Box = box;

                        m_From.CloseGump( typeof( GraniteBoxGump1 ) );
                        m_From.CloseGump( typeof( IngotBoxGump1 ) );
                        m_From.CloseGump( typeof( ClothANDLeatherBoxGump ) );
                        m_From.CloseGump( typeof( WoodBoxGump1 ) );
                        m_From.CloseGump( typeof( ReagentBoxGump ) );
                        m_From.CloseGump( typeof( ChooseResourceGump ) );
                        m_From.CloseGump( typeof( ScalesBoxGump ) );

                        AddPage( 0 );

                        AddBackground( 50, 10, 455, 260, 5054 );
                        AddImageTiled( 58, 20, 438, 241, 2624 );
                        AddAlphaRegion( 58, 20, 438, 241 );

                        AddLabel( 225, 25, 0x480, "Choose Resource" );

                        AddLabel( 125, 50, 1152, "Logs" );
                        AddButton( 75, 50, 4005, 4007, 1, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 75, 1152, "Ingots" );
                        AddButton( 75, 75, 4005, 4007, 2, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 100, 1152, "Granites" );
                        AddButton( 75, 100, 4005, 4007, 3, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 125, 1152, "Cloth & Leathers" );
                        AddButton( 75, 125, 4005, 4007, 4, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 150, 1152, "Reagents" );
                        AddButton( 75, 150, 4005, 4007, 5, GumpButtonType.Reply, 0 );
                        AddLabel( 325, 50, 1152, "Scales" );
                        AddButton( 275, 50, 4005, 4007, 6, GumpButtonType.Reply, 0 );
                        
                        AddButton( 275, 200, 4005, 4007, 7, GumpButtonType.Reply, 0 );
                        AddLabel( 325, 200, 0x8AB, "Add Resource" );
                        
                }

                public override void OnResponse( NetState sender, RelayInfo info )
                {
                        if ( m_Box.Deleted )
                                return;

                        if ( info.ButtonID == 1 )
                        {
				m_From.SendGump( new WoodBoxGump1( m_From, m_Box ) );
                        }
                        if ( info.ButtonID == 2 )
                        {
				m_From.SendGump( new IngotBoxGump1( m_From, m_Box ) );
                        }
                        if ( info.ButtonID == 3 )
                        {
				m_From.SendGump( new GraniteBoxGump1( m_From, m_Box ) );
                        }
                        if ( info.ButtonID == 4 )
                        {
				m_From.SendGump( new ClothANDLeatherBoxGump( m_From, m_Box ) );
                        }
                        if ( info.ButtonID == 5 )
                        {
				m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                        }
                        if ( info.ButtonID == 6 )
                        {
				m_From.SendGump( new ScalesBoxGump( m_From, m_Box ) );
                        }
                        if ( info.ButtonID == 7 )
                        {
                                m_From.SendGump( new ChooseResourceGump( m_From, m_Box ) );
                                m_Box.BeginCombine( m_From );
                        }
                }
        }
        public class WoodBoxGump1 : Gump
        {
                private PlayerMobile m_From;
                private ResourceBox m_Box;

                public WoodBoxGump1( PlayerMobile from, ResourceBox box ) : base( 25, 25 )
                {
                        m_From = from;
                        m_Box = box;

                        m_From.CloseGump( typeof( GraniteBoxGump1 ) );
                        m_From.CloseGump( typeof( IngotBoxGump1 ) );
                        m_From.CloseGump( typeof( ClothANDLeatherBoxGump ) );
                        m_From.CloseGump( typeof( WoodBoxGump1 ) );
                        m_From.CloseGump( typeof( ReagentBoxGump ) );
                        m_From.CloseGump( typeof( ChooseResourceGump ) );
                        m_From.CloseGump( typeof( ScalesBoxGump ) );

                        AddPage( 0 );

                        AddBackground( 50, 10, 455, 260, 5054 );
                        AddImageTiled( 58, 20, 438, 241, 2624 );
                        AddAlphaRegion( 58, 20, 438, 241 );

                        AddLabel( 225, 25, 0x480, "Wood Box" );
/*
                        AddLabel( 125, 50, 1152, "Ash Log" );
                        AddLabel( 225, 50, 0x480, box.AshLog.ToString() );
                        AddButton( 75, 50, 4005, 4007, 1, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 75, 1152, "Cedar Log" );
                        AddLabel( 225, 75, 0x480, box.CedarLog.ToString() );
                        AddButton( 75, 75, 4005, 4007, 2, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 100, 1152, "Cherry Log" );
                        AddLabel( 225, 100, 0x480, box.CherryLog.ToString() );
                        AddButton( 75, 100, 4005, 4007, 3, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 125, 1152, "Mahogany Log" );
                        AddLabel( 225, 125, 0x480, box.MahoganyLog.ToString() );
                        AddButton( 75, 125, 4005, 4007, 4, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 150, 1152, "Maple Log" );
                        AddLabel( 225, 150, 0x480, box.MapleLog.ToString() );
                        AddButton( 75, 150, 4005, 4007, 5, GumpButtonType.Reply, 0 );
                        AddLabel( 325, 50, 1152, "Pine Log" );
                        AddLabel( 425, 50, 0x480, box.PineLog.ToString() );
                        AddButton( 275, 50, 4005, 4007, 6, GumpButtonType.Reply, 0 );
                        AddLabel( 325, 75, 1152, "Sycamore Log" );
                        AddLabel( 425, 75, 0x480, box.SycamoreLog.ToString() );
                        AddButton( 275, 75, 4005, 4007, 7, GumpButtonType.Reply, 0 );
                        AddLabel( 325, 100, 1152, "Walnut Log" );
                        AddLabel( 425, 100, 0x480, box.WalnutLog.ToString() );
                        AddButton( 275, 100, 4005, 4007, 8, GumpButtonType.Reply, 0 );
                        AddLabel( 325, 125, 1152, "Yew Log" );
                        AddLabel( 425, 125, 0x480, box.YewLog.ToString() );
                        AddButton( 275, 125, 4005, 4007, 9, GumpButtonType.Reply, 0 );
*/
                        AddLabel( 325, 150, 1152, "Log" );
                        AddLabel( 425, 150, 0x480, box.RegularLog.ToString() );
                        AddButton( 275, 150, 4005, 4007, 10, GumpButtonType.Reply, 0 );
                        
                        AddButton( 275, 200, 4005, 4007, 11, GumpButtonType.Reply, 0 );
                        AddLabel( 325, 200, 0x8AB, "Add Resource" );
                        
                        AddButton( 75, 25, 4005, 4007, 12, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 25, 0x8AB, "Back" );
                        
                }

                public override void OnResponse( NetState sender, RelayInfo info )
                {
                        if ( m_Box.Deleted )
                                return;
/*
                        if ( info.ButtonID == 1 )
                        {
                                if ( m_Box.AshLog >= 100 )
                                {
                                        m_Box.AshLog = ( m_Box.AshLog - 100 );
                                        m_From.AddToBackpack( new AshLog( 100 ) );
                                        m_From.SendGump( new WoodBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that log!" );
                                        m_From.SendGump( new WoodBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 2 )
                        {
                                if ( m_Box.CedarLog >= 100 )
                                {
                                        m_Box.CedarLog = ( m_Box.CedarLog - 100 );
                                        m_From.AddToBackpack( new CedarLog( 100 ) );
                                        m_From.SendGump( new WoodBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that log!" );
                                        m_From.SendGump( new WoodBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 3 )
                        {
                                if ( m_Box.CherryLog >= 100 )
                                {
                                        m_Box.CherryLog = ( m_Box.CherryLog - 100 );
                                        m_From.AddToBackpack( new CherryLog( 100 ) );
                                        m_From.SendGump( new WoodBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that log!" );
                                        m_From.SendGump( new WoodBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 4 )
                        {
                                if ( m_Box.MahoganyLog >= 100 )
                                {
                                        m_Box.MahoganyLog = ( m_Box.MahoganyLog - 100 );
                                        m_From.AddToBackpack( new MahoganyLog( 100 ) );
                                        m_From.SendGump( new WoodBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that log!" );
                                        m_From.SendGump( new WoodBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 5 )
                        {
                                if ( m_Box.MapleLog >= 100 )
                                {
                                        m_Box.MapleLog = ( m_Box.MapleLog - 100 );
                                        m_From.AddToBackpack( new MapleLog( 100 ) );
                                        m_From.SendGump( new WoodBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that log!" );
                                        m_From.SendGump( new WoodBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 6 )
                        {
                                if ( m_Box.SycamoreLog >= 100 )
                                {
                                        m_Box.SycamoreLog = ( m_Box.SycamoreLog - 100 );
                                        m_From.AddToBackpack( new SycamoreLog( 100 ) );
                                        m_From.SendGump( new WoodBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that log!" );
                                        m_From.SendGump( new WoodBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 7 )
                        {
                                if ( m_Box.PineLog >= 100 )
                                {
                                        m_Box.PineLog = ( m_Box.PineLog - 100 );
                                        m_From.AddToBackpack( new PineLog( 100 ) );
                                        m_From.SendGump( new WoodBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that log!" );
                                        m_From.SendGump( new WoodBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 8 )
                        {
                                if ( m_Box.WalnutLog >= 100 )
                                {
                                        m_Box.WalnutLog = ( m_Box.WalnutLog - 100 );
                                        m_From.AddToBackpack( new WalnutLog( 100 ) );
                                        m_From.SendGump( new WoodBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that log!" );
                                        m_From.SendGump( new WoodBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 9 )
                        {
                                if ( m_Box.YewLog >= 100 )
                                {
                                        m_Box.YewLog = ( m_Box.YewLog - 100 );
                                        m_From.AddToBackpack( new YewLog( 100 ) );
                                        m_From.SendGump( new WoodBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that log!" );
                                        m_From.SendGump( new WoodBoxGump1( m_From, m_Box ) );
                                }
                        }
*/
                        if ( info.ButtonID == 10 )
                        {
                                if ( m_Box.RegularLog >= 100 )
                                {
                                        m_Box.RegularLog = ( m_Box.RegularLog - 100 );
                                        m_From.AddToBackpack( new Log( 100 ) );
                                        m_From.SendGump( new WoodBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that log!" );
                                        m_From.SendGump( new WoodBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 11 )
                        {
                                m_From.SendGump( new WoodBoxGump1( m_From, m_Box ) );
                                m_Box.BeginCombine( m_From );
                        }
                        if ( info.ButtonID == 12 )
                        {
                                m_From.SendGump( new ChooseResourceGump( m_From, m_Box ) );
                        }
                }
        }
        public class GraniteBoxGump1 : Gump
        {
                private PlayerMobile m_From;
                private ResourceBox m_Box;

                public GraniteBoxGump1( PlayerMobile from, ResourceBox box ) : base( 25, 25 )
                {
                        m_From = from;
                        m_Box = box;

                        m_From.CloseGump( typeof( GraniteBoxGump1 ) );
                        m_From.CloseGump( typeof( IngotBoxGump1 ) );
                        m_From.CloseGump( typeof( ClothANDLeatherBoxGump ) );
                        m_From.CloseGump( typeof( WoodBoxGump1 ) );
                        m_From.CloseGump( typeof( ReagentBoxGump ) );
                        m_From.CloseGump( typeof( ChooseResourceGump ) );
                        m_From.CloseGump( typeof( ScalesBoxGump ) );

                        AddPage( 0 );

                        AddBackground( 50, 10, 455, 260, 5054 );
                        AddImageTiled( 58, 20, 438, 241, 2624 );
                        AddAlphaRegion( 58, 20, 438, 241 );

                        AddLabel( 225, 25, 0x480, "Granite & Sand Box" );

                        AddLabel( 125, 50, 1152, "Iron" );
                        AddLabel( 225, 50, 0x480, box.IronGranite.ToString() );
                        AddButton( 75, 50, 4005, 4007, 1, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 75, 1152, "Dull Copper" );
                        AddLabel( 225, 75, 0x480, box.DullCopperGranite.ToString() );
                        AddButton( 75, 75, 4005, 4007, 2, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 100, 1152, "Shadow Iron" );
                        AddLabel( 225, 100, 0x480, box.ShadowIronGranite.ToString() );
                        AddButton( 75, 100, 4005, 4007, 3, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 125, 1152, "Copper" );
                        AddLabel( 225, 125, 0x480, box.CopperGranite.ToString() );
                        AddButton( 75, 125, 4005, 4007, 4, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 150, 1152, "Bronze" );
                        AddLabel( 225, 150, 0x480, box.BronzeGranite.ToString() );
                        AddButton( 75, 150, 4005, 4007, 5, GumpButtonType.Reply, 0 );
                        AddLabel( 325, 50, 1152, "Gold" );
                        AddLabel( 425, 50, 0x480, box.GoldGranite.ToString() );
                        AddButton( 275, 50, 4005, 4007, 6, GumpButtonType.Reply, 0 );
                        AddLabel( 325, 75, 1152, "Agapite" );
                        AddLabel( 425, 75, 0x480, box.AgapiteGranite.ToString() );
                        AddButton( 275, 75, 4005, 4007, 7, GumpButtonType.Reply, 0 );
                        AddLabel( 325, 100, 1152, "Verite" );
                        AddLabel( 425, 100, 0x480, box.VeriteGranite.ToString() );
                        AddButton( 275, 100, 4005, 4007, 8, GumpButtonType.Reply, 0 );
                        AddLabel( 325, 125, 1152, "Valorite" );
                        AddLabel( 425, 125, 0x480, box.ValoriteGranite.ToString() );
                        AddButton( 275, 125, 4005, 4007, 9, GumpButtonType.Reply, 0 );
                        AddLabel( 325, 125, 1152, "Sand" );
                        AddLabel( 425, 125, 0x480, box.Sand.ToString() );
                        AddButton( 275, 125, 4005, 4007, 10, GumpButtonType.Reply, 0 );
                        
                        AddButton( 275, 200, 4005, 4007, 11, GumpButtonType.Reply, 0 );
                        AddLabel( 325, 200, 0x8AB, "Add Resource" );
                        
                        AddButton( 75, 25, 4005, 4007, 12, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 25, 0x8AB, "Back" );
                        
                }

                public override void OnResponse( NetState sender, RelayInfo info )
                {
                        if ( m_Box.Deleted )
                                return;

                        if ( info.ButtonID == 1 )
                        {
                                if ( m_Box.IronGranite > 0 )
                                {
                                        m_Box.IronGranite = ( m_Box.IronGranite - 1 );
                                        m_From.AddToBackpack( new Granite() );
                                        m_From.SendGump( new GraniteBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that granite!" );
                                        m_From.SendGump( new GraniteBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 2 )
                        {
                                if ( m_Box.DullCopperGranite > 0 )
                                {
                                        m_Box.DullCopperGranite = ( m_Box.DullCopperGranite - 1 );
                                        m_From.AddToBackpack( new DullCopperGranite() );
                                        m_From.SendGump( new GraniteBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that granite!" );
                                        m_From.SendGump( new GraniteBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 3 )
                        {
                                if ( m_Box.ShadowIronGranite > 0 )
                                {
                                        m_Box.ShadowIronGranite = ( m_Box.ShadowIronGranite - 1 );
                                        m_From.AddToBackpack( new ShadowIronGranite() );
                                        m_From.SendGump( new GraniteBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that granite!" );
                                        m_From.SendGump( new GraniteBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 4 )
                        {
                                if ( m_Box.CopperGranite > 0 )
                                {
                                        m_Box.CopperGranite = ( m_Box.CopperGranite - 1 );
                                        m_From.AddToBackpack( new CopperGranite() );
                                        m_From.SendGump( new GraniteBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that granite!" );
                                        m_From.SendGump( new GraniteBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 5 )
                        {
                                if ( m_Box.BronzeGranite > 0 )
                                {
                                        m_Box.BronzeGranite = ( m_Box.BronzeGranite - 1 );
                                        m_From.AddToBackpack( new BronzeGranite() );
                                        m_From.SendGump( new GraniteBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that granite!" );
                                        m_From.SendGump( new GraniteBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 6 )
                        {
                                if ( m_Box.GoldGranite > 0 )
                                {
                                        m_Box.GoldGranite = ( m_Box.GoldGranite - 1 );
                                        m_From.AddToBackpack( new GoldGranite() );
                                        m_From.SendGump( new GraniteBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that granite!" );
                                        m_From.SendGump( new GraniteBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 7 )
                        {
                                if ( m_Box.AgapiteGranite > 0 )
                                {
                                        m_Box.AgapiteGranite = ( m_Box.AgapiteGranite - 1 );
                                        m_From.AddToBackpack( new AgapiteGranite() );
                                        m_From.SendGump( new GraniteBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that granite!" );
                                        m_From.SendGump( new GraniteBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 8 )
                        {
                                if ( m_Box.VeriteGranite > 0 )
                                {
                                        m_Box.VeriteGranite = ( m_Box.VeriteGranite - 1 );
                                        m_From.AddToBackpack( new VeriteGranite() );
                                        m_From.SendGump( new GraniteBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that granite!" );
                                        m_From.SendGump( new GraniteBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 9 )
                        {
                                if ( m_Box.ValoriteGranite > 0 )
                                {
                                        m_Box.ValoriteGranite = ( m_Box.ValoriteGranite - 1 );
                                        m_From.AddToBackpack( new ValoriteGranite() );
                                        m_From.SendGump( new GraniteBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that granite!" );
                                        m_From.SendGump( new GraniteBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 10 )
                        {
                                if ( m_Box.Sand > 0 )
                                {
                                        m_Box.Sand = ( m_Box.Sand - 1 );
                                        m_From.AddToBackpack( new Sand() );
                                        m_From.SendGump( new GraniteBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that granite!" );
                                        m_From.SendGump( new GraniteBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 11 )
                        {
                                m_From.SendGump( new GraniteBoxGump1( m_From, m_Box ) );
                                m_Box.BeginCombine( m_From );
                        }
                        if ( info.ButtonID == 12 )
                        {
                                m_From.SendGump( new ChooseResourceGump( m_From, m_Box ) );
                        }
                }
        }
        public class IngotBoxGump1 : Gump
        {
                private PlayerMobile m_From;
                private ResourceBox m_Box;

                public IngotBoxGump1( PlayerMobile from, ResourceBox box ) : base( 25, 25 )
                {
                        m_From = from;
                        m_Box = box;

                        m_From.CloseGump( typeof( GraniteBoxGump1 ) );
                        m_From.CloseGump( typeof( IngotBoxGump1 ) );
                        m_From.CloseGump( typeof( ClothANDLeatherBoxGump ) );
                        m_From.CloseGump( typeof( WoodBoxGump1 ) );
                        m_From.CloseGump( typeof( ReagentBoxGump ) );
                        m_From.CloseGump( typeof( ChooseResourceGump ) );
                        m_From.CloseGump( typeof( ScalesBoxGump ) );

                        AddPage( 0 );

                        AddBackground( 50, 10, 455, 260, 5054 );
                        AddImageTiled( 58, 20, 438, 241, 2624 );
                        AddAlphaRegion( 58, 20, 438, 241 );

                        AddLabel( 225, 25, 0x480, "Ingot Box" );

                        AddLabel( 125, 50, 1152, "Iron" );
                        AddLabel( 225, 50, 0x480, box.IronIngot.ToString() );
                        AddButton( 75, 50, 4005, 4007, 1, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 75, 1152, "Dull Copper" );
                        AddLabel( 225, 75, 0x480, box.DullCopperIngot.ToString() );
                        AddButton( 75, 75, 4005, 4007, 2, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 100, 1152, "Shadow Iron" );
                        AddLabel( 225, 100, 0x480, box.ShadowIronIngot.ToString() );
                        AddButton( 75, 100, 4005, 4007, 3, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 125, 1152, "Copper" );
                        AddLabel( 225, 125, 0x480, box.CopperIngot.ToString() );
                        AddButton( 75, 125, 4005, 4007, 4, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 150, 1152, "Bronze" );
                        AddLabel( 225, 150, 0x480, box.BronzeIngot.ToString() );
                        AddButton( 75, 150, 4005, 4007, 5, GumpButtonType.Reply, 0 );
                        AddLabel( 325, 50, 1152, "Gold" );
                        AddLabel( 425, 50, 0x480, box.GoldIngot.ToString() );
                        AddButton( 275, 50, 4005, 4007, 6, GumpButtonType.Reply, 0 );
                        AddLabel( 325, 75, 1152, "Agapite" );
                        AddLabel( 425, 75, 0x480, box.AgapiteIngot.ToString() );
                        AddButton( 275, 75, 4005, 4007, 7, GumpButtonType.Reply, 0 );
                        AddLabel( 325, 100, 1152, "Verite" );
                        AddLabel( 425, 100, 0x480, box.VeriteIngot.ToString() );
                        AddButton( 275, 100, 4005, 4007, 8, GumpButtonType.Reply, 0 );
                        AddLabel( 325, 125, 1152, "Valorite" );
                        AddLabel( 425, 125, 0x480, box.ValoriteIngot.ToString() );
                        AddButton( 275, 125, 4005, 4007, 9, GumpButtonType.Reply, 0 );
                        
                        AddButton( 275, 200, 4005, 4007, 10, GumpButtonType.Reply, 0 );
                        AddLabel( 325, 200, 0x8AB, "Add Resource" );
                        
                        AddButton( 75, 25, 4005, 4007, 11, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 25, 0x8AB, "Back" );
                        
                }

                public override void OnResponse( NetState sender, RelayInfo info )
                {
                        if ( m_Box.Deleted )
                                return;

                        if ( info.ButtonID == 1 )
                        {
                                if ( m_Box.IronIngot >= 100 )
                                {
                                        m_Box.IronIngot = ( m_Box.IronIngot - 100 );
                                        m_From.AddToBackpack( new IronIngot( 100 ) );
                                        m_From.SendGump( new IngotBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that Ingot!" );
                                        m_From.SendGump( new IngotBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 2 )
                        {
                                if ( m_Box.DullCopperIngot >= 100 )
                                {
                                        m_Box.DullCopperIngot = ( m_Box.DullCopperIngot - 100 );
                                        m_From.AddToBackpack( new DullCopperIngot( 100 ) );
                                        m_From.SendGump( new IngotBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that Ingot!" );
                                        m_From.SendGump( new IngotBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 3 )
                        {
                                if ( m_Box.ShadowIronIngot >= 100 )
                                {
                                        m_Box.ShadowIronIngot = ( m_Box.ShadowIronIngot - 100 );
                                        m_From.AddToBackpack( new ShadowIronIngot( 100 ) );
                                        m_From.SendGump( new IngotBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that Ingot!" );
                                        m_From.SendGump( new IngotBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 4 )
                        {
                                if ( m_Box.CopperIngot >= 100 )
                                {
                                        m_Box.CopperIngot = ( m_Box.CopperIngot - 100 );
                                        m_From.AddToBackpack( new CopperIngot( 100 ) );
                                        m_From.SendGump( new IngotBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that Ingot!" );
                                        m_From.SendGump( new IngotBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 5 )
                        {
                                if ( m_Box.BronzeIngot >= 100 )
                                {
                                        m_Box.BronzeIngot = ( m_Box.BronzeIngot - 100 );
                                        m_From.AddToBackpack( new BronzeIngot( 100 ) );
                                        m_From.SendGump( new IngotBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that Ingot!" );
                                        m_From.SendGump( new IngotBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 6 )
                        {
                                if ( m_Box.GoldIngot >= 100 )
                                {
                                        m_Box.GoldIngot = ( m_Box.GoldIngot - 100 );
                                        m_From.AddToBackpack( new GoldIngot( 100 ) );
                                        m_From.SendGump( new IngotBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that Ingot!" );
                                        m_From.SendGump( new IngotBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 7 )
                        {
                                if ( m_Box.AgapiteIngot >= 100 )
                                {
                                        m_Box.AgapiteIngot = ( m_Box.AgapiteIngot - 100 );
                                        m_From.AddToBackpack( new AgapiteIngot( 100 ) );
                                        m_From.SendGump( new IngotBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that Ingot!" );
                                        m_From.SendGump( new IngotBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 8 )
                        {
                                if ( m_Box.VeriteIngot >= 100 )
                                {
                                        m_Box.VeriteIngot = ( m_Box.VeriteIngot - 100 );
                                        m_From.AddToBackpack( new VeriteIngot( 100 ) );
                                        m_From.SendGump( new IngotBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that Ingot!" );
                                        m_From.SendGump( new IngotBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 9 )
                        {
                                if ( m_Box.ValoriteIngot >= 100 )
                                {
                                        m_Box.ValoriteIngot = ( m_Box.ValoriteIngot - 100 );
                                        m_From.AddToBackpack( new ValoriteIngot( 100 ) );
                                        m_From.SendGump( new IngotBoxGump1( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that Ingot!" );
                                        m_From.SendGump( new IngotBoxGump1( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 10 )
                        {
                                m_From.SendGump( new IngotBoxGump1( m_From, m_Box ) );
                                m_Box.BeginCombine( m_From );
                        }
                        if ( info.ButtonID == 11 )
                        {
                                m_From.SendGump( new ChooseResourceGump( m_From, m_Box ) );
                        }
                }
        }
        public class ClothANDLeatherBoxGump : Gump
        {
                private PlayerMobile m_From;
                private ResourceBox m_Box;

                public ClothANDLeatherBoxGump( PlayerMobile from, ResourceBox box ) : base( 25, 25 )
                {
                        m_From = from;
                        m_Box = box;

                        m_From.CloseGump( typeof( GraniteBoxGump1 ) );
                        m_From.CloseGump( typeof( IngotBoxGump1 ) );
                        m_From.CloseGump( typeof( ClothANDLeatherBoxGump ) );
                        m_From.CloseGump( typeof( WoodBoxGump1 ) );
                        m_From.CloseGump( typeof( ReagentBoxGump ) );
                        m_From.CloseGump( typeof( ChooseResourceGump ) );
                        m_From.CloseGump( typeof( ScalesBoxGump ) );

                        AddPage( 0 );

                        AddBackground( 50, 10, 455, 260, 5054 );
                        AddImageTiled( 58, 20, 438, 241, 2624 );
                        AddAlphaRegion( 58, 20, 438, 241 );

                        AddLabel( 225, 25, 0x480, "Cloth & Leather Box" );

                        AddLabel( 125, 50, 1152, "Cloth" );
                        AddLabel( 225, 50, 0x480, box.Cloth.ToString() );
                        AddButton( 75, 50, 4005, 4007, 1, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 75, 1152, "Leather" );
                        AddLabel( 225, 75, 0x480, box.Leather.ToString() );
                        AddButton( 75, 75, 4005, 4007, 2, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 100, 1152, "Spined Leather" );
                        AddLabel( 225, 100, 0x480, box.Spined.ToString() );
                        AddButton( 75, 100, 4005, 4007, 3, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 125, 1152, "Horned Leather" );
                        AddLabel( 225, 125, 0x480, box.Horned.ToString() );
                        AddButton( 75, 125, 4005, 4007, 4, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 150, 1152, "Barbed Leather" );
                        AddLabel( 225, 150, 0x480, box.Barbed.ToString() );
                        AddButton( 75, 150, 4005, 4007, 5, GumpButtonType.Reply, 0 );
                        
                        AddButton( 275, 200, 4005, 4007, 6, GumpButtonType.Reply, 0 );
                        AddLabel( 325, 200, 0x8AB, "Add Resource" );
                        
                        AddButton( 75, 25, 4005, 4007, 7, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 25, 0x8AB, "Back" );
                        
                }

                public override void OnResponse( NetState sender, RelayInfo info )
                {
                        if ( m_Box.Deleted )
                                return;

                        if ( info.ButtonID == 1 )
                        {
                                if ( m_Box.Cloth >= 100 )
                                {
                                        m_Box.Cloth = ( m_Box.Cloth - 100 );
                                        m_From.AddToBackpack( new Cloth( 100 ) );
                                        m_From.SendGump( new ClothANDLeatherBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that!" );
                                        m_From.SendGump( new ClothANDLeatherBoxGump( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 2 )
                        {
                                if ( m_Box.Leather >= 100 )
                                {
                                        m_Box.Leather = ( m_Box.Leather - 100 );
                                        m_From.AddToBackpack( new Leather( 100 ) );
                                        m_From.SendGump( new ClothANDLeatherBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that leather!" );
                                        m_From.SendGump( new ClothANDLeatherBoxGump( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 3 )
                        {
                                if ( m_Box.Spined >= 100 )
                                {
                                        m_Box.Spined = ( m_Box.Spined - 100 );
                                        m_From.AddToBackpack( new SpinedLeather( 100 ) );
                                        m_From.SendGump( new ClothANDLeatherBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that leather!" );
                                        m_From.SendGump( new ClothANDLeatherBoxGump( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 4 )
                        {
                                if ( m_Box.Horned >= 100 )
                                {
                                        m_Box.Horned = ( m_Box.Horned - 100 );
                                        m_From.AddToBackpack( new HornedLeather( 100 ) );
                                        m_From.SendGump( new ClothANDLeatherBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that leather!" );
                                        m_From.SendGump( new ClothANDLeatherBoxGump( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 5 )
                        {
                                if ( m_Box.Barbed >= 100 )
                                {
                                        m_Box.Barbed = ( m_Box.Barbed - 100 );
                                        m_From.AddToBackpack( new BarbedLeather( 100 ) );
                                        m_From.SendGump( new ClothANDLeatherBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that leather!" );
                                        m_From.SendGump( new ClothANDLeatherBoxGump( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 6 )
                        {
                                m_From.SendGump( new ClothANDLeatherBoxGump( m_From, m_Box ) );
                                m_Box.BeginCombine( m_From );
                        }
                        if ( info.ButtonID == 7 )
                        {
                                m_From.SendGump( new ChooseResourceGump( m_From, m_Box ) );
                        }
                }
        }
        public class ReagentBoxGump : Gump
        {
                private PlayerMobile m_From;
                private ResourceBox m_Box;

                public ReagentBoxGump( PlayerMobile from, ResourceBox box ) : base( 25, 25 )
                {
                        m_From = from;
                        m_Box = box;

                        m_From.CloseGump( typeof( GraniteBoxGump1 ) );
                        m_From.CloseGump( typeof( IngotBoxGump1 ) );
                        m_From.CloseGump( typeof( ClothANDLeatherBoxGump ) );
                        m_From.CloseGump( typeof( WoodBoxGump1 ) );
                        m_From.CloseGump( typeof( ReagentBoxGump ) );
                        m_From.CloseGump( typeof( ChooseResourceGump ) );
                        m_From.CloseGump( typeof( ScalesBoxGump ) );

                        AddPage( 0 );

                        AddBackground( 50, 10, 455, 260, 5054 );
                        AddImageTiled( 58, 20, 438, 241, 2624 );
                        AddAlphaRegion( 58, 20, 438, 241 );

                        AddLabel( 225, 25, 0x480, "Reagent Box" );

                        AddLabel( 125, 50, 1152, "Mandrake Root" );
                        AddLabel( 225, 50, 0x480, box.MandrakeRoot.ToString() );
                        AddButton( 75, 50, 4005, 4007, 1, GumpButtonType.Reply, 0 );

                        AddLabel( 125, 75, 1152, "Blood Moss" );
                        AddLabel( 225, 75, 0x480, box.Bloodmoss.ToString() );
                        AddButton( 75, 75, 4005, 4007, 2, GumpButtonType.Reply, 0 );

                        AddLabel( 125, 100, 1152, "Black Pearl" );
                        AddLabel( 225, 100, 0x480, box.BlackPearl.ToString() );
                        AddButton( 75, 100, 4005, 4007, 3, GumpButtonType.Reply, 0 );

                        AddLabel( 125, 125, 1152, "Garlic" );
                        AddLabel( 225, 125, 0x480, box.Garlic.ToString() );
                        AddButton( 75, 125, 4005, 4007, 4, GumpButtonType.Reply, 0 );

                        AddLabel( 125, 150, 1152, "Nightshade" );
                        AddLabel( 225, 150, 0x480, box.Nightshade.ToString() );
                        AddButton( 75, 150, 4005, 4007, 5, GumpButtonType.Reply, 0 );

                        AddLabel( 125, 175, 1152, "Sulfurous Ash" );
                        AddLabel( 225, 175, 0x480, box.SulfurousAsh.ToString() );
                        AddButton( 75, 175, 4005, 4007, 6, GumpButtonType.Reply, 0 );

                        AddLabel( 125, 200, 1152, "Spiders' Silk" );
                        AddLabel( 225, 200, 0x480, box.SpidersSilk.ToString() );
                        AddButton( 75, 200, 4005, 4007, 7, GumpButtonType.Reply, 0 );

                        AddLabel( 125, 225, 1152, "Ginseng" );
                        AddLabel( 225, 225, 0x480, box.Ginseng.ToString() );
                        AddButton( 75, 225, 4005, 4007, 8, GumpButtonType.Reply, 0 );

                        AddLabel( 325, 50, 1152, "Nox Crystal" );
                        AddLabel( 425, 50, 0x480, box.NoxCrystal.ToString() );
                        AddButton( 275, 50, 4005, 4007, 9, GumpButtonType.Reply, 0 );

                        AddLabel( 325, 75, 1152, "Daemon Blood" );
                        AddLabel( 425, 75, 0x480, box.DaemonBlood.ToString() );
                        AddButton( 275, 75, 4005, 4007, 10, GumpButtonType.Reply, 0 );

                        AddLabel( 325, 100, 1152, "Bat Wing" );
                        AddLabel( 425, 100, 0x480, box.BatWing.ToString() );
                        AddButton( 275, 100, 4005, 4007, 11, GumpButtonType.Reply, 0 );

                        AddLabel( 325, 125, 1152, "Pig Iron" );
                        AddLabel( 425, 125, 0x480, box.PigIron.ToString() );
                        AddButton( 275, 125, 4005, 4007, 12, GumpButtonType.Reply, 0 );

                        AddLabel( 325, 150, 1152, "Grave Dust" );
                        AddLabel( 425, 150, 0x480, box.GraveDust.ToString() );
                        AddButton( 275, 150, 4005, 4007, 13, GumpButtonType.Reply, 0 );
/*
                        AddLabel( 325, 175, 1152, "Destroying Angel" );
                        AddLabel( 425, 175, 0x480, box.DestroyingAngel.ToString() );
                        AddButton( 275, 175, 4005, 4007, 14, GumpButtonType.Reply, 0 );

                        AddLabel( 325, 200, 1152, "Petrafied Wood" );
                        AddLabel( 425, 200, 0x480, box.PetrafiedWood.ToString() );
                        AddButton( 275, 200, 4005, 4007, 15, GumpButtonType.Reply, 0 );

                        AddLabel( 325, 225, 1152, "Spring Water" );
                        AddLabel( 425, 225, 0x480, box.SpringWater.ToString() );
                        AddButton( 275, 225, 4005, 4007, 16, GumpButtonType.Reply, 0 );
*/

                        
                        AddButton( 275, 25, 4005, 4007, 17, GumpButtonType.Reply, 0 );
                        AddLabel( 325, 25, 0x8AB, "Add Resource" );
                        
                        AddButton( 75, 25, 4005, 4007, 18, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 25, 0x8AB, "Back" );
                        
                }

                public override void OnResponse( NetState sender, RelayInfo info )
                {
                        if ( m_Box.Deleted )
                                return;

                        if ( info.ButtonID == 1 )
                        {
                                if ( m_Box.MandrakeRoot >= 100 )
                                {
                                        m_Box.MandrakeRoot = ( m_Box.MandrakeRoot - 100 );
                                        m_From.AddToBackpack( new MandrakeRoot( 100 ) );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that reagent!" );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 2 )
                        {
                                if ( m_Box.Bloodmoss >= 100 )
                                {
                                        m_Box.Bloodmoss = ( m_Box.Bloodmoss - 100 );
                                        m_From.AddToBackpack( new Bloodmoss( 100 ) );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that reagent!" );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 3 )
                        {
                                if ( m_Box.BlackPearl >= 100 )
                                {
                                        m_Box.BlackPearl = ( m_Box.BlackPearl - 100 );
                                        m_From.AddToBackpack( new BlackPearl( 100 ) );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that reagent!" );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 4 )
                        {
                                if ( m_Box.Garlic >= 100 )
                                {
                                        m_Box.Garlic = ( m_Box.Garlic - 100 );
                                        m_From.AddToBackpack( new Garlic( 100 ) );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that reagent!" );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 5 )
                        {
                                if ( m_Box.Nightshade >= 100 )
                                {
                                        m_Box.Nightshade = ( m_Box.Nightshade - 100 );
                                        m_From.AddToBackpack( new Nightshade( 100 ) );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that reagent!" );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 6 )
                        {
                                if ( m_Box.SulfurousAsh >= 100 )
                                {
                                        m_Box.SulfurousAsh = ( m_Box.SulfurousAsh - 100 );
                                        m_From.AddToBackpack( new SulfurousAsh( 100 ) );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that reagent!" );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 7 )
                        {
                                if ( m_Box.SpidersSilk >= 100 )
                                {
                                        m_Box.SpidersSilk = ( m_Box.SpidersSilk - 100 );
                                        m_From.AddToBackpack( new SpidersSilk( 100 ) );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that reagent!" );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 8 )
                        {
                                if ( m_Box.Ginseng >= 100 )
                                {
                                        m_Box.Ginseng = ( m_Box.Ginseng - 100 );
                                        m_From.AddToBackpack( new Ginseng( 100 ) );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that reagent!" );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 9 )
                        {
                                if ( m_Box.NoxCrystal >= 100 )
                                {
                                        m_Box.NoxCrystal = ( m_Box.NoxCrystal - 100 );
                                        m_From.AddToBackpack( new NoxCrystal( 100 ) );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that reagent!" );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 10 )
                        {
                                if ( m_Box.DaemonBlood >= 100 )
                                {
                                        m_Box.DaemonBlood = ( m_Box.DaemonBlood - 100 );
                                        m_From.AddToBackpack( new DaemonBlood( 100 ) );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that reagent!" );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 11 )
                        {
                                if ( m_Box.BatWing >= 100 )
                                {
                                        m_Box.BatWing = ( m_Box.BatWing - 100 );
                                        m_From.AddToBackpack( new BatWing( 100 ) );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that reagent!" );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 12 )
                        {
                                if ( m_Box.PigIron >= 100 )
                                {
                                        m_Box.PigIron = ( m_Box.PigIron - 100 );
                                        m_From.AddToBackpack( new PigIron( 100 ) );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that reagent!" );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 13 )
                        {
                                if ( m_Box.GraveDust >= 100 )
                                {
                                        m_Box.GraveDust = ( m_Box.GraveDust - 100 );
                                        m_From.AddToBackpack( new GraveDust( 100 ) );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that reagent!" );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                        }
/*
                        if ( info.ButtonID == 14 )
                        {
                                if ( m_Box.DestroyingAngel >= 100 )
                                {
                                        m_Box.DestroyingAngel = ( m_Box.DestroyingAngel - 100 );
                                        m_From.AddToBackpack( new DestroyingAngel( 100 ) );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that reagent!" );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 15 )
                        {
                                if ( m_Box.PetrafiedWood >= 100 )
                                {
                                        m_Box.PetrafiedWood = ( m_Box.PetrafiedWood - 100 );
                                        m_From.AddToBackpack( new PetrafiedWood( 100 ) );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that reagent!" );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 16 )
                        {
                                if ( m_Box.SpringWater >= 100 )
                                {
                                        m_Box.SpringWater = ( m_Box.SpringWater - 100 );
                                        m_From.AddToBackpack( new SpringWater( 100 ) );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that reagent!" );
                                        m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                }
                        }
*/
                        if ( info.ButtonID == 17 )
                        {
                                m_From.SendGump( new ReagentBoxGump( m_From, m_Box ) );
                                m_Box.BeginCombine( m_From );
                        }
                        if ( info.ButtonID == 18 )
                        {
                                m_From.SendGump( new ChooseResourceGump( m_From, m_Box ) );
                        }
                }
        }
        public class ScalesBoxGump : Gump
        {
                private PlayerMobile m_From;
                private ResourceBox m_Box;

                public ScalesBoxGump( PlayerMobile from, ResourceBox box ) : base( 25, 25 )
                {
                        m_From = from;
                        m_Box = box;

                        m_From.CloseGump( typeof( GraniteBoxGump1 ) );
                        m_From.CloseGump( typeof( IngotBoxGump1 ) );
                        m_From.CloseGump( typeof( ClothANDLeatherBoxGump ) );
                        m_From.CloseGump( typeof( WoodBoxGump1 ) );
                        m_From.CloseGump( typeof( ReagentBoxGump ) );
                        m_From.CloseGump( typeof( ChooseResourceGump ) );
                        m_From.CloseGump( typeof( ScalesBoxGump ) );

                        AddPage( 0 );

                        AddBackground( 50, 10, 455, 260, 5054 );
                        AddImageTiled( 58, 20, 438, 241, 2624 );
                        AddAlphaRegion( 58, 20, 438, 241 );

                        AddLabel( 225, 25, 0x480, "Scale Box" );

                        AddLabel( 125, 50, 1152, "Red Scales" );
                        AddLabel( 225, 50, 0x480, box.RedScales.ToString() );
                        AddButton( 75, 50, 4005, 4007, 1, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 75, 1152, "Yellow Scales" );
                        AddLabel( 225, 75, 0x480, box.YellowScales.ToString() );
                        AddButton( 75, 75, 4005, 4007, 2, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 100, 1152, "Black Scales" );
                        AddLabel( 225, 100, 0x480, box.BlackScales.ToString() );
                        AddButton( 75, 100, 4005, 4007, 3, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 125, 1152, "Green Scales" );
                        AddLabel( 225, 125, 0x480, box.GreenScales.ToString() );
                        AddButton( 75, 125, 4005, 4007, 4, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 150, 1152, "White Scales" );
                        AddLabel( 225, 150, 0x480, box.WhiteScales.ToString() );
                        AddButton( 75, 150, 4005, 4007, 5, GumpButtonType.Reply, 0 );
                        AddLabel( 325, 50, 1152, "Blue Scales" );
                        AddLabel( 425, 50, 0x480, box.BlueScales.ToString() );
                        AddButton( 275, 50, 4005, 4007, 6, GumpButtonType.Reply, 0 );
                        
                        AddButton( 275, 200, 4005, 4007, 7, GumpButtonType.Reply, 0 );
                        AddLabel( 325, 200, 0x8AB, "Add Resource" );
                        
                        AddButton( 75, 25, 4005, 4007, 8, GumpButtonType.Reply, 0 );
                        AddLabel( 125, 25, 0x8AB, "Back" );
                        
                }

                public override void OnResponse( NetState sender, RelayInfo info )
                {
                        if ( m_Box.Deleted )
                                return;

                        if ( info.ButtonID == 1 )
                        {
                                if ( m_Box.RedScales >= 100 )
                                {
                                        m_Box.RedScales = ( m_Box.RedScales - 100 );
                                        m_From.AddToBackpack( new RedScales( 100 ) );
                                        m_From.SendGump( new ScalesBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of those scales!" );
                                        m_From.SendGump( new ScalesBoxGump( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 2 )
                        {
                                if ( m_Box.YellowScales >= 100 )
                                {
                                        m_Box.YellowScales = ( m_Box.YellowScales - 100 );
                                        m_From.AddToBackpack( new YellowScales( 100 ) );
                                        m_From.SendGump( new ScalesBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of those scales!" );
                                        m_From.SendGump( new ScalesBoxGump( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 3 )
                        {
                                if ( m_Box.BlackScales >= 100 )
                                {
                                        m_Box.BlackScales = ( m_Box.BlackScales - 100 );
                                        m_From.AddToBackpack( new BlackScales( 100 ) );
                                        m_From.SendGump( new ScalesBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of those scales!" );
                                        m_From.SendGump( new ScalesBoxGump( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 4 )
                        {
                                if ( m_Box.GreenScales >= 100 )
                                {
                                        m_Box.GreenScales = ( m_Box.GreenScales - 100 );
                                        m_From.AddToBackpack( new GreenScales( 100 ) );
                                        m_From.SendGump( new ScalesBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of those scales!" );
                                        m_From.SendGump( new ScalesBoxGump( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 5 )
                        {
                                if ( m_Box.WhiteScales >= 100 )
                                {
                                        m_Box.WhiteScales = ( m_Box.WhiteScales - 100 );
                                        m_From.AddToBackpack( new WhiteScales( 100 ) );
                                        m_From.SendGump( new ScalesBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of those scales!" );
                                        m_From.SendGump( new ScalesBoxGump( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 6 )
                        {
                                if ( m_Box.BlueScales >= 100 )
                                {
                                        m_Box.BlueScales = ( m_Box.BlueScales - 100 );
                                        m_From.AddToBackpack( new BlueScales( 100 ) );
                                        m_From.SendGump( new ScalesBoxGump( m_From, m_Box ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of those scales!" );
                                        m_From.SendGump( new ScalesBoxGump( m_From, m_Box ) );
                                }
                        }
                        if ( info.ButtonID == 7 )
                        {
                                m_From.SendGump( new ScalesBoxGump( m_From, m_Box ) );
                                m_Box.BeginCombine( m_From );
                        }
                        if ( info.ButtonID == 8 )
                        {
                                m_From.SendGump( new ChooseResourceGump( m_From, m_Box ) );
                        }
                }
        }
}

namespace Server.Items
{
        public class ResourceBoxTarget : Target
        {
                private ResourceBox m_Box;

                public ResourceBoxTarget( ResourceBox box ) : base( 18, false, TargetFlags.None )
                {
                        m_Box = box;
                }

                protected override void OnTarget( Mobile from, object targeted )
                {
                        if ( m_Box.Deleted )
                                return;

                        m_Box.EndCombine( from, targeted );
                }
        }
}
