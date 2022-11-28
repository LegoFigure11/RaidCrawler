using PKHeX.Core;

namespace RaidCrawler.Structures
{
    public class TeraEncounter : ITeraRaid
    {
        public readonly EncounterTera9 Entity; // Raw data
        public ushort Species => Entity.Species;
        public byte Form => Entity.Form;
        public sbyte Gender => Entity.Gender;
        public AbilityPermission Ability => Entity.Ability;
        public byte FlawlessIVCount => Entity.FlawlessIVCount;
        public Shiny Shiny => Entity.Shiny;
        public byte Level => Entity.Level;
        public ushort Move1 => Entity.Moves.Move1;
        public ushort Move2 => Entity.Moves.Move2;
        public ushort Move3 => Entity.Moves.Move3;
        public ushort Move4 => Entity.Moves.Move4;
        public byte Stars => Entity.Stars;
        public byte RandRate => Entity.RandRate;

        public TeraEncounter(EncounterTera9 enc) => Entity = enc;

        public static ITeraRaid[] GetAllEncounters(string resource)
        {
            var encs = EncounterTera9.GetArray(Utils.GetBinaryResource("encounter_gem_paldea.pkl"));
            var result = new TeraEncounter[encs.Length];
            for (int i = 0; i < encs.Length; i++)
                result[i] = new TeraEncounter(encs[i]);
            return result;
        }

        public static ITeraRaid? GetEncounter(uint Seed, int stage, bool black)
        {
            var clone = new Xoroshiro128Plus(Seed);
            var starcount = black ? 6 : Raid.GetStarCount((uint)clone.NextInt(100), stage, false);
            var total = Raid.Game == "Scarlet" ? GetRateTotalBaseSL(starcount) : GetRateTotalBaseVL(starcount);
            var speciesroll = clone.NextInt((ulong)total);
            foreach (TeraEncounter enc in Raid.GemTeraRaids)
            {
                if (enc.Stars != starcount)
                    continue;
                var minimum = Raid.Game == "Scarlet" ? enc.Entity.RandRateMinScarlet : enc.Entity.RandRateMinViolet;
                if (minimum >= 0 && (uint)((int)speciesroll - minimum) < enc.Entity.RandRate)
                    return enc;
            }
            return null;
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
