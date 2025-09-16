using System;
using Server;
using Server.Mobiles;
using Server.Multis; // if BaseHouse is in this namespace in your project adjust as needed

public static class HousePlacementHelper
{
    // Returns true when 'placer' should be allowed to overlap house 'existing'
    public static bool CanOverlapHouse(BaseHouse existing, Mobile placer)
    {
        if (existing == null || placer == null)
            return false;

        try
        {
            // Owner always allowed
            if (existing.IsOwner(placer))
                return true;

            // Optionally allow co-owners / friends / account-shared owners:
            // Uncomment/adapt to match your BaseHouse implementation:
            // if (existing.IsCoOwner(placer)) return true;
            // if (existing.IsFriend(placer)) return true;
            // if (existing.HasAccess(placer)) return true;
            // if (existing.IsOnSameAccount(placer)) return true;

            return false;
        }
        catch (Exception)
        {
            // conservative default: do not allow overlap on error
            return false;
        }
    }
}