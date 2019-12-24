using System;
using System.Collections; 
using Server;
using Server.Engines.Quests.Haven;
using Server.Factions;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items
{
	[Flipable( 0x14EF, 0x14F0 )]
	public class MonsterContract : Item
	{
		private Mobile m_LastUser;
		private Mobile m_CompletedBy;
		private bool m_Completed;
		private Type m_type;
		private int m_amount;
		private int m_killed;
		private AosAttribute m_AA = (AosAttribute)(0);
		private AosWeaponAttribute m_AWA = (AosWeaponAttribute)(0);
		private AosArmorAttribute m_AAA = (AosArmorAttribute)(0);

		public Mobile LastUser
		{
			get{ return m_LastUser; }
			set{ m_LastUser = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public bool Completed
		{
			get{ return m_Completed; }
			set{ m_Completed = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile CompletedBy
		{
			get{ return m_CompletedBy; }
			set{ m_CompletedBy = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public AosAttribute AA
		{
			get{ return m_AA; }
			set{ m_AA = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public AosWeaponAttribute AWA
		{
			get{ return m_AWA; }
			set{ m_AWA = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public AosArmorAttribute AAA
		{
			get{ return m_AAA; }
			set{ m_AAA = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Type Monster
		{
			get{ return m_type; }
			set{ m_type = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int AmountToKill
		{
			get{ return m_amount; }
			set{ m_amount = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int AmountKilled
		{
			get{ return m_killed; }
			set
			{
				m_killed = value;

				if ( m_killed >= AmountToKill )
				{
					m_killed = AmountToKill;
					Completed = true;
				}
			}
		}
		
		[Constructable]
		public MonsterContract() : base( 0x14EF )
		{
                        Hue = 18;
			Movable = true;
			AmountToKill = Utility.Random( 10 ) + 5;
			Monster = GetRandomMonster();

			string s;
			if ( Monster.DeclaringType == null )
				s = Monster.Name;
			else
				s = Monster.FullName;

			int capsbreak = s.IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(),1);

			if( capsbreak > -1 )
			{
				string secondhalf = s.Substring( capsbreak );
 				string firsthalf = s.Substring(0, capsbreak );

				capsbreak = secondhalf.IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(),1);
				if( capsbreak > -1 )
				{
					string secondhalf2 = secondhalf.Substring( capsbreak );
 					string firsthalf2 = secondhalf.Substring(0, capsbreak );

					Name = "a Contract: "+AmountToKill+" "+firsthalf+" "+firsthalf2+" "+secondhalf2+"s";
				}
				else
					Name = "a Contract: "+AmountToKill+" "+firsthalf+" "+secondhalf+"s";
			}
			else
				Name = "a Contract: "+AmountToKill+" "+s+"s";

			AmountKilled = 0;
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if( IsChildOf( from.Backpack ) )
			{
				if ( m_LastUser != from && m_LastUser != null )
				{
					m_LastUser.CloseGump( typeof( MCBookGump ) );
					m_LastUser.CloseGump( typeof( MCGump ) );
					m_LastUser.CloseGump( typeof( MCIndGump ) );
					m_LastUser.CloseGump( typeof( MCSearchGump ) );
					m_LastUser.CloseGump( typeof( MCSearchedForGump ) );
					m_LastUser.CloseGump( typeof( MonsterContractGump ) );
				}
				m_LastUser = from;
				from.CloseGump( typeof( MCBookGump ) );
				from.CloseGump( typeof( MCGump ) );
				from.CloseGump( typeof( MCIndGump ) );
				from.CloseGump( typeof( MCSearchGump ) );
				from.CloseGump( typeof( MCSearchedForGump ) );
				from.CloseGump( typeof( MonsterContractGump ) );
				from.SendGump( new MonsterContractGump( from, this ) );
			} 
			else 
		    {
		    	from.SendLocalizedMessage( 1047012 ); // This contract must be in your backpack to use it
		    }
		}
		
		public MonsterContract( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 1 ); // version 
			
			if ( m_type != null )
			{
				writer.Write( true );
				if ( m_type.DeclaringType == null )
					writer.Write( m_type.Name );
				else
					writer.Write( m_type.FullName );
			}
			else
				writer.Write( false );
			writer.Write( m_amount );
			writer.Write( m_killed );
			writer.Write( (int)m_AA );
			writer.Write( (int)m_AWA );
			writer.Write( (int)m_AAA );
			writer.Write( (Mobile) m_CompletedBy );
				if ( m_killed >= m_amount )
				m_Completed = true;
			writer.Write( (bool) m_Completed );

	/*	Console.WriteLine("m_Type : {0}",m_type);			
		
			Console.WriteLine("m_amount : {0}",m_amount);
		
			Console.WriteLine("m_killed : {0}",m_killed);
			
			Console.WriteLine("AoS Attributes : {0}",AA);
		
			Console.WriteLine("AWA : {0}",AWA);
	
			Console.WriteLine("AAA : {0}",AAA);
	
			Console.WriteLine("m_completedby : {0}",m_CompletedBy);
	
			Console.WriteLine("m_completed : {0}",m_Completed); */
		}

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt();
			switch(version)
			{
				case 1:
				{
		if ( reader.ReadBool() )
			{//Remove after orb conversion
				if ( 0 == 0 )//DoingUpdate.Version  == 0//Remove after orb conversion
				{//Remove after orb conversion
					string st = reader.ReadString();//Remove after orb conversion
					st= (st=="HellKitten"?"HellCat":st=="HellCat"?"PredatorHellCat":st);//Remove after orb conversion
					m_type = ScriptCompiler.FindTypeByName( st );//Remove after orb conversion
				}//Remove after orb conversion
				else//Remove after orb conversion
				{//Remove after orb conversion
				m_type = ScriptCompiler.FindTypeByName( reader.ReadString() );
				}//Remove after orb conversion
			}//Remove after orb conversion

			//m_type = ScriptCompiler.FindTypeByName( reader.ReadString() );
		//	Console.WriteLine("m_Type : {0}",m_type);			
			m_amount = reader.ReadInt();
		//	Console.WriteLine("m_amount : {0}",m_amount);
			m_killed = reader.ReadInt();
		//	Console.WriteLine("m_killed : {0}",m_killed);
			m_AA = (AosAttribute)reader.ReadInt();
		//	Console.WriteLine("AoS Attributes : {0}",AA);
			m_AWA = (AosWeaponAttribute)reader.ReadInt();
		//	Console.WriteLine("AWA : {0}",AWA);
			m_AAA = (AosArmorAttribute)reader.ReadInt();
		//	Console.WriteLine("AAA : {0}",AAA);
			m_CompletedBy = reader.ReadMobile();
		//	Console.WriteLine("m_completedby : {0}",m_CompletedBy);
			m_Completed = reader.ReadBool();
		//	Console.WriteLine("m_completed : {0}",m_Completed);
			string s;
			if ( Monster.DeclaringType == null )
				s = Monster.Name;
			else
				s = Monster.FullName;
		//	Console.WriteLine("monster name : {0}",s);
			int capsbreak = s.IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(),1);
			if( capsbreak > -1 )
			{
				string secondhalf = s.Substring( capsbreak );
 				string firsthalf = s.Substring(0, capsbreak );
				if ( firsthalf == "C" )
				{
					try
					{
						Type t = ScriptCompiler.FindTypeByName( secondhalf );
						if ( t != null )
							m_type = t;
				//		Console.WriteLine("Type 2nd half : {0}",t);
					}
					catch{}
					capsbreak = secondhalf.IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(),1);
					if( capsbreak > -1 )
					{
						string secondhalf2 = secondhalf.Substring( capsbreak );
 						string firsthalf2 = secondhalf.Substring(0, capsbreak );

						Name = "a Contract: "+AmountToKill+" "+firsthalf2+" "+secondhalf2+"s";
					}
					else
						Name = "a Contract: "+AmountToKill+" "+secondhalf+"s";
				//	Console.WriteLine("contract name : {0}",Name);
				}
			}
				
				
				break;
				}
			}		
		}

		public Type GetRandomMonster()
		{
			ArrayList list = new ArrayList(); 

			foreach ( Mobile m in World.Mobiles.Values ) 
			{ 
				if ( m is BaseCreature ) 
				{
					if ( !((m as BaseCreature).FightMode == FightMode.None) && !((m as BaseCreature).FightMode == FightMode.Aggressor) && !((m as BaseCreature).FightMode == FightMode.Evil) )
					{
					//	if ( !(m is Spirit || m is KhaldunZealot || m is SpawnedOrcishLord || m is KhaldunSummoner || m is MilitiaFighter || m is MilitiaCanoneer || m is SpectralArmour || m is GrimmochDrummel || m is BladeSpirits || m is ShadowFiend || m is SeaHorse || m is Guardian || m is Model || m is FactionKnight || m is FactionNecromancer || m is FactionBerserker || m is FactionDeathKnight || m is FactionDragoon || m is FactionHenchman || m is FactionMercenary || m is FactionNecromancer || m is FactionPaladin || m is FactionSorceress || m is FactionWizard || m is DullCopperElemental || m is ShadowIronElemental || m is CopperElemental || m is BronzeElemental || m is GoldenElemental || m is AgapiteElemental || m is VeriteElemental || m is ValoriteElemental || m is BlazeElemental || m is IceElemental || m is ToxicElemental || m is ElectrumElemental || m is MoonstoneElemental || m is BloodstoneElemental || m is PlatinumElemental || m is AbysmalHorror || m is DarknightCreeper || m is DemonKnight || m is FleshRenderer || m is Impaler || m is ShadowKnight || m is WandererOfTheVoid ) ) ///*(m is EvolutionDragon || m is CustomGolem || m is SpiritDrake */ 
				/*		{
							if ( !(m is CBogle || m is CMongbat || m is CGhoul || m is CShade || m is CWraith || m is CSpectre || m is CSnake || m is CGiantRat || m is CWisp || m is CMummy || m is CGargoyle || m is CBoneMagi || m is CHellHound || m is CSkeletalMage || m is CSlime || m is CDireWolf || m is CRatman || m is CRottingCorpse || m is CStoneGargoyle || m is CGiantSpider || m is CShadowWisp || m is CLizardman || m is CTerathanWarrior || m is CTerathanDrone || m is CSilverSerpent || m is CLavaLizard || m is CPixie || m is CFireGargoyle || m is CTerathanMatriarch || m is CLichLord || m is CLich || m is COphidianMatriarch || m is CDreadSpider || m is COphidianWarrior || m is CBoneKnight || m is CSkeletalKnight || m is CDaemon || m is CHarpy || m is CImp || m is CCentaur || m is CRatmanArcher || m is COphidianArchmage || m is CRatmanMage || m is CScorpion || m is COphidianMage || m is CTerathanAvenger || m is CPoisonElemental || m is CDrake || m is COphidianKnight || m is CDragon || m is CUnicorn || m is CKirin || m is CEtherealWarrior || m is CSuccubus || m is CSerpentineDragon || m is Machine || m is BrokenMechanicalAssembly || m is MechanicalAssembly || m is BrokenMachine  || m is EarthSnake || m is GiantEarthSerpent || m is EarthDrake || m is EarthDragon || m is FireSnake || m is GiantFireSerpent || m is FireDrake || m is FireDragon || m is WaterSnake || m is GiantWaterSerpent || m is WaterDrake || m is WaterDragon || m is WindSnake || m is GiantWindSerpent || m is WindDrake || m is WindDragon) )
							{
								list.Add( m );
							}
						}
				} */ list.Add( m ); }
				}
			}

			if ( list.Count == 0 )
				return typeof( Mongbat );

			int random = Utility.RandomMinMax( 0, list.Count-1 );
			Type type = ((Mobile)list[random]).GetType();
			Mobile m2 = (Mobile)list[random];

			string s;
			if ( type.DeclaringType == null )
				s = type.Name;
			else
				s = type.FullName;
			int capsbreak = s.IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(),1);
			if( capsbreak > -1 )
			{
				string secondhalf = s.Substring( capsbreak );
 				string firsthalf = s.Substring(0, capsbreak );
				if ( firsthalf == "C" )
				{
					try
					{
						Type t = ScriptCompiler.FindTypeByName( secondhalf );
						if ( t != null )
							type = t;
					}
					catch{}
				}
			}

			ChooseAttribute( (BaseCreature)m2 );

			return type;
		}
		
		public string GetAttr()
		{
			string name = "";
			string str = "";

			if ( AA >= (AosAttribute)1 )
				str = AA.ToString();
			else if ( AWA >= (AosWeaponAttribute)1 )
				str = AWA.ToString();
			else if ( AAA >= (AosArmorAttribute)1 )
				str = AAA.ToString();

			int capsbreak = str.IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(),1);

			if( capsbreak > -1 )
			{
				string secondhalf = str.Substring( capsbreak );
 				string firsthalf = str.Substring(0, capsbreak );

				capsbreak = secondhalf.IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(),1);
				if( capsbreak > -1 )
				{
					string secondhalf2 = secondhalf.Substring( capsbreak );
 					string firsthalf2 = secondhalf.Substring(0, capsbreak );

					name = firsthalf+" "+firsthalf2+" "+secondhalf2;
				}
				else
					name = firsthalf+" "+secondhalf;
			}
			else
				name = str;

			return name;
		}

		public void ChooseAttribute( BaseCreature m )
		{
			int karma;

			if ( m.Karma <= 0 )
				karma = m.Karma * -1;
			else
				karma = m.Karma;

			int monsterstats = ( ( m.Fame + karma ) / 5 ) + ( m.RawStatTotal * 3 ) + ( ( m.HitsMaxSeed + m.StamMaxSeed + m.ManaMaxSeed ) * 3 ) + ( m.DamageMax - m.DamageMin ) + ( ( m.ColdResistSeed + m.FireResistSeed + m.EnergyResistSeed + m.PhysicalResistanceSeed + m.PoisonResistSeed ) * 5 ) + m.VirtualArmor;

			int rwd = (int)(monsterstats*0.45*AmountToKill);

			int level;

			if ( rwd <= 25000 )
				level = 1;
			else if ( rwd <= 50000 )
				level = 2;
			else if ( rwd <= 75000 )
				level = 3;
			else
				level = 4;

			if ( level == 1 )
			{
				switch( Utility.Random(3) )
				{
					case 0:AA = (AosAttribute)Utility.RandomList(0x00000008,0x00000100,0x00000200,0x00000400,0x00010000,0x00020000,0x00080000,0x00100000);break;
					case 1:AWA = (AosWeaponAttribute)Utility.RandomList(0x00000001,0x00000002,0x01000000);break;
					case 2:AAA = (AosArmorAttribute)Utility.RandomList(0x00000001,0x00000002,0x00000008);break;
				}
			}
			else if ( level == 2 )
			{
				switch( Utility.Random(2) )
				{
					case 0:AA = (AosAttribute)Utility.RandomList(0x00000001,0x00000002,0x00000004,0x00000010,0x00000800,0x00001000,0x00002000);break;
					case 1:AWA = (AosWeaponAttribute)Utility.RandomList(0x00000020,0x00000040,0x00000080,0x00000100,0x00000200,0x00000400);break;
				}
			}
			else if ( level == 3 )
			{
				switch( Utility.Random(3) )
				{
					case 0:AA = (AosAttribute)Utility.RandomList(0x00000020,0x00000040,0x00000080,0x00040000);break;
					case 1:AWA = (AosWeaponAttribute)Utility.RandomList(0x00001000,0x00002000,0x00004000,0x00008000,0x00010000);break;
					case 2:AAA = (AosArmorAttribute)Utility.RandomList(0x00000004);break;
				}
			}
			else if ( level == 4 )
			{
				switch( Utility.Random(2) )
				{
					case 0:AA = (AosAttribute)Utility.RandomList(0x00004000,0x00008000,0x00200000);break;
					case 1:AWA = (AosWeaponAttribute)Utility.RandomList(0x00000004,0x00000008,0x00000010);break;
				}
			}
		}
		
		public bool CheckAttribute( Item item, AosAttribute attr )
		{
			if ( item is BaseWeapon )
			{
				BaseWeapon b = (BaseWeapon)item;

				if ( attr == AosAttribute.WeaponDamage && b.Attributes[attr] < 40 )
					return true;
				else if ( attr == AosAttribute.DefendChance && b.Attributes[attr] < 40 )
					return true;
				else if ( attr == AosAttribute.BonusDex && b.Attributes[attr] < 10 )
					return true;
				else if ( attr == AosAttribute.EnhancePotions && b.Attributes[attr] < 30 )
					return true;
				else if ( attr == AosAttribute.CastRecovery && b.Attributes[attr] < 1 )
					return true;
				else if ( attr == AosAttribute.CastSpeed && b.Attributes[attr] < 1 )
					return true;
				else if ( attr == AosAttribute.AttackChance && b.Attributes[attr] < 40 )
					return true;
				else if ( attr == AosAttribute.BonusHits && b.Attributes[attr] < 15 )
					return true;
				else if ( attr == AosAttribute.BonusInt && b.Attributes[attr] < 10 )
					return true;
				else if ( attr == AosAttribute.LowerManaCost && b.Attributes[attr] < 40 )
					return true;
				else if ( attr == AosAttribute.LowerRegCost && b.Attributes[attr] < 20 )
					return true;
				else if ( attr == AosAttribute.Luck && b.GetLuckBonus()+b.Attributes[attr] < 100 )
					return true;
				else if ( attr == AosAttribute.BonusMana && b.Attributes[attr] < 15 )
					return true;
				else if ( attr == AosAttribute.RegenMana && b.Attributes[attr] < 10 )
					return true;
				else if ( attr == AosAttribute.ReflectPhysical && b.Attributes[attr] < 30 )
					return true;
				else if ( attr == AosAttribute.RegenStam && b.Attributes[attr] < 10 )
					return true;
				else if ( attr == AosAttribute.RegenHits && b.Attributes[attr] < 10 )
					return true;
				else if ( attr == AosAttribute.SpellDamage && b.Attributes[attr] < 40 )
					return true;
				else if ( attr == AosAttribute.BonusStam && b.Attributes[attr] < 15 )
					return true;
				else if ( attr == AosAttribute.BonusStr && b.Attributes[attr] < 10 )
					return true;
				else if ( attr == AosAttribute.WeaponSpeed && b.Attributes[attr] < 15 )
					return true;
				else if ( attr == AosAttribute.SpellChanneling && b.Attributes[attr] < 1 )
					return true;
			}
			else if ( item is BaseArmor )
			{
				BaseArmor b = (BaseArmor)item;

				if ( attr == AosAttribute.WeaponDamage && b.Attributes[attr] < 40 )
					return true;
				else if ( attr == AosAttribute.DefendChance && b.Attributes[attr] < 40 )
					return true;
				else if ( attr == AosAttribute.BonusDex && b.Attributes[attr] < 10 )
					return true;
				else if ( attr == AosAttribute.EnhancePotions && b.Attributes[attr] < 30 )
					return true;
				else if ( attr == AosAttribute.CastRecovery && b.Attributes[attr] < 1 )
					return true;
				else if ( attr == AosAttribute.CastSpeed && b.Attributes[attr] < 1 )
					return true;
				else if ( attr == AosAttribute.AttackChance && b.Attributes[attr] < 40 )
					return true;
				else if ( attr == AosAttribute.BonusHits && b.Attributes[attr] < 15 )
					return true;
				else if ( attr == AosAttribute.BonusInt && b.Attributes[attr] < 10 )
					return true;
				else if ( attr == AosAttribute.LowerManaCost && b.Attributes[attr] < 40 )
					return true;
				else if ( attr == AosAttribute.LowerRegCost && b.Attributes[attr] < 20 )
					return true;
				else if ( attr == AosAttribute.Luck && b.GetLuckBonus()+b.Attributes[attr] < 100 )
					return true;
				else if ( attr == AosAttribute.BonusMana && b.Attributes[attr] < 15 )
					return true;
				else if ( attr == AosAttribute.RegenMana && b.Attributes[attr] < 10 )
					return true;
				else if ( attr == AosAttribute.ReflectPhysical && b.Attributes[attr] < 30 )
					return true;
				else if ( attr == AosAttribute.RegenStam && b.Attributes[attr] < 10 )
					return true;
				else if ( attr == AosAttribute.RegenHits && b.Attributes[attr] < 10 )
					return true;
				else if ( attr == AosAttribute.SpellDamage && b.Attributes[attr] < 40 )
					return true;
				else if ( attr == AosAttribute.BonusStam && b.Attributes[attr] < 15 )
					return true;
				else if ( attr == AosAttribute.BonusStr && b.Attributes[attr] < 10 )
					return true;
				else if ( attr == AosAttribute.WeaponSpeed && b.Attributes[attr] < 15 )
					return true;
				else if ( attr == AosAttribute.SpellChanneling && b.Attributes[attr] < 1 )
					return true;
			}
			else if ( item is BaseJewel )
			{
				BaseJewel b = (BaseJewel)item;

				if ( attr == AosAttribute.WeaponDamage && b.Attributes[attr] < 40 )
					return true;
				else if ( attr == AosAttribute.DefendChance && b.Attributes[attr] < 40 )
					return true;
				else if ( attr == AosAttribute.BonusDex && b.Attributes[attr] < 10 )
					return true;
				else if ( attr == AosAttribute.EnhancePotions && b.Attributes[attr] < 30 )
					return true;
				else if ( attr == AosAttribute.CastRecovery && b.Attributes[attr] < 1 )
					return true;
				else if ( attr == AosAttribute.CastSpeed && b.Attributes[attr] < 1 )
					return true;
				else if ( attr == AosAttribute.AttackChance && b.Attributes[attr] < 40 )
					return true;
				else if ( attr == AosAttribute.BonusHits && b.Attributes[attr] < 15 )
					return true;
				else if ( attr == AosAttribute.BonusInt && b.Attributes[attr] < 10 )
					return true;
				else if ( attr == AosAttribute.LowerManaCost && b.Attributes[attr] < 40 )
					return true;
				else if ( attr == AosAttribute.LowerRegCost && b.Attributes[attr] < 20 )
					return true;
				else if ( attr == AosAttribute.Luck && b.Attributes[attr] < 100 )
					return true;
				else if ( attr == AosAttribute.BonusMana && b.Attributes[attr] < 15 )
					return true;
				else if ( attr == AosAttribute.RegenMana && b.Attributes[attr] < 10 )
					return true;
				else if ( attr == AosAttribute.ReflectPhysical && b.Attributes[attr] < 30 )
					return true;
				else if ( attr == AosAttribute.RegenStam && b.Attributes[attr] < 10 )
					return true;
				else if ( attr == AosAttribute.RegenHits && b.Attributes[attr] < 10 )
					return true;
				else if ( attr == AosAttribute.SpellDamage && b.Attributes[attr] < 40 )
					return true;
				else if ( attr == AosAttribute.BonusStam && b.Attributes[attr] < 15 )
					return true;
				else if ( attr == AosAttribute.BonusStr && b.Attributes[attr] < 10 )
					return true;
				else if ( attr == AosAttribute.WeaponSpeed && b.Attributes[attr] < 15 )
					return true;
				else if ( attr == AosAttribute.SpellChanneling && b.Attributes[attr] < 1 )
					return true;
			}

			return false;
		}
		public bool CheckAttribute( BaseWeapon b, AosWeaponAttribute attr )
		{
			if ( attr == AosWeaponAttribute.LowerStatReq && b.GetLowerStatReq() < 40 )
				return true;
			else if ( attr == AosWeaponAttribute.SelfRepair && b.WeaponAttributes[attr] < 10 )
				return true;
			else if ( attr == AosWeaponAttribute.HitColdArea && b.WeaponAttributes[attr] < 60 )
				return true;
			else if ( attr == AosWeaponAttribute.HitDispel && b.WeaponAttributes[attr] < 100 )
				return true;
			else if ( attr == AosWeaponAttribute.HitEnergyArea && b.WeaponAttributes[attr] < 60 )
				return true;
			else if ( attr == AosWeaponAttribute.HitFireArea && b.WeaponAttributes[attr] < 60 )
				return true;
			else if ( attr == AosWeaponAttribute.HitFireball && b.WeaponAttributes[attr] < 50 )
				return true;
			else if ( attr == AosWeaponAttribute.HitHarm && b.WeaponAttributes[attr] < 50 )
				return true;
			else if ( attr == AosWeaponAttribute.HitLeechHits && b.WeaponAttributes[attr] < 40 )
				return true;
			else if ( attr == AosWeaponAttribute.HitLightning && b.WeaponAttributes[attr] < 50 )
				return true;
			else if ( attr == AosWeaponAttribute.HitLowerAttack && b.WeaponAttributes[attr] < 40 )
				return true;
			else if ( attr == AosWeaponAttribute.HitLowerDefend && b.WeaponAttributes[attr] < 40 )
				return true;
			else if ( attr == AosWeaponAttribute.HitMagicArrow && b.WeaponAttributes[attr] < 50 )
				return true;
			else if ( attr == AosWeaponAttribute.HitLeechMana && b.WeaponAttributes[attr] < 40 )
				return true;
			else if ( attr == AosWeaponAttribute.HitPhysicalArea && b.WeaponAttributes[attr] < 60 )
				return true;
			else if ( attr == AosWeaponAttribute.HitPoisonArea && b.WeaponAttributes[attr] < 60 )
				return true;
			else if ( attr == AosWeaponAttribute.HitLeechStam && b.WeaponAttributes[attr] < 40 )
				return true;
			else if ( attr == AosWeaponAttribute.DurabilityBonus && b.WeaponAttributes[attr] < 255 )
				return true;

			return false;
		}
		public bool CheckAttribute( BaseArmor b, AosArmorAttribute attr )
		{
			if ( attr == AosArmorAttribute.LowerStatReq && b.GetLowerStatReq() < 40 )
				return true;
			else if ( attr == AosArmorAttribute.SelfRepair && b.ArmorAttributes[attr] < 10 )
				return true;
			else if ( attr == AosArmorAttribute.MageArmor && b.ArmorAttributes[attr] < 1 )
				return true;
			else if ( attr == AosArmorAttribute.DurabilityBonus && b.ArmorAttributes[attr] < 255 )
				return true;

				return false;
		}

		public bool ApplyAttribute( Mobile from, Item item )
		{
			if ( item is BaseWeapon )
			{
				BaseWeapon b = (BaseWeapon)item;

				if ( AA > 0 && CheckAttribute( b, AA ) )
				{
					b.Attributes[AA] += 1;
					return true;
				}
				else if ( AWA > 0 && CheckAttribute( b, AWA ) )
				{
					b.WeaponAttributes[AWA] += 1;
					return true;
				}
				else if ( AAA == AosArmorAttribute.LowerStatReq && CheckAttribute( b, AosWeaponAttribute.LowerStatReq ) )
				{
					b.WeaponAttributes.LowerStatReq += 1;
					return true;
				}
				else if ( AAA == AosArmorAttribute.SelfRepair && CheckAttribute( b, AosWeaponAttribute.SelfRepair ) )
				{
					b.WeaponAttributes.SelfRepair += 1;
					return true;
				}
				else if ( AAA == AosArmorAttribute.DurabilityBonus && CheckAttribute( b, AosWeaponAttribute.DurabilityBonus ) )
				{
					b.WeaponAttributes.DurabilityBonus += 1;
					return true;
				}
				else
					from.SendMessage( "You cannot apply this attribute to that item." );
			}
			else if ( item is BaseArmor )
			{
				BaseArmor b = (BaseArmor)item;

				if ( AA > 0 && AA != AosAttribute.SpellChanneling )
				{
					b.Attributes[AA] += 1;
					return true;
				}
				else if ( AAA > 0 && CheckAttribute( b, AAA ) )
				{
					b.ArmorAttributes[AAA] += 1;
					return true;
				}
				else if ( AWA == AosWeaponAttribute.LowerStatReq && CheckAttribute( b, AosArmorAttribute.LowerStatReq ) )
				{
					b.ArmorAttributes.LowerStatReq += 1;
					return true;
				}
				else if ( AWA == AosWeaponAttribute.SelfRepair && CheckAttribute( b, AosArmorAttribute.SelfRepair ) )
				{
					b.ArmorAttributes.SelfRepair += 1;
					return true;
				}
				else if ( AWA == AosWeaponAttribute.DurabilityBonus && CheckAttribute( b, AosArmorAttribute.DurabilityBonus ) )
				{
					b.ArmorAttributes.DurabilityBonus += 1;
					return true;
				}
				else
					from.SendMessage( "You cannot apply this attribute to that item." );
			}
			else if ( item is BaseJewel )
			{
				BaseJewel b = (BaseJewel)item;

				if ( AA > 0 && CheckAttribute( b, AA ) && AA != AosAttribute.SpellChanneling )
				{
					b.Attributes[AA] += 1;
					return true;
				}
				else
					from.SendMessage( "You cannot apply this attribute to that item." );
			}
			else
				from.SendMessage( "That does not have attributes." );
			return false;
		}
	}
}
