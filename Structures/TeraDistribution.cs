using PKHeX.Core;

namespace RaidCrawler.Structures
{
    public class TeraDistribution : ITeraRaid
    {
        public readonly EncounterDist9 Entity; // Raw data

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

        public TeraDistribution(EncounterDist9 enc) => Entity = enc;

        public static ITeraRaid[] GetAllEncounters(string resource)
        {
            var encs = EncounterDist9.GetArray(Utils.GetBinaryResource("encounter_dist_paldea.pkl"));
            var result = new TeraDistribution[encs.Length];
            for (int i = 0; i < encs.Length; i++)
                result[i] = new TeraDistribution(encs[i]);
            return result;
        }

        public static ITeraRaid? GetEncounter(uint Seed, int stage)
        {
            if (stage < 0)
                return null;
            foreach (TeraDistribution enc in Raid.DistTeraRaids)
            {
                var total = Raid.Game == "Scarlet" ? enc.Entity.GetRandRateTotalScarlet(stage) : enc.Entity.GetRandRateTotalViolet(stage);
                if (total != 0)
                {
                    var rand = new Xoroshiro128Plus(Seed);
                    _ = rand.NextInt(100);
                    var val = rand.NextInt(total);
                    var min = Raid.Game == "Scarlet" ? enc.Entity.GetRandRateMinScarlet(stage) : enc.Entity.GetRandRateMinViolet(stage);
                    if ((uint)((int)val - min) < enc.RandRate)
                        return enc;
                }
            }
            return null;
        }
    }
}
