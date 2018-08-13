using System.Collections.Generic;
using System.Linq;
using Server;
using Server.Commands;
using Server.Engines.PartySystem;
using Server.Factions;
using Server.Mobiles;
using Tresni;

namespace Tresdni
{
    public class Matchmaking
    {
        // List of players waiting on a party
        public static readonly List<Matchmaking> WaitingForParty = new List<Matchmaking>();

        public readonly bool AllowReds;
        public readonly Mobile Player;

        // 0 = PvM, 1 = PvP
        public readonly int Type;

        public static void Initialize()
        {
            EventSink.Logout += new LogoutEventHandler(EventSink_Logout);
            CommandSystem.Register("Matchmaking", AccessLevel.Player, new CommandEventHandler(Command_Handler));
        }

        private static void Command_Handler(CommandEventArgs e)
        {
            PlayerMobile pm = e.Mobile as PlayerMobile;

            if (pm == null)
                return;

            if (pm.HasGump(typeof (MatchmakingGump)))
                pm.CloseGump(typeof (MatchmakingGump));

            if (pm.HasGump(typeof(MatchmakingCreateGump)))
                pm.CloseGump(typeof(MatchmakingCreateGump));

            pm.SendGump(new MatchmakingGump(pm, 0));
        }

        private static void EventSink_Logout(LogoutEventArgs e)
        {
            Mobile from = e.Mobile;

            List<Matchmaking> toremove = WaitingForParty.Where(match => match.Player == @from).ToList();

            if (toremove.Count <= 0)
                return;

            foreach (Matchmaking drop in toremove.Where(drop => WaitingForParty.Contains(drop)))
            {
                WaitingForParty.Remove(drop);
            }
        }

        public Matchmaking(Mobile player, bool allowreds, int type)
        {
            Player = player;
            AllowReds = allowreds;
            Type = type;
        }

        public static void AddToParty(Matchmaking match, Mobile add)
        {
            if ( match == null || add == null)          
                return;

            Party addparty = Party.Get(add);

            if (addparty != null)
            {
                add.SendMessage("You are already in a party.");
                return;
            }

            if (match.Player == add)
            {
                add.SendMessage("You cannot join your own Matchmaking party.");
                return;
            }

            if (IsInMatchmaking(add))
            {
                add.SendMessage("You must remove your own party from Matchmaking if you wish to join another party.");
                return;
            }

            if (add.Criminal)
            {
                add.SendMessage("Criminals may not join a matchmaking party.");
                return;
            }
            
            if (!match.AllowReds && add.Kills >= 5)
            {
                add.SendMessage("This party does not allow murderers to join.");
                return;
            }

            Party p = Party.Get(match.Player);

            if (p == null)
                match.Player.Party = p = new Party(match.Player);

            if (p.Candidates.Contains(add))
            {
                add.SendMessage("You have already been invited to join this party outside of Matchmaking, and are pending acceptance.");
                return;
            }

            if ((p.Members.Count + p.Candidates.Count) >= Party.Capacity)
            {
                add.SendMessage("This party is currently full.");
                CancelWaitForParty(match.Player, true);
                return;
            }

            Faction ourFaction = Faction.Find(p.Leader);
            Faction theirFaction = Faction.Find(add);

            if (ourFaction != null && theirFaction != null && ourFaction != theirFaction)
            {
                add.SendMessage("You cannot join a party who's leader is in a different faction than your own.");
                return;
            }

            p.Add(add);

            foreach (PartyMemberInfo member in p.Members.Where(member => member.Mobile != null))
            {
                member.Mobile.SendMessage("{0} has been added to the party via Matchmaking.", add.Name);
            }      
        }

        private static bool IsInMatchmaking(Mobile from)
        {
            return WaitingForParty.Any(match => match.Player == @from);
        }

        public static void WaitForParty(Mobile from, bool allowreds, int type)
        {
            if (from == null)
                return;

            if (IsInMatchmaking(from))
            {
                from.SendMessage("Your party is already in Matchmaking.");
                return;
            }

            bool hasparty = false;
            Party p = Party.Get(from);

            if (p != null)
            {
                hasparty = true;

                if (p.Leader != from)
                {
                    from.SendMessage("Only the party leader can add your party to Matchmaking.");
                    return;
                }

                if ((p.Members.Count + p.Candidates.Count) >= Party.Capacity)
                {
                    from.SendMessage("Your party is already full.");
                    return;
                }
            }

            WaitingForParty.Add(new Matchmaking(from, allowreds, type));

            if (hasparty)
            {
                from.SendMessage("You have entered your party into Matchmaking.");
                World.Broadcast(173, true, "{0} has entered their party into Matchmaking.", from.Name);
            }
            else
            {
                from.SendMessage("You have opened a Matchmaking party.");
                World.Broadcast(173, true, "{0} has opened a Matchmaking party.", from.Name);
            }         
        }

        public static void CancelWaitForParty(Mobile from, bool partyfull = false)
        {
            if (from == null)
                return;

            List<Matchmaking> waiting = WaitingForParty.Where(match => match.Player == @from).ToList();

            foreach (Matchmaking toremove in waiting)
            {
                WaitingForParty.Remove(toremove);
            }

            @from.SendMessage(partyfull
                ? "Your party has been removed from Matchmaking, because it is now full."
                : "Your party has been removed from Matchmaking.");
        }
    }
}