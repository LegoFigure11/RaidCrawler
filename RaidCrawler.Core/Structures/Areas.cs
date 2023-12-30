using PKHeX.Core;

namespace RaidCrawler.Core.Structures;

public static class Areas
{
    private static readonly string[] AreaList =
    [
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
    ];

    private static readonly string[] AreaListKitakami =
    [
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
    ];

    private static readonly string[] AreaListBlueberry =
    [
        "Savanna Biome",
        "Coastal Biome",
        "Canyon Biome",
        "Polar Biome",
        "Savanna Biome",
        "Coastal Biome",
        "Canyon Biome",
        "Polar Biome",
    ];

    public static string GetArea(int index, TeraRaidMapParent type) => type switch
    {
        TeraRaidMapParent.Kitakami => AreaListKitakami[index],
        TeraRaidMapParent.Blueberry => AreaListBlueberry[index],
        _ => AreaList[index],
    };
}
