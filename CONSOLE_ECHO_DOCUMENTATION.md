# ConsoleEcho Command

## Overview
The `ConsoleEcho` command allows server administrators to enable or disable the echoing of command outputs to the server console.

## Purpose
When enabled, all command outputs that would normally appear only in a player's journal will also be displayed in the server console window. This is particularly useful for:

- Debugging commands
- Monitoring command usage and results
- Keeping a server-side log of command outputs
- Observing bulk operations (like Area commands)

## Usage

### Syntax
```
[ConsoleEcho]
```

### Access Level
Administrator

### How It Works
1. Type `[ConsoleEcho]` in-game to toggle the feature
2. When enabled, you'll see: "Command outputs will now be echoed to the console."
3. When disabled, you'll see: "Command outputs will no longer be echoed to the console."

### Example

**Before enabling ConsoleEcho:**
1. Admin uses command: `[Area get name, hue where powercrystal]`
2. Selects a bounding box
3. Results appear only in the player's journal

**After enabling ConsoleEcho:**
1. Admin uses command: `[Area get name, hue where powercrystal]`
2. Selects a bounding box
3. Results appear in:
   - Player's journal (as normal)
   - Server console window (new behavior)

### Console Output Format
- **Command outputs**: `[Command Output] PlayerName: message`
- **Command failures**: `[Command Failure] PlayerName: message`

## Examples

### Area Get Command
When using `[Area get name, hue where powercrystal`, the console might show:
```
[Command Output] AdminName: Name = "Power Crystal", Hue = 1150
[Command Output] AdminName: Name = "Power Crystal", Hue = 1150
[Command Output] AdminName: Name = "Power Crystal", Hue = 1150
```

### Count Command
When using `[Area count where item`, the console might show:
```
[Command Output] AdminName: There are 47 matching objects.
```

## Technical Details

### Implementation
- The feature is controlled by a static flag in the `BaseCommand` class
- The `Flush()` method in `BaseCommand` checks this flag and writes to console when enabled
- The state persists until explicitly toggled or the server restarts

### Performance Considerations
- Minimal performance impact as console writing only occurs during command execution
- No impact when the feature is disabled
- The BaseCommand system collects up to 10 unique response messages per command, and all collected messages are echoed to console when enabled

## Files Modified
- `Scripts/Commands/Generic/Commands/BaseCommand.cs` - Added ConsoleEcho property and modified Flush method
- `Scripts/Commands/ConsoleEcho.cs` - New command implementation
