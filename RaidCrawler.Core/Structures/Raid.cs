using PKHeX.Core;
using static System.Buffers.Binary.BinaryPrimitives;

namespace RaidCrawler.Core.Structures
{
    public class Raid
    // See also https://github.com/kwsch/PKHeX/blob/master/PKHeX.Core/Saves/Substructures/Gen9/RaidSpawnList9.cs
    {
        public const byte SIZE = 0x20;
        private readonly byte[] Data; // Raw data

        public Raid(
            Span<byte> data,
            TeraRaidMapParent map = TeraRaidMapParent.Paldea
        )
        {
            Data = data.ToArray();
            MapParent = map;
        }

        public TeraRaidMapParent MapParent;

        public bool IsValid => Validate();
        public bool IsActive => ReadUInt32LittleEndian(Data.AsSpan(0x00)) == 1;
        public uint Area => ReadUInt32LittleEndian(Data.AsSpan(0x04));
        public uint DisplayType => ReadUInt32LittleEndian(Data.AsSpan(0x08));
        public uint Den => ReadUInt32LittleEndian(Data.AsSpan(0x0C));
        public uint Seed => ReadUInt32LittleEndian(Data.AsSpan(0x10));
        public uint Flags => ReadUInt32LittleEndian(Data.AsSpan(0x18));
        public bool IsBlack => Flags == 1;
        public bool IsEvent => Flags >= 2;

        public int TeraType => GetTeraType(Seed);
        public uint Difficulty => GetDifficulty(Seed);

        public uint EC => GenericRaidData[0];
        public uint PID => GenericRaidData[2];
        public bool IsShiny => GenericRaidData[3] == 1;

        uint[] GenericRaidData => GenerateGenericRaidData(Seed);

        public byte[] GetData() => Data;

        private bool Validate()
        {
            if (Seed == 0 || !IsActive || Area > 22)
                return false;

            GenerateGenericRaidData(Seed);
            return true;
        }

        private static int GetTeraType(uint Seed)
        {
            var rng = new Xoroshiro128Plus(Seed);
            return (int)rng.NextInt(18);
        }

        private static uint[] GenerateGenericRaidData(uint Seed)
        {
            var rng = new Xoroshiro128Plus(Seed);
            uint EC = (uint)rng.NextInt();
            uint TIDSID = (uint)rng.NextInt();
            uint PID = (uint)rng.NextInt();
            var Shiny =
                (((PID >> 16) ^ (PID & 0xFFFF)) >> 4) == (((TIDSID >> 16) ^ (TIDSID & 0xFFFF)) >> 4)
                    ? 1
                    : 0;
            return new uint[] { EC, TIDSID, PID, (uint)Shiny };
        }

        private static uint GetDifficulty(uint Seed)
        {
            var rng = new Xoroshiro128Plus(Seed);
            return (uint)rng.NextInt(100);
        }
    }
}
