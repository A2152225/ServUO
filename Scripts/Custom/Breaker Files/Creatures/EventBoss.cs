using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Regions;
using Server.Targeting;
using Server.Network;
using Server.Multis;
using Server.Spells;
using Server.Misc;
using Server.Items;
using Server.Mobiles;
using Server.ContextMenus;
using Server.Engines.Quests;
using Server.Factions;
using Server.Spells.Necromancy;
using Server.Spells.Spellweaving;
using Server.Spells.Bushido;
using Server.Engines.PartySystem;
using PARTY = Server.Engines.PartySystem.Party;
using Server.Engines.CannedEvil;
using Server.Gumps;

namespace Server.Mobiles
{
	[CorpseName( "a Boss corpse" )]
	public class EventBoss : BaseCreature
	{
		[Constructable]
		public EventBoss() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Boss";
			Body = 999;
			
			SetStr( 352, 384 );
			SetDex( 216, 321 );
			SetInt( 216, 220 );

			SetHits( 500, 500 );

			SetDamage( 29, 35 );

			SetDamageType( ResistanceType.Cold, 100 );
			SetDamageType( ResistanceType.Fire, 20 );
			

			SetResistance( ResistanceType.Physical, 1, 2 );
			SetResistance( ResistanceType.Poison, 1, 2 );

			SetSkill( SkillName.Poisoning, 130.1, 150.0 );
			SetSkill( SkillName.MagicResist, 150.1, 200.0 );
			SetSkill( SkillName.Tactics, 194.3, 208.0 );
			SetSkill( SkillName.Wrestling, 189.3, 214.0 );

			Fame = 30000;
			Karma = -30000;

			VirtualArmor = 1;

			Tamable = false;
			ControlSlots = 1;
			MinTameSkill = 230.1;
		}
		
		public Timer m_Timer;

		public override void GenerateLoot()
		{
			AddLoot( LootPack.SuperBoss, 8 );
			AddLoot( LootPack.Gems, 12 );	
			AddLoot( LootPack.HighScrolls, Utility.RandomMinMax( 24, 120 ) );
		}

		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish | FoodType.FruitsAndVegies | FoodType.GrainsAndHay | FoodType.Eggs; } }

		
	public override int Damage(int amount, Mobile from)
		{
		/*
////////////////////////RUNE SPELL
                        if ( from != null )
                        {
                                if ( !from.CanBeginAction( typeof(FrostBite) ) )
                                        amount -= (int)(amount*0.4);
                                if ( !from.CanBeginAction( typeof(Hallucinogen) ) )
                                        amount -= (int)(amount*0.3);
////////////////////////BLOW DART GUN
                                if ( !from.CanBeginAction( typeof(HallucinationDart) ) )
                                        amount -= (int)(amount*0.1);
////////////////////////End BLOW DART GUN
                        }
                        if ( !this.CanBeginAction( typeof(Hallucinogen) ) )
                                amount += (int)(amount*0.1);
////////////////////////End RUNE SPELL
////////////////////////BLOW DART GUN
                        if ( !this.CanBeginAction( typeof(HallucinationDart) ) )
                                amount += (int)(amount*0.1);
////////////////////////End BLOW DART GUN
*/
			int oldHits = this.Hits;

			if (Core.AOS && !this.Summoned && this.Controlled && 0.2 > Utility.RandomDouble())
				amount = (int)(amount * BonusPetDamageScalar);

			if (Spells.Necromancy.EvilOmenSpell.TryEndEffect(this))
				amount = (int)(amount * 1.25);

			Mobile oath = Spells.Necromancy.BloodOathSpell.GetBloodOath(from);
		/*	if (from is BaseCreature) //Damage Cap effect 
			{
					
			if (amount > ((BaseCreature)from).DmgCap && ((BaseCreature)from).DmgCap != 0  )
			amount = ((BaseCreature)from).DmgCap;
			}*/
			
			
			if (oath == this)
			{
				amount = (int)(amount * 1.1);
				from.Damage(amount, from);
			}
			int critchance = 1;
			int HMD = 0;
			if (from is PlayerMobile)
			{
				if (from == null)
					return 0;
					
				if (from.Dex >= 1000)
				{
				if (from.Dex >= 2000)
				HMD = 15;
				else if (from.Dex >= 1900)
				HMD = 14;
				else if (from.Dex >= 1800)
				HMD = 13;				
				else if (from.Dex >= 1700)
				HMD = 12;
				else if (from.Dex >= 1600)
				HMD = 11;
				else if (from.Dex >= 1500)
				HMD = 10;				
				else if (from.Dex >= 1400)
				HMD = 9;
				else if (from.Dex >= 1300)
				HMD = 8;
				else if (from.Dex >= 1200)
				HMD = 7;				
				else if (from.Dex >= 1100)
				HMD = 6;	
				else
				HMD = 5;
				critchance = Utility.RandomMinMax(1,100);
				if (critchance <= HMD )
				{
				amount = (int)(amount * 1.2);
				this.SpeechHue = 0x23F;
				//SayTo(from, "A critical hit!? That really didn't hurt me!");
				
				}
				//SayTo(from, "The crit chance number was {0}!",critchance);
				
				}
			
			
			
			}
			
			
			base.Damage(1, from);
			
			   if ( from == null )
                           return 0;
								
			from.Hits -= amount;
			if (from.Hits < 1)
			{
			from.Kill();
            Say("Loser!");
			}
		if (from.Alive == false)
		this.Hits += 5;
		
		if (Hits == 30)
		Enrage(this);

			if (SubdueBeforeTame && !Controlled)
			{
				if ((oldHits > (this.HitsMax / 10)) && (this.Hits <= (this.HitsMax / 10)))
					PublicOverheadMessage(MessageType.Regular, 0x3B2, false, "* The creature has been beaten into subjugation! *");
			}
			return 1;
		}
		
            
		public override void OnDamagedBySpell(Mobile from)
		{
		
		}
	
		
		public void Enrage(Mobile from)
		{
			m_Timer = new InternalTimer( from, TimeSpan.FromSeconds( 30.0 ) );
					m_Timer.Start();
		from.Say("*Kill the Boss before it heals itself*");
		}
		
		private class InternalTimer : Timer
			{
					private Mobile mob;

				public InternalTimer( Mobile from, TimeSpan duration ) : base( duration )
				{
					Priority = TimerPriority.FiftyMS;
					mob = from;
			
				
				}

		 protected override void OnTick()
			{
			    if( mob == null || mob.Deleted )
            {
                Stop();
                return;
            }
			
			mob.Hits += 50;
			Stop();
			}
			
			
			
		}
		
		public EventBoss( Serial serial ) : base( serial )
		{
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
