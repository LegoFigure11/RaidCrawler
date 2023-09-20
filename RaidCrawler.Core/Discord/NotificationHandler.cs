using PKHeX.Core;
using RaidCrawler.Core.Interfaces;
using RaidCrawler.Core.Structures;
using SysBot.Base;
using System.Globalization;
using System.Text.Json;
using System.Text;

namespace RaidCrawler.Core.Discord
{
    public class NotificationHandler
    {
        private readonly HttpClient _client;
        private readonly string[]? DiscordWebhooks;
        private readonly IWebhookConfig Config;

        public NotificationHandler(IWebhookConfig config)
        {
            _client = new();
            Config = config;
            DiscordWebhooks = config.EnableNotification ? config.DiscordWebhook.Split(',') : null;
        }

        public async Task SendNotification(
            ITeraRaid encounter,
            Raid raid,
            RaidFilter filter,
            string time,
            IReadOnlyList<(int, int, int)> RewardsList,
            string hexColor,
            string spriteName,
            CancellationToken token
        )
        {
            if (DiscordWebhooks is null || !Config.EnableNotification)
                return;

            var webhook = GenerateWebhook(
                encounter,
                raid,
                filter,
                time,
                RewardsList,
                hexColor,
                spriteName
            );
            var content = new StringContent(
                JsonSerializer.Serialize(webhook),
                Encoding.UTF8,
                "application/json"
            );
            foreach (var url in DiscordWebhooks)
                await _client.PostAsync(url.Trim(), content, token).ConfigureAwait(false);
        }

        public async Task SendErrorNotification(
            string error,
            string caption,
            CancellationToken token
        )
        {
            if (DiscordWebhooks is null || !Config.EnableNotification)
                return;

            var instance =
                Config.InstanceName != "" ? $"RaidCrawler {Config.InstanceName}" : "RaidCrawler";
            var webhook = new
            {
                username = instance,
                avatar_url = "https://www.serebii.net/scarletviolet/ribbons/mightiestmark.png",
                content = Config.DiscordMessageContent,
                embeds = new List<object>
                {
                    new
                    {
                        title = caption != "" ? caption : "RaidCrawler Error",
                        description = error,
                        color = 0xf7262a,
                    }
                }
            };

            var content = new StringContent(
                JsonSerializer.Serialize(webhook),
                Encoding.UTF8,
                "application/json"
            );
            foreach (var url in DiscordWebhooks)
                await _client.PostAsync(url.Trim(), content, token).ConfigureAwait(false);
        }

        public async Task SendScreenshot(ISwitchConnectionAsync nx, CancellationToken token)
        {
            if (DiscordWebhooks is null || !Config.EnableNotification)
                return;

            var data = await nx.PixelPeek(token).ConfigureAwait(false);
            var content = new MultipartFormDataContent();
            var info = new
            {
                username = "RaidCrawler",
                avatar_url = "https://www.serebii.net/scarletviolet/ribbons/mightiestmark.png",
                content = "Switch Screenshot",
            };

            var basic_info = new StringContent(
                JsonSerializer.Serialize(info),
                System.Text.Encoding.UTF8,
                "application/json"
            );
            content.Add(basic_info, "payload_json");
            content.Add(new ByteArrayContent(data), "screenshot.jpg", "screenshot.jpg");
            foreach (var url in DiscordWebhooks)
                await _client.PostAsync(url.Trim(), content, token).ConfigureAwait(false);
        }

        private object GenerateWebhook(
            ITeraRaid encounter,
            Raid raid,
            RaidFilter filter,
            string time,
            IReadOnlyList<(int, int, int)> rewardsList,
            string hexColor,
            string spriteName
        )
        {
            var strings = GameInfo.GetStrings(1);
            var param = encounter.GetParam();
            var blank = new PK9 { Species = encounter.Species, Form = encounter.Form };

            Encounter9RNG.GenerateData(blank, param, EncounterCriteria.Unrestricted, raid.Seed);
            var form = Utils.GetFormString(blank.Species, blank.Form, strings);
            var species = $"{strings.Species[encounter.Species]}{form}";
            var difficulty = Difficulty(encounter.Stars, raid.IsEvent);
            var nature = $"{strings.Natures[blank.Nature]}";
            var ability = $"{strings.Ability[blank.Ability]}";
            var shiny = Shiny(
                raid.CheckIsShiny(encounter),
                ShinyExtensions.IsSquareShinyExist(blank)
            );
            var gender = GenderEmoji(blank.Gender);
            var teratype = raid.GetTeraType(encounter);
            var tera = $"{strings.types[teratype]}";
            var teraemoji = TeraEmoji(strings.types[teratype]);
            var ivs = IVsStringEmoji(ToSpeedLast(blank.IVs));
            var moves = new ushort[4]
            {
                encounter.Move1,
                encounter.Move2,
                encounter.Move3,
                encounter.Move4
            };
            var movestr = string.Concat(
                    moves.Where(z => z != 0).Select(z => $"{strings.Move[z]}ㅤ\n")
                )
                .Trim();
            var extramoves = string.Concat(
                    encounter.ExtraMoves.Where(z => z != 0).Select(z => $"{strings.Move[z]}ㅤ\n")
                )
                .Trim();
            var area =
                $"{Areas.GetArea((int)(raid.Area - 1), raid.RaidType)}"
                + (Config.ToggleDen ? $" [Den {raid.Den}]ㅤ" : "ㅤ");
            var rewards = GetRewards(rewardsList);
            var SuccessWebHook = new
            {
                username = "RaidCrawler " + Config.InstanceName,
                avatar_url = "https://www.serebii.net/scarletviolet/ribbons/mightiestmark.png",
                content = Config.DiscordMessageContent,
                embeds = new List<object>
                {
                    new
                    {
                        title = $"{shiny} {species} {gender} {teraemoji}",
                        description = "",
                        color = int.Parse(hexColor, NumberStyles.HexNumber),
                        thumbnail = new
                        {
                            url = $"https://github.com/kwsch/PKHeX/blob/master/PKHeX.Drawing.PokeSprite/Resources/img/Artwork%20Pokemon%20Sprites/a{spriteName}.png?raw=true"
                        },
                        fields = new List<object>
                        {
                            new
                            {
                                name = "Difficultyㅤㅤㅤㅤㅤㅤ",
                                value = difficulty,
                                inline = true,
                            },
                            new
                            {
                                name = "Natureㅤㅤㅤ",
                                value = nature,
                                inline = true
                            },
                            new
                            {
                                name = "Ability",
                                value = ability,
                                inline = true,
                            },
                            new
                            {
                                name = "IVs",
                                value = ivs,
                                inline = true,
                            },
                            new
                            {
                                name = "Moves",
                                value = movestr,
                                inline = true,
                            },
                            new
                            {
                                name = "Extra Moves",
                                value = extramoves == string.Empty ? "None" : extramoves,
                                inline = true,
                            },
                            new
                            {
                                name = "Location󠀠󠀠󠀠",
                                value = area,
                                inline = true,
                            },
                            new
                            {
                                name = "Search Time󠀠󠀠󠀠",
                                value = time,
                                inline = true,
                            },
                            new
                            {
                                name = "Filter Name",
                                value = filter.Name,
                                inline = true,
                            },
                            new
                            {
                                name = rewards != "" ? "Rewards" : "",
                                value = rewards,
                                inline = false,
                            },
                        },
                    }
                }
            };
            return SuccessWebHook;
        }

        private string Difficulty(byte stars, bool isEvent)
        {
            bool enable = Config.EnableEmoji;
            string emoji = !enable
                ? ":star:"
                : stars == 7
                    ? Config.Emoji["7 Star"]
                    : isEvent
                        ? Config.Emoji["Event Star"]
                        : Config.Emoji["Star"];

            return string.Concat(Enumerable.Repeat(emoji, stars));
        }

        private string GenderEmoji(int genderInt) =>
            genderInt switch
            {
                (int)Gender.Male => Config.EnableEmoji ? Config.Emoji["Male"] : ":male_sign:",
                (int)Gender.Female => Config.EnableEmoji ? Config.Emoji["Female"] : ":female_sign:",
                _ => "",
            };

        private string GetRewards(IReadOnlyList<(int, int, int)> rewards)
        {
            string s = string.Empty;
            int abilitycapsule = 0;
            int bottlecap = 0;
            int abilitypatch = 0;
            int sweetherba = 0;
            int saltyherba = 0;
            int sourherba = 0;
            int bitterherba = 0;
            int spicyherba = 0;

            for (int i = 0; i < rewards.Count; i++)
            {
                switch (rewards[i].Item1)
                {
                    case 0645:
                        abilitycapsule++;
                        break;
                    case 0795:
                        bottlecap++;
                        break;
                    case 1606:
                        abilitypatch++;
                        break;
                    case 1904:
                        sweetherba++;
                        break;
                    case 1905:
                        saltyherba++;
                        break;
                    case 1906:
                        sourherba++;
                        break;
                    case 1907:
                        bitterherba++;
                        break;
                    case 1908:
                        spicyherba++;
                        break;
                }
            }

            bool emoji = Config.EnableEmoji;
            s +=
                (abilitycapsule > 0)
                    ? (
                        emoji
                            ? $"`{abilitycapsule}`{Config.Emoji["Ability Capsule"]} "
                            : $"`{abilitycapsule}` Ability Capsule  "
                    )
                    : "";
            s +=
                (bottlecap > 0)
                    ? (
                        emoji
                            ? $"`{bottlecap}`{Config.Emoji["Bottle Cap"]} "
                            : $"`{bottlecap}` Bottle Cap  "
                    )
                    : "";
            s +=
                (abilitypatch > 0)
                    ? (
                        emoji
                            ? $"`{abilitypatch}`{Config.Emoji["Ability Patch"]} "
                            : $"`{abilitypatch}` Ability Patch  "
                    )
                    : "";
            s +=
                (sweetherba > 0)
                    ? (
                        emoji
                            ? $"`{sweetherba}`{Config.Emoji["Sweet Herba"]} "
                            : $"`{sweetherba}` Sweet Herba  "
                    )
                    : "";
            s +=
                (saltyherba > 0)
                    ? (
                        emoji
                            ? $"`{saltyherba}`{Config.Emoji["Salty Herba"]} "
                            : $"`{saltyherba}` Salty Herba  "
                    )
                    : "";
            s +=
                (sourherba > 0)
                    ? (
                        emoji
                            ? $"`{sourherba}`{Config.Emoji["Sour Herba"]} "
                            : $"`{sourherba}` Sour Herba  "
                    )
                    : "";
            s +=
                (bitterherba > 0)
                    ? (
                        emoji
                            ? $"`{bitterherba}`{Config.Emoji["Bitter Herba"]} "
                            : $"`{bitterherba}` Bitter Herba  "
                    )
                    : "";
            s +=
                (spicyherba > 0)
                    ? (
                        emoji
                            ? $"`{spicyherba}`{Config.Emoji["Spicy Herba"]} "
                            : $"`{spicyherba}` Spicy Herba  "
                    )
                    : "";

            return s;
        }

        private string IVsStringEmoji(int[] ivs)
        {
            string s = string.Empty;
            bool emoji = Config.EnableEmoji;
            bool verbose = Config.VerboseIVs;
            var stats = new[] { "HP", "Atk", "Def", "SpA", "SpD", "Spe" };
            var iv0 = new[]
            {
                Config.Emoji["Health 0"],
                Config.Emoji["Attack 0"],
                Config.Emoji["Defense 0"],
                Config.Emoji["SpAttack 0"],
                Config.Emoji["SpDefense 0"],
                Config.Emoji["Speed 0"]
            };
            var iv31 = new[]
            {
                Config.Emoji["Health 31"],
                Config.Emoji["Attack 31"],
                Config.Emoji["Defense 31"],
                Config.Emoji["SpAttack 31"],
                Config.Emoji["SpDefense 31"],
                Config.Emoji["Speed 31"]
            };
            for (int i = 0; i < ivs.Length; i++)
            {
                switch (Config.IVsStyle)
                {
                    case 0:
                    {
                        s += ivs[i] switch
                        {
                            0
                                => emoji
                                    ? $"{iv0[i]:D}{(verbose ? " " + stats[i] : string.Empty)}"
                                    : $"`{"✓":D}`{(verbose ? " " + stats[i] : string.Empty)}",
                            31
                                => emoji
                                    ? $"{iv31[i]:D}{(verbose ? " " + stats[i] : string.Empty)}"
                                    : $"`{"✓":D}`{(verbose ? " " + stats[i] : string.Empty)}",
                            _ => $"`{ivs[i]:D}`{(verbose ? " " + stats[i] : string.Empty)}",
                        };

                        if (i < 5)
                            s += " / ";
                        break;
                    }
                    case 1:
                    {
                        s += $"`{ivs[i]:D}`{(verbose ? " " + stats[i] : string.Empty)}";
                        if (i < 5)
                            s += " / ";
                        break;
                    }
                    case 2:
                    {
                        s += $"{ivs[i]:D}{(verbose ? " " + stats[i] : string.Empty)}";
                        if (i < 5)
                            s += " / ";
                        break;
                    }
                }
            }
            return s;
        }

        private string Shiny(bool shiny, bool square)
        {
            bool emoji = Config.EnableEmoji;
            string s = "";
            if (square && shiny)
                s = $"{(emoji ? Config.Emoji["Square Shiny"] : "Square shiny")}";
            else if (shiny)
                s = $"{(emoji ? Config.Emoji["Shiny"] : "Shiny")}";

            return s;
        }

        private static int[] ToSpeedLast(int[] ivs)
        {
            var res = new int[6];
            res[0] = ivs[0];
            res[1] = ivs[1];
            res[2] = ivs[2];
            res[3] = ivs[4];
            res[4] = ivs[5];
            res[5] = ivs[3];
            return res;
        }

        private string TeraEmoji(string tera) => Config.EnableEmoji ? Config.Emoji[tera] : tera;
    }
}
