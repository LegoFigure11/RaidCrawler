namespace RaidCrawler.Core.Structures
{
    public class Areas
    {
        private static readonly string[] AreaList = new string[] {
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

        public static string GetArea(int index) => AreaList[index];
    }
}
