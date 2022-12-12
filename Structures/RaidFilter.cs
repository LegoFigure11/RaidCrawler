using PKHeX.Core;
namespace RaidCrawler.Structures
{
    public class RaidFilter
    {
        public string? Name { get; set; }
        public int? Species { get; set; }
        public int? Stars { get; set; }
        public int StarsComp { get; set; }
        public bool Shiny { get; set; }
        public int? Nature { get; set; }
        public int? TeraType { get; set; }
        public int IVBin { get; set; }
        public int IVComps { get; set; }
        public int IVVals { get; set; }
        public bool Enabled { get; set; }

        public bool IsFilterSet()
        {
            if (Species == null && Stars == null && Shiny == false && Nature == null && TeraType == null && IVBin == 0)
                return false;
            return true;
        }

        public bool IsSpeciesSatisfied(Raid raid, int StoryProgress, int EventProgress)
        {
            if (Species == null)
                return true;
            var progress = raid.IsEvent ? EventProgress : StoryProgress;
            ITeraRaid? encounter = raid.Encounter(progress);
            if (encounter == null)
                return false;
            return encounter.Species == (int)Species;
        }

        public bool IsStarsSatisfied(Raid raid, int StoryProgress)
        {
            if (Stars == null)
                return true;
            switch (StarsComp)
            {
                case 0:
                    return (int)Stars == Raid.GetStarCount(raid.Difficulty, StoryProgress, raid.IsBlack);
                case 1:
                    return (int)Stars < Raid.GetStarCount(raid.Difficulty, StoryProgress, raid.IsBlack);
                case 2:
                    return (int)Stars <= Raid.GetStarCount(raid.Difficulty, StoryProgress, raid.IsBlack);
                case 3:
                    return (int)Stars >= Raid.GetStarCount(raid.Difficulty, StoryProgress, raid.IsBlack);
                case 4:
                    return (int)Stars > Raid.GetStarCount(raid.Difficulty, StoryProgress, raid.IsBlack);
                default: return false;
            }
        }

        public bool IsShinySatisfied(Raid raid)
        {
            if (Shiny == false)
                return true;
            return raid.IsShiny == true;
        }

        public bool IsTeraTypeSatisfied(Raid raid)
        {
            if (TeraType == null)
                return true;
            return raid.TeraType == (int)TeraType;
        }

        public bool IsNatureSatisfied(Raid raid, int StoryProgress, int EventProgress)
        {
            if (Nature == null)
                return true;
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

        public bool IsIVsSatisfied(Raid raid, int StoryProgress, int EventProgress)
        {
            if (IVBin == 0)
                return true;
            var progress = raid.IsEvent ? EventProgress : StoryProgress;
            ITeraRaid? encounter = raid.Encounter(progress);
            if (encounter == null)
                return false;
            var ivs = raid.GetIVs(raid.Seed, encounter.FlawlessIVCount);
            for (int i = 0; i < 6; i++)
            {
                var iv = IVVals >> i * 5 & 31;
                var ivbin = IVBin >> i & 1;
                var ivcomp = IVComps >> i * 3 & 7;
                if (ivbin == 1)
                {
                    switch (ivcomp)
                    {
                        case 0:
                            if (ivs[i] != iv)
                                return false;
                            break;
                        case 1:
                            if (ivs[i] <= iv)
                                return false;
                            break;
                        case 2:
                            if (ivs[i] < iv)
                                return false;
                            break;
                        case 3:
                            if (ivs[i] > iv)
                                return false;
                            break;
                        case 4:
                            if (ivs[i] >= iv)
                                return false;
                            break;
                    }
                }
            }
            return true;
        }

        public bool FilterSatisfied(Raid raid, int StoryProgress, int EventProgress)
        {
            return Enabled && IsIVsSatisfied(raid, StoryProgress, EventProgress) && IsShinySatisfied(raid) && IsSpeciesSatisfied(raid, StoryProgress, EventProgress)
                && IsNatureSatisfied(raid, StoryProgress, EventProgress) && IsStarsSatisfied(raid, StoryProgress) && IsTeraTypeSatisfied(raid);
        }

        public bool FilterSatisfied(List<Raid> Raids, int StoryProgress, int EventProgress)
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
