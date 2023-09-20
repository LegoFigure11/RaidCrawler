using PKHeX.Core;
using pkNX.Structures.FlatBuffers.Gen9;
using System.Diagnostics;

namespace RaidCrawler.Core.Structures
{
    public class TeraEncounter : ITeraRaid
    {
        public readonly EncounterTera9 Entity; // Raw data
        public readonly ulong DropTableFix;
        public readonly ulong DropTableRandom;
        public readonly ushort[] ExtraMoves;
        public ushort Species => Entity.Species;
        public byte Form => Entity.Form;
        public byte Gender => Entity.Gender;
        public AbilityPermission Ability => Entity.Ability;
        public byte FlawlessIVCount => Entity.FlawlessIVCount;
        public Shiny Shiny => Entity.Shiny;
        public byte Level => Entity.Level;
        public ushort Move1 => Entity.Moves.Move1;
        public ushort Move2 => Entity.Moves.Move2;
        public ushort Move3 => Entity.Moves.Move3;
        public ushort Move4 => Entity.Moves.Move4;
        public byte Stars => Entity.Stars;
        public byte RandRate => Entity.RandRate;
        ushort[] ITeraRaid.ExtraMoves => ExtraMoves;

        public TeraEncounter(
            EncounterTera9 enc,
            ulong fixedRewards,
            ulong lotteryRewards,
            ushort[] extras
        )
        {
            Entity = enc;
            DropTableFix = fixedRewards;
            DropTableRandom = lotteryRewards;
            ExtraMoves = extras
                .Where(z => z != 0 && !Entity.Moves.Contains(z))
                .Distinct()
                .ToArray();
            if (ExtraMoves.Length > 4)
                Debug.WriteLine(ExtraMoves);
        }

        public static TeraEncounter[] GetAllEncounters(string[] resources, TeraRaidMapParent map)
        {
            var data = FlatbufferDumper.DumpBaseROMRaids(resources);
            var encs = EncounterTera9.GetArray(data[0], map);
            var extras = data[1];
            var rewards = TeraDistribution.GetRewardTables(data[2]);
            var result = new TeraEncounter[encs.Length];
            for (int i = 0; i < encs.Length; i++)
                result[i] = new TeraEncounter(
                    encs[i],
                    rewards[i].Item1,
                    rewards[i].Item2,
                    TeraDistribution.GetExtraMoves(extras[(12 * i)..])
                );
            return result;
        }

        public static List<(int, int, int)> GetRewards(
            TeraEncounter enc,
            uint seed,
            int teratype,
            IReadOnlyList<RaidFixedRewards>? fixed_rewards,
            IReadOnlyList<RaidLotteryRewards>? lottery_rewards,
            int boost
        )
        {
            List<(int, int, int)> result = new();
            if (lottery_rewards is null || fixed_rewards is null)
                return result;

            var fixed_table = fixed_rewards.Where(z => z.TableName == enc.DropTableFix).First();
            if (fixed_table is null)
                return result;

            var lottery_table = lottery_rewards
                .Where(z => z.TableName == enc.DropTableRandom)
                .First();
            if (lottery_table is null)
                return result;

            // fixed reward
            for (int i = 0; i < RaidFixedRewards.Count; i++)
            {
                var item = fixed_table.GetReward(i);
                if (item is null || item.Category == 0 && item.ItemID == 0)
                    continue;

                result.Add(
                    (
                        item.ItemID == 0
                            ? item.Category == 2
                                ? Rewards.GetTeraShard(teratype)
                                : Rewards.GetMaterial(enc.Species)
                            : item.ItemID,
                        item.Num,
                        item.SubjectType
                    )
                );
            }

            // lottery reward
            var total = 0;
            for (int i = 0; i < RaidLotteryRewards.RewardItemCount; i++)
                total += lottery_table.GetRewardItem(i)!.Rate;

            var rand = new Xoroshiro128Plus(seed);
            var count = (int)rand.NextInt(100); // sandwich = extra rolls? how does this work? is this even 100?
            count = Rewards.GetRewardCount(count, enc.Stars) + boost;
            for (int i = 0; i < count; i++)
            {
                var roll = (int)rand.NextInt((ulong)total);
                for (int j = 0; j < DeliveryRaidLotteryRewardItem.RewardItemCount; j++)
                {
                    var item = lottery_table.GetRewardItem(j);
                    if (roll < item!.Rate)
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
                                    ? (Rewards.GetTeraShard(teratype), item.Num, 0)
                                    : (item.ItemID, item.Num, 0)
                            );
                        break;
                    }
                    roll -= item!.Rate;
                }
            }
            return Rewards.ReorderRewards(result);
        }
    }
}
