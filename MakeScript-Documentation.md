# MakeScript Command Documentation

## Overview
The `[MakeScript` command generates C# script files from existing items and mobiles in ServUO. The generated scripts can be modified and compiled to create new item/mobile types.

## Basic Usage

### Single Target
```
[MakeScript
```
Then target an item or mobile. A `.txt` file will be generated in the project root directory.

### Area Selection
```
[Area MakeScript
```
Then select a bounding box by targeting two corners. All items/mobiles in the area will be documented.

### With Where Filter
```
[Area MakeScript where type = BaseWeapon
```
This will generate scripts only for weapons in the selected area.

```
[Area MakeScript where hue = 0x123
```
This will generate scripts only for items with a specific hue.

## Generated Script Format

The command generates a `.txt` file named `MakeScript-{TypeName}-{DateTime}.txt` with the following structure:

```csharp
using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class GeneratedItemName : BaseItemType
    {
        // Change this name to customize the item name
        private static readonly string ItemName = "Original Item Name";

        [Constructable]
        public GeneratedItemName()
            : base()
        {
            Name = ItemName;
            Hue = 0x123;
            // ... other properties
        }

        public GeneratedItemName(Serial serial)
            : base(serial)
        {
        }

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
}
```

## Converting to Compilable Script

1. Rename the file extension from `.txt` to `.cs`
2. Move the file to the `Scripts/Items/` or `Scripts/Mobiles/` folder (or appropriate subfolder)
3. Edit the class name and ItemName variable at the top as needed
4. Review and adjust property values as necessary
5. Recompile the server

## Features

- **Automatic Property Detection**: The command automatically detects and includes all writable properties
- **Customizable Name**: The `ItemName` variable at the top of the script makes it easy to rename items
- **Serialization Support**: Proper Serialize/Deserialize methods are included
- **Area Support**: Works with `[Area` command for bulk operations
- **Where Filter**: Compatible with `where` conditions for filtering

## Examples

### Creating a Custom Weapon
1. Find a weapon you like in-game
2. Use `[MakeScript` and target it
3. Edit the generated file to customize properties
4. Rename class and ItemName
5. Move to Scripts folder and compile

### Documenting Quest Items
```
[Area MakeScript where name = QuestItem
```
This generates documentation for all quest items in an area.

### Exporting Rare Items
```
[Area MakeScript where hue = 0x489
```
This generates scripts for all items with a specific rare hue.

## Notes

- The command requires Administrator access level
- System properties (Serial, Location, Map, etc.) are automatically excluded
- Some complex properties may need manual adjustment
- Generated scripts are meant as starting points and may require modifications
- The `.txt` extension prevents accidental compilation until reviewed

## Troubleshooting

**Script won't compile:**
- Check that the base class exists and is accessible
- Verify all property types are valid
- Some properties may need to be removed if they're read-only in the base class

**Missing properties:**
- Some inherited properties may not be included
- Check the base class for additional properties you can set

**Complex properties:**
- Properties with complex types (objects, collections) may show as `default(TypeName)`
- These will need manual implementation

## Access Level
- Command: `[MakeScript`
- Required Level: Administrator
- Area/Where Support: Yes
