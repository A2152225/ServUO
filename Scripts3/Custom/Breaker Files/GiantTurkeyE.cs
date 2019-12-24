using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
    [CorpseName( "a giant turkey corpse" )]
    public class GiantTurkeyE : BaseCreature
    {
        [Constructable]
        public GiantTurkeyE() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
        {
            Name = "a giant turkey";
            Body = 1026;
         //   [COLOR=rgb(255, 0, 0)]//BaseSoundID = ;//Not Known[/COLOR]

            SetStr( 354, 494 );
            SetDex( 172, 258 );
            SetInt( 275, 357 );

            SetHits( 2000, 3000 );
            SetMana( 1000 );

            SetDamage( 8, 15 );

            SetDamageType( ResistanceType.Physical, 100 );

            SetResistance( ResistanceType.Physical, 65, 80 );
            SetResistance( ResistanceType.Fire, 71, 87 );
            SetResistance( ResistanceType.Cold, 35, 55 );
            SetResistance( ResistanceType.Poison, 45, 62 );
            SetResistance( ResistanceType.Energy, 46, 64 );

            SetSkill( SkillName.MagicResist, 60.2,90.3 );
            SetSkill( SkillName.Tactics, 80.6, 110.6 );
            SetSkill( SkillName.Wrestling, 71.8, 120.8 );
            SetSkill( SkillName.Anatomy, 75.1, 105.6 );

            Fame = 15000;
            Karma = -15000;
            Tamable = false;
			Team = Utility.Random(1,9);

        }

		public override int GetAngerSound()
        {
            return 0x275;
        }
 
        public override int GetIdleSound()
        {
            return 0x270;
        }
 
        public override int GetAttackSound()
        {
            return 0x277;
        }
 
        public override int GetHurtSound()
        {
            return 0x273;
        }
 
        public override int GetDeathSound()
        {
            return 0x279;
        }
		
		
		
        public override void GenerateLoot()
        {
			AddLoot( LootPack.SuperBoss, 4 );
			 AddToBackpack(new Gold(25000));
              // not sure on lootpack I'd venture to guess nothing
			  

        }

		
		
		public override bool OnBeforeDeath()
		{
		

			if ( .07 >= Utility.RandomDouble())
			{
			HeritageToken gift = new HeritageToken();
			gift.MoveToWorld( this.Location, this.Map );
			}
			return base.OnBeforeDeath();
		}

public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

if ( 0.15 >= Utility.RandomDouble())
ZeldaEffect(attacker);
		}


public override void OnHarmfulSpell(Mobile from)
		{ 

base.OnHarmfulSpell( from );

if ( 0.15 >= Utility.RandomDouble())
ZeldaEffect(from);

}

public void ZeldaEffect( Mobile attacker)
{
	
	Map map = this.Map;
	if ( map == null ) 	return;
this.Say( "You should know better than to hit a fowl!" );
int newTurkeys = Utility.RandomMinMax( 3, 12 );
if (this.GetTeamSize(50) >= 50)
		newTurkeys = 0;	
for ( int i = 0; i < newTurkeys; ++i )
	{
			
				
			
			
				SmallTurkey turk = new SmallTurkey();
				//turk.Level = this.Level;
				turk.Team = this.Team;
				turk.FightMode = FightMode.Closest;
bool validLocation = false;
				Point3D loc = this.Location;

				for ( int j = 0; !validLocation && j < 10; ++j )
				{
					int x = X + Utility.Random( 3 ) - 1;
					int y = Y + Utility.Random( 3 ) - 1;
					int z = map.GetAverageZ( x, y );

					if ( validLocation = map.CanFit( x, y, this.Z, 16, false, false ) )
						loc = new Point3D( x, y, Z );
					else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
						loc = new Point3D( x, y, z );
						
				} 

				turk.MoveToWorld( loc, map );
				turk.Combatant = attacker;
				
			}


}





        public override WeaponAbility GetWeaponAbility()
        {

            return WeaponAbility.BleedAttack;

        }

        public override int Meat{ get{ return 14; } }
        public override MeatType MeatType{ get{ return MeatType.Bird; } }
        public override int Feathers{ get{ return 50; } }

        public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish; } }


        private class TeleportTimer : Timer
        {
            private Mobile m_Owner;

            private static int[] m_Offsets = new int[]
            {
                -1, -1,
                -1,  0,
                -1,  1,
                0, -1,
                0,  1,
                1, -1,
                1,  0,
                1,  1
            };

            public TeleportTimer( Mobile owner ) : base( TimeSpan.FromSeconds( 5.0 ), TimeSpan.FromSeconds( 5.0 ) )
            {
                Priority = TimerPriority.TwoFiftyMS;

                m_Owner = owner;
            }

            protected override void OnTick()
            {
                if ( m_Owner.Deleted )
                {
                    Stop();
                    return;
                }

                Map map = m_Owner.Map;

                if ( map == null )
                    return;

                if ( 0.25 < Utility.RandomDouble() )
                    return;

                Mobile toTeleport = null;

                foreach ( Mobile m in m_Owner.GetMobilesInRange( 16 ) )
                {
                    if ( m != m_Owner && m.Player && m_Owner.CanBeHarmful( m ) && m_Owner.CanSee( m ) )
                    {
                        toTeleport = m;
                        break;
                    }
                }

                if ( toTeleport != null )
                {
                    int offset = Utility.Random( 8 ) * 2;

                    Point3D to = m_Owner.Location;

                    for ( int i = 0; i < m_Offsets.Length; i += 2 )
                    {
                        int x = m_Owner.X + m_Offsets[(offset + i) % m_Offsets.Length];
                        int y = m_Owner.Y + m_Offsets[(offset + i + 1) % m_Offsets.Length];

                        if ( map.CanSpawnMobile( x, y, m_Owner.Z ) )
                        {
                            to = new Point3D( x, y, m_Owner.Z );
                            break;
                        }
                        else
                        {
                            int z = map.GetAverageZ( x, y );

                            if ( map.CanSpawnMobile( x, y, z ) )
                            {
                                to = new Point3D( x, y, z );
                                break;
                            }
                        }
                    }

                    Mobile m = toTeleport;

                    Point3D from = m.Location;

                    m.Location = to;

                    Server.Spells.SpellHelper.Turn( m_Owner, toTeleport );
                    Server.Spells.SpellHelper.Turn( toTeleport, m_Owner );

                    m.ProcessDelta();

                    Effects.SendLocationParticles( EffectItem.Create( from, m.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
                    Effects.SendLocationParticles( EffectItem.Create(   to, m.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 5023 );

                    m.PlaySound( 0x1FE );

                    m_Owner.Combatant = toTeleport;
                }
            }
        }

        public GiantTurkeyE(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();


        }
    }
}