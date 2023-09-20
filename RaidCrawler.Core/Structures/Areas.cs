using pkNX.Structures.FlatBuffers.Gen9;

namespace RaidCrawler.Core.Structures
{
    public class Areas
    {
        private static readonly string[] AreaList = new string[]
        {
            "South Province (Area 1)",
            "", // Unused
            "", // Unused
            "South Province (Area 2)",
            "South Province (Area 4)",
            "South Province (Area 6)",
            "South Province (Area 5)",
            "South Province (Area 3)",
            "West Province (Area 1)",
            "Asado Desert",
            "West Province (Area 2)",
            "West Province (Area 3)",
            "Tagtree Thicket",
            "East Province (Area 3)",
            "East Province (Area 1)",
            "East Province (Area 2)",
            "Dalizapa Passage",
            "Casseroya Lake",
            "Glaseado Mountain",
            "North Province (Area 3)",
            "North Province (Area 1)",
            "North Province (Area 2)",
        };

        private static readonly string[] AreaListKitakami = new string[] {
            "Kitakami Road",
            "Apple Hills",
            "Reveler's Road",
            "Oni Mountain",
            "Infernal Pass",
            "Crystal Pool",
            "Wistful Fields",
            "Mossfell Confluence",
            "Fellhorn Gorge",
            "Paradise Barrens",
            "Timeless Woods",
        };

        public static string GetArea(int index, RaidSerializationFormat type) => type switch
        {
            RaidSerializationFormat.KitakamiROM => AreaListKitakami[index],
            _ => AreaList[index],
        };
    }
}
