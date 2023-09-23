using PKHeX.Core;
using pkNX.Structures.FlatBuffers.Gen9;
using System.Diagnostics;
using static System.Buffers.Binary.BinaryPrimitives;

namespace RaidCrawler.Core.Structures
{
    public class TeraDistribution : ITeraRaid
    {
        public readonly EncounterDist9 Entity; // Raw data
        public readonly ulong DropTableFix;
        public readonly ulong DropTableRandom;
        public readonly ushort[] ExtraMoves;
        public readonly sbyte DeliveryGroupID;

        public ushort Species => Entity.Species;
        public byte Form => Entity.Form;
        public byte Gender => Entity.Gender;
        public AbilityPermission Ability => Entity.Ability;
        public byte FlawlessIVCount => Entity.FlawlessIVCount;
        public Shiny Shiny => Entity.Shiny;
        public Nature Nature => Nature.Random;
        public byte Level => Entity.Level;
        public IndividualValueSet IVs => Entity.IVs;
        public ushort Move1 => Entity.Moves.Move1;
        public ushort Move2 => Entity.Moves.Move2;
        public ushort Move3 => Entity.Moves.Move3;
        public ushort Move4 => Entity.Moves.Move4;
        public byte Stars => Entity.Stars;
        public byte RandRate => Entity.RandRate;
        ushort[] ITeraRaid.ExtraMoves => ExtraMoves;

        public static bool AvailableInGame(ITeraRaid9 enc, string game)
        {
            if (enc is EncounterDist9 encd)
            {
                if (game == "Scarlet")
                    return encd.RandRate0TotalScarlet
                            + encd.RandRate1TotalScarlet
                            + encd.RandRate2TotalScarlet
                            + encd.RandRate3TotalScarlet
                        != 0;
                else if (game == "Violet")
                    return encd.RandRate0TotalViolet
                            + encd.RandRate1TotalViolet
                            + encd.RandRate2TotalViolet
                            + encd.RandRate3TotalViolet
                        != 0;
                return false;
            }
            else if (enc is EncounterMight9 encm)
            {
                if (game == "Scarlet")
                    return encm.RandRate0TotalScarlet
                            + encm.RandRate1TotalScarlet
                            + encm.RandRate2TotalScarlet
                            + encm.RandRate3TotalScarlet
                        != 0;
                else if (game == "Violet")
                    return encm.RandRate0TotalViolet
                            + encm.RandRate1TotalViolet
                            + encm.RandRate2TotalViolet
                            + encm.RandRate3TotalViolet
                        != 0;
                return false;
            }
            return false;
        }

        public TeraDistribution(
            EncounterDist9 enc,
            ulong fixedRewards,
            ulong lotteryRewards,
            ushort[] extras,
            sbyte group
        )
        {
            Entity = enc;
            DropTableFix = fixedRewards;
            DropTableRandom = lotteryRewards;
            ExtraMoves = extras
                .Where(z => z != 0 && !Entity.Moves.Contains(z))
                .Distinct()
                .ToArray();
            DeliveryGroupID = group;
            if (ExtraMoves.Length > 4)
                Debug.WriteLine(ExtraMoves);
        }

        public static TeraDistribution[] GetAllEncounters(byte[] encounters)
        {
            var all = FlatbufferDumper.DumpDistributionRaids(encounters);
            var type2 = EncounterDist9.GetArray(all[0]);
            var rewards2 = GetRewardTables(all[2]);
            var extra2 = all[4];
            var group2 = all[6];
            var result = new TeraDistribution[type2.Length];
            for (int i = 0; i < result.Length; i++)
                result[i] = new TeraDistribution(
                    type2[i],
                    rewards2[i].Item1,
                    rewards2[i].Item2,
                    GetExtraMoves(extra2[(12 * i)..]),
                    (sbyte)group2[i]
                );
            return result;
        }

        public static (ulong, ulong)[] GetRewardTables(byte[] rewards)
        {
            var count = rewards.Length / 0x10;
            var result = new (ulong, ulong)[count];
            for (int i = 0; i < result.Length; i++)
                result[i] = (
                    ReadUInt64LittleEndian(rewards.AsSpan()[(0x10 * i)..]),
                    ReadUInt64LittleEndian(rewards.AsSpan()[(0x10 * i + 0x8)..])
                );
            return result;
        }

        public static ushort[] GetExtraMoves(byte[] extra)
        {
            var result = new ushort[6];
            for (int i = 0; i < result.Length; i++)
                result[i] = ReadUInt16LittleEndian(extra.AsSpan()[(0x2 * i)..]);
            return result;
        }

        public static List<(int, int, int)> GetRewards(
            TeraDistribution enc,
            uint seed,
            int teraType,
            IReadOnlyList<DeliveryRaidFixedRewardItem>? fixedRewards,
            IReadOnlyList<DeliveryRaidLotteryRewardItem>? lotteryRewards,
            int boost
        )
        {
            List<(int, int, int)> result = new();
            if (lotteryRewards is null)
                return result;

            if (fixedRewards is null)
                return result;

            var fixedTable = fixedRewards.Where(z => z.TableName == enc.DropTableFix).First();
            if (fixedTable is null)
                return result;

            var lotteryTable = lotteryRewards
                .Where(z => z.TableName == enc.DropTableRandom)
                .First();
            if (lotteryTable is null)
                return result;

            // fixed reward
            for (int i = 0; i < DeliveryRaidFixedRewardItem.Count; i++)
            {
                var item = fixedTable.GetReward(i);
                if (item.Category == 0 && item.ItemID == 0)
                    continue;
                result.Add(
                    (
                        item.ItemID == 0
                            ? item.Category == 2
                                ? Rewards.GetTeraShard(teraType)
                                : Rewards.GetMaterial(enc.Species)
                            : item.ItemID,
                        item.Num,
                        item.SubjectType
                    )
                );
            }

            // lottery reward
            var total = 0;
            for (int i = 0; i < DeliveryRaidLotteryRewardItem.RewardItemCount; i++)
                total += lotteryTable.GetRewardItem(i).Rate;

            var rand = new Xoroshiro128Plus(seed);
            var count = (int)rand.NextInt(100);
            count = Rewards.GetRewardCount(count, enc.Stars) + boost;

            for (int i = 0; i < count; i++)
            {
                var roll = (int)rand.NextInt((ulong)total);
                for (int j = 0; j < DeliveryRaidLotteryRewardItem.RewardItemCount; j++)
                {
                    var item = lotteryTable.GetRewardItem(j);
                    if (roll < item.Rate)
                    {
                        if (item.Category == 0)
                            result.Add((item.ItemID, item.Num, 0));
                        else if (item.Category == 1)
                            result.Add(
                                item.ItemID == 0
                                    ? (Rewards.GetMaterial(enc.Species), item.Num, 0)
                                    : (item.ItemID, item.Num, 0)
                            );
                        else
                            result.Add(
                                item.ItemID == 0
                                    ? (Rewards.GetTeraShard(teraType), item.Num, 0)
                                    : (item.ItemID, item.Num, 0)
                            );
                        break;
                    }
                    roll -= item.Rate;
                }
            }
            return Rewards.ReorderRewards(result);
        }
    }

    public class TeraMight : ITeraRaid
    {
        public readonly EncounterMight9 Entity; // Raw data
        public readonly ulong DropTableFix;
        public readonly ulong DropTableRandom;
        public readonly ushort[] ExtraMoves;
        public readonly sbyte DeliveryGroupID;

        public ushort Species => Entity.Species;
        public byte Form => Entity.Form;
        public byte Gender => Entity.Gender;
        public AbilityPermission Ability => Entity.Ability;
        public byte FlawlessIVCount => Entity.FlawlessIVCount;
        public Shiny Shiny => Entity.Shiny;
        public Nature Nature => Entity.Nature;
        public byte Level => Entity.Level;
        public IndividualValueSet IVs => Entity.IVs;
        public ushort Move1 => Entity.Moves.Move1;
        public ushort Move2 => Entity.Moves.Move2;
        public ushort Move3 => Entity.Moves.Move3;
        public ushort Move4 => Entity.Moves.Move4;
        public byte Stars => Entity.Stars;
        public byte RandRate => Entity.RandRate;
        ushort[] ITeraRaid.ExtraMoves => ExtraMoves;

        public static bool AvailableInGame(ITeraRaid9 enc, string game)
        {
            if (enc is EncounterDist9 encd)
            {
                if (game == "Scarlet")
                    return encd.RandRate0TotalScarlet
                            + encd.RandRate1TotalScarlet
                            + encd.RandRate2TotalScarlet
                            + encd.RandRate3TotalScarlet
                        != 0;
                else if (game == "Violet")
                    return encd.RandRate0TotalViolet
                            + encd.RandRate1TotalViolet
                            + encd.RandRate2TotalViolet
                            + encd.RandRate3TotalViolet
                        != 0;
                return false;
            }
            else if (enc is EncounterMight9 encm)
            {
                if (game == "Scarlet")
                    return encm.RandRate0TotalScarlet
                            + encm.RandRate1TotalScarlet
                            + encm.RandRate2TotalScarlet
                            + encm.RandRate3TotalScarlet
                        != 0;
                else if (game == "Violet")
                    return encm.RandRate0TotalViolet
                            + encm.RandRate1TotalViolet
                            + encm.RandRate2TotalViolet
                            + encm.RandRate3TotalViolet
                        != 0;
                return false;
            }
            return false;
        }

        public TeraMight(
            EncounterMight9 enc,
            ulong fixedRewards,
            ulong lotteryRewards,
            ushort[] extras,
            sbyte group
        )
        {
            Entity = enc;
            DropTableFix = fixedRewards;
            DropTableRandom = lotteryRewards;
            ExtraMoves = extras
                .Where(z => z != 0 && !Entity.Moves.Contains(z))
                .Distinct()
                .ToArray();
            DeliveryGroupID = group;
            if (ExtraMoves.Length > 4)
                Debug.WriteLine(ExtraMoves);
        }

        public static TeraMight[] GetAllEncounters(byte[] encounters)
        {
            var all = FlatbufferDumper.DumpDistributionRaids(encounters);
            var type3 = EncounterMight9.GetArray(all[1]);
            var rewards3 = GetRewardTables(all[3]);
            var extra3 = all[5];
            var group3 = all[7];
            var result = new TeraMight[type3.Length];
            for (int i = 0; i < result.Length; i++)
                result[i] = new TeraMight(
                    type3[i],
                    rewards3[i].Item1,
                    rewards3[i].Item2,
                    GetExtraMoves(extra3[(12 * i)..]),
                    (sbyte)group3[i]
                );
            return result;
        }

        public static (ulong, ulong)[] GetRewardTables(byte[] rewards)
        {
            var count = rewards.Length / 0x10;
            var result = new (ulong, ulong)[count];
            for (int i = 0; i < result.Length; i++)
                result[i] = (
                    ReadUInt64LittleEndian(rewards.AsSpan()[(0x10 * i)..]),
                    ReadUInt64LittleEndian(rewards.AsSpan()[(0x10 * i + 0x8)..])
                );
            return result;
        }

        public static ushort[] GetExtraMoves(byte[] extra)
        {
            var result = new ushort[6];
            for (int i = 0; i < result.Length; i++)
                result[i] = ReadUInt16LittleEndian(extra.AsSpan()[(0x2 * i)..]);
            return result;
        }

        public static List<(int, int, int)> GetRewards(
            TeraMight enc,
            uint seed,
            int teraType,
            IReadOnlyList<DeliveryRaidFixedRewardItem>? fixedRewards,
            IReadOnlyList<DeliveryRaidLotteryRewardItem>? lotteryRewards,
            int boost
        )
        {
            List<(int, int, int)> result = new();
            if (lotteryRewards is null)
                return result;

            if (fixedRewards is null)
                return result;

            var fixedTable = fixedRewards.Where(z => z.TableName == enc.DropTableFix).First();
            if (fixedTable is null)
                return result;

            var lotteryTable = lotteryRewards
                .Where(z => z.TableName == enc.DropTableRandom)
                .First();
            if (lotteryTable is null)
                return result;

            // fixed reward
            for (int i = 0; i < DeliveryRaidFixedRewardItem.Count; i++)
            {
                var item = fixedTable.GetReward(i);
                if (item.Category == 0 && item.ItemID == 0)
                    continue;
                result.Add(
                    (
                        item.ItemID == 0
                            ? item.Category == 2
                                ? Rewards.GetTeraShard(teraType)
                                : Rewards.GetMaterial(enc.Species)
                            : item.ItemID,
                        item.Num,
                        item.SubjectType
                    )
                );
            }

            // lottery reward
            var total = 0;
            for (int i = 0; i < DeliveryRaidLotteryRewardItem.RewardItemCount; i++)
                total += lotteryTable.GetRewardItem(i).Rate;

            var rand = new Xoroshiro128Plus(seed);
            var count = (int)rand.NextInt(100);
            count = Rewards.GetRewardCount(count, enc.Stars) + boost;

            for (int i = 0; i < count; i++)
            {
                var roll = (int)rand.NextInt((ulong)total);
                for (int j = 0; j < DeliveryRaidLotteryRewardItem.RewardItemCount; j++)
                {
                    var item = lotteryTable.GetRewardItem(j);
                    if (roll < item.Rate)
                    {
                        if (item.Category == 0)
                            result.Add((item.ItemID, item.Num, 0));
                        else if (item.Category == 1)
                            result.Add(
                                item.ItemID == 0
                                    ? (Rewards.GetMaterial(enc.Species), item.Num, 0)
                                    : (item.ItemID, item.Num, 0)
                            );
                        else
                            result.Add(
                                item.ItemID == 0
                                    ? (Rewards.GetTeraShard(teraType), item.Num, 0)
                                    : (item.ItemID, item.Num, 0)
                            );
                        break;
                    }
                    roll -= item.Rate;
                }
            }
            return Rewards.ReorderRewards(result);
        }
    }
}
