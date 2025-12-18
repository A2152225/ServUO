using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Commands;
using Server.Network;

namespace Server.Custom
{
    // Stackable, blessed Dan Dollar item (scroll-style)
    public class DanDollar : Item
    {
        // Expose hue as a constant so message code can reference it without instantiating an item.
        public const int CurrencyHue = 113;

        [Constructable]
        public DanDollar() : base(0x0E34) // scroll/paper-style sprite
        {
            Stackable = true;
            Amount = 1;
            Movable = true;
            Name = "Dan Dollar";
            Hue = CurrencyHue;
            Weight = 0.0;
            LootType = LootType.Blessed; // won't be lost on death
        }

        [Constructable]
        public DanDollar(int amount) : base(0x0E34)
        {
            Stackable = true;
            Amount = Math.Max(1, amount);
            Movable = true;
            Name = "Dan Dollar";
            Hue = CurrencyHue;
            Weight = 0.0;
            LootType = LootType.Blessed;
        }

        public DanDollar(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    // Stackable, blessed Breaker Buck item (scroll-style)
    public class BreakerBuck : Item
    {
        public const int CurrencyHue = 1157;

        [Constructable]
        public BreakerBuck() : base(0x0E34)
        {
            Stackable = true;
            Amount = 1;
            Movable = true;
            Name = "Breaker Buck";
            Hue = CurrencyHue;
            Weight = 0.0;
            LootType = LootType.Blessed;
        }

        [Constructable]
        public BreakerBuck(int amount) : base(0x0E34)
        {
            Stackable = true;
            Amount = Math.Max(1, amount);
            Movable = true;
            Name = "Breaker Buck";
            Hue = CurrencyHue;
            Weight = 0.0;
            LootType = LootType.Blessed;
        }

        public BreakerBuck(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    // Manager using integer ticks for precise small increments and robust persistence.
    public static class DanDollarManager
    {
        // Data file placed at server root to avoid Saves folder rotation issues.
        private static readonly string DataPath = Path.Combine(Core.BaseDirectory, "DanDollarBalancesTicks.txt");
        private static readonly object _lock = new object();

        // balances stored in ticks (long). 1.00 == TicksPerDollar ticks.
        private static Dictionary<string, long> _balancesTicks = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase);

        // Configurable parameters
        public const long TicksPerDollar = 100_000L; // resolution: 1 tick == 0.00001
        private static readonly decimal HourlyIncrementDecimal = 0.01m; // 0.01 per hour
        private static readonly long HourlyIncrementTicks = DecimalToTicks(HourlyIncrementDecimal);

        // Autosave interval and flags
        private static readonly TimeSpan AutosaveInterval = TimeSpan.FromMinutes(1.0); // save every minute
        private static bool _dirty = false;

        // Debug / safety options
        // When true, Save() is called on every AddTicks (useful for debugging, not recommended for production).
        private static readonly bool ForceImmediateSaveOnAdd = false;

        // Init guard to avoid duplicate timers if Initialize is called more than once.
        private static bool _initialized = false;

        // Initialize and hook timers/commands
        public static void Initialize()
        {
            // Guard: only initialize once
            if (_initialized)
            {
                Console.WriteLine("DanDollarManager: Initialize called but already initialized; skipping duplicate initialization.");
                return;
            }
            _initialized = true;

            // Log DataPath for debugging
            Console.WriteLine("DanDollarManager: DataPath = " + DataPath);

            // Ensure top-level directory exists and can be written
            try
            {
                var rootDir = Core.BaseDirectory;
                if (!Directory.Exists(rootDir))
                    Directory.CreateDirectory(rootDir);
            }
            catch (Exception ex)
            {
                Console.WriteLine("DanDollarManager: Warning - could not ensure server root directory: " + ex.Message);
            }

            Load();

            // Early-bird: reward ~1 minute after restart (custom message)
            Timer.DelayCall(TimeSpan.FromMinutes(1.0), new TimerCallback(EarlyBirdTick));

            // Schedule the first hourly award aligned to the next UTC top-of-hour using a self-rescheduling timer.
            ScheduleNextHourlyTick();

            // Start autosave timer
            Timer.DelayCall(AutosaveInterval, AutosaveInterval, new TimerCallback(AutoSaveTick));

            // Register process exit handlers to ensure Save on shutdown/restart
            try
            {
                AppDomain.CurrentDomain.ProcessExit += (s, e) =>
                {
                    try
                    {
                        Console.WriteLine("DanDollarManager: ProcessExit triggered, saving balances...");
                        Save();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("DanDollarManager: ProcessExit save error: " + ex.Message);
                    }
                };

                AppDomain.CurrentDomain.DomainUnload += (s, e) =>
                {
                    try
                    {
                        Console.WriteLine("DanDollarManager: DomainUnload triggered, saving balances...");
                        Save();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("DanDollarManager: DomainUnload save error: " + ex.Message);
                    }
                };
            }
            catch
            {
                // ignore if environment restricts domain hooks
            }

            // Commands
            CommandSystem.Register("ddbalance", AccessLevel.Player, new CommandEventHandler(CmdCheckBalance));
            CommandSystem.Register("ddadd", AccessLevel.Administrator, new CommandEventHandler(CmdAdminAdd));
            CommandSystem.Register("ddsave", AccessLevel.Administrator, new CommandEventHandler(CmdAdminSave));
            CommandSystem.Register("ddinfo", AccessLevel.Administrator, new CommandEventHandler(CmdAdminInfo));
        }

        // Compute delay until next UTC top-of-hour and schedule a single-shot timer that will
        // execute the hourly award and then reschedule itself for the next top-of-hour.
        private static void ScheduleNextHourlyTick()
        {
            try
            {
                DateTime nowUtc = DateTime.UtcNow;
                DateTime nextHourUtc = new DateTime(nowUtc.Year, nowUtc.Month, nowUtc.Day, nowUtc.Hour, 0, 0, DateTimeKind.Utc).AddHours(1);
                TimeSpan initialDelayToTopOfHour = nextHourUtc - nowUtc;
                if (initialDelayToTopOfHour < TimeSpan.Zero)
                    initialDelayToTopOfHour = TimeSpan.Zero;

                Timer.DelayCall(initialDelayToTopOfHour, new TimerCallback(HourlyTickAndReschedule));
                Console.WriteLine("DanDollarManager: Scheduled hourly tick in " + initialDelayToTopOfHour.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("DanDollarManager: ScheduleNextHourlyTick error: " + ex.Message);
            }
        }

        // Timer callback used for hourly award; executes the award and schedules the next occurrence
        private static void HourlyTickAndReschedule()
        {
            try
            {
                HourlyTick();           // do the award
            }
            catch (Exception ex)
            {
                Console.WriteLine("DanDollarManager: HourlyTick error: " + ex.Message);
            }

            // Always schedule the next top-of-hour from current UTC time (prevents drift)
            ScheduleNextHourlyTick();
        }

        // Early-bird: award hourly increment with custom message and suppress default notify
        private static void EarlyBirdTick()
        {
            decimal amtDecimal = TicksToDecimal(HourlyIncrementTicks);

            foreach (Mobile m in World.Mobiles.Values)
            {
                if (m == null) continue;
                if (!(m is PlayerMobile)) continue;
                if (m.NetState == null) continue; // only online players

                // Add ticks but suppress the standard notification so we can send a custom early-bird message
                AddTicks(m, HourlyIncrementTicks, "Early-bird reward", notify: false);

                try
                {
                    // Use Dan Dollar hue for friendly early-bird message color
                    m.SendMessage(DanDollar.CurrencyHue, $"Welcome back! You received an early‑bird reward of {amtDecimal:0.00000} Dan Dollar(s) for returning quickly.");
                }
                catch
                {
                    // swallow messaging errors
                }
            }
        }

        // Hourly tick: standard notification
        private static void HourlyTick()
        {
            foreach (Mobile m in World.Mobiles.Values)
            {
                if (m == null) continue;
                if (!(m is PlayerMobile)) continue;
                if (m.NetState == null) continue; // only online players

                AddTicks(m, HourlyIncrementTicks, "Hourly reward");
            }
        }

        // Autosave timer callback
        private static void AutoSaveTick()
        {
            SaveIfDirty();
        }

        // Public API: add a decimal amount (converts to ticks)
        public static void AddBalanceDecimal(Mobile m, decimal amountDecimal, string reason = "")
        {
            if (m == null || amountDecimal == 0m) return;

            long ticks = DecimalToTicks(amountDecimal);
            if (ticks == 0L)
            {
                // rounding left zero ticks; nothing to add
                return;
            }

            AddTicks(m, ticks, reason);
        }

        // Add ticks directly; notify controls whether the standard message is shown.
        public static void AddTicks(Mobile m, long ticks, string reason = "", bool notify = true)
        {
            if (m == null || ticks == 0L) return;

            string key = GetKeyForMobile(m);

            lock (_lock)
            {
                if (!_balancesTicks.TryGetValue(key, out long cur))
                    cur = 0L;

                long next = cur + ticks;
                if (next < 0L) // clamp
                    next = 0L;

                _balancesTicks[key] = next;
                _dirty = true;
            }

            // Perform redemption under its own lock so we atomically remove whole units
            TryRedeemTicks(m, key);

            // Optionally force save immediately (disabled by default)
            if (ForceImmediateSaveOnAdd)
            {
                try { Save(); } catch { }
            }

            // Standard notification (unless suppressed)
            if (notify)
            {
                try
                {
                    decimal display = GetBalanceDecimal(m);
                    m.SendMessage($"Dan Dollar: {(ticks >= 0 ? "Gained" : "Lost")} {TicksToDecimal(Math.Abs(ticks)):0.00000}. Balance: {display:0.00000}. {reason}");
                }
                catch
                {
                    // ignore messaging failures
                }
            }
        }

        // Get decimal balance for player
        public static decimal GetBalanceDecimal(Mobile m)
        {
            if (m == null) return 0m;
            string key = GetKeyForMobile(m);
            lock (_lock)
            {
                if (_balancesTicks.TryGetValue(key, out long v))
                    return TicksToDecimal(v);
                return 0m;
            }
        }

        // Get raw ticks
        public static long GetBalanceTicks(Mobile m)
        {
            if (m == null) return 0L;
            string key = GetKeyForMobile(m);
            lock (_lock)
            {
                if (_balancesTicks.TryGetValue(key, out long v))
                    return v;
                return 0L;
            }
        }

        // Redemption: converts whole units into items (50/50 split Dan Dollar / Breaker Buck)
        // Subtracts redeemed units in ticks to avoid duplicate redemptions.
        private static void TryRedeemTicks(Mobile m, string key)
        {
            List<Item> itemsToGive = new List<Item>();
            long totalGiven = 0;
            long danCount = 0;
            long buckCount = 0;

            lock (_lock)
            {
                if (!_balancesTicks.TryGetValue(key, out long balanceTicks))
                    return;

                if (balanceTicks >= TicksPerDollar)
                {
                    long whole = balanceTicks / TicksPerDollar;
                    // subtract redeemed whole units in ticks
                    balanceTicks -= whole * TicksPerDollar;
                    _balancesTicks[key] = balanceTicks;
                    _dirty = true;

                    // Decide per whole unit (50/50) whether it's DanDollar or BreakerBuck
                    for (long i = 0; i < whole; i++)
                    {
                        if (Utility.RandomDouble() < 0.5)
                            danCount++;
                        else
                            buckCount++;
                    }

                    // Create stacks for DanDollar
                    long remainingDan = danCount;
                    while (remainingDan > 0)
                    {
                        int stackAmount = (remainingDan > int.MaxValue) ? int.MaxValue : (int)remainingDan;
                        itemsToGive.Add(new DanDollar(stackAmount));
                        totalGiven += stackAmount;
                        remainingDan -= stackAmount;
                    }

                    // Create stacks for BreakerBuck
                    long remainingBuck = buckCount;
                    while (remainingBuck > 0)
                    {
                        int stackAmount = (remainingBuck > int.MaxValue) ? int.MaxValue : (int)remainingBuck;
                        itemsToGive.Add(new BreakerBuck(stackAmount));
                        totalGiven += stackAmount;
                        remainingBuck -= stackAmount;
                    }
                }
            }

            if (itemsToGive.Count == 0)
                return;

            // Deliver items to player (backpack preferred)
            Container pack = m.Backpack;
            if (pack == null)
            {
                foreach (var it in itemsToGive)
                    it.MoveToWorld(m.Location, m.Map);

                // Send separate colored messages for each currency type, indicating drop-at-feet.
                try
                {
                    int danSum = itemsToGive.OfType<DanDollar>().Sum(i => i.Amount);
                    int buckSum = itemsToGive.OfType<BreakerBuck>().Sum(i => i.Amount);

                    if (danSum > 0)
                        m.SendMessage(DanDollar.CurrencyHue, $"Dan Dollar: You received {danSum} Dan Dollar(s); dropped at your feet because you have no backpack.");
                    if (buckSum > 0)
                        m.SendMessage(BreakerBuck.CurrencyHue, $"Breaker Buck: You received {buckSum} Breaker Buck(s); dropped at your feet because you have no backpack.");
                }
                catch { }
            }
            else
            {
                foreach (var it in itemsToGive)
                    pack.DropItem(it);

                // Send separate colored messages per currency using the currency's hue constant
                try
                {
                    int danSum = itemsToGive.OfType<DanDollar>().Sum(i => i.Amount);
                    int buckSum = itemsToGive.OfType<BreakerBuck>().Sum(i => i.Amount);

                    if (danSum > 0)
                        m.SendMessage(DanDollar.CurrencyHue, $"Dan Dollar: You received {danSum} Dan Dollar(s).");
                    if (buckSum > 0)
                        m.SendMessage(BreakerBuck.CurrencyHue, $"Breaker Buck: You received {buckSum} Breaker Buck(s).");
                }
                catch { }
            }

            // Persist immediately after giving items to avoid duplication on crash
            SaveIfDirty();
        }

        #region Persistence (atomic write + backup)

        private static void Load()
        {
            lock (_lock)
            {
                _balancesTicks.Clear();
                try
                {
                    Console.WriteLine("DanDollarManager: Loading balances from " + DataPath);
                    if (!File.Exists(DataPath))
                        return;

                    foreach (var line in File.ReadAllLines(DataPath))
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;
                        var parts = line.Split('|');
                        if (parts.Length < 2) continue;
                        var key = parts[0];
                        if (long.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out long val))
                            _balancesTicks[key] = val;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DanDollarManager: Load error: " + ex.Message);
                }

                _dirty = false;
            }
        }

        private static void Save()
        {
            lock (_lock)
            {
                try
                {
                    var dir = Path.GetDirectoryName(DataPath);
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    // Backup outside the Saves folder in case Saves is rotated externally
                    var backupRoot = Path.Combine(Core.BaseDirectory, "Backups", "DanDollar");
                    try
                    {
                        if (!Directory.Exists(backupRoot))
                            Directory.CreateDirectory(backupRoot);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("DanDollarManager: Could not create backup directory: " + ex.Message);
                    }

                    // If existing file exists, copy to backup with timestamp
                    try
                    {
                        if (File.Exists(DataPath))
                        {
                            string backupPath = Path.Combine(backupRoot, $"DanDollarBalancesTicks.{DateTime.UtcNow:yyyyMMdd_HHmmss}.bak");
                            File.Copy(DataPath, backupPath, true);
                            Console.WriteLine("DanDollarManager: Backed up existing save to " + backupPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("DanDollarManager: Backup copy failed: " + ex.Message);
                    }

                    // Write atomically to temp then replace
                    var tempPath = DataPath + ".tmp";
                    using (var sw = new StreamWriter(tempPath, false))
                    {
                        foreach (var kv in _balancesTicks)
                        {
                            sw.WriteLine($"{kv.Key}|{kv.Value.ToString(CultureInfo.InvariantCulture)}");
                        }
                    }

                    if (File.Exists(DataPath))
                    {
                        try
                        {
                            File.Replace(tempPath, DataPath, null);
                        }
                        catch
                        {
                            // fallback if Replace unsupported
                            try { File.Delete(DataPath); } catch { }
                            File.Move(tempPath, DataPath);
                        }
                    }
                    else
                    {
                        File.Move(tempPath, DataPath);
                    }

                    _dirty = false;
                    Console.WriteLine("DanDollarManager: Save completed to " + DataPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DanDollarManager: Save error: " + ex.Message);
                }
            }
        }

        private static void SaveIfDirty()
        {
            lock (_lock)
            {
                if (!_dirty) return;
            }
            Save();
        }

        #endregion

        #region Helpers

        private static long DecimalToTicks(decimal amount)
        {
            decimal ticksDecimal = amount * TicksPerDollar;
            ticksDecimal = decimal.Round(ticksDecimal, 0, MidpointRounding.AwayFromZero);
            try
            {
                return Convert.ToInt64(ticksDecimal);
            }
            catch
            {
                if (ticksDecimal <= long.MinValue) return long.MinValue;
                if (ticksDecimal >= long.MaxValue) return long.MaxValue;
                return 0L;
            }
        }

        private static decimal TicksToDecimal(long ticks)
        {
            return (decimal)ticks / (decimal)TicksPerDollar;
        }

        // Prefer account username when available, fallback to character serial
        private static string GetKeyForMobile(Mobile m)
        {
            try
            {
                if (m.Account != null)
                {
                    var acct = m.Account;
                    var acctType = acct.GetType();
                    var unameProp = acctType.GetProperty("Username") ?? acctType.GetProperty("Name");
                    if (unameProp != null)
                    {
                        var nameObj = unameProp.GetValue(acct, null);
                        if (nameObj != null)
                            return nameObj.ToString();
                    }
                }
            }
            catch
            {
                // fallback
            }

            return $"CHAR-{m.Serial}";
        }

        #endregion

        #region Commands

        private static void CmdCheckBalance(CommandEventArgs e)
        {
            Mobile from = e.Mobile;
            decimal bal = GetBalanceDecimal(from);
            from.SendMessage($"Dan Dollar balance: {bal:0.00000}");
        }

        private static void CmdAdminAdd(CommandEventArgs e)
        {
            Mobile from = e.Mobile;
            if (e.Length < 2)
            {
                from.SendMessage("Usage: [ddadd <playername> <decimalAmount>  (e.g. 0.05)");
                return;
            }

            string targetName = e.GetString(0);
            if (!decimal.TryParse(e.GetString(1), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal amount))
            {
                from.SendMessage("Invalid amount.");
                return;
            }

            Mobile target = World.Mobiles.Values.FirstOrDefault(m => m is PlayerMobile && m.Name != null && m.Name.Equals(targetName, StringComparison.OrdinalIgnoreCase)) as Mobile;
            if (target == null)
            {
                from.SendMessage("Player not found online. Use server-side scripts to add to offline accounts if needed.");
                return;
            }

            AddBalanceDecimal(target, amount, "Admin add");
            from.SendMessage($"Added {amount:0.00000} to {target.Name}");
        }

        private static void CmdAdminSave(CommandEventArgs e)
        {
            Mobile from = e.Mobile;
            SaveIfDirty();
            from.SendMessage("Dan Dollar balances saved.");
        }

        // Debug/info command: prints datapath, dirty flag, and current player's raw ticks
        private static void CmdAdminInfo(CommandEventArgs e)
        {
            Mobile from = e.Mobile;
            try
            {
                long raw = GetBalanceTicks(from);
                from.SendMessage("DanDollar DataPath: " + DataPath);
                from.SendMessage("DanDollar raw ticks: " + raw);
                from.SendMessage("DanDollar decimal: " + GetBalanceDecimal(from).ToString("0.00000"));
                lock (_lock)
                {
                    from.SendMessage("DanDollar dirty flag: " + _dirty);
                }
            }
            catch (Exception ex)
            {
                from.SendMessage("DanDollar info error: " + ex.Message);
            }
        }

        #endregion
    }

    // Script loader bootstrap
    public class DanDollarBootstrap
    {
        public static void Initialize()
        {
            DanDollarManager.Initialize();
        }
    }
}