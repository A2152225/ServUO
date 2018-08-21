using System;
using System.Collections;

namespace Server.Items
{
    public enum CraftResource
    {
		None = 0,
		Iron = 1,
		DullCopper,
		ShadowIron,
		Copper,
		Bronze,
		Gold,
		Agapite,
		Verite,
		Valorite,
		//daat99 OWLTR start - custom ores
		Blaze,
		Ice,
		Toxic,
		Electrum,
		Platinum,
		//daat99 OWLTR end - custom ores
		Royalite,
		Danite, 

		RegularLeather = 101,
		SpinedLeather,
		HornedLeather,
		BarbedLeather,
		//daat99 OWLTR start - custom leather
		PolarLeather,
		SyntheticLeather,
		BlazeLeather,
		DaemonicLeather,
		ShadowLeather,
		FrostLeather,
		EtherealLeather,
		//daat99 OWLTR end - custom leather

		RedScales = 201,
		YellowScales,
		BlackScales,
		GreenScales,
		WhiteScales,
		BlueScales,
		//daat99 OWLTR start - custom scales
		CopperScales,
		SilverScales,
		GoldScales,
		//daat99 OWLTR end - custom scales

		//daat99 OWLTR start - custom wood
		RegularWood = 301,
        OakWood,
        AshWood,
        YewWood,
		Heartwood,
		Bloodwood,
		Frostwood,
		Ebony,
		Bamboo,
		PurpleHeart,
		Redwood,
		Petrified
		//daat99 OWLTR end - custom wood
    }

    public enum CraftResourceType
    {
        None,
        Metal,
        Leather,
        Scales,
        Wood
    }

    public class CraftAttributeInfo
    {
        private int m_WeaponFireDamage;
        private int m_WeaponColdDamage;
        private int m_WeaponPoisonDamage;
        private int m_WeaponEnergyDamage;
        private int m_WeaponChaosDamage;
        private int m_WeaponDirectDamage;
        private int m_WeaponDurability;
        private int m_WeaponLuck;
        private int m_WeaponGoldIncrease;
        private int m_WeaponLowerRequirements;
        private int m_WeaponDamage;
        private int m_WeaponHitChance;
        private int m_WeaponHitLifeLeech;
        private int m_WeaponRegenHits;
        private int m_WeaponSwingSpeed;

        private int m_ArmorPhysicalResist;
        private int m_ArmorFireResist;
        private int m_ArmorColdResist;
        private int m_ArmorPoisonResist;
        private int m_ArmorEnergyResist;
        private int m_ArmorDurability;
        private int m_ArmorLuck;
        private int m_ArmorGoldIncrease;
        private int m_ArmorLowerRequirements;
        private int m_ArmorDamage;
        private int m_ArmorHitChance;
        private int m_ArmorRegenHits;
        private int m_ArmorMage;

        private int m_ShieldPhysicalResist;
        private int m_ShieldFireResist;
        private int m_ShieldColdResist;
        private int m_ShieldPoisonResist;
        private int m_ShieldEnergyResist;
        private int m_ShieldPhysicalRandom;
        private int m_ShieldColdRandom;
        private int m_ShieldSpellChanneling;
        private int m_ShieldLuck;
        private int m_ShieldLowerRequirements;
        private int m_ShieldRegenHits;
        private int m_ShieldBonusDex;
        private int m_ShieldBonusStr;
        private int m_ShieldReflectPhys;
        private int m_SelfRepair;

        private int m_OtherSpellChanneling;
        private int m_OtherLuck;
        private int m_OtherRegenHits;
        private int m_OtherLowerRequirements;

        private int m_RunicMinAttributes;
        private int m_RunicMaxAttributes;
        private int m_RunicMinIntensity;
        private int m_RunicMaxIntensity;
        
        public int WeaponFireDamage { get { return m_WeaponFireDamage; } set { m_WeaponFireDamage = value; } }
        public int WeaponColdDamage { get { return m_WeaponColdDamage; } set { m_WeaponColdDamage = value; } }
        public int WeaponPoisonDamage { get { return m_WeaponPoisonDamage; } set { m_WeaponPoisonDamage = value; } }
        public int WeaponEnergyDamage { get { return m_WeaponEnergyDamage; } set { m_WeaponEnergyDamage = value; } }
        public int WeaponChaosDamage { get { return m_WeaponChaosDamage; } set { m_WeaponChaosDamage = value; } }
        public int WeaponDirectDamage { get { return m_WeaponDirectDamage; } set { m_WeaponDirectDamage = value; } }
        public int WeaponDurability { get { return m_WeaponDurability; } set { m_WeaponDurability = value; } }
        public int WeaponLuck { get { return m_WeaponLuck; } set { m_WeaponLuck = value; } }
        public int WeaponGoldIncrease { get { return m_WeaponGoldIncrease; } set { m_WeaponGoldIncrease = value; } }
        public int WeaponLowerRequirements { get { return m_WeaponLowerRequirements; } set { m_WeaponLowerRequirements = value; } }
        public int WeaponDamage { get { return m_WeaponDamage; } set { m_WeaponDamage = value; } }
        public int WeaponHitChance { get { return m_WeaponHitChance; } set { m_WeaponHitChance = value; } }
        public int WeaponHitLifeLeech { get { return m_WeaponHitLifeLeech; } set { m_WeaponHitLifeLeech = value; } }
        public int WeaponRegenHits { get { return m_WeaponRegenHits; } set { m_WeaponRegenHits = value; } }
        public int WeaponSwingSpeed { get { return m_WeaponSwingSpeed; } set { m_WeaponSwingSpeed = value; } }

        public int ArmorPhysicalResist { get { return m_ArmorPhysicalResist; } set { m_ArmorPhysicalResist = value; } }
        public int ArmorFireResist { get { return m_ArmorFireResist; } set { m_ArmorFireResist = value; } }
        public int ArmorColdResist { get { return m_ArmorColdResist; } set { m_ArmorColdResist = value; } }
        public int ArmorPoisonResist { get { return m_ArmorPoisonResist; } set { m_ArmorPoisonResist = value; } }
        public int ArmorEnergyResist { get { return m_ArmorEnergyResist; } set { m_ArmorEnergyResist = value; } }
        public int ArmorDurability { get { return m_ArmorDurability; } set { m_ArmorDurability = value; } }
        public int ArmorLuck { get { return m_ArmorLuck; } set { m_ArmorLuck = value; } }
        public int ArmorGoldIncrease { get { return m_ArmorGoldIncrease; } set { m_ArmorGoldIncrease = value; } }
        public int ArmorLowerRequirements { get { return m_ArmorLowerRequirements; } set { m_ArmorLowerRequirements = value; } }
        public int ArmorDamage { get { return m_ArmorDamage; } set { m_ArmorDamage = value; } }
        public int ArmorHitChance { get { return m_ArmorHitChance; } set { m_ArmorHitChance = value; } }
        public int ArmorRegenHits { get { return m_ArmorRegenHits; } set { m_ArmorRegenHits = value; } }
        public int ArmorMage { get { return m_ArmorMage; } set { m_ArmorMage = value; } }

        public int ShieldPhysicalResist { get { return m_ShieldPhysicalResist; } set { m_ShieldPhysicalResist = value; } }
        public int ShieldFireResist { get { return m_ShieldFireResist; } set { m_ShieldFireResist = value; } }
        public int ShieldColdResist { get { return m_ShieldColdResist; } set { m_ShieldColdResist = value; } }
        public int ShieldPoisonResist { get { return m_ShieldPoisonResist; } set { m_ShieldPoisonResist = value; } }
        public int ShieldEnergyResist { get { return m_ShieldEnergyResist; } set { m_ShieldEnergyResist = value; } }
        public int ShieldPhysicalRandom { get { return m_ShieldPhysicalRandom; } set { m_ShieldPhysicalRandom = value; } }
        public int ShieldColdRandom { get { return m_ShieldColdRandom; } set { m_ShieldColdRandom = value; } }
        public int ShieldSpellChanneling { get { return m_ShieldSpellChanneling; } set { m_ShieldSpellChanneling = value; } }
        public int ShieldLuck { get { return m_ShieldLuck; } set { m_ShieldLuck = value; } }
        public int ShieldLowerRequirements { get { return m_ShieldLowerRequirements; } set { m_ShieldLowerRequirements = value; } }
        public int ShieldRegenHits { get { return m_ShieldRegenHits; } set { m_ShieldRegenHits = value; } }
        public int ShieldBonusDex { get { return m_ShieldBonusDex; } set { m_ShieldBonusDex = value; } }
        public int ShieldBonusStr { get { return m_ShieldBonusStr; } set { m_ShieldBonusStr = value; } }
        public int ShieldReflectPhys { get { return m_ShieldReflectPhys; } set { m_ShieldReflectPhys = value; } }
        public int ShieldSelfRepair { get { return m_SelfRepair; } set { m_SelfRepair = value; } }

        public int OtherSpellChanneling { get { return m_OtherSpellChanneling; } set { m_OtherSpellChanneling = value; } }
        public int OtherLuck { get { return m_OtherLuck; } set { m_OtherLuck = value; } }
        public int OtherRegenHits { get { return m_OtherRegenHits; } set { m_OtherRegenHits = value; } }
        public int OtherLowerRequirements { get { return m_OtherLowerRequirements; } set { m_OtherLowerRequirements = value; } }

        public int RunicMinAttributes { get { return m_RunicMinAttributes; } set { m_RunicMinAttributes = value; } }
        public int RunicMaxAttributes { get { return m_RunicMaxAttributes; } set { m_RunicMaxAttributes = value; } }
        public int RunicMinIntensity { get { return m_RunicMinIntensity; } set { m_RunicMinIntensity = value; } }
        public int RunicMaxIntensity { get { return m_RunicMaxIntensity; } set { m_RunicMaxIntensity = value; } }

        public CraftAttributeInfo()
        {
        }

        public static readonly CraftAttributeInfo Blank;
        public static readonly CraftAttributeInfo DullCopper, ShadowIron, Copper, Bronze, Golden, Agapite, Verite, Valorite, Blaze, Ice, Toxic, Electrum, Platinum, Royalite, Danite;
		public static readonly CraftAttributeInfo Spined, Horned, Barbed, Polar, Synthetic, BlazeL, Daemonic, Shadow, Frost, Ethereal;
		public static readonly CraftAttributeInfo RedScales, YellowScales, BlackScales, GreenScales, WhiteScales, BlueScales, CopperScales, SilverScales, GoldScales;
        public static readonly CraftAttributeInfo OakWood, AshWood, YewWood, Heartwood, Bloodwood, Frostwood, Ebony, Bamboo, PurpleHeart, Redwood, Petrified;

        static CraftAttributeInfo()
        {
            Blank = new CraftAttributeInfo();

            //daat99 OWLTR start - custom resources
			bool Uber = true;// OWLTROptionsManager.IsEnabled(OWLTROptionsManager.OPTIONS_ENUM.UBBER_RESOURCES);
			CraftAttributeInfo dullCopper = DullCopper = new CraftAttributeInfo();

			dullCopper.ArmorPhysicalResist = Uber ? 1 : Utility.Random(2);
			dullCopper.ArmorFireResist = Uber ? 1 : Utility.Random(2);
			dullCopper.ArmorColdResist = Uber ? 1 : Utility.Random(2);
			dullCopper.ArmorPoisonResist = Uber ? 1 : Utility.Random(2);
			dullCopper.ArmorEnergyResist = Uber ? 1 : Utility.Random(2);
			dullCopper.ArmorDurability = 10;
			dullCopper.WeaponDurability = 10;
			dullCopper.ArmorLowerRequirements = 5;
			dullCopper.WeaponLowerRequirements = 5;
			dullCopper.RunicMinAttributes = 1;
			dullCopper.RunicMaxAttributes = Uber ? 2 : 1;
			dullCopper.RunicMinIntensity = Uber ?  25 : 10;
			dullCopper.RunicMaxIntensity = Uber ? 100 : 25;

			CraftAttributeInfo shadowIron = ShadowIron = new CraftAttributeInfo();

			shadowIron.ArmorPhysicalResist = Uber ? 2 : Utility.Random(3);
			shadowIron.ArmorFireResist = Uber ? 1 : Utility.Random(2);
			shadowIron.ArmorColdResist = Uber ? 1 : Utility.Random(2);
			shadowIron.ArmorPoisonResist = Uber ? 1 : Utility.Random(2);
			shadowIron.ArmorEnergyResist = Uber ? 1 : Utility.Random(2);
			shadowIron.ArmorDurability = 20;
			shadowIron.WeaponDurability = 20;
			shadowIron.ArmorLowerRequirements = 10;
			shadowIron.WeaponLowerRequirements = 10;
			shadowIron.WeaponColdDamage = 20;
			shadowIron.RunicMinAttributes = 1;
			shadowIron.RunicMaxAttributes = Uber ? 3 : 2;
			shadowIron.RunicMinIntensity = Uber ? 30 : 15;
			shadowIron.RunicMaxIntensity = Uber ? 100 : 30;

			CraftAttributeInfo copper = Copper = new CraftAttributeInfo();

			copper.ArmorPhysicalResist = Uber ? 2 : Utility.Random(3);
			copper.ArmorFireResist = Uber ? 2 : Utility.Random(3);
			copper.ArmorColdResist = Uber ? 2 : Utility.Random(3);
			copper.ArmorPoisonResist = Uber ? 2 : Utility.Random(3);
			copper.ArmorEnergyResist = Uber ? 2 : Utility.Random(3);
			copper.ArmorDurability = 30;
			copper.WeaponDurability = 30;
			copper.ArmorLowerRequirements = 15;
			copper.WeaponLowerRequirements = 15;
			copper.WeaponPoisonDamage = 10;
			copper.WeaponEnergyDamage = 20;
			copper.RunicMinAttributes = 2;
			copper.RunicMaxAttributes = Uber ? 3 : 2;
			copper.RunicMinIntensity = Uber ? 40 : 20;
			copper.RunicMaxIntensity = Uber ? 100 : 40;

			CraftAttributeInfo bronze = Bronze = new CraftAttributeInfo();

			bronze.ArmorPhysicalResist = Uber ? 2 : Utility.Random(3);
			bronze.ArmorFireResist = Uber ? 3 : Utility.Random(4);
			bronze.ArmorColdResist = Uber ? 2 : Utility.Random(3);
			bronze.ArmorPoisonResist = Uber ? 2 : Utility.Random(3);
			bronze.ArmorEnergyResist = Uber ? 2 : Utility.Random(3);
			bronze.ArmorDurability = 40;
			bronze.WeaponDurability = 40;
			bronze.ArmorLowerRequirements = 20;
			bronze.WeaponLowerRequirements = 20;
			bronze.WeaponFireDamage = 40;
			bronze.RunicMinAttributes = 2;
			bronze.RunicMaxAttributes = Uber ? 4 : 3;
			bronze.RunicMinIntensity = Uber ?  45 : 25;
			bronze.RunicMaxIntensity = Uber ? 100 : 45;

			CraftAttributeInfo golden = Golden = new CraftAttributeInfo();

			golden.ArmorPhysicalResist = Uber ? 3 : Utility.Random(4);
			golden.ArmorFireResist = Uber ? 3 : Utility.Random(4);
			golden.ArmorColdResist = Uber ? 3 : Utility.Random(4);
			golden.ArmorPoisonResist = Uber ? 3 : Utility.Random(4);
			golden.ArmorEnergyResist = Uber ? 3 : Utility.Random(4);
			golden.ArmorDurability = 55;
			golden.WeaponDurability = 55;
			golden.ArmorLowerRequirements = 25;
			golden.WeaponLowerRequirements = 25;
			golden.ArmorLuck = 40;
			golden.ArmorLowerRequirements = 30;
			golden.WeaponLuck = 40;
			golden.WeaponLowerRequirements = 50;
			golden.RunicMinAttributes = 2;
			golden.RunicMaxAttributes = Uber ? 5 : 3;
			golden.RunicMinIntensity = Uber ?  50 : 30;
			golden.RunicMaxIntensity = Uber ? 100 : 50;

			CraftAttributeInfo agapite = Agapite = new CraftAttributeInfo();

			agapite.ArmorPhysicalResist = Uber ? 3 : Utility.Random(4);
			agapite.ArmorFireResist = Uber ? 3 : Utility.Random(4);
			agapite.ArmorColdResist = Uber ? 4 : Utility.Random(5);
			agapite.ArmorPoisonResist = Uber ? 3 : Utility.Random(4);
			agapite.ArmorEnergyResist = Uber ? 3 : Utility.Random(4);
			agapite.ArmorDurability = 70;
			agapite.WeaponDurability = 70;
			agapite.ArmorLowerRequirements = 30;
			agapite.WeaponLowerRequirements = 30;
			agapite.WeaponColdDamage = 30;
			agapite.WeaponEnergyDamage = 20;
			agapite.RunicMinAttributes = 3;
			agapite.RunicMaxAttributes = Uber ? 5 : 3;
			agapite.RunicMinIntensity = Uber ?  55 : 35;
			agapite.RunicMaxIntensity = Uber ? 100 : 55;

			CraftAttributeInfo verite = Verite = new CraftAttributeInfo();

			verite.ArmorPhysicalResist = Uber ? 4 : Utility.Random(5);
			verite.ArmorFireResist = Uber ? 4 : Utility.Random(5);
			verite.ArmorColdResist = Uber ? 4 : Utility.Random(5);
			verite.ArmorPoisonResist = Uber ? 4 : Utility.Random(5);
			verite.ArmorEnergyResist = Uber ? 4 : Utility.Random(5);
			verite.ArmorDurability = 85;
			verite.WeaponDurability = 85;
			verite.ArmorLowerRequirements = 40;
			verite.WeaponLowerRequirements = 40;
			verite.WeaponPoisonDamage = 40;
			verite.WeaponEnergyDamage = 20;
			verite.RunicMinAttributes = 3;
			verite.RunicMaxAttributes = Uber ? 5 : 4;
			verite.RunicMinIntensity = Uber ?  65 : 40;
			verite.RunicMaxIntensity = Uber ? 100 : 65;

			CraftAttributeInfo valorite = Valorite = new CraftAttributeInfo();

			valorite.ArmorPhysicalResist = Uber ? 4 : Utility.Random(5);
			valorite.ArmorFireResist = Uber ? 4 : Utility.Random(5);
			valorite.ArmorColdResist = Uber ? 4 : Utility.Random(5);
			valorite.ArmorPoisonResist = Uber ? 5 : Utility.Random(6);
			valorite.ArmorEnergyResist = Uber ? 4 : Utility.Random(5);
			valorite.ArmorDurability = 100;
			valorite.WeaponDurability = 100;
			valorite.ArmorLowerRequirements = 50;
			valorite.WeaponLowerRequirements = 50;
			valorite.WeaponFireDamage = 10;
			valorite.WeaponColdDamage = 20;
			valorite.WeaponPoisonDamage = 10;
			valorite.WeaponEnergyDamage = 20;
			valorite.RunicMinAttributes = 4;
			valorite.RunicMaxAttributes = Uber ? 6 : 4;
			valorite.RunicMinIntensity = Uber ?  70 : 45;
			valorite.RunicMaxIntensity = Uber ? 100 : 70;

CraftAttributeInfo blaze = Blaze = new CraftAttributeInfo();

			blaze.ArmorPhysicalResist = Uber ? 5 : Utility.Random(6);
			blaze.ArmorFireResist = Uber ? 5 : Utility.Random(6);
			blaze.ArmorColdResist = Uber ? 5 : Utility.Random(6);
			blaze.ArmorPoisonResist = Uber ? 5 : Utility.Random(6);
			blaze.ArmorEnergyResist = Uber ? 5 : Utility.Random(6);
			blaze.ArmorDurability = 125;
			blaze.WeaponDurability = 125;
			blaze.ArmorLowerRequirements = 60;
			blaze.WeaponLowerRequirements = 60;
			blaze.WeaponFireDamage = 100;
			blaze.RunicMinAttributes = 4;
			blaze.RunicMaxAttributes = Uber ? 7 : 5;
			blaze.RunicMinIntensity = Uber ?  75 : 50;
			blaze.RunicMaxIntensity = Uber ? 100 : 75;

			CraftAttributeInfo ice = Ice = new CraftAttributeInfo();

			ice.ArmorPhysicalResist = Uber ? 5 : Utility.Random(6);
			ice.ArmorFireResist = Uber ? 5 : Utility.Random(6);
			ice.ArmorColdResist = Uber ? 5 : Utility.Random(6);
			ice.ArmorPoisonResist = Uber ? 5 : Utility.Random(6);
			ice.ArmorEnergyResist = Uber ? 6 : Utility.Random(7);
			ice.ArmorDurability = 150;
			ice.WeaponDurability = 150;
			ice.ArmorLowerRequirements = 70;
			ice.WeaponLowerRequirements = 70;
			ice.WeaponColdDamage = 100;
			ice.RunicMinAttributes =5;
			ice.RunicMaxAttributes = Uber ? 8 : 5;
			ice.RunicMinIntensity = Uber ?  80 : 55;
			ice.RunicMaxIntensity = Uber ? 100 : 80;
		
			CraftAttributeInfo toxic = Toxic = new CraftAttributeInfo();

			toxic.ArmorPhysicalResist = Uber ? 6 : Utility.Random(7);
			toxic.ArmorFireResist = Uber ? 6 : Utility.Random(7);
			toxic.ArmorColdResist = Uber ? 6 : Utility.Random(7);
			toxic.ArmorPoisonResist = Uber ? 6 : Utility.Random(7);
			toxic.ArmorEnergyResist = Uber ? 6 : Utility.Random(7);
			toxic.ArmorDurability = 175;
			toxic.WeaponDurability = 175;
			toxic.ArmorLowerRequirements = 80;
			toxic.WeaponLowerRequirements = 80;
			toxic.WeaponPoisonDamage = 100;
			toxic.RunicMinAttributes = 5;
			toxic.RunicMaxAttributes = Uber ? 8 : 6;
			toxic.RunicMinIntensity = Uber ? 90 : 60;
			toxic.RunicMaxIntensity = Uber ? 100 : 90;

			CraftAttributeInfo electrum = Electrum = new CraftAttributeInfo();

			electrum.ArmorPhysicalResist = Uber ? 7 : Utility.Random(8);
			electrum.ArmorFireResist = Uber ? 7 : Utility.Random(8);
			electrum.ArmorColdResist = Uber ? 7 : Utility.Random(8);
			electrum.ArmorPoisonResist = Uber ? 7 : Utility.Random(8);
			electrum.ArmorEnergyResist = Uber ? 7 : Utility.Random(8);
			electrum.ArmorDurability = 200;
			electrum.WeaponDurability = 200;
			electrum.ArmorLowerRequirements = 90;
			electrum.WeaponLowerRequirements = 90;
			electrum.WeaponEnergyDamage = 100;
			electrum.RunicMinAttributes = 5;
			electrum.RunicMaxAttributes = Uber ? 9 : 6;
			electrum.RunicMinIntensity = Uber ? 100 : 65;
			electrum.RunicMaxIntensity = Uber ? 100 : 100;

			CraftAttributeInfo platinum = Platinum = new CraftAttributeInfo();

			platinum.ArmorPhysicalResist = Uber ? 8 : Utility.Random(9);
			platinum.ArmorFireResist = Uber ? 8 : Utility.Random(9);
			platinum.ArmorColdResist = Uber ? 8 : Utility.Random(9);
			platinum.ArmorPoisonResist = Uber ? 8 : Utility.Random(9);
			platinum.ArmorEnergyResist = Uber ? 8 : Utility.Random(9);
			platinum.ArmorDurability = 250;
			platinum.WeaponDurability = 250;
			platinum.ArmorLowerRequirements = 100;
			platinum.WeaponLowerRequirements = 100;
			platinum.RunicMinAttributes = 6;
			platinum.RunicMaxAttributes = Uber ? 9 : 7;
			platinum.RunicMinIntensity = Uber ? 105 : 70;
			platinum.RunicMaxIntensity = Uber ? 110 : 105;

			
				CraftAttributeInfo royalite = Royalite = new CraftAttributeInfo();

			royalite.ArmorPhysicalResist = Uber ? 9 : Utility.Random(9);
			royalite.ArmorFireResist = Uber ? 8 : Utility.Random(9);
			royalite.ArmorColdResist = Uber ? 8 : Utility.Random(9);
			royalite.ArmorPoisonResist = Uber ? 9 : Utility.Random(9);
			royalite.ArmorEnergyResist = Uber ? 9 : Utility.Random(9);
			royalite.ArmorDurability = 250;
			royalite.WeaponDurability = 250;
			royalite.ArmorLowerRequirements = 100;
			royalite.WeaponLowerRequirements = 100;
			royalite.RunicMinAttributes = 7;
			royalite.RunicMaxAttributes = Uber ? 10 : 8;
			royalite.RunicMinIntensity = Uber ? 105 : 85;
			royalite.RunicMaxIntensity = Uber ? 115 : 120;

			
			
				CraftAttributeInfo danite = Danite = new CraftAttributeInfo();

			danite.ArmorPhysicalResist = Uber ? 10 : Utility.Random(9);
			danite.ArmorFireResist = Uber ? 8 : Utility.Random(9);
			danite.ArmorColdResist = Uber ? 10 : Utility.Random(9);
			danite.ArmorPoisonResist = Uber ? 8 : Utility.Random(9);
			danite.ArmorEnergyResist = Uber ? 10 : Utility.Random(9);
			danite.ArmorDurability = 250;
			danite.WeaponDurability = 250;
			danite.ArmorLowerRequirements = 100;
			danite.WeaponLowerRequirements = 100;
			danite.RunicMinAttributes = 8;
			danite.RunicMaxAttributes = Uber ? 11 : 9;
			danite.RunicMinIntensity = Uber ? 115 : 90;
			danite.RunicMaxIntensity = Uber ? 120 : 125;

			
			
			CraftAttributeInfo spined = Spined = new CraftAttributeInfo();

			spined.ArmorPhysicalResist = Uber ? 1 : Utility.Random(2);
			spined.ArmorFireResist = Uber ? 1 : Utility.Random(2);
			spined.ArmorColdResist = Uber ? 1 : Utility.Random(2);
			spined.ArmorPoisonResist = Uber ? 1 : Utility.Random(2);
			spined.ArmorEnergyResist = Uber ? 1 : Utility.Random(2);
			spined.ArmorDurability = 25;
			spined.ArmorLowerRequirements = 20;
			spined.ArmorLuck = 40;
			spined.RunicMinAttributes = 1;
			spined.RunicMaxAttributes = Uber ? 3 : 2;
			spined.RunicMinIntensity = Uber ?  20 : 10;
			spined.RunicMaxIntensity = Uber ? 100 : 20;

			CraftAttributeInfo horned = Horned = new CraftAttributeInfo();

			horned.ArmorPhysicalResist = Uber ? 2 : Utility.Random(3);
			horned.ArmorFireResist = Uber ? 2 : Utility.Random(3);
			horned.ArmorColdResist = Uber ? 2 : Utility.Random(3);
			horned.ArmorPoisonResist = Uber ? 2 : Utility.Random(3);
			horned.ArmorEnergyResist = Uber ? 2 : Utility.Random(3);
			horned.ArmorDurability = 50;
			horned.ArmorLowerRequirements = 30;
			horned.RunicMinAttributes = 2;
			horned.RunicMaxAttributes = Uber ? 3 : 2;
			horned.RunicMinIntensity = Uber ?  30 : 15;
			horned.RunicMaxIntensity = Uber ? 100 : 30;
			
			CraftAttributeInfo barbed = Barbed = new CraftAttributeInfo();

			barbed.ArmorPhysicalResist = Uber ? 3 : Utility.Random(4);
			barbed.ArmorFireResist = Uber ? 3 : Utility.Random(4);
			barbed.ArmorColdResist = Uber ? 3 : Utility.Random(4);
			barbed.ArmorPoisonResist = Uber ? 3 : Utility.Random(4);
			barbed.ArmorEnergyResist = Uber ? 3 : Utility.Random(4);
			barbed.ArmorDurability = 75;
			barbed.ArmorLowerRequirements = 40;
			barbed.RunicMinAttributes = 2;
			barbed.RunicMaxAttributes = Uber ?  4 : 3;
			barbed.RunicMinIntensity = Uber ?  35 : 20;
			barbed.RunicMaxIntensity = Uber ? 100 : 35;

			CraftAttributeInfo polar = Polar = new CraftAttributeInfo();

			polar.ArmorPhysicalResist = Uber ? 4 : Utility.Random(5);
			polar.ArmorFireResist = Uber ? 3 : Utility.Random(4);
			polar.ArmorColdResist = Uber ? 4 : Utility.Random(5);
			polar.ArmorPoisonResist = Uber ? 3 : Utility.Random(4);
			polar.ArmorEnergyResist = Uber ? 4 : Utility.Random(5);
			polar.ArmorDurability = 100;
			polar.ArmorLowerRequirements = 50;
			polar.RunicMinAttributes = 3;
			polar.RunicMaxAttributes = Uber ? 5 : 3;
			polar.RunicMinIntensity = Uber ? 45 : 25;
			polar.RunicMaxIntensity = Uber ? 100 : 45;

			CraftAttributeInfo synthetic = Synthetic = new CraftAttributeInfo();

			synthetic.ArmorPhysicalResist = Uber ? 4 : Utility.Random(5);
			synthetic.ArmorFireResist = Uber ? 4 : Utility.Random(5);
			synthetic.ArmorColdResist = Uber ? 4 : Utility.Random(5);
			synthetic.ArmorPoisonResist = Uber ? 4 : Utility.Random(5);
			synthetic.ArmorEnergyResist = Uber ? 4 : Utility.Random(5);
			synthetic.ArmorLowerRequirements = 60;
			synthetic.ArmorDurability = 125;
			synthetic.RunicMinAttributes = 3;
			synthetic.RunicMaxAttributes = Uber ? 5 : 4;
			synthetic.RunicMinIntensity = Uber ? 50 : 30;
			synthetic.RunicMaxIntensity = Uber ? 100 : 50;
			
			CraftAttributeInfo blazel = BlazeL = new CraftAttributeInfo();

			blazel.ArmorPhysicalResist = Uber ? 4 : Utility.Random(5);
			blazel.ArmorFireResist = Uber ? 5 : Utility.Random(6);
			blazel.ArmorColdResist = Uber ? 4 : Utility.Random(5);
			blazel.ArmorPoisonResist = Uber ? 5 : Utility.Random(6);
			blazel.ArmorEnergyResist = Uber ? 4 : Utility.Random(5);
			blazel.ArmorLowerRequirements = 60;
			blazel.ArmorDurability = 125;
			blazel.RunicMinAttributes = 4;
			blazel.RunicMaxAttributes = Uber ? 6 : 4;
			blazel.RunicMinIntensity = Uber ? 60 : 35;
			blazel.RunicMaxIntensity = Uber ? 100 : 60;

			CraftAttributeInfo daemonic = Daemonic = new CraftAttributeInfo();

			daemonic.ArmorPhysicalResist = Uber ? 5 : Utility.Random(6);
			daemonic.ArmorFireResist = Uber ? 5 : Utility.Random(6);
			daemonic.ArmorColdResist = Uber ? 5 : Utility.Random(6);
			daemonic.ArmorPoisonResist = Uber ? 5 : Utility.Random(6);
			daemonic.ArmorEnergyResist = Uber ? 5 : Utility.Random(6);
			daemonic.ArmorDurability = 150;
			daemonic.ArmorLowerRequirements = 70;
			daemonic.RunicMinAttributes = 4;
			daemonic.RunicMaxAttributes = Uber ? 7 : 5;
			daemonic.RunicMinIntensity = Uber ? 65 : 40;
			daemonic.RunicMaxIntensity = Uber ? 100 : 65;
			
			CraftAttributeInfo shadow = Shadow = new CraftAttributeInfo();

			shadow.ArmorPhysicalResist = Uber ? 6 : Utility.Random(7);
			shadow.ArmorFireResist = Uber ? 6 : Utility.Random(7);
			shadow.ArmorColdResist = Uber ? 6 : Utility.Random(7);
			shadow.ArmorPoisonResist = Uber ? 6 : Utility.Random(7);
			shadow.ArmorEnergyResist = Uber ? 6 : Utility.Random(7);
			shadow.ArmorDurability = 175;
			shadow.ArmorLowerRequirements = 80;
			shadow.RunicMinAttributes = 5;
			shadow.RunicMaxAttributes = Uber ? 7 : 5;
			shadow.RunicMinIntensity = Uber ? 75 : 45;
			shadow.RunicMaxIntensity = Uber ? 100 : 75;
			
			CraftAttributeInfo frost = Frost = new CraftAttributeInfo();

			frost.ArmorPhysicalResist = Uber ? 7 : Utility.Random(8);
			frost.ArmorFireResist = Uber ? 7 : Utility.Random(8);
			frost.ArmorColdResist = Uber ? 7 : Utility.Random(8);
			frost.ArmorPoisonResist = Uber ? 7 : Utility.Random(8);
			frost.ArmorEnergyResist = Uber ? 7 : Utility.Random(8);
			frost.ArmorDurability = 200;
			frost.ArmorLowerRequirements = 90;
			frost.RunicMinAttributes = 5;
			frost.RunicMaxAttributes = Uber ? 8 : 6;
			frost.RunicMinIntensity = Uber ? 80 : 50;
			frost.RunicMaxIntensity = Uber ? 100 : 80;
			
			CraftAttributeInfo ethereal = Ethereal = new CraftAttributeInfo();

			ethereal.ArmorPhysicalResist = Uber ? 8 : Utility.Random(9);
			ethereal.ArmorFireResist = Uber ? 8 : Utility.Random(9);
			ethereal.ArmorColdResist = Uber ? 8 : Utility.Random(9);
			ethereal.ArmorPoisonResist = Uber ? 8 : Utility.Random(9);
			ethereal.ArmorEnergyResist = Uber ? 8 : Utility.Random(9);
			ethereal.ArmorDurability = 250;
			ethereal.ArmorLowerRequirements = 100;
			ethereal.RunicMinAttributes = 6;
			ethereal.RunicMaxAttributes = Uber ? 9 : 7;
			ethereal.RunicMinIntensity = Uber ? 100 : 60;
			ethereal.RunicMaxIntensity = Uber ? 110 : 100;
			
			CraftAttributeInfo red = RedScales = new CraftAttributeInfo();

			red.ArmorFireResist = 10;
			red.ArmorColdResist = -3;

			CraftAttributeInfo yellow = YellowScales = new CraftAttributeInfo();

			yellow.ArmorPhysicalResist = -3;
			yellow.ArmorLuck = 20;

			CraftAttributeInfo black = BlackScales = new CraftAttributeInfo();

			black.ArmorPhysicalResist = 10;
			black.ArmorEnergyResist = -3;

			CraftAttributeInfo green = GreenScales = new CraftAttributeInfo();

			green.ArmorFireResist = -3;
			green.ArmorPoisonResist = 10;

			CraftAttributeInfo white = WhiteScales = new CraftAttributeInfo();

			white.ArmorPhysicalResist = -3;
			white.ArmorColdResist = 10;

			CraftAttributeInfo blue = BlueScales = new CraftAttributeInfo();

			blue.ArmorPoisonResist = -3;
			blue.ArmorEnergyResist = 10;

			CraftAttributeInfo coppers = CopperScales = new CraftAttributeInfo();

			coppers.ArmorPoisonResist = Uber ? 6 : Utility.Random(7);
			coppers.ArmorPhysicalResist = Uber ? 6 : Utility.Random(7);
			coppers.ArmorEnergyResist = Uber ? 6 : Utility.Random(7);

			CraftAttributeInfo silver = SilverScales = new CraftAttributeInfo();

			silver.ArmorColdResist = Uber ? 7 : Utility.Random(8);
			silver.ArmorEnergyResist = Uber ? 7 : Utility.Random(8);
			silver.ArmorPhysicalResist = Uber ? 7 : Utility.Random(8);

			CraftAttributeInfo gold = GoldScales = new CraftAttributeInfo();

			gold.ArmorPoisonResist = Uber ? 8 : Utility.Random(9);
			gold.ArmorColdResist = Uber ? 8 : Utility.Random(9);
			gold.ArmorPhysicalResist = Uber ? 8 : Utility.Random(9);
			gold.ArmorEnergyResist = Uber ? 8 : Utility.Random(9);
			gold.ArmorFireResist = Uber ? 8 : Utility.Random(9);
			
			CraftAttributeInfo oak = OakWood = new CraftAttributeInfo();

            oak.WeaponColdDamage = 20;
            oak.WeaponDurability = 10;
            oak.WeaponLowerRequirements = 5;
            oak.RunicMinAttributes = 1;
            oak.RunicMaxAttributes = Uber ? 2 : 2;
            oak.RunicMinIntensity = Uber ? 10 : 5;
            oak.RunicMaxIntensity = 100;

            CraftAttributeInfo ash = AshWood = new CraftAttributeInfo();

            ash.WeaponFireDamage = 40;
            ash.WeaponDurability = 25;
            ash.WeaponLowerRequirements = 10;
            ash.RunicMinAttributes = 2;
            ash.RunicMaxAttributes = Uber ? 3 : 2;
            ash.RunicMinIntensity = Uber ? 15 : 10;
            ash.RunicMaxIntensity = 100;

            CraftAttributeInfo yew = YewWood = new CraftAttributeInfo();

            yew.WeaponPoisonDamage = 10;
            yew.WeaponEnergyDamage = 20;
            yew.WeaponDurability = 50;
            yew.WeaponLowerRequirements = 20;
            yew.RunicMinAttributes = 1;
            yew.RunicMaxAttributes = Uber ? 4 : 3;
            yew.RunicMinIntensity = Uber ? 20 : 15;
            yew.RunicMaxIntensity = 100;

            CraftAttributeInfo heartwood = Heartwood = new CraftAttributeInfo();

            heartwood.WeaponColdDamage = 30;
            heartwood.WeaponEnergyDamage = 20;
            heartwood.WeaponDurability = 75;
            heartwood.WeaponLowerRequirements = 30;
            heartwood.RunicMinAttributes = 2;
            heartwood.RunicMaxAttributes = Uber ? 5 : 3;
            heartwood.RunicMinIntensity = Uber ? 30 : 20;
            heartwood.RunicMaxIntensity = 100;

            CraftAttributeInfo bloodwood = Bloodwood = new CraftAttributeInfo();

            bloodwood.WeaponPoisonDamage = 40;
            bloodwood.WeaponEnergyDamage = 20;
            bloodwood.WeaponDurability = 100;
            bloodwood.WeaponLowerRequirements = 40;
            bloodwood.RunicMinAttributes = 3;
            bloodwood.RunicMaxAttributes = Uber ? 5 : 3;
            bloodwood.RunicMinIntensity = Uber ? 40 : 25;
            bloodwood.RunicMaxIntensity = 100;

            CraftAttributeInfo Frostwood = Frostwood = new CraftAttributeInfo();

            Frostwood.WeaponDurability = 125;
            Frostwood.WeaponLowerRequirements = 50;
            Frostwood.WeaponFireDamage = 25;
            Frostwood.WeaponColdDamage = 25;
            Frostwood.WeaponPoisonDamage = 25;
            Frostwood.WeaponEnergyDamage = 25;
            Frostwood.RunicMinAttributes = 2;
            Frostwood.RunicMaxAttributes = Uber ? 6 : 4;
            Frostwood.RunicMinIntensity = Uber ? 50 : 30;
            Frostwood.RunicMaxIntensity = 100;

			CraftAttributeInfo ebony = Ebony = new CraftAttributeInfo();

			ebony.WeaponDurability = 150;
			ebony.WeaponLowerRequirements = 60;
			ebony.WeaponColdDamage = 100;
			ebony.RunicMinAttributes = 3;
			ebony.RunicMaxAttributes = Uber ? 6 : 4;
			ebony.RunicMinIntensity = Uber ? 60 : 35;
			ebony.RunicMaxIntensity = 100;
			
			CraftAttributeInfo bamboo = Bamboo = new CraftAttributeInfo();

			bamboo.WeaponDurability = 175;
			bamboo.WeaponLowerRequirements = 70;
			bamboo.WeaponEnergyDamage = 100;
			bamboo.RunicMinAttributes = 4;
			bamboo.RunicMaxAttributes = Uber ? 7 : 4;
			bamboo.RunicMinIntensity = Uber ? 70 : 40;
			bamboo.RunicMaxIntensity = 100;

			CraftAttributeInfo purpleheart = PurpleHeart = new CraftAttributeInfo();

			purpleheart.WeaponDurability = 200;
			purpleheart.WeaponLowerRequirements = 80;
			purpleheart.WeaponFireDamage = 100;
			purpleheart.RunicMinAttributes = 3;
			purpleheart.RunicMaxAttributes = Uber ? 7 : 5;
			purpleheart.RunicMinIntensity = Uber ? 80 : 50;
			purpleheart.RunicMaxIntensity = 100;

			CraftAttributeInfo redwood = Redwood = new CraftAttributeInfo();

			redwood.WeaponDurability = 225;
			redwood.WeaponLowerRequirements = 90;
			redwood.WeaponPoisonDamage = 100;
			redwood.RunicMinAttributes = 4;
			redwood.RunicMaxAttributes = Uber ? 8 : 5;
			redwood.RunicMinIntensity = Uber ? 90 : 55;
			redwood.RunicMaxIntensity = 100;

			CraftAttributeInfo petrified = Petrified = new CraftAttributeInfo();

			petrified.WeaponDurability = 250;
			petrified.WeaponLowerRequirements = 100;
			petrified.RunicMinAttributes = 5;
			petrified.RunicMaxAttributes = Uber ? 8 : 5;
			petrified.RunicMinIntensity = Uber ? 100 : 60;
			petrified.RunicMaxIntensity = 100;
			//daat99 OWLTR end - custom resources
			
        }
    }

    public class CraftResourceInfo
    {
        private readonly int m_Hue;
        private readonly int m_Number;
        private readonly string m_Name;
        private readonly CraftAttributeInfo m_AttributeInfo;
        private readonly CraftResource m_Resource;
        private readonly Type[] m_ResourceTypes;

        public int Hue
        {
            get
            {
                return this.m_Hue;
            }
        }
        public int Number
        {
            get
            {
                return this.m_Number;
            }
        }
        public string Name
        {
            get
            {
                return this.m_Name;
            }
        }
        public CraftAttributeInfo AttributeInfo
        {
            get
            {
                return this.m_AttributeInfo;
            }
        }
        public CraftResource Resource
        {
            get
            {
                return this.m_Resource;
            }
        }
        public Type[] ResourceTypes
        {
            get
            {
                return this.m_ResourceTypes;
            }
        }

        public CraftResourceInfo(int hue, int number, string name, CraftAttributeInfo attributeInfo, CraftResource resource, params Type[] resourceTypes)
        {
            this.m_Hue = hue;
            this.m_Number = number;
            this.m_Name = name;
            this.m_AttributeInfo = attributeInfo;
            this.m_Resource = resource;
            this.m_ResourceTypes = resourceTypes;

            for (int i = 0; i < resourceTypes.Length; ++i)
                CraftResources.RegisterType(resourceTypes[i], resource);
        }
    }

    public class CraftResources
    {
        private static readonly CraftResourceInfo[] m_MetalInfo = new CraftResourceInfo[]
        {
            new CraftResourceInfo(0x000, 1053109, "Iron", CraftAttributeInfo.Blank, CraftResource.Iron, typeof(IronIngot), typeof(IronOre), typeof(Granite)),
            new CraftResourceInfo(0x973, 1053108, "Dull Copper",	CraftAttributeInfo.DullCopper,	CraftResource.DullCopper, typeof(DullCopperIngot),	typeof(DullCopperOre),	typeof(DullCopperGranite)),
            new CraftResourceInfo(0x966, 1053107, "Shadow Iron",	CraftAttributeInfo.ShadowIron,	CraftResource.ShadowIron, typeof(ShadowIronIngot),	typeof(ShadowIronOre),	typeof(ShadowIronGranite)),
            new CraftResourceInfo(0x96D, 1053106, "Copper", CraftAttributeInfo.Copper, CraftResource.Copper, typeof(CopperIngot), typeof(CopperOre), typeof(CopperGranite)),
            new CraftResourceInfo(0x972, 1053105, "Bronze", CraftAttributeInfo.Bronze, CraftResource.Bronze, typeof(BronzeIngot), typeof(BronzeOre), typeof(BronzeGranite)),
            new CraftResourceInfo(0x8A5, 1053104, "Gold", CraftAttributeInfo.Golden, CraftResource.Gold, typeof(GoldIngot), typeof(GoldOre), typeof(GoldGranite)),
            new CraftResourceInfo(0x979, 1053103, "Agapite", CraftAttributeInfo.Agapite, CraftResource.Agapite, typeof(AgapiteIngot), typeof(AgapiteOre), typeof(AgapiteGranite)),
            new CraftResourceInfo(0x89F, 1053102, "Verite", CraftAttributeInfo.Verite, CraftResource.Verite, typeof(VeriteIngot), typeof(VeriteOre), typeof(VeriteGranite)),
            new CraftResourceInfo(0x8AB, 1053101, "Valorite", CraftAttributeInfo.Valorite,	CraftResource.Valorite, typeof(ValoriteIngot),	typeof(ValoriteOre), typeof(ValoriteGranite)),
       
		new CraftResourceInfo( 1161,	0,		"Blaze",		CraftAttributeInfo.Blaze,		CraftResource.Blaze,			typeof( BlazeIngot ),		typeof( BlazeOre ),			typeof( BlazeGranite ) ),
				new CraftResourceInfo( 1152,	0,		"Ice",			CraftAttributeInfo.Ice,			CraftResource.Ice,				typeof( IceIngot ),			typeof( IceOre ),			typeof( IceGranite ) ),
				new CraftResourceInfo( 1272,	0,		"Toxic",		CraftAttributeInfo.Toxic,		CraftResource.Toxic,			typeof( ToxicIngot ),		typeof( ToxicOre ),			typeof( ToxicGranite ) ),
				new CraftResourceInfo( 1278,	0,		"Electrum",		CraftAttributeInfo.Electrum,	CraftResource.Electrum,			typeof( ElectrumIngot ),	typeof( ElectrumOre ),		typeof( ElectrumGranite ) ),
				new CraftResourceInfo( 1153,	0,		"Platinum",		CraftAttributeInfo.Platinum,	CraftResource.Platinum,			typeof( PlatinumIngot ),	typeof( PlatinumOre ),		typeof( PlatinumGranite ) ),
				new CraftResourceInfo( 1153,	0,		"Royalite",		CraftAttributeInfo.Royalite,	CraftResource.Royalite,			typeof( RoyaliteIngot ),	typeof( RoyaliteOre ),		typeof( RoyaliteGranite ) ),
				new CraftResourceInfo( 1153,	0,		"Danite",		CraftAttributeInfo.Danite,	CraftResource.Danite,			typeof( DaniteIngot ),	typeof( DaniteOre ),		typeof( DaniteGranite ) ),
 };
        private static readonly CraftResourceInfo[] m_ScaleInfo = new CraftResourceInfo[]
        {
            new CraftResourceInfo(0x66D, 1053129, "Red Scales",	CraftAttributeInfo.RedScales, CraftResource.RedScales, typeof(RedScales)),
            new CraftResourceInfo(0x8A8, 1053130, "Yellow Scales",	CraftAttributeInfo.YellowScales,	CraftResource.YellowScales, typeof(YellowScales)),
            new CraftResourceInfo(0x455, 1053131, "Black Scales",	CraftAttributeInfo.BlackScales, CraftResource.BlackScales, typeof(BlackScales)),
            new CraftResourceInfo(0x851, 1053132, "Green Scales",	CraftAttributeInfo.GreenScales, CraftResource.GreenScales, typeof(GreenScales)),
            new CraftResourceInfo(0x8FD, 1053133, "White Scales",	CraftAttributeInfo.WhiteScales, CraftResource.WhiteScales, typeof(WhiteScales)),
            new CraftResourceInfo(0x8B0, 1053134, "Blue Scales",	CraftAttributeInfo.BlueScales, CraftResource.BlueScales, typeof(BlueScales)),
			new CraftResourceInfo( 0x96D,	0,		"Copper Scales",	CraftAttributeInfo.CopperScales,	CraftResource.CopperScales,		typeof( CopperScales ) ),
				new CraftResourceInfo( 0x8FD,	0,		"Silver Scales",	CraftAttributeInfo.SilverScales,	CraftResource.SilverScales,		typeof( SilverScales ) ),
				new CraftResourceInfo( 49,  	0,		"Gold Scales",		CraftAttributeInfo.GoldScales,		CraftResource.GoldScales,		typeof( GoldScales ) ),
		};

        private static readonly CraftResourceInfo[] m_LeatherInfo = new CraftResourceInfo[]
        {
            new CraftResourceInfo(0x000, 1049353, "Normal", CraftAttributeInfo.Blank, CraftResource.RegularLeather,	typeof(Leather), typeof(Hides)),
            new CraftResourceInfo(0x283, 1049354, "Spined", CraftAttributeInfo.Spined, CraftResource.SpinedLeather,	typeof(SpinedLeather),	typeof(SpinedHides)),
            new CraftResourceInfo(0x227, 1049355, "Horned", CraftAttributeInfo.Horned, CraftResource.HornedLeather,	typeof(HornedLeather),	typeof(HornedHides)),
            new CraftResourceInfo(0x1C1, 1049356, "Barbed", CraftAttributeInfo.Barbed, CraftResource.BarbedLeather,	typeof(BarbedLeather),	typeof(BarbedHides)),
        new CraftResourceInfo( 1150,	0,			"Polar",		CraftAttributeInfo.Polar,			CraftResource.PolarLeather,		typeof( PolarLeather ),		typeof( PolarHides ) ),
				new CraftResourceInfo( 1023,	0,			"Synthetic",	CraftAttributeInfo.Synthetic,		CraftResource.SyntheticLeather,	typeof( SyntheticLeather ),	typeof( SyntheticHides ) ),
				new CraftResourceInfo( 1260,	0,			"Blaze",		CraftAttributeInfo.BlazeL,			CraftResource.BlazeLeather,		typeof( BlazeLeather ),		typeof( BlazeHides ) ),
				new CraftResourceInfo( 32,		0,			"Daemonic",		CraftAttributeInfo.Daemonic,		CraftResource.DaemonicLeather,	typeof( DaemonicLeather ),	typeof( DaemonicHides ) ),
				new CraftResourceInfo( 0x966,	0,			"Shadow",		CraftAttributeInfo.Shadow,			CraftResource.ShadowLeather,	typeof( ShadowLeather ),	typeof( ShadowHides ) ),
				new CraftResourceInfo( 93,		0,			"Frost",		CraftAttributeInfo.Frost,			CraftResource.FrostLeather,		typeof( FrostLeather ),		typeof( FrostHides ) ),
				new CraftResourceInfo( 1159,	0,			"Ethereal",		CraftAttributeInfo.Ethereal,		CraftResource.EtherealLeather,	typeof( EtherealLeather ),	typeof( EtherealHides ) ),
				//daat99 OWLTR end - custom leather
		};

        private static readonly CraftResourceInfo[] m_AOSLeatherInfo = new CraftResourceInfo[]
        {
            new CraftResourceInfo(0x000, 1049353, "Normal", CraftAttributeInfo.Blank, CraftResource.RegularLeather,	typeof(Leather), typeof(Hides)),
            new CraftResourceInfo(0x8AC, 1049354, "Spined", CraftAttributeInfo.Spined, CraftResource.SpinedLeather,	typeof(SpinedLeather),	typeof(SpinedHides)),
            new CraftResourceInfo(0x845, 1049355, "Horned", CraftAttributeInfo.Horned, CraftResource.HornedLeather,	typeof(HornedLeather),	typeof(HornedHides)),
            new CraftResourceInfo(0x851, 1049356, "Barbed", CraftAttributeInfo.Barbed, CraftResource.BarbedLeather,	typeof(BarbedLeather),	typeof(BarbedHides)),
			new CraftResourceInfo( 1150,	0,			"Polar",		CraftAttributeInfo.Polar,			CraftResource.PolarLeather,		typeof( PolarLeather ),		typeof( PolarHides ) ),
				new CraftResourceInfo( 1023,	0,			"Synthetic",	CraftAttributeInfo.Synthetic,		CraftResource.SyntheticLeather,	typeof( SyntheticLeather ),	typeof( SyntheticHides ) ),
				new CraftResourceInfo( 1260,	0,			"Blaze",		CraftAttributeInfo.BlazeL,			CraftResource.BlazeLeather,		typeof( BlazeLeather ),		typeof( BlazeHides ) ),
				new CraftResourceInfo( 32,		0,			"Daemonic",		CraftAttributeInfo.Daemonic,		CraftResource.DaemonicLeather,	typeof( DaemonicLeather ),	typeof( DaemonicHides ) ),
				new CraftResourceInfo( 0x966,	0,			"Shadow",		CraftAttributeInfo.Shadow,			CraftResource.ShadowLeather,	typeof( ShadowLeather ),	typeof( ShadowHides ) ),
				new CraftResourceInfo( 93,		0,			"Frost",		CraftAttributeInfo.Frost,			CraftResource.FrostLeather,		typeof( FrostLeather ),		typeof( FrostHides ) ),
				new CraftResourceInfo( 1159,	0,			"Ethereal",		CraftAttributeInfo.Ethereal,		CraftResource.EtherealLeather,	typeof( EtherealLeather ),	typeof( EtherealHides ) ),
				//daat99 OWLTR end - custom leather
        };

        private static readonly CraftResourceInfo[] m_WoodInfo = new CraftResourceInfo[]
        {
            new CraftResourceInfo(0x000, 1011542, "Normal", CraftAttributeInfo.Blank, CraftResource.RegularWood,	typeof(Log), typeof(Board)),
            new CraftResourceInfo(0x7DA, 1072533, "Oak", CraftAttributeInfo.OakWood, CraftResource.OakWood, typeof(OakLog), typeof(OakBoard)),
            new CraftResourceInfo(0x4A7, 1072534, "Ash", CraftAttributeInfo.AshWood, CraftResource.AshWood, typeof(AshLog), typeof(AshBoard)),
            new CraftResourceInfo(0x4A8, 1072535, "Yew", CraftAttributeInfo.YewWood, CraftResource.YewWood, typeof(YewLog), typeof(YewBoard)),
            new CraftResourceInfo(0x4A9, 1072536, "Heartwood", CraftAttributeInfo.Heartwood,	CraftResource.Heartwood,	typeof(HeartwoodLog),	typeof(HeartwoodBoard)),
            new CraftResourceInfo(0x4AA, 1072538, "Bloodwood", CraftAttributeInfo.Bloodwood,	CraftResource.Bloodwood,	typeof(BloodwoodLog),	typeof(BloodwoodBoard)),
            new CraftResourceInfo(0x47F, 1072539, "Frostwood", CraftAttributeInfo.Frostwood,	CraftResource.Frostwood,	typeof(FrostwoodLog),	typeof(FrostwoodBoard)),
						new CraftResourceInfo( 1457, 0, "Ebony",		CraftAttributeInfo.Ebony,		CraftResource.Ebony,		typeof(EbonyBoard),			typeof( EbonyLog ) ),
				new CraftResourceInfo( 1719, 0,	"Bamboo",		CraftAttributeInfo.Bamboo,		CraftResource.Bamboo,		typeof(BambooBoard),		typeof( BambooLog ) ),
				new CraftResourceInfo( 114,  0, "PurpleHeart",	CraftAttributeInfo.PurpleHeart,	CraftResource.PurpleHeart,	typeof(PurpleHeartBoard),	typeof( PurpleHeartLog ) ),
				new CraftResourceInfo( 37,   0,	"Redwood",		CraftAttributeInfo.Redwood,		CraftResource.Redwood,		typeof(RedwoodBoard),		typeof( RedwoodLog ) ),
				new CraftResourceInfo( 1153, 0, "Petrified",	CraftAttributeInfo.Petrified,	CraftResource.Petrified,	typeof(PetrifiedBoard),		typeof( PetrifiedLog ) ),
				//daat99 OWLTR end - custom wood
        };

        /// <summary>
        /// Returns true if '<paramref name="resource"/>' is None, Iron, RegularLeather or RegularWood. False if otherwise.
        /// </summary>
        public static bool IsStandard(CraftResource resource)
        {
            return (resource == CraftResource.None || resource == CraftResource.Iron || resource == CraftResource.RegularLeather || resource == CraftResource.RegularWood);
        }

        private static Hashtable m_TypeTable;

        /// <summary>
        /// Registers that '<paramref name="resourceType"/>' uses '<paramref name="resource"/>' so that it can later be queried by <see cref="CraftResources.GetFromType"/>
        /// </summary>
        public static void RegisterType(Type resourceType, CraftResource resource)
        {
            if (m_TypeTable == null)
                m_TypeTable = new Hashtable();

            m_TypeTable[resourceType] = resource;
        }

        /// <summary>
        /// Returns the <see cref="CraftResource"/> value for which '<paramref name="resourceType"/>' uses -or- CraftResource.None if an unregistered type was specified.
        /// </summary>
        public static CraftResource GetFromType(Type resourceType)
        {
            if (m_TypeTable == null)
                return CraftResource.None;

            object obj = m_TypeTable[resourceType];

            if (!(obj is CraftResource))
                return CraftResource.None;

            return (CraftResource)obj;
        }

        /// <summary>
        /// Returns a <see cref="CraftResourceInfo"/> instance describing '<paramref name="resource"/>' -or- null if an invalid resource was specified.
        /// </summary>
        public static CraftResourceInfo GetInfo(CraftResource resource)
        {
            CraftResourceInfo[] list = null;

            switch (GetType(resource))
            {
                case CraftResourceType.Metal:
                    list = m_MetalInfo;
                    break;
                case CraftResourceType.Leather:
                    list = Core.AOS ? m_AOSLeatherInfo : m_LeatherInfo;
                    break;
                case CraftResourceType.Scales:
                    list = m_ScaleInfo;
                    break;
                case CraftResourceType.Wood:
                    list = m_WoodInfo;
                    break;
            }

            if (list != null)
            {
                int index = GetIndex(resource);

                if (index >= 0 && index < list.Length)
                    return list[index];
            }

            return null;
        }

        /// <summary>
        /// Returns a <see cref="CraftResourceType"/> value indiciating the type of '<paramref name="resource"/>'.
        /// </summary>
        public static CraftResourceType GetType(CraftResource resource)
        {
            if (resource >= CraftResource.Iron && resource <= CraftResource.Danite)
                return CraftResourceType.Metal;

            if (resource >= CraftResource.RegularLeather && resource <= CraftResource.EtherealLeather)
                return CraftResourceType.Leather;

            if (resource >= CraftResource.RedScales && resource <= CraftResource.GoldScales)
                return CraftResourceType.Scales;

            if (resource >= CraftResource.RegularWood && resource <= CraftResource.Petrified)
                return CraftResourceType.Wood;

            return CraftResourceType.None;
        }

        /// <summary>
        /// Returns the first <see cref="CraftResource"/> in the series of resources for which '<paramref name="resource"/>' belongs.
        /// </summary>
        public static CraftResource GetStart(CraftResource resource)
        {
            switch (GetType(resource))
            {
                case CraftResourceType.Metal:
                    return CraftResource.Iron;
                case CraftResourceType.Leather:
                    return CraftResource.RegularLeather;
                case CraftResourceType.Scales:
                    return CraftResource.RedScales;
                case CraftResourceType.Wood:
                    return CraftResource.RegularWood;
            }

            return CraftResource.None;
        }

        /// <summary>
        /// Returns the index of '<paramref name="resource"/>' in the seriest of resources for which it belongs.
        /// </summary>
        public static int GetIndex(CraftResource resource)
        {
            CraftResource start = GetStart(resource);

            if (start == CraftResource.None)
                return 0;

            return (int)(resource - start);
        }

        /// <summary>
        /// Returns the <see cref="CraftResourceInfo.Number"/> property of '<paramref name="resource"/>' -or- 0 if an invalid resource was specified.
        /// </summary>
        public static int GetLocalizationNumber(CraftResource resource)
        {
            CraftResourceInfo info = GetInfo(resource);

            return (info == null ? 0 : info.Number);
        }

        /// <summary>
        /// Returns the <see cref="CraftResourceInfo.Hue"/> property of '<paramref name="resource"/>' -or- 0 if an invalid resource was specified.
        /// </summary>
        public static int GetHue(CraftResource resource)
        {
            CraftResourceInfo info = GetInfo(resource);

            return (info == null ? 0 : info.Hue);
        }

        /// <summary>
        /// Returns the <see cref="CraftResourceInfo.Name"/> property of '<paramref name="resource"/>' -or- an empty string if the resource specified was invalid.
        /// </summary>
        public static string GetName(CraftResource resource)
        {
            CraftResourceInfo info = GetInfo(resource);

            return (info == null ? String.Empty : info.Name);
        }

        /// <summary>
        /// Returns the <see cref="CraftResource"/> value which represents '<paramref name="info"/>' -or- CraftResource.None if unable to convert.
        /// </summary>
        public static CraftResource GetFromOreInfo(OreInfo info)
        {
            if (info.Name.IndexOf("Spined") >= 0)
                return CraftResource.SpinedLeather;
            else if (info.Name.IndexOf("Horned") >= 0)
                return CraftResource.HornedLeather;
            else if (info.Name.IndexOf("Barbed") >= 0)
                return CraftResource.BarbedLeather;
            else if (info.Name.IndexOf("Leather") >= 0)
                return CraftResource.RegularLeather;

            if (info.Level == 0)
                return CraftResource.Iron;
            else if (info.Level == 1)
                return CraftResource.DullCopper;
            else if (info.Level == 2)
                return CraftResource.ShadowIron;
            else if (info.Level == 3)
                return CraftResource.Copper;
            else if (info.Level == 4)
                return CraftResource.Bronze;
            else if (info.Level == 5)
                return CraftResource.Gold;
            else if (info.Level == 6)
                return CraftResource.Agapite;
            else if (info.Level == 7)
                return CraftResource.Verite;
            else if (info.Level == 8)
                return CraftResource.Valorite;
			else if (info.Level == 9)
                return CraftResource.Blaze;
			else if (info.Level == 10)
                return CraftResource.Ice;
			else if (info.Level == 11)
                return CraftResource.Toxic;
			else if (info.Level == 12)
                return CraftResource.Electrum;
			else if (info.Level == 13)
                return CraftResource.Platinum;
				else if (info.Level == 14)
                return CraftResource.Royalite;
				else if (info.Level == 15)
                return CraftResource.Danite;

            return CraftResource.None;
        }

        /// <summary>
        /// Returns the <see cref="CraftResource"/> value which represents '<paramref name="info"/>', using '<paramref name="material"/>' to help resolve leather OreInfo instances.
        /// </summary>
        public static CraftResource GetFromOreInfo(OreInfo info, ArmorMaterialType material)
        {
            if (material == ArmorMaterialType.Studded || material == ArmorMaterialType.Leather || material == ArmorMaterialType.Spined ||
                material == ArmorMaterialType.Horned || material == ArmorMaterialType.Barbed)
            {
                if (info.Level == 0)
                    return CraftResource.RegularLeather;
                else if (info.Level == 1)
                    return CraftResource.SpinedLeather;
                else if (info.Level == 2)
                    return CraftResource.HornedLeather;
                else if (info.Level == 3)
                    return CraftResource.BarbedLeather;

                return CraftResource.None;
            }

            return GetFromOreInfo(info);
        }
    }

    // NOTE: This class is only for compatability with very old RunUO versions.
    // No changes to it should be required for custom resources.
    public class OreInfo
    {
        public static readonly OreInfo Iron = new OreInfo(0, 0x000, "Iron");
        public static readonly OreInfo DullCopper = new OreInfo(1, 0x973, "Dull Copper");
        public static readonly OreInfo ShadowIron = new OreInfo(2, 0x966, "Shadow Iron");
        public static readonly OreInfo Copper = new OreInfo(3, 0x96D, "Copper");
        public static readonly OreInfo Bronze = new OreInfo(4, 0x972, "Bronze");
        public static readonly OreInfo Gold = new OreInfo(5, 0x8A5, "Gold");
        public static readonly OreInfo Agapite = new OreInfo(6, 0x979, "Agapite");
        public static readonly OreInfo Verite = new OreInfo(7, 0x89F, "Verite");
        public static readonly OreInfo Valorite = new OreInfo(8, 0x8AB, "Valorite");
			//daat99 OWLTR start - custom ores
		public static readonly OreInfo Blaze		= new OreInfo(  9, 1161, "Blaze" );
		public static readonly OreInfo Ice			= new OreInfo( 10, 1152, "Ice" );
		public static readonly OreInfo Toxic		= new OreInfo( 11, 1272, "Toxic" );
		public static readonly OreInfo Electrum		= new OreInfo( 12, 1278, "Electrum" );
		public static readonly OreInfo Platinum		= new OreInfo( 13, 1153, "Platinum" );
		public static readonly OreInfo Royalite		= new OreInfo( 13, 1153, "Royalite" );
		public static readonly OreInfo Danite		= new OreInfo( 13, 1153, "Danite" );
		

        private readonly int m_Level;
        private readonly int m_Hue;
        private readonly string m_Name;

        public OreInfo(int level, int hue, string name)
        {
            this.m_Level = level;
            this.m_Hue = hue;
            this.m_Name = name;
        }

        public int Level
        {
            get
            {
                return this.m_Level;
            }
        }

        public int Hue
        {
            get
            {
                return this.m_Hue;
            }
        }

        public string Name
        {
            get
            {
                return this.m_Name;
            }
        }
    }
}