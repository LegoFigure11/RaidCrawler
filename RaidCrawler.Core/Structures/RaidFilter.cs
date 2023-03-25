﻿using PKHeX.Core;

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

        public bool IsSpeciesSatisfied(ITeraRaid? enc)
        {
            if (Species is null)
                return true;

            if (enc is null)
                return false;

            return enc.Species == (ushort)Species;
        }

        public bool IsFormSatisfied(ITeraRaid? enc)
        {
            if (Form is null)
                return true;

            if (enc is null)
                return false;

            return enc.Form == Form;
        }

        public bool IsStarsSatisfied(ITeraRaid? enc)
        {
            if (Stars is null)
                return true;

            if (enc is null)
                return false;

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

        public bool IsRewardsSatisfied(ITeraRaid? enc, Raid raid, int SandwichBoost)
        {
            if (RewardItems is null || RewardsCount == 0)
                return true;

            var rewards = Rewards.GetRewards(enc, raid.Seed, Raid.GetTeraType(enc, raid), SandwichBoost);
            if (rewards is null)
                return false;

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

        public bool IsShinySatisfied(ITeraRaid? encounter, Raid raid)
        {
            if (!Shiny)
                return true;

            return Raid.CheckIsShiny(raid, encounter) == true;
        }

        public bool IsSquareSatisfied(ITeraRaid? encounter, Raid raid)
        {
            if (!Square)
                return true;

            _ = PersonalTable.SV.GetFormEntry(encounter!.Species, encounter.Form);
            var param = Raid.GetParam(encounter);
            PK9 blank = new()
            {
                Species = encounter.Species,
                Form = encounter.Form
            };

            Encounter9RNG.GenerateData(blank, param, EncounterCriteria.Unrestricted, raid.Seed);
            return (Raid.CheckIsShiny(raid, encounter) && ShinyExtensions.IsSquareShinyExist(blank));
        }

        public bool IsTeraTypeSatisfied(Raid raid)
        {
            if (TeraType is null)
                return true;

            return raid.TeraType == TeraType;
        }

        public bool IsNatureSatisfied(ITeraRaid? encounter, Raid raid)
        {
            if (Nature is null)
                return true;

            if (encounter is null)
                return false;

            _ = PersonalTable.SV.GetFormEntry(encounter.Species, encounter.Form);
            var param = Raid.GetParam(encounter);
            PK9 blank = new()
            {
                Species = encounter.Species,
                Form = encounter.Form
            };

            Encounter9RNG.GenerateData(blank, param, EncounterCriteria.Unrestricted, raid.Seed);
            return blank.Nature == Nature;
        }

        public bool IsIVsSatisfied(ITeraRaid? encounter, Raid raid)
        {
            if (IVBin == 0)
                return true;

            if (encounter is null)
                return false;

            var ivs = Raid.GetIVs(raid.Seed, encounter.FlawlessIVCount);
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

        public bool IsGenderSatisfied(ITeraRaid? encounter, Raid raid)
        {
            if (Gender is null)
                return true;

            if (encounter is null)
                return false;

            if (encounter.Gender <= 2 && encounter.Gender == Gender)
                return true;

            var param = Raid.GetParam(encounter);
            PK9 blank = new()
            {
                Species = encounter.Species,
                Form = encounter.Form
            };

            Encounter9RNG.GenerateData(blank, param, EncounterCriteria.Unrestricted, raid.Seed);
            return blank.Gender == Gender;
        }

        public bool IsBatchFilterSatisfied(ITeraRaid? encounter, Raid raid)
        {
            if (BatchFilters is null)
                return true;

            if (encounter is null)
                return false;

            var filters = StringInstruction.GetFilters(BatchFilters.AsSpan());
            if (filters.Count == 0)
                return true;

            BatchEditing.ScreenStrings(filters);
            var param = Raid.GetParam(encounter);
            PK9 blank = new()
            {
                Species = encounter.Species,
                Form = encounter.Form
            };

            Encounter9RNG.GenerateData(blank, param, EncounterCriteria.Unrestricted, raid.Seed);
            return BatchEditing.IsFilterMatch(filters, blank);
        }

        public bool FilterSatisfied(ITeraRaid? encounter, Raid raid, int SandwichBoost)
        {
            return Enabled && IsIVsSatisfied(encounter, raid) && IsShinySatisfied(encounter, raid) && IsSquareSatisfied(encounter, raid) && IsSpeciesSatisfied(encounter) && IsFormSatisfied(encounter)
                && IsNatureSatisfied(encounter, raid) && IsStarsSatisfied(encounter) && IsTeraTypeSatisfied(raid)
                && IsRewardsSatisfied(encounter, raid, SandwichBoost) && IsGenderSatisfied(encounter, raid) && IsBatchFilterSatisfied(encounter, raid);
        }

        public bool FilterSatisfied(List<ITeraRaid?> Encounters, List<Raid> Raids, int SandwichBoost)
        {
            if (Raids.Count != Encounters.Count)
                throw new Exception("Raid Count does not match Encounter count");

            for (int i = 0; i < Raids.Count; i++)
            {
                if (FilterSatisfied(Encounters[i], Raids[i], SandwichBoost))
                    return true;
            }
            return false;
        }
    }
}
