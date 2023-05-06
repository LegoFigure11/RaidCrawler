using FluentAssertions;
using System.ComponentModel;
using Xunit;

namespace RaidCrawler.Tests
{
    public class FilterTests : TestUtil
    {
        private const string SennaDitto = "RaidCrawler.Tests.Blocks.senna_9_132_Modest_31_0_31_31_31_31_SL"; // Progress: 4 story
        private const string SennaDittoFilterAtk = "RaidCrawler.Tests.Filters.Ditto0Atk.json";
        private const string SennaDittoFilterSpe = "RaidCrawler.Tests.Filters.Ditto0Spe.json";

        private const string NewtShinyBounsweet = "RaidCrawler.Tests.Blocks.newt_56_761_Calm_4_7_4_3_31_25_VL"; // Progress: 0 story
        private const string NewtBounsweetFilter = "RaidCrawler.Tests.Filters.BounsweetShiny.json";

        private const string IVControlFilter = "RaidCrawler.Tests.Filters.IVControl.json";

        [Theory]
        [InlineData(SennaDitto, SennaDittoFilterAtk, IVControlFilter, 4)]
        [InlineData(SennaDitto, SennaDittoFilterSpe, IVControlFilter, 4)]
        [InlineData(NewtShinyBounsweet, NewtBounsweetFilter, IVControlFilter, 0)]
        [Description("Test various stat filters.")]
        public void FilterTest(string path, string filterPath, string controlPath, int storyPrg)
        {
            var raid = GetRaidContainer(path, storyPrg);
            raid.Item1.delivery.Should().Be(0);
            raid.Item1.enc.Should().Be(0);

            var container = raid.Item2;
            container.Should().NotBeNull();

            var filter = GetRaidFilter(filterPath)[0];
            filter.Should().NotBeNull();

            var raids = container!.Raids;
            var encounters = container.Encounters;
            var satisfied = 0;
            for (int i = 0; i < raids.Count; i++)
            {
                if (filter.FilterSatisfied(container, encounters[i], raids[i], 0))
                    satisfied++;
            }
            satisfied.Should().NotBe(0);

            var controlFilter = GetRaidFilter(controlPath)[0];
            filter.IVBin = controlFilter.IVBin;
            filter.IVComps = controlFilter.IVComps;
            filter.IVVals = controlFilter.IVVals;
            satisfied = 0;
            for (int i = 0; i < raids.Count; i++)
            {
                if (filter.FilterSatisfied(container, encounters[i], raids[i], 0))
                    satisfied++;
            }
            satisfied.Should().Be(0);
        }
    }
}
