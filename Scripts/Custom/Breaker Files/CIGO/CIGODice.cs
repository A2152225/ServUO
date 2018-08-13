using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class CIGODices : Item
	{
		[Constructable]
		public CIGODices() : base( 0xFA7 )
		{
			Weight = 1.0;
		}

		public CIGODices( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( this.GetWorldLocation(), 2 ) )
				return;
		int numa = Utility.Random(1,6);
		int numb = Utility.Random(1,6);
			this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format( "*{0} rolls {1}, {2}*", from.Name, numa, numb ) );
// added in
switch ( numa + numb )
{
case 2:
{
this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format( "*{0} loses the bet and its added to the CIGO Chest!", from.Name ) );
break;
}
case 12:
{
this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format( "*{0} loses the bet and its added to the CIGO Chest!", from.Name ) );
break;
}
case 3:
{
this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format( "*{0} Wins the bet and recieves double in return!", from.Name ) );
break;
}
case 4:
{
this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format( "*{0} Wins the bet and recieves double in return!", from.Name ) );
break;
}
case 9:
{
this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format( "*{0} Wins the bet and recieves double in return!", from.Name ) );
break;
}
case 10:
{
this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format( "*{0} Wins the bet and recieves double in return!", from.Name ) );
break;
}
case 11:
{
this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format( "*{0} Wins the bet and recieves double in return!", from.Name ) );
break;
}
case 5:
{
this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format( "*{0} loses the bet!", from.Name ) );
break;
}case 6:
{
this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format( "*{0} loses the bet!", from.Name ) );
break;
}
case 8:
{
this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format( "*{0} loses the bet!", from.Name ) );
break;
}
case 7:
{
this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format( "*{0} loses the bet but makes their way to the CIGO box!!!", from.Name ) );
break;
}

}		
// added in
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}