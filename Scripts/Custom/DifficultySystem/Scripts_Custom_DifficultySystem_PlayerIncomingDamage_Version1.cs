using System;
using Server;
using Server.Mobiles;

namespace Server.Mobiles
{
    public partial class PlayerMobile
    {
        // Optional: keep a trace for incoming hits; no scaling here.
     /*   public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            base.OnDamage(amount, from, willKill);

            // Uncomment if you want to trace live incoming values
             if (from is BaseCreature bc && !bc.Controlled && !bc.Summoned && BaseCreature.DifficultyDebug)
             {
                Console.WriteLine($"[Trace:OnDamage] {bc.Name} -> {Name}: incoming={amount}, DR={this.PhysicalResistance}");
             }
        }
     */
    }
}
