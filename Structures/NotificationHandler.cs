using Newtonsoft.Json;
using PKHeX.Core;
using PKHeX.Drawing.PokeSprite;
using System.Text;

namespace RaidCrawler.Structures
{
    public static class NotificationHandler
    {
        private static HttpClient? _client;
        public static HttpClient Client
        {
            get {
                if (_client == null)
                    _client = new HttpClient();
                return _client;
            }
        }

        public static string? DiscordWebhook = Properties.Settings.Default.CfgEnableNotification ? Properties.Settings.Default.CfgDiscordWebhook : null;

        public static void SendNotifications(ITeraRaid? encounter, Raid raid, RaidFilter filter)
        {
            if (encounter == null)
                return;
            if (DiscordWebhook == null || DiscordWebhook.Trim() == string.Empty)
                return;
            var webhook = GenerateWebhook(encounter, raid, filter);
            var content = new StringContent(JsonConvert.SerializeObject(webhook), Encoding.UTF8, "application/json");
            Client.PostAsync(DiscordWebhook.Trim(), content).Wait();
        }

        public static object GenerateWebhook(ITeraRaid encounter, Raid raid, RaidFilter filter)
        {
            var param = Raid.GetParam(encounter);
            var blank = new PK9
            {
                Species = encounter.Species,
                Form = encounter.Form
            };
            Encounter9RNG.GenerateData(blank, param, EncounterCriteria.Unrestricted, raid.Seed);
            var species = $"{Raid.strings.Species[encounter.Species]}{(encounter.Form != 0 ? $"-{encounter.Form}" : "")}";
            var nature = $"{Raid.strings.Natures[blank.Nature]}";
            var ability = $"{Raid.strings.Ability[blank.Ability]}";
            var shiny = Raid.CheckIsShiny(raid, encounter);
            var teratype = Raid.GetTeraType(encounter, raid);
            var color = TypeColor.GetTypeSpriteColor((byte)teratype);
            var hexcolor = $"{color.R:X2}{color.G:X2}{color.B:X2}";
            var tera = $"{Raid.strings.types[teratype]}";
            var ivs = IVsString(ToSpeedLast(blank.IVs));
            var sprite_name = SpriteName.GetResourceStringSprite(blank.Species, blank.Form, blank.Gender, blank.FormArgument, blank.Generation, shiny);
            var moves = new ushort[4] { encounter.Move1, encounter.Move2, encounter.Move3, encounter.Move4 };
            var movestr = string.Concat(moves.Where(z => z != 0).Select(z => $"- {Raid.strings.Move[z]}\n")).Trim();
            var extramoves = string.Concat(encounter.ExtraMoves.Where(z => z != 0).Select(z => $"- {Raid.strings.Move[z]}\n")).Trim();
            var SuccessWebHook = new
            {
                username = $"RaidCrawler",
                avatar_url = "https://www.serebii.net/scarletviolet/ribbons/mightiestmark.png",
                embeds = new List<object>
                {
                    new
                    {
                        title = $"{species}{(shiny ? " ⭐" : "")}",
                        description = $"Stop condition `{filter.Name}` met. Raid found.",
                        color = int.Parse(hexcolor, System.Globalization.NumberStyles.HexNumber),
                        thumbnail = new
                        {
                            url = $"https://github.com/kwsch/PKHeX/blob/master/PKHeX.Drawing.PokeSprite/Resources/img/Artwork%20Pokemon%20Sprites/a{sprite_name}.png?raw=true"
                        },
                        fields = new List<object>
                        {
                            new { name = "Stars", value = string.Concat(Enumerable.Repeat("☆", encounter.Stars)), inline = true, },
                            new { name = "Ability", value = ability, inline = true, },
                            new { name = "Tera Type", value = tera, inline = true, },
                            new { name = "Shiny", value = shiny, inline = true, },
                            new { name = "IVs", value = ivs, inline = true, },
                            new { name= "\u200B", value= "\u200B", inline= false },
                            new { name = "Moves", value = movestr, inline = true, },
                            new { name = "Extra Moves", value = extramoves == string.Empty ? "None" : extramoves, inline = true, },
                        },
                    }
                }
            };
            return SuccessWebHook;
        }

        private static string IVsString(int[] ivs, bool verbose = false)
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
    }
}
