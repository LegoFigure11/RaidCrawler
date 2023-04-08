using PKHeX.Core;

namespace RaidCrawler.Core.Structures
{
    public static class RaidExtensions
    {
        public static ITeraRaid GetTeraEncounter(this Raid raid, int progress, int id) => raid.IsEvent ? raid.GetDistributionEncounter(progress, raid.Flags == 3, id) : raid.GetEncounter(progress, raid.IsBlack);

        public static ITeraRaid GetEncounter(this Raid raid, int stage, bool black)
        {
            var clone = new Xoroshiro128Plus(raid.Seed);
            var starcount = black ? 6 : raid.GetStarCount((uint)clone.NextInt(100), stage, false);
            var total = raid.Game == "Scarlet" ? GetRateTotalBaseSL(starcount) : GetRateTotalBaseVL(starcount);
            var speciesroll = clone.NextInt((ulong)total);
            if (raid.GemTeraRaids != null)
            {
                foreach (TeraEncounter enc in (TeraEncounter[])raid.GemTeraRaids)
                {
                    if (enc.Stars != starcount)
                        continue;

                    var minimum = raid.Game == "Scarlet" ? enc.Entity.RandRateMinScarlet : enc.Entity.RandRateMinViolet;
                    if (minimum >= 0 && (uint)((int)speciesroll - minimum) < enc.Entity.RandRate)
                        return enc;
                }
            }
            throw new ArgumentNullException($"No encounters found for given raid: den {raid.Den}, stage {stage}");
        }

        public static ITeraRaid GetDistributionEncounter(this Raid raid, int stage, bool isFixed, int groupid)
        {
            if (stage < 0 || raid.DistTeraRaids is null)
                throw new ArgumentNullException($"No encounters found for given distribution raid: den {raid.Den}, stage {stage}");

            if (!isFixed)
            {
                foreach (TeraDistribution enc in raid.DistTeraRaids.Cast<TeraDistribution>())
                {
                    if (enc.Entity is not EncounterDist9 encd || enc.DeliveryGroupID != groupid)
                        continue;

                    var total = raid.Game == "Scarlet" ? encd.GetRandRateTotalScarlet(stage) : encd.GetRandRateTotalViolet(stage);
                    if (total != 0 || isFixed)
                    {
                        if (isFixed)
                            return enc;

                        var rand = new Xoroshiro128Plus(raid.Seed);
                        _ = rand.NextInt(100);
                        var val = rand.NextInt(total);
                        var min = raid.Game == "Scarlet" ? encd.GetRandRateMinScarlet(stage) : encd.GetRandRateMinViolet(stage);
                        if ((uint)((int)val - min) < enc.RandRate)
                            return enc;
                    }
                }
            }
            else
            {
                foreach (TeraDistribution enc in raid.DistTeraRaids.Cast<TeraDistribution>())
                {
                    if (enc.Entity is not EncounterMight9 encm || enc.DeliveryGroupID != groupid)
                        continue;

                    var total = raid.Game == "Scarlet" ? encm.GetRandRateTotalScarlet(stage) : encm.GetRandRateTotalViolet(stage);
                    if (total != 0 || isFixed)
                    {
                        if (isFixed)
                            return enc;

                        var rand = new Xoroshiro128Plus(raid.Seed);
                        _ = rand.NextInt(100);
                        var val = rand.NextInt(total);
                        var min = raid.Game == "Scarlet" ? encm.GetRandRateMinScarlet(stage) : encm.GetRandRateMinViolet(stage);
                        if ((uint)((int)val - min) < enc.RandRate)
                            return enc;
                    }
                }
            }
            throw new ArgumentNullException($"No encounters found for given distribution raid: den {raid.Den}, stage {stage}, isFixed {isFixed}");
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
