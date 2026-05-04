using System;
using Server.Engines.CannedEvil;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a power crystal elemental corpse")]
    public class PowerCrystalChampion : BaseChampion
    {
        private static readonly Type[] m_EmptyTypes = new Type[0];
        private static readonly MonsterStatuetteType[] m_EmptyStatues = new MonsterStatuetteType[0];

        private int m_EntryIndex;

        [Constructable]
        public PowerCrystalChampion()
            : base(AIType.AI_Melee)
        {
            Body = 14;
            BaseSoundID = 268;

            SetStr(850, 1000);
            SetDex(120, 180);
            SetInt(250, 350);

            SetHits(14000);
            SetStam(120, 180);
            SetMana(250, 350);

            SetDamage(24, 34);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 65, 80);
            SetResistance(ResistanceType.Fire, 35, 50);
            SetResistance(ResistanceType.Cold, 35, 50);
            SetResistance(ResistanceType.Poison, 45, 60);
            SetResistance(ResistanceType.Energy, 45, 60);

            SetSkill(SkillName.MagicResist, 120.0, 140.0);
            SetSkill(SkillName.Tactics, 110.0, 125.0);
            SetSkill(SkillName.Wrestling, 110.0, 125.0);

            Fame = 22500;
            Karma = -22500;

            VirtualArmor = 75;

            ApplyEntry(PowerCrystalChampRegistry.GetRandomIndex());
        }

        public PowerCrystalChampion(Serial serial)
            : base(serial)
        {
        }

        public override ChampionSkullType SkullType { get { return ChampionSkullType.None; } }
        public override Type[] UniqueList { get { return m_EmptyTypes; } }
        public override Type[] SharedList { get { return m_EmptyTypes; } }
        public override Type[] DecorativeList { get { return m_EmptyTypes; } }
        public override MonsterStatuetteType[] StatueTypes { get { return m_EmptyStatues; } }
        public override bool AutoDispel { get { return true; } }
        public override bool BardImmune { get { return true; } }
        public override bool BleedImmune { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 4);
            AddLoot(LootPack.Gems, 6);
        }

        public override void OnDeath(Container c)
        {
            if (c != null)
            {
                c.DropItem(PowerCrystalChampRegistry.GetEntry(m_EntryIndex).CreateItem());
            }

            base.OnDeath(c);
        }

        private void ApplyEntry(int index)
        {
            m_EntryIndex = index;

            PowerCrystalChampEntry entry = PowerCrystalChampRegistry.GetEntry(index);

            Name = entry.Name;
            Hue = entry.Hue;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0);
            writer.Write(m_EntryIndex);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version == 0)
            {
                m_EntryIndex = reader.ReadInt();
            }

            ApplyEntry(m_EntryIndex);
        }
    }
}
