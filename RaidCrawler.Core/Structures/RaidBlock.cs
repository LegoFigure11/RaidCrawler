namespace RaidCrawler.Core.Structures;

public static class RaidBlock
{
    public const uint HEADER_SIZE = 0x10;

    public const uint MAX_COUNT_BASE = 72;
    public const uint SIZE_BASE = Raid.SIZE * MAX_COUNT_BASE;

    public const uint MAX_COUNT_KITAKAMI = 100;
    public const uint SIZE_KITAKAMI = Raid.SIZE * MAX_COUNT_KITAKAMI;

    public const uint MAX_COUNT_BLUEBERRY = 100;
    public const uint SIZE_BLUEBERRY = Raid.SIZE * MAX_COUNT_BLUEBERRY;
}
