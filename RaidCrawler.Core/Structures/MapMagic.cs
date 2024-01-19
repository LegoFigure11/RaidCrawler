using PKHeX.Core;

namespace RaidCrawler.Core.Structures
{
    public class MapMagic
    {
        public double MultX { get; init; }
        public double MultY { get; init; }
        public double AddX { get; init; }
        public double AddY { get; init; }
        public short MultConst { get; init; }
        public short DivConst { get; init; }

        public static MapMagic GetMapMagic(TeraRaidMapParent parent) => parent switch
        {
            TeraRaidMapParent.Blueberry => Blueberry,
            TeraRaidMapParent.Kitakami => Kitakami,
            _ => Base,
        };

        private static readonly MapMagic Base = new()
        {
            MultX = 1,
            AddX = 2.072021484,
            MultY = 1,
            AddY = 5505.240018,
            MultConst = 512,
            DivConst = 500,
        };

        private static readonly MapMagic Kitakami = new()
        {
            MultX = 2.766970605475146,
            AddX = -248.08352352566726,
            MultY = 2.5700782642623805,
            AddY = 5070.808599816581,
            MultConst = 512,
            DivConst = 500,
        };

        private static readonly MapMagic Blueberry = new()
        {
            MultX = 0.2566504136675,
            AddX = 0.893932258207,
            MultY = 0.2559781068906,
            AddY = 511.5361519625,
            MultConst = 1,
            DivConst = 1,
        };
    }
}
