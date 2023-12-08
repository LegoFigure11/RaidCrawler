using PKHeX.Core;

namespace RaidCrawler.Core.Structures;

public interface ITeraRaid : ISpeciesForm
{
    ushort[] ExtraMoves { get; }
    byte Gender { get; }
    AbilityPermission Ability { get; }
    byte FlawlessIVCount { get; }
    Shiny Shiny { get; }
    byte Level { get; }
    ushort Move1 { get; }
    ushort Move2 { get; }
    ushort Move3 { get; }
    ushort Move4 { get; }
    byte Stars { get; }
    byte RandRate { get; }
}
