using RaidCrawler.Core.Structures;
using System.Reflection;
using System.Text.Json;

namespace RaidCrawler.Tests
{
    public class TestUtil
    {
        private readonly Assembly assembly;

        public TestUtil()
        {
            assembly = Assembly.GetExecutingAssembly();
        }

        private static string GetGame(string resource) => resource.Contains("_VL", StringComparison.Ordinal) ? "Violet" : "Scarlet";
        private static string GetTestResourceName(string name, string resource) => $"{name}.{resource}";

        private byte[] GetBinaryTestResource(string name)
        {
            using var resource = assembly.GetManifestResourceStream(name)!;
            using var reader = new BinaryReader(resource);
            return reader.ReadBytes((int)resource.Length);
        }

        private string GetStringTestResource(string name)
        {
            using var resource = assembly.GetManifestResourceStream(name)!;
            using var reader = new StreamReader(resource);
            return reader.ReadToEnd();
        }

        public (int, RaidContainer?) GetRaidContainer(string path, int storyPrg)
        {
            var game = GetGame(path);
            var container = new Raid(game);
            var eventPrg = Math.Min(storyPrg, 3);

            // Read embedded distribution data.
            var delivery_raid_prio = GetBinaryTestResource(GetTestResourceName(path, "raid_priority_array"));
            (var group_id, var priority) = FlatbufferDumper.DumpDeliveryPriorities(delivery_raid_prio);
            if (priority == 0)
                return (-1, null);

            var delivery_raid_fbs = GetBinaryTestResource(GetTestResourceName(path, "raid_enemy_array"));
            var delivery_fixed_rewards = GetBinaryTestResource(GetTestResourceName(path, "fixed_reward_item_array"));
            var delivery_lottery_rewards = GetBinaryTestResource(GetTestResourceName(path, "lottery_reward_item_array"));

            container.DistTeraRaids = TeraDistribution.GetAllEncounters(delivery_raid_fbs);
            container.DeliveryRaidPriority = group_id;
            container.DeliveryRaidFixedRewards = FlatbufferDumper.DumpFixedRewards(delivery_fixed_rewards);
            container.DeliveryRaidLotteryRewards = FlatbufferDumper.DumpLotteryRewards(delivery_lottery_rewards);

            // Read embedded base data and read all raids.
            var baseData = GetBinaryTestResource(GetTestResourceName(path, "base"));
            var failed = container.ReadAllRaids(baseData, storyPrg, eventPrg, 0);
            return (failed, container.Container);
        }

        public IReadOnlyList<RaidFilter> GetRaidFilter(string path)
        {
            var text = GetStringTestResource(path);
            return JsonSerializer.Deserialize<List<RaidFilter>>(text)!;
        }
    }
}
