// Updated Experience.cs — replaced large if/else chains with dictionary lookups,
// made EXPDeed stacking multiplicative (each deed multiplies xp by 1.2),
// kept DanDollar award constant per XP event (5 ticks == 0.00005).
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
using Server.Custom; // DanDollarManager

namespace Server.Mobiles
{
	public class Experience
	{
		public static int AvgMonsterExp = 10; // Exp gained from monster same level in normal circumstances

		// Small constant: 0.00005 DanDollar == 5 ticks when TicksPerDollar == 100000
		private const long DanDollarTicksPerXPEvent = 5L;

		// Harvest base values (without oremodifier). Ores listed here will be multiplied by oremodifier in method.
		private static readonly Dictionary<Type, int> HarvestBaseExpMap = new Dictionary<Type, int>()
		{
			// Ores (base values)
			{ typeof(IronOre), 3 },
			{ typeof(DullCopperOre), 5 },
			{ typeof(ShadowIronOre), 6 },
			{ typeof(CopperOre), 7 },
			{ typeof(BronzeOre), 8 },
			{ typeof(GoldOre), 9 },
			{ typeof(AgapiteOre), 10 },
			{ typeof(VeriteOre), 12 },
			{ typeof(ValoriteOre), 13 },
			{ typeof(BlazeOre), 12 },
			{ typeof(IceOre), 13 },
			{ typeof(ToxicOre), 14 },
			{ typeof(ElectrumOre), 15 },
			{ typeof(PlatinumOre), 16 },
			{ typeof(BariteOre), 16 },
			{ typeof(WulfeniteOre), 16 },
			{ typeof(DragoniteOre), 16 },
			{ typeof(BunteriteOre), 16 },
			{ typeof(PineiteOre), 16 },
			{ typeof(SamiteOre), 16 },
			{ typeof(ToberiteOre), 16 },
			{ typeof(LisiteOre), 16 },
			{ typeof(MariteOre), 16 },
			{ typeof(TealOre), 18 },
			{ typeof(RoyaliteOre), 17 },
			{ typeof(DaniteOre), 18 },

			// Logs / generic resources
			{ typeof(Log), 3 },
			{ typeof(OakLog), 4 },
			{ typeof(AshLog), 6 },
			{ typeof(YewLog), 7 },
			{ typeof(HeartwoodLog), 8 },
			{ typeof(BloodwoodLog), 10 },
			{ typeof(FrostwoodLog), 11 },
			{ typeof(EbonyLog), 12 },
			{ typeof(BambooLog), 13 },
			{ typeof(PurpleHeartLog), 14 },
			{ typeof(RedwoodLog), 15 },
			{ typeof(PetrifiedLog), 16 },

			// Fishing, etc
			{ typeof(Fish), 20 }, // previous code used random 1-100; keep a reasonable base if used

			// Granites
			{ typeof(Granite), 15 },
			{ typeof(DullCopperGranite), 15 },
			{ typeof(ShadowIronGranite), 16 },
			{ typeof(CopperGranite), 17 },
			{ typeof(BronzeGranite), 18 },
			{ typeof(GoldGranite), 19 },
			{ typeof(AgapiteGranite), 20 },
			{ typeof(VeriteGranite), 21 },
			{ typeof(ValoriteGranite), 22 },
			{ typeof(BlazeGranite), 23 },
			{ typeof(IceGranite), 24 },
			{ typeof(ToxicGranite), 25 },
			{ typeof(ElectrumGranite), 26 },
			{ typeof(PlatinumGranite), 27 },
			{ typeof(TealGranite), 28 },
			{ typeof(RoyaliteGranite), 28 },
			{ typeof(DaniteGranite), 30 },

			// Boards
			{ typeof(Board), 5 },
			{ typeof(OakBoard), 6 },
			{ typeof(AshBoard), 7 },
			{ typeof(YewBoard), 8 },
			{ typeof(HeartwoodBoard), 9 },
			{ typeof(BloodwoodBoard), 10 },
			{ typeof(FrostwoodBoard), 11 },
			{ typeof(EbonyBoard), 12 },
			{ typeof(BambooBoard), 13 },
			{ typeof(PurpleHeartBoard), 14 },
			{ typeof(RedwoodBoard), 15 },
			{ typeof(PetrifiedBoard), 16 },

			// Other simple resources
			{ typeof(Garlic), 11 },
			{ typeof(BlackPearl), 11 },
			{ typeof(Bloodmoss), 11 },
			{ typeof(Ginseng), 11 },
			{ typeof(MandrakeRoot), 11 },
			{ typeof(Nightshade), 11 },
			{ typeof(SulfurousAsh), 11 },
			{ typeof(SpidersSilk), 11 },
			{ typeof(Bottle), 4 },
			{ typeof(Shaft), 2 },
			{ typeof(Feather), 3 },
			{ typeof(BlankMap), 10 },
			{ typeof(SackFlour), 4 },
			{ typeof(Dough), 3 },
			{ typeof(CakeMix), 10 },
			{ typeof(CookieMix), 8 },
			{ typeof(RawBird), 3 },
			{ typeof(RawChickenLeg), 3 },
			{ typeof(RawFishSteak), 2 },
			{ typeof(RawLambLeg), 3 },
			{ typeof(RawRibs), 3 },
			{ typeof(Sand), 15 },
			{ typeof(Keg), 20 },
			{ typeof(Axle), 3 },
			{ typeof(ClockFrame), 14 },
			{ typeof(AxleGears), 3 },
			{ typeof(SextantParts), 4 },
			{ typeof(BolaBall), 5 },
			{ typeof(GateTravelScroll), 7 },
			{ typeof(RecallScroll), 7 },
			{ typeof(RecallRune), 8 },
			{ typeof(BlankScroll), 7 },
			{ typeof(Cloth), 3 },
			{ typeof(Leather), 4 },
			{ typeof(Bone), 4 },
			{ typeof(DaemonBone), 11 },
			{ typeof(DaemonBlood), 11 },
			{ typeof(Cotton), 3 }
		};

		// Quick lookup of known ore types for applying oremodifier
		private static readonly HashSet<Type> OreTypes = new HashSet<Type>()
		{
			typeof(IronOre),
			typeof(DullCopperOre),
			typeof(ShadowIronOre),
			typeof(CopperOre),
			typeof(BronzeOre),
			typeof(GoldOre),
			typeof(AgapiteOre),
			typeof(VeriteOre),
			typeof(ValoriteOre),
			typeof(BlazeOre),
			typeof(IceOre),
			typeof(ToxicOre),
			typeof(ElectrumOre),
			typeof(PlatinumOre),
			typeof(BariteOre),
			typeof(WulfeniteOre),
			typeof(DragoniteOre),
			typeof(BunteriteOre),
			typeof(PineiteOre),
			typeof(SamiteOre),
			typeof(ToberiteOre),
			typeof(LisiteOre),
			typeof(MariteOre),
			typeof(TealOre),
			typeof(RoyaliteOre),
			typeof(DaniteOre)
		};

		// Crafting hold-resource -> base exp
		private static readonly Dictionary<Type, int> CraftHoldResourceExpMap = new Dictionary<Type, int>()
		{
			{ typeof(DullCopperIngot), 5 },
			{ typeof(ShadowIronIngot), 6 },
			{ typeof(CopperIngot), 7 },
			{ typeof(BronzeIngot), 8 },
			{ typeof(GoldIngot), 9 },
			{ typeof(AgapiteIngot), 10 },
			{ typeof(VeriteIngot), 11 },
			{ typeof(ValoriteIngot), 12 },
			{ typeof(BlazeIngot), 13 },
			{ typeof(IceIngot), 14 },
			{ typeof(ToxicIngot), 15 },
			{ typeof(ElectrumIngot), 16 },
			{ typeof(PlatinumIngot), 17 },
			{ typeof(TealIngot), 17 },
			{ typeof(RoyaliteIngot), 18 },
			{ typeof(DaniteIngot), 20 },

			// Boards / logs used in crafting
			{ typeof(OakLog), 6 },
			{ typeof(AshLog), 7 },
			{ typeof(YewLog), 8 },
			{ typeof(HeartwoodLog), 9 },
			{ typeof(BloodwoodLog), 10 },
			{ typeof(FrostwoodLog), 11 },
			{ typeof(EbonyLog), 12 },
			{ typeof(BambooLog), 13 },
			{ typeof(PurpleHeartLog), 14 },
			{ typeof(RedwoodLog), 15 },
			{ typeof(PetrifiedLog), 16 },

			// Granites (if used as hold resource)
			{ typeof(DullCopperGranite), 15 },
			{ typeof(ShadowIronGranite), 16 },
			{ typeof(CopperGranite), 17 },
			{ typeof(BronzeGranite), 18 },
			{ typeof(GoldGranite), 19 },
			{ typeof(AgapiteGranite), 20 },
			{ typeof(VeriteGranite), 21 },
			{ typeof(ValoriteGranite), 22 },
			{ typeof(BlazeGranite), 23 },
			{ typeof(IceGranite), 24 },
			{ typeof(ToxicGranite), 25 },
			{ typeof(ElectrumGranite), 26 },
			{ typeof(PlatinumGranite), 27 },
			{ typeof(TealGranite), 28 },
			{ typeof(RoyaliteGranite), 28 },
			{ typeof(DaniteGranite), 30 }
		};

		// Craft resource item type fallback map
		private static readonly Dictionary<Type, int> CraftItemTypeExpMap = new Dictionary<Type, int>()
		{
			{ typeof(IronIngot), 4 },
			{ typeof(Log), 5 },
			{ typeof(Granite), 15 },
			{ typeof(Board), 5 },
			{ typeof(RedScales), 4 },
			{ typeof(BlankScroll), 7 },
			{ typeof(Shaft), 2 },
			{ typeof(Feather), 3 },
			{ typeof(Bottle), 4 },
			{ typeof(Garlic), 11 },
			{ typeof(BlackPearl), 11 },
			{ typeof(Bloodmoss), 11 },
			{ typeof(Ginseng), 11 },
			{ typeof(MandrakeRoot), 11 },
			{ typeof(Nightshade), 11 },
			{ typeof(SulfurousAsh), 11 },
			{ typeof(SpidersSilk), 11 },
			{ typeof(SackFlour), 4 },
			{ typeof(Dough), 3 },
			{ typeof(CakeMix), 10 },
			{ typeof(CookieMix), 8 },
			{ typeof(RawBird), 3 },
			{ typeof(RawChickenLeg), 3 },
			{ typeof(RawFishSteak), 2 },
			{ typeof(RawLambLeg), 3 },
			{ typeof(RawRibs), 3 },
			{ typeof(Sand), 15 },
			{ typeof(BlankMap), 10 },
			{ typeof(Cloth), 3 },
			{ typeof(Leather), 4 },
			{ typeof(Bone), 4 },
			{ typeof(DaemonBone), 11 },
			{ typeof(DaemonBlood), 11 },
			{ typeof(Cotton), 3 }
		};

		public static void CheckLevel(PlayerMobile pm)
		{
			PlayerMobile from = pm;

			if (from == null)
				return;

			long Exp = from.EXP;
			long XPO = 0;
			int Level = (from.LvL == from.MaxLvl) ? from.PrestigeLvl : from.LvL;

			if (from.LvL > from.MaxLvl)
			{
				while (from.LvL > from.MaxLvl)
				{
					XPO += (long)((100 * Math.Pow((from.LvL - 1), 1.35)));
					from.LvL--;
				}
				from.EXP += XPO;
			}

			while (from.EXP > GetNextLevelXP(pm))
			{
				long XPMinus = GetNextLevelXP(pm);

				if (from.LvL == from.MaxLvl)
				{
					from.PrestigeLvl++;
					Effects.SendTargetParticles(from, 14170, 1, 17, 2924 - 1, 2, 0, EffectLayer.Waist, 0);
					from.Say("*Has gained a paragon level*");
					from.PlaySound(61);
					from.Hits = from.HitsMax;
					from.Mana = from.ManaMax;
					from.Stam = from.StamMax;
					from.ParagonPoints++;
					pm.InvalidateProperties();
				}
				else if (from.LvL < from.MaxLvl)
				{
					LevelUp(from);
					if (from.LvL == from.MaxLvl && from.PrestigeLvl == 0)
					{
						from.LastLevelExp = 100;
						from.SendMessage("You have reached the maximum level at this time, additional experience will go into paragon levels.");
					}
				}

				from.EXP -= XPMinus;
			}

			if (from.ShowExpBar == true)
				from.SendGump(new ExpBarGump(from));
		}

		public static long GetNextLevelXP(PlayerMobile from)
		{
			int Level = (from.LvL == from.MaxLvl) ? from.PrestigeLvl : from.LvL;
			double XP = (100 * Math.Pow(Level, 1.35));
			if (XP <= 100) XP = 100;
			return (long)XP;
		}

		public static void LevelUp(PlayerMobile from)
		{
			double XP = (100 * Math.Pow(1.35, from.LvL));
			if (XP <= 100) XP = 100;
			from.LastLevelExp = (long)XP;
			from.LvL++;
			from.InvalidateProperties();

			if (from.Young && from.LvL == 20)
			{
				Account acc = from.Account as Account;
				if (acc != null) acc.RemoveYoungStatus(0);
				from.Young = false;
				from.YoungSaves = 0;
				from.SendMessage("You have reached a point where you are no longer considered to be young, you will no longer have the protection from death that has been afforded to you until this point. Good Luck.");
			}

			if (from.LvL > from.OldMaxLvl && from.LvL <= from.MaxLvl)
				from.StatCap += 8;
			from.SP += 4;
			from.RawStr++;
			from.RawStr++;
			from.RawDex++;
			from.RawDex++;
			from.RawInt++;
			from.RawInt++;

			if (from.LvL >= 1 && from.LvL > from.OldMaxLvl && from.LvL <= from.MaxLvl)
				from.SkillsCap += 80;

			from.SendMessage("You have gained a level! You are now level " + from.LvL);
			Effects.SendTargetParticles(from, 14170, 1, 17, 2924 - 1, 2, 0, EffectLayer.Waist, 0);
			from.Say("*Has gained a level*");
			from.PlaySound(61);
			from.Hits = from.HitsMax;
			from.Mana = from.ManaMax;
			from.Stam = from.StamMax;
			from.InvalidateProperties();
		}

		public static void MonsterExp(Mobile killer, Mobile monster, Point3D loc, double Percent, long XPAMOUNT)
		{
			BaseCreature Monster = monster as BaseCreature;
			PlayerMobile from = null;
			if (killer is PlayerMobile)
				from = killer as PlayerMobile;
			else if (killer is BaseCreature)
			{
				BaseCreature c = (BaseCreature)killer;
				if (c.ControlMaster is PlayerMobile)
					from = (PlayerMobile)c.ControlMaster;
			}

			if (from == null || Monster == null)
				return;

			long ExpGained = XPAMOUNT > 0 ? XPAMOUNT : 1;
			Point3D DeathLoc = loc;

			if (!Utility.InRange(from.Location, DeathLoc, 18))
				ExpGained = (long)(ExpGained / 2);

			// Party bonus
			Party p = from.Party as Party;
			if (p != null)
			{
				double xpbonus = 1;
				for (int i = 0; i < p.Members.Count; ++i)
				{
					PartyMemberInfo pmi = (PartyMemberInfo)p.Members[i];
					if (pmi?.Mobile != null && Utility.InRange(pmi.Mobile.Location, from.Location, 18))
						xpbonus += .05;
				}
				ExpGained = (long)(ExpGained * xpbonus);
			}

			// EXPDeed stacking multiplicative: each deed multiplies xp by 1.2
			int deedCount = 0;
			if (from.Backpack != null)
				deedCount = from.Backpack.Items.OfType<EXPDeed>().Count();
			if (deedCount > 0)
			{
				double factor = Math.Pow(1.2, deedCount);
				ExpGained = (long)Math.Ceiling(ExpGained * factor);
			}

			long minexp = 0;
			long maxexp = 0;

			int mapb = Tweaks.GetMapXPMod(from.Map);
			long finalexp = (long)((double)(Utility.RandomMinMax((int)minexp, (int)maxexp)) * Percent * (Tweaks.XPMod + mapb));

			Region re = Region.Find(from.Location, from.Map);
			string regname = re.ToString().ToLower();

			if (regname == "championspawnregion")
			{
				if (Tweaks.CXPMod == 0)
					finalexp = 0;
				else
					finalexp /= (100 / Tweaks.CXPMod);
				if (finalexp <= 10)
					finalexp = 10;
			}

			if (Monster.IsParagon)
				finalexp *= 2;

			from.EXP += ExpGained;

			// Award DanDollar ticks for the XP event (kill). Use direct ticks API for efficiency.
			try
{
    // Read player's difficulty level and health multiplier from your DifficultySystem
    int diffLevel = 1;
    double healthMul = 1.0;

    try
    {
        diffLevel = DifficultySettings.GetPlayerDifficulty(from);               // returns int
        healthMul  = DifficultySettings.GetHealthMultiplier(diffLevel);        // returns double
    }
    catch
    {
        // If DifficultySystem isn't present or methods differ, fallback to 1.0
        diffLevel = 1;
        healthMul  = 1.0;
    }

    long baseTicks = DanDollarTicksPerXPEvent; // 5 ticks == 0.00005 with TicksPerDollar == 100000
    // Scale by health multiplier and round up to avoid losing fractional increases
    long scaledTicks = (long)Math.Ceiling(baseTicks * healthMul);
    if (scaledTicks <= 0)
        scaledTicks = baseTicks;

    DanDollarManager.AddTicks(from, scaledTicks, $"XP from kill (diff={diffLevel}, mul={healthMul:0.###})");
}
catch
{
    // ignore if DanDollar system isn't present or other errors occur
}

			CheckLevel(from); // Re-triggers levelup if exp is more than 1 level ahead
		}

		public static void HarvestExp(Mobile m, HarvestResource resource, bool success, int amount)
		{
			PlayerMobile from = m as PlayerMobile;
			if (from == null || resource == null) return;

			int oremodifier = 8;
			var Res = resource;
			Type resType = (Res.Types != null && Res.Types.Length > 0) ? Res.Types[0] : null;

			int baseExp = 0;
			if (resType != null && HarvestBaseExpMap.TryGetValue(resType, out int v))
				baseExp = v;
			else
				baseExp = 3; // default fallback

			int ExpGained = baseExp;

			// If it's an ore type, apply oremodifier
			if (resType != null && OreTypes.Contains(resType))
				ExpGained *= oremodifier;

			// Special handling for fish randomness if desired (previous code used random 1..100)
			if (resType == typeof(Fish))
				ExpGained = Utility.RandomMinMax(1, 100);

			if (!success)
				ExpGained -= ExpGained / 2;

			if (ExpGained > 0)
			{
				from.EXP += (ExpGained * Tweaks.XPMod * amount);

				// Award DanDollar ticks for this harvest event
				try
				{
					DanDollarManager.AddTicks(from, DanDollarTicksPerXPEvent, "XP from harvest");
				}
				catch { }
			}
		}

		public static void CraftExp(Mobile m, int quality, bool failed, CraftItem item)
		{
			PlayerMobile from = m as PlayerMobile;
			if (from == null || item == null) return;

			// Get the resource used (take first if multiple)
			CraftRes Res = (item.Resources != null && item.Resources.Count > 0) ? item.Resources.GetAt(0) : null;

			int ResExp = 3; // default
			if (Res != null)
			{
				Type holdRes = item.HoldResource;
				Type resItemType = Res.ItemType;

				if (holdRes != null && CraftHoldResourceExpMap.TryGetValue(holdRes, out int v))
					ResExp = v;
				else if (resItemType != null && CraftItemTypeExpMap.TryGetValue(resItemType, out int v2))
					ResExp = v2;
				else
					ResExp = 3;
			}

			int ExpGained = ResExp * (Res?.Amount ?? 1);

			if (quality == 0) // low quality
				ExpGained = (int)((decimal.Divide(ExpGained, 5)) * 2);
			else if (quality == 1) // normal quality
				ExpGained = (int)((decimal.Divide(ExpGained, 5)) * 5);
			else if (quality == 2) // exceptional
				ExpGained = (int)((decimal.Divide(ExpGained, 5)) * 6);

			if (failed)
				ExpGained = (int)(decimal.Divide(ExpGained, 5));

			if (ExpGained > 0)
			{
				if (ExpGained > 6000) ExpGained = 6000;
				ExpGained *= (1 + (2 * Tweaks.XPMod));
				from.EXP += ExpGained;

				// Award DanDollar ticks for craft event (one award per craft action)
				try
				{
					DanDollarManager.AddTicks(from, DanDollarTicksPerXPEvent, "XP from craft");
				}
				catch { }
			}
		}
	}
}