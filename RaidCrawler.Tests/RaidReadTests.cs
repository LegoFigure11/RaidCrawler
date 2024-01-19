using FluentAssertions;
using System.ComponentModel;
using Xunit;

namespace RaidCrawler.Tests;

public class RaidReadTests : TestUtil
{
    private const string AnubisMightCleared = "RaidCrawler.Tests.Blocks.anubis_Might_cleared_VL"; // Progress: 4 story
    private const string Buddy12Distro = "RaidCrawler.Tests.Blocks.buddy_12_Distro_noMight_VL"; // Progress: 3 story
    private const string Chaos12Distro = "RaidCrawler.Tests.Blocks.chaos_12_Distro_noMight_VL"; // Progress: 3 story
    private const string ZyroInteleonIL = "RaidCrawler.Tests.Blocks.zyro_Inteleon_IL_VL"; // Progress: 4 story

    [Theory]
    [InlineData(AnubisMightCleared, 4, 67)]
    [InlineData(Buddy12Distro, 3, 69)]
    [InlineData(Chaos12Distro, 3, 69)]
    [InlineData(ZyroInteleonIL, 4, 69)]
    [Description(
        "Test read conditions where Might7 is possible but cleared, and where Might7 is possible but not present."
    )]
    public void RaidReadTest(string path, int storyPrg, int expectedRaids)
    {
        var raid = GetRaidContainer(path, storyPrg);
        raid.Item1.delivery.Should().Be(0);
        raid.Item1.encounter.Should().Be(0); // Corrected from 'enc' to 'encounter'

        var container = raid.Item2;
        container.Should().NotBeNull();

        var raidCount = container!.GetRaidCount();
        var encCount = container.Encounters.Count;
        container.GetRaidCount().Should().Be(expectedRaids);
        raidCount.Should().Be(encCount);
    }

}
