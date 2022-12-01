using PKHeX.Core;
using RaidCrawler.Properties;
namespace RaidCrawler.Structures
{
    public static class RaidFilters
    {
        public static Species? Species = Settings.Default.SpeciesEnabled ? (Species)Settings.Default.SpeciesFilter : null;
        public static int? Stars = Settings.Default.StarsEnabled ? Settings.Default.StarsFilter : null;
        public static bool Shiny = Settings.Default.SearchTillShiny;
        public static Nature? Nature = Settings.Default.NatureEnabled ? (Nature)Settings.Default.NatureFilter : null;
        public static MoveType? TeraType = Settings.Default.TeraEnabled ? (MoveType)Settings.Default.TeraFilter : null;
        public static int IVBin = Settings.Default.IVBin;
        public static int IVVals = Settings.Default.IVVals;
        public static bool SatisfyAny = Settings.Default.SatisfyAny;
        public static bool SpeciesFixed = Settings.Default.SpeciesFixed;
        public static bool NatureFixed = Settings.Default.NatureFixed;
        public static bool StarFixed = Settings.Default.StarFixed;
        public static bool TeraFixed = Settings.Default.TeraFixed;

        public static bool IsFilterSet()
        {
            if (Species == null && Stars == null && Shiny == false && Nature == null)
                return false;
            return true;
        }

        public static bool IsSpeciesSatisfied(Raid raid, int StoryProgress, int EventProgress)
        {
            if (Species == null)
                return SatisfyAny ? false : true;
            var progress = raid.IsEvent ? EventProgress : StoryProgress;
            ITeraRaid? encounter = raid.Encounter(progress);
            if (encounter == null)
                return false;
            return encounter.Species == (int)Species;
        }

        public static bool IsStarsSatisfied(Raid raid, int StoryProgress)
        {
            if (Stars == null)
                return SatisfyAny ? false : true;
            return (int)Stars == Raid.GetStarCount(raid.Difficulty, StoryProgress, raid.IsBlack);
        }

        public static bool IsShinySatisfied(Raid raid)
        {
            if (Shiny == false)
                return SatisfyAny ? false : true;
            return raid.IsShiny == true;
        }

        public static bool IsTeraTypeSatisfied(Raid raid)
        {
            if (TeraType == null)
                return SatisfyAny ? false : true;
            return raid.TeraType == (int)TeraType;
        }

        public static bool IsNatureSatisfied(Raid raid, int StoryProgress, int EventProgress)
        {
            if (Nature == null)
                return SatisfyAny ? false : true;
            var progress = raid.IsEvent ? EventProgress : StoryProgress;
            ITeraRaid? encounter = raid.Encounter(progress);
            if (encounter == null)
                return false;
            var pi = PersonalTable.SV.GetFormEntry(encounter.Species, encounter.Form);
            var StarCount = Raid.GetStarCount(raid.Difficulty, StoryProgress, raid.IsBlack);
            var param = new GenerateParam9((byte)pi.Gender, (byte)(StarCount - 1), 1, 0, 0, 0, encounter.Ability, encounter.Shiny);
            var blank = new PK9();
            blank.Species = encounter.Species;
            blank.Form = encounter.Form;
            Encounter9RNG.GenerateData(blank, param, EncounterCriteria.Unrestricted, raid.Seed);
            return blank.Nature == (int)Nature;
        }

        public static bool IsIVsSatisfied(Raid raid, int StoryProgress, int EventProgress)
        {
            if (IVBin == 0)
                return SatisfyAny ? false : true;
            var progress = raid.IsEvent ? EventProgress : StoryProgress;
            ITeraRaid? encounter = raid.Encounter(progress);
            if (encounter == null)
                return false;
            var ivs = raid.GetIVs(raid.Seed, encounter.FlawlessIVCount);
            for (int i = 0; i < 6; i++)
            {
                if (ivs[i] != ((IVVals >> (i * 5)) & 31) && ((IVBin >> i) & 1) == 1)
                    return false;
            }
            return true;
        }

        public static bool FilterSatisfied(Raid raid, int StoryProgress, int EventProgress)
        {
            var speciessatisfied = Species != null && SpeciesFixed ? IsSpeciesSatisfied(raid, StoryProgress, EventProgress) : true;
            var naturesatisfied = Nature != null && NatureFixed ? IsNatureSatisfied(raid, StoryProgress, EventProgress) : true;
            var starsatisfied = Stars != null && StarFixed ? IsStarsSatisfied(raid, StoryProgress) : true;
            var terasatisfied = TeraType != null && TeraFixed ? IsTeraTypeSatisfied(raid) : true;
            var fixedsatisfied = speciessatisfied && naturesatisfied && starsatisfied && terasatisfied;
            if (!fixedsatisfied)
                return false;
            var satisfied = false;
            if (SatisfyAny)
            {
                satisfied = IsIVsSatisfied(raid, StoryProgress, EventProgress) || IsShinySatisfied(raid);
                if (!SpeciesFixed)
                    satisfied = satisfied || IsSpeciesSatisfied(raid, StoryProgress, EventProgress);
                if (!NatureFixed)
                    satisfied = satisfied || IsNatureSatisfied(raid, StoryProgress, EventProgress);
                if (!StarFixed)
                    satisfied = satisfied || IsStarsSatisfied(raid, StoryProgress);
                if (!TeraFixed)
                    satisfied = satisfied || IsTeraTypeSatisfied(raid);

            }
            else
            {
                satisfied = IsIVsSatisfied(raid, StoryProgress, EventProgress) && IsShinySatisfied(raid);
                if (!SpeciesFixed)
                    satisfied = satisfied && IsSpeciesSatisfied(raid, StoryProgress, EventProgress);
                if (!NatureFixed)
                    satisfied = satisfied && IsNatureSatisfied(raid, StoryProgress, EventProgress);
                if (!StarFixed)
                    satisfied = satisfied && IsStarsSatisfied(raid, StoryProgress);
                if (!TeraFixed)
                    satisfied = satisfied && IsTeraTypeSatisfied(raid);
            }
            return satisfied;
        }

        public static bool FilterSatisfied(List<Raid> Raids, int StoryProgress, int EventProgress)
        {
            foreach (Raid raid in Raids)
            {
                if (FilterSatisfied(raid, StoryProgress, EventProgress))
                    return true;
            }
            return false;
        }
    }
}
