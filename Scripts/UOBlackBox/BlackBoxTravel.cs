 //UO Black Box - By GoldDraco13
//1.0.0.99

using Server.Mobiles;
using System;

namespace Server.UOBlackBox
{
    class BlackBoxTravel : Item
    {
        public string Owner;

        private Map old_Map;
        private Map new_Map = Map.Trammel;

        private Point3D old_Location;
        private Point3D new_Location = new Point3D(1602, 1591, 20);

        private bool IsSpecial = false;

        private DeleteTimer deleteTimer;
        private static int deleteDelay = 10;

        private QuickDeleteTimer q_DeleteTimer;
        private static int q_DeleteDelay = 50;

        private SoundTimer soundTimer;
        private static int soundDelay = 50;

        private MoveTimer moveTimer;
        private static int moveDelay = 50;

        private RevealTimer revealTimer;
        private static int revealDelay = 500;

        Random rnd = new Random();
        private int hue;

        [Constructable]
        public BlackBoxTravel(string owner)
        {
            Name = "Black Box Portal";
            Owner = owner;

            Movable = false;
            ItemID = 0xF6C;
            Light = LightType.Circle300;

            old_Map = Map;
            old_Location = new Point3D(X, Y, Z);

            StartDeleteGate(this);

            StartingSound(this);
        }

        [Constructable]
        public BlackBoxTravel(string owner, Map map, int x, int y, int z, string pass)
        {
            Name = "Black Box Portal";
            Owner = owner;

            Movable = false;
            ItemID = 0xF6C;
            Light = LightType.Circle300;

            hue = Hue = rnd.Next(1, 1001);

            old_Map = Map;
            old_Location = new Point3D(X, Y, Z);

            new_Map = map;
            new_Location = new Point3D(x, y, z);

            if (ProperCMD(pass))
            {
                StartDeleteGate(this);
            }
            else
            {
                QuickDelete(this);
            }
            StartingSound(this);
        }

        [Constructable]
        public BlackBoxTravel(string owner, Map map, int x, int y, int z, string pass, bool isSpecial)
        {
            Name = "Black Box SoulStone";
            Owner = owner;

            IsSpecial = isSpecial;

            Movable = false;
            ItemID = 0x423F;
            Light = LightType.Circle300;

            int RandomEffect = rnd.Next(10);

            if (RandomEffect <= 1) //Fire
                Hue = 1260;
            if (RandomEffect == 2) //Ice
                Hue = 1266;
            if (RandomEffect == 3) //Toxic
                Hue = 1272;
            if (RandomEffect == 4) //Electric
                Hue = 1283;
            if (RandomEffect == 5) //Mist
                Hue = 1288;
            if (RandomEffect == 6) //Explosion
                Hue = 1174;
            if (RandomEffect == 7) //Stone
                Hue = 1177;
            if (RandomEffect >= 8) //Shiney
                Hue = 1287;

            old_Map = Map;
            old_Location = new Point3D(X, Y, Z);

            new_Map = map;
            new_Location = new Point3D(x, y, z);

            if (ProperCMD(pass))
            {
                StartDeleteGate(this);
            }
            else
            {
                QuickDelete(this);
            }
            StartingSound(this);
        }

        private bool ProperCMD(string pass)
        {
            if (pass != "BBCMD72")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void StartDeleteGate(BlackBoxTravel gate)
        {
            if (gate != null)
            {
                deleteTimer = new DeleteTimer(gate);
                deleteTimer.Start();
            }
        }

        private void QuickDelete(BlackBoxTravel gate)
        {
            if (gate != null)
            {
                q_DeleteTimer = new QuickDeleteTimer(gate);
                q_DeleteTimer.Start();
            }
        }

        private void StartingSound(BlackBoxTravel gate)
        {
            if (gate != null)
            {
                soundTimer = new SoundTimer(gate);
                soundTimer.Start();
            }
        }

        public BlackBoxTravel(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile m)
        {
            PlayerMobile pm = m as PlayerMobile;

            if (pm == null)
                return;

            if (!IsSpecial)
            {
                if (m.InRange(GetWorldLocation(), 1))
                    OnMoveOver(m);
                else
                    m.SendLocalizedMessage(500446); // That is too far away.
            }
            else
            {
                OnMoveOver(m);
            }
        }

        public override bool OnMoveOver(Mobile m)
        {
            PlayerMobile pm = m as PlayerMobile;

            if (pm != null)
            {
                if (IsSpecial)
                {
                    pm.Hidden = true;

                    if (Hue == 1260) //Fire
                        Effects.SendLocationEffect(old_Location, old_Map, 0x3709, 30);
                    if (Hue == 1266) //Ice
                        Effects.SendLocationEffect(old_Location, old_Map, 0x37CC, 40);
                    if (Hue == 1272) //Toxic
                        Effects.SendLocationEffect(old_Location, old_Map, 0x374A, 17);
                    if (Hue == 1283) //Electric
                        Effects.SendBoltEffect(pm);
                    if (Hue == 1288) //Mist
                        Effects.SendLocationEffect(old_Location, old_Map, 0x3728, 13);
                    if (Hue == 1174) //Explosion
                        Effects.SendLocationEffect(old_Location, old_Map, 0x36BD, 10);
                    if (Hue == 1177) //Stone
                        Effects.SendLocationEffect(old_Location, old_Map, 0x37C4, 31);
                    if (Hue == 1287) //Shiny
                        Effects.SendLocationEffect(old_Location, old_Map, 0x375A, 30);

                    revealTimer = new RevealTimer(pm);
                    revealTimer.Start();
                }
                else
                {
                    Effects.SendLocationEffect(old_Location, old_Map, 0x3728, 13);
                }
                StartTravel(pm, this);
                    return true;
            }
            else
            {
                return false;
            }
        }

        private void StartTravel(PlayerMobile pm, BlackBoxTravel BBT)
        {
            if (new_Map == null || new_Map == Map.Internal)
                return;

            if (pm != null && BBT != null)
            {
                moveTimer = new MoveTimer(pm, BBT);
                moveTimer.Start();
            }
        }

        private void StartSound(BlackBoxTravel BBT)
        {
            foreach (Mobile player in World.Mobiles.Values)
            {
                PlayerMobile pm = player as PlayerMobile;

                if (pm != null && BBT != null)
                {
                    if (pm.Map == BBT.Map)
                    {
                        if (pm.X > (BBT.X - 20) && pm.X < (BBT.X + 20))
                        {
                            if (pm.Y > (BBT.Y - 20) && pm.Y < (BBT.Y + 20))
                            {
                                if (BBT.IsSpecial)
                                {
                                    if (Hue == 1260) //Fire
                                    {
                                        pm.PlaySound(855);
                                    }
                                    if (Hue == 1266) //Ice
                                    {
                                        pm.PlaySound(20);
                                    }
                                    if (Hue == 1272) //Toxic
                                    {
                                        pm.PlaySound(1140);
                                    }
                                    if (Hue == 1283) //Electric
                                    {
                                        pm.PlaySound(41);
                                        Effects.SendBoltEffect(pm);
                                    }
                                    if (Hue == 1288) //Mist
                                    {
                                        pm.PlaySound(252);
                                    }
                                    if (Hue == 1174) //Explosion
                                    {
                                        pm.PlaySound(519);
                                    }
                                    if (Hue == 1177) //Stone
                                    {
                                        pm.PlaySound(515);
                                    }
                                    if (Hue == 1287) //Shiny
                                    {
                                        pm.PlaySound(492);
                                    }
                                }
                                else
                                {
                                    pm.PlaySound(0x20F);
                                }
                            }
                        }
                    }
                }
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();
        }

        private class MoveTimer : Timer
        {
            private readonly PlayerMobile pm;
            private readonly BlackBoxTravel i_Gate;

            public MoveTimer(PlayerMobile player, BlackBoxTravel gate) : base(TimeSpan.FromMilliseconds(moveDelay))
            {
                pm = player;
                i_Gate = gate;
            }

            protected override void OnTick()
            {
                pm.MoveToWorld(i_Gate.new_Location, i_Gate.new_Map);

                if (i_Gate.IsSpecial)
                {
                    Point3D point3D = new Point3D(pm.X, pm.Y, pm.Z);

                    if (i_Gate.Hue == 1260) //Fire
                        Effects.SendLocationEffect(i_Gate.new_Location, i_Gate.new_Map, 0x3709, 30);
                    if (i_Gate.Hue == 1266) //Ice
                        Effects.SendLocationEffect(i_Gate.new_Location, i_Gate.new_Map, 0x37CC, 40);
                    if (i_Gate.Hue == 1272) //Toxic
                        Effects.SendLocationEffect(i_Gate.new_Location, i_Gate.new_Map, 0x374A, 17);
                    if (i_Gate.Hue == 1283) //Electric
                        Effects.SendBoltEffect(pm);
                    if (i_Gate.Hue == 1288) //Mist
                        Effects.SendLocationEffect(i_Gate.new_Location, i_Gate.new_Map, 0x3728, 13);
                    if (i_Gate.Hue == 1174) //Explosion
                        Effects.SendLocationEffect(i_Gate.new_Location, i_Gate.new_Map, 0x36BD, 10);
                    if (i_Gate.Hue == 1177) //Stone
                        Effects.SendLocationEffect(i_Gate.new_Location, i_Gate.new_Map, 0x37C4, 31);
                    if (i_Gate.Hue == 1287) //Shiny
                        Effects.SendLocationEffect(i_Gate.new_Location, i_Gate.new_Map, 0x375A, 30);
                }
                else
                {
                    BlackBoxTravelEnd i_BlackBoxEnd = new BlackBoxTravelEnd(i_Gate.hue, pm.Name);

                    i_BlackBoxEnd.MoveToWorld(i_Gate.new_Location, i_Gate.new_Map);
                    Effects.SendLocationEffect(i_Gate.new_Location, i_Gate.new_Map, 0x3728, 13);
                }
                i_Gate.StartSound(i_Gate);

                if (i_Gate.IsSpecial)
                {
                    i_Gate.Delete();
                }
                Stop();
            }
        }

        private class RevealTimer : Timer
        {
            private readonly PlayerMobile pm;

            public RevealTimer(PlayerMobile player) : base(TimeSpan.FromMilliseconds(revealDelay))
            {
                pm = player;
            }

            protected override void OnTick()
            {
                pm.Hidden = false;
                Stop();
            }
        }

        private class SoundTimer : Timer
        {
            private readonly BlackBoxTravel i_Gate;

            public SoundTimer(BlackBoxTravel gate) : base(TimeSpan.FromMilliseconds(soundDelay))
            {
                i_Gate = gate;
            }

            protected override void OnTick()
            {
                i_Gate.StartSound(i_Gate);
                Stop();
            }
        }

        private class QuickDeleteTimer : Timer
        {
            private readonly BlackBoxTravel i_Gate;

            public QuickDeleteTimer(BlackBoxTravel gate) : base(TimeSpan.FromMilliseconds(q_DeleteDelay))
            {
                i_Gate = gate;
            }

            protected override void OnTick()
            {
                i_Gate.Delete();
                Stop();
            }
        }

        private class DeleteTimer : Timer
        {
            private readonly BlackBoxTravel i_Gate;

            public DeleteTimer(BlackBoxTravel gate) : base(TimeSpan.FromSeconds(deleteDelay))
            {
                i_Gate = gate;
            }

            protected override void OnTick()
            {
                i_Gate.StartSound(i_Gate);
                i_Gate.Delete();
                Stop();
            }
        }
    }

    class BlackBoxTravelEnd : Item
    {
        public string Owner;

        private DeleteTimer deleteTimer;
        private static int deleteDelay = 10;

        private SoundTimer soundTimer;
        private static int soundDelay = 50;

        [Constructable]
        public BlackBoxTravelEnd(int hue, string owner)
        {
            Name = "Black Box Portal Exit";
            Owner = owner;

            Movable = false;
            ItemID = 0xF6C;
            Hue = hue;
            Light = LightType.Circle300;

            StartDeleteGate(this);

            StartingSound(this);
        }

        private void StartDeleteGate(BlackBoxTravelEnd gate)
        {
            if (gate != null)
            {
                deleteTimer = new DeleteTimer(gate);
                deleteTimer.Start();
            }
        }

        public BlackBoxTravelEnd(Serial serial) : base(serial)
        {
        }

        public override bool OnMoveOver(Mobile m)
        {
            return false;
        }

        private void StartingSound(BlackBoxTravelEnd gate)
        {
            if (gate != null)
            {
                soundTimer = new SoundTimer(gate);
                soundTimer.Start();
            }
        }

        private void StartSound(BlackBoxTravelEnd BBT)
        {
            foreach (Mobile player in World.Mobiles.Values)
            {
                PlayerMobile pm = player as PlayerMobile;

                if (pm != null && BBT != null)
                {
                    if (pm.Map == BBT.Map)
                    {
                        if (pm.X > (BBT.X - 20) && pm.X < (BBT.X + 20))
                        {
                            if (pm.Y > (BBT.Y - 20) && pm.Y < (BBT.Y + 20))
                            {
                                pm.PlaySound(0x20F);
                            }
                        }
                    }
                }
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();
        }

        private class SoundTimer : Timer
        {
            private readonly BlackBoxTravelEnd i_Gate;

            public SoundTimer(BlackBoxTravelEnd gate) : base(TimeSpan.FromMilliseconds(soundDelay))
            {
                i_Gate = gate;
            }

            protected override void OnTick()
            {
                i_Gate.StartSound(i_Gate);
                Stop();
            }
        }

        private class DeleteTimer : Timer
        {
            private readonly BlackBoxTravelEnd i_Gate;

            public DeleteTimer(BlackBoxTravelEnd gate) : base(TimeSpan.FromSeconds(deleteDelay))
            {
                i_Gate = gate;
            }

            protected override void OnTick()
            {
                i_Gate.StartSound(i_Gate);
                i_Gate.Delete();
                Stop();
            }
        }
    }
}
