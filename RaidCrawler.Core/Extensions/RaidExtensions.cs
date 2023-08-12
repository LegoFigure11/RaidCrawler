using PKHeX.Core;
using pkNX.Structures.FlatBuffers.Gen9;

namespace RaidCrawler.Core.Structures
{
    public static class RaidExtensions
    {
        public static ITeraRaid? GetTeraEncounter(this Raid raid, RaidContainer container, int progress, int id)
            => raid.IsEvent ? raid.GetDistributionEncounter(container, progress, raid.Flags == 3, id) : raid.GetEncounter(container, progress, raid.IsBlack);

        public static ITeraRaid? GetEncounter(this Raid raid, RaidContainer container, int stage, bool black)
        {
            var clone = new Xoroshiro128Plus(raid.Seed);
            var starcount = black ? 6 : raid.GetStarCount((uint)clone.NextInt(100), stage, false);
            var total = container.Game == "Scarlet" ? GetRateTotalBaseSL(starcount) : GetRateTotalBaseVL(starcount);
            var speciesroll = clone.NextInt((ulong)total);
            if (container.GemTeraRaids is not null)
            {
                foreach (TeraEncounter enc in (TeraEncounter[])container.GemTeraRaids)
                {
                    if (enc.Stars != starcount)
                        continue;

                    var minimum = container.Game == "Scarlet" ? enc.Entity.RandRateMinScarlet : enc.Entity.RandRateMinViolet;
                    if (minimum >= 0 && (uint)((int)speciesroll - minimum) < enc.Entity.RandRate)
                        return enc;
                }
            }
            return null;
        }

        public static ITeraRaid? GetDistributionEncounter(this Raid raid, RaidContainer container, int stage, bool isFixed, int groupid)
        {
            if (stage < 0 || container.DistTeraRaids is null)
                return null;

            if (!isFixed)
            {
                foreach (TeraDistribution enc in container.DistTeraRaids.Cast<TeraDistribution>())
                {
                    if (enc.Entity is not EncounterDist9 encd || enc.DeliveryGroupID != groupid)
                        continue;

                    var total = container.Game == "Scarlet" ? encd.GetRandRateTotalScarlet(stage) : encd.GetRandRateTotalViolet(stage);
                    if (total > 0)
                    {
                        var rand = new Xoroshiro128Plus(raid.Seed);
                        _ = rand.NextInt(100);
                        var val = rand.NextInt(total);
                        var min = container.Game == "Scarlet" ? encd.GetRandRateMinScarlet(stage) : encd.GetRandRateMinViolet(stage);
                        if ((uint)((int)val - min) < enc.RandRate)
                            return enc;
                    }
                }
            }
            else
            {
                foreach (TeraDistribution enc in container.DistTeraRaids.Cast<TeraDistribution>())
                {
                    if (enc.Entity is not EncounterMight9 encm || enc.DeliveryGroupID != groupid)
                        continue;

                    var total = container.Game == "Scarlet" ? encm.GetRandRateTotalScarlet(stage) : encm.GetRandRateTotalViolet(stage);
                    if (total > 0) return enc;
                }
            }
            return null;
        }

        public static (int delivery, int encounter) ReadAllRaids(this RaidContainer container, byte[] data, int storyPrg, int eventPrg, int boost)
        {
            var dbgFile = "raid_dbg.txt";
            if (File.Exists(dbgFile))
                File.Delete(dbgFile);

            var count = data.Length / Raid.SIZE;
            List<int> possible_groups = new();
            if (container.DistTeraRaids is not null)
            {
                foreach (TeraDistribution e in container.DistTeraRaids.Cast<TeraDistribution>())
                {
                    if (TeraDistribution.AvailableInGame(e.Entity, container.Game) && !possible_groups.Contains(e.DeliveryGroupID))
                        possible_groups.Add(e.DeliveryGroupID);
                }
            }

            (int delivery, int encounter) failed = (0, 0);
            List<Raid> newRaids = new();
            List<ITeraRaid> newTera = new();
            List<List<(int, int, int)>> newRewards = new();
            int eventct = 0;
            for (int i = 0; i < count; i++)
            {
                var raid = new Raid(data.AsSpan(i * Raid.SIZE, Raid.SIZE));

                if (raid.Den == 0)
                {
                    eventct++;
                    continue;
                }

                if (!raid.IsValid)
                    continue;

                var progress = raid.IsEvent ? eventPrg : storyPrg;
                var raid_delivery_group_id = -1;
                try
                {
                    raid_delivery_group_id = raid.GetDeliveryGroupID(container.DeliveryRaidPriority, possible_groups, eventct);
                }
                catch (Exception ex)
                {
                    var extra = $"Group ID: {raid_delivery_group_id}\nisFixed: {raid.Flags == 3}\nisBlack: {raid.IsBlack}\nisEvent: {raid.IsEvent}\n\n";
                    var msg = $"{ex.Message}\nDen: {raid.Den}\nProgress: {progress}\nDifficulty: {raid.Difficulty}\n{extra}";
                    File.AppendAllText(dbgFile, msg);
                    failed.delivery++;
                    continue;
                }

                var encounter = raid.GetTeraEncounter(container, progress, raid_delivery_group_id);
                if (encounter is null)
                {
                    var extra = $"Group ID: {raid_delivery_group_id}\nisFixed: {raid.Flags == 3}\nisBlack: {raid.IsBlack}\nisEvent: {raid.IsEvent}\n\n";
                    var msg = $"No encounters found.\nDen: {raid.Den}\nProgress: {progress}\nDifficulty: {raid.Difficulty}\n{extra}";
                    File.AppendAllText(dbgFile, msg);
                    failed.encounter++;
                    continue;
                }

                if (raid.IsEvent) eventct++;

                newRaids.Add(raid);
                newTera.Add(encounter);
                newRewards.Add(encounter.GetRewards(container, raid, boost));
            }

            container.SetRaids(newRaids);
            container.SetEncounters(newTera);
            container.SetRewards(newRewards);
            return failed;
        }

        public static bool CheckIsShiny(this Raid raid, ITeraRaid? enc)
        {
            if (enc is null)
                return raid.IsShiny;

            if (enc.Shiny == Shiny.Never)
                return false;

            if (enc.Shiny.IsShiny())
                return true;
            return raid.IsShiny;
        }

        public static int GetTeraType(this Raid raid, ITeraRaid? encounter)
        {
            if (encounter is null)
                return raid.TeraType;

            if (encounter is TeraDistribution td && td.Entity is ITeraRaid9 gem)
                return (int)gem.TeraType > 1 ? (int)gem.TeraType - 2 : raid.TeraType;
            return raid.TeraType;
        }

        public static int GetStarCount(this Raid _, uint difficulty, int progress, bool isBlack)
        {
            if (isBlack)
                return 6;

            return progress switch
            {
                0 => difficulty switch
                {
                    > 80 => 2,
                    _ => 1,
                },
                1 => difficulty switch
                {
                    > 70 => 3,
                    > 30 => 2,
                    _ => 1,
                },
                2 => difficulty switch
                {
                    > 70 => 4,
                    > 40 => 3,
                    > 20 => 2,
                    _ => 1,
                },
                3 => difficulty switch
                {
                    > 75 => 5,
                    > 40 => 4,
                    _ => 3,
                },
                4 => difficulty switch
                {
                    > 70 => 5,
                    > 30 => 4,
                    _ => 3,
                },
                _ => 1,
            };
        }

        public static int GetDeliveryGroupID(this Raid raid, DeliveryGroupID ids, List<int> possible_groups, int eventct)
        {
            if (!raid.IsEvent)
                return -1;

            // WW/IL re-run has DeliveryGroupID = 3, having a Might7 alongside it conflicts.
            bool special = possible_groups.Contains(3) && raid.Flags != 3;
            var groups = ids.GroupID;

            var mod = special ? 2 : raid.Flags != 3 ? 1 : 0;

            for (int i = 0; i < groups.Table_Length; i++)
            {
                var ct = groups.Table(i) + mod;
                if (special)
                {
                    var result = possible_groups.Find(x => x == ct);
                    if (result > 0)
                        return ct;
                }
                else
                {
                    ct -= mod;
                    if (!possible_groups.Contains(i + 1)) continue;
                    if (eventct < ct) return i + 1;
                    eventct -= ct;
                }
            }
            throw new Exception("Found event out of priority range.");
        }

        private static short GetRateTotalBaseSL(int star) => star switch
        {
            1 => 5800,
            2 => 5300,
            3 => 7400,
            4 => 8800, // Scarlet has one more encounter.
            5 => 9100,
            6 => 6500,
            _ => 0,
        };

        private static short GetRateTotalBaseVL(int star) => star switch
        {
            1 => 5800,
            2 => 5300,
            3 => 7400,
            4 => 8700, // Violet has one less encounter.
            5 => 9100,
            6 => 6500,
            _ => 0,
        };
    }
}
