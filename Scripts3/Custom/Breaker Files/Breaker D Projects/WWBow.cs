using System;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Targeting;
using Server.Spells;

namespace Server.Items
{
	[FlipableAttribute( 0x26C2, 0x26CC )]
	public class WWBow : BaseRanged
	{
	public int dmg =1;
	public int m_WWRange;
	public int m_WWDamage;
	public double m_WWLeech;
	public double m_DexMod;
	public bool m_WWAI;

	[CommandProperty(AccessLevel.Seer)]
	public bool WWAI
	{
	  get { return m_WWAI; }
      set { m_WWAI = value; }
	
	}
		
	[CommandProperty(AccessLevel.Seer)]
	public double DexMod
	{
	  get { return m_DexMod; }
      set { m_DexMod = value; }
	
	}	
		
	[CommandProperty(AccessLevel.Seer)]
	public double WWLeech
	{
	  get { return m_WWLeech; }
      set { m_WWLeech = value; }
	
	}
	
	[CommandProperty(AccessLevel.Seer)]
	public int WWRange
	{
	  get { return m_WWRange; }
      set { m_WWRange = value; }
	
	}
	[CommandProperty(AccessLevel.Seer)]
	public int WWDamage
	{
	  get { return m_WWDamage; }
      set { m_WWDamage = value; }
	
	}
	
		public override int EffectID{ get{ return 0xF42; } }
		public override Type AmmoType{ get{ return typeof( Arrow ); } }
		public override Item Ammo{ get{ return new Arrow(); } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MovingShot; } }

		public override int AosStrengthReq{ get{ return 45; } }
		public override int AosMinDamage{ get{ return Core.ML ? 30 : 40; } }
		public override int AosMaxDamage{ get{ return 40; } }
		public override int AosSpeed{ get{ return 15; } }
		public override float MlSpeed{ get{ return 2.00f; } }

		public override int OldStrengthReq{ get{ return 45; } }
		public override int OldMinDamage{ get{ return 15; } }
		public override int OldMaxDamage{ get{ return 17; } }
		public override int OldSpeed{ get{ return 25; } }

		public override int DefMaxRange{ get{ return 10; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.ShootBow; } }

		[Constructable]
		public WWBow() : base( 0x26C2 )
		{
			LootType = LootType.Blessed;
			Weight = 1.0;
			WWDamage = 300;
			WWRange = 1;
			DexMod = 1.5;
		}

		
		public override void OnHit( Mobile attacker, IDamageable damageable, double damageBonus)
		{
			#region SA
	 if (AmmoType != null && attacker.Player && damageable is Mobile && !((Mobile)damageable).Player && (((Mobile)damageable).Body.IsAnimal || ((Mobile)damageable).Body.IsMonster) &&
				0.4 >= Utility.RandomDouble())
			{
				((Mobile)damageable).AddToBackpack(Ammo);
			}
			#endregion

	
	
			                List<Mobile> targets = new List<Mobile>();
							
			    Map map = attacker.Map;
		int coun = 1;	
				    IPooledEnumerable eable = map.GetMobilesInRange( damageable.Location, m_WWRange );
					
					
					
					
		foreach ( Mobile creature in eable)
		{
			bool valid = Server.Spells.SpellHelper.ValidIndirectTarget(attacker,  creature);
						if (valid && creature != attacker && !creature.Blessed)
					{
						
						if( !(creature is BaseVendor))
						targets.Add(creature);
							
					}
					
					
		
		
		
	//	Damage( Mobile m, int damage, bool ignoreArmor, int phys, int fire, int cold, int pois, int nrgy )
		}
  eable.Free();
	
	  if (targets.Count > 0)
                {

 dmg = WWDamage;
		dmg +=(((int)(attacker.Dex*DexMod)));
		
		//	attacker.SendMessage("Damage is at: {0}",dmg);
		
		 for (int i = 0; i < targets.Count; ++i)
                    {
					
	
					
		
                        Mobile m = targets[i];
		//	m.Say("That hurt, for {0}: {1}",dmg,m.Name);
                
				if (!WWAI){
			
		dmg = ((int)((dmg)*((BaseCreature)(damageable)).PhysicalResistanceSeed/100));

		}

								
                        attacker.DoHarmful(m);
							
                     (m).Damage( dmg, attacker );
					
					 
					 if(WWLeech >= 1)
					 {
					 attacker.Hits += (int)(dmg*(WWLeech/100));
					 attacker.Mana += (int)(dmg*(WWLeech/100));
					 attacker.Stam += (int)(dmg*(WWLeech/100));
					 }

                    }
		
		
		/*	foreach(Mobile m in targets){
			m.Say("That hurt, for {0}: {1}",dmg,m.Name);
		(m).Damage( dmg, attacker );
		}*/
		
		}
		
		
			
			base.OnHit( attacker, damageable, damageBonus );
			
			
			
		}
		
		
		
		
		
		public WWBow( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			
			writer.Write((double)m_DexMod);
			writer.Write( (bool)m_WWAI );
			writer.Write((double)m_WWLeech );
			writer.Write( (int)m_WWDamage  );
			writer.Write( (int)m_WWRange  );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			switch ( version )
			{
			case 0:
			{
			
			m_DexMod = reader.ReadDouble();
			m_WWAI = reader.ReadBool();
			m_WWLeech = reader.ReadDouble();
			m_WWDamage = reader.ReadInt();
			m_WWRange = reader.ReadInt();
		
			
					break;
				}
			
			}
		}
	}
}