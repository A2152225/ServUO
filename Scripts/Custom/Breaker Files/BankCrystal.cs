using System; 
using System.Collections; 
using System.Collections.Generic;
using Server.Items;
using Server.Misc; 
using Server.Network; 
using Server.Multis; 

namespace Server.Items 
{ 
public class BankCrystal : Item
	{ 

      		[Constructable] 
      		public BankCrystal() : base( 0x1F1C ) 
      		{ 
         		Name = "a bank crystal"; 
         		Movable = true;
		 		Hue = 1152; 
				LootType = LootType.Blessed;
      		}

      		public BankCrystal( Serial serial ) : base( serial ) 
      		{ 
      		} 

      		public override bool HandlesOnSpeech{ get{ return true; } } 

      		public override void OnSpeech( SpeechEventArgs e ) 
      		{ 
         		if ( !e.Handled && e.Mobile.InLOS( this ) ) 
         	{ 
					string tc = e.Speech.ToLower();
            		for ( int i = 0; i < e.Keywords.Length; ++i ) 
            	{ 
               		int keyword = e.Keywords[i]; 

               		switch ( keyword ) 
               	{ 
                  	case 0x0000: // *withdraw* 
                { 
                     	e.Handled = true; 

                     	string[] split = e.Speech.Split( ' ' ); 

                     	if ( split.Length >= 2 ) 
              	{ 
                        int amount; 

                        try 
             	{ 
                       	amount = Convert.ToInt32( split[1] ); 
                } 
                        catch 
                { 
                        break; 
                } 

                        if ( amount > 5000 ) 
                { 
                        this.Say( 500381 ); // Thou canst not withdraw so much at one time! 
                } 
                        else if ( amount > 0 ) 
                { 
                        BankBox box = e.Mobile.BankBox; 

                       	if ( box == null || !box.ConsumeTotal( typeof( Gold ), amount ) ) 
                { 
                       	this.Say( 500384 ); // Ah, art thou trying to fool me? Thou hast not so much gold! 
                } 
                else 
               	{ 
                        e.Mobile.AddToBackpack( new Gold( amount ) ); 

                   	this.Say( 1010005 ); // Thou hast withdrawn gold from thy account. 
              	} 
   	}
} 

                     	break; 
              	} 
                  	case 0x0001: // *balance* 
              	{ 
						int totaltokens = 0;
                     	e.Handled = true; 

                     	BankBox box = e.Mobile.BankBox; 

							if ( box != null )
							{
								foreach( Item item in box.Items )
								{
									if( item is Tokens )
									{
										totaltokens += item.Amount;
									}
									else if( item is Container )
									{
										List<Item> list = item.Items;
										for ( int t = 0; t < list.Count; ++t )
										{
											Item inbag = (Item)list[t];
											if ( inbag is Tokens )
											{
												totaltokens += inbag.Amount;
											}
										}		
									}
								}

								this.Say( String.Format( "Thy current bank balance is {0} gold and {1} tokens.", box.TotalGold.ToString(), totaltokens ));
							}

                     	break; 
               	} 
                  	case 0x0002: // *bank* 
          	{ 
                     	e.Handled = true; 

                     	BankBox box = e.Mobile.BankBox; 

                     	if ( box != null ) 
                        box.Open(); 

                     	break; 
               	} 
                  	case 0x0003: // *check* 
              	{ 
                     	e.Handled = true; 

                   	string[] split = e.Speech.Split( ' ' ); 

                     	if ( split.Length >= 2 ) 
                { 
                        int amount; 

                        try 
          	{ 
                        amount = Convert.ToInt32( split[1] ); 
               	} 
                        catch 
               	{ 
                       	break; 
                } 

                        if ( amount < 5000 ) 
                { 
                      	this.Say( 1010006 ); // We cannot create checks for such a paltry amount of gold! 
             	} 
                       	else if ( amount > 1000000 ) 
           	{ 
                       	this.Say( 1010007 ); // Our policies prevent us from creating checks worth that much! 
              	} 
               	else 
             	{ 
                       	BankCheck check = new BankCheck( amount ); 

                       	BankBox box = e.Mobile.BankBox; 

                     	if ( box == null || !box.TryDropItem( e.Mobile, check, false ) ) 
            	{ 
                       	this.Say( 500386 ); // There's not enough room in your bankbox for the check! 
                      	check.Delete(); 
             	} 
                       	else if ( !box.ConsumeTotal( typeof( Gold ), amount ) ) 
             	{ 
                       	this.Say( 500384 ); // Ah, art thou trying to fool me? Thou hast not so much gold! 
                       	check.Delete(); 
                } 
                else 
                { 
                       	this.Say( String.Format("Into your bank box I have placed a check in the amount of: {0}",  amount.ToString() ) ); 
             	} 
     	} 
} 

                     	break; 
                  	} 
               	} 
  	} 
				if ( tc.StartsWith( "tokencheck" ))
				{
					e.Handled = true;

					if ( e.Mobile.Criminal )
					{
						this.Say( 500389 ); // I will not do business with a criminal!
						return;
					}

					string[] split = e.Speech.Split( ' ' );

					if ( split.Length >= 2 )
					{
						int amount;

						try
						{
							amount = Convert.ToInt32( split[1] );
						}

						catch
						{
							return;
						}

						if ( amount < 5000 )
						{
							this.Say( 1010006 ); // We cannot create checks for such a paltry amount of gold!
						}
						else if ( amount > 1000000 )
						{
							this.Say( 1010007 ); // Our policies prevent us from creating checks worth that much!
						}
						else
						{
							TokensBankCheck tokencheck = new TokensBankCheck( amount );

							BankBox box = e.Mobile.BankBox;

							if ( !box.TryDropItem( e.Mobile, tokencheck, false ) )
							{
								this.Say( 500386 ); // There's not enough room in your bankbox for the check!
								tokencheck.Delete();
							}
							else if ( !box.ConsumeTotal( typeof( Tokens ), amount ) )
							{
								this.Say( "Ah, art thou trying to fool me? Thou hast not so many tokens!" ); 
								tokencheck.Delete();
							}
							else
							{
								this.Say( String.Format( "Into your bank box I have placed a check in the amount of: " + amount.ToString() )); 
							}
						}
					}
				}
			}

         		base.OnSpeech( e ); 
      		} 

      		public void Say( int number ) 
      		{ 
         	PublicOverheadMessage( MessageType.Regular, 0x3B2, number ); 
      		} 

      		public void Say( string args ) 
      		{ 
         	PublicOverheadMessage( MessageType.Regular, 0x3B2, false, args ); 
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
