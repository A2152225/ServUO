using System;
using Server;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Gumps
{
	public class ParagonGump : Gump
	{
		public static PlayerMobile pm;
		
		public ParagonGump(PlayerMobile from)
			: base( 0, 0 )
		{
			PlayerMobile pm = from;
			string PPoints =  Convert.ToString(pm.ParagonPoints);

			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(0, 0, 1040, 520, 9270)  ; //9200  
		//	this.AddAlphaRegion(1, 0, 1040, 517);
			this.AddLabel(507, 6,500, @PPoints);
			this.AddLabel(15, 41,500, @"1 Point");
			this.AddLabel(395, 6,500, @"Paragon Points:");
			this.AddLabel(235, 222,500, @"3 Points");
			this.AddLabel(130, 27,500, @"Self");
			this.AddLabel(235, 347,500, @"4 Points");
			this.AddLabel(611, 27,500, @"Pets: Applies to all Actively Controlled Pets");
			this.AddButton(950, 460, 241, 243, (int)Buttons.Close, GumpButtonType.Reply, 0);
			this.AddButton(15, 70, 210, 211, (int)Buttons.P1Str, GumpButtonType.Reply, 0);
			this.AddButton(15, 100, 210, 211, (int)Buttons.P1Dex, GumpButtonType.Reply, 0);
			this.AddButton(15, 130, 210, 211, (int)Buttons.P1Int, GumpButtonType.Reply, 0);
			this.AddButton(15, 180, 210, 211, (int)Buttons.P10Hits, GumpButtonType.Reply, 0);
			this.AddButton(15, 210, 210, 211, (int)Buttons.P10Stam, GumpButtonType.Reply, 0);
			this.AddButton(15, 240, 210, 211, (int)Buttons.P10Mana, GumpButtonType.Reply, 0);
			this.AddButton(15, 290, 210, 211, (int)Buttons.P1HitRegen, GumpButtonType.Reply, 0);
			this.AddButton(15, 320, 210, 211, (int)Buttons.P1StamRegen, GumpButtonType.Reply, 0);
			this.AddButton(15, 350, 210, 211, (int)Buttons.P1ManaRegen, GumpButtonType.Reply, 0);
			this.AddButton(15, 400, 210, 211, (int)Buttons.P1SkillCap, GumpButtonType.Reply, 0);
			this.AddButton(15, 430, 210, 211, (int)Buttons.P5Healing, GumpButtonType.Reply, 0);
			this.AddButton(15, 460, 210, 211, (int)Buttons.P3Carry, GumpButtonType.Reply, 0);
			this.AddLabel(36, 70,500,  "+1 Str ("+Convert.ToString(pm.Paragon_1Str)+")");
			this.AddLabel(36, 130,500, "+1 Int ("+Convert.ToString(pm.Paragon_1Int)+")");
			this.AddLabel(36, 100,500, "+1 Dex ("+Convert.ToString(pm.Paragon_1Dex)+")");
			this.AddLabel(36, 180,500, "+10 Health ("+Convert.ToString(pm.Paragon_10Health)+")");
			this.AddLabel(36, 210,500, "+10 Stamina ("+Convert.ToString(pm.Paragon_10Stamina)+")");
			this.AddLabel(36, 240,500, "+10 Mana ("+Convert.ToString(pm.Paragon_10Mana)+")");
			this.AddLabel(36, 290,500, "+1 Health Regen ("+Convert.ToString(pm.Paragon_1HealthRegen)+")");
			this.AddLabel(36, 320,500, "+1 Stamina Regen ("+Convert.ToString(pm.Paragon_1StaminaRegen)+")");
			this.AddLabel(36, 350,500, "+1 Mana Regen ("+Convert.ToString(pm.Paragon_1ManaRegen)+")");
			this.AddLabel(36, 460,500, "+3 Bag Item Capacity ("+Convert.ToString(pm.Paragon_3MaxCarryCapacity)+")");
			this.AddLabel(36, 430,500, "+5 Increased Healing ("+Convert.ToString(pm.Paragon_5HealingInc)+")");
			this.AddLabel(36, 400,500, "+1 Total Skill Cap ("+Convert.ToString(pm.Paragon_1SkillsCap)+")");
			this.AddLabel(257, 454,500, "+1 Control/Follower Slot ("+Convert.ToString(pm.Paragon_1ControlSlot)+")");
			this.AddButton(236, 66, 210, 211, (int)Buttons.P2Physical, GumpButtonType.Reply, 0);
			this.AddButton(236, 96, 210, 211, (int)Buttons.P2Fire, GumpButtonType.Reply, 0);
			this.AddButton(236, 126, 210, 211, (int)Buttons.P2Cold, GumpButtonType.Reply, 0);
			this.AddLabel(257, 66,500, @"+2 Resist: Physical ("+Convert.ToString(pm.Paragon_2ResistPhysical)+")");
			this.AddLabel(258, 96,500, @"+2 Resist: Fire ("+Convert.ToString(pm.Paragon_2ResistFire)+")");
			this.AddLabel(257, 126,500, @"+2 Resist: Cold ("+Convert.ToString(pm.Paragon_2ResistCold)+")");
			this.AddButton(236, 156, 210, 211, (int)Buttons.P2Poison, GumpButtonType.Reply, 0);
			this.AddButton(236, 186, 210, 211, (int)Buttons.P2Energy, GumpButtonType.Reply, 0);
			this.AddLabel(257, 186,500, @"+2 Resist: Energy ("+Convert.ToString(pm.Paragon_2ResistEnergy)+")");
			this.AddLabel(257, 156,500, @"+2 Resist: Poison ("+Convert.ToString(pm.Paragon_2ResistPoison)+")");
			this.AddButton(236, 249, 210, 211, (int)Buttons.P1SDI, GumpButtonType.Reply, 0);
			this.AddButton(236, 279, 210, 211, (int)Buttons.P1WDI, GumpButtonType.Reply, 0);
			this.AddButton(235, 308, 210, 211, (int)Buttons.P1SSI, GumpButtonType.Reply, 0);
			this.AddLabel(256, 249,500, @"+1 Spell Damage Increase ("+Convert.ToString(pm.Paragon_1SDI)+")");
			this.AddLabel(257, 279,500, @"+1 Weapon Damage Increase ("+Convert.ToString(pm.Paragon_1WDI)+")");
			this.AddLabel(256, 308,500, @"+1 Swing Speed Increase ("+Convert.ToString(pm.Paragon_1SSI)+")");
			this.AddButton(236, 370, 210, 211, (int)Buttons.P1FC, GumpButtonType.Reply, 0);
			this.AddButton(236, 400, 210, 211, (int)Buttons.P1FCR, GumpButtonType.Reply, 0);
			this.AddLabel(257, 370,500, @"+1 Faster Casting Speed ("+Convert.ToString(pm.Paragon_1FC)+")");
			this.AddLabel(257, 400,500, @"+1 Faster Casting Recovery ("+Convert.ToString(pm.Paragon_1FCR)+")");
			this.AddButton(237, 455, 210, 211, (int)Buttons.P1Control, GumpButtonType.Reply, 0);
			this.AddButton(559, 72, 210, 211, (int)Buttons.PetStats, GumpButtonType.Reply, 0);
			this.AddButton(559, 102, 210, 211, (int)Buttons.Pet2DR, GumpButtonType.Reply, 0);
			this.AddLabel(579, 72,500, @"+10 Health, +10 Mana, +10 Stam ("+Convert.ToString(pm.Paragon_PetStats)+")");
			this.AddLabel(580, 102,500, @"+2 Damage Reduction: Reduces Damage After Resists ("+Convert.ToString(pm.Paragon_2PetDamageReduction)+")");
			this.AddButton(559, 188, 210, 211, (int)Buttons.Pet1Damage, GumpButtonType.Reply, 0);
			this.AddLabel(579, 186,500, @"+1 Min & Max Damage ("+Convert.ToString(pm.Paragon_1PetMinMaxDamage)+")");
			this.AddLabel(557, 45,500, @"1 Point");
			this.AddLabel(235, 430,500, @"10 Points");
			this.AddLabel(559, 159,500, @"5 Points");
			this.AddLabel(236, 37,500, @"1 Point");

		}
		
		public enum Buttons
		{
			Close,
			P1Str,
			P1Dex,
			P1Int,
			P10Hits,
			P10Stam,
			P10Mana,
			P1HitRegen,
			P1StamRegen,
			P1ManaRegen,
			P1SkillCap,
			P5Healing,
			P3Carry,
			P2Physical,
			P2Fire,
			P2Cold,
			P2Poison,
			P2Energy,
			P1SDI,
			P1WDI,
			P1SSI,
			P1FC,
			P1FCR,
			P1Control,
			PetStats,
			Pet2DR,
			Pet1Damage,
		}
		
		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
		pm = (PlayerMobile)sender.Mobile;	
			int index = info.ButtonID;
			
try{

			switch ( index )
			{
				case 0: // Apply
				{
					
					//Console.WriteLine("++ Close?");
					pm.CloseGump( typeof( ParagonGump ) );
					
					break;
				}
				case 1: // Set Book Filter
				{
		//	Console.WriteLine("++ Str?");
				if (pm.ParagonPoints>0){ pm.ParagonPoints--;
				pm.RawStr++; pm.Paragon_1Str++;}
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				case 2: // Set Your Filter    
				{
			//		Console.WriteLine("++ Dex");
				if (pm.ParagonPoints>0){ pm.ParagonPoints--;
				pm.RawDex++; pm.Paragon_1Dex++;}
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				case 3: // Clear Filter
				{
			//	Console.WriteLine("++ Int");
				if (pm.ParagonPoints>0){ pm.ParagonPoints--;
				pm.RawInt++; pm.Paragon_1Int++;}
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				case 4: // Clear Filter
				{
			//	Console.WriteLine("++ Int");
				if (pm.ParagonPoints>0){ pm.ParagonPoints--;
				pm.Paragon_10Health++;}
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				case 5: // Clear Filter
				{
			//	Console.WriteLine("++ Int");
				if (pm.ParagonPoints>0){ pm.ParagonPoints--;
				pm.Paragon_10Stamina++;}
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				case 6: // Clear Filter
				{
			//	Console.WriteLine("++ Int");
				if (pm.ParagonPoints>0){ pm.ParagonPoints--;
				pm.Paragon_10Mana++;}
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				case 7: // Clear Filter
				{
			//	Console.WriteLine("++ Int");
				if (pm.ParagonPoints>0){ pm.ParagonPoints--;
				pm.Paragon_1HealthRegen++;}
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				case 8: // Clear Filter
				{
			//	Console.WriteLine("++ Int");
				if (pm.ParagonPoints>0){ pm.ParagonPoints--;
				pm.Paragon_1StaminaRegen++;}
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				case 9: // Clear Filter
				{
			//	Console.WriteLine("++ Int");
				if (pm.ParagonPoints>0){ pm.ParagonPoints--;
				pm.Paragon_1ManaRegen++;}
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				case 10: // Clear Filter
				{
			//	Console.WriteLine("++ Int");
				if (pm.ParagonPoints>0){ pm.ParagonPoints--;
				pm.SkillsCap+=10; pm.Paragon_1SkillsCap++;}
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				case 11: // Clear Filter
				{
			//	Console.WriteLine("++ Int");
				if (pm.ParagonPoints>0){ pm.ParagonPoints--;
				pm.Paragon_5HealingInc++;}
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				case 12: // Clear Filter
				{
			//	Console.WriteLine("++ Int");
				if (pm.ParagonPoints>0){ pm.ParagonPoints--;
				pm.Paragon_3MaxCarryCapacity++;  pm.Backpack.MaxItems =( 125 + (pm.Paragon_3MaxCarryCapacity*3));}
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				case 13: // Clear Filter
				{
			//	Console.WriteLine("++ Int");
				if (pm.ParagonPoints>0){ pm.ParagonPoints--;
				pm.Paragon_2ResistPhysical++;pm.InvalidateProperties();}
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				case 14: // Clear Filter
				{
			//	Console.WriteLine("++ Int");
				if (pm.ParagonPoints>0){ pm.ParagonPoints--;
				pm.Paragon_2ResistFire++;pm.InvalidateProperties();}
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				case 15: // Clear Filter
				{
			//	Console.WriteLine("++ Int");
				if (pm.ParagonPoints>0){ pm.ParagonPoints--;
				pm.Paragon_2ResistCold++;pm.InvalidateProperties();}
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				case 16: // Clear Filter
				{
			//	Console.WriteLine("++ Int");
				if (pm.ParagonPoints>0){ pm.ParagonPoints--;
				pm.Paragon_2ResistPoison++;pm.InvalidateProperties();}
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				case 17: // Clear Filter
				{
			//	Console.WriteLine("++ Int");
				if (pm.ParagonPoints>0){ pm.ParagonPoints--;
				pm.Paragon_2ResistEnergy++; pm.InvalidateProperties();}
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				case 18: // Clear Filter
				{
			//	Console.WriteLine("++ Int");
				if (pm.ParagonPoints>=3){ pm.ParagonPoints-=3;
				pm.Paragon_1SDI++;}
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				case 19: // Clear Filter
				{
			//	Console.WriteLine("++ Int");
				if (pm.ParagonPoints>=3){ pm.ParagonPoints-=3;
				pm.Paragon_1WDI++;}
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				case 20: // Clear Filter
				{
			//	Console.WriteLine("++ Int");
				if (pm.ParagonPoints>=3){ pm.ParagonPoints-=3;
				pm.Paragon_1SSI++;}
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				case 21: // Clear Filter
				{
			//	Console.WriteLine("++ Int");
				if (pm.ParagonPoints>=4){ pm.ParagonPoints-=4;
				pm.Paragon_1FC++;}
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				case 22: // Clear Filter
				{
			//	Console.WriteLine("++ Int");
				if (pm.ParagonPoints>=4){ pm.ParagonPoints-=4;
				pm.Paragon_1FCR++;}
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				case 23: // Clear Filter
				{
			//	Console.WriteLine("++ Int");
				if (pm.ParagonPoints>=10){ pm.ParagonPoints-=10;
				pm.Paragon_1ControlSlot++; pm.FollowersMax++;}
				
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				case 24: // Clear Filter
				{
			//	Console.WriteLine("++ Int");
				if (pm.ParagonPoints>0){ pm.ParagonPoints--;
				pm.Paragon_PetStats++;}
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				case 25: // Clear Filter
				{
			//	Console.WriteLine("++ Int");
				if (pm.ParagonPoints>0){ pm.ParagonPoints--;
				pm.Paragon_2PetDamageReduction++;}
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				case 26: // Clear Filter
				{
			//	Console.WriteLine("++ Int");
				if (pm.ParagonPoints>=5){ pm.ParagonPoints-=5;
				pm.Paragon_1PetMinMaxDamage++;}
				pm.SendGump( new ParagonGump(pm));	

					break;
				}
				default:
				{
				//pm.SendGump( new ParagonGump(pm));						
				pm.SendGump( new ParagonGump(pm));	

	//		Console.WriteLine("++ Default");
					break;
					
				}
			}
}catch (Exception e){
		Console.WriteLine($"Index is {index} and info is {info} ");
		Console.WriteLine($"{e}  Values: Index:{index}  InfO: {info}    button id: {info.ButtonID}");}
/*			Close,  0
			P1Str, 1
			P1Dex,2
			P1Int,3
			P10Hits,4
			P10Stam,5
			P10Mana,6
			P1HitRegen,7
			P1StamRegen,8
			P1ManaRegen,9
			P1SkillCap,10
			P5Healing,11
			P3Carry,12
			P2Physical,13
			P2Fire,14
			P2Cold,15
			P2Poison,16
			P2Energy,17
			P1SDI,18
			P1WDI,19
			P1SSI,20
			P1FC,21
			P1FCR,22
			P1Control,23
			PetStats,24
			Pet2DR,25
			Pet1Damage,26  */
		}

	}
}