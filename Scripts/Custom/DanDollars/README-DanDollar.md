```markdown
# DanDollar system for A2152225/ServUO

What this adds
- Credits online players 0.01 DanDollar (fractional) every hour.
- Tracks fractional balance per account/character (uses account username when available, otherwise character serial).
- When balance reaches 1.00, converts each whole 1.00 into a DanDollar item and places it into the player's backpack (or drops at feet if no backpack).
- Public API: Server.Custom.DanDollarManager.AddBalance(mobile, amountDecimal, "reason")

Files to install
- Scripts/Custom/DanDollarSystem.cs  — main implementation (DanDollar item, manager, persistence, commands)
- Scripts/Custom/README-DanDollar.md — this file

Installation
1. Copy both files into your repository under Scripts/Custom/.
2. Build/restart your server. The bootstrap class `DanDollarBootstrap.Initialize` will be called by the script loader on startup (standard ServUO script bootstrapping). If your fork requires explicit registration, call `DanDollarManager.Initialize()` during server startup.
3. Ensure the server process can write to the Saves directory; balances are stored in Saves/DanDollarBalances.txt.

Integration points (where to award extra progress)
- Crafting: after a successful craft completion, call:
  - Server.Custom.DanDollarManager.AddBalance(from, 0.02m, "Crafting");
- Harvesting: after successful harvest, call:
  - Server.Custom.DanDollarManager.AddBalance(from, 0.01m, "Harvest");
- Kill: in your monster death/killer reward logic:
  - Server.Custom.DanDollarManager.AddBalance(killerMobile, 0.05m, "Kill");

Notes & customization
- Change HourlyIncrement and RedeemThreshold constants in the C# file to tweak rate and threshold.
- Persistence format is a simple text file (key|decimal). You can swap to JSON or DB easily if desired.
- Account username lookup is tolerant but may need small adjustments depending on your fork's account class (property named "Username" or "Name"). It falls back to character serial.
- The DanDollar item graphic and name are configurable in the DanDollar class.

Commands
- Player: [ddbalance  — shows current fractional balance (e.g., 0.37)
- Admin: [ddadd <playername> <amount> — add amount to online character (debug/admin)

If you want, I can:
- Open a PR on your repository with the files on a new branch.
- Adapt the file to target a specific branch or hook these calls directly into your crafting/harvest/kill systems if you point me to the exact files where those events happen.
```