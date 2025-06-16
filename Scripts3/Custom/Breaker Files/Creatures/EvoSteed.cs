    //////////////////////////////////
   //			         		   //
  //      Scripted by Burner      //
 //		             			 //
//////////////////////////////////
using System;
using System.Collections; 
using Server.Mobiles;
using Server.Items;
using Server.Network; 
using Server.Targeting;
using Server.Gumps;

namespace Server.Mobiles
{
	[CorpseName( "an evo steed corpse" )]
	public class EvoSteed : BaseMount
	{

		private Timer m_PetLoyaltyTimer;
		private DateTime m_EndPetLoyalty;

		public DateTime EndPetLoyalty{ get{ return m_EndPetLoyalty; } set{ m_EndPetLoyalty = value; } }

		public int m_Stage;
		public int m_KP;


		public bool m_S1;
		public bool m_S2;
		public bool m_S3;
		public bool m_S4;
		public bool m_S5;
		public bool m_S6;

		public bool S1
		{
			get{ return m_S1; }
			set{ m_S1 = value; }
		}
		public bool S2
		{
			get{ return m_S2; }
			set{ m_S2 = value; }
		}
		public bool S3
		{
			get{ return m_S3; }
			set{ m_S3 = value; }
		}
		public bool S4
		{
			get{ return m_S4; }
			set{ m_S4 = value; }
		}
		public bool S5
		{
			get{ return m_S5; }
			set{ m_S5 = value; }
		}
		public bool S6
		{
			get{ return m_S6; }
			set{ m_S6 = value; }
		}

		

		[CommandProperty( AccessLevel.GameMaster )]
		public int KP
		{
			get{ return m_KP; }
			set{ m_KP = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Stage
		{
			get{ return m_Stage; }
			set{ m_Stage = value; }
		}

		[Constructable]
		public EvoSteed()  : base( "a kitten", 0x74, 0x3EA7, AIType.AI_Melee, FightMode.Closest, 12, 1, 0.1, 0.3 )
		{
			Female = Utility.RandomBool();
			Name = "a kitten";
			Body = 201;
			//Hue = Utility.RandomList( 1157, 1175, 1172, 1171, 1170, 1169, 1168, 1167, 1166, 1165 );
			BaseSoundID = 105;
			Stage = 1;

			S1 = true;
			S2 = true;
			S3 = true;
			S4 = true;
			S5 = true;
			S6 = true;

			SetStr( 196, 225 );
			SetDex( 56, 75 );
			SetInt( 76, 96 );

			SetHits( 300, 350 );

			SetDamage(11, 14 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 80 );

		
			SetSkill( SkillName.Tactics, 45.3, 64.0 );
			SetSkill( SkillName.Wrestling, 49.3, 64.0 );
			SetSkill( SkillName.Anatomy, 49.3, 64.0 );

			Fame = 300;
			Karma = -300;

			VirtualArmor = 70;

			ControlSlots = 3;


			m_PetLoyaltyTimer = new PetLoyaltyTimerE( this, TimeSpan.FromSeconds( 5.0 ) );
			m_PetLoyaltyTimer.Start();
			m_EndPetLoyalty = DateTime.Now + TimeSpan.FromSeconds( 5.0 );
		}

		public EvoSteed(Serial serial) : base(serial)
		{
		}



		

		public override void OnDamage(int amount, Mobile defender, bool willKill )
		{
			if (this.S5 == true)
			{
				if ( defender is BaseCreature )
				{
					BaseCreature bc = (BaseCreature)defender;
					
					if ( bc.Controlled != true )
					{
					int gain = amount;
					if (gain > this.Hits/10)
					gain = (int)(this.Hits/10);
					
					gain = (int)Utility.RandomMinMax(5, gain );
					
					if (gain < 5)
					{
					this.KP += 5;
					}
					else
					
					this.KP += gain;
					}
				}
			
			CheckEvolve();
			}
			base.OnDamage(amount, defender, willKill );
		}

		public void CheckEvolve()
		{
			if (Stage == 1)
			{
			if (KP >= 25000)
				{
				S1 = false;
				this.Say( "*"+ this.Name +" evolves*");
				HitsMaxSeed += 200;
				Hits = HitsMax;
						if (Name == "a kitten")
				Name = "a small rat";
				BodyValue = 238;
				BaseSoundID = 204;
				this.Stage = 2;
				this.RawStr += 150;
				this.RawInt += 60;
				this.RawDex += 60;
				SetDamage(16, 19 );
				if ( Skills.MagicResist.Value <= 25.0)
				SetSkill( SkillName.MagicResist, 25.1, 40.0 );
				if ( Skills.Tactics.Value <= 95.0)
			SetSkill( SkillName.Tactics, 95.3, 120.0 );
				if ( Skills.Wrestling.Value <= 95.0)
			SetSkill( SkillName.Wrestling, 95.3, 120.0 );
				if ( Skills.Anatomy.Value <= 95.0)
			SetSkill( SkillName.Anatomy,95.3, 120.0 );
				
				
						this.SetDamageType( ResistanceType.Physical, 100 );
						this.SetDamageType( ResistanceType.Poison, 25 );
	

						this.SetResistance( ResistanceType.Physical, 85 );
						this.SetResistance( ResistanceType.Fire, 30 );
						this.SetResistance( ResistanceType.Cold, 30 );
						this.SetResistance( ResistanceType.Poison, 30 );
						this.SetResistance( ResistanceType.Energy, 30 );
				}
			}
			//end stage 1
			if (Stage == 2)
			{
			if (KP > 375000)
				{
				S2 = false;
				this.Say( "*"+ this.Name +" evolves*");
				HitsMaxSeed += 330;
				Hits = HitsMax;
						if (Name == "a small rat")
				Name = "a large rat";
				BodyValue = 215;
				BaseSoundID = 392;
				this.Stage = 3;
				this.RawStr += 150;
				this.RawInt += 40;
				this.RawDex += 60;
				SetDamage(21, 25 );
				if (Skills.MagicResist.Value <=25.0)
				SetSkill( SkillName.MagicResist, 25.1, 50.0 );
				
						this.SetDamageType( ResistanceType.Physical, 120 );
						this.SetDamageType( ResistanceType.Poison, 35 );
	

						this.SetResistance( ResistanceType.Physical, 88 );
						this.SetResistance( ResistanceType.Fire, 40 );
						this.SetResistance( ResistanceType.Cold, 40 );
						this.SetResistance( ResistanceType.Poison, 40 );
						this.SetResistance( ResistanceType.Energy, 40 );
						
			if ( Skills.Tactics.Value <= 160.0)
			SetSkill( SkillName.Tactics, 160, 200.0 );
				if ( Skills.Wrestling.Value <= 160.0)
			SetSkill( SkillName.Wrestling,160, 200.0 );
				if ( Skills.Anatomy.Value <= 160.0)
			SetSkill( SkillName.Anatomy,160, 200.0 );
				
				}
			}
			//end stage 2
			if (Stage == 3)
			{
			if (KP > 2250000)
				{
				S3 = false;
				this.Say( "*"+ this.Name +" evolves*");
				HitsMaxSeed += 400;
				Hits = HitsMax;
				if (Name == "a large rat")
				Name = "a hellcat";
				//AI = AIType.AI_Mage;
				//FightMode = FightMode.Aggressor;
				BodyValue = 127;
				BaseSoundID = 186;
				this.Stage = 4;
				this.RawStr += 100;
				this.RawInt += 070;
				this.RawDex += 150;
				SetDamage(48, 54 );
				if (Skills.MagicResist.Value <=20.0)
				SetSkill( SkillName.MagicResist, 20.1, 40.0 );
					//	SetSkill( SkillName.Magery, 55.1, 90.0 );
					//	SetSkill( SkillName.EvalInt, 35.1, 60.0 );
					//	SetSkill( SkillName.Meditation, 35.1, 60.0 );
	
						this.SetDamageType( ResistanceType.Physical, 120 );
						this.SetDamageType( ResistanceType.Poison, 10 );
						this.SetDamageType( ResistanceType.Fire, 10 );
						this.SetDamageType( ResistanceType.Cold, 10 );
						this.SetDamageType( ResistanceType.Energy, 10 );

						this.SetResistance( ResistanceType.Physical, 90 );
						this.SetResistance( ResistanceType.Fire, 50 );
						this.SetResistance( ResistanceType.Cold, 50 );
						this.SetResistance( ResistanceType.Poison, 50 );
						this.SetResistance( ResistanceType.Energy, 50 );
				}
			}
			//end stage 3
		if (Stage == 4)
			{
			if (KP > 7250000)
				{
				S4 = false;
				this.Say( "*"+ this.Name +" evolves*");
				HitsMaxSeed += 500;
				Hits = HitsMax;
				if (Name == "a hellcat")
				Name = "a nightmare";
				AI = AIType.AI_Mage;
				//FightMode = FightMode.Closest;
				BodyValue = 179;
				BaseSoundID = 168;
				this.Stage = 5;
				this.RawStr += 100;
				this.RawInt += 170;
				this.RawDex += 50;
				SetDamage(48, 54 );
				if (Skills.MagicResist.Value <=70.0)
				SetSkill( SkillName.MagicResist, 75.1, 90.0 );
						SetSkill( SkillName.Magery, 55.1, 90.0 );
						SetSkill( SkillName.EvalInt, 35.1, 60.0 );
						SetSkill( SkillName.Meditation, 35.1, 60.0 );
	
						this.SetDamageType( ResistanceType.Physical, 120 );
						this.SetDamageType( ResistanceType.Poison, 35 );
						this.SetDamageType( ResistanceType.Fire, 25 );
						this.SetDamageType( ResistanceType.Cold, 25 );
						this.SetDamageType( ResistanceType.Energy, 25 );

						this.SetResistance( ResistanceType.Physical, 90 );
						this.SetResistance( ResistanceType.Fire, 65 );
						this.SetResistance( ResistanceType.Cold, 65 );
						this.SetResistance( ResistanceType.Poison, 65 );
						this.SetResistance( ResistanceType.Energy, 65 );
				}
			}
			//end stage 3
			if (Stage == 5)
			{
			if (KP > 17750000)
				{
				S5 = false;
				this.Say( "*"+ this.Name +" evolves*");
				HitsMaxSeed += 1500;
				Hits = HitsMax;
				if (Name == "a nightmare")
				Name = "a lich steed";
				Body = 793;
				((BaseMount)this).ItemID = 16059;
				//itemID = 0x3EBB;
				BodyValue = 793;
				BaseSoundID = 0;
				this.Stage = 6;
				this.RawStr += 260;
				this.RawInt += 370;
				this.RawDex += 250;
				SetDamage(76, 82 );
				if (Skills.MagicResist.Value <=155.0)
				SetSkill( SkillName.MagicResist, 155.1, 200.0 );
					if (Skills.Magery.Value <=155.0)
						SetSkill( SkillName.Magery, 155.1, 200.0 );
								if (Skills.EvalInt.Value <=155.0)
						SetSkill( SkillName.EvalInt, 155.1, 200.0 );
								if (Skills.Meditation.Value <=155.0)
						SetSkill( SkillName.Meditation, 155.1, 200.0 );
	
						this.SetDamageType( ResistanceType.Physical, 140 );
						this.SetDamageType( ResistanceType.Poison, 45 );
						this.SetDamageType( ResistanceType.Fire, 45 );
						this.SetDamageType( ResistanceType.Cold, 45 );
						this.SetDamageType( ResistanceType.Energy, 45 );

						this.SetResistance( ResistanceType.Physical, 98 );
						this.SetResistance( ResistanceType.Fire, 90 );
						this.SetResistance( ResistanceType.Cold, 90 );
						this.SetResistance( ResistanceType.Poison, 100 );
						this.SetResistance( ResistanceType.Energy, 90 );
				}
			}
		
		
		}

	
		public override void OnDoubleClick( Mobile from )
		{
			if ( this.Controlled == true && this.ControlMaster == from )
			{
				if (this.Stage >= 5)
				base.OnDoubleClick(from);
				else
				return;
			}
		}

	public class PetLoyaltyTimerE : Timer
	{ 
		private EvoSteed ed;

		public PetLoyaltyTimerE( EvoSteed owner, TimeSpan duration ) : base( duration ) 
		{ 
			Priority = TimerPriority.OneSecond;
			ed = owner;
		}

		protected override void OnTick() 
		{
			foreach ( Network.NetState state in Network.NetState.Instances )
			{
				if ( state.Mobile == null )
					continue;

				Mobile owner = (Mobile)state.Mobile;

				if ( ed.ControlMaster == owner )
					ed.Loyalty = BaseCreature.MaxLoyalty;
			}

			PetLoyaltyTimerE lt = new PetLoyaltyTimerE( ed, TimeSpan.FromSeconds( 5.0 ) );
			lt.Start();
			ed.EndPetLoyalty = DateTime.Now + TimeSpan.FromSeconds( 5.0 );

			Stop();
		}
	}
	public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 1);
                                writer.Write( m_S1 ); 
                        writer.Write( m_S2 ); 
                        writer.Write( m_S3 ); 
                        writer.Write( m_S4 ); 
                        writer.Write( m_S5 ); 
                        writer.Write( m_S6 ); 
			writer.Write( (int) m_KP );
			writer.Write( (int) m_Stage );
			writer.WriteDeltaTime( m_EndPetLoyalty );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
                        		
                        		m_S1 = reader.ReadBool(); 
                        		m_S2 = reader.ReadBool(); 
                        		m_S3 = reader.ReadBool(); 
                        		m_S4 = reader.ReadBool(); 
                        		m_S5 = reader.ReadBool(); 
                        		m_S6 = reader.ReadBool(); 
					m_KP = reader.ReadInt();
					m_Stage = reader.ReadInt();

					m_EndPetLoyalty = reader.ReadDeltaTime();
					m_PetLoyaltyTimer = new PetLoyaltyTimerE( this, m_EndPetLoyalty - DateTime.Now );
					m_PetLoyaltyTimer.Start();

					break;
				}
				case 0:
				{
					
					TimeSpan durationloyalty = TimeSpan.FromSeconds( 5.0 );

					

					m_PetLoyaltyTimer = new PetLoyaltyTimerE( this, durationloyalty );
					m_PetLoyaltyTimer.Start();
					m_EndPetLoyalty = DateTime.Now + durationloyalty;

					break;
				}
			}
		}
}
}