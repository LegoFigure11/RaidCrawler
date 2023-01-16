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

        public static void SendNotifications(Config c, ITeraRaid? encounter, Raid raid, RaidFilter filter, String time, List<(int, int, int)>? RewardsList)
        {
            if (encounter == null)
                return;
            DiscordWebhooks = c.EnableNotification ? c.DiscordWebhook.Split(',') : null;
            if (DiscordWebhooks == null)
                return;
            var webhook = GenerateWebhook(c, encounter, raid, filter, time, RewardsList);
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

        public static object GenerateWebhook(Config c, ITeraRaid encounter, Raid raid, RaidFilter filter, String time, List<(int, int, int)>? RewardsList)
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
                        title = $"{(shiny ? (emoji ? "<:shiny:1060794151869874227>" : "Shiny") : "")} {species} {gender} {teraemoji}",
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
                            new { name = "Filter Name", value = filter.Name, inline = true, },

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
            string mstar = (emoji ? "<:raidStarM:1060475723405606994>" : "☆");
            string bstar = (emoji ? "<:raidStarB:1060475726572294144>" : "☆");
            string ystar = (emoji ? "<:raidStarY:1060475725498560512>" : "☆");
            s = (stars == 7) ? string.Concat(Enumerable.Repeat(mstar, stars)) :
                (isevent) ? string.Concat(Enumerable.Repeat(bstar, stars)) : string.Concat(Enumerable.Repeat(ystar, stars));
            return s;
        }
        private static string Gender(int genderInt, bool emoji)
        {
            string gender = string.Empty;
            switch (genderInt)
            {
                case 0: gender = (emoji ? "<:male:1060738367274352730>" : "Male"); break;
                case 1: gender = (emoji ? "<:female:1060738368541048965>" : "Female"); break;
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
                    case 0645: abilitycapsule ++; break;
                    case 0795: bottlecap ++; break;
                    case 1606: abilitypatch ++; break;
                    case 1904: sweetherba ++; break;
                    case 1905: saltyherba ++; break;
                    case 1906: sourherba ++; break;
                    case 1907: bitterherba ++; break;
                    case 1908: spicyherba ++; break;
                }
            }

            s += (abilitycapsule > 0) ? (emoji ? $"`{abilitycapsule}`<:abilitycapsule:1059122237019537478> " : $"`{abilitycapsule}` Ability Capsule  ") : "";
            s += (bottlecap > 0) ? (emoji ? $"`{bottlecap}`<:abilitypatch:1059123255283302450> " : $"`{bottlecap}` Bottle Cap  ") : "";
            s += (abilitypatch > 0) ? (emoji ? $"`{abilitypatch}`<:bottlecap:1058436109761265765> " : $"`{abilitypatch}` Ability Patch  ") : "";
            s += (sweetherba > 0) ? (emoji ? $"`{sweetherba}`<:herbaSweet:1058436152924844052> " : $"`{sweetherba}` Sweet Herba  ") : "";
            s += (saltyherba > 0) ? (emoji ? $"`{saltyherba}`<:herbaSalty:1058436153931464764> " : $"`{saltyherba}` Salty Herba  ") : "";
            s += (sourherba > 0) ? (emoji ? $"`{sourherba}`<:herbaSour:1058436114752475228> " : $"`{sourherba}` Sour Herba  ") : "";
            s += (bitterherba > 0) ? (emoji ? $"`{bitterherba}`<:herbaBitter:1058436112034562088> " : $"`{bitterherba}` Bitter Herba  ") : "";
            s += (spicyherba > 0) ? (emoji ? $"`{spicyherba}`<:herbaSpicy:1058436113276096614> " : $"`{spicyherba}` Spicy Herba  ") : "";

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
            var iv0 = new[] { "<:m1Health0:1063983356309688430>", "<:m2Attack0:1063983327385751683>", "<:m3Defence0:1063983331294838814>", "<:m4SpecialAttack0:1063983360294273084>", "<:m5SpecialDefence0:1063983385762082867>", "<:m6Speed0:1063983390052847659>" };
            var iv31 = new[] { "<:m1Health31:1063983357773500508>", "<:m2Attack31:1063983329097039992>", "<:m3Defence31:1063983333056458822>", "<:m4SpecialAttack31:1063983361619660861>", "<:m5SpecialDefence31:1063983387137822761>", "<:m6Speed31:1063983441672163469>" };
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
                            s += spacer.Replace("\"","");
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
                case "Bug": s = (emoji ? "<:tBug:1060235283976699995>" : tera); break;
                case "Dark": s = (emoji ? "<:tDark:1060235285394366564>" : tera); break;
                case "Dragon": s = (emoji ? "<:tDragon:1060235286879141917>" : tera); break;
                case "Electric": s = (emoji ? "<:tElectric:1060235288691093566>" : tera); break;
                case "Fairy": s = (emoji ? "<:tFairy:1060235282127003730>" : tera); break;
                case "Fighting": s = (emoji ? "<:tFighting:1060235325705822309>" : tera); break;
                case "Fire": s = (emoji ? "<:tFire:1060235326834102382>" : tera); break;
                case "Flying": s = (emoji ? "<:tFlying:1060235328717336646>" : tera); break;
                case "Ghost": s = (emoji ? "<:tGhost:1060235329665241129>" : tera); break;
                case "Grass": s = (emoji ? "<:tGrass:1060235303828332655>" : tera); break;
                case "Ground": s = (emoji ? "<:tGround:1060235355867058308>" : tera); break;
                case "Ice": s = (emoji ? "<:tIce:1060235356710109246>" : tera); break;
                case "Normal": s = (emoji ? "<:tNormal:1060235360334008331>" : tera); break;
                case "Poison": s = (emoji ? "<:tPoison:1060235353732161569>" : tera); break;
                case "Psychic": s = (emoji ? "<:tPsychic:1060235385235570811>" : tera); break;
                case "Rock": s = (emoji ? "<:tRock:1060235386279972906>" : tera); break;
                case "Steel": s = (emoji ? "<:tSteel:1060235358358491147>" : tera); break;
                case "Water": s = (emoji ? "<:tWater:1060235383411056640>" : tera); break;
            }
            return s;
        }
    }
}
