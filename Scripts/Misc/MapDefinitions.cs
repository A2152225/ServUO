using System;

namespace Server.Misc
{
    public class MapDefinitions
    {
        public static void Configure()
        {
            /* Here we configure all maps. Some notes:
            * 
            * 1) The first 32 maps are reserved for core use.
            * 2) Map 0x7F is reserved for core use.
            * 3) Map 0xFF is reserved for core use.
            * 4) Changing or removing any predefined maps may cause server instability.
            */
            if (Siege.SiegeShard)
            {
                RegisterMap(0, 0, 0, 7168, 4096, 4, "Felucca", MapRules.FeluccaRules);
                RegisterMap(1, 1, 1, 7168, 4096, 0, "Trammel", MapRules.FeluccaRules);
                RegisterMap(2, 2, 2, 2304, 1600, 1, "Ilshenar", MapRules.FeluccaRules);
                RegisterMap(3, 3, 3, 2560, 2048, 1, "Malas", MapRules.FeluccaRules);
                RegisterMap(4, 4, 4, 1448, 1448, 1, "Tokuno", MapRules.FeluccaRules);
                RegisterMap(5, 5, 5, 1280, 4096, 1, "TerMur", MapRules.FeluccaRules);
            }
            else
            {
                RegisterMap(0, 0, 0, 7168, 4096, 4, "Felucca", MapRules.FeluccaRules);
                RegisterMap(1, 1, 1, 7168, 4096, 0, "Trammel", MapRules.TrammelRules);
                RegisterMap(2, 2, 2, 2304, 1600, 1, "Ilshenar", MapRules.TrammelRules);
                RegisterMap(3, 3, 3, 2560, 2048, 1, "Malas", MapRules.TrammelRules);
                RegisterMap(4, 4, 4, 1448, 1448, 1, "Tokuno", MapRules.TrammelRules);
                RegisterMap(5, 5, 5, 1280, 4096, 1, "TerMur", MapRules.TrammelRules);
            }

            RegisterMap(0x7F, 0x7F, 0x7F, Map.SectorSize, Map.SectorSize, 1, "Internal", MapRules.Internal);
			RegisterMap(32, 32, 32, 7168, 4096, 4, "Akara", MapRules.FeluccaRules);
			//end normal 
			
			//start hard
			RegisterMap(40, 40, 40, 7168, 4096, 4, "Felucca(Hard)", MapRules.FeluccaRules);
			RegisterMap(41, 41, 41, 7168, 4096, 0, "Trammel(Hard)", MapRules.TrammelRules);
			RegisterMap(42, 42, 42, 2304, 1600, 1, "Ilshenar(Hard)", MapRules.TrammelRules);
			RegisterMap(43, 43, 43, 2560, 2048, 1, "Malas(Hard)", MapRules.TrammelRules);
			RegisterMap(44, 44, 44, 1448, 1448, 1, "Tokuno(Hard)", MapRules.TrammelRules);
			RegisterMap(45, 45, 45, 1280, 4096, 1, "TerMur(Hard)", MapRules.TrammelRules);
			RegisterMap(46, 46, 46, 7168, 4096, 4, "Akara(Hard)", MapRules.FeluccaRules);
			
			
			//start Expert
			RegisterMap(50, 50, 50, 7168, 4096, 4, "Felucca(Expert)", MapRules.FeluccaRules);
			RegisterMap(51, 51, 51, 7168, 4096, 0, "Trammel(Expert)", MapRules.TrammelRules);
			RegisterMap(52, 52, 52, 2304, 1600, 1, "Ilshenar(Expert)", MapRules.TrammelRules);
			RegisterMap(53, 53, 53, 2560, 2048, 1, "Malas(Expert)", MapRules.TrammelRules);
			RegisterMap(54, 54, 54, 1448, 1448, 1, "Tokuno(Expert)", MapRules.TrammelRules);
			RegisterMap(55, 55, 55, 1280, 4096, 1, "TerMur(Expert)", MapRules.TrammelRules);
			RegisterMap(56, 56, 56, 7168, 4096, 4, "Akara(Expert)", MapRules.FeluccaRules);
			
			
			
			//start Master
			RegisterMap(60, 60, 60, 7168, 4096, 4, "Felucca(Master)", MapRules.FeluccaRules);
			RegisterMap(61, 61, 61, 7168, 4096, 0, "Trammel(Master)", MapRules.TrammelRules);
			RegisterMap(62, 62, 62, 2304, 1600, 1, "Ilshenar(Master)", MapRules.TrammelRules);
			RegisterMap(63, 63, 63, 2560, 2048, 1, "Malas(Master)", MapRules.TrammelRules);
			RegisterMap(64, 64, 64, 1448, 1448, 1, "Tokuno(Master)", MapRules.TrammelRules);
			RegisterMap(65, 65, 65, 1280, 4096, 1, "TerMur(Master)", MapRules.TrammelRules);
			RegisterMap(66, 66, 66, 7168, 4096, 4, "Akara(Master)", MapRules.FeluccaRules);
			
			
			
			
			//start T1
			RegisterMap(70, 70, 70, 7168, 4096, 4, "Felucca(T1)", MapRules.FeluccaRules);
			RegisterMap(71, 71, 71, 7168, 4096, 0, "Trammel(T1)", MapRules.TrammelRules);
			RegisterMap(72, 72, 72, 2304, 1600, 1, "Ilshenar(T1)", MapRules.TrammelRules);
			RegisterMap(73, 73, 73, 2560, 2048, 1, "Malas(T1)", MapRules.TrammelRules);
			RegisterMap(74, 74, 74, 1448, 1448, 1, "Tokuno(T1)", MapRules.TrammelRules);
			RegisterMap(75, 75, 75, 1280, 4096, 1, "TerMur(T1)", MapRules.TrammelRules);
			RegisterMap(76, 76, 76, 7168, 4096, 4, "Akara(T1)", MapRules.FeluccaRules);
			
			
			
			
			//start T2
			RegisterMap(80, 80, 80, 7168, 4096, 4, "Felucca(T2)", MapRules.FeluccaRules);
			RegisterMap(81, 81, 81, 7168, 4096, 0, "Trammel(T2)", MapRules.TrammelRules);
			RegisterMap(82, 82, 82, 2304, 1600, 1, "Ilshenar(T2)", MapRules.TrammelRules);
			RegisterMap(83, 83, 83, 2560, 2048, 1, "Malas(T2)", MapRules.TrammelRules);
			RegisterMap(84, 84, 84, 1448, 1448, 1, "Tokuno(T2)", MapRules.TrammelRules);
			RegisterMap(85, 85, 85, 1280, 4096, 1, "TerMur(T2)", MapRules.TrammelRules);
			RegisterMap(86, 86, 86, 7168, 4096, 4, "Akara(T2)", MapRules.FeluccaRules);
			
			
			
			
			//start T3
			RegisterMap(90, 90, 90, 7168, 4096, 4, "Felucca(T3)", MapRules.FeluccaRules);
			RegisterMap(91, 91, 91, 7168, 4096, 0, "Trammel(T3)", MapRules.TrammelRules);
			RegisterMap(92, 92, 92, 2304, 1600, 1, "Ilshenar(T3)", MapRules.TrammelRules);
			RegisterMap(93, 93, 93, 2560, 2048, 1, "Malas(T3)", MapRules.TrammelRules);
			RegisterMap(94, 94, 94, 1448, 1448, 1, "Tokuno(T3)", MapRules.TrammelRules);
			RegisterMap(95, 95, 95, 1280, 4096, 1, "TerMur(T3)", MapRules.TrammelRules);
			RegisterMap(96, 96, 96, 7168, 4096, 4, "Akara(T3)", MapRules.FeluccaRules);
			
			
			
			
			//start T4
			RegisterMap(100, 100, 100, 7168, 4096, 4, "Felucca(T4)", MapRules.FeluccaRules);
			RegisterMap(101, 101, 101, 7168, 4096, 0, "Trammel(T4)", MapRules.TrammelRules);
			RegisterMap(102, 102, 102, 2304, 1600, 1, "Ilshenar(T4)", MapRules.TrammelRules);
			RegisterMap(103, 103, 103, 2560, 2048, 1, "Malas(T4)", MapRules.TrammelRules);
			RegisterMap(104, 104, 104, 1448, 1448, 1, "Tokuno(T4)", MapRules.TrammelRules);
			RegisterMap(105, 105, 105, 1280, 4096, 1, "TerMur(T4)", MapRules.TrammelRules);
			RegisterMap(106, 106, 106, 7168, 4096, 4, "Akara(T4)", MapRules.FeluccaRules);
			
			
			
			
			//start T5
			RegisterMap(110, 110, 110, 7168, 4096, 4, "Felucca(T5)", MapRules.FeluccaRules);
			RegisterMap(111, 111, 111, 7168, 4096, 0, "Trammel(T5)", MapRules.TrammelRules);
			RegisterMap(112, 112, 112, 2304, 1600, 1, "Ilshenar(T5)", MapRules.TrammelRules);
			RegisterMap(113, 113, 113, 2560, 2048, 1, "Malas(T5)", MapRules.TrammelRules);
			RegisterMap(114, 114, 114, 1448, 1448, 1, "Tokuno(T5)", MapRules.TrammelRules);
			RegisterMap(115, 115, 115, 1280, 4096, 1, "TerMur(T5)", MapRules.TrammelRules);
			RegisterMap(116, 116, 116, 7168, 4096, 4, "Akara(T5)", MapRules.FeluccaRules);
			
			
			
			
			//start T6
			RegisterMap(120, 120, 120, 7168, 4096, 4, "Felucca(T6)", MapRules.FeluccaRules);
			RegisterMap(121, 121, 121, 7168, 4096, 0, "Trammel(T6)", MapRules.TrammelRules);
			RegisterMap(122, 122, 122, 2304, 1600, 1, "Ilshenar(T6)", MapRules.TrammelRules);
			RegisterMap(123, 123, 123, 2560, 2048, 1, "Malas(T6)", MapRules.TrammelRules);
			RegisterMap(124, 124, 124, 1448, 1448, 1, "Tokuno(T6)", MapRules.TrammelRules);
			RegisterMap(125, 125, 125, 1280, 4096, 1, "TerMur(T6)", MapRules.TrammelRules);
			RegisterMap(126, 126, 126, 7168, 4096, 4, "Akara(T6)", MapRules.FeluccaRules);
			
			
			
			
			
				
            /* Example of registering a custom map:
            * RegisterMap( 32, 0, 0, 6144, 4096, 3, "Iceland", MapRules.FeluccaRules );
            * 
            * Defined:
            * RegisterMap( <index>, <mapID>, <fileIndex>, <width>, <height>, <season>, <name>, <rules> );
            *  - <index> : An unreserved unique index for this map
            *  - <mapID> : An identification number used in client communications. For any visible maps, this value must be from 0-5
            *  - <fileIndex> : A file identification number. For any visible maps, this value must be from 0-5
            *  - <width>, <height> : Size of the map (in tiles)
            *  - <season> : Season of the map. 0 = Spring, 1 = Summer, 2 = Fall, 3 = Winter, 4 = Desolation
            *  - <name> : Reference name for the map, used in props gump, get/set commands, region loading, etc
            *  - <rules> : Rules and restrictions associated with the map. See documentation for details
            */

            TileMatrixPatch.Enabled = false; // OSI Client Patch 6.0.0.0

            MultiComponentList.PostHSFormat = true; // OSI Client Patch 7.0.9.0
        }

        public static void RegisterMap(int mapIndex, int mapID, int fileIndex, int width, int height, int season, string name, MapRules rules)
        {
            Map newMap = new Map(mapID, mapIndex, fileIndex, width, height, season, name, rules);

            Map.Maps[mapIndex] = newMap;
            Map.AllMaps.Add(newMap);
        }
    }
}