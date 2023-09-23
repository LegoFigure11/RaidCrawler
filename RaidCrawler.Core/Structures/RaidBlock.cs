namespace RaidCrawler.Core.Structures
{
    public class RaidBlock
    {
        public const uint RAID_BLOCK_SIZE = 0x20;

        public const uint HEADER_SIZE_BASE = 0x10;
        public const uint MAX_COUNT_BASE = 72;
        public const uint TOTAL_SIZE_BASE = HEADER_SIZE_BASE + (RAID_BLOCK_SIZE * MAX_COUNT_BASE);

        public const uint HEADER_SIZE_KITAKAMI = 0;
        public const uint MAX_COUNT_KITAKAMI = 100;
        public const uint TOTAL_SIZE_KITAKAMI = HEADER_SIZE_KITAKAMI + (RAID_BLOCK_SIZE * MAX_COUNT_KITAKAMI);
    }
}
