# Testing ConsoleEcho Feature

## Test Scenario 1: Basic Toggle Functionality

### Steps:
1. Start the server
2. Login as an Administrator
3. Type `[ConsoleEcho]` in game
4. Observe console output: `[ConsoleEcho] AdminName enabled console echo for command outputs.`
5. Observe in-game message: "Command outputs will now be echoed to the console." (green)
6. Type `[ConsoleEcho]` again
7. Observe console output: `[ConsoleEcho] AdminName disabled console echo for command outputs.`
8. Observe in-game message: "Command outputs will no longer be echoed to the console." (gray)

### Expected Results:
- Command toggles successfully
- Console messages appear as expected
- In-game feedback is clear

## Test Scenario 2: Area Get Command (Problem Statement Example)

### Setup:
1. Place several PowerCrystal items in the world at known locations
2. Enable ConsoleEcho with `[ConsoleEcho]`

### Steps:
1. Type command: `[Area get name, hue where powercrystal]`
2. Target a bounding box containing the PowerCrystal items
3. Observe both journal and console output

### Expected Results in Journal:
```
Name = "Power Crystal", Hue = 1150
Name = "Power Crystal", Hue = 1150
Name = "Power Crystal", Hue = 1150
```

### Expected Results in Console:
```
[Command Output] AdminName: Name = "Power Crystal", Hue = 1150
[Command Output] AdminName: Name = "Power Crystal", Hue = 1150
[Command Output] AdminName: Name = "Power Crystal", Hue = 1150
```

## Test Scenario 3: Get Command on Single Object

### Setup:
1. Enable ConsoleEcho with `[ConsoleEcho]`

### Steps:
1. Type command: `[Get name, hue]`
2. Target any item

### Expected Results:
- Journal shows: `Name = "ItemName", Hue = XXX`
- Console shows: `[Command Output] AdminName: Name = "ItemName", Hue = XXX`

## Test Scenario 4: Command Failures

### Setup:
1. Enable ConsoleEcho with `[ConsoleEcho]`

### Steps:
1. Type command: `[Get invalidproperty]`
2. Target any item

### Expected Results:
- Journal shows: `Property not found.`
- Console shows: `[Command Failure] AdminName: Property not found.`

## Test Scenario 5: Feature Disabled

### Setup:
1. Ensure ConsoleEcho is disabled (use `[ConsoleEcho]` if needed)

### Steps:
1. Type command: `[Area get name where item]`
2. Target a bounding box with items

### Expected Results:
- Journal shows results normally
- Console shows NO command output messages
- Behavior is identical to before the feature was added

## Test Scenario 6: Multiple Commands

### Setup:
1. Enable ConsoleEcho with `[ConsoleEcho]`

### Steps:
1. Execute: `[Get name]` on an item
2. Execute: `[Set hue 1150]` on an item
3. Execute: `[Count where item]` on an area

### Expected Results:
- All command outputs appear in both journal and console
- Each console line is prefixed with player name
- Console output is clear and readable

## Test Scenario 7: Server Restart

### Steps:
1. Enable ConsoleEcho with `[ConsoleEcho]`
2. Execute a command and verify console output
3. Restart the server
4. Login and execute another command

### Expected Results:
- ConsoleEcho defaults to OFF after restart (property is not persisted)
- No console output until `[ConsoleEcho]` is used again
- This is expected behavior as the flag is not saved

## Performance Testing

### Setup:
1. Create a large area with many items

### Steps:
1. WITHOUT ConsoleEcho: Execute `[Area count where item]` on large area
2. Enable ConsoleEcho
3. WITH ConsoleEcho: Execute `[Area count where item]` on same large area
4. Compare execution time

### Expected Results:
- Minimal performance difference (console I/O is relatively fast)
- No noticeable lag or delays
- Feature is performant enough for production use

## Security Testing

### Test Access Level:
1. Login as a player (non-admin)
2. Try to use `[ConsoleEcho]`

### Expected Results:
- Command should not execute
- No access to ConsoleEcho functionality
- Only Administrators can toggle this feature

## Edge Cases

### Test 1: No Results
- Execute a command that returns no results
- Console should show nothing or appropriate "no results" message

### Test 2: Many Results
- Execute Area command on very large area
- All results should appear in console (up to the BaseCommand limit of 10 unique messages)

### Test 3: Special Characters
- Execute command on item with special characters in name
- Console output should handle special characters correctly

## Notes for Manual Testing

This feature requires a running ServUO server to test properly. The implementation:
- Uses standard C# console output
- Follows existing ServUO patterns
- Is minimal and non-invasive
- Should work with all BaseCommand-derived commands

The best way to verify this works is:
1. Compile the server with the changes
2. Run the server
3. Follow test scenarios above
4. Verify console output matches expectations
