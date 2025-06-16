using Server;
using Server.Mobiles;
using System.Collections.Generic;
using System.Linq;


namespace Scripts.Mythik.Systems.Achievements
{
    public class AchievementSystemMemoryStone : Item
    {
        public static AchievementSystemMemoryStone GetInstance()
        {
            if (m_instance == null)
            {
                m_instance = new AchievementSystemMemoryStone();
                m_instance.MoveToWorld(new Point3D(0, 0, 0), Map.Felucca);
            }

            return m_instance;
        }
        internal Dictionary<Serial, Dictionary<int, AchieveData>> Achievements = new Dictionary<Serial, Dictionary<int, AchieveData>>();
        private Dictionary<Serial, int> m_PointsTotal = new Dictionary<Serial, int>();
		
		//private	Dictionary<Serial, Dictionary<int, string>> m_Tally = new Dictionary<Serial, Dictionary<int, string>>();
				
        private static AchievementSystemMemoryStone m_instance;

        public int GetPlayerPointsTotal(PlayerMobile m)
        {
            if (!m_PointsTotal.ContainsKey(m.Serial))
                m_PointsTotal.Add(m.Serial, 0);
            return m_PointsTotal[m.Serial];
        }
        public void AddPoints(PlayerMobile m, int points)
        {
            if (!m_PointsTotal.ContainsKey(m.Serial))
                m_PointsTotal.Add(m.Serial, 0);
            m_PointsTotal[m.Serial] += points;
        }
		
		
	/*	public int GetTallyTotal(PlayerMobile m )
        {
            if (!m_Tally.ContainsKey(m.Serial))
                m_Tally.Add(m.Serial, 0, "0" );
            return m_Tally[m.Serial];
        }
        public void AddTally(PlayerMobile m, int points)
        {
            if (!m_Tally.ContainsKey(m.Serial))
                m_Tally.Add(m.Serial, 0);
            m_Tally[m.Serial] += points;
        }
		

		
		*/
		
        [Constructable]
        public AchievementSystemMemoryStone() : base(0xED4)
        {
            Visible = false;
            Name = "AchievementSystemStone DO NOT REMOVE";
            m_instance = this;
        }

        public AchievementSystemMemoryStone(Serial serial) : base(serial)
        {
            m_instance = this;
            
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version 
			/*
			  writer.Write(m_Tally.Count);
            foreach (var kv in m_Tally)
            {
                writer.Write(kv.Key);
                writer.Write(kv.Value);
				 foreach (var ac in kv.Value)
                {
                    writer.Write(ac.Key);
                    ac.Value.Serialize(writer);
                }
            }
*/
            writer.Write(m_PointsTotal.Count);
            foreach (var kv in m_PointsTotal)
            {
                writer.Write(kv.Key);
                writer.Write(kv.Value);
            }

            writer.Write(Achievements.Count);
            foreach (var kv in Achievements)
            {
                writer.Write(kv.Key);
                writer.Write(kv.Value.Count);
                foreach (var ac in kv.Value)
                {
                    writer.Write(ac.Key);
                    ac.Value.Serialize(writer);
                }
            }
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
			/*r//*
			
			for (int i = 0; i < count; i++)
                {
					 int count = reader.ReadInt();
                    Serial id = reader.ReadInt();
                    var dict = new Dictionary<string, int>();
                    int iCount = reader.ReadInt();
                    if (iCount > 0)
                    {
                        for (int x = 0; x < iCount; x++)
                        {
                            dict.Add(reader.ReadInt(), new string(reader));
                        }

                    }
                    m_Tally.Add(id, dict);
                }
			
						
			
			*////
            int count = reader.ReadInt();
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    m_PointsTotal.Add(reader.ReadInt(), reader.ReadInt());
                }
            }

            count = reader.ReadInt();
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Serial id = reader.ReadInt();
                    var dict = new Dictionary<int, AchieveData>();
                    int iCount = reader.ReadInt();
                    if (iCount > 0)
                    {
                        for (int x = 0; x < iCount; x++)
                        {
                            dict.Add(reader.ReadInt(), new AchieveData(reader));
                        }

                    }
                    Achievements.Add(id, dict);
                }
            }
            System.Console.WriteLine("Loaded Achievement store: " + m_PointsTotal.Count);
            m_instance = this;
        }
    }
}


