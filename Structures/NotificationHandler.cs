using Newtonsoft.Json;
using PKHeX.Core;
using PKHeX.Drawing.PokeSprite;
using SysBot.Base;
using System.Text;

namespace RaidCrawler.Structures
{
    public static class NotificationHandler
    {
        private static HttpClient? _client;
        public static HttpClient Client
        {
            get
            {
                if (_client == null)
                    _client = new HttpClient();
                return _client;
            }
        }

        public static string[]? DiscordWebhooks;

        public static void SendNotifications(Config c, ITeraRaid? encounter, Raid raid, IEnumerable<RaidFilter> filters, String time, List<(int, int, int)>? RewardsList)
        {
            if (encounter == null)
                return;
            DiscordWebhooks = c.EnableNotification ? c.DiscordWebhook.Split(',') : null;
            if (DiscordWebhooks == null)
                return;
            var webhook = GenerateWebhook(c, encounter, raid, filters, time, RewardsList);
            var content = new StringContent(JsonConvert.SerializeObject(webhook), Encoding.UTF8, "application/json");
            foreach (var url in DiscordWebhooks)
                Client.PostAsync(url.Trim(), content).Wait();
        }

        public static void SendScreenshot(Config c, SwitchSocketAsync nx)
        {
            DiscordWebhooks = c.EnableNotification ? c.DiscordWebhook.Split(',') : null;
            if (DiscordWebhooks == null)
                return;
            var screenshot = SysBot.Base.Decoder.ConvertHexByteStringToBytes(nx.PixelPeek(new CancellationToken()).Result);
            var content = new MultipartFormDataContent();
            var info = new
            {
                username = $"RaidCrawler",
                avatar_url = "https://www.serebii.net/scarletviolet/ribbons/mightiestmark.png",
                content = "Switch Screenshot",
            };
            var basic_info = new StringContent(JsonConvert.SerializeObject(info), Encoding.UTF8, "application/json");
            content.Add(basic_info, "payload_json");
            content.Add(new ByteArrayContent(screenshot), "screenshot.jpg", "screenshot.jpg");
            foreach (var url in DiscordWebhooks)
                Client.PostAsync(url.Trim(), content).Wait();
        }

        public static object GenerateWebhook(Config c, ITeraRaid encounter, Raid raid, IEnumerable<RaidFilter> filters, String time, List<(int, int, int)>? RewardsList)
        {
            var param = Raid.GetParam(encounter);
            var blank = new PK9
            {
                Species = encounter.Species,
                Form = encounter.Form
            };
            Encounter9RNG.GenerateData(blank, param, EncounterCriteria.Unrestricted, raid.Seed);
            var emoji = c.EnableEmoji;
            var isevent = raid.IsEvent;
            var species = $"{Raid.strings.Species[encounter.Species]}{(encounter.Form != 0 ? $"-{encounter.Form}" : "")}";
            var dexNumber = SpeciesConverter.FromEncounterToDex(encounter.Species);
            var difficulty = Difficulty(c, encounter.Stars, isevent, emoji);
            var nature = $"{Raid.strings.Natures[blank.Nature]}";
            var ability = $"{Raid.strings.Ability[blank.Ability]}";
            var shiny = Shiny(c, Raid.CheckIsShiny(raid, encounter), ShinyExtensions.IsSquareShinyExist(blank), emoji);
            var gender = Gender(c, blank.Gender, emoji);
            var teratype = Raid.GetTeraType(encounter, raid);
            var color = TypeColor.GetTypeSpriteColor((byte)teratype);
            var hexcolor = $"{color.R:X2}{color.G:X2}{color.B:X2}";
            var tera = $"{Raid.strings.types[teratype]}";
            var teraemoji = TeraEmoji(c, $"{Raid.strings.types[teratype]}", emoji);
            var ivs = IVsStringEmoji(c, ToSpeedLast(blank.IVs), c.IVsStyle, c.IVsSpacer, c.VerboseIVs, emoji);
            var sprite_name = SpriteName.GetResourceStringSprite(dexNumber, blank.Form, blank.Gender, blank.FormArgument, blank.Generation, Raid.CheckIsShiny(raid, encounter));
            var moves = new ushort[4] { encounter.Move1, encounter.Move2, encounter.Move3, encounter.Move4 };
            var movestr = string.Concat(moves.Where(z => z != 0).Select(z => $"{Raid.strings.Move[z]}ㅤ\n")).Trim();
            var extramoves = string.Concat(encounter.ExtraMoves.Where(z => z != 0).Select(z => $"{Raid.strings.Move[z]}ㅤ\n")).Trim();
            var area = $"{Areas.Area[raid.Area - 1]}" + (c.ToggleDen ? $" [Den {raid.Den}]ㅤ" : "ㅤ");
            var instance = " " + c.InstanceName;
            var rewards = GetRewards(c, RewardsList, emoji);

            var SuccessWebHook = new
            {
                username = $"RaidCrawler" + instance,
                avatar_url = "https://www.serebii.net/scarletviolet/ribbons/mightiestmark.png",
                content = c.DiscordMessageContent,
                embeds = new List<object>
                {
                    new
                    {
                        title = $"{shiny} {species} {gender} {teraemoji}",
                        description = $"",
                        color = int.Parse(hexcolor, System.Globalization.NumberStyles.HexNumber),
                        thumbnail = new
                        {
                            url = $"https://github.com/kwsch/PKHeX/blob/master/PKHeX.Drawing.PokeSprite/Resources/img/Artwork%20Pokemon%20Sprites/a{sprite_name}.png?raw=true"
                        },
                        fields = new List<object>
                        {
                            new { name = "Difficultyㅤㅤㅤㅤㅤㅤ", value = difficulty, inline = true, },
                            new { name = "Natureㅤㅤㅤ", value = nature, inline = true },
                            new { name = "Ability", value = ability, inline = true, },

                            new { name = "IVs", value = ivs, inline = true, },
                            new { name = "Moves", value = movestr, inline = true, },
                            new { name = "Extra Moves", value = extramoves == string.Empty ? "None" : extramoves, inline = true, },

                            new { name = "Location󠀠󠀠󠀠", value = area, inline = true, },
                            new { name = "Search Time󠀠󠀠󠀠", value = time, inline = true, },
                            new { name = "Filter Name" + (filters.Count() > 1 ? "s" : string.Empty), value = string.Join(", ", filters.Select(z => z.Name)), inline = true, },

                            new { name = (rewards != "" ? "Rewards" : ""), value = rewards, inline = false, },
                        },
                    }
                }
            };
            return SuccessWebHook;
        }

        private static string Difficulty(Config c, byte stars, bool isevent, bool emoji)
        {
            string s = string.Empty;
            string mstar = (emoji ? c.Emoji["7 Star"] : ":star:");
            string bstar = (emoji ? c.Emoji["Event Star"] : ":star:");
            string ystar = (emoji ? c.Emoji["Star"] : ":star:");
            s = (stars == 7) ? string.Concat(Enumerable.Repeat(mstar, stars)) :
                (isevent) ? string.Concat(Enumerable.Repeat(bstar, stars)) : string.Concat(Enumerable.Repeat(ystar, stars));
            return s;
        }
        private static string Gender(Config c, int genderInt, bool emoji)
        {
            string gender = string.Empty;
            switch (genderInt)
            {
                case 0: gender = (emoji ? c.Emoji["Male"] : ":male_sign:"); break;
                case 1: gender = (emoji ? c.Emoji["Female"] : ":female_sign:"); break;
                case 2: gender = ""; break;
            }
            return gender;
        }

        private static string GetRewards(Config c, List<(int, int, int)>? rewards, bool emoji)
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
                    case 0645: abilitycapsule++; break;
                    case 0795: bottlecap++; break;
                    case 1606: abilitypatch++; break;
                    case 1904: sweetherba++; break;
                    case 1905: saltyherba++; break;
                    case 1906: sourherba++; break;
                    case 1907: bitterherba++; break;
                    case 1908: spicyherba++; break;
                }
            }

            s += (abilitycapsule > 0) ? (emoji ? $"`{abilitycapsule}`{c.Emoji["Ability Capsule"]} " : $"`{abilitycapsule}` Ability Capsule  ") : "";
            s += (bottlecap > 0) ? (emoji ? $"`{bottlecap}`{c.Emoji["Bottle Cap"]} " : $"`{bottlecap}` Bottle Cap  ") : "";
            s += (abilitypatch > 0) ? (emoji ? $"`{abilitypatch}`{c.Emoji["Ability Patch"]} " : $"`{abilitypatch}` Ability Patch  ") : "";
            s += (sweetherba > 0) ? (emoji ? $"`{sweetherba}`{c.Emoji["Sweet Herba"]} " : $"`{sweetherba}` Sweet Herba  ") : "";
            s += (saltyherba > 0) ? (emoji ? $"`{saltyherba}`{c.Emoji["Salty Herba"]} " : $"`{saltyherba}` Salty Herba  ") : "";
            s += (sourherba > 0) ? (emoji ? $"`{sourherba}`{c.Emoji["Sour Herba"]} " : $"`{sourherba}` Sour Herba  ") : "";
            s += (bitterherba > 0) ? (emoji ? $"`{bitterherba}`{c.Emoji["Bitter Herba"]} " : $"`{bitterherba}` Bitter Herba  ") : "";
            s += (spicyherba > 0) ? (emoji ? $"`{spicyherba}`{c.Emoji["Spicy Herba"]} " : $"`{spicyherba}` Spicy Herba  ") : "";

            return s;
        }

        private static string IVsStringEmoji(Config c, int[] ivs, int style, string spacer, bool verbose, bool emoji)
        {
            string s = string.Empty;
            var stats = new[] { "HP", "Atk", "Def", "SpA", "SpD", "Spe" };
            var iv0 = new[] { c.Emoji["Health 0"], c.Emoji["Attack 0"], c.Emoji["Defense 0"], c.Emoji["SpAttack 0"], c.Emoji["SpDefense 0"], c.Emoji["Speed 0"] };
            var iv31 = new[] { c.Emoji["Health 31"], c.Emoji["Attack 31"], c.Emoji["Defense 31"], c.Emoji["SpAttack 31"], c.Emoji["SpDefense 31"], c.Emoji["Speed 31"] };
            for (int i = 0; i < ivs.Length; i++)
            {
                switch (style)
                {
                    case 0:
                        switch (ivs[i])
                        {
                            case 0: s += (emoji) ? $"{iv0[i]:D2}{(verbose ? " " + stats[i] : string.Empty)}" : $"`{"✓":D2}`{(verbose ? " " + stats[i] : string.Empty)}"; break;
                            case 31: s += (emoji) ? $"{iv31[i]:D2}{(verbose ? " " + stats[i] : string.Empty)}" : $"`{"✓":D2}`{(verbose ? " " + stats[i] : string.Empty)}"; break;
                            default: s += $"`{ivs[i]:D2}`{(verbose ? " " + stats[i] : string.Empty)}"; break;
                        }
                        if (i < 5)
                            s += spacer.Replace("\"", "");
                        break;
                    case 1:
                        s += $"`{ivs[i]:D2}`{(verbose ? " " + stats[i] : string.Empty)}";
                        if (i < 5)
                            s += spacer.Replace("\"", "");
                        break;
                    case 2:
                        s += $"{ivs[i]:D2}{(verbose ? " " + stats[i] : string.Empty)}";
                        if (i < 5)
                            s += spacer.Replace("\"", "");
                        break;
                }
            }
            return s;
        }

        private static string Shiny(Config c, bool shiny, bool square, bool emoji)
        {
            string s = string.Empty;

            if (square && shiny)
            {
                s = $"{(emoji ? c.Emoji["Square Shiny"] : "Square shiny")}";
            }
            else if (shiny)
            {
                s = $"{(emoji ? c.Emoji["Shiny"] : "Shiny")}";
            }
            else
            {
                s = "";
            }

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

        private static string TeraEmoji(Config c, string tera, bool emoji)
        {
            string s = string.Empty;
            switch (tera)
            {
                case "Bug": s = (emoji ? c.Emoji["Bug"] : tera); break;
                case "Dark": s = (emoji ? c.Emoji["Dark"] : tera); break;
                case "Dragon": s = (emoji ? c.Emoji["Dragon"] : tera); break;
                case "Electric": s = (emoji ? c.Emoji["Electric"] : tera); break;
                case "Fairy": s = (emoji ? c.Emoji["Fairy"] : tera); break;
                case "Fighting": s = (emoji ? c.Emoji["Fighting"] : tera); break;
                case "Fire": s = (emoji ? c.Emoji["Fire"] : tera); break;
                case "Flying": s = (emoji ? c.Emoji["Flying"] : tera); break;
                case "Ghost": s = (emoji ? c.Emoji["Ghost"] : tera); break;
                case "Grass": s = (emoji ? c.Emoji["Grass"] : tera); break;
                case "Ground": s = (emoji ? c.Emoji["Ground"] : tera); break;
                case "Ice": s = (emoji ? c.Emoji["Ice"] : tera); break;
                case "Normal": s = (emoji ? c.Emoji["Normal"] : tera); break;
                case "Poison": s = (emoji ? c.Emoji["Poison"] : tera); break;
                case "Psychic": s = (emoji ? c.Emoji["Psychic"] : tera); break;
                case "Rock": s = (emoji ? c.Emoji["Rock"] : tera); break;
                case "Steel": s = (emoji ? c.Emoji["Steel"] : tera); break;
                case "Water": s = (emoji ? c.Emoji["Water"] : tera); break;
            }
            return s;
        }
    }
}
