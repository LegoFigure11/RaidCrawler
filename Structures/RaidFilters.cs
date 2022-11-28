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
        public static int IVBin = Settings.Default.IVBin;

        public static bool IsFilterSet()
        {
            if (Species == null && Stars == null && Shiny == false && Nature == null)
                return false;
            return true;
        }

        public static bool IsSpeciesSatisfied(Raid raid, int StoryProgress, int EventProgress)
        {
            if (Species == null)
                return true;
            ITeraRaid? encounter = raid.IsEvent ? TeraDistribution.GetEncounter(raid.Seed, EventProgress) : TeraEncounter.GetEncounter(raid.Seed, StoryProgress, raid.IsBlack);
            if (encounter == null)
                return false;
            return encounter.Species == (int)Species;
        }

        public static bool IsStarsSatisfied(Raid raid, int StoryProgress)
        {
            if (Stars == null)
                return true;
            return (int)Stars == Raid.GetStarCount(raid.Difficulty, StoryProgress, raid.IsBlack);
        }

        public static bool IsShinySatisfied(Raid raid)
        {
            if (Shiny == false)
                return true;
            return raid.IsShiny == true;
        }

        public static bool IsNatureSatisfied(Raid raid, int StoryProgress, int EventProgress)
        {
            if (Nature == null)
                return true;
            ITeraRaid? encounter = raid.IsEvent ? TeraDistribution.GetEncounter(raid.Seed, EventProgress) : TeraEncounter.GetEncounter(raid.Seed, StoryProgress, raid.IsBlack);
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

        public static bool IsIVsSatisfied(Raid raid, int StoryProgress)
        {
            int StarCount = Raid.GetStarCount(raid.Difficulty, StoryProgress, raid.IsBlack);
            var ivs = raid.GetIVs(raid.Seed, StarCount - 1);
            for (int i = 0; i < 6; i++) 
            {
                if (ivs[i] != 31 && ((IVBin >> i) & 1) == 1) 
                    return false; 
            }
            return true;
        }

        public static bool FilterSatisfied(Raid raid, int StoryProgress, int EventProgress) => IsSpeciesSatisfied(raid, StoryProgress, EventProgress) && IsStarsSatisfied(raid, StoryProgress) && IsIVsSatisfied(raid, StoryProgress) && IsShinySatisfied(raid) && IsNatureSatisfied(raid, StoryProgress, EventProgress);
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
