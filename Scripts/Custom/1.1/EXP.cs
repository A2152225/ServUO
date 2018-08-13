using System; 
using System.Collections; 
using Server.Network;
using Server.Items; 
using Server.Mobiles;
using Server.Gumps;
using Server.Engines.Harvest;
using Server.Engines.Craft;
using Server.Misc;
using Server.Engines.PartySystem;
using PARTY = Server.Engines.PartySystem.Party;
using Server.Accounting;


namespace Server.Mobiles 
{ 
    	public class Experience
    	{ 
		public static int AvgMonsterExp = 10;//Exp gained from monster same level in normal circumstances (//was 10)
		public static void CheckLevel( PlayerMobile pm ) 
        	{
			PlayerMobile from = pm;
			long Exp = from.EXP;
			int Level = from.LvL;
			long LastLevel = from.LastLevelExp;
			long ExpRequired = ((long)(LastLevel *1.2));     // (int)(((Math.Pow((Level*2.5), 1.5)*1.5)+20)*AvgMonsterExp)+LastLevel;
			if (ExpRequired == 0)
				ExpRequired = 100;
			
			//from.SendMessage("You have {0} XP, and needed {1} XP to level up last level, this level you need {2}: check 1",from.EXP,from.LastLevelExp,ExpRequired);
			
			if (LastLevel == 0)
				ExpRequired = 100;
			if (from.LvL == 1 || from.PrestigeLvl == 1)
						ExpRequired = 220;
					
			if ( Exp >= ExpRequired )
			{
				
			
				
				
				
					//from.EXP -= ExpRequired;
		
				
				
				

			if ( Level < from.MaxLvl )		//changed 12-12-14 breaker, MaxLvl  prop	/////// Changed from 500 on 7-12-07 {RE} to 2000 ////
				{

					
					
					LevelUp(from);
					


						if (from.LvL == from.MaxLvl && from.PrestigeLvl == 0)	
						{
							
						from.LastLevelExp = 0;
						from.EXP = 0;
							from.SendMessage("You have reached the maximum level at this time, additional experience will go into paragon levels.");
						}
				
				//	CheckLevel(from) ;//Re-triggers levelup if exp is more than 1 level ahead
				//ExpRequired = ((long)(ExpRequired * 1.2));

				
				}

			if (from.LvL == from.MaxLvl)
				{
				
				from.PrestigeLvl++;
				
					Effects.SendTargetParticles( from, 14170, 1, 17, 2924-1, 2, 0, EffectLayer.Waist, 0 );//Blue/gold sparkles
			from.Say( "*Has gained a paragon level*" ); 
			from.PlaySound( 61 );//Flute
			from.Hits = from.HitsMax;
			from.Mana = from.ManaMax;
			from.Stam = from.StamMax;
			pm.InvalidateProperties();
		//	CheckLevel(from) ;//Re-triggers levelup if exp is more than 1 level ahead
	//	ExpRequired = ((long)(ExpRequired * 1.2));

				}
		Exp = Exp - ExpRequired;
		from.LastLevelExp = ExpRequired;
			CheckLevel(from) ;//Re-triggers levelup if exp is more than 1 level ahead
				
			}
			
			if ( from.ShowExpBar == true )
				from.SendGump( new ExpBarGump(from));
		}
		public static void LevelUp( PlayerMobile pm )
		{
			PlayerMobile from = pm;
			
			from.LvL += 1;
			
			   if (pm.Young && pm.LvL == 50)
            {
                Account acc = pm.Account as Account;

                if (acc != null)
                    acc.RemoveYoungStatus(0);
				pm.Young = false;
				pm.YoungSaves = 0;
				pm.SendMessage("You have reached a point where you are no longer considered to be young, you will no longer have the protection from death that has been afforded to you up till this point. Good Luck.");
					
			}
			
			if (from.LvL > from.OldMaxLvl && from.LvL <= from.MaxLvl)
			from.StatCap += 8; //= 1200 + from.LvL;  //stat cap should be set to 1200 base on character creation, no need to redefine with every level, just increase
			
			from.RawStr ++;
			from.RawStr ++;
			from.RawDex ++;
			from.RawDex ++;
			from.RawInt ++;
			from.RawInt ++;
			
			
			int Level = from.LvL;

			if ( Level >= 1 && from.LvL > from.OldMaxLvl && from.LvL <= from.MaxLvl)
				from.SkillsCap += 80;

			if ( Level <= 500 )
			{
				from.SP += 5;
			}

			if ( Level > 500 )
			{
				from.SP += 3;
				if ( from.LvL % 5 == 0)
					from.SP += 1;
			}
			
										
			/*if ( Level >= 100 )
				from.NameMod = "Elder "+ from.RawName;
			if ( Level >= 300 )
				from.NameMod = "Veteran "+ from.RawName;
			if ( Level >= 500 )
				from.NameMod = "The Elite "+ from.RawName;
			if ( Level >= 1100 )
				from.NameMod = "The Eternal "+ from.RawName;
			if ( Level >= 1800 )
				from.NameMod = "The Ancient "+ from.RawName;

			if ( Level == 100 )
				from.SendMessage( "You are now an Elder of your race." );
			if ( Level == 300 )
				from.SendMessage( "You are now a Veteran Player." );
			if ( Level == 500 )
				from.SendMessage( "You are now an Elite Player." );
			if ( Level == 1100 )
				from.SendMessage( "You are now an Eternal Player." );
			if ( Level == 1800 )
				from.SendMessage( "You are now an Ancient Player." );

			if ( Level >= 1500 )
			{
					if ( pm.Following == Following.Good )
						from.Title = "The Enlightened";
					else if ( pm.Following == Following.Evil )
						from.Title = "The Vicious";
			}
			if ( Level >= 750 )
			{
					if ( pm.Following == Following.Good )
						from.Title = "The High";
					else if ( pm.Following == Following.Evil )
						from.Title = "The Chaotic";
			}
			else if ( Level >= 500 )
			{
					if ( from.Following == Following.Good )
						from.Title = "The Noble";
					else if ( from.Following == Following.Evil )
						from.Title = "The Evil";
			}*/
			from.SendMessage("You have gained a level! You are now level " + Level);
			if ( from.SP > 0 )
			{
				from.SendMessage("You have " + from.SP + " specialization points to allocate");
				from.SendGump( new StatGump(from) );
			}

			Effects.SendTargetParticles( from, 14170, 1, 17, 2924-1, 2, 0, EffectLayer.Waist, 0 );//Blue/gold sparkles
			from.Say( "*Has gained a level*" ); 
			from.PlaySound( 61 );//Flute
			from.Hits = from.HitsMax;
			from.Mana = from.ManaMax;
			from.Stam = from.StamMax;
			pm.InvalidateProperties();

		//	CheckLevel(from) ;//Re-triggers levelup if exp is more than 1 level ahead
		}
		public static void MonsterExp( Mobile killer, Mobile monster, Point3D loc, double Percent, long XPAMOUNT )
		{
			BaseCreature Monster = monster as BaseCreature;
			PlayerMobile from = null;
			if ( killer is PlayerMobile )
				from = killer as PlayerMobile;
			else if ( killer is BaseCreature )
			{
				BaseCreature c = (BaseCreature)killer;
				if ( c.ControlMaster is PlayerMobile )
				{
					from = (PlayerMobile)c.ControlMaster;
				}
			}
			if ( from != null && Monster != null )
			{
				int Level = from.LvL;
				long LastLevel = from.LastLevelExp;
				long ExpRequired;
				if (LastLevel == 0)
				 ExpRequired = 100;
			 else
				  ExpRequired = ((long)(LastLevel*1.2));
				int mapb = 0;
				int karma;

				if ( Monster.Karma <= 0 )
					karma = Monster.Karma * -1;
				else
					karma = Monster.Karma;

			//	int LevelDifference = Monster.Level - from.LvL;
				long ExpGained;
				int monsterstats = ( ( Monster.Fame + karma ) / 5 ) + ( Monster.RawStatTotal * 3 ) + ( ( Monster.HitsMaxSeed + Monster.StamMaxSeed + Monster.ManaMaxSeed ) * 2 ) + ( Monster.DamageMax - Monster.DamageMin ) + ( ( Monster.ColdResistSeed + Monster.FireResistSeed + Monster.EnergyResistSeed + Monster.PhysicalResistanceSeed + Monster.PoisonResistSeed ) * 2 ) + Monster.VirtualArmor;   
															//5								//3																				//3																																																//3
				if ( Monster.MinTameSkill > 0 )
					monsterstats += (int)Monster.MinTameSkill;

	/*			if ( Monster.Level <= 0 )
					if ( from.LvL <= 0 )
						ExpGained = monsterstats;
					else
						ExpGained = monsterstats / from.LvL;
				else
					if ( from.LvL <= 0 )
						ExpGained = ( Monster.Level * monsterstats );
					else
						*/
					
						ExpGained = (  monsterstats );
					ExpGained = XPAMOUNT;
				Point3D DeathLoc = loc;


				if ( ExpGained <= 0 )
					ExpGained = 1;
				
				if ( !Utility.InRange( from.Location, DeathLoc, 18 ))
				{
					ExpGained = (long)(ExpGained/2);
				}
				
				// party up for more xp
				//Party party = Engines.PartySystem.Party.Get(from);
				Party p = from.Party as Party;

			if ( p != null )
			{
			double xpbonus =1;
				for ( int i = 0; i < p.Members.Count; ++i )
				{
					PartyMemberInfo pmi = (PartyMemberInfo)p.Members[i];
					if ( pmi != null )
					{
						Mobile member = pmi.Mobile;

						if ( member != null && Utility.InRange( member.Location, from.Location, 18 ) )
							xpbonus += .05;
					}
				}
				
				ExpGained = (long)(ExpGained * xpbonus);
			}
				

				foreach( Item item in from.Backpack.Items )
				{
					if( item is EXPDeed ) 
					{ 
						ExpGained += ExpGained / 5;
					}
				}

				long minexp = 0;
				long maxexp = 0;

	/*			if ( LevelDifference > 0 )
				{
					minexp = 5+(ExpGained/9);  // Changed from 60 to 9 by RE 6-19-07
					maxexp = 5+(ExpGained/5);  // Changed from 40 to 5 by RE 6-19-07
				}
				else
				{
					minexp = 5+(ExpGained/15);  // Changed from 120 to 15 by RE 6-19-07
					maxexp = 5+(ExpGained/9);  // Changed from 100 to 9 by RE 6-19-07
				}
		*/	
			/*	Paragon/Prestige levels added - no more XP cap
			if ( from.LvL >= from.MaxLvl )          // Commented Section out until can be further tested 6-19-07  HUNTER LOOK AT THIS PLEASE LET ME KNOW IF WE CAN USE IT!!
				 {
					minexp *= 0;
					maxexp *= 0;
				 }
				 */
			
			
				
	//if (from.Map != Map.TerMur)
	 mapb = Tweaks.GetMapXPMod(from.Map);
				 
				long finalexp = (long)((double)( Utility.RandomMinMax( (int)minexp, (int)maxexp ) )*Percent*(Tweaks.XPMod+mapb));

			/*	if ( finalexp > ( ExpRequired - LastLevel ) )
				{
					finalexp = (long)( ( ExpRequired - LastLevel ) / 2 );
				}
*/
				Region re = Region.Find( from.Location, from.Map );
				string regname = re.ToString().ToLower();
				
		
			/*	if ( Monster is CBogle || Monster is CMongbat || Monster is CGhoul || Monster is CShade || Monster is CWraith || Monster is CSpectre || Monster is CSnake || Monster is CGiantRat || Monster is CWisp || Monster is CMummy || Monster is CGargoyle || Monster is CBoneMagi || Monster is CHellHound || Monster is CSkeletalMage || Monster is CSlime || Monster is CDireWolf || Monster is CRatman || Monster is CRottingCorpse || Monster is CStoneGargoyle || Monster is CGiantSpider || Monster is CShadowWisp || Monster is CLizardman || Monster is CTerathanWarrior || Monster is CTerathanDrone || Monster is CSilverSerpent || Monster is CLavaLizard || Monster is CPixie || Monster is CFireGargoyle || Monster is CTerathanMatriarch || Monster is CLichLord || Monster is CLich || Monster is COphidianMatriarch || Monster is CDreadSpider || Monster is COphidianWarrior || Monster is CBoneKnight || Monster is CSkeletalKnight || Monster is CDaemon || Monster is CHarpy || Monster is CImp || Monster is CCentaur || Monster is CRatmanArcher || Monster is COphidianArchmage || Monster is CRatmanMage || Monster is CScorpion || Monster is COphidianMage || Monster is CTerathanAvenger || Monster is CPoisonElemental || Monster is CDrake || Monster is COphidianKnight || Monster is CDragon || Monster is CUnicorn || Monster is CKirin || Monster is CEtherealWarrior || Monster is CSuccubus || Monster is CSerpentineDragon || Monster is Machine || Monster is BrokenMechanicalAssembly || Monster is MechanicalAssembly || Monster is BrokenMachine  || Monster is EarthSnake || Monster is GiantEarthSerpent || Monster is EarthDrake || Monster is EarthDragon || Monster is FireSnake || Monster is GiantFireSerpent || Monster is FireDrake || Monster is FireDragon || Monster is WaterSnake || Monster is GiantWaterSerpent || Monster is WaterDrake || Monster is WaterDragon || Monster is WindSnake || Monster is GiantWindSerpent || Monster is WindDrake || Monster is WindDragon )*/
			if (regname == "championspawnregion" ) 
			{
				if (Tweaks.CXPMod == 0)
					finalexp = 0;
				else
					finalexp /= (100/Tweaks.CXPMod);//3; //  2/3rds exp for champ spawns, now 1/20th// now 20%
					//finalexp *= 2; // was *=2, want people out in the world more
					if (finalexp <= 10)
						finalexp = 10;
			}


				if ( Monster.IsParagon )
					finalexp *= 2;

				from.EXP += ExpGained ;//finalexp;
				
			CheckLevel(from) ;//Re-triggers levelup if exp is more than 1 level ahead
			}
		}
		public static void HarvestExp( Mobile m, HarvestResource resource, bool success )
		{
			PlayerMobile from = m as PlayerMobile;
			if ( from != null )
			{
				HarvestResource Res = resource;
				int ExpGained = new int();
				if (Res.Types[0] == typeof(IronOre))
				{
					ExpGained = 3;
				}
				if (Res.Types[0] == typeof(DullCopperOre))
				{
					ExpGained = 5;
				}
				if (Res.Types[0] == typeof(ShadowIronOre))
				{
					ExpGained = 6;
				}
				if (Res.Types[0] == typeof(CopperOre))
				{
					ExpGained = 7;
				}
				if (Res.Types[0] == typeof(BronzeOre))
				{
					ExpGained = 8;
				}
				if (Res.Types[0] == typeof(GoldOre))
				{
					ExpGained = 9;
				}
				if (Res.Types[0] == typeof(AgapiteOre))
				{
					ExpGained = 10;
				}
				if (Res.Types[0] == typeof(VeriteOre))
				{
					ExpGained = 12;
				}
				if (Res.Types[0] == typeof(ValoriteOre))
				{
					ExpGained = 13;
				}
			/*	if (Res.Types[0] == typeof(BlazeOre))
				{
					ExpGained = 12;
				}
				if (Res.Types[0] == typeof(IceOre))
				{
					ExpGained = 13;
				}
				if (Res.Types[0] == typeof(ToxicOre))
				{
					ExpGained = 14;
				}
				if (Res.Types[0] == typeof(ElectrumOre))
				{
					ExpGained = 15;
				}
				if (Res.Types[0] == typeof(MoonstoneOre))
				{
					ExpGained = 16;
				}
				if (Res.Types[0] == typeof(BloodstoneOre))
				{
					ExpGained = 16;
				}				
				if (Res.Types[0] == typeof(PlatinumOre))
				{
					ExpGained = 16;
				}
			*/	if (Res.Types[0] == typeof(Log))
				{
					ExpGained = 3;
				}
				if (Res.Types[0] == typeof(OakLog))
				{
					ExpGained = 4;
				}
			/*	if (Res.Types[0] == typeof(MapleLog))
				{
					ExpGained = 4;
				}
				if (Res.Types[0] == typeof(PineLog))
				{
					ExpGained = 5;
				}
			*/	if (Res.Types[0] == typeof(AshLog))
				{
					ExpGained = 6;
				}
				if (Res.Types[0] == typeof(YewLog))
				{
					ExpGained = 7;
				}
				if (Res.Types[0] == typeof(HeartwoodLog))
				{
					ExpGained = 8;
				}
				if (Res.Types[0] == typeof(FrostwoodLog))
				{
					ExpGained = 9;
				}
			/*	if (Res.Types[0] == typeof(BlueBarkLog))
				{
					ExpGained = 10;
				}
				if (Res.Types[0] == typeof(BlackBarkLog))
				{
					ExpGained = 11;
				}
			*/	if (Res.Types[0] == typeof(BloodwoodLog))
				{
					ExpGained = 12;
				}
			/*	if (Res.Types[0] == typeof(SwampLog))
				{
					ExpGained = 13;
				}
				if (Res.Types[0] == typeof(ElvenLog))
				{
					ExpGained = 14;
				}
			*/	if (Res.Types[0] == typeof(Fish))
				{
					ExpGained = Utility.RandomMinMax(1,4);//to vary fishing a bit
				}
				if ( success == false )
					ExpGained -= ExpGained/2;
				if ( ExpGained > 0 )
				{
				int finalexp = (ExpGained * Tweaks.XPMod * 2);  //12
					from.EXP += (ExpGained * Tweaks.XPMod * 2) ; //4  //12
				//	GearUp(from, finalexp);
				}
			}
		}
		public static void CraftExp( Mobile m, int quality, bool failed, CraftItem item)
		{
			PlayerMobile from = m as PlayerMobile;
			if ( from != null )
			{
				CraftRes Res = null;
				int ResExp = 0;
				for ( int i = 0; i < item.Resources.Count; ++i )
				{
					Res = item.Resources.GetAt(i);
				}
				int ExpGained = 0;
				if ( Res != null )
				{
					if (Res.ItemType == typeof(IronIngot))
					{
						ResExp = 4;
					}
					else if (Res.ItemType == typeof(Log))
					{
						ResExp = 5;
					}
					else if (Res.ItemType == typeof(Garlic))
					{
						ResExp = 4;
					}
					else if (Res.ItemType == typeof(BlackPearl))
					{
						ResExp = 4;
					}
					else if (Res.ItemType == typeof(Bloodmoss))
					{
						ResExp = 4;
					}
					else if (Res.ItemType == typeof(Ginseng))
					{
						ResExp = 4;
					}
					else if (Res.ItemType == typeof(MandrakeRoot))
					{
						ResExp = 4;
					}
					else if (Res.ItemType == typeof(Nightshade))
					{
						ResExp = 4;
					}
					else if (Res.ItemType == typeof(SulfurousAsh))
					{
						ResExp = 4;
					}
					else if (Res.ItemType == typeof(SpidersSilk))
					{
						ResExp = 4;
					}
					else if (Res.ItemType == typeof(Bottle))
					{
						ResExp = 4;
					}
					else if (Res.ItemType == typeof(Shaft))
					{
						ResExp = 2;
					}
					else if (Res.ItemType == typeof(Feather))
					{
						ResExp = 3;
					}
					else if (Res.ItemType == typeof(BlankMap))
					{
						ResExp = 10;
					}
					else if (Res.ItemType == typeof(SackFlour))
					{
						ResExp = 4;
					}
					else if (Res.ItemType == typeof(Dough))
					{
						ResExp = 3;
					}
					else if (Res.ItemType == typeof(CakeMix))
					{
						ResExp = 10;
					}
					else if (Res.ItemType == typeof(CookieMix))
					{
						ResExp = 8;
					}
					else if (Res.ItemType == typeof(RawBird))
					{
						ResExp = 3;
					}
					else if (Res.ItemType == typeof(RawChickenLeg))
					{
						ResExp = 3;
					}
					else if (Res.ItemType == typeof(RawFishSteak))
					{
						ResExp = 2;
					}
					else if (Res.ItemType == typeof(RawLambLeg))
					{
						ResExp = 3;
					}
					else if (Res.ItemType == typeof(RawRibs))
					{
						ResExp = 3;
					}
					else if (Res.ItemType == typeof(Sand))
					{
						ResExp = 15;
					}
					else if (Res.ItemType == typeof(Granite))
					{
						ResExp = 15;
					}
					else if (Res.ItemType == typeof(Board))
					{
						ResExp = 5;
					}
					else if (Res.ItemType == typeof(BatWing))
					{
						ResExp = 3;
					}
					else if (Res.ItemType == typeof(GraveDust))
					{
						ResExp = 3;
					}
					else if (Res.ItemType == typeof(DaemonBlood))
					{
						ResExp = 3;
					}
					else if (Res.ItemType == typeof(NoxCrystal))
					{
						ResExp = 3;
					}
					else if (Res.ItemType == typeof(PigIron))
					{
						ResExp = 3;
					}
					else if (Res.ItemType == typeof(BlankScroll))
					{
						ResExp = 5;
					}
					else if (Res.ItemType == typeof(Cloth))
					{
						ResExp = 3;
					}
					else if (Res.ItemType == typeof(Leather))
					{
						ResExp = 4;
					}
					else if (Res.ItemType == typeof(Bone))
					{
						ResExp = 4;
					}
					else if (Res.ItemType == typeof(DaemonBone))
					{
						ResExp = 5;
					}
					else if (Res.ItemType == typeof(DaemonBlood))
					{
						ResExp = 3;
					}
					else if (Res.ItemType == typeof(Cotton))
					{
						ResExp = 3;
					}
					else if (Res.ItemType == typeof(Keg))
					{
						ResExp = 20;
					}
					else if (Res.ItemType == typeof(Axle))
					{
						ResExp = 3;
					}
					else if (Res.ItemType == typeof(ClockFrame))
					{
						ResExp = 14;
					}
					else if (Res.ItemType == typeof(AxleGears))
					{
						ResExp = 3;
					}
					else if (Res.ItemType == typeof(SextantParts))
					{
						ResExp = 4;
					}
					else if (Res.ItemType == typeof(BolaBall))
					{
						ResExp = 5;
					}
					else if (Res.ItemType == typeof(GateTravelScroll))
					{
						ResExp = 7;
					}
					else if (Res.ItemType == typeof(RecallScroll))
					{
						ResExp = 7;
					}
					else if (Res.ItemType == typeof(RecallRune))
					{
						ResExp = 8;
					}
					else
						ResExp = 3;
					ExpGained = ResExp * Res.Amount; /// 3;
					ExpGained *= (1 +(2 * Tweaks.XPMod));  //4
			
					

					if ( quality == 0 )//low quality
						ExpGained = (int)((decimal.Divide(ExpGained,4))*2);
					else if ( quality == 2 )//exceptional
						ExpGained = (int)((decimal.Divide(ExpGained,4))*4);
					if ( failed == true )//Quater exp on fail
						ExpGained = (int)(decimal.Divide(ExpGained,4));
					if ( ExpGained > 0 )
					{
				if (ExpGained > 6000)
					ExpGained = 6000;
					
						from.EXP += ExpGained;
			//			GearUp(from,ExpGained);
					}
				}
			}
		}
		
		
	/*public static void GearUp( Mobile from, int finalxp)
	{
	   if ( from == null )
               return;

                        BaseWeapon wep = from.FindItemOnLayer( Layer.FirstValid ) as BaseWeapon;
                        if ( wep == null )
                                wep = from.FindItemOnLayer( Layer.OneHanded ) as BaseWeapon;
                        if ( wep == null )
                                wep = from.FindItemOnLayer( Layer.TwoHanded ) as BaseWeapon;

                        if ( from.Backpack != null )
                        {
                                
                                if ( wep != null )
                                {
                                        int ExpRequired = (int)(((Math.Pow((wep.Level*1.5), 1.5)*1.5)+20)*Experience.AvgMonsterExp)+wep.LastLevelExp;

                                   
								   
                                        int ExpGained = finalxp;
                                 
										

                                        if ( ExpGained <= 0 )
                                                ExpGained = 1;
                                 
								 
								 
                                        foreach( Item item in from.Backpack.Items )
                                        {
                                                if( item is EXPDeed ) 
                                                { 
                                                        ExpGained += ExpGained / 5;
                                                }
                                        }

                                        int minexp = 0;
                                        int maxexp = 0;

                                      
                                                minexp = 5+(ExpGained/30);
                                                maxexp = 5+(ExpGained/25);
                                        

                                        if ( wep.Level >= 200 )
                                        {
                                                minexp *= 5;
                                                maxexp *= 5;
                                        }
                                        else if ( wep.Level >= 150 )
                                        {
                                                minexp *= 4;
                                                maxexp *= 4;
                                        }
                                        else if ( wep.Level >= 100 )
                                        {
                                                minexp *= 3;
                                                maxexp *= 3;
                                        }
                                        else if ( wep.Level >= 50 )
                                        {
                                                minexp *= 2;
                                                maxexp *= 2;
                                        }
										

                                        int finalexp = ( Utility.RandomMinMax( minexp, maxexp ) );
						
                                        if ( finalexp > ( ExpRequired - wep.LastLevelExp ) )
                                        {
                                                finalexp = (int)( ( ExpRequired - wep.LastLevelExp ) / 2 );
                                        }

                                        if ( wep.CanGainLevels )
                                        {
                                                wep.Exp += finalexp;
                                                SetWeaponLevel.CheckLevel( wep, from );
                                        }
                                }
                                for ( int i = 0; i < from.Items.Count; ++i )
                                {
                                        Item item = (Item)from.Items[i];

                                        if ( item is BaseArmor && item.Parent == from )
                                        {
                                                BaseArmor armor = (BaseArmor)item;

                                                int ExpRequired = (int)(((Math.Pow((armor.Level*1.5), 1.5)*1.5)+20)*Experience.AvgMonsterExp)+armor.LastLevelExp;

                                        
                                                int ExpGained = finalxp;
                                                
                                                if ( ExpGained <= 0 )
                                                        ExpGained = 1;
												

                                                foreach( Item item2 in from.Backpack.Items )
                                                {
                                                        if( item2 is EXPDeed ) 
                                                        { 
                                                                ExpGained += ExpGained / 5;
                                                        }
                                                }

                                                int minexp = 0;
                                                int maxexp = 0;

                                           
                                                
                                                        minexp = 5+(ExpGained/30);
                                                        maxexp = 5+(ExpGained/25);
                                                

                                                if ( armor.Level >= 200 )
                                                {
                                                        minexp *= 5;
                                                        maxexp *= 5;
                                                }
                                                else if ( armor.Level >= 150 )
                                                {
                                                        minexp *= 4;
                                                        maxexp *= 4;
                                                }
                                                else if ( armor.Level >= 100 )
                                                {
                                                        minexp *= 3;
                                                        maxexp *= 3;
                                                }
                                                else if ( armor.Level >= 50 )
                                                {
                                                        minexp *= 2;
                                                        maxexp *= 2;
                                                }

                                                int finalexp = ( Utility.RandomMinMax( minexp, maxexp ) );

                                                if ( finalexp > ( ExpRequired - armor.LastLevelExp ) )
                                                {
                                                        finalexp = (int)( ( ExpRequired - armor.LastLevelExp ) / 2 );
                                                }

                                                if ( armor.CanGainLevels )
                                                {
                                                        armor.Exp += finalexp;
                                                        SetArmorLevel.CheckLevel( armor, from );
                                                }
                                        }
                                        else if ( item is BaseJewel && item.Parent == from )
                                        {
                                                BaseJewel jewelry = (BaseJewel)item;

                                                int ExpRequired = (int)(((Math.Pow((jewelry.Level*1.5), 1.5)*1.5)+20)*Experience.AvgMonsterExp)+jewelry.LastLevelExp;

                                              

                                               
                                                int ExpGained = finalxp;
                                              
											  
                                                if ( ExpGained <= 0 )
                                                        ExpGained = 1;
                                              
											  

                                                foreach( Item item2 in from.Backpack.Items )
                                                {
                                                        if( item2 is EXPDeed ) 
                                                        { 
                                                                ExpGained += ExpGained / 5;
                                                        }
                                                }

                                                int minexp = 0;
                                                int maxexp = 0;

                                               
                                                        minexp = 5+(ExpGained/30);
                                                        maxexp = 5+(ExpGained/25);
                                        

                                                if ( jewelry.Level >= 200 )
                                                {
                                                        minexp *= 5;
                                                        maxexp *= 5;
                                                }
                                                else if ( jewelry.Level >= 150 )
                                                {
                                                        minexp *= 4;
                                                        maxexp *= 4;
                                                }
                                                else if ( jewelry.Level >= 100 )
                                                {
                                                        minexp *= 3;
                                                        maxexp *= 3;
                                                }
                                                else if ( jewelry.Level >= 50 )
                                                {
                                                        minexp *= 2;
                                                        maxexp *= 2;
                                                }

                                                int finalexp = ( Utility.RandomMinMax( minexp, maxexp ) );

                                                if ( finalexp > ( ExpRequired - jewelry.LastLevelExp ) )
                                                {
                                                        finalexp = (int)( ( ExpRequired - jewelry.LastLevelExp ) / 2 );
                                                }

                                                if ( jewelry.CanGainLevels)
                                                {
                                                        jewelry.Exp += finalexp;
                                                        SetJewelryLevel.CheckLevel( jewelry, from );
                                                }
                                        }
										 else if ( item is BaseClothing && item.Parent == from )
                                        {
                                                BaseClothing Clothing = (BaseClothing)item;

                                                int ExpRequired = (int)(((Math.Pow((Clothing.Level*1.5), 1.5)*1.5)+20)*Experience.AvgMonsterExp)+Clothing.LastLevelExp;

                                              

                                               
                                                int ExpGained = finalxp;
                                              
											  
                                                if ( ExpGained <= 0 )
                                                        ExpGained = 1;
                                              
											  

                                                foreach( Item item2 in from.Backpack.Items )
                                                {
                                                        if( item2 is EXPDeed ) 
                                                        { 
                                                                ExpGained += ExpGained / 5;
                                                        }
                                                }

                                                int minexp = 0;
                                                int maxexp = 0;

                                               
                                                        minexp = 5+(ExpGained/30);
                                                        maxexp = 5+(ExpGained/25);
                                        

                                                if ( Clothing.Level >= 200 )
                                                {
                                                        minexp *= 5;
                                                        maxexp *= 5;
                                                }
                                                else if ( Clothing.Level >= 150 )
                                                {
                                                        minexp *= 4;
                                                        maxexp *= 4;
                                                }
                                                else if ( Clothing.Level >= 100 )
                                                {
                                                        minexp *= 3;
                                                        maxexp *= 3;
                                                }
                                                else if ( Clothing.Level >= 50 )
                                                {
                                                        minexp *= 2;
                                                        maxexp *= 2;
                                                }

                                                int finalexp = ( Utility.RandomMinMax( minexp, maxexp ) );

                                                if ( finalexp > ( ExpRequired - Clothing.LastLevelExp ) )
                                                {
                                                        finalexp = (int)( ( ExpRequired - Clothing.LastLevelExp ) / 2 );
                                                }

                                                if ( Clothing.CanGainLevels)
                                                {
                                                        Clothing.Exp += finalexp;
                                                        SetClothingLevel.CheckLevel( Clothing, from );
                                                }
                                        }
                                }
                        }
	
	
	
	}
		
		
	} */ 
}
	public class ExpBarGump : Gump
	{
		public ExpBarGump( PlayerMobile m ) : base( 20,20 )
		{
			Dragable = true;
			Closable=false;
			PlayerMobile from = m;
			from.CloseGump( typeof( ExpBarGump ) );

			int Level = from.LvL;
			long LastLevel = from.LastLevelExp;
			long ExpRequired = ((long)(LastLevel*1.2));//(long)(((Math.Pow((Level*1.5), 1.5)*1.5)+20)*Experience.AvgMonsterExp)+LastLevel;
				if (ExpRequired == 0 || (LastLevel == 0) )
				{
				 ExpRequired = 100;
				  if ( from.HorizontalExpBar == false )
					{
						AddImage(0, 0, 9741);
						AddImageTiled(0, 0, 8, 96-(int)(decimal.Divide(1,100)*96), 9740);
					}
						else
					{
						AddImage(0, 0, 9750);
						AddImageTiled(0, 0, (int)(decimal.Divide(1,100)*96), 8, 9751);
					}
				}
			else if ( from.HorizontalExpBar == false )
			{
				AddImage(0, 0, 9741);
				AddImageTiled(0, 0, 8, 96-(int)(decimal.Divide(from.EXP-from.LastLevelExp,ExpRequired-from.LastLevelExp)*96), 9740);
			}
			else
			{
				AddImage(0, 0, 9750);
				AddImageTiled(0, 0, (int)(decimal.Divide(from.EXP-from.LastLevelExp,ExpRequired-from.LastLevelExp)*96), 8, 9751);
			}
		}
	}
	public class StatGump : Gump
	{
		public StatGump(PlayerMobile m) : base( 50,50 )
		{
			PlayerMobile from = m;
			from.CloseGump( typeof( StatGump ) );
			AddButton(0, 0, 2643, 2642, 1, GumpButtonType.Reply, 0);
		}
		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile m = state.Mobile;
			PlayerMobile from = m as PlayerMobile;
			
			if ( from != null )
			switch ( info.ButtonID )
			{
				case 0:
				{
					break;
				}
				case 1:
				{
					from.Send( new PlaySound( 81, from.Location ) );
					from.SendGump( new CharInfoGump(from) ); 
					break;
				}
			}
		}
	}
	
	
}

		
		
		
		
		
		
		
		