using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Gumps
{
	public class MonsterContractGump : Gump
	{
		private MonsterContract MCparent;
		
		public MonsterContractGump( Mobile from, MonsterContract parentMC ) : base( 0, 0 )
		{
			from.CloseGump( typeof( MonsterContractGump ) );
			
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			if ( parentMC.Monster == null )
				parentMC.Monster = typeof( Mongbat );
			string s;
			if ( parentMC.Monster.DeclaringType == null )
				s = parentMC.Monster.Name;
			else
				s = parentMC.Monster.FullName;

			int capsbreak = s.IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(),1);

			string name;

			if( capsbreak > -1 )
			{
				string secondhalf = s.Substring( capsbreak );
 				string firsthalf = s.Substring(0, capsbreak );

				capsbreak = secondhalf.IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(),1);
				if( capsbreak > -1 )
				{
					string secondhalf2 = secondhalf.Substring( capsbreak );
 					string firsthalf2 = secondhalf.Substring(0, capsbreak );

					name = parentMC.AmountToKill+" "+firsthalf+" "+firsthalf2+" "+secondhalf2+"s";
				}
				else
					name = parentMC.AmountToKill+" "+firsthalf+" "+secondhalf+"s";
			}
			else
			{
				name = parentMC.AmountToKill+" "+s+"s";
			}

			this.AddPage(0);
			this.AddBackground(0, 0, 350, 170, 5170);
			this.AddLabel(40, 40, 0, @"A Contract for: " + name);
			this.AddLabel(40, 60, 0, @"Amount Killed: " + parentMC.AmountKilled);
			this.AddLabel(40, 80, 0, @"Attribute: " + parentMC.GetAttr());
			if ( parentMC.AmountKilled != parentMC.AmountToKill )
			{
				this.AddButton(90, 110, 2061, 2062, 1, GumpButtonType.Reply, 0);
				this.AddLabel(104, 108, 0, @"Claim Corpse");
			}
			else
			{
				this.AddButton(90, 110, 2061, 2062, 2, GumpButtonType.Reply, 0);
				this.AddLabel(104, 108, 0, @"Apply Attr");
			}

			MCparent = parentMC;
		}
		
		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile m_from = state.Mobile;
			
			if ( info.ButtonID == 1 )
			{
				m_from.SendMessage("Please choose the corpse to add.");
				m_from.Target = new MonsterCorpseTarget( MCparent );
			}
			if ( info.ButtonID == 2 )
			{
				if ( MCparent.AmountKilled >= MCparent.AmountToKill )
				{
					m_from.SendMessage("Target the item to add this attribute to.");
					m_from.Target = new MCApplyAttrTarget( MCparent );
				}
			}
		}
		
		private class MonsterCorpseTarget : Target
		{
			private MonsterContract MCparent;
			
			public MonsterCorpseTarget( MonsterContract parentMC ) : base( -1, true, TargetFlags.None )
			{
				MCparent = parentMC;
			}
			
			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Corpse )
				{
					try
					{
						Corpse MCcorpse = (Corpse)o;
					
						if ( MCcorpse.Channeled )
						{
							from.SendMessage("This corpse has already been claimed!");
							return;
						}
						if ( MCcorpse.Killer == from || (MCcorpse.Killer is BaseCreature && ((BaseCreature)MCcorpse.Killer).ControlMaster == from) )
						{
							if ( MCparent.Monster == MCcorpse.Owner.GetType() )
							{
								MCparent.AmountKilled += 1;
								MCcorpse.Channeled = true;
								MCcorpse.Hue = 15;

								if ( MCparent.AmountKilled >= MCparent.AmountToKill )
									MCparent.CompletedBy = from;
							}
							else
								from.SendMessage("That corpse is not of the correct type!");
						}
						else
							from.SendMessage("You cannot claim someone elses work!");
					}
					catch ( Exception e )
					{
						Console.WriteLine( "MonsterContractGump.MonsterCorpseTarget Crash Prevented: "+e );
					}
				}
				else
					from.SendMessage("That is not a corpse.");
			}
		}
		
		private class MCApplyAttrTarget : Target
		{
			private MonsterContract MCparent;
			
			public MCApplyAttrTarget( MonsterContract parentMC ) : base( -1, true, TargetFlags.None )
			{
				MCparent = parentMC;
			}
			
			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Item )
				{
					Item item = (Item)o;
					if ( item.IsChildOf( from ) )
					{
					
					if (item is BaseArmor)
		/*			if (((BaseArmor)item).ArtifactRarity > 0 && !(((BaseArmor)item).CanGainLevels)  )
					{
					from.SendMessage( "You cannot apply attributes to artifacts that cannot level." );
					return ;
					}
					
					if (item is BaseJewel)
						if (((BaseJewel)item).ArtifactRarity > 0 && !(((BaseJewel)item).CanGainLevels)  )
					{
					from.SendMessage( "You cannot apply attributes to artifacts that cannot level." );
					return ;
					}
					
					if (item is BaseWeapon)
					if (((BaseWeapon)item).ArtifactRarity > 0 && !(((BaseWeapon)item).CanGainLevels)  )
					{
					from.SendMessage( "You cannot apply attributes to artifacts that cannot level." );
					return ;
					}
				*/
			
				
						if( MCparent.ApplyAttribute( from, item ) )
						{
							MCparent.Delete();
							from.SendMessage("You apply the attribute to the item.");
						}
					}
					else
						from.SendMessage("That is not yours.");
				}
				else
					from.SendMessage("That does not have attributes.");
			}
		}
	}
}
