using PKHeX.Core;

namespace RaidCrawler.Core.Structures
{
    public class RaidFilter
    {
        public string? Name { get; set; }
        public int? Species { get; set; }
        public int? Form { get; set; }
        public int? Stars { get; set; }
        public int StarsComp { get; set; }
        public bool Shiny { get; set; }
        public bool Square { get; set; }
        public int? Nature { get; set; }
        public int? TeraType { get; set; }
        public int? Gender { get; set; }
        public int IVBin { get; set; }
        public int IVComps { get; set; }
        public int IVVals { get; set; }
        public bool Enabled { get; set; }
        public int[]? RewardItems { get; set; }
        public int RewardsComp { get; set; }
        public int RewardsCount { get; set; }
        public string[]? BatchFilters { get; set; }

        public bool IsFilterSet()
        {
            if (Species == null && Form == null && Stars == null && Shiny == false && Square == false && Nature == null && TeraType == null && Gender == null && IVBin == 0 && (RewardItems == null || RewardsCount == 0) && BatchFilters == null)
                return false;
            return true;
        }

        public bool IsSpeciesSatisfied(ushort species)
        {
            if (Species is null)
                return true;

            return species == (ushort)Species;
        }

        public bool IsFormSatisfied(byte form)
        {
            if (Form is null)
                return true;

            return form == Form;
        }

        public bool IsStarsSatisfied(ITeraRaid enc)
        {
            if (Stars is null)
                return true;

            return StarsComp switch
            {
                0 => enc.Stars == Stars,
                1 => enc.Stars > Stars,
                2 => enc.Stars >= Stars,
                3 => enc.Stars <= Stars,
                4 => enc.Stars < Stars,
                _ => false
            };
        }

        public bool IsRewardsSatisfied(ITeraRaid enc, Raid raid, int sandwichBoost)
        {
            if (RewardItems is null || RewardsCount == 0)
                return true;

            var rewards = enc.GetRewards(raid, sandwichBoost);
            var count = rewards.Where(z => RewardItems.Contains(z.Item1)).Count();
            return RewardsComp switch
            {
                0 => count == RewardsCount,
                1 => count > RewardsCount,
                2 => count >= RewardsCount,
                3 => count <= RewardsCount,
                4 => count < RewardsCount,
                _ => false
            };
        }

        public bool IsShinySatisfied(PK9 blank)
        {
            if (!Shiny)
                return true;

            return blank.IsShiny;
        }

        public bool IsSquareSatisfied(PK9 blank)
        {
            if (!Square)
                return true;

            return blank.IsShiny && ShinyExtensions.IsSquareShinyExist(blank);
        }

        public bool IsTeraTypeSatisfied(Raid raid)
        {
            if (TeraType is null)
                return true;

            return raid.TeraType == TeraType;
        }

        public bool IsNatureSatisfied(int nature)
        {
            if (Nature is null)
                return true;

            return nature == Nature;
        }

        public bool IsIVsSatisfied(PK9 blank)
        {
            if (IVBin == 0)
                return true;

            var ivs = blank.IVs;
            for (int i = 0; i < 6; i++)
            {
                var iv = IVVals >> i * 5 & 31;
                var ivbin = IVBin >> i & 1;
                var ivcomp = IVComps >> i * 3 & 7;
                if (ivbin != 1)
                    continue;

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
            return true;
        }

        public bool IsGenderSatisfied(ITeraRaid encounter, int gender)
        {
            if (Gender is null || encounter.Gender <= 2 && encounter.Gender == Gender)
                return true;

            return gender == Gender;
        }

        public bool IsBatchFilterSatisfied(PK9 blank)
        {
            if (BatchFilters is null)
                return true;

            var filters = StringInstruction.GetFilters(BatchFilters.AsSpan());
            if (filters.Count == 0)
                return true;

            BatchEditing.ScreenStrings(filters);
            return BatchEditing.IsFilterMatch(filters, blank);
        }

        public bool FilterSatisfied(ITeraRaid enc, Raid raid, int SandwichBoost)
        {
            var param = enc.GetParam();
            var blank = new PK9
            {
                Species = enc.Species,
                Form = enc.Form
            };
            Encounter9RNG.GenerateData(blank, param, EncounterCriteria.Unrestricted, raid.Seed);

            return Enabled && IsIVsSatisfied(blank) && IsShinySatisfied(blank) && IsSquareSatisfied(blank) && IsSpeciesSatisfied(blank.Species) && IsFormSatisfied(blank.Form)
                && IsNatureSatisfied(blank.Nature) && IsStarsSatisfied(enc) && IsTeraTypeSatisfied(raid)
                && IsRewardsSatisfied(enc, raid, SandwichBoost) && IsGenderSatisfied(enc, blank.Gender) && IsBatchFilterSatisfied(blank);
        }

        public bool FilterSatisfied(IReadOnlyList<ITeraRaid> encounters, IReadOnlyList<Raid> raids, int sandwichBoost)
        {
            if (raids.Count != encounters.Count)
                throw new Exception("Raid count does not match Encounter count");

            for (int i = 0; i < raids.Count; i++)
            {
                if (FilterSatisfied(encounters[i], raids[i], sandwichBoost))
                    return true;
            }
            return false;
        }
    }
}
