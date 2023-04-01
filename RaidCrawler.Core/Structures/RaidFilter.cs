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

        public bool IsSpeciesSatisfied(ITeraRaid enc)
        {
            if (Species is null)
                return true;

            return enc.Species == (ushort)Species;
        }

        public bool IsFormSatisfied(ITeraRaid enc)
        {
            if (Form is null)
                return true;

            return enc.Form == Form;
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

        public bool IsShinySatisfied(ITeraRaid encounter, Raid raid)
        {
            if (!Shiny)
                return true;

            return raid.CheckIsShiny(encounter);
        }

        public bool IsSquareSatisfied(ITeraRaid encounter, Raid raid)
        {
            if (!Square)
                return true;

            _ = PersonalTable.SV.GetFormEntry(encounter.Species, encounter.Form);
            var param = encounter.GetParam();
            PK9 blank = new()
            {
                Species = encounter.Species,
                Form = encounter.Form
            };

            Encounter9RNG.GenerateData(blank, param, EncounterCriteria.Unrestricted, raid.Seed);
            return raid.CheckIsShiny(encounter) && ShinyExtensions.IsSquareShinyExist(blank);
        }

        public bool IsTeraTypeSatisfied(Raid raid)
        {
            if (TeraType is null)
                return true;

            return raid.TeraType == TeraType;
        }

        public bool IsNatureSatisfied(ITeraRaid encounter, Raid raid)
        {
            if (Nature is null)
                return true;

            _ = PersonalTable.SV.GetFormEntry(encounter.Species, encounter.Form);
            var param = encounter.GetParam();
            PK9 blank = new()
            {
                Species = encounter.Species,
                Form = encounter.Form
            };

            Encounter9RNG.GenerateData(blank, param, EncounterCriteria.Unrestricted, raid.Seed);
            return blank.Nature == Nature;
        }

        public bool IsIVsSatisfied(ITeraRaid encounter, Raid raid)
        {
            if (IVBin == 0)
                return true;

            var ivs = GetIVs(raid.Seed, encounter.FlawlessIVCount);
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

        public bool IsGenderSatisfied(ITeraRaid encounter, Raid raid)
        {
            if (Gender is null || encounter.Gender <= 2 && encounter.Gender == Gender)
                return true;

            var param = encounter.GetParam();
            PK9 blank = new()
            {
                Species = encounter.Species,
                Form = encounter.Form
            };

            Encounter9RNG.GenerateData(blank, param, EncounterCriteria.Unrestricted, raid.Seed);
            return blank.Gender == Gender;
        }

        public bool IsBatchFilterSatisfied(ITeraRaid encounter, Raid raid)
        {
            if (BatchFilters is null)
                return true;

            var filters = StringInstruction.GetFilters(BatchFilters.AsSpan());
            if (filters.Count == 0)
                return true;

            BatchEditing.ScreenStrings(filters);
            var param = encounter.GetParam();
            PK9 blank = new()
            {
                Species = encounter.Species,
                Form = encounter.Form
            };

            Encounter9RNG.GenerateData(blank, param, EncounterCriteria.Unrestricted, raid.Seed);
            return BatchEditing.IsFilterMatch(filters, blank);
        }

        public bool FilterSatisfied(ITeraRaid encounter, Raid raid, int SandwichBoost)
        {
            return Enabled && IsIVsSatisfied(encounter, raid) && IsShinySatisfied(encounter, raid) && IsSquareSatisfied(encounter, raid) && IsSpeciesSatisfied(encounter) && IsFormSatisfied(encounter)
                && IsNatureSatisfied(encounter, raid) && IsStarsSatisfied(encounter) && IsTeraTypeSatisfied(raid)
                && IsRewardsSatisfied(encounter, raid, SandwichBoost) && IsGenderSatisfied(encounter, raid) && IsBatchFilterSatisfied(encounter, raid);
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

        private int[] GetIVs(uint Seed, int FlawlessIVs)
        {
            var rng = new Xoroshiro128Plus(Seed);
            // Dummy calls
            rng.NextInt(); // EC
            rng.NextInt(); // TIDSID
            rng.NextInt(); // PID

            Span<int> ivs = stackalloc[] { -1, -1, -1, -1, -1, -1 };
            // Flawless IVs
            for (int i = 0; i < FlawlessIVs; i++)
            {
                int index;
                do { index = (int)rng.NextInt(6); }
                while (ivs[index] != -1);

                ivs[index] = 31;
            }

            // Other IVs
            for (int i = 0; i < ivs.Length; i++)
            {
                if (ivs[i] == -1)
                    ivs[i] = (int)rng.NextInt(32);
            }

            return ivs.ToArray();
        }
    }
}
