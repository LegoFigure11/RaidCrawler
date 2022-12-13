#nullable disable
using FlatSharp.Attributes;
using Newtonsoft.Json;
using System.ComponentModel;

namespace RaidCrawler.Structures
{
    public class Rewards
    {
        public static readonly int[][] RewardSlots =
        {
            new [] { 4, 5, 6, 7, 8 },
            new [] { 4, 5, 6, 7, 8 },
            new [] { 5, 6, 7, 8, 9 },
            new [] { 5, 6, 7, 8, 9 },
            new [] { 6, 7, 8, 9, 10 },
            new [] { 7, 8, 9, 10, 11 },
            new [] { 7, 8, 9, 10, 11 },
        };

        public static readonly int[] RareRewards = { 4, 645, 1606, 1904, 1905, 1906, 1907, 1908, 795 };

        public static int GetRewardCount(int random, int stars)
        {
            return random switch
            {
                < 10 => RewardSlots[stars-1][0],
                < 40 => RewardSlots[stars-1][1],
                < 70 => RewardSlots[stars-1][2],
                < 90 => RewardSlots[stars-1][3],
                _ => RewardSlots[stars-1][4],
            };
        }

        public static List<(int, int, int)> ReorderRewards(List<(int, int, int)> rewards)
        {
            var rares = new List<(int, int, int)>();
            var commons = new List<(int, int, int)>();
            for (int i = 0; i < rewards.Count; i++)
            {
                if (RareRewards.Contains(rewards[i].Item1))
                    rares.Add(rewards[i]);
                else commons.Add(rewards[i]);
            }
            rares.AddRange(commons);
            return rares;
        }

        public static List<(int, int, int)>? GetRewards(Raid raid, int story_progress, int event_progress, int sandwich_boost)
        {
            var progress = raid.IsEvent ? event_progress : story_progress;
            var encounter = raid.Encounter(progress);

            var rewards = encounter switch
            {
                TeraDistribution => TeraDistribution.GetRewards((TeraDistribution)encounter, raid.Seed, Raid.DeliveryRaidFixedRewards, Raid.DeliveryRaidLotteryRewards, sandwich_boost),
                TeraEncounter => TeraEncounter.GetRewards((TeraEncounter)encounter, raid.Seed, Raid.BaseFixedRewards, Raid.BaseLotteryRewards, sandwich_boost),
                _ => null,
            };
            return rewards;
        }
    }

    public class RaidFixedRewards
    {
        public ulong TableName { get; set; }
        public RaidFixedRewardItemInfo RewardItem00 { get; set; }
        public RaidFixedRewardItemInfo RewardItem01 { get; set; }
        public RaidFixedRewardItemInfo RewardItem02 { get; set; }
        public RaidFixedRewardItemInfo RewardItem03 { get; set; }
        public RaidFixedRewardItemInfo RewardItem04 { get; set; }
        public RaidFixedRewardItemInfo RewardItem05 { get; set; }
        public RaidFixedRewardItemInfo RewardItem06 { get; set; }
        public RaidFixedRewardItemInfo RewardItem07 { get; set; }
        public RaidFixedRewardItemInfo RewardItem08 { get; set; }
        public RaidFixedRewardItemInfo RewardItem09 { get; set; }
        public RaidFixedRewardItemInfo RewardItem10 { get; set; }
        public RaidFixedRewardItemInfo RewardItem11 { get; set; }
        public RaidFixedRewardItemInfo RewardItem12 { get; set; }
        public RaidFixedRewardItemInfo RewardItem13 { get; set; }
        public RaidFixedRewardItemInfo RewardItem14 { get; set; }

        public const int Count = 15;

        public RaidFixedRewardItemInfo GetReward(int index) => index switch
        {
            00 => RewardItem00,
            01 => RewardItem01,
            02 => RewardItem02,
            03 => RewardItem03,
            04 => RewardItem04,
            05 => RewardItem05,
            06 => RewardItem06,
            07 => RewardItem07,
            08 => RewardItem08,
            09 => RewardItem09,
            10 => RewardItem10,
            11 => RewardItem11,
            12 => RewardItem12,
            13 => RewardItem13,
            14 => RewardItem14,
            _ => throw new ArgumentOutOfRangeException(nameof(index))
        };

    }

    public class RaidLotteryRewards
    {
        public ulong TableName { get; set; }
        public RaidLotteryRewardItemInfo RewardItem00 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem01 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem02 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem03 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem04 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem05 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem06 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem07 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem08 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem09 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem10 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem11 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem12 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem13 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem14 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem15 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem16 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem17 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem18 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem19 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem20 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem21 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem22 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem23 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem24 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem25 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem26 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem27 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem28 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem29 { get; set; }

        public const int RewardItemCount = 30;

        // Get reward item from index
        public RaidLotteryRewardItemInfo GetRewardItem(int index) => index switch
        {
            00 => RewardItem00,
            01 => RewardItem01,
            02 => RewardItem02,
            03 => RewardItem03,
            04 => RewardItem04,
            05 => RewardItem05,
            06 => RewardItem06,
            07 => RewardItem07,
            08 => RewardItem08,
            09 => RewardItem09,
            10 => RewardItem10,
            11 => RewardItem11,
            12 => RewardItem12,
            13 => RewardItem13,
            14 => RewardItem14,
            15 => RewardItem15,
            16 => RewardItem16,
            17 => RewardItem17,
            18 => RewardItem18,
            19 => RewardItem19,
            20 => RewardItem20,
            21 => RewardItem21,
            22 => RewardItem22,
            23 => RewardItem23,
            24 => RewardItem24,
            25 => RewardItem25,
            26 => RewardItem26,
            27 => RewardItem27,
            28 => RewardItem28,
            29 => RewardItem29,
            _ => throw new ArgumentOutOfRangeException(nameof(index), index, null)
        };
    }

    [JsonObject]
    [FlatBufferTable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class RaidLotteryRewardItemInfo
    {
        [FlatBufferItem(0)] public int Category { get; set; }
        [FlatBufferItem(1)] public int ItemID { get; set; }
        [FlatBufferItem(2)] public sbyte Num { get; set; }
        [FlatBufferItem(3)] public int Rate { get; set; }
        [FlatBufferItem(4)] public bool RareItemFlag { get; set; }
    }

    [JsonObject]
    [FlatBufferTable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class RaidFixedRewardItemInfo
    {
        [FlatBufferItem(0)] public int Category { get; set; }
        [FlatBufferItem(1)] public int SubjectType { get; set; }
        [FlatBufferItem(2)] public int ItemID { get; set; }
        [FlatBufferItem(3)] public sbyte Num { get; set; }
    }
}
