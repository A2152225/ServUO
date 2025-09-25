using Server;

namespace Server.Mobiles
{
    public partial class PlayerMobile
    {
        // If true, show 0-damage popups; if false, hide 0 values
        [CommandProperty(AccessLevel.Player)]
        public bool ShowZeroDamagePopups { get; set; } = true;
    }
}           
