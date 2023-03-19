namespace RaidCrawler.Structures
{
    internal class Offsets
    {
        public const string ScarletID = "0100A3D008C5C000";
        public const string VioletID = "01008F6008C5E000";

        public static IReadOnlyList<long> SaveBlockPointer = new List<long>() { 0x44AAC88, 0xE0, 0x80, 0x08, 0x0 }; // Thanks Lincoln-LM!
        public static IReadOnlyList<long> RaidBlockPointer = new List<long>() { 0x44A98C8, 0x180, 0x40 };
        public static IReadOnlyList<long> BlockKeyPointer = new List<long>() { 0x449EEE8, 0xD8, 0x0, 0x0, 0x30, 0x0 };
        public static IReadOnlyList<uint> DifficultyFlags = new List<uint>() { 0xEC95D8EF, 0xA9428DFE, 0x9535F471, 0x6E7F8220 };

        public static uint BCATRaidBinaryLocation = 0x520A1B0; // Thanks Lincoln-LM!
        public static uint BCATRaidPriorityLocation = 0x95451E4; // Thanks Lincoln-LM!
        public static uint BCATRaidFixedRewardLocation = 0x7D6C2B82;
        public static uint BCATRaidLotteryRewardLocation = 0xA52B4811;
    }
}
