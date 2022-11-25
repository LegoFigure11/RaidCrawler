using PKHeX.Core;
using static System.Buffers.Binary.BinaryPrimitives;

namespace RaidCrawler.Structures
{
    public partial class Raid
    {
        public const byte SIZE = 0x20;
        const int IV_MAX = 31;

        public readonly byte[] Data; // Raw data

        public Raid(byte[] data) => Data = data;
        public Raid(int size) => Data = new byte[size];

        public virtual bool IsValid => Validate(); // Does this look like an actual raid

        public virtual uint UNK_1 => ReadUInt32LittleEndian(Data.AsSpan(0x00));  // Unused? Seems to always be 1
        public virtual uint Area => ReadUInt32LittleEndian(Data.AsSpan(0x04));
        public virtual uint UNK_3 => ReadUInt32LittleEndian(Data.AsSpan(0x08));  // Unused? Seems to always be 1
        public virtual uint Den => ReadUInt32LittleEndian(Data.AsSpan(0x0C));
        public virtual uint Seed => ReadUInt32LittleEndian(Data.AsSpan(0x10));
        public virtual bool IsEvent => ReadUInt32LittleEndian(Data.AsSpan(0x18)) == 2;
        public virtual bool IsBlack { get; set; }

        // Derived Values
        public virtual string? TeraType => GetTeraType(Seed);
        public virtual uint EC => GenericRaidData[0];
        /* public virtual uint TIDSID => GenericRaidData[1]; */ // Unneeded
        public virtual uint PID => GenericRaidData[2];
        public virtual bool IsShiny => GenericRaidData[3] == 1;

        public virtual int[] Flawless_0 => GetIVs(Seed, 0);
        public virtual int[] Flawless_1 => GetIVs(Seed, 1);
        public virtual int[] Flawless_2 => GetIVs(Seed, 2);
        public virtual int[] Flawless_3 => GetIVs(Seed, 3);
        public virtual int[] Flawless_4 => GetIVs(Seed, 4);
        public virtual int[] Flawless_5 => GetIVs(Seed, 5);


        uint[] GenericRaidData => GenerateGenericRaidData(Seed);

        // Methods
        private bool Validate()
        {
            if (Seed == 0) return false;
            GenerateGenericRaidData(Seed);
            return true;
        }

        private static string GetTeraType(uint Seed)
        {
            var rng = new Xoroshiro128Plus(Seed);
            var Type = rng.NextInt(18);
            return $"{GameInfo.GetStrings(1).types[Type]} ({Type})";
        }

        private static uint[] GenerateGenericRaidData(uint Seed)
        {
            var rng = new Xoroshiro128Plus(Seed);
            uint EC = (uint)rng.NextInt();
            uint TIDSID = (uint)rng.NextInt();
            uint PID = (uint)rng.NextInt();
            var Shiny = (((PID >> 16) ^ (PID & 0xFFFF)) >> 4) == (((TIDSID >> 16) ^ (TIDSID & 0xFFFF)) >> 4) ? 1 : 0;
            return new uint[] { EC, TIDSID, PID, (uint)Shiny };
        }

        private static int[] GetIVs(uint Seed, int FlawlessIVs)
        {
            var rng = new Xoroshiro128Plus(Seed);
            // Dummy calls
            rng.NextInt();
            rng.NextInt();
            rng.NextInt();

            Span<int> ivs = stackalloc[] { -1, -1, -1, -1, -1, -1 };
            // Flawless IVs
            for (int i = 0; i < FlawlessIVs; i++)
            {
                int index;
                do { index = (int)rng.NextInt(6); }
                while (ivs[index] != -1);

                ivs[index] = IV_MAX;
            }
            // Other IVs
            for (int i = 0; i < ivs.Length; i++)
            {
                if (ivs[i] == -1)
                    ivs[i] = (int)rng.NextInt(32);
            }

            return ivs.ToArray();
        }
    }
}
