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

// allow same-account overlap
try
{
    if (existing.Owner != null && existing.Owner.Account != null && placer.Account != null && existing.Owner.Account == placer.Account)
        return true;
}
catch { }


            // If your BaseHouse has additional helpers like IsCoOwner, HasAccess, IsFriend, etc.,
            // and you want to allow those, uncomment/adapt the following lines to match your API:
            //
            // try { if (existing.IsCoOwner(placer)) return true; } catch { }
            // try { if (existing.HasAccess(placer)) return true; } catch { }
            // try { if (existing.IsFriend(placer)) return true; } catch { }

            return false;
        }
        catch (Exception)
        {
            // conservative default: do not allow overlap on error
            return false;
        }
    }
}