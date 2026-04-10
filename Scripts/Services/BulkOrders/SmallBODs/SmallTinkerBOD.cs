using System;
using System.Collections.Generic;
using Server.Engines.Craft;
using Server.Items;
using System.Linq;

namespace Server.Engines.BulkOrders
{
    public class SmallTinkerBOD : SmallBOD
    {
        public override BODType BODType { get { return BODType.Tinkering; } }

        private GemType _GemType;

        [CommandProperty(AccessLevel.GameMaster)]
        public GemType GemType
        {
            get { return _GemType; }
            set
            {
                if (this.Type != null && this.Type.IsSubclassOf(typeof(BaseJewel)))
                {
                    _GemType = value;
                    AssignGemNumber(this.Type);

                    InvalidateProperties();
                }
            }
        }

        public static readonly BulkMaterialType[] m_TinkerMaterials = new BulkMaterialType[]
        {
            BulkMaterialType.None,
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

        public static readonly double[] m_TinkerMaterialWeights = new double[]
        {
            512.0,
            256.0,
            128.0,
            64.0,
            32.0,
            16.0,
            8.0,
            4.0,
            2.0,
            1.0,
            0.9,
            0.8,
            0.7,
            0.6,
            0.5,
            0.45,
            0.4,
            0.35,
            0.3,
            0.25,
            0.2,
            0.15,
            0.12,
            0.09,
            0.06,
            0.03,
            0.015
        };

        [Constructable]
        public SmallTinkerBOD()
        {
            SmallBulkEntry[] entries;
            bool useMaterials;

            if (useMaterials = 0.75 > Utility.RandomDouble())
                entries = SmallBulkEntry.TinkeringSmalls;
            else
                entries = SmallBulkEntry.TinkeringSmallsRegular;

            if (entries.Length > 0)
            {
                int amountMax = Utility.RandomList(10, 15, 20);

                BulkMaterialType material;

                if (useMaterials)
                    material = GetRandomMaterial(m_TinkerMaterials, m_TinkerMaterialWeights);
                else
                    material = BulkMaterialType.None;

                bool reqExceptional = useMaterials ? Utility.RandomBool() : false;

                SmallBulkEntry entry = entries[Utility.Random(entries.Length)];

                if (material != BulkMaterialType.None && CannotAssignMaterial(entry.Type))
                {
                    material = BulkMaterialType.None;
                }

                Hue = 1109;
                AmountMax = amountMax;
                Type = entry.Type;
                Number = entry.Number;
                Graphic = entry.Graphic;
                RequireExceptional = reqExceptional;
                Material = material;
                GraphicHue = entry.Hue;

                if (entry.Type.IsSubclassOf(typeof(BaseJewel)))
                {
                    AssignGemType(entry.Type);
                }
            }
        }

        public SmallTinkerBOD(int amountCur, int amountMax, Type type, int number, int graphic, bool reqExceptional, BulkMaterialType mat, int hue, GemType gemType)
        {
            Hue = 1109;
            AmountMax = amountMax;
            AmountCur = amountCur;
            Type = type;
            Number = number;
            Graphic = graphic;
            RequireExceptional = reqExceptional;
            Material = mat;
            GraphicHue = hue;
            GemType = gemType;
        }

        public SmallTinkerBOD(Serial serial)
            : base(serial)
        {
        }

        private SmallTinkerBOD(SmallBulkEntry entry, BulkMaterialType material, int amountMax, bool reqExceptional)
        {
            Hue = 1109;
            AmountMax = amountMax;
            Type = entry.Type;
            Number = entry.Number;
            Graphic = entry.Graphic;
            RequireExceptional = reqExceptional;
            Material = material;
        }

        public static SmallTinkerBOD CreateRandomFor(Mobile m)
        {
            SmallBulkEntry[] entries;
            bool useMaterials;

            if (useMaterials = 0.75 > Utility.RandomDouble())
                entries = SmallBulkEntry.TinkeringSmalls;
            else
                entries = SmallBulkEntry.TinkeringSmallsRegular;

            if (entries.Length > 0)
            {
                double theirSkill = BulkOrderSystem.GetBODSkill(m, SkillName.Tinkering);
                int amountMax;

                if (theirSkill >= 70.1)
                    amountMax = Utility.RandomList(10, 15, 20, 20);
                else if (theirSkill >= 50.1)
                    amountMax = Utility.RandomList(10, 15, 15, 20);
                else
                    amountMax = Utility.RandomList(10, 10, 15, 20);

                BulkMaterialType material = BulkMaterialType.None;

                if (useMaterials && theirSkill >= 70.1)
                {
                    for (int i = 0; i < 20; ++i)
                    {
                        BulkMaterialType check = GetRandomMaterial(m_TinkerMaterials, m_TinkerMaterialWeights);
                        double skillReq = GetRequiredSkill(check);

                        if (theirSkill >= skillReq)
                        {
                            material = check;
                            break;
                        }
                    }
                }

                double excChance = 0.0;

                if (useMaterials && theirSkill >= 70.1)
                    excChance = (theirSkill + 80.0) / 200.0;

                bool reqExceptional = (excChance > Utility.RandomDouble());

                CraftSystem system = DefTinkering.CraftSystem;

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

                    if (material != BulkMaterialType.None && CannotAssignMaterial(entry.Type))
                    {
                        material = BulkMaterialType.None;
                    }

                    var bod = new SmallTinkerBOD(entry, material, amountMax, reqExceptional);

                    if (entry.Type.IsSubclassOf(typeof(BaseJewel)))
                    {
                        bod.AssignGemType(entry.Type);
                    }

                    return bod;
                }
            }

            return null;
        }

        public static bool CannotAssignMaterial(Type t)
        {
            return _NonMaterials.Any(x => x == t || t.IsSubclassOf(x));
        }

        private static Type[] _NonMaterials =
        {
            typeof(BaseTool), typeof(SmithyHammer), typeof(BaseJewel)
        };

        public override bool CheckType(Type type)
        {
            bool check = base.CheckType(type);

            if (!check)
            {
                check = CheckTinkerType(type, Type);
            }

            return check;
        }

        public override bool CheckType(Item item)
        {
            if (_GemType != GemType.None && (!(item is BaseJewel) || ((BaseJewel)item).GemType != _GemType))
            {
                return false;
            }

            bool check = base.CheckType(item);

            if (!check)
            {
                check = CheckTinkerType(item.GetType(), Type);
            }

            return check;
        }

        /* Tinkering needs conditional check for combining:
        * SpoonLeft/SpoonRight, ForkLeft/ForkRight, KnifeLeft/KnifeRight, ClockRight/ClockLeft
        */
        private static Type[][] _TinkerTypeTable =
        {
            new Type[] { typeof(Spoon), typeof(SpoonRight), typeof(SpoonLeft) },
            new Type[] { typeof(Fork), typeof(ForkRight), typeof(ForkLeft) },
            new Type[] { typeof(Knife), typeof(KnifeRight), typeof(KnifeLeft) },
            new Type[] { typeof(Clock), typeof(ClockRight), typeof(ClockLeft) },
            new Type[] { typeof(GoldRing), typeof(SilverRing) },
            new Type[] { typeof(GoldBracelet), typeof(SilverBracelet) },
            new Type[] { typeof(GoldEarrings), typeof(SilverEarrings) },
            new Type[] { typeof(SmithHammer), typeof(SmithyHammer) }
        };

        public static bool CheckTinkerType(Type actual, Type lookingfor)
        {
            foreach (Type[] types in _TinkerTypeTable)
            {
                foreach (Type t in types)
                {
                    if (t == lookingfor) // found the list, lets see if the actual is here
                    {
                        foreach (Type t2 in types)
                        {
                            if (t2 == actual)
                            {
                                return true;
                            }
                        }
                    }
                }

                /*if (types[0] == lookingfor)
                {
                    foreach (Type t in types)
                    {
                        if (actual == t)
                            return true;
                    }
                }*/
            }

            return false;
        }

        public override int ComputeFame()
        {
            return TinkeringRewardCalculator.Instance.ComputeFame(this);
        }

        public override int ComputeGold()
        {
            return TinkeringRewardCalculator.Instance.ComputeGold(this);
        }

        public override List<Item> ComputeRewards(bool full)
        {
            List<Item> list = new List<Item>();

            RewardGroup rewardGroup = TinkeringRewardCalculator.Instance.LookupRewards(TinkeringRewardCalculator.Instance.ComputePoints(this));

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

        public void AssignGemType(Type jewelType)
        {
            _GemType = (GemType)Utility.RandomMinMax(1, 9);

            AssignGemNumber(jewelType);
        }

        public void AssignGemNumber(Type jewelType)
        {
            int offset = (int)GemType - 1;
            int loc = 0;

            if (jewelType == typeof(GoldRing) || jewelType == typeof(SilverRing))
            {
                loc = 1044176;
            }
            else if (jewelType == typeof(GoldBracelet) || jewelType == typeof(SilverBracelet))
            {
                loc = 1044221;
            }
            else
            {
                loc = 1044203;
            }

            this.Number = loc + offset;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)2); // version

            writer.Write((int)GemType);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 2:
                    GemType = (GemType)reader.ReadInt();
                    break;
                case 1:
                    break;
            }

            if (version < 2)
            {
                if (CannotAssignMaterial(Type) && Material != BulkMaterialType.None)
                {
                    Material = BulkMaterialType.None;
                }

                if (this.Type.IsSubclassOf(typeof(BaseJewel)))
                {
                    AssignGemType(this.Type);
                }
            }
        }
    }
}
