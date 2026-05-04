using Server.Items;

namespace Server.Engines.CannedEvil
{
    public sealed class PowerCrystalChampEntry
    {
        public string Name { get; private set; }
        public int Hue { get; private set; }

        public PowerCrystalChampEntry(string name, int hue)
        {
            Name = name;
            Hue = hue;
        }

        public PowerCrystal CreateItem()
        {
            return new PowerCrystal(Name, Hue);
        }
    }

    public static class PowerCrystalChampRegistry
    {
        private static readonly PowerCrystalChampEntry[] m_Entries = new PowerCrystalChampEntry[]
        {
            new PowerCrystalChampEntry("Banella", 2971),
            new PowerCrystalChampEntry("Roger", 2354),
            new PowerCrystalChampEntry("Kevin", 1194),
            new PowerCrystalChampEntry("Dave", 2864),
            new PowerCrystalChampEntry("Scott", 2987),
            new PowerCrystalChampEntry("Wally", 2976),
            new PowerCrystalChampEntry("Jake", 1675),
            new PowerCrystalChampEntry("Jim", 2357),
            new PowerCrystalChampEntry("Shane", 1186),
            new PowerCrystalChampEntry("Karen", 1075),
            new PowerCrystalChampEntry("Tom", 2855),
            new PowerCrystalChampEntry("Jared", 2889),
            new PowerCrystalChampEntry("Samantha", 2890),
            new PowerCrystalChampEntry("Maria", 2950),
            new PowerCrystalChampEntry("Barry", 1283),
            new PowerCrystalChampEntry("Josh", 2861),
            new PowerCrystalChampEntry("Hunter", 2887),
            new PowerCrystalChampEntry("Taylor", 2934),
            new PowerCrystalChampEntry("Fred", 2992),
            new PowerCrystalChampEntry("Matt", 1287),
            new PowerCrystalChampEntry("Joe", 2398),
            new PowerCrystalChampEntry("Jenny", 1673),
            new PowerCrystalChampEntry("Steve", 1100),
            new PowerCrystalChampEntry("Harry", 2184),
            new PowerCrystalChampEntry("Paul", 2782),
            new PowerCrystalChampEntry("Patrick", 1292),
            new PowerCrystalChampEntry("Rex", 1465),
            new PowerCrystalChampEntry("Mike", 2999),
            new PowerCrystalChampEntry("Alice", 2957),
            new PowerCrystalChampEntry("Dan", 2990),
            new PowerCrystalChampEntry("Kelly", 2973),
            new PowerCrystalChampEntry("John", 2172),
            new PowerCrystalChampEntry("Will", 1288),
            new PowerCrystalChampEntry("Toby", 2991),
            new PowerCrystalChampEntry("Betty", 2240),
            new PowerCrystalChampEntry("James", 2249),
            new PowerCrystalChampEntry("Jack", 1174),
            new PowerCrystalChampEntry("Lisa", 2974),
            new PowerCrystalChampEntry("Mary", 2352),
            new PowerCrystalChampEntry("Jeff", 2174),
            new PowerCrystalChampEntry("Brandy", 2994),
            new PowerCrystalChampEntry("George", 1973),
            new PowerCrystalChampEntry("Kirk", 2816),
            new PowerCrystalChampEntry("Bob", 2189),
            new PowerCrystalChampEntry("Mark", 2904),
            new PowerCrystalChampEntry("Alex", 2366),
            new PowerCrystalChampEntry("Noah", 2811),
            new PowerCrystalChampEntry("Penny", 2801),
            new PowerCrystalChampEntry("Jason", 1496),
            new PowerCrystalChampEntry("Daniel", 2061)
        };

        public static int GetRandomIndex()
        {
            return Utility.Random(m_Entries.Length);
        }

        public static PowerCrystalChampEntry GetEntry(int index)
        {
            if (index < 0 || index >= m_Entries.Length)
            {
                index = 0;
            }

            return m_Entries[index];
        }
    }
}
