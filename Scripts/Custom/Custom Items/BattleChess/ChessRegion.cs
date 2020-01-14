using System;
using System.Collections;

using Server;
using Server.Regions;

namespace Arya.Chess
{
	public class ChessRegion : Region
	{
		/// <summary>
		/// Specifies whether spectators should be allowed on the chessboard or not
		/// </summary>
		private bool m_AllowSpectators = false;
		/// <summary>
		/// The game that's being held on the chessboard
		/// </summary>
		private ChessGame m_Game;
		/// <summary>
		/// The bounds of the region
		/// </summary>
		private Rectangle2D m_Bounds;
		/// <summary>
		/// The bounds of the chessboard
		/// </summary>
		private Rectangle2D m_BoardBounds;
		/// <summary>
		/// The height of the chessboard
		/// </summary>
		private int m_Height;

		public ChessRegion( Map map, ChessGame game, bool allowSpectators, Rectangle2D bounds, int height ) : base( "Chssbrd", map, 100, new Rectangle3D(bounds.X-12, bounds.Y-12, sbyte.MinValue, bounds.Width+24, bounds.Height+24, sbyte.MaxValue - sbyte.MinValue ) )
		{
			m_Game = game;
			m_AllowSpectators = allowSpectators;
			m_BoardBounds = bounds;
			m_Height = height;
		}

		public override void OnLocationChanged(Mobile m, Point3D oldLocation)
		{
			if ( m_Game == null || ! m.Player || m_AllowSpectators || m_Game.IsPlayer( m ) )
				base.OnLocationChanged (m, oldLocation);
			else if ( m_BoardBounds.Contains( m.Location ) )
			{
				m.SendMessage( 0x40, "Spectators aren't allowed on the chessboard" );

				// Expel
				if ( ! m_BoardBounds.Contains( oldLocation as IPoint2D ) )
					m.Location = oldLocation;
				else
					m.Location = new Point3D( m_BoardBounds.X - 1, m_BoardBounds.Y - 1, m_Height );
			}
			else
				base.OnLocationChanged( m, oldLocation );
		}

		public override bool OnBeginSpellCast(Mobile m, ISpell s)
		{
			if ( s is Server.Spells.Sixth.InvisibilitySpell )
			{
				m.SendMessage( 0x40, "You can't cast that spell when you're close to a chessboard" );
				return false;
			}
			else
			{
				return base.OnBeginSpellCast (m, s);
			}
		}


		// Don't announce
		public override void OnEnter(Mobile m)
		{
		}

		public override void OnExit(Mobile m)
		{
		}

		public override bool AllowSpawn()
		{
			return false;
		}

		public override void OnSpeech(SpeechEventArgs args)
		{
			if ( m_Game != null && m_Game.IsPlayer( args.Mobile ) && m_Game.AllowTarget )
			{
				if ( args.Speech.ToLower().IndexOf( ChessConfig.ResetKeyword.ToLower() ) > -1 )
					m_Game.SendAllGumps( null, null );				
			}

			base.OnSpeech( args );
		}

		public override void OnRegister()
		{
			base.OnRegister();

			if ( m_Game != null && ! m_AllowSpectators )
			{
				IPooledEnumerable en = Map.GetMobilesInBounds( m_BoardBounds );
				ArrayList expel = new ArrayList();

				try
				{
					foreach( Mobile m in en )
					{
						if ( m.Player && ! m_Game.IsPlayer( m ) )
						{
							expel.Add( m );
						}
					}
				}
				finally
				{
					en.Free();
				}

				foreach( Mobile m in expel )
				{
					m.SendMessage( 0x40, "Spectators aren't allowed on the chessboard" );
					m.Location = new Point3D( m_BoardBounds.X - 1, m_BoardBounds.Y - 1, m_Height );
				}
			}
		}
	}
}
