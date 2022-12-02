using PKHeX.Core;

namespace RaidCrawler.Structures
{
    public class TeraDistribution : ITeraRaid
    {
        public readonly EncounterStatic Entity; // Raw data

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
        public byte Stars => Entity is ITeraRaid9 t9 ? t9.Stars : (byte)0;
        public byte RandRate => Entity is ITeraRaid9 t9 ? t9.RandRate : (byte)0;

        public TeraDistribution(EncounterStatic enc)
        {
            Entity = enc;
        }

        public static ITeraRaid[] GetAllEncounters(string resource)
        {
            var all = FlatbufferDumper.DumpDistributionRaids(resource);
            var type2 = EncounterDist9.GetArray(all[0]);
            var type3 = EncounterMight9.GetArray(all[1]);
            var result = new TeraDistribution[type2.Length + type3.Length];
            for (int i = 0; i < result.Length; i++)
                result[i] = i < type2.Length ? new TeraDistribution(type2[i]) : new TeraDistribution(type3[i-type2.Length]);
            return result;
        }

        public static ITeraRaid? GetEncounter(uint Seed, int stage, bool isFixed)
        {
            if (stage < 0)
                return null;
            if (!isFixed)
            {
                foreach (TeraDistribution enc in Raid.DistTeraRaids)
                {
                    if (enc.Entity is not EncounterDist9 encd)
                        continue;
                    var total = Raid.Game == "Scarlet" ? encd.GetRandRateTotalScarlet(stage) : encd.GetRandRateTotalViolet(stage);
                    if (total != 0 || isFixed)
                    {
                        if (isFixed)
                            return enc;
                        var rand = new Xoroshiro128Plus(Seed);
                        _ = rand.NextInt(100);
                        var val = rand.NextInt(total);
                        var min = Raid.Game == "Scarlet" ? encd.GetRandRateMinScarlet(stage) : encd.GetRandRateMinViolet(stage);
                        if ((uint)((int)val - min) < enc.RandRate)
                            return enc;
                    }
                }
            }
            else
            {
                foreach (TeraDistribution enc in Raid.DistTeraRaids)
                {
                    if (enc.Entity is not EncounterMight9 encm)
                        continue;
                    var total = Raid.Game == "Scarlet" ? encm.GetRandRateTotalScarlet(stage) : encm.GetRandRateTotalViolet(stage);
                    if (total != 0 || isFixed)
                    {
                        if (isFixed)
                            return enc;
                        var rand = new Xoroshiro128Plus(Seed);
                        _ = rand.NextInt(100);
                        var val = rand.NextInt(total);
                        var min = Raid.Game == "Scarlet" ? encm.GetRandRateMinScarlet(stage) : encm.GetRandRateMinViolet(stage);
                        if ((uint)((int)val - min) < enc.RandRate)
                            return enc;
                    }
                }
            }
            return null;
        }
    }
}
