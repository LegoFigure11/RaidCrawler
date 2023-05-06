using PKHeX.Core;
using System.Text.Json;
using pkNX.Structures.FlatBuffers.Gen9;

namespace RaidCrawler.Core.Structures
{
    public record RaidContainer
    {
        public ITeraRaid[]? GemTeraRaids;
        public ITeraRaid[]? DistTeraRaids;
        public IReadOnlyList<RaidFixedRewards>? BaseFixedRewards;
        public IReadOnlyList<RaidLotteryRewards>? BaseLotteryRewards;
        public IReadOnlyList<DeliveryRaidFixedRewardItem>? DeliveryRaidFixedRewards;
        public IReadOnlyList<DeliveryRaidLotteryRewardItem>? DeliveryRaidLotteryRewards;
        public DeliveryGroupID DeliveryRaidPriority = new() { GroupID = new() };

        public IReadOnlyList<Raid> Raids { get; private set; } = new List<Raid>();
        public IReadOnlyList<ITeraRaid> Encounters { get; private set; } = new List<ITeraRaid>();
        public IReadOnlyList<IReadOnlyList<(int, int, int)>> Rewards { get; private set; } = new List<List<(int, int, int)>>();
        public string Game { get; private set; } = "Scarlet";
        public GameStrings Strings { get; private set; }

        private readonly string[] Raid_data = new[]
        {
            "raid_enemy_01_array.bin",
            "raid_enemy_02_array.bin",
            "raid_enemy_03_array.bin",
            "raid_enemy_04_array.bin",
            "raid_enemy_05_array.bin",
            "raid_enemy_06_array.bin",
        };

        public RaidContainer(string game)
        {
            Game = game;
            Strings = GameInfo.GetStrings(1);
            GemTeraRaids = TeraEncounter.GetAllEncounters(Raid_data);
            BaseFixedRewards = JsonSerializer.Deserialize<IReadOnlyList<RaidFixedRewards>>(Utils.GetStringResource("raid_fixed_reward_item_array.json") ?? "[]");
            BaseLotteryRewards = JsonSerializer.Deserialize<IReadOnlyList<RaidLotteryRewards>>(Utils.GetStringResource("raid_lottery_reward_item_array.json") ?? "[]");
        }

        public int GetRaidCount() => Raids.Count;
        public void ClearRaids() => Raids = new List<Raid>();
        public void SetRaids(IReadOnlyList<Raid> raids) => Raids = raids;

        public int GetEncounterCount() => Encounters.Count;
        public void ClearEncounters() => Encounters = new List<ITeraRaid>();
        public void SetEncounters(IReadOnlyList<ITeraRaid> encs) => Encounters = encs;

        public int GetRewardsCount() => Rewards.Count;
        public void ClearRewards() => Rewards = new List<List<(int, int, int)>>();
        public void SetRewards(IReadOnlyList<IReadOnlyList<(int, int, int)>> rewards) => Rewards = rewards;
        public void SetGame(string game) => Game = game;
    }
}
