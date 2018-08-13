using System; 
using System.Collections; 
using Server.Items; 
using Server.Gumps;
using Server.Network;
using Server.Targeting; 
using Server.Spells; 
using Server.Mobiles; 
using Server.Engines.Craft;

namespace Server.Items 
{ 
	public class MC
	{
		private Mobile m_CompletedBy;
		private bool m_Completed;
		private Type m_type;
		private int m_amount;
		private int m_killed;
		private AosAttribute m_AA = (AosAttribute)(0);
		private AosWeaponAttribute m_AWA = (AosWeaponAttribute)(0);
		private AosArmorAttribute m_AAA = (AosArmorAttribute)(0);

		public bool Completed
		{
			get{ return m_Completed; }
			set{ m_Completed = value; }
		}
		public Mobile CompletedBy
		{
			get{ return m_CompletedBy; }
			set{ m_CompletedBy = value; }
		}
		public AosAttribute AA
		{
			get{ return m_AA; }
			set{ m_AA = value; }
		}
		public AosWeaponAttribute AWA
		{
			get{ return m_AWA; }
			set{ m_AWA = value; }
		}
		public AosArmorAttribute AAA
		{
			get{ return m_AAA; }
			set{ m_AAA = value; }
		}
		public Type Monster
		{
			get{ return m_type; }
			set{ m_type = value; }
		}
		public int AmountToKill
		{
			get{ return m_amount; }
			set{ m_amount = value; }
		}
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

		public MC()
		{
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

		/*		if ( b.ArtifactRarity > 0 && !(b.CanGainLevels) )
				{
					from.SendMessage( "You cannot apply attributes to artifacts that cannot level." );
					return false;
				}
				else*/ if ( AA > 0 && CheckAttribute( b, AA ) )
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

			/*	if ( b.ArtifactRarity > 0 && !(b.CanGainLevels))
				{
					from.SendMessage( "You cannot apply attributes to artifacts that cannot level." );
					return false;
				}
				else*/ if ( AA > 0 && AA != AosAttribute.SpellChanneling )
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

			/*	if ( b.ArtifactRarity > 0 && !(b.CanGainLevels)  )
				{
					from.SendMessage( "You cannot apply attributes to artifacts that cannot level." );
					return false;
				}
				else */ if ( AA > 0 && CheckAttribute( b, AA ) && AA != AosAttribute.SpellChanneling )
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

		public MC( GenericReader reader )
		{
			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_CompletedBy = reader.ReadMobile();
					m_Completed = reader.ReadBool();
					if ( reader.ReadBool() )
						m_type = ScriptCompiler.FindTypeByName( reader.ReadString() );

					m_amount = reader.ReadInt();
					m_killed = reader.ReadInt();
					AA = (AosAttribute)reader.ReadInt();
					AWA = (AosWeaponAttribute)reader.ReadInt();
					AAA = (AosArmorAttribute)reader.ReadInt();

					if ( m_amount == m_killed )
						m_Completed = true;

					break;
				}
			}
		}

		public void Serialize( GenericWriter writer )
		{
			writer.Write( (int) 0 ); // version

			writer.Write( (Mobile) m_CompletedBy );
			writer.Write( (bool) m_Completed );
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
		}
	}

   	public class MCBook: Item, ICraftable
   	{ 
		private ArrayList m_Entries;

		private Mobile m_LastUser;
		private Mobile m_Crafter;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Crafter
		{
			get
			{
				return m_Crafter;
			}
			set
			{
				m_Crafter = value;
				InvalidateProperties();
			}
		}

		//oncraft 
		    public  int OnCraft(int quality, bool makersMark, Mobile from, Server.Engines.Craft.CraftSystem craftSystem, Type typeRes, ITool tool, Server.Engines.Craft.CraftItem craftItem, int resHue)
        {
            if (makersMark)
                Crafter = from;

            //Quality = (BookQuality)(quality - 1);

      //      MaxCharges = 100;

            return quality;
        }

		
		
		
		public ArrayList Entries{ get{ return m_Entries; } }

		[Constructable]
		public MCBook() : base( Core.AOS ? 0x22C5 : 0xEFA )
		{
			Weight = 1.0;
			LootType = LootType.Blessed;
			Name = "Monster Contract Book";
			Hue = 18;

			Layer = Layer.OneHanded;

			m_Entries = new ArrayList();
		}

		public int CountEntryType( string m )
		{
			Type t = ScriptCompiler.FindTypeByName( m );
			return CountEntryType( t );
		}

		public int CountEntryType( Type m )
		{
			int count = 0;
			for( int i = 0; i < Entries.Count; i++ )
			{
				MC mc = (MC)Entries[i];
				if ( mc.Monster == m )
					count++;
			}

			return count;
		}

		public int CountEntryType( AosAttribute a )
		{
			int count = 0;
			for( int i = 0; i < Entries.Count; i++ )
			{
				MC mc = (MC)Entries[i];
				if ( mc.AA == a )
					count++;
			}

			return count;
		}

		public int CountEntryType( AosWeaponAttribute a )
		{
			int count = 0;
			for( int i = 0; i < Entries.Count; i++ )
			{
				MC mc = (MC)Entries[i];
				if ( mc.AWA == a )
					count++;
				else if ( a == AosWeaponAttribute.SelfRepair && mc.AAA == AosArmorAttribute.SelfRepair )
					count++;
				else if ( a == AosWeaponAttribute.LowerStatReq && mc.AAA == AosArmorAttribute.LowerStatReq )
					count++;
				else if ( a == AosWeaponAttribute.DurabilityBonus && mc.AAA == AosArmorAttribute.DurabilityBonus )
					count++;
			}

			return count;
		}

		public int CountEntryType( AosArmorAttribute a )
		{
			int count = 0;
			for( int i = 0; i < Entries.Count; i++ )
			{
				MC mc = (MC)Entries[i];
				if ( mc.AAA == a )
					count++;
				else if ( a == AosArmorAttribute.SelfRepair && mc.AWA == AosWeaponAttribute.SelfRepair )
					count++;
				else if ( a == AosArmorAttribute.LowerStatReq && mc.AWA == AosWeaponAttribute.LowerStatReq )
					count++;
				else if ( a == AosArmorAttribute.DurabilityBonus && mc.AWA == AosWeaponAttribute.DurabilityBonus )
					count++;
			}

			return count;
		}

		public int CountEntryType( MC mc, bool showcompleted )
		{
			int count = 0;
			for( int i = 0; i < Entries.Count; i++ )
			{
				MC smc = (MC)Entries[i];
				if ( smc.Completed == showcompleted && mc.AA == smc.AA && mc.AAA == smc.AAA && mc.AWA == smc.AWA )
					count++;
				else if ( smc.Completed == showcompleted && ((mc.AAA == AosArmorAttribute.SelfRepair && smc.AWA == AosWeaponAttribute.SelfRepair) || (smc.AAA == AosArmorAttribute.SelfRepair && mc.AWA == AosWeaponAttribute.SelfRepair)) )
					count++;
				else if ( smc.Completed == showcompleted && ((mc.AAA == AosArmorAttribute.LowerStatReq && smc.AWA == AosWeaponAttribute.LowerStatReq) || (smc.AAA == AosArmorAttribute.LowerStatReq && mc.AWA == AosWeaponAttribute.LowerStatReq)) )
					count++;
				else if ( smc.Completed == showcompleted && ((mc.AAA == AosArmorAttribute.DurabilityBonus && smc.AWA == AosWeaponAttribute.DurabilityBonus) || (smc.AAA == AosArmorAttribute.DurabilityBonus && mc.AWA == AosWeaponAttribute.DurabilityBonus)) )
					count++;
			}

			return count;
		}

		public static bool HasListed( ArrayList list, bool showcompleted, MC mc, int count )
		{
			bool hasit = false;
			for( int i = 0; i < list.Count; i++ )
			{
				MC smc = (MC)list[i];
				if ( i < count && smc.Completed == showcompleted && mc.AA == smc.AA && mc.AAA == smc.AAA && mc.AWA == smc.AWA )
					hasit = true;
				else if ( i < count && smc.Completed == showcompleted && ((mc.AAA == AosArmorAttribute.SelfRepair && smc.AWA == AosWeaponAttribute.SelfRepair) || (smc.AAA == AosArmorAttribute.SelfRepair && mc.AWA == AosWeaponAttribute.SelfRepair)) )
					count++;
				else if ( i < count && smc.Completed == showcompleted && ((mc.AAA == AosArmorAttribute.LowerStatReq && smc.AWA == AosWeaponAttribute.LowerStatReq) || (smc.AAA == AosArmorAttribute.LowerStatReq && mc.AWA == AosWeaponAttribute.LowerStatReq)) )
					count++;
				else if ( i < count && smc.Completed == showcompleted && ((mc.AAA == AosArmorAttribute.DurabilityBonus && smc.AWA == AosWeaponAttribute.DurabilityBonus) || (smc.AAA == AosArmorAttribute.DurabilityBonus && mc.AWA == AosWeaponAttribute.DurabilityBonus)) )
					count++;
			}

			return hasit;
		}

		public static void SortLevel( ArrayList list, bool showcompleted, ref ArrayList entries )
		{
			ArrayList e = new ArrayList();

			for( int i = 0; i < list.Count; i++ )
			{
				MC mc = (MC)list[i];
				if ( mc.Completed == showcompleted )
					e.Add( mc );
			}
			entries = new ArrayList( e );
		}

		public void DropMC( Mobile from, MC mc )
		{
			if ( m_Entries.Contains( mc ) )
			{
				m_Entries.Remove( mc );
				InvalidateProperties();

				MonsterContract newMC = new MonsterContract();

				newMC.Monster = mc.Monster;
				newMC.AmountToKill = mc.AmountToKill;
				newMC.AmountKilled = mc.AmountKilled;
				newMC.AA = mc.AA;
				newMC.AAA = mc.AAA;
				newMC.AWA = mc.AWA;
				newMC.Completed = mc.Completed;
				newMC.CompletedBy = mc.CompletedBy;

				string s;
				if ( newMC.Monster.DeclaringType == null )
					s = newMC.Monster.Name;
				else
					s = newMC.Monster.FullName;

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

						newMC.Name = "a Contract: "+newMC.AmountToKill+" "+firsthalf+" "+firsthalf2+" "+secondhalf2+"s";
					}
					else
						newMC.Name = "a Contract: "+newMC.AmountToKill+" "+firsthalf+" "+secondhalf+"s";
				}
				else
				newMC.Name = "a Contract: "+newMC.AmountToKill+" "+s+"s";

				from.AddToBackpack( newMC );
			}
			else
			{
				from.SendMessage("That contract is no longer in the book.");
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( Crafter != null )
				list.Add( 1050043, Crafter.Name ); // crafted by ~1_NAME~

			list.Add( 1060663, "Contracts\t{0}", Entries.Count );

			int com = 0;
			int inc = 0;

			for( int i = 0; i < Entries.Count; i++ )
			{
				if ( ((MC)Entries[i]).Completed )
					com++;
				else
					inc++;
			}

			list.Add( 1060660, "Completed Contracts\t{0}", com );
			list.Add( 1060659, "Incompleted Contracts\t{0}", inc );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( GetWorldLocation(), 1 ) )
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
				from.SendGump( new MCBookGump( from, this, false ) );
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is MonsterContract )
			{
				MonsterContract mc = (MonsterContract)dropped;

				if ( mc.LastUser != from && mc.LastUser != null )
				{
					mc.LastUser.CloseGump( typeof( MCBookGump ) );
					mc.LastUser.CloseGump( typeof( MCGump ) );
					mc.LastUser.CloseGump( typeof( MCIndGump ) );
					mc.LastUser.CloseGump( typeof( MCSearchGump ) );
					mc.LastUser.CloseGump( typeof( MCSearchedForGump ) );
					mc.LastUser.CloseGump( typeof( MonsterContractGump ) );
				}
				from.CloseGump( typeof( MCBookGump ) );
				from.CloseGump( typeof( MCGump ) );
				from.CloseGump( typeof( MCIndGump ) );
				from.CloseGump( typeof( MCSearchGump ) );
				from.CloseGump( typeof( MCSearchedForGump ) );
				from.CloseGump( typeof( MonsterContractGump ) );

				MC m = new MC();

				m.Monster = mc.Monster;
				m.AmountToKill = mc.AmountToKill;
				m.AmountKilled = mc.AmountKilled;
				m.AA = mc.AA;
				m.AAA = mc.AAA;
				m.AWA = mc.AWA;
				m.Completed = mc.Completed;
				m.CompletedBy = mc.CompletedBy;

				m_Entries.Add( m );
				InvalidateProperties();

				dropped.Delete();

				//from.Send( new PlaySound( 0x42, GetWorldLocation() ) );

				from.SendMessage( "You drop the contract into the book." );

				return true;
			}

			return false;
		}

		#region ICraftable Members

		public int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			if ( quality == 2 )
				Crafter = from;

			return quality;
		}

		#endregion

            	public MCBook( Serial serial ) : base ( serial ) 
            	{             
           	} 

           	public override void Serialize( GenericWriter writer ) 
           	{ 
              		base.Serialize( writer ); 

			writer.Write( (int) 0 );

			writer.Write( m_Entries.Count );

			for ( int i = 0; i < m_Entries.Count; ++i )
				((MC)m_Entries[i]).Serialize( writer );

			writer.Write( m_Crafter );
           	} 
            
           	public override void Deserialize( GenericReader reader ) 
           	{ 
              		base.Deserialize( reader ); 
              		int version = reader.ReadInt(); 

			switch ( version )
			{
				case 0:
				{
					int count = reader.ReadInt();

					m_Entries = new ArrayList( count );

					for ( int i = 0; i < count; ++i )
						m_Entries.Add( new MC( reader ) );

					m_Crafter = reader.ReadMobile();

					break;
				}
			}
           	} 
        }
	public class MCBookGump : Gump
	{
		private Mobile m_Mobile;
		private MCBook m_Book;
		private bool m_ShowCompleted;

		public MCBookGump( Mobile mobile, MCBook book, bool showcompleted ) : base( 25, 50 )
		{
			m_Mobile = mobile;
			m_Book = book;
			m_ShowCompleted = showcompleted;

			AddPage( 0 );

			AddBackground( 0, 0, 230, 180, 0x2436 );

			if ( showcompleted )
				AddLabel( 30, 20, 1152, "Completed Monster Contracts" );
			else
				AddLabel( 30, 20, 1152, "Incomplete Monster Contracts" );

			AddButton( 10, 40, 2103, 2104, 1, GumpButtonType.Reply, 0 );
			AddLabel( 25, 37, 1152, "Monster Contracts: "+book.Entries.Count );

			AddButton( 20, 65, 2103, 2104, 2, GumpButtonType.Reply, 0 );
			if ( showcompleted )
				AddLabel( 35, 62, 1152, "Incomplete Contracts" );
			else
				AddLabel( 35, 62, 1152, "Completed Contracts" );

			AddButton( 20, 85, 2061, 2062, 3, GumpButtonType.Reply, 0 );
			AddLabel( 34, 83, 1152, @"Claim Corpse" );

			AddButton( 20, 105, 2103, 2104, 4, GumpButtonType.Reply, 0 );
			AddLabel( 35, 102, 1152, "Collect All Contracts" );

			AddButton( 20, 125, 2103, 2104, 5, GumpButtonType.Reply, 0 );
			AddLabel( 35, 122, 1152, "Search" );

			AddButton( 20, 150, 2103, 2104, 0, GumpButtonType.Reply, 0 );
			AddLabel( 35, 147, 1152, "Close" );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile; 

			if ( from == null || m_Book == null )
				return;

			if ( info.ButtonID == 0 )
			{
				from.SendMessage( "Closed." );
			}
			if ( info.ButtonID == 1 )
			{
				from.SendGump( new MCGump( from, m_Book, m_ShowCompleted ) );
			}
			if ( info.ButtonID == 2 )
			{
				from.SendGump( new MCBookGump( from, m_Book, !m_ShowCompleted ) );
			}
			if ( info.ButtonID == 3 )
			{
				from.SendMessage( "Please choose the corpse to add." );
				from.Target = new MonsterCorpseTarget( m_Book );
			}
			if ( info.ButtonID == 4 )
			{
				int collected = 0;

				Item[] list = from.Backpack.FindItemsByType( typeof( MonsterContract ) );

				for( int i = 0; i < list.Length; i++ )
				{
					Item item = list[i];

					if ( item is MonsterContract )
					{
						MonsterContract mc = (MonsterContract)item;

						MC m = new MC();

						m.Monster = mc.Monster;
						m.AmountToKill = mc.AmountToKill;
						m.AmountKilled = mc.AmountKilled;
						m.AA = mc.AA;
						m.AAA = mc.AAA;
						m.AWA = mc.AWA;
						m.Completed = mc.Completed;
						m.CompletedBy = mc.CompletedBy;

						m_Book.Entries.Add( m );
						collected++;

						mc.Delete();
					}
				}
				m_Book.InvalidateProperties();

				from.SendMessage( "{0} contracts were collected in the book.", collected );
			}
			if ( info.ButtonID == 5 )
			{
				from.SendGump( new MCSearchGump( m_Book ) );
			}
		}

		private class MonsterCorpseTarget : Target
		{
			private MCBook book;
			
			public MonsterCorpseTarget( MCBook mbook ) : base( -1, true, TargetFlags.None )
			{
				book = mbook;
			}
			
			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Corpse )
				{
					Corpse MCcorpse = (Corpse)o;
					
					if ( MCcorpse.Channeled )
					{
						from.SendMessage("This corpse has already been claimed!");
						return;
					}
					if ( MCcorpse.Killer == from || (MCcorpse.Killer is BaseCreature && ((BaseCreature)MCcorpse.Killer).ControlMaster == from) )
					{
						bool done = false;
						for( int i = 0; i < book.Entries.Count; i++ )
						{
							if ( done )
								continue;

							MC mc = (MC)book.Entries[i];

							if ( mc.Monster == MCcorpse.Owner.GetType() )
							{
								done = true;
								mc.AmountKilled += 1;
								MCcorpse.Channeled = true;
								MCcorpse.Hue = 15;

								if ( mc.AmountKilled >= mc.AmountToKill )
									mc.CompletedBy = from;
							}
						}
						if ( !done )
							from.SendMessage("There are no contracts in this book that need this mosnter's corpse.");
					}
					else
						from.SendMessage("You cannot claim someone elses work!");
				}
				else
					from.SendMessage("That is not a corpse.");
			}
		}
	}
	public class MCGump : Gump
	{
		private Mobile m_Mobile;
		private MCBook m_Book;
		private bool m_ShowCompleted;
		private int m_Page;

		public MCGump( Mobile mobile, MCBook book, bool showcompleted ) : this( mobile, book, showcompleted, 1 )
		{
		}
		public MCGump( Mobile mobile, MCBook book, bool showcompleted, int page ) : base( 25, 50 )
		{
			m_Mobile = mobile;
			m_Book = book;
			m_ShowCompleted = showcompleted;
			m_Page = page;

			if ( m_Book == null )
				return;

			AddPage( 0 );

			AddBackground( 0, 0, 700, 560, 0x2436 );

			if ( showcompleted )
				AddLabel( 30, 20, 1152, "Completed Contracts" );
			else
				AddLabel( 30, 20, 1152, "Incomplete Contracts" );

			AddButton( 20, 465, 2103, 2104, 0, GumpButtonType.Reply, 0 );
			AddLabel( 35, 462, 1152, "Close" );

			ArrayList list = null;

			MCBook.SortLevel( book.Entries, showcompleted, ref list );

			int totalpages = (int)((list.Count/20)+( (list.Count % 20 == 0) ? 0 : 1 ));
			AddLabel( 450, 20, 1152, "Page "+page+"/"+totalpages );

			int index = (m_Page*20)-20;

			if ( list.Count > index+20 )
			{
				AddButton( 250, 450, 0x1196, 0x1196, 1, GumpButtonType.Reply, 0 );
				AddLabel( 250, 435, 1152, "Next page" ); 
			}
			if ( m_Page > 1 )
			{

				AddButton( 360, 450, 0x119a, 0x119a, 2, GumpButtonType.Reply, 0 );
				AddLabel( 360, 435, 1152, "Previous page" ); 
			}

			for ( int i = 0; i < 20 && index >= 0 && index < list.Count; ++i, ++index )
			{
				MC mc = (MC)list[index];

				if ( mc.Monster == null )
					mc.Monster = typeof( Mongbat );
				string s;
				if ( mc.Monster.DeclaringType == null )
					s = mc.Monster.Name;
				else
					s = mc.Monster.FullName;

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

						name = firsthalf+" "+firsthalf2+" "+secondhalf2+"s";
					}
					else
						name = firsthalf+" "+secondhalf+"s";
				}
				else
				{
					name = s+"s";
				}

				AddButton( 10, 45 + (i * 20), 2103, 2104, i+3, GumpButtonType.Reply, 0 );
				if ( mc.Completed )
				{
					if ( mc.CompletedBy == null )
						AddLabel( 25, 42 + (i * 20), 1152, "Attribute: "+mc.GetAttr()+", Monster: "+name+", To Kill: "+mc.AmountKilled+"/"+mc.AmountToKill+", Completed" );
					else
						AddLabel( 25, 42 + (i * 20), 1152, "Attribute: "+mc.GetAttr()+", Monster: "+name+", To Kill: "+mc.AmountKilled+"/"+mc.AmountToKill+", Completed By: "+mc.CompletedBy.Name );
				}
				else
					AddLabel( 25, 42 + (i * 20), 1152, "Attribute: "+mc.GetAttr()+", Monster: "+name+", To Kill: "+mc.AmountKilled+"/"+mc.AmountToKill );
			}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

			if ( info.ButtonID == 0 )
			{
				from.SendMessage( "Back." );
				from.SendGump( new MCBookGump( from, m_Book, false ) );
			}
			if ( info.ButtonID == 1 )
			{
				from.SendGump( new MCGump( from, m_Book, m_ShowCompleted, m_Page+1 ) );
			}
			if ( info.ButtonID == 2 )
			{
				from.SendGump( new MCGump( from, m_Book, m_ShowCompleted, m_Page-1 ) );
			}
			if ( info.ButtonID >= 3 )
			{
				ArrayList list = null;
				MCBook.SortLevel( m_Book.Entries, m_ShowCompleted, ref list );
				int index = (m_Page*20)-20;
				if ( list.Count >= info.ButtonID-3 )
				{
					if ( (index+(info.ButtonID-3)) < list.Count )
					{
						MC mc = (MC)list[(index+(info.ButtonID-3))];
						from.SendGump( new MCIndGump( from, m_Book, mc ) );
					}
				}
			}
		}
	}
	public class MCIndGump : Gump
	{
		private Mobile m_Mobile;
		private MCBook m_Book;
		private MC m_MC;

		public MCIndGump( Mobile mobile, MCBook book, MC mc ) : base( 25, 50 )
		{
			m_Mobile = mobile;
			m_Book = book;
			m_MC = mc;

			if ( mc.Monster == null )
				mc.Monster = typeof( Mongbat );
			string s;
			if ( mc.Monster.DeclaringType == null )
				s = mc.Monster.Name;
			else
				s = mc.Monster.FullName;

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

					name = firsthalf+" "+firsthalf2+" "+secondhalf2+"s";
				}
				else
					name = firsthalf+" "+secondhalf+"s";
			}
			else
			{
				name = s+"s";
			}

			AddPage( 0 );

			AddBackground( 0, 0, 220, 180, 0x2436 );

			AddLabel( 10, 20, 1152, name+" Contract" );

			AddLabel( 10, 40, 1152, "To Kill: "+mc.AmountKilled+"/"+mc.AmountToKill );

			AddLabel( 10, 60, 1152, "Attribute: "+mc.GetAttr() );

			if ( mc.Completed && mc.CompletedBy != null )
				AddLabel( 10, 80, 1152, "Completed By: "+mc.CompletedBy.Name );

			AddButton( 10, 100, 2437, 2438, 1, GumpButtonType.Reply, 0 );
			AddLabel( 25, 97, 1152, "Drop Contract" );

			AddButton( 10, 120, 2061, 2062, 2, GumpButtonType.Reply, 0 );
			if ( mc.AmountKilled < mc.AmountToKill )
				this.AddLabel( 24, 118, 1152, @"Claim Corpse" );
			else
				this.AddLabel( 24, 118, 1152, @"Apply Attr" );

			AddButton( 10, 150, 2103, 2104, 0, GumpButtonType.Reply, 0 );
			AddLabel( 25, 147, 1152, "Close" );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

			if ( info.ButtonID == 0 )
			{
				from.SendMessage( "Closed." );
			}
			if ( info.ButtonID == 1 )
			{
				m_Book.DropMC( from, m_MC );
			}
			if ( info.ButtonID == 2 )
			{
				if ( m_MC.AmountKilled < m_MC.AmountToKill )
				{
					from.SendMessage( "Please choose the corpse to add." );
					from.Target = new MonsterCorpseTarget( m_MC );
				}
				else
				{
					from.SendMessage( "Target the item to add this attribute to." );
					from.Target = new MCApplyAttrTarget( m_Book, m_MC );
				}
			}
		}

		private class MonsterCorpseTarget : Target
		{
			private MC mc;
			
			public MonsterCorpseTarget( MC mmc ) : base( -1, true, TargetFlags.None )
			{
				mc = mmc;
			}
			
			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Corpse )
				{
					Corpse MCcorpse = (Corpse)o;
					
					if ( MCcorpse.Channeled )
					{
						from.SendMessage("This corpse has already been claimed!");
						return;
					}
					if ( MCcorpse.Killer == from || (MCcorpse.Killer is BaseCreature && ((BaseCreature)MCcorpse.Killer).ControlMaster == from) )
					{
						if ( mc.Monster == MCcorpse.Owner.GetType() )
						{
							mc.AmountKilled += 1;
							MCcorpse.Channeled = true;
							MCcorpse.Hue = 15;

							if ( mc.AmountKilled >= mc.AmountToKill )
								mc.CompletedBy = from;
						}
						else
							from.SendMessage("That corpse is not of the correct type!");
					}
					else
						from.SendMessage("You cannot claim someone elses work!");
				}
				else
					from.SendMessage("That is not a corpse.");
			}
		}

		private class MCApplyAttrTarget : Target
		{
			private MC mc;
			private MCBook book;
			
			public MCApplyAttrTarget( MCBook mbook, MC mmc ) : base( -1, true, TargetFlags.None )
			{
				mc = mmc;
				book = mbook;
			}
			
			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Item )
				{
					Item item = (Item)o;
					if ( item.IsChildOf( from ) )
					{
						if ( book.Entries.Contains( mc ) )
						{
							if( mc.ApplyAttribute( from, item ) )
							{
								book.Entries.Remove( mc );
								from.SendMessage("You apply the attribute to the item.");
							}
						}
						else
							from.SendMessage("That contract has alread been used.");
					}
					else
						from.SendMessage("That is not yours.");
				}
				else
					from.SendMessage("That does not have attributes.");
			}
		}
	}
	public class MCSearchGump : Gump
	{
		private MCBook m_Book;

		public MCSearchGump( MCBook book ) : base( 25, 50 )
		{
			m_Book = book;

			AddPage( 0 );

			AddBackground( 0, 0, 290, 220, 0x2436 );
			AddBackground( 290, 0, 700, 550, 0x2436 );

			AddLabel( 125, 20, 1152, "Contract Search" );

			AddCheck( 40, 60, 208, 209, false, 0 );
			AddLabel( 70, 60, 1152, "Monster Name:" );
			AddTextEntry( 170, 60, 70, 15, 1152, 0, "Name Here" );

			AddCheck( 40, 80, 208, 209, false, 1 );
			AddLabel( 70, 80, 1152, "Amount To Kill:" );
			AddTextEntry( 170, 80, 70, 15, 1152, 1, "0" );

			AddCheck( 40, 100, 208, 209, false, 2 );
			AddLabel( 70, 100, 1152, "Amount Killed:" );
			AddTextEntry( 170, 100, 70, 15, 1152, 2, "0" );

			AddCheck( 40, 120, 208, 209, false, 3 );
			AddLabel( 70, 120, 1152, "Percentage Killed:" );
			AddTextEntry( 190, 120, 70, 15, 1152, 3, "0" );

			AddCheck( 40, 140, 208, 209, false, 4 );
			AddLabel( 70, 140, 1152, "Only Incomplete" );

			AddCheck( 40, 160, 208, 209, false, 5 );
			AddLabel( 70, 160, 1152, "Only Complete" );

			AddLabel( 360, 20, 1152, "AoS Attributes" );

			AddCheck( 330, 60, 208, 209, false, 6 );
			AddLabel( 360, 60, 1152, "Hit Regen" );
			AddCheck( 330, 80, 208, 209, false, 7 );
			AddLabel( 360, 80, 1152, "Stam Regen" );
			AddCheck( 330, 100, 208, 209, false, 8 );
			AddLabel( 360, 100, 1152, "Mana Regen" );
			AddCheck( 330, 120, 208, 209, false, 9 );
			AddLabel( 360, 120, 1152, "Defense Chance Increase" );
			AddCheck( 330, 140, 208, 209, false, 10 );
			AddLabel( 360, 140, 1152, "Attack Chance Increase" );
			AddCheck( 330, 160, 208, 209, false, 11 );
			AddLabel( 360, 160, 1152, "Strength Bonus" );
			AddCheck( 330, 180, 208, 209, false, 12 );
			AddLabel( 360, 180, 1152, "Dexterity Bonus" );
			AddCheck( 330, 200, 208, 209, false, 13 );
			AddLabel( 360, 200, 1152, "Intelligence Bonus" );
			AddCheck( 330, 220, 208, 209, false, 14 );
			AddLabel( 360, 220, 1152, "Hit Point Increase" );
			AddCheck( 330, 240, 208, 209, false, 15 );
			AddLabel( 360, 240, 1152, "Stamina Increase" );
			AddCheck( 330, 260, 208, 209, false, 16 );
			AddLabel( 360, 260, 1152, "Mana Increase" );
			AddCheck( 330, 280, 208, 209, false, 17 );
			AddLabel( 360, 280, 1152, "Damage Increase" );
			AddCheck( 330, 300, 208, 209, false, 18 );
			AddLabel( 360, 300, 1152, "Swing Speed Increase" );
			AddCheck( 330, 320, 208, 209, false, 19 );
			AddLabel( 360, 320, 1152, "Spell Damage Increase" );
			AddCheck( 330, 340, 208, 209, false, 20 );
			AddLabel( 360, 340, 1152, "Faster Cast Recovery" );
			AddCheck( 330, 360, 208, 209, false, 21 );
			AddLabel( 360, 360, 1152, "Faster Casting" );
			AddCheck( 330, 380, 208, 209, false, 22 );
			AddLabel( 360, 380, 1152, "Lower Mana Cost" );
			AddCheck( 330, 400, 208, 209, false, 23 );
			AddLabel( 360, 400, 1152, "Lower Reagent Cost" );
			AddCheck( 330, 420, 208, 209, false, 24 );
			AddLabel( 360, 420, 1152, "Reflect Physical" );
			AddCheck( 330, 440, 208, 209, false, 25 );
			AddLabel( 360, 440, 1152, "Enhance Potions" );
			AddCheck( 330, 460, 208, 209, false, 26 );
			AddLabel( 360, 460, 1152, "Luck" );
			AddCheck( 330, 480, 208, 209, false, 27 );
			AddLabel( 360, 480, 1152, "Spell Channeling" );
			AddCheck( 330, 500, 208, 209, false, 28 );
			AddLabel( 360, 500, 1152, "Night Sight" );
			AddCheck( 330, 520, 208, 209, false, 29 );
			AddLabel( 360, 520, 1152, "Mage Armor" );

			AddCheck( 550, 60, 208, 209, false, 30 );
			AddLabel( 580, 60, 1152, "Hit Life Leech" );
			AddCheck( 550, 80, 208, 209, false, 31 );
			AddLabel( 580, 80, 1152, "Hit Stamina Leech" );
			AddCheck( 550, 100, 208, 209, false, 32 );
			AddLabel( 580, 100, 1152, "Hit Mana Leech" );
			AddCheck( 550, 120, 208, 209, false, 33 );
			AddLabel( 580, 120, 1152, "Hit Lower Attack" );
			AddCheck( 550, 140, 208, 209, false, 34 );
			AddLabel( 580, 140, 1152, "Hit Lower Defense" );
			AddCheck( 550, 160, 208, 209, false, 35 );
			AddLabel( 580, 160, 1152, "Hit Magic Arrow" );
			AddCheck( 550, 180, 208, 209, false, 36 );
			AddLabel( 580, 180, 1152, "Hit Harm" );
			AddCheck( 550, 200, 208, 209, false, 37 );
			AddLabel( 580, 200, 1152, "Hit Fireball" );
			AddCheck( 550, 220, 208, 209, false, 38 );
			AddLabel( 580, 220, 1152, "Hit Lightning" );
			AddCheck( 550, 240, 208, 209, false, 39 );
			AddLabel( 580, 240, 1152, "Hit Dispel" );
			AddCheck( 550, 260, 208, 209, false, 40 );
			AddLabel( 580, 260, 1152, "Hit Cold Area" );
			AddCheck( 550, 280, 208, 209, false, 41 );
			AddLabel( 580, 280, 1152, "Hit Fire Area" );
			AddCheck( 550, 300, 208, 209, false, 42 );
			AddLabel( 580, 300, 1152, "Hit Poison Area" );
			AddCheck( 550, 320, 208, 209, false, 43 );
			AddLabel( 580, 320, 1152, "Hit Energy Area" );
			AddCheck( 550, 340, 208, 209, false, 44 );
			AddLabel( 580, 340, 1152, "Hit Physical Area" );
			AddCheck( 550, 360, 208, 209, false, 45 );
			AddLabel( 580, 360, 1152, "Use Best Weapon Skill" );
			AddCheck( 550, 380, 208, 209, false, 46 );
			AddLabel( 580, 380, 1152, "Mage Weapon" );
			AddCheck( 550, 400, 208, 209, false, 47 );
			AddLabel( 580, 400, 1152, "Lower Requirements" );
			AddCheck( 550, 420, 208, 209, false, 48 );
			AddLabel( 580, 420, 1152, "Self Repair" );
			AddCheck( 550, 440, 208, 209, false, 49 );
			AddLabel( 580, 440, 1152, "Durability Bonus" );

			AddButton( 40, 190, 4005, 4007, 0, GumpButtonType.Reply, 0 );
			AddLabel( 70, 190, 1152, "Back" );
			AddButton( 120, 190, 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddLabel( 150, 190, 1152, "Go" );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

			bool[] attrs = new bool[44];
			for( int k = 0; k < attrs.Length; k++ )
				attrs[k] = info.IsSwitched( k+6 );

			if ( info.ButtonID == 0 )
			{
				from.SendGump( new MCBookGump( from, m_Book, false ) );
			}
			if ( info.ButtonID == 1 )
			{
				string mn = ""; 
				int atk = 0; 
				int ak = 0; 
				int pk = 0; 

				foreach( TextRelay tr in info.TextEntries )
				{
					switch ( tr.EntryID )
					{
						case 0:
						{
							if ( tr.Text != null )
							{
								if ( tr.Text != "" )
								{
									mn = tr.Text;
								}
							}
							break;
						}
						case 1:
						{
							if ( tr.Text != null )
							{
								if ( tr.Text != "" )
								{
									try{atk = int.Parse(tr.Text);}
									catch{}
								}
							}
							break;
						}
						case 2:
						{
							if ( tr.Text != null )
							{
								if ( tr.Text != "" )
								{
									try{ak = int.Parse(tr.Text);}
									catch{}
								}
							}
							break;
						}
						case 3:
						{
							if ( tr.Text != null )
							{
								if ( tr.Text != "" )
								{
									try{pk = Utility.ToInt32(tr.Text);}
									catch{}
								}
							}
							break;
						}
					}
				}

				ArrayList list = new ArrayList();

				for ( int i = 0; i < m_Book.Entries.Count; ++i )
				{
					MC mc = (MC)m_Book.Entries[i];

					bool com = mc.Completed;
					int matk = mc.AmountToKill;
					int mak = mc.AmountKilled;
					int mpk = (int)((mc.AmountKilled/mc.AmountToKill)*100);
					Type mtype = mc.Monster;

					bool canadd = true;

					for( int j = 0; j < 6; ++j )
					{
						if ( info.IsSwitched( j ) )
						{
							if ( j == 0 )
							{
								bool hasm = false;
								Type type = ScriptCompiler.FindTypeByName( mn );
								if ( type != null )
								{
									if ( type == mtype )
										hasm = true;
								}
								canadd = hasm;
							}
							if ( j == 1 )
							{
								if ( matk != atk )
									canadd = false;
							}
							if ( j == 2 )
							{
								if ( mak != ak )
									canadd = false;
							}
							if ( j == 3 )
							{
								if ( mpk != pk )
									canadd = false;
							}
							if ( j == 4 )
							{
								if ( com || matk == mak )
									canadd = false;
							}
							if ( j == 5 )
							{
								if ( !com )
									canadd = false;
							}
						}
					}

					if ( canadd )
					{
						for( int n = 0; n < attrs.Length; n++ )
						{
							bool attr = attrs[n];

							if ( attr )
							{
								if ( n == 0 && mc.AA != AosAttribute.RegenHits )
									canadd = false;
								if ( n == 1 && mc.AA != AosAttribute.RegenStam )
									canadd = false;
								if ( n == 2 && mc.AA != AosAttribute.RegenMana )
									canadd = false;
								if ( n == 3 && mc.AA != AosAttribute.DefendChance )
									canadd = false;
								if ( n == 4 && mc.AA != AosAttribute.AttackChance )
									canadd = false;
								if ( n == 5 && mc.AA != AosAttribute.BonusStr )
									canadd = false;
								if ( n == 6 && mc.AA != AosAttribute.BonusDex )
									canadd = false;
								if ( n == 7 && mc.AA != AosAttribute.BonusInt )
									canadd = false;
								if ( n == 8 && mc.AA != AosAttribute.BonusHits )
									canadd = false;
								if ( n == 9 && mc.AA != AosAttribute.BonusStam )
									canadd = false;
								if ( n == 10 && mc.AA != AosAttribute.BonusMana )
									canadd = false;
								if ( n == 11 && mc.AA != AosAttribute.WeaponDamage )
									canadd = false;
								if ( n == 12 && mc.AA != AosAttribute.WeaponSpeed )
									canadd = false;
								if ( n == 13 && mc.AA != AosAttribute.SpellDamage )
									canadd = false;
								if ( n == 14 && mc.AA != AosAttribute.CastRecovery )
									canadd = false;
								if ( n == 15 && mc.AA != AosAttribute.CastSpeed )
									canadd = false;
								if ( n == 16 && mc.AA != AosAttribute.LowerManaCost )
									canadd = false;
								if ( n == 17 && mc.AA != AosAttribute.LowerRegCost )
									canadd = false;
								if ( n == 18 && mc.AA != AosAttribute.ReflectPhysical )
									canadd = false;
								if ( n == 19 && mc.AA != AosAttribute.EnhancePotions )
									canadd = false;
								if ( n == 20 && mc.AA != AosAttribute.Luck )
									canadd = false;
								if ( n == 21 && mc.AA != AosAttribute.SpellChanneling )
									canadd = false;
								if ( n == 22 && mc.AA != AosAttribute.NightSight )
									canadd = false;
								if ( n == 23 && mc.AAA != AosArmorAttribute.MageArmor )
									canadd = false;
								if ( n == 24 && mc.AWA != AosWeaponAttribute.HitLeechHits )
									canadd = false;
								if ( n == 25 && mc.AWA != AosWeaponAttribute.HitLeechStam )
									canadd = false;
								if ( n == 26 && mc.AWA != AosWeaponAttribute.HitLeechMana )
									canadd = false;
								if ( n == 27 && mc.AWA != AosWeaponAttribute.HitLowerAttack )
									canadd = false;
								if ( n == 28 && mc.AWA != AosWeaponAttribute.HitLowerDefend )
									canadd = false;
								if ( n == 29 && mc.AWA != AosWeaponAttribute.HitMagicArrow )
									canadd = false;
								if ( n == 30 && mc.AWA != AosWeaponAttribute.HitHarm )
									canadd = false;
								if ( n == 31 && mc.AWA != AosWeaponAttribute.HitFireball )
									canadd = false;
								if ( n == 32 && mc.AWA != AosWeaponAttribute.HitLightning )
									canadd = false;
								if ( n == 33 && mc.AWA != AosWeaponAttribute.HitDispel )
									canadd = false;
								if ( n == 34 && mc.AWA != AosWeaponAttribute.HitColdArea )
									canadd = false;
								if ( n == 35 && mc.AWA != AosWeaponAttribute.HitFireArea )
									canadd = false;
								if ( n == 36 && mc.AWA != AosWeaponAttribute.HitPoisonArea )
									canadd = false;
								if ( n == 37 && mc.AWA != AosWeaponAttribute.HitEnergyArea )
									canadd = false;
								if ( n == 38 && mc.AWA != AosWeaponAttribute.HitPhysicalArea )
									canadd = false;
								if ( n == 39 && mc.AWA != AosWeaponAttribute.UseBestSkill )
									canadd = false;
								if ( n == 40 && mc.AWA != AosWeaponAttribute.MageWeapon )
									canadd = false;
								if ( n == 41 )
								{
									canadd = false;
									if ( mc.AAA == AosArmorAttribute.LowerStatReq || mc.AWA == AosWeaponAttribute.LowerStatReq )
										canadd = true;
								}
								if ( n == 42 )
								{
									canadd = false;
									if ( mc.AAA == AosArmorAttribute.SelfRepair || mc.AWA == AosWeaponAttribute.SelfRepair )
										canadd = true;
								}
								if ( n == 43 )
								{
									canadd = false;
									if ( mc.AAA == AosArmorAttribute.DurabilityBonus || mc.AWA == AosWeaponAttribute.DurabilityBonus )
										canadd = true;
								}
							}
						}
					}

					if ( canadd )
						list.Add( mc );
				}

				from.SendGump( new MCSearchedForGump( m_Book, list ) );
			}
		}
	}
	public class MCSearchedForGump : Gump
	{
		private MCBook m_Book;
		private ArrayList m_List;
		private int m_Page;

		public MCSearchedForGump( MCBook book, ArrayList list ) : this( book, list, 1 )
		{
		}
		public MCSearchedForGump( MCBook book, ArrayList list, int page ) : base( 25, 50 )
		{
			m_Book = book;
			m_List = list;
			m_Page = page;

			AddPage( 0 );

			AddBackground( 0, 0, 700, 560, 0x2436 );

			int totalpages = (int)((list.Count/20)+( (list.Count % 20 == 0) ? 0 : 1 ));
			AddLabel( 450, 20, 1152, "Page "+page+"/"+totalpages );

			AddLabel( 30, 20, 1152, "Found Contracts" );

			AddButton( 20, 465, 2103, 2104, 0, GumpButtonType.Reply, 0 );
			AddLabel( 35, 462, 1152, "Close" );

			int index = (m_Page*20)-20;

			if ( list.Count > index+20 )
			{
				AddButton( 250, 450, 0x1196, 0x1196, 1, GumpButtonType.Reply, 0 );
				AddLabel( 250, 435, 1152, "Next page" ); 
			}
			if ( m_Page > 1 )
			{

				AddButton( 360, 450, 0x119a, 0x119a, 2, GumpButtonType.Reply, 0 );
				AddLabel( 360, 435, 1152, "Previous page" ); 
			}

			for ( int i = 0; i < 20 && index >= 0 && index < list.Count; ++i, ++index )
			{
				MC mc = (MC)list[index];

				if ( mc.Monster == null )
					mc.Monster = typeof( Mongbat );
				string s;
				if ( mc.Monster.DeclaringType == null )
					s = mc.Monster.Name;
				else
					s = mc.Monster.FullName;

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

						name = firsthalf+" "+firsthalf2+" "+secondhalf2+"s";
					}
					else
						name = firsthalf+" "+secondhalf+"s";
				}
				else
				{
					name = s+"s";
				}

				AddButton( 10, 45 + (i * 20), 2103, 2104, i+3, GumpButtonType.Reply, 0 );
				if ( mc.Completed )
				{
					if ( mc.CompletedBy == null )
						AddLabel( 25, 42 + (i * 20), 1152, "Attribute: "+mc.GetAttr()+", Monster: "+name+", To Kill: "+mc.AmountKilled+"/"+mc.AmountToKill+", Completed" );
					else
						AddLabel( 25, 42 + (i * 20), 1152, "Attribute: "+mc.GetAttr()+", Monster: "+name+", To Kill: "+mc.AmountKilled+"/"+mc.AmountToKill+", Completed By: "+mc.CompletedBy.Name );
				}
				else
					AddLabel( 25, 42 + (i * 20), 1152, "Attribute: "+mc.GetAttr()+", Monster: "+name+", To Kill: "+mc.AmountKilled+"/"+mc.AmountToKill );
			}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

			if ( info.ButtonID == 0 )
			{
				from.SendMessage( "Back." );
				from.SendGump( new MCSearchGump( m_Book ) );
			}
			if ( info.ButtonID == 1 )
			{
				from.SendGump( new MCSearchedForGump( m_Book, m_List, m_Page+1 ) );
			}
			if ( info.ButtonID == 2 )
			{
				from.SendGump( new MCSearchedForGump( m_Book, m_List, m_Page-1 ) );
			}
			if ( info.ButtonID >= 3 )
			{
				int index = (m_Page*20)-20;
				if ( m_List.Count >= info.ButtonID-3 )
				{
					if ( (index+(info.ButtonID-3)) < m_List.Count )
					{
						MC mc = (MC)m_List[(index+(info.ButtonID-3))];
						from.SendGump( new MCIndGump( from, m_Book, mc ) );
					}
				}
			}
		}
	}
} 
