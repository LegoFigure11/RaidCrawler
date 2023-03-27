using PKHeX.Core;

namespace RaidCrawler.Core.Structures
{
    public static class EncounterExtensions
    {
        public static List<(int, int, int)> GetRewards(this ITeraRaid encounter, Raid raid, int sandwich_boost)
        {
            return encounter switch
            {
                TeraDistribution => TeraDistribution.GetRewards((TeraDistribution)encounter, raid.Seed, raid.TeraType, raid.DeliveryRaidFixedRewards, raid.DeliveryRaidLotteryRewards, sandwich_boost),
                TeraEncounter => TeraEncounter.GetRewards((TeraEncounter)encounter, raid.Seed, raid.TeraType, raid.BaseFixedRewards, raid.BaseLotteryRewards, sandwich_boost),
                _ => throw new NotImplementedException($"Unknown encounter for rewards: {encounter.GetType()}"),
            };
        }

        public static GenerateParam9 GetParam(this ITeraRaid encounter)
        {
            var gender = GetGender(encounter);
            if (encounter is TeraDistribution td && td.Entity is EncounterMight9 em)
                return new GenerateParam9(em.Species, gender, em.FlawlessIVCount, 1, 0, 0, em.ScaleType, em.Scale, em.Ability, em.Shiny, em.Nature, em.IVs);
            return new GenerateParam9(encounter.Species, gender, encounter.FlawlessIVCount, 1, 0, 0, 0, 0, encounter.Ability, encounter.Shiny);
        }

        private static byte GetGender(ITeraRaid enc)
        {
            if (enc is not TeraDistribution td || td.Entity is EncounterDist9)
                return PersonalTable.SV.GetFormEntry(enc.Species, enc.Form).Gender;

            if (td.Entity is EncounterMight9 em)
            {
                return em.Gender switch
                {
                    0 => PersonalInfo.RatioMagicMale,
                    1 => PersonalInfo.RatioMagicFemale,
                    2 => PersonalInfo.RatioMagicGenderless,
                    _ => PersonalTable.SV.GetFormEntry(enc.Species, enc.Form).Gender,
                };
            }
            return PersonalTable.SV.GetFormEntry(enc.Species, enc.Form).Gender;
        }
    }
}
