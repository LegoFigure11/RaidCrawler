using FlatSharp;
using System.Buffers.Binary;

namespace RaidCrawler.Structures
{

    public static class FlatbufferDumper
    {
        public static byte[][] DumpDistributionRaids(string path)
        {
            var type2 = new List<byte[]>();
            var type3 = new List<byte[]>();

            var encounters = Utils.GetBinaryResource(path);
            if (encounters.Length == 0)
                return new byte[0][];
            var tableEncounters = FlatBufferSerializer.Default.Parse<DeliveryRaidEnemyTableArray>(encounters);
            var byGroupID = tableEncounters.Table
            .Where(z => z.RaidEnemyInfo.Rate != 0)
            .GroupBy(z => z.RaidEnemyInfo.DeliveryGroupID);

            foreach (var group in byGroupID)
            {
                var items = group.ToArray();
                if (items.Any(z => z.RaidEnemyInfo.Difficulty > 7))
                    continue;
                if (items.All(z => z.RaidEnemyInfo.Difficulty == 7))
                    AddToList(items, type3, RaidSerializationFormat.Type3);
                else if (items.Any(z => z.RaidEnemyInfo.Difficulty == 7))
                    throw new Exception($"Mixed difficulty {items.First(z => z.RaidEnemyInfo.Difficulty > 7).RaidEnemyInfo.Difficulty}");
                else AddToList(items, type2, RaidSerializationFormat.Type2);
            }

            var ordered2 = type2
                    .OrderBy(z => BinaryPrimitives.ReadUInt16LittleEndian(z)) // Species
                    .ThenBy(z => z[2]) // Form
                    .ThenBy(z => z[3]) // Level
                    .ThenBy(z => z[0x11]) // Distribution Index
                ;
            var ordered3 = type3
                .OrderBy(z => BinaryPrimitives.ReadUInt16LittleEndian(z)) // Species
                    .ThenBy(z => z[2]) // Form
                    .ThenBy(z => z[3]) // Level
                    .ThenBy(z => z[0x11]) // Distribution Index
                ;
            return new[] { ordered2.SelectMany(z => z).ToArray(), ordered3.SelectMany(z => z).ToArray() };
        }

        private static readonly int[][] StageStars =
        {
            new [] { 1, 2 },
            new [] { 1, 2, 3 },
            new [] { 1, 2, 3, 4 },
            new [] { 3, 4, 5, 6, 7 },
        };

        private static void AddToList(IReadOnlyCollection<DeliveryRaidEnemyTable> table, List<byte[]> list, RaidSerializationFormat format)
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
                TryAddToPickle(info, list, format, weightTotalS, weightTotalV, weightMinS, weightMinV);
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

        private static void TryAddToPickle(RaidEnemyInfo enc, ICollection<byte[]> list, RaidSerializationFormat format, ReadOnlySpan<ushort> totalS, ReadOnlySpan<ushort> totalV, ReadOnlySpan<ushort> minS, ReadOnlySpan<ushort> minV)
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
                bw.Write(noTotal || enc.RomVer == RaidRomType.TYPE_B ? (ushort)0 : totalS[stage]);
                bw.Write(noTotal || enc.RomVer == RaidRomType.TYPE_A ? (ushort)0 : totalV[stage]);
            }
            if (format == RaidSerializationFormat.Type3)
                enc.SerializeType3(bw);

            var bin = ms.ToArray();
            if (!list.Any(z => z.SequenceEqual(bin)))
                list.Add(bin);
        }
    }
}
