using PKHeX.Core;

namespace RaidCrawler.Core.Structures
{
    public class MapMagic
    {
        private double ScaleX { get; init; }
        private double ScaleZ { get; init; }
        private double OffsetZ { get; init; }
        public double ConvertWidth(double s) => (512 / ScaleX) * s;
        public double ConvertHeight(double s) => (512 / ScaleZ) * s;
        public double ConvertX(double x) => (512 / ScaleX) * x;
        public double ConvertZ(double z) => (512 / ScaleZ) * (z + OffsetZ);

        public static MapMagic GetMapMagic(TeraRaidMapParent parent) => parent switch
        {
            TeraRaidMapParent.Blueberry => Blueberry,
            TeraRaidMapParent.Kitakami => Kitakami,
            _ => Base,
        };

        private static readonly MapMagic Base = new()
        {
            ScaleX = 5000,
            ScaleZ = 5000,
            OffsetZ = 5500,
        };

        private static readonly MapMagic Kitakami = new()
        {
            ScaleX = 2000,
            ScaleZ = 2000,
            OffsetZ = 2000,
        };

        private static readonly MapMagic Blueberry = new()
        {
            ScaleX = 2000,
            ScaleZ = 2000,
            OffsetZ = 2000,
        };
    }
}
