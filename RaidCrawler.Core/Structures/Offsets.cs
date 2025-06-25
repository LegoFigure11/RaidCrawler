namespace RaidCrawler.Core.Structures;

public abstract class Offsets
{
    public const string ScarletID = "0100A3D008C5C000";
    public const string VioletID = "01008F6008C5E000";

    public static ReadOnlySpan<long> RaidBlockPointerBase => [0x47350D8, 0x1C0, 0x88, 0x40];
    public static ReadOnlySpan<long> RaidBlockPointerKitakami => [0x47350D8, 0x1C0, 0x88, 0xCD8];
    public static ReadOnlySpan<long> RaidBlockPointerBlueberry => [0x47350D8, 0x1C0, 0x88, 0x1958];
    public static ReadOnlySpan<long> BlockKeyPointer => [0x47350D8, 0xD8, 0x0, 0x0, 0x30, 0x0];
    public static ReadOnlySpan<uint> DifficultyFlags => [0xEC95D8EF, 0xA9428DFE, 0x9535F471, 0x6E7F8220];

    public const uint BCATRaidBinaryLocation = 0x520A1B0; // Thanks Lincoln-LM!
    public const uint BCATRaidPriorityLocation = 0x95451E4; // Thanks Lincoln-LM!
    public const uint BCATRaidFixedRewardLocation = 0x7D6C2B82;
    public const uint BCATRaidLotteryRewardLocation = 0xA52B4811;
}
