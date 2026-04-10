using System;
using System.Collections.Generic;
using Server.Engines.Craft;

namespace Server.Engines.BulkOrders
{
    [TypeAlias("Scripts.Engines.BulkOrders.SmallSmithBOD")]
    public class SmallSmithBOD : SmallBOD
    {
        public override BODType BODType { get { return BODType.Smith; } }

                public static readonly BulkMaterialType[] m_BlacksmithMaterials = new BulkMaterialType[]
            {
                    BulkMaterialType.DullCopper,
                    BulkMaterialType.ShadowIron,
                    BulkMaterialType.Copper,
                    BulkMaterialType.Bronze,
                    BulkMaterialType.Gold,
                    BulkMaterialType.Agapite,
                    BulkMaterialType.Verite,
                    BulkMaterialType.Valorite,
                    BulkMaterialType.Blaze,
                    BulkMaterialType.Ice,
                    BulkMaterialType.Toxic,
                    BulkMaterialType.Electrum,
                    BulkMaterialType.Platinum,
                    BulkMaterialType.Barite,
                    BulkMaterialType.Wulfenite,
                    BulkMaterialType.Dragonite,
                    BulkMaterialType.Bunterite,
                    BulkMaterialType.Pineite,
                    BulkMaterialType.Samite,
                    BulkMaterialType.Toberite,
                    BulkMaterialType.Teal,
                    BulkMaterialType.Lisite,
                    BulkMaterialType.Marite,
                    BulkMaterialType.Royalite,
                    BulkMaterialType.Danite
                };

                public static readonly double[] m_BlacksmithMaterialWeights = new double[]
                {
                        95.0,
                        90.0,
                        90.0,
                        80.0,
                        80.0,
                        70.0,
                        70.0,
                        60.0,
                        50.0,
                        45.0,
                        40.0,
                        35.0,
                        25.0,
                        10.0,
                        9.5,
                        9.0,
                        8.5,
                        8.0,
                        7.5,
                        7.0,
                        6.5,
                        5.5,
                        4.5,
                        3.5,
                        2.5
        };
        [Constructable]
        public SmallSmithBOD()
        {
            bool useMaterials = Utility.RandomBool();
            SmallBulkEntry[] entries = Utility.RandomBool()
                ? SmallBulkEntry.BlacksmithArmor
                : SmallBulkEntry.BlacksmithWeapons;

            if (entries.Length > 0)
            {
                int hue = 0x44E;
                int amountMax = Utility.RandomList(10, 15, 20);

                BulkMaterialType material;

                if (useMaterials)
                    material = GetRandomMaterial(m_BlacksmithMaterials, m_BlacksmithMaterialWeights);
                else
                    material = BulkMaterialType.None;

                bool reqExceptional = Utility.RandomBool() || (material == BulkMaterialType.None);

                SmallBulkEntry entry = entries[Utility.Random(entries.Length)];

                Hue = hue;
                AmountMax = amountMax;
                Type = entry.Type;
                Number = entry.Number;
                Graphic = entry.Graphic;
                RequireExceptional = reqExceptional;
                Material = material;
                GraphicHue = entry.Hue;
            }
        }

        public SmallSmithBOD(int amountCur, int amountMax, Type type, int number, int graphic, bool reqExceptional, BulkMaterialType mat, int hue)
        {
            Hue = 0x44E;
            AmountMax = amountMax;
            AmountCur = amountCur;
            Type = type;
            Number = number;
            Graphic = graphic;
            RequireExceptional = reqExceptional;
            Material = mat;
            GraphicHue = hue;
        }

        public SmallSmithBOD(Serial serial)
            : base(serial)
        {
        }

        private SmallSmithBOD(SmallBulkEntry entry, BulkMaterialType material, int amountMax, bool reqExceptional)
        {
            Hue = 0x44E;
            AmountMax = amountMax;
            Type = entry.Type;
            Number = entry.Number;
            Graphic = entry.Graphic;
            RequireExceptional = reqExceptional;
            Material = material;
        }

        public static BulkMaterialType GetRandomMaterialForSkill(double theirSkill)
        {
            if (GetIronMaterialChance(theirSkill) > Utility.RandomDouble())
            {
                return BulkMaterialType.None;
            }

            for (int i = 0; i < 20; ++i)
            {
                BulkMaterialType check = GetRandomMaterial(m_BlacksmithMaterials, m_BlacksmithMaterialWeights);
                double skillReq = GetRequiredSkill(check);

                if (theirSkill >= skillReq)
                {
                    return check;
                }
            }

            return BulkMaterialType.None;
        }

        private static double GetIronMaterialChance(double theirSkill)
        {
            return 0.5;
        }

        public static SmallSmithBOD CreateRandomFor(Mobile m)
        {
            SmallBulkEntry[] entries = Utility.RandomBool()
                ? SmallBulkEntry.BlacksmithArmor
                : SmallBulkEntry.BlacksmithWeapons;

            if (entries.Length > 0)
            {
                double theirSkill = BulkOrderSystem.GetBODSkill(m, SkillName.Blacksmith);
                int amountMax;

                if (theirSkill >= 70.1)
                    amountMax = Utility.RandomList(10, 15, 20, 20);
                else if (theirSkill >= 50.1)
                    amountMax = Utility.RandomList(10, 15, 15, 20);
                else
                    amountMax = Utility.RandomList(10, 10, 15, 20);

                BulkMaterialType material = BulkMaterialType.None;

                if (theirSkill >= 70.1)
                {
                    material = GetRandomMaterialForSkill(theirSkill);
                }

                double excChance = 0.0;

                if (theirSkill >= 70.1)
                    excChance = (theirSkill + 80.0) / 200.0;

                bool reqExceptional = (excChance > Utility.RandomDouble());

                CraftSystem system = DefBlacksmithy.CraftSystem;

                List<SmallBulkEntry> validEntries = new List<SmallBulkEntry>();

                for (int i = 0; i < entries.Length; ++i)
                {
                    CraftItem item = system.CraftItems.SearchFor(entries[i].Type);

                    if (item != null)
                    {
                        bool allRequiredSkills = true;
                        double chance = item.GetSuccessChance(m, null, system, false, ref allRequiredSkills);

                        if (allRequiredSkills && chance >= 0.0)
                        {
                            if (reqExceptional)
                                chance = item.GetExceptionalChance(system, chance, m);

                            if (chance > 0.0)
                                validEntries.Add(entries[i]);
                        }
                    }
                }

                if (validEntries.Count > 0)
                {
                    SmallBulkEntry entry = validEntries[Utility.Random(validEntries.Count)];
                    return new SmallSmithBOD(entry, material, amountMax, reqExceptional);
                }
            }

            return null;
        }

        public override int ComputeFame()
        {
            return SmithRewardCalculator.Instance.ComputeFame(this);
        }

        public override int ComputeGold()
        {
            return SmithRewardCalculator.Instance.ComputeGold(this);
        }

        public override List<Item> ComputeRewards(bool full)
        {
            List<Item> list = new List<Item>();

            RewardGroup rewardGroup = SmithRewardCalculator.Instance.LookupRewards(SmithRewardCalculator.Instance.ComputePoints(this));

            if (rewardGroup != null)
            {
                if (full)
                {
                    for (int i = 0; i < rewardGroup.Items.Length; ++i)
                    {
                        Item item = rewardGroup.Items[i].Construct();

                        if (item != null)
                            list.Add(item);
                    }
                }
                else
                {
                    RewardItem rewardItem = rewardGroup.AcquireItem();

                    if (rewardItem != null)
                    {
                        Item item = rewardItem.Construct();

                        if (item != null)
                            list.Add(item);
                    }
                }
            }

            return list;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
