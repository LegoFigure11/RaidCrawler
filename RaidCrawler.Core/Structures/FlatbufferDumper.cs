using FlatSharp;
using pkNX.Structures.FlatBuffers.Gen9;
using System.Diagnostics;

namespace RaidCrawler.Core.Structures
{
    public static class FlatbufferDumper
    {
        public static byte[][] DumpBaseROMRaids(string[] paths)
        {
            var list = new List<RaidStorage>();
            var rateTotal = new (int Scarlet, int Violet)[8];
            for (int i = 0; i < paths.Length; i++)
            {
                var path = paths[i];
                var data = Utils.GetBinaryResource(path);
                var fb = RaidEnemyTableArray.Serializer.Parse(data, FlatBufferDeserializationOption.Greedy);
                var table = fb.Table;
                int totalRateScarlet = 0;
                int totalRateViolet = 0;
                foreach (var enc in table)
                {
                    var wrap = new RaidStorage(enc, i);
                    if (enc.Info.RomVer != RaidRomType.TYPE_B)
                    {
                        wrap.RandRateStartScarlet = totalRateScarlet;
                        totalRateScarlet += enc.Info.Rate;
                    }

                    if (enc.Info.RomVer != RaidRomType.TYPE_A)
                    {
                        wrap.RandRateStartViolet = totalRateViolet;
                        totalRateViolet += enc.Info.Rate;
                    }
                    list.Add(wrap);
                }
                rateTotal[i + 1] = (totalRateScarlet, totalRateViolet);
            }

            var all = list
            .OrderBy(z => z.Species)
            .ThenBy(z => z.Form)
            .ThenByDescending(z => z.Stars)
            .ThenByDescending(z => z.Delivery)
            .ToList();

            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);
            using var ms2 = new MemoryStream();
            using var bw2 = new BinaryWriter(ms2);
            using var ms3 = new MemoryStream();
            using var bw3 = new BinaryWriter(ms3);
            foreach (var enc in list)
            {
                var rmS = enc.GetScarletRandMinScarlet();
                var rmV = enc.GetVioletRandMinViolet();
                enc.Enemy.Info.SerializePKHeX(bw, (byte)enc.Stars, enc.Rate, RaidSerializationFormat.BaseROM);

                bw.Write(rmS);
                bw.Write(rmV);

                enc.Enemy.Info.BossDesc.SerializePKHeX(bw2);

                bw3.Write(enc.Enemy.Info.DropTableFix);
                bw3.Write(enc.Enemy.Info.DropTableRandom);
            }
            var pickle = ms.ToArray();
            var extra_moves = ms2.ToArray();
            var rewards = ms3.ToArray();
            return new[] { pickle, extra_moves, rewards };
        }

        public static byte[][] DumpDistributionRaids(byte[] encounters)
        {
            var type2 = new List<byte[]>();
            var type3 = new List<byte[]>();

            if (encounters.Length == 0)
                return Array.Empty<byte[]>();

            var tableEncounters = DeliveryRaidEnemyTableArray.Serializer.Parse(encounters, FlatBufferDeserializationOption.Greedy);
            var byGroupID = tableEncounters.Table
            .Where(z => z.Info.Rate != 0)
            .GroupBy(z => z.Info.DeliveryGroupID);

            foreach (var group in byGroupID)
            {
                var items = group.ToArray();
                if (items.Any(z => z.Info.Difficulty > 7))
                    continue;

                if (items.All(z => z.Info.Difficulty == 7))
                    AddToList(items, type3, RaidSerializationFormat.Might, group.Key);
                else if (items.Any(z => z.Info.Difficulty == 7))
                    throw new Exception($"Mixed difficulty {items.First(z => z.Info.Difficulty > 7).Info.Difficulty}");

                AddToList(items, type2, RaidSerializationFormat.Distribution, group.Key);
            }

            return new[]
            {
                type2.SelectMany(z => z.SkipLast(16 + 12 + 1)).ToArray(), type3.SelectMany(z => z.SkipLast(16 + 12 + 1)).ToArray(),
                type2.SelectMany(z => z.TakeLast(16 + 12 + 1).Take(16)).ToArray(), type3.SelectMany(z => z.TakeLast(16 + 12 + 1).Take(16)).ToArray(),
                type2.SelectMany(z => z.TakeLast(12 + 1).Take(12)).ToArray(), type3.SelectMany(z => z.TakeLast(12 + 1).Take(12)).ToArray(),
                type2.SelectMany(z => z.TakeLast(1)).ToArray(), type3.SelectMany(z => z.TakeLast(1)).ToArray()
            };
        }

        public static List<DeliveryRaidLotteryRewardItem> DumpLotteryRewards(byte[] rewards)
        {
            var tableRewards = DeliveryRaidLotteryRewardItemArray.Serializer.Parse(rewards, FlatBufferDeserializationOption.Greedy);
            return tableRewards.Table.ToList();
        }

        public static List<DeliveryRaidFixedRewardItem> DumpFixedRewards(byte[] rewards)
        {
            var tableRewards = DeliveryRaidFixedRewardItemArray.Serializer.Parse(rewards, FlatBufferDeserializationOption.Greedy);
            return tableRewards.Table.ToList();
        }

        public static (DeliveryGroupID, int) DumpDeliveryPriorities(byte[] flatbuffer)
        {
            try
            {
                var prios = DeliveryRaidPriorityArray.Serializer.Parse(flatbuffer, FlatBufferDeserializationOption.Greedy);
                return (prios.Table[0].GroupID, prios.Table[0].VersionNo);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return (new DeliveryGroupID { GroupID = new GroupIDSet() }, 0);
            }
        }

        private static readonly int[][] StageStars =
        {
            new [] { 1, 2 },
            new [] { 1, 2, 3 },
            new [] { 1, 2, 3, 4 },
            new [] { 3, 4, 5, 6, 7 },
        };

        private static void AddToList(IReadOnlyCollection<DeliveryRaidEnemyTable> table, List<byte[]> list, RaidSerializationFormat format, sbyte group)
        {
            // Get the total weight for each stage of star count
            Span<ushort> weightTotalS = stackalloc ushort[StageStars.Length];
            Span<ushort> weightTotalV = stackalloc ushort[StageStars.Length];
            foreach (var enc in table)
            {
                var info = enc.Info;
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
                var info = enc.Info;
                if (info.Rate == 0)
                    continue;

                var difficulty = info.Difficulty;
                TryAddToPickle(info, list, format, weightTotalS, weightTotalV, weightMinS, weightMinV, group);
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

        private static void TryAddToPickle(RaidEnemyInfo enc, ICollection<byte[]> list, RaidSerializationFormat format, ReadOnlySpan<ushort> totalS, ReadOnlySpan<ushort> totalV, ReadOnlySpan<ushort> minS, ReadOnlySpan<ushort> minV, sbyte group)
        {
            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);

            enc.SerializePKHeX(bw, (byte)enc.Difficulty, enc.Rate, format);
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

            if (format == RaidSerializationFormat.Distribution)
                enc.SerializeDistribution(bw);

            if (format == RaidSerializationFormat.Might)
                enc.SerializeMight(bw);

            // drop table reference
            bw.Write(enc.DropTableFix);
            bw.Write(enc.DropTableRandom);

            // extra moves reference
            enc.BossDesc.SerializePKHeX(bw);

            // group id reference
            bw.Write(group);

            var bin = ms.ToArray();
            if (!list.Any(z => z.SequenceEqual(bin)))
                list.Add(bin);
        }

        private record RaidStorage(RaidEnemyTable Enemy, int File)
        {
            private PokeDataBattle Poke => Enemy.Info.BossPokePara;

            public int Stars => Enemy.Info.Difficulty == 0 ? File + 1 : Enemy.Info.Difficulty;
            public ushort Species => Poke.DevId;
            public short Form => Poke.FormId;
            public int Delivery => Enemy.Info.DeliveryGroupID;
            public sbyte Rate => Enemy.Info.Rate;

            public int RandRateStartScarlet { get; set; }
            public int RandRateStartViolet { get; set; }

            public short GetScarletRandMinScarlet()
            {
                if (Enemy.Info.RomVer == RaidRomType.TYPE_B)
                    return -1;
                return (short)RandRateStartScarlet;
            }

            public short GetVioletRandMinViolet()
            {
                if (Enemy.Info.RomVer == RaidRomType.TYPE_A)
                    return -1;
                return (short)RandRateStartViolet;
            }
        }
    }
}
