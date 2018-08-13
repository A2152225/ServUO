using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Server;
//using daat99;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;
using Server.Items;
using Server.Multis;
using Server.Gumps;
using Server.Engines.PartySystem;
using Server.Commands;

namespace Server.Scripts.Commands
{
	public class PlayerCommands
	{
		public static void Initialize()
		{
			CommandSystem.Register( "BandSelf", AccessLevel.Player, new CommandEventHandler( BandSelf_OnCommand ) );
			CommandSystem.Register( "Band", AccessLevel.Player, new CommandEventHandler( Band_OnCommand ) );
			CommandSystem.Register( "CleanUp", AccessLevel.Player, new CommandEventHandler( CleanUp_OnCommand ) );
		}

		[Usage( "BandSelf" )]
		[Description( "Heals yourself with bandages." )]
		public static void BandSelf_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;

			if ( from.Backpack == null )
				return;

			Item[] items = from.Backpack.FindItemsByType( typeof( Bandage ) );

			Bandage b = null;

			for( int i = 0; i < items.Length; ++i )
			{
				Item item = items[i];

				if ( item is Bandage )
				{
					b = (Bandage)item;
				}
			}
			if ( b != null && BandageContext.GetContext( from ) == null && BandageContext.BeginHeal( from, from ) != null )
			{
				b.Consume();
				from.RevealingAction();
			}
		}
		[Usage( "Band <target name>" )]
		[Description( "Heals anyone within a radius of 1 that has the typed in name." )]
		public static void Band_OnCommand( CommandEventArgs e )
		{
			try
			{
				Mobile from = e.Mobile;

				if ( from.Backpack == null )
					return;

				if ( e.Length >= 1 )
				{
					string name = null;

					for( int i = 0; i < e.Length; i++ )
					{
						string str = e.GetString( i );

						if ( name == null || name == "" )
							name = str;
						else
							name += " "+ str;
					}

					if ( name == null )
						return;

					Item[] items = from.Backpack.FindItemsByType( typeof( Bandage ) );

					Bandage b = null;
					Mobile target = null;

					for( int i = 0; i < items.Length; ++i )
					{
						Item item = items[i];

						if ( item is Bandage )
						{
							b = (Bandage)item;
						}
					}
					IPooledEnumerable eable = from.Map.GetMobilesInRange( from.Location, Core.AOS ? 2 : 1 );

					foreach ( Mobile m in eable )
					{
						if ( name.ToLower() == m.Name.ToLower() && !m.Hidden )
							target = m;
					}

					eable.Free();

					if ( target == null )
						from.SendMessage( "There was no one in range that had that name." );
					else if ( b != null && BandageContext.GetContext( from ) == null && BandageContext.BeginHeal( from, target ) != null )
					{
						b.Consume();
						from.RevealingAction();
					}
				}
				else
					from.SendMessage( "Format: Band <target name>" );
			}

			catch ( Exception f )
			{
				Console.WriteLine( "[Band OnCommand Crash Prevented: "+f );
			}
		}

		[Usage( "CleanUp" )]
		[Description( "Cleans up the area around you of certain items and redeems corpses." )]
		public static void CleanUp_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;

			try{
			if ( from.Backpack == null || from.Region.Name == "Jail" || from.Region.Name == "EventsArea" || !from.Alive ) //edit Breaker - Prevent Dead from using cleanup
				return;

			IPooledEnumerable eable = from.Map.GetObjectsInRange( from.Location, 18 );
			ArrayList toCollect = new ArrayList();

			foreach ( object o in eable )
			{
				if ( o is Item )
					toCollect.Add( o );
			}
			eable.Free();

			int tokens = 0;
			int gold = 0;
			int corpses = 0;

			ArrayList toDelete = new ArrayList();
			for( int i = 0; i < toCollect.Count; ++i )
			{
				object obj = toCollect[i];

				if ( CleanUp.LootItem( (Item)obj, from ) && ((Item)obj).Movable )
				{
					CUPref p = CleanUp.FindPref( from );

					CUItem cui = p.GetCUI( (Item)obj );

					if ( cui == null || cui.GoTo == null || !cui.GoTo.IsChildOf( from ) || !cui.GoTo.TryDropItem( from, (Item)obj, false ) )
						from.AddToBackpack( (Item)obj );
				}
				//else if ( obj is Corpse && ((Corpse)obj).Owner != null && !((Corpse)obj).Owner.Player && (((Corpse)obj).Killer == null || ((Corpse)obj).Killer == from || (((Corpse)obj).Killer is BaseCreature && (((BaseCreature)((Corpse)obj).Killer).ControlMaster == from || ((BaseCreature)((Corpse)obj).Killer).SummonMaster == from)) ) )
				else if ( obj is Corpse )
				{
					Corpse c = (Corpse)obj;

					if ( c.Owner != null && c.Owner.Player )
						continue;

					BaseCreature m = (BaseCreature)c.Owner;

					if ( m == null )
					{
						toDelete.Add( c );
						corpses++;
						continue;
					}

					bool canloot = false;
					List<DamageStore> rights = m.GetLootingRights();//( m.DamageEntries, m.HitsMax );

					for ( int k = rights.Count - 1; k >= 0; --k )
					{
						DamageStore ds = (DamageStore)rights[k];

						if ( ds.m_HasRight && ds.m_Mobile == from )
							canloot = true;
					}

					if ( c.Killer == null || c.Killer == from || (c.Killer is BaseCreature && (((BaseCreature)c.Killer).ControlMaster == from || ((BaseCreature)c.Killer).SummonMaster == from)) )
						canloot = true;

					Party pty = from.Party as Party;

					if ( pty != null )
					{
						for ( int j = 0; j < pty.Members.Count; ++j )
						{
							PartyMemberInfo pmi = (PartyMemberInfo)pty.Members[j];
							if ( pmi != null )
							{
								Mobile member = pmi.Mobile;

								if ( member != null )
								{
									for ( int k = rights.Count - 1; k >= 0; --k )
									{
										DamageStore ds = (DamageStore)rights[k];

										if ( ds.m_HasRight && ds.m_Mobile == member )
											canloot = true;
									}
									if ( c.Killer == null || c.Killer == member || (c.Killer is BaseCreature && (((BaseCreature)c.Killer).ControlMaster == member || ((BaseCreature)c.Killer).SummonMaster == member)) )
										canloot = true;
								}
							}
						}
					}

					if ( !canloot )
						continue;

					CUPref cup = CleanUp.FindPref( from );
					if ( cup.CollectMCCorpses )
					{
						Item[] mcbooks = from.Backpack.FindItemsByType( typeof( MCBook ) );
						Item[] mcs = from.Backpack.FindItemsByType( typeof( MonsterContract ) );
						bool done = false;
						for( int j = 0; j < mcbooks.Length; j++ )
						{
							if ( done )
								continue;
							Item item = mcbooks[j];

							if ( item is MCBook )
							{
								MCBook book = (MCBook)item;

								for( int k = 0; k < book.Entries.Count; k++ )
								{
									if ( done )
										continue;

									MC mc = (MC)book.Entries[k];

									if ( mc.Completed || mc.AmountKilled >= mc.AmountToKill )
										continue;

									if ( mc.Monster == c.Owner.GetType() && !c.Channeled )
									{
										done = true;
										mc.AmountKilled += 1;
										c.Channeled = true;
										c.Hue = 15;

										if ( mc.AmountKilled >= mc.AmountToKill )
											mc.CompletedBy = from;
									}
								}
							}
						}
						for( int j = 0; j < mcs.Length; j++ )
						{
							if ( done )
								continue;
							Item item = mcs[j];

							if ( item is MonsterContract )
							{
								MonsterContract mc = (MonsterContract)item;

								if ( mc.Completed || mc.AmountKilled >= mc.AmountToKill )
									continue;

								if ( mc.Monster == c.Owner.GetType() )
								{
									done = true;
									mc.AmountKilled += 1;
									c.Channeled = true;
									c.Hue = 15;

									if ( mc.AmountKilled >= mc.AmountToKill )
										mc.CompletedBy = from;
								}
							}
						}
					}

					ArrayList list = new ArrayList( c.Items );

					for( int j = 0; j < list.Count; ++j )
					{
						Item item = (Item)list[j];

						if ( CleanUp.LootItem( item, from ) && item.Movable )
						{
							CUPref p = CleanUp.FindPref( from );

							CUItem cui = p.GetCUI( item );

							if ( cui == null || cui.GoTo == null || !cui.GoTo.IsChildOf( from ) || !cui.GoTo.TryDropItem( from, item, false ) )
								from.AddToBackpack( item );
						}
					}

					int karma;

					if ( m.Karma <= 0 )
						karma = m.Karma * -1;
					else
						karma = m.Karma;

					double monsterstats = (double)( ( ( m.Fame + karma ) / 5 ) + ( m.RawStatTotal * 3 ) + ( ( m.HitsMaxSeed + m.StamMaxSeed + m.ManaMaxSeed ) * 3 ) + ( m.DamageMax - m.DamageMin ) + ( ( m.ColdResistSeed + m.FireResistSeed + m.EnergyResistSeed + m.PhysicalResistanceSeed + m.PoisonResistSeed ) * 5 ) + m.VirtualArmor);

					double ContractChance = 0.02; //0.003 - removed leveling - increase drop chance
					if ( m.IsParagon )
						ContractChance = 0.05;  // ^^ .1  ^^

					if ( ContractChance >= Utility.RandomDouble() )
					{
						Item item = null;
						switch( Utility.Random(1) )
						{
							case 0:item = new MonsterContract();break;
						}

						if ( CleanUp.LootItem( item, from ) && item.Movable )
						{
							CUPref p = CleanUp.FindPref( from );

							CUItem cui = p.GetCUI( item );

							if ( cui == null || cui.GoTo == null || !cui.GoTo.IsChildOf( from ) || !cui.GoTo.TryDropItem( from, item, false ) )
								from.AddToBackpack( item );
						}
						else
							from.AddToBackpack( item );
					}

					tokens += (int)(monsterstats*0.0004);
					gold += (int)(monsterstats*0.0009);
					
					if (tokens < 5)
					tokens = 5;
					if (gold < 5)
					gold = 5;
					
					toDelete.Add( c );
					corpses++;
				}
			}
			for( int j = 0; j < toDelete.Count; ++j )
			{
				Item td = (Item)toDelete[j];
				td.Delete();
			}
			if ( corpses > 0 )
				from.SendMessage( "You redeem "+(corpses > 1 ? corpses.ToString() : "a")+" corpse"+(corpses > 1 ? "s" : "")+" and recieve "+tokens+" tokens and "+gold+" gold." );

			int left = tokens;

				while ( left > 0)
			{
			//Daat99Tokens t = new Daat99Tokens();
			Gold t = new Gold();
			
			
				if ( left >= 60000 )
					t.Amount = 60000;
				else
					t.Amount = left;

				if ( CleanUp.LootItem( t, from ) && t.Movable )
				{
					CUPref p = CleanUp.FindPref( from );

					CUItem cui = p.GetCUI( t );

					if ( cui == null || cui.GoTo == null || !cui.GoTo.IsChildOf( from ) || !cui.GoTo.TryDropItem( from, t, false ) )
						from.AddToBackpack( t );
				}
				else
					from.AddToBackpack( t );

				left -= 60000;
			}
			
			
			left = gold;

			while ( left > 0)
			{
				Gold g = new Gold();

				if ( left >= 60000 )
					g.Amount = 60000;
				else
					g.Amount = left;

				if ( CleanUp.LootItem( g, from ) && g.Movable )
				{
					CUPref p = CleanUp.FindPref( from );

					CUItem cui = p.GetCUI( g );

					if ( cui == null || cui.GoTo == null || !cui.GoTo.IsChildOf( from ) || !cui.GoTo.TryDropItem( from, g, false ) )
						from.AddToBackpack( g );
				}
				else
					from.AddToBackpack( g );

				left -= 60000;
			}
			}catch{}
		}
	}
	public class CUItem
	{
		private Type m_Item;
		private Container m_GoTo;

		public Type Item{ get{ return m_Item; } set{ m_Item = value; } }
		public Container GoTo{ get{ return m_GoTo; } set{ m_GoTo = value; } }

		public CUItem()
		{
		}

		public void Save( XmlTextWriter xml )
		{
			bool nn = GoTo != null;
			xml.WriteAttributeString( "notnull", nn.ToString() );
			if ( nn )
			{
				int gt = GoTo.Serial.Value;
				xml.WriteAttributeString( "cont", gt.ToString() );
			}

			string name = "";

			nn = Item != null;
			xml.WriteAttributeString( "inotnull", nn.ToString() );
			if ( Item != null )
			{
				if ( Item.DeclaringType == null )
					name = Item.Name;
				else
					name = Item.FullName;
				xml.WriteAttributeString( "type", name );
			}
		}

		public void Load( XmlElement item )
		{
			bool nn = XmlConvert.ToBoolean( item.Attributes["notnull"].Value.ToLower() );

			if ( nn )
				GoTo = (Container)World.FindItem( XmlConvert.ToInt32( item.Attributes["cont"].Value ) );

			nn = XmlConvert.ToBoolean( item.Attributes["inotnull"].Value.ToLower() );
			if ( nn )
			{
				string name = item.Attributes["type"].Value;

				Type type = ScriptCompiler.FindTypeByName( name );

				Item = type;
			}
		}
	}
	public class CUPref
	{
		private ArrayList m_Items = new ArrayList();
		private Mobile m_Owner;
		private bool m_CollectMCCorpses;

		public ArrayList Items{ get{ return m_Items; } set{ m_Items = value; } }
		public Mobile Owner{ get{ return m_Owner; } set{ m_Owner = value; } }
		public bool CollectMCCorpses{ get{ return m_CollectMCCorpses; } set{ m_CollectMCCorpses = value; } }

		public CUPref()
		{
			CleanUp.CUPs.Add( this );
		}
		public CUPref( Mobile from )
		{
			Owner = from;
			CleanUp.CUPs.Add( this );
		}
		public void Add( CUItem cui )
		{
			Items.Add( cui );
		}
		public void Add( Type type )
		{
			CUItem cui = new CUItem();
			cui.Item = type;
			Items.Add( cui );
		}
		public void Remove( int at )
		{
			Items.RemoveAt( at );
		}
		public void Remove( CUItem cui )
		{
			Items.Remove( cui );
		}
		public void Defrag()
		{
			for( int i = 0; i < Items.Count; i++ )
			{
				CUItem cui = (CUItem)Items[i];

				if ( cui.Item == null )
					Items.Remove( cui );
			}
		}
		public CUItem GetCUI( Item item )
		{
			CUItem gcui = null;
			for ( int i = 0; i < Items.Count; i++ )
			{
				CUItem cui = (CUItem)Items[i];

				if ( cui.Item == item.GetType() || item.GetType().IsSubclassOf( cui.Item ) )
					gcui = cui;
			}
			return gcui;
		}

		public void Save( XmlTextWriter xml )
		{
			xml.WriteStartElement( "cup" );

			bool nn = Owner != null;
			xml.WriteAttributeString( "notnull", nn.ToString() );
			if ( nn )
			{
				int owner = Owner.Serial.Value;
				xml.WriteAttributeString( "owner", owner.ToString() );
			}
			xml.WriteAttributeString( "cmcc", CollectMCCorpses.ToString() );

			xml.WriteStartElement( "items" );

			for ( int i = 0; i < Items.Count; ++i )
			{
				xml.WriteStartElement( "item" );

				CUItem cui = (CUItem)Items[i];
				cui.Save( xml );

				xml.WriteEndElement();
			}

			xml.WriteEndElement();

			xml.WriteEndElement();
		}

		public void Load( XmlElement cup )
		{
			bool nn = XmlConvert.ToBoolean( cup.Attributes["notnull"].Value.ToLower() );

			if ( nn )
				Owner = World.FindMobile( XmlConvert.ToInt32( cup.Attributes["owner"].Value ) );

			CollectMCCorpses = XmlConvert.ToBoolean( cup.Attributes["cmcc"].Value.ToLower() );

			XmlElement items = cup["items"];

			if ( items != null )
			{
				foreach ( XmlElement item in items.GetElementsByTagName( "item" ) )
				{
					CUItem cui = new CUItem();
					cui.Load( item );
					Items.Add( cui );
				}
			}
		}
	}
	public class CleanUp
	{
		public static void Initialize() 
		{
			CommandSystem.Register( "EditCleanUp", AccessLevel.Player, new CommandEventHandler( EditCleanUp_OnCommand ) ); 
		}

		[Usage( "EditCleanUp" )]
		[Description( "Change settings for your cleanup list." )]
		private static void EditCleanUp_OnCommand( CommandEventArgs e ) 
		{
			Mobile from = e.Mobile;

			from.SendGump( new CleanUpGump( from ) );
		}

		private static ArrayList m_CUPs = new ArrayList();
		public static ArrayList CUPs{ get{ return m_CUPs; } set{ m_CUPs = value; } }

		public static void Configure()
		{
			EventSink.WorldLoad += new WorldLoadEventHandler( OnLoad );
			EventSink.WorldSave += new WorldSaveEventHandler( OnSave );
		}
		public static bool LootItem( Item item, Mobile owner )
		{
			CUPref p = FindPref( owner );

			bool loot = false;

			for( int i = 0; i < p.Items.Count; ++i )
			{
				CUItem cui = (CUItem)p.Items[i];

				if ( (item.GetType() == cui.Item || item.GetType().IsSubclassOf( cui.Item )) && !loot )
					loot = true;
			}

			return loot;
		}
		public static CUPref FindPref( Mobile owner )
		{
			CUPref pref = null;

			for( int i = 0; i < CUPs.Count; ++i )
			{
				CUPref p = (CUPref)CUPs[i];

				if ( p.Owner == owner )
					pref = p;
			}

			if ( pref == null )
				pref = new CUPref( owner );

			if ( pref != null )
				pref.Defrag();

			return pref;
		}
		private static void OnSave( WorldSaveEventArgs e )
		{
			if ( !Directory.Exists( "Saves/CleanUp" ) )
				Directory.CreateDirectory( "Saves/CleanUp" );

			string filePath = Path.Combine( "Saves/CleanUp", "cleanup.xml" );

			using ( StreamWriter op = new StreamWriter( filePath ) )
			{
				XmlTextWriter xml = new XmlTextWriter( op );

				xml.Formatting = Formatting.Indented;
				xml.IndentChar = '\t';
				xml.Indentation = 1;

				xml.WriteStartDocument( true );

				xml.WriteStartElement( "cleanup" );

				xml.WriteStartElement( "cups" );

				for( int i = 0; i < CUPs.Count; ++i )
				{
					CUPref p = (CUPref)CUPs[i];

					p.Save( xml );
				}

				xml.WriteEndElement();

				xml.WriteEndElement();

				xml.Close();
			}
		}

		private static void OnLoad()
		{
			string filePath = Path.Combine( "Saves/CleanUp", "cleanup.xml" );

			if ( !File.Exists( filePath ) )
				return;

			try{

			XmlDocument doc = new XmlDocument();
			doc.Load( filePath );

			XmlElement node = doc["cleanup"];

			XmlElement innernode = node["cups"];

			if ( innernode != null )
			{
				foreach ( XmlElement cup in innernode.GetElementsByTagName( "cup" ) )
				{
					CUPref p = new CUPref();
					p.Load( cup );
				}
			}

			}catch{ Console.WriteLine( "Your '/Saves/CleanUp/cleanup.xml' file may be corrupted, as it was unable to load properly." ); }
		}
	}
	public class CleanUpGump : Gump
	{
                public CleanUpGump( Mobile from ) : base( 125, 125 )
                {
			CUPref p = CleanUp.FindPref( from );

			AddPage( 0 ); 

			AddBackground( 0, 0, 200, 150, 5054 );

			AddLabel( 60, 10, 1152, "Clean Up Gump" );
			AddButton( 10, 30, 4005, 4007, 1, GumpButtonType.Reply, 0 ); 
			AddLabel( 40, 30, 1152, "Add Item" );
			AddButton( 10, 50, 4005, 4007, 2, GumpButtonType.Reply, 0 ); 
			AddLabel( 40, 50, 1152, "Edit Item" );
			AddButton( 10, 70, 4005, 4007, 3, GumpButtonType.Reply, 0 ); 
			AddLabel( 40, 70, 1152, "Remove Item" );

			AddButton( 10, 90, p.CollectMCCorpses ? 0x2343 : 0x2342, p.CollectMCCorpses ? 0x2342: 0x2343, 4, GumpButtonType.Reply, 0 ); 
			AddLabel( 33, 90, 1152, "Collect Contract Corpses" );

			AddButton( 10, 120, 4005, 4007, 0, GumpButtonType.Reply, 0 ); 
			AddLabel( 40, 120, 33, "Close" );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{ 
			Mobile from = state.Mobile;

			if ( info.ButtonID == 0 )
			{
				from.SendMessage( "Closed." );
			}
			if ( info.ButtonID == 1 )
			{
				from.Target = new InternalTarget();
			}
			if ( info.ButtonID == 2 )
			{
				CUPref p = CleanUp.FindPref( from );

				from.SendGump( new CleanUpEditItemGump( p.Items ) );
			}
			if ( info.ButtonID == 3 )
			{
				CUPref p = CleanUp.FindPref( from );

				from.SendGump( new CleanUpRemoveItemGump( p.Items ) );
			}
			if ( info.ButtonID == 4 )
			{
				CUPref p = CleanUp.FindPref( from );
				if ( p.CollectMCCorpses )
					p.CollectMCCorpses = false;
				else
					p.CollectMCCorpses = true;

				from.SendGump( new CleanUpGump( from ) );
			}
		}
  		public class InternalTarget : Target
		{
			public InternalTarget() : base( -1, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object target )
			{
				if ( target is Item )
				{
					Type type = ((Item)target).GetType();
					string name;

					if ( type.DeclaringType == null )
						name = type.Name;
					else
						name = type.FullName;

					if ( ScriptCompiler.FindTypeByName( name ) != null )
					{
						CUPref p = CleanUp.FindPref( from );

						if ( p == null )
							p = new CUPref( from );

						p.Add( type );

						from.SendGump( new CleanUpGump( from ) );
					}
					else
						from.SendMessage( "Invalid Target." );
				}
				else
					from.SendMessage( "Invalid Target." );
			}
		}
	}
	public class CleanUpRemoveItemGump : Gump
	{
		private ArrayList m_List;

		private struct RemoveInfo 
		{ 
			public CUItem At; 
			public bool toRemove; 

			public RemoveInfo( CUItem at, bool remove ) 
			{ 
				At = at; 
				toRemove = remove; 
			} 
			public RemoveInfo( CUItem at ) 
			{ 
				At = at; 
				toRemove = false; 
			} 
		}
		private RemoveInfo[] m_RemoveList; 

                public CleanUpRemoveItemGump( ArrayList list ) : base( 25, 25 )
                {
			m_RemoveList = new RemoveInfo[list.Count]; 
			for ( int i = 0; i < list.Count;i++ ) 
			{ 
				m_RemoveList[i] = new RemoveInfo( (CUItem)list[i] ); 
			} 

			m_List = list;

			AddPage( 0 );

			AddBackground( 25, 10, 220, 490, 5054 );

			AddImageTiled( 33, 20, 200, 470, 2624 );
			AddAlphaRegion( 33, 20, 200, 470 );

			AddLabel( 40, 40, 1152, "CleanUp Types" );

			AddButton( 170, 380, 4005, 4007, 0, GumpButtonType.Reply, 0 );
			AddLabel( 170, 363, 1152, "Back" );
			AddButton( 170, 433, 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddLabel( 170, 403, 1152, "Remove" );
			AddLabel( 170, 418, 1152, "Selected" );

			for ( int i = 0; i < list.Count; ++i )
			{
				if ( (i % 20) == 0 )
				{
					if ( i != 0 )
					{
						AddButton( 170, 315, 0x1196, 0x1196, 1152, GumpButtonType.Page, (i / 20) + 1 );
						AddLabel( 170, 300, 1152, "Next page" ); 
					}

					AddPage( (i / 20) + 1 );

					if ( i != 0 )
					{
						AddButton( 170, 275, 0x119a, 0x119a, 1152, GumpButtonType.Page, (i / 20) );
						AddLabel( 170, 260, 1152, "Previous page" ); 
					}
				}

				CUItem cui = (CUItem)list[i];

				string name = "";

				if ( cui.Item.DeclaringType == null )
					name = cui.Item.Name;
				else
					name = cui.Item.FullName;

				AddLabel( 70, 60 + ((i % 20) * 20), 5, ""+ name );
				AddCheck( 40, 60 + ((i % 20) * 20), 0x2342, 0x2343, m_RemoveList[i].toRemove, i );
			}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

			for ( int i=0; i < m_List.Count; i++ )
			{
				if ( i >= m_RemoveList.Length ) break; 
					m_RemoveList[i].toRemove = info.IsSwitched( i ); 
			}

			if ( info.ButtonID == 0 )
			{
				from.SendGump( new CleanUpGump( from ) );
			}
			if ( info.ButtonID == 1 )
			{
				int removed = 0;
				for ( int i = 0; i < m_RemoveList.Length; i++ ) 
					if ( m_RemoveList[i].toRemove ) 
					{ 
						CUPref p = CleanUp.FindPref( from );

						p.Remove( m_RemoveList[i].At );

						removed += 1;
					}
				from.SendMessage( "{0} items have been removed.", removed );
				from.SendGump( new CleanUpRemoveItemGump( m_List ) );
			}
		}
	}
	public class CleanUpEditItemGump : Gump
	{
		private ArrayList m_List;

                public CleanUpEditItemGump( ArrayList list ) : base( 25, 25 )
                {
			m_List = list;

			AddPage( 0 );

			AddBackground( 25, 10, 220, 490, 5054 );

			AddImageTiled( 33, 20, 200, 470, 2624 );
			AddAlphaRegion( 33, 20, 200, 470 );

			AddLabel( 40, 40, 1152, "CleanUp Types" );

			AddButton( 170, 380, 4005, 4007, 0, GumpButtonType.Reply, 0 );
			AddLabel( 170, 363, 1152, "Back" );

			for ( int i = 0; i < list.Count; ++i )
			{
				if ( (i % 20) == 0 )
				{
					if ( i != 0 )
					{
						AddButton( 170, 315, 0x1196, 0x1196, 1152, GumpButtonType.Page, (i / 20) + 1 );
						AddLabel( 170, 300, 1152, "Next page" ); 
					}

					AddPage( (i / 20) + 1 );

					if ( i != 0 )
					{
						AddButton( 170, 275, 0x119a, 0x119a, 1152, GumpButtonType.Page, (i / 20) );
						AddLabel( 170, 260, 1152, "Previous page" ); 
					}
				}

				CUItem cui = (CUItem)list[i];

				string name = "";

				if ( cui.Item.DeclaringType == null )
					name = cui.Item.Name;
				else
					name = cui.Item.FullName;

				AddLabel( 70, 60 + ((i % 20) * 20), 5, ""+ name );
				AddButton( 40, 60 + ((i % 20) * 20), 4005, 4007, i+1, GumpButtonType.Reply, 0 );
			}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

			if ( info.ButtonID == 0 )
			{
				from.SendGump( new CleanUpGump( from ) );
			}
			if ( info.ButtonID >= 1 )
			{
				if ( info.ButtonID-1 <= m_List.Count-1 )
				{
					CUItem cui = (CUItem)m_List[info.ButtonID-1];

					from.SendGump( new CleanUpEditIndItemGump( cui ) );
				}
			}
		}
	}
	public class CleanUpEditIndItemGump : Gump
	{
		private CUItem m_CUI;

                public CleanUpEditIndItemGump( CUItem cui ) : base( 125, 125 )
                {
			m_CUI = cui;

			AddPage( 0 ); 

			AddBackground( 0, 0, 225, 120, 5054 );

			AddLabel( 60, 10, 1152, "Edit" );

			string name = "";

			if ( cui.Item.DeclaringType == null )
				name = cui.Item.Name;
			else
				name = cui.Item.FullName;

			AddImageTiled( 85, 10, 75, 20, 0xBBC );
			AddTextEntry( 85, 10, 75, 20, 0x480, 0, name );

			AddButton( 95, 30, 0x15E1, 0x15E5, 1, GumpButtonType.Reply, 0 );
			AddLabel( 20, 30, 1152, "Target Item" );

			AddButton( 10, 50, cui.GoTo != null ? 0x2343 : 0x2342, cui.GoTo != null ? 0x2342: 0x2343, 2, GumpButtonType.Reply, 0 ); 
			AddLabel( 40, 50, 1152, "Container" );

			AddButton( 120, 70, 0x15E1, 0x15E5, 3, GumpButtonType.Reply, 0 );
			AddLabel( 65, 70, 1152, "Nullify" );

			AddButton( 10, 90, 4005, 4007, 0, GumpButtonType.Reply, 0 ); 
			AddLabel( 40, 90, 33, "Close" );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{ 
			Mobile from = state.Mobile;

			string name = "";

			string[] tr = new string[ 1 ];
			foreach( TextRelay t in info.TextEntries )
			{
				tr[ t.EntryID ] = t.Text;
			}

			try { name = tr[ 0 ]; } 
			catch {}

			Type type = ScriptCompiler.FindTypeByName( name );

			if ( type != null )
				m_CUI.Item = type;
			else
				from.SendMessage( "That was an invalid type, it was not changed." );

			if ( info.ButtonID == 0 )
			{
				from.SendMessage( "Closed." );
				from.SendGump( new CleanUpGump( from ) );
			}
			if ( info.ButtonID == 1 )
			{
				from.Target = new InternalTarget( m_CUI );
			}
			if ( info.ButtonID == 2 )
			{
				from.Target = new InternalTarget2( m_CUI );
			}
			if ( info.ButtonID == 3 )
			{
				m_CUI.GoTo = null;
				from.SendGump( new CleanUpEditIndItemGump( m_CUI ) );
				from.SendMessage( "These types of items will now go straight to your inventory." );
			}
		}
  		public class InternalTarget : Target
		{
			private CUItem cui;

			public InternalTarget( CUItem CUI ) : base( -1, false, TargetFlags.None )
			{
				cui = CUI;
			}

			protected override void OnTarget( Mobile from, object target )
			{
				if ( target is Item )
				{
					Type type = ((Item)target).GetType();

					cui.Item = type;
				}
				else
					from.SendMessage( "You must target an item." );
				from.SendGump( new CleanUpEditIndItemGump( cui ) );
			}
		}
  		public class InternalTarget2 : Target
		{
			private CUItem cui;

			public InternalTarget2( CUItem CUI ) : base( -1, false, TargetFlags.None )
			{
				cui = CUI;
			}

			protected override void OnTarget( Mobile from, object target )
			{
				if ( target is Container )
				{
					Container cont = (Container)target;

					cui.GoTo = cont;
				}
				else
					from.SendMessage( "You must target a container." );
				from.SendGump( new CleanUpEditIndItemGump( cui ) );
			}
		}
	}
}
