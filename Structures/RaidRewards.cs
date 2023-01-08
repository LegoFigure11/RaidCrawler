#nullable disable
using FlatSharp.Attributes;
using Newtonsoft.Json;
using PKHeX.Core;
using System.ComponentModel;

namespace RaidCrawler.Structures
{
    public class Rewards
    {
        public static readonly int[][] RewardSlots =
        {
            new [] { 4, 5, 6, 7, 8 },
            new [] { 4, 5, 6, 7, 8 },
            new [] { 5, 6, 7, 8, 9 },
            new [] { 5, 6, 7, 8, 9 },
            new [] { 6, 7, 8, 9, 10 },
            new [] { 7, 8, 9, 10, 11 },
            new [] { 7, 8, 9, 10, 11 },
        };

        public static readonly int[] RareRewards = { 4, 645, 1606, 1904, 1905, 1906, 1907, 1908, 795 };

        public static int GetRewardCount(int random, int stars)
        {
            return random switch
            {
                < 10 => RewardSlots[stars - 1][0],
                < 40 => RewardSlots[stars - 1][1],
                < 70 => RewardSlots[stars - 1][2],
                < 90 => RewardSlots[stars - 1][3],
                _ => RewardSlots[stars - 1][4],
            };
        }

        public static List<(int, int, int)> ReorderRewards(List<(int, int, int)> rewards)
        {
            var rares = new List<(int, int, int)>();
            var commons = new List<(int, int, int)>();
            for (int i = 0; i < rewards.Count; i++)
            {
                if (RareRewards.Contains(rewards[i].Item1))
                    rares.Add(rewards[i]);
                else commons.Add(rewards[i]);
            }
            rares.AddRange(commons);
            return rares;
        }

        public static int GetTeraShard(int type)
        {
            return (MoveType)type switch
            {
                MoveType.Normal => 1862,
                MoveType.Fighting => 1868,
                MoveType.Flying => 1871,
                MoveType.Poison => 1869,
                MoveType.Ground => 1870,
                MoveType.Rock => 1874,
                MoveType.Bug => 1873,
                MoveType.Ghost => 1875,
                MoveType.Steel => 1878,
                MoveType.Fire => 1863,
                MoveType.Water => 1864,
                MoveType.Grass => 1866,
                MoveType.Electric => 1865,
                MoveType.Psychic => 1872,
                MoveType.Ice => 1867,
                MoveType.Dragon => 1876,
                MoveType.Dark => 1877,
                MoveType.Fairy => 1879,
                _ => 20000,
            };
        }

        public static int GetMaterial(int species)
        {
            return (Species)species switch
            {
                Species.Venonat or Species.Venomoth => 1956,
                Species.Diglett or Species.Dugtrio => 1957,
                Species.Meowth or Species.Persian => 1958,
                Species.Psyduck or Species.Golduck => 1959,
                Species.Mankey or Species.Primeape or Species.Annihilape => 1960,
                Species.Growlithe or Species.Arcanine => 1961,
                Species.Slowpoke or Species.Slowbro or Species.Slowking => 1962,
                Species.Magnemite or Species.Magneton or Species.Magnezone => 1963,
                Species.Grimer or Species.Muk => 1964,
                Species.Shellder or Species.Cloyster => 1965,
                Species.Gastly or Species.Haunter or Species.Gengar => 1966,
                Species.Drowzee or Species.Hypno => 1967,
                Species.Voltorb or Species.Electrode => 1968,
                Species.Scyther or Species.Scizor or Species.Kleavor => 1969,
                Species.Tauros => 1970,
                Species.Magikarp or Species.Gyarados => 1971,
                Species.Ditto => 1972,
                Species.Eevee or Species.Vaporeon or Species.Jolteon
                or Species.Flareon or Species.Espeon or Species.Umbreon
                or Species.Leafeon or Species.Glaceon or Species.Sylveon => 1973,
                Species.Dratini or Species.Dragonair or Species.Dragonite => 1974,
                Species.Pichu or Species.Pikachu or Species.Raichu => 1975,
                Species.Igglybuff or Species.Jigglypuff or Species.Wigglytuff => 1976,
                Species.Mareep or Species.Flaaffy or Species.Ampharos => 1977,
                Species.Hoppip or Species.Skiploom or Species.Jumpluff => 1978,
                Species.Sunkern or Species.Sunflora => 1979,
                Species.Murkrow or Species.Honchkrow => 1980,
                Species.Misdreavus or Species.Mismagius => 1981,
                Species.Girafarig or Species.Farigiraf => 1982,
                Species.Pineco or Species.Forretress => 1983,
                Species.Dunsparce or Species.Dudunsparce => 1984,
                Species.Qwilfish or Species.Overqwil => 1985,
                Species.Heracross => 1986,
                Species.Sneasel or Species.Weavile or Species.Sneasler => 1987,
                Species.Teddiursa or Species.Ursaring or Species.Ursaluna => 1988,
                Species.Delibird => 1989,
                Species.Houndour or Species.Houndoom => 1990,
                Species.Phanpy or Species.Donphan => 1991,
                Species.Stantler or Species.Wyrdeer => 1992,
                Species.Larvitar or Species.Pupitar or Species.Tyranitar => 1993,
                Species.Wingull or Species.Pelipper => 1994,
                Species.Ralts or Species.Kirlia or Species.Gardevoir or Species.Gallade => 1995,
                Species.Surskit or Species.Masquerain => 1996,
                Species.Shroomish or Species.Breloom => 1997,
                Species.Slakoth or Species.Vigoroth or Species.Slaking => 1998,
                Species.Makuhita or Species.Hariyama => 1999,
                Species.Azurill or Species.Marill or Species.Azumarill => 2000,
                Species.Sableye => 2001,
                Species.Meditite or Species.Medicham => 2002,
                Species.Gulpin or Species.Swalot => 2003,
                Species.Numel or Species.Camerupt => 2004,
                Species.Torkoal => 2005,
                Species.Spoink or Species.Grumpig => 2006,
                Species.Cacnea or Species.Cacturne => 2007,
                Species.Swablu or Species.Altaria => 2008,
                Species.Zangoose => 2009,
                Species.Seviper => 2010,
                Species.Barboach or Species.Whiscash => 2011,
                Species.Shuppet or Species.Banette => 2012,
                Species.Tropius => 2013,
                Species.Snorunt or Species.Glalie or Species.Froslass => 2014,
                Species.Luvdisc => 2015,
                Species.Bagon or Species.Shelgon or Species.Salamence => 2016,
                Species.Starly or Species.Staravia or Species.Staraptor => 2017,
                Species.Kricketot or Species.Kricketune => 2018,
                Species.Shinx or Species.Luxio or Species.Luxray => 2019,
                Species.Combee or Species.Vespiquen => 2020,
                Species.Pachirisu => 2021,
                Species.Buizel or Species.Floatzel => 2022,
                Species.Shellos or Species.Gastrodon => 2023,
                Species.Drifloon or Species.Drifblim => 2024,
                Species.Stunky or Species.Skuntank => 2025,
                Species.Bronzor or Species.Bronzong => 2026,
                Species.Bonsly or Species.Sudowoodo => 2027,
                Species.Happiny or Species.Chansey or Species.Blissey => 2028,
                Species.Spiritomb => 2029,
                Species.Gible or Species.Gabite or Species.Garchomp => 2030,
                Species.Riolu or Species.Lucario => 2031,
                Species.Hippopotas or Species.Hippowdon => 2032,
                Species.Croagunk or Species.Toxicroak => 2033,
                Species.Finneon or Species.Lumineon => 2034,
                Species.Snover or Species.Abomasnow => 2035,
                Species.Rotom => 2036,
                Species.Petilil or Species.Lilligant => 2037,
                Species.Basculin or Species.Basculegion => 2038,
                Species.Sandile or Species.Krokorok or Species.Krookodile => 2039,
                Species.Zorua or Species.Zoroark => 2040,
                Species.Gothita or Species.Gothorita or Species.Gothitelle => 2041,
                Species.Deerling or Species.Sawsbuck => 2042,
                Species.Foongus or Species.Amoonguss => 2043,
                Species.Alomomola => 2044,
                Species.Tynamo or Species.Eelektrik or Species.Eelektross => 2045,
                Species.Axew or Species.Fraxure or Species.Haxorus => 2046,
                Species.Cubchoo or Species.Beartic => 2047,
                Species.Cryogonal => 2048,
                Species.Pawniard or Species.Bisharp or Species.Kingambit => 2049,
                Species.Rufflet or Species.Braviary => 2050,
                Species.Deino or Species.Zweilous or Species.Hydreigon => 2051,
                Species.Larvesta or Species.Volcarona => 2052,
                Species.Fletchling or Species.Fletchinder or Species.Talonflame => 2053,
                Species.Scatterbug or Species.Spewpa or Species.Vivillon => 2054,
                Species.Litleo or Species.Pyroar => 2055,
                Species.Flabébé or Species.Floette or Species.Florges => 2056,
                Species.Skiddo or Species.Gogoat => 2057,
                Species.Skrelp or Species.Dragalge => 2058,
                Species.Clauncher or Species.Clawitzer => 2059,
                Species.Hawlucha => 2060,
                Species.Dedenne => 2061,
                Species.Goomy or Species.Sliggoo or Species.Goodra => 2062,
                Species.Klefki => 2063,
                Species.Bergmite or Species.Avalugg => 2064,
                Species.Noibat or Species.Noivern => 2065,
                Species.Yungoos or Species.Gumshoos => 2066,
                Species.Crabrawler or Species.Crabominable => 2067,
                Species.Oricorio => 2068,
                Species.Rockruff or Species.Lycanroc => 2069,
                Species.Mareanie or Species.Toxapex => 2070,
                Species.Mudbray or Species.Mudsdale => 2071,
                Species.Fomantis or Species.Lurantis => 2072,
                Species.Salandit or Species.Salazzle => 2073,
                Species.Bounsweet or Species.Steenee or Species.Tsareena => 2074,
                Species.Oranguru => 2075,
                Species.Passimian => 2076,
                Species.Sandygast or Species.Palossand => 2077,
                Species.Komala => 2078,
                Species.Mimikyu => 2079,
                Species.Bruxish => 2080,
                Species.Chewtle or Species.Drednaw => 2081,
                Species.Skwovet or Species.Greedent => 2082,
                Species.Arrokuda or Species.Barraskewda => 2083,
                Species.Rookidee or Species.Corvisquire or Species.Corviknight => 2084,
                Species.Toxel or Species.Toxtricity => 2085,
                Species.Falinks => 2086,
                Species.Cufant or Species.Copperajah => 2087,
                Species.Rolycoly or Species.Carkol or Species.Coalossal => 2088,
                Species.Silicobra or Species.Sandaconda => 2089,
                Species.Indeedee => 2090,
                Species.Pincurchin => 2091,
                Species.Snom or Species.Frosmoth => 2092,
                Species.Impidimp or Species.Morgrem or Species.Grimmsnarl => 2093,
                Species.Applin or Species.Flapple or Species.Appletun => 2094,
                Species.Sinistea or Species.Polteageist => 2095,
                Species.Hatenna or Species.Hattrem or Species.Hatterene => 2096,
                Species.Stonjourner => 2097,
                Species.Eiscue => 2098,
                Species.Dreepy or Species.Drakloak or Species.Dragapult => 2099,

                Species.Lechonk or Species.Oinkologne => 2103,
                Species.Tarountula or Species.Spidops => 2104,
                Species.Nymble or Species.Lokix => 2105,
                Species.Rellor or Species.Rabsca => 2106,
                Species.Greavard or Species.Houndstone => 2107,
                Species.Flittle or Species.Espathra => 2108,
                Species.Wiglett or Species.Wugtrio => 2109,
                Species.Dondozo => 2110,
                Species.Veluza => 2111,
                Species.Finizen or Species.Palafin => 2112,
                Species.Smoliv or Species.Dolliv or Species.Arboliva => 2113,
                Species.Capsakid or Species.Scovillain => 2114,
                Species.Tadbulb or Species.Bellibolt => 2115,
                Species.Varoom or Species.Revavroom => 2116,
                Species.Orthworm => 2117,
                Species.Tandemaus or Species.Maushold => 2118,
                Species.Cetoddle or Species.Cetitan => 2119,
                Species.Frigibax or Species.Arctibax or Species.Baxcalibur => 2120,
                Species.Tatsugiri => 2121,
                Species.Cyclizar => 2122,
                Species.Pawmi or Species.Pawmo or Species.Pawmot => 2123,

                Species.Wattrel or Species.Kilowattrel => 2126,
                Species.Bombirdier => 2127,
                Species.Squawkabilly => 2128,
                Species.Flamigo => 2129,
                Species.Klawf => 2130,
                Species.Nacli or Species.Naclstack or Species.Garganacl => 2131,
                Species.Glimmet or Species.Glimmora => 2132,
                Species.Shroodle or Species.Grafaiai => 2133,
                Species.Fidough or Species.Dachsbun => 2134,
                Species.Maschiff or Species.Mabosstiff => 2135,
                Species.Bramblin or Species.Brambleghast => 2136,
                Species.Gimmighoul or Species.Gholdengo => 2137,

                Species.Tinkatink or Species.Tinkatuff or Species.Tinkaton => 2156,
                Species.Charcadet or Species.Armarouge or Species.Ceruledge => 2157,
                Species.Toedscool or Species.Toedscruel => 2158,
                Species.Wooper or Species.Quagsire or Species.Clodsire => 2159,

                _ => 10000,
            };
        }

        public static List<(int, int, int)>? GetRewards(ITeraRaid? encounter, uint seed, int teratype, int sandwich_boost)
        {
            var rewards = encounter switch
            {
                TeraDistribution => TeraDistribution.GetRewards((TeraDistribution)encounter, seed, teratype, Raid.DeliveryRaidFixedRewards, Raid.DeliveryRaidLotteryRewards, sandwich_boost),
                TeraEncounter => TeraEncounter.GetRewards((TeraEncounter)encounter, seed, teratype, Raid.BaseFixedRewards, Raid.BaseLotteryRewards, sandwich_boost),
                _ => null,
            };
            return rewards;
        }
    }

    public class RaidFixedRewards
    {
        public ulong TableName { get; set; }
        public RaidFixedRewardItemInfo RewardItem00 { get; set; }
        public RaidFixedRewardItemInfo RewardItem01 { get; set; }
        public RaidFixedRewardItemInfo RewardItem02 { get; set; }
        public RaidFixedRewardItemInfo RewardItem03 { get; set; }
        public RaidFixedRewardItemInfo RewardItem04 { get; set; }
        public RaidFixedRewardItemInfo RewardItem05 { get; set; }
        public RaidFixedRewardItemInfo RewardItem06 { get; set; }
        public RaidFixedRewardItemInfo RewardItem07 { get; set; }
        public RaidFixedRewardItemInfo RewardItem08 { get; set; }
        public RaidFixedRewardItemInfo RewardItem09 { get; set; }
        public RaidFixedRewardItemInfo RewardItem10 { get; set; }
        public RaidFixedRewardItemInfo RewardItem11 { get; set; }
        public RaidFixedRewardItemInfo RewardItem12 { get; set; }
        public RaidFixedRewardItemInfo RewardItem13 { get; set; }
        public RaidFixedRewardItemInfo RewardItem14 { get; set; }

        public const int Count = 15;

        public RaidFixedRewardItemInfo GetReward(int index) => index switch
        {
            00 => RewardItem00,
            01 => RewardItem01,
            02 => RewardItem02,
            03 => RewardItem03,
            04 => RewardItem04,
            05 => RewardItem05,
            06 => RewardItem06,
            07 => RewardItem07,
            08 => RewardItem08,
            09 => RewardItem09,
            10 => RewardItem10,
            11 => RewardItem11,
            12 => RewardItem12,
            13 => RewardItem13,
            14 => RewardItem14,
            _ => throw new ArgumentOutOfRangeException(nameof(index))
        };

    }

    public class RaidLotteryRewards
    {
        public ulong TableName { get; set; }
        public RaidLotteryRewardItemInfo RewardItem00 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem01 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem02 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem03 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem04 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem05 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem06 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem07 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem08 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem09 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem10 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem11 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem12 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem13 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem14 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem15 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem16 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem17 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem18 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem19 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem20 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem21 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem22 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem23 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem24 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem25 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem26 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem27 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem28 { get; set; }
        public RaidLotteryRewardItemInfo RewardItem29 { get; set; }

        public const int RewardItemCount = 30;

        // Get reward item from index
        public RaidLotteryRewardItemInfo GetRewardItem(int index) => index switch
        {
            00 => RewardItem00,
            01 => RewardItem01,
            02 => RewardItem02,
            03 => RewardItem03,
            04 => RewardItem04,
            05 => RewardItem05,
            06 => RewardItem06,
            07 => RewardItem07,
            08 => RewardItem08,
            09 => RewardItem09,
            10 => RewardItem10,
            11 => RewardItem11,
            12 => RewardItem12,
            13 => RewardItem13,
            14 => RewardItem14,
            15 => RewardItem15,
            16 => RewardItem16,
            17 => RewardItem17,
            18 => RewardItem18,
            19 => RewardItem19,
            20 => RewardItem20,
            21 => RewardItem21,
            22 => RewardItem22,
            23 => RewardItem23,
            24 => RewardItem24,
            25 => RewardItem25,
            26 => RewardItem26,
            27 => RewardItem27,
            28 => RewardItem28,
            29 => RewardItem29,
            _ => throw new ArgumentOutOfRangeException(nameof(index), index, null)
        };
    }

    [JsonObject]
    [FlatBufferTable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class RaidLotteryRewardItemInfo
    {
        [FlatBufferItem(0)] public int Category { get; set; }
        [FlatBufferItem(1)] public int ItemID { get; set; }
        [FlatBufferItem(2)] public sbyte Num { get; set; }
        [FlatBufferItem(3)] public int Rate { get; set; }
        [FlatBufferItem(4)] public bool RareItemFlag { get; set; }
    }

    [JsonObject]
    [FlatBufferTable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class RaidFixedRewardItemInfo
    {
        [FlatBufferItem(0)] public int Category { get; set; }
        [FlatBufferItem(1)] public int SubjectType { get; set; }
        [FlatBufferItem(2)] public int ItemID { get; set; }
        [FlatBufferItem(3)] public sbyte Num { get; set; }
    }
}
