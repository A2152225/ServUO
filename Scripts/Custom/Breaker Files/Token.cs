//==============================================//
// Created by Dupre					//
//==============================================//
using System; 
using Server.Misc;

namespace Server.Items 
{ 
public class Tokens : Item 
{ 
[Constructable] 
public Tokens() : this( 1 ) 
{ 
} 

[Constructable] 
public Tokens( int amountFrom, int amountTo ) : this( Utility.Random( amountFrom, amountTo ) ) 
{ 
} 

[Constructable] 
public Tokens( int amount ) : base( 0xEED ) 
{
Name = "Tokens";
Stackable = true; 
Weight = 0; 
Amount = amount; 
Hue = 1152;// TokenSettings.Token_Colour;
LootType = LootType.Regular;
} 

public Tokens( Serial serial ) : base( serial ) 
{ 
} 

public override int GetDropSound() 
{ 
if ( Amount <= 1 ) 
return 0x2E4; 
else if ( Amount <= 5 ) 
return 0x2E5; 
else 
return 0x2E6; 
}

public override void OnDoubleClick( Mobile from ) 
{ 

{ 
from.SendMessage( 0x35, "Tokens can be traded in for Prizes!"); 
from.SendMessage( 0x35, "Call a GameMaster for information,"); 
from.SendMessage( 0x35, "or use the Token Stone provided."); 
} 
} 

public override void Serialize( GenericWriter writer ) 
{ 
base.Serialize( writer ); 

writer.Write( (int) 0 ); // version 
} 

public override void Deserialize( GenericReader reader ) 
{ 
base.Deserialize( reader ); 

int version = reader.ReadInt(); 
} 
} 
}
