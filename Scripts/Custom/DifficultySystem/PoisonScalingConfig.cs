using System;
using Server;
using Server.Mobiles;

namespace Server.Custom.Difficulty
{
    // Central configuration & formula hooks for poison scaling
    public static class PoisonScalingConfig
    {
        // Master enable
        public static bool Enabled = true;

        // How strongly high difficulty (health multiplier) suppresses poison
        // Effective displayed scalar = max(MinScalar, 1 / (HealthMultiplier ^ HighDiffScalarExponent))
        public static double HighDiffScalarExponent = 0.50;

        // Never reduce outward (displayed) poison below this fraction of the raw tick
        public static double MinScalar = 0.35;

        // Cap internal (post-difficulty) HP removal per tick (percent of creature base HitsMax)
        public static double InternalTickCapPercent = 0.10;

        // Player investment bonuses -------------
        public static bool EnableSkillBonus = true;
        // At 100.0 Poisoning skill => +SkillBonusMaxMultiplierMultiplier (linearly scaled)
        public static double SkillBonusMaxMultiplier = 0.50; // +50% at 100.0

        // Optional hybrid synergy (averages the strongest supporting skill)
        public static bool EnableHybridSynergy = true;
        // Portion of (best supporting skill / 100) added (e.g. 0.25 => up to +25%)
        public static double HybridSynergyWeight = 0.25;
        // Supporting skills considered
        public static SkillName[] HybridSupportSkills =
        {
            SkillName.Alchemy,
            SkillName.Anatomy,
            SkillName.EvalInt
        };

        // Optional catalyst item bonus (you can toggle where applied)
        public static bool EnableCatalystBonus = true;
        public static double CatalystBonus = 0.20; // +20% when active

        // Optional diminishing returns when a player has multiple active poisons on same target
        public static bool EnableMultiStackDiminish = true;
        public static double MultiStackFirst = 1.00;  // 1st stack 100%
        public static double MultiStackSecond = 0.70; // 2nd stack 70%
        public static double MultiStackThird = 0.50;  // 3rd+ 50%

        // Hook: detect catalyst (override to integrate with your item / buff system)
        public static bool HasCatalyst(PlayerMobile pm)
        {
            // Example placeholder: check a backpack item hue / name / custom flag
            return false;
        }

        // Hook: count active poisons this player owns on target (needs integration with your poison tracking)
        // Return 1 if you don't track multiple ownership to avoid suppression.
        public static int GetActiveOwnedPoisonStacks(PlayerMobile pm, BaseCreature target)
        {
            return 1;
        }

        public static double ComputePlayerBonusScalar(PlayerMobile pm, BaseCreature target)
        {
            if (pm == null)
                return 1.0;

            double scalar = 1.0;

            if (EnableSkillBonus)
            {
                double pSkill = pm.Skills[SkillName.Poisoning].Value;
                if (pSkill > 0)
                    scalar += (pSkill / 100.0) * SkillBonusMaxMultiplier; // linear
            }

            if (EnableHybridSynergy)
            {
                double best = 0.0;
                foreach (var s in HybridSupportSkills)
                {
                    double v = pm.Skills[s].Value;
                    if (v > best) best = v;
                }
                if (best > 0)
                    scalar += (best / 100.0) * HybridSynergyWeight;
            }

            if (EnableCatalystBonus && HasCatalyst(pm))
                scalar += CatalystBonus;

            if (EnableMultiStackDiminish)
            {
                int stacks = GetActiveOwnedPoisonStacks(pm, target);
                if (stacks > 1)
                {
                    if (stacks == 2) scalar *= MultiStackSecond;
                    else scalar *= MultiStackThird;
                }
            }

            return Math.Max(0.05, scalar); // floor
        }
    }
}
