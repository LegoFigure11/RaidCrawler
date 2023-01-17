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
            var difficulty = Difficulty(encounter.Stars, isevent, emoji);
            var nature = $"{Raid.strings.Natures[blank.Nature]}";
            var ability = $"{Raid.strings.Ability[blank.Ability]}";
            var shiny = Raid.CheckIsShiny(raid, encounter);
            var gender = Gender(blank.Gender, emoji);
            var teratype = Raid.GetTeraType(encounter, raid);
            var color = TypeColor.GetTypeSpriteColor((byte)teratype);
            var hexcolor = $"{color.R:X2}{color.G:X2}{color.B:X2}";
            var tera = $"{Raid.strings.types[teratype]}";
            var teraemoji = TeraEmoji($"{Raid.strings.types[teratype]}", emoji);
            var ivs = IVsStringEmoji(ToSpeedLast(blank.IVs), c.IVsStyle, c.IVsSpacer, c.VerboseIVs, emoji);
            var sprite_name = SpriteName.GetResourceStringSprite(blank.Species, blank.Form, blank.Gender, blank.FormArgument, blank.Generation, shiny);
            var moves = new ushort[4] { encounter.Move1, encounter.Move2, encounter.Move3, encounter.Move4 };
            var movestr = string.Concat(moves.Where(z => z != 0).Select(z => $"{Raid.strings.Move[z]}ㅤ\n")).Trim();
            var extramoves = string.Concat(encounter.ExtraMoves.Where(z => z != 0).Select(z => $"{Raid.strings.Move[z]}ㅤ\n")).Trim();
            var area = $"{Areas.Area[raid.Area - 1]}" + (c.ToggleDen ? $" [Den {raid.Den}]ㅤ" : "ㅤ");
            var instance = " " + c.InstanceName;
            var rewards = GetRewards(RewardsList, emoji);
            var SuccessWebHook = new
            {
                username = $"RaidCrawler" + instance,
                avatar_url = "https://www.serebii.net/scarletviolet/ribbons/mightiestmark.png",
                content = c.DiscordMessageContent,
                embeds = new List<object>
                {
                    new
                    {
                        title = $"{(shiny ? (emoji ? "<:shiny:1064845915036323840>" : "Shiny") : "")} {species} {gender} {teraemoji}",
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

        private static string Difficulty(byte stars, bool isevent, bool emoji)
        {
            string s = string.Empty;
            string mstar = (emoji ? "<:pinkstar:1064538642934140978>" : ":star:");
            string bstar = (emoji ? "<:bluestar:1064538604409471016>" : ":star:");
            string ystar = (emoji ? "<:yellowstar:1064538672113922109>" : ":star:");
            s = (stars == 7) ? string.Concat(Enumerable.Repeat(mstar, stars)) :
                (isevent) ? string.Concat(Enumerable.Repeat(bstar, stars)) : string.Concat(Enumerable.Repeat(ystar, stars));
            return s;
        }
        private static string Gender(int genderInt, bool emoji)
        {
            string gender = string.Empty;
            switch (genderInt)
            {
                case 0: gender = (emoji ? "<:male:1064844611341795398>" : ":male_sign:"); break;
                case 1: gender = (emoji ? "<:female:1064844510636552212>" : ":female_sign:"); break;
                case 2: gender = ""; break;
            }
            return gender;
        }

        private static string GetRewards(List<(int, int, int)>? rewards, bool emoji)
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

            s += (abilitycapsule > 0) ? (emoji ? $"`{abilitycapsule}`<:abilitycapsule:1064541406921752737> " : $"`{abilitycapsule}` Ability Capsule  ") : "";
            s += (bottlecap > 0) ? (emoji ? $"`{bottlecap}`<:bottlecap:1064537470370320495> " : $"`{bottlecap}` Bottle Cap  ") : "";
            s += (abilitypatch > 0) ? (emoji ? $"`{abilitypatch}`<:abilitypatch:1064538087763476522> " : $"`{abilitypatch}` Ability Patch  ") : "";
            s += (sweetherba > 0) ? (emoji ? $"`{sweetherba}`<:sweetherba:1064541764163227759> " : $"`{sweetherba}` Sweet Herba  ") : "";
            s += (saltyherba > 0) ? (emoji ? $"`{saltyherba}`<:saltyherba:1064541768147796038> " : $"`{saltyherba}` Salty Herba  ") : "";
            s += (sourherba > 0) ? (emoji ? $"`{sourherba}`<:sourherba:1064541770148483073> " : $"`{sourherba}` Sour Herba  ") : "";
            s += (bitterherba > 0) ? (emoji ? $"`{bitterherba}`<:bitterherba:1064541773763977256> " : $"`{bitterherba}` Bitter Herba  ") : "";
            s += (spicyherba > 0) ? (emoji ? $"`{spicyherba}`<:spicyherba:1064541776699994132> " : $"`{spicyherba}` Spicy Herba  ") : "";

            return s;
        }

        private static string IVsString(int[] ivs, bool verbose)
        {
            string s = string.Empty;
            var stats = new[] { "HP", "Atk", "Def", "SpA", "SpD", "Spe" };
            for (int i = 0; i < ivs.Length; i++)
            {
                s += $"{ivs[i]:D2}{(verbose ? " " + stats[i] : string.Empty)}";
                if (i < 5)
                    s += "/";
            }
            return s;
        }

        private static string IVsStringEmoji(int[] ivs, int style, string spacer, bool verbose, bool emoji)
        {
            string s = string.Empty;
            var stats = new[] { "HP", "Atk", "Def", "SpA", "SpD", "Spe" };
            var iv0 = new[] { "<:h0:1064842950573572126>", "<:a0:1064842895712075796>", "<:b0:1064842811196833832>", "<:c0:1064842749272133752>", "<:d0:1064842668624068608>", "<:s0:1064842545953243176>" };
            var iv31 = new[] { "<:h31:1064726680628895784>", "<:a31:1064726668419289138>", "<:b31:1064726671703429220>", "<:c31:1064726673649582121>", "<:d31:1064726677176987832>", "<:s31:1064726682721865818>" };
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

        private static string TeraEmoji(string tera, bool emoji)
        {
            string s = string.Empty;
            switch (tera)
            {
                case "Bug": s = (emoji ? "<:bug:1064546304048496812>" : tera); break;
                case "Dark": s = (emoji ? "<:dark:1064557656079085588>" : tera); break;
                case "Dragon": s = (emoji ? "<:dragon:1064557631890538566>" : tera); break;
                case "Electric": s = (emoji ? "<:electric:1064557559563943956>" : tera); break;
                case "Fairy": s = (emoji ? "<:fairy:1064557682566123701>" : tera); break;
                case "Fighting": s = (emoji ? "<:fighting:1064546289406189648>" : tera); break;
                case "Fire": s = (emoji ? "<:fire:1064557482468446230>" : tera); break;
                case "Flying": s = (emoji ? "<:flying:1064546291239104623>" : tera); break;
                case "Ghost": s = (emoji ? "<:ghost:1064546307848536115>" : tera); break;
                case "Grass": s = (emoji ? "<:grass:1064557534096130099>" : tera); break;
                case "Ground": s = (emoji ? "<:ground:1064546296725241988>" : tera); break;
                case "Ice": s = (emoji ? "<:ice:1064557609857863770>" : tera); break;
                case "Normal": s = (emoji ? "<:normal:1064546286247886938>" : tera); break;
                case "Poison": s = (emoji ? "<:poison:1064546294854586400>" : tera); break;
                case "Psychic": s = (emoji ? "<:psychic:1064557585124049006>" : tera); break;
                case "Rock": s = (emoji ? "<:rock:1064546299992625242>" : tera); break;
                case "Steel": s = (emoji ? "<:steel:1064557443742453790>" : tera); break;
                case "Water": s = (emoji ? "<:water:1064557509404270642>" : tera); break;
            }
            return s;
        }
    }
}
