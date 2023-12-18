using PKHeX.Core;
using pkNX.Structures.FlatBuffers.Gen9;

namespace RaidCrawler.Core.Structures;

public static class RaidExtensions
{
    public static ITeraRaid? GetTeraEncounter(this Raid raid, RaidContainer container, int progress, int id)
    {
        if (raid.IsEvent)
            return raid.GetDistributionEncounter(container, progress, raid.Flags == 3, id);
        return raid.MapParent switch
        {
            TeraRaidMapParent.Paldea => raid.GetEncounterBase(container, progress, raid.IsBlack),
            TeraRaidMapParent.Kitakami => raid.GetEncounterKitakami(container, progress, raid.IsBlack),
            _ => raid.GetEncounterBlueberry(container, progress, raid.IsBlack),
        };
    }

    public static ITeraRaid? GetEncounterBase(this Raid raid, RaidContainer container, int progress, bool isBlack)
    {
        var clone = new Xoroshiro128Plus(raid.Seed);
        var starCount = isBlack
            ? 6
            : raid.GetStarCount((uint)clone.NextInt(100), progress, false);
        var total =
            container.Game == "Scarlet"
                ? GetRateTotalBaseSL(starCount)
                : GetRateTotalBaseVL(starCount);
        var speciesRoll = clone.NextInt((ulong)total);
        if (container.GemTeraRaidsBase is not null)
        {
            foreach (TeraEncounter enc in container.GemTeraRaidsBase)
            {
                if (enc.Stars != starCount)
                    continue;

                var minimum =
                    container.Game == "Scarlet"
                        ? enc.Entity.RandRateMinScarlet
                        : enc.Entity.RandRateMinViolet;
                if (minimum >= 0 && (uint)((int)speciesRoll - minimum) < enc.Entity.RandRate)
                    return enc;
            }
        }
        return null;
    }

    public static ITeraRaid? GetEncounterKitakami(this Raid raid, RaidContainer container, int progress, bool isBlack)
    {
        var clone = new Xoroshiro128Plus(raid.Seed);
        var starCount = isBlack
            ? 6
            : raid.GetStarCount((uint)clone.NextInt(100), progress, false);
        var total =
            container.Game == "Scarlet"
                ? GetRateTotalKitakamiSL(starCount)
                : GetRateTotalKitakamiVL(starCount);
        var speciesRoll = clone.NextInt((ulong)total);
        if (container.GemTeraRaidsKitakami is not null)
        {
            foreach (TeraEncounter enc in container.GemTeraRaidsKitakami)
            {
                if (enc.Stars != starCount)
                    continue;

                var minimum =
                    container.Game == "Scarlet"
                        ? enc.Entity.RandRateMinScarlet
                        : enc.Entity.RandRateMinViolet;
                if (minimum >= 0 && (uint)((int)speciesRoll - minimum) < enc.Entity.RandRate)
                    return enc;
            }
        }
        return null;
    }

    public static ITeraRaid? GetEncounterBlueberry(this Raid raid, RaidContainer container, int progress, bool isBlack)
    {
        var clone = new Xoroshiro128Plus(raid.Seed);
        var starCount = isBlack
            ? 6
            : raid.GetStarCount((uint)clone.NextInt(100), progress, false);
        var total = GetRateTotalBlueberry(starCount);
        var speciesRoll = clone.NextInt((ulong)total);
        if (container.GemTeraRaidsBlueberry is not null)
        {
            foreach (TeraEncounter enc in container.GemTeraRaidsBlueberry)
            {
                if (enc.Stars != starCount)
                    continue;

                var minimum =
                    container.Game == "Scarlet"
                        ? enc.Entity.RandRateMinScarlet
                        : enc.Entity.RandRateMinViolet;
                if (minimum >= 0 && (uint)((int)speciesRoll - minimum) < enc.Entity.RandRate)
                    return enc;
            }
        }
        return null;
    }

    public static ITeraRaid? GetDistributionEncounter(this Raid raid, RaidContainer container, int progress, bool isFixed, int groupID)
    {
        if (progress < 0)
            return null;

        if (!isFixed)
        {
            if (container.DistTeraRaids == null)
                return null;

            foreach (TeraDistribution enc in container.DistTeraRaids)
            {
                if (enc.DeliveryGroupID != groupID)
                    continue;

                var total =
                    container.Game == "Scarlet"
                        ? enc.Entity.GetRandRateTotalScarlet(progress)
                        : enc.Entity.GetRandRateTotalViolet(progress);
                if (total > 0)
                {
                    var rand = new Xoroshiro128Plus(raid.Seed);
                    _ = rand.NextInt(100);
                    var val = rand.NextInt(total);
                    var min =
                        container.Game == "Scarlet"
                            ? enc.Entity.GetRandRateMinScarlet(progress)
                            : enc.Entity.GetRandRateMinViolet(progress);
                    if ((uint)((int)val - min) < enc.RandRate)
                        return enc;
                }
            }
        }
        else
        {
            if (container.MightTeraRaids == null)
                return null;

            foreach (TeraMight enc in container.MightTeraRaids)
            {
                if (enc.DeliveryGroupID != groupID)
                    continue;

                var total =
                    container.Game == "Scarlet"
                        ? enc.Entity.GetRandRateTotalScarlet(progress)
                        : enc.Entity.GetRandRateTotalViolet(progress);
                if (total > 0)
                    return enc;
            }
        }
        return null;
    }

    public static (int delivery, int encounter) ReadAllRaids(this RaidContainer container, byte[] data, int storyPrg, int eventPrg, int boost, TeraRaidMapParent type)
    {
        var dbgFile = $"raid_dbg_{type}.txt";
        if (File.Exists(dbgFile))
            File.Delete(dbgFile);

        var count = data.Length / Raid.SIZE;
        List<int> possibleGroups = [];
        if (container.DistTeraRaids is not null)
        {
            foreach (TeraDistribution e in container.DistTeraRaids)
            {
                if (TeraDistribution.AvailableInGame(e.Entity, container.Game) && !possibleGroups.Contains(e.DeliveryGroupID))
                    possibleGroups.Add(e.DeliveryGroupID);
            }
        }

        if (container.MightTeraRaids is not null)
        {
            foreach (TeraMight e in container.MightTeraRaids)
            {
                if (TeraMight.AvailableInGame(e.Entity, container.Game) && !possibleGroups.Contains(e.DeliveryGroupID))
                    possibleGroups.Add(e.DeliveryGroupID);
            }
        }

        (int delivery, int encounter) failed = (0, 0);
        List<Raid> newRaids = [];
        List<ITeraRaid> newTera = [];
        List<List<(int, int, int)>> newRewards = [];
        int eventCount = 0;
        for (int i = 0; i < count; i++)
        {
            var slice = data.AsSpan(i * Raid.SIZE, Raid.SIZE);
            var raid = new Raid(slice, type);

            if (raid.Den == 0)
            {
                eventCount++;
                continue;
            }

            if (!raid.IsValid)
                continue;

            var progress = raid.IsEvent ? eventPrg : storyPrg;
            var raidDeliveryGroupID = -1;
            try
            {
                raidDeliveryGroupID = raid.GetDeliveryGroupID(container.DeliveryRaidPriority, possibleGroups, eventCount);
            }
            catch (Exception ex)
            {
                var extra = $"Group ID: {raidDeliveryGroupID}\nisFixed: {raid.Flags == 3}\nisBlack: {raid.IsBlack}\nisEvent: {raid.IsEvent}\n\n";
                var msg = $"{ex.Message}\nDen: {raid.Den}\nProgress: {progress}\nDifficulty: {raid.Difficulty}\n{extra}";
                File.AppendAllText(dbgFile, msg);
                failed.delivery++;
                continue;
            }

            var encounter = raid.GetTeraEncounter(container, progress, raidDeliveryGroupID);
            if (encounter is null)
            {
                var extra = $"Group ID: {raidDeliveryGroupID}\nisFixed: {raid.Flags == 3}\nisBlack: {raid.IsBlack}\nisEvent: {raid.IsEvent}\n\n";
                var msg = $"No encounters found.\nDen: {raid.Den}\nProgress: {progress}\nDifficulty: {raid.Difficulty}\n{extra}";
                File.AppendAllText(dbgFile, msg);
                failed.encounter++;
                continue;
            }

            if (raid.IsEvent)
                eventCount++;

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

        if (encounter is TeraDistribution { Entity: ITeraRaid9 d })
            return (int)d.TeraType > 1 ? (int)d.TeraType - 2 : raid.TeraType;

        if (encounter is TeraMight { Entity: ITeraRaid9 m })
            return (int)m.TeraType > 1 ? (int)m.TeraType - 2 : raid.TeraType;

        return raid.TeraType;
    }

    public static int GetStarCount(this Raid _, uint difficulty, int progress, bool isBlack)
    {
        if (isBlack)
            return 6;

        return GetStarCount(difficulty, progress);
    }

    private static int GetStarCount(uint difficulty, int progress) => progress switch
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

    public static int GetDeliveryGroupID(this Raid raid, DeliveryGroupID ids, List<int> possibleGroups, int eventCount)
    {
        if (!raid.IsEvent)
            return -1;

        // WW/IL re-run has DeliveryGroupID = 3, having a Might7 alongside it conflicts.
        var groups = ids.GroupID;

        for (int i = 0; i < groups.Table_Length; i++)
        {
            var count = groups.Table(i);
            if (!possibleGroups.Contains(i + 1))
                continue;
            if (eventCount < count)
                return i + 1;
            eventCount -= count;
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

    private static short GetRateTotalKitakamiSL(int star) => star switch
    {
        1 => 1500,
        2 => 1500,
        3 => 2500,
        4 => 2100,
        5 => 2250,
        6 => 2475, // Scarlet has one less encounter
        _ => 0,
    };

    private static short GetRateTotalKitakamiVL(int star) => star switch
    {
        1 => 1500,
        2 => 1500,
        3 => 2500,
        4 => 2100,
        5 => 2250,
        6 => 2574, // Violet has one more encounter
        _ => 0,
    };

    private static short GetRateTotalBlueberry(int star) => star switch
    {
        // Both games have the same number of encounters
        1 => 1100,
        2 => 1100,
        3 => 2000,
        4 => 1900,
        5 => 2100,
        6 => 2600,
        _ => 0,
    };
}
