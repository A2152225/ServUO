using System;
using System.Reflection;
using Server;
using Server.Mobiles;
using Server.Gumps;
using Server.Network;
using Server.Commands;

namespace Server.Custom.DifficultySystem
{
    public class AnimalLoreScaling
    {
        public static void Initialize()
        {
            // Register a command to test the scaling
            CommandSystem.Register("ScaleLore", AccessLevel.Player, new CommandEventHandler(OnCommand));
            
            // We'll use a timer-based approach to hook Animal Lore
            EventSink.WorldLoad += OnWorldLoad;
        }
        
        private static void OnWorldLoad()
        {
            // Start a timer to periodically check for Animal Lore gumps
            Timer.DelayCall(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1), CheckAnimalLoreGumps);
        }
        
        private static void CheckAnimalLoreGumps()
        {
            // Check all online players
            foreach (Mobile m in World.Mobiles.Values)
            {
                if (m is PlayerMobile)
                {
                    PlayerMobile pm = (PlayerMobile)m;
                    
                    // If they're using Animal Lore and have a higher difficulty
                    if (pm.LastSkillUsed == SkillName.AnimalLore && 
                        pm.Target != null && 
                        pm.Target.TargetObject is BaseCreature)
                    {
                        BaseCreature bc = (BaseCreature)pm.Target.TargetObject;
                        int difficultyLevel = DifficultySystem.GetPlayerDifficulty(pm);
                        
                        if (difficultyLevel > 1)
                        {
                            // Replace with scaled gump
                            double healthMultiplier = Math.Pow(2, difficultyLevel - 1);
                            pm.SendGump(new ScaledAnimalLoreGump(bc, healthMultiplier));
                        }
                    }
                }
            }
        }
        
        [Usage("ScaleLore")]
        [Description("Tests the animal lore scaling on target.")]
        public static void OnCommand(CommandEventArgs e)
        {
            e.Mobile.Target = new AnimalLoreScalingTarget(e.Mobile);
            e.Mobile.SendMessage("Target a creature to view scaled stats.");
        }
        
        private class AnimalLoreScalingTarget : Server.Targeting.Target
        {
            public AnimalLoreScalingTarget(Mobile from) : base(-1, false, Server.Targeting.TargetFlags.None)
            {
            }
            
            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is BaseCreature)
                {
                    BaseCreature bc = (BaseCreature)targeted;
                    int difficultyLevel = DifficultySystem.GetPlayerDifficulty(from);
                    double healthMultiplier = Math.Pow(2, difficultyLevel - 1);
                    
                    from.SendGump(new ScaledAnimalLoreGump(bc, healthMultiplier));
                }
                else
                    from.SendMessage("That is not a creature.");
            }
        }
    }
    
    // Custom AnimalLore gump that shows scaled health
    public class ScaledAnimalLoreGump : Gump
    {
        public ScaledAnimalLoreGump(BaseCreature bc, double healthMultiplier) : base(250, 50)
        {
            // Calculate scaled health values
            int scaledCurrentHits = (int)(bc.Hits * healthMultiplier);
            int scaledMaxHits = (int)(bc.HitsMax * healthMultiplier);
            
            // Create a customized Animal Lore gump with scaled health
            AddPage(0);
            AddBackground(0, 0, 250, 170, 3500);
            
            AddHtmlLocalized(45, 15, 200, 20, 1049593, 0x7FFF, false, false); // Creature Properties
            
            AddHtmlLocalized(45, 35, 100, 20, 1018097, 0x7FFF, false, false); // Hits
            AddHtml(150, 35, 100, 20, String.Format("{0} / {1}", scaledCurrentHits, scaledMaxHits), false, false);
            
            // Add the rest of the standard properties...
            AddHtmlLocalized(45, 55, 100, 20, 1049578, 0x7FFF, false, false); // Strength
            AddHtml(150, 55, 100, 20, bc.Str.ToString(), false, false);
            
            AddHtmlLocalized(45, 75, 100, 20, 1049579, 0x7FFF, false, false); // Dexterity
            AddHtml(150, 75, 100, 20, bc.Dex.ToString(), false, false);
            
            AddHtmlLocalized(45, 95, 100, 20, 1049580, 0x7FFF, false, false); // Intelligence
            AddHtml(150, 95, 100, 20, bc.Int.ToString(), false, false);
            
            // Special note about difficulty scaling
            AddHtml(45, 130, 200, 40, "<i>*Health values shown are scaled based on your difficulty setting.</i>", false, false);
        }
    }
}