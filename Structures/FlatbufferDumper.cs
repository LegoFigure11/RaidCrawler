using System.Buffers.Binary;
using FlatSharp;
using FlatSharp.Attributes;

namespace RaidCrawler.Structures
{
    public static class FlatbufferDumper
    {
        public static byte[] DumpDistributionRaids(string path)
        {
            var list = new List<byte[]>();

            var encounters = Utils.GetBinaryResource(path);
            if (encounters.Length == 0)
                return encounters;
            var tableEncounters = FlatBufferSerializer.Default.Parse<DeliveryRaidEnemyTableArray>(encounters);
            AddToList(tableEncounters.Table, list);
            var ordered = list
                    .OrderBy(z => BinaryPrimitives.ReadUInt16LittleEndian(z)) // Species
                    .ThenBy(z => z[2]) // Form
                    .ThenBy(z => z[3]) // Level
                    .ThenBy(z => z[0x11]) // Distribution Index
                ;
            return ordered.SelectMany(z => z).ToArray();
        }

        private static readonly int[][] StageStars =
        {
            new [] { 1, 2 },
            new [] { 1, 2, 3 },
            new [] { 1, 2, 3, 4 },
            new [] { 3, 4, 5 },
        };

        private static void AddToList(IReadOnlyCollection<DeliveryRaidEnemyTable> table, List<byte[]> list)
        {
            // Get the total weight for each stage of star count
            Span<ushort> weightTotalS = stackalloc ushort[StageStars.Length];
            Span<ushort> weightTotalV = stackalloc ushort[StageStars.Length];
            foreach (var enc in table)
            {
                var info = enc.RaidEnemyInfo;
                if (info.Rate == 0)
                    continue;
                var difficulty = info.Difficulty;
                for (int stage = 0; stage < StageStars.Length; stage++)
                {
                    if (!StageStars[stage].Contains(difficulty))
                        continue;
                    if (info.RomVer != RaidRomType.TYPE_B)
                        weightTotalS[stage] += (ushort)info.Rate;
                    if (info.RomVer != RaidRomType.TYPE_A)
                        weightTotalV[stage] += (ushort)info.Rate;
                }
            }

            Span<ushort> weightMinS = stackalloc ushort[StageStars.Length];
            Span<ushort> weightMinV = stackalloc ushort[StageStars.Length];
            foreach (var enc in table)
            {
                var info = enc.RaidEnemyInfo;
                if (info.Rate == 0)
                    continue;
                var difficulty = info.Difficulty;
                TryAddToPickle(info, list, weightTotalS, weightTotalV, weightMinS, weightMinV);
                for (int stage = 0; stage < StageStars.Length; stage++)
                {
                    if (!StageStars[stage].Contains(difficulty))
                        continue;
                    if (info.RomVer != RaidRomType.TYPE_B)
                        weightMinS[stage] += (ushort)info.Rate;
                    if (info.RomVer != RaidRomType.TYPE_A)
                        weightMinV[stage] += (ushort)info.Rate;
                }
            }
        }

        private static void TryAddToPickle(RaidEnemyInfo enc, ICollection<byte[]> list, ReadOnlySpan<ushort> totalS, ReadOnlySpan<ushort> totalV, ReadOnlySpan<ushort> minS, ReadOnlySpan<ushort> minV)
        {
            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);

            enc.SerializePKHeX(bw, (byte)enc.Difficulty, enc.Rate);
            for (int stage = 0; stage < StageStars.Length; stage++)
            {
                bool noTotal = !StageStars[stage].Contains(enc.Difficulty);
                ushort mS = minS[stage];
                ushort mV = minV[stage];
                bw.Write(noTotal ? (ushort)0 : mS);
                bw.Write(noTotal ? (ushort)0 : mV);
                bw.Write(noTotal ? (ushort)0 : totalS[stage]);
                bw.Write(noTotal ? (ushort)0 : totalV[stage]);
            }

            var bin = ms.ToArray();
            if (!list.Any(z => z.SequenceEqual(bin)))
                list.Add(bin);
        }
    }
}
