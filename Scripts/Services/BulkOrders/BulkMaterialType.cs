using System;
using Server.Items;
using Server.Engines.Craft;

namespace Server.Engines.BulkOrders
{
    public enum BulkMaterialType
    {
        None,
        DullCopper,
        ShadowIron,
        Copper,
        Bronze,
        Gold,
        Agapite,
        Verite,
        Valorite,
        Blaze,
        Ice,
        Toxic,
        Electrum,
        Platinum,
		Royalite,
		Danite,
        Spined,
        Horned,
        Barbed,
        Polar,
        Synthetic,
        BlazeL,
        Daemonic,
        Shadow,
        Frost,
        Ethereal,
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
        Petrified,
        Barite,
        Wulfenite,
        Dragonite,
        Bunterite,
        Pineite,
        Samite,
        Toberite,
        Lisite,
        Marite,
        Teal
        //daat99 OWLTR end - custom resources
    }

    public static class BulkMaterialInfo
    {
        private static readonly BulkMaterialType[] m_SmithMetals =
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

        public static BulkMaterialType[] SmithMetals
        {
            get
            {
                return m_SmithMetals;
            }
        }

        public static bool IsSmithMetal(BulkMaterialType material)
        {
            return GetSmithMetalIndex(material) >= 0;
        }

        public static int GetSmithMetalIndex(BulkMaterialType material)
        {
            return Array.IndexOf(m_SmithMetals, material);
        }

        public static bool IsLastSmithMetal(BulkMaterialType material)
        {
            return GetSmithMetalIndex(material) == (m_SmithMetals.Length - 1);
        }

        public static BulkMaterialType GetNextSmithMetal(BulkMaterialType material)
        {
            int index = GetSmithMetalIndex(material);

            if (index < 0)
            {
                return m_SmithMetals[0];
            }

            if ((index + 1) < m_SmithMetals.Length)
            {
                return m_SmithMetals[index + 1];
            }

            return material;
        }

        public static int GetSmithRewardTier(BulkMaterialType material)
        {
            return GetSmithMetalIndex(material);
        }
    }

    public enum BulkGenericType
    {
        Iron,
        Cloth,
        Leather,
        Wood
        //RegularWood
    }

    public class BGTClassifier
    {
        public static BulkGenericType Classify(BODType deedType, Type itemType)
        {
            if (deedType == BODType.Tailor)
            {
                if (itemType == null || itemType.IsSubclassOf(typeof(BaseArmor)) || itemType.IsSubclassOf(typeof(BaseShoes)))
                    return BulkGenericType.Leather;

                return BulkGenericType.Cloth;
            }
            else if (deedType == BODType.Tinkering && itemType != null)
            {
                if(itemType == typeof(Clock) || itemType.IsSubclassOf(typeof(Clock)))
                    return BulkGenericType.Wood;

                CraftItem item = DefTinkering.CraftSystem.CraftItems.SearchFor(itemType);

                if (item != null)
                {
                    Type typeRes = item.Resources.GetAt(0).ItemType;

                    if (typeRes == typeof(Board) || typeRes == typeof(Log))
                        return BulkGenericType.Wood;
                }
            }
            else if (deedType == BODType.Fletching || deedType == BODType.Carpentry)
            {
                return BulkGenericType.Wood;
                //return BulkGenericType.RegularWood;
            }

            return BulkGenericType.Iron;
        }
    }
}
