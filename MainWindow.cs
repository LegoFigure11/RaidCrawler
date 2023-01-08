using Newtonsoft.Json;
using PKHeX.Core;
using PKHeX.Drawing;
using PKHeX.Drawing.PokeSprite;
using RaidCrawler.Properties;
using RaidCrawler.Structures;
using RaidCrawler.Subforms;
using SysBot.Base;
using System.Data;
using System.Net.Sockets;
using System.Text;
using static RaidCrawler.Structures.Offsets;
using static SysBot.Base.SwitchButton;
using static System.Buffers.Binary.BinaryPrimitives;

namespace RaidCrawler
{
    public partial class MainWindow : Form
    {

        private readonly static SwitchConnectionConfig Config = new() { Protocol = SwitchProtocol.WiFi, IP = Settings.Default.SwitchIP, Port = 6000 };
        private readonly static SwitchSocketAsync SwitchConnection = new(Config);

        private readonly static OffsetUtil OffsetUtil = new(SwitchConnection);

        private readonly List<Raid> Raids = new();
        private readonly List<ITeraRaid?> Encounters = new();
        private List<uint> prev = new();
        private List<RaidFilter> RaidFilters = new();
        private static readonly Image map = Image.FromStream(new MemoryStream(Utils.GetBinaryResource("paldea.png")));
        private static Dictionary<string, float[]>? den_locations;

        // rewards
        private readonly List<List<(int, int, int)>?> RewardsList = new();

        private int index = 0;
        private ulong offset;
        private bool IsReading = false;
        private bool HideSeed = false;
        private bool ShowExtraMoves = false;

        private Color DefaultColor;
        private FormWindowState _WindowState;

        public MainWindow()
        {
            string build = string.Empty;
#if DEBUG
            var date = File.GetLastWriteTime(System.Reflection.Assembly.GetEntryAssembly()!.Location);
            build = $" (dev-{date:yyyyMMdd})";
#endif
            var v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version!;
            Text = "RaidCrawler v" + v.Major + "." + v.Minor + "." + v.Build + build;

            var raid_data = new[]
            {
                "raid_enemy_01_array.bin",
                "raid_enemy_02_array.bin",
                "raid_enemy_03_array.bin",
                "raid_enemy_04_array.bin",
                "raid_enemy_05_array.bin",
                "raid_enemy_06_array.bin",
            };

            Raid.GemTeraRaids = TeraEncounter.GetAllEncounters(raid_data);
            var filterpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "filters.json");
            if (File.Exists(filterpath))
                RaidFilters = JsonConvert.DeserializeObject<List<RaidFilter>>(File.ReadAllText(filterpath)) ?? new List<RaidFilter>();
            den_locations = JsonConvert.DeserializeObject<Dictionary<string, float[]>>(Utils.GetStringResource("den_locations.json") ?? "{}");

            // load rewards
            Raid.BaseFixedRewards = JsonConvert.DeserializeObject<List<RaidFixedRewards>>(Utils.GetStringResource("raid_fixed_reward_item_array.json") ?? "[]");
            Raid.BaseLotteryRewards = JsonConvert.DeserializeObject<List<RaidLotteryRewards>>(Utils.GetStringResource("raid_lottery_reward_item_array.json") ?? "[]");

            Raid.Game = Settings.Default.Game;
            SpriteBuilder.ShowTeraThicknessStripe = 0x4;
            SpriteBuilder.ShowTeraOpacityStripe = 0xAF;
            SpriteBuilder.ShowTeraOpacityBackground = 0xFF;
            SpriteUtil.ChangeMode(SpriteBuilderMode.SpritesArtwork5668);

            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Location = Settings.Default.CfgLocation;
            if (Location.X == 0 && Location.Y == 0)
                CenterToScreen();
            InputSwitchIP.Text = Settings.Default.SwitchIP;
            DefaultColor = IVs.BackColor;
            Progress.SelectedIndex = Settings.Default.Progress;
            EventProgress.SelectedIndex = Settings.Default.EventProgress;
            Game.SelectedIndex = Game.FindString(Settings.Default.Game);
            RaidBoost.SelectedIndex = 0;
        }

        private void InputSwitchIP_Changed(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text != "192.168.0.0")
            {
                Settings.Default.SwitchIP = textBox.Text;
                Config.IP = textBox.Text;
            }
            Settings.Default.Save();
        }

        private void Disconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
            ConnectionStatusText.Text = "Disconnected.";
            ButtonConnect.Enabled = true;
            ButtonDisconnect.Enabled = false;
            ButtonReadRaids.Enabled = false;
            ButtonAdvanceDate.Enabled = false;
            ButtonViewRAM.Enabled = false;
            ButtonDownloadEvents.Enabled = false;
        }

        private async void Connect()
        {
            if (!SwitchConnection.Connected)
            {
                try
                {
                    ConnectionStatusText.Text = "Connecting...";
                    SwitchConnection.Connect();
                    ConnectionStatusText.Text = "Connected!";
                    IsReading = true;
                    ConnectionStatusText.Text = "Detecting game version...";
                    string id = await GetGameID(CancellationToken.None);
                    if (id is ScarletID)
                    {
                        Game.SelectedIndex = Game.FindString("Scarlet");
                    }
                    else if (id is VioletID)
                    {
                        Game.SelectedIndex = Game.FindString("Violet");
                    }
                    else
                    {
                        MessageBox.Show("Unable to detect Pokémon Scarlet or Pokémon Violet running on your switch!");
                        Disconnect();
                        IsReading = false;
                    }

                    ConnectionStatusText.Text = "Reading story progress...";
                    Progress.SelectedIndex = await GetStoryProgress(CancellationToken.None);
                    EventProgress.SelectedIndex = Math.Min(Progress.SelectedIndex, 3);

                    ConnectionStatusText.Text = "Reading event raid status...";
                    await ReadEventRaids();

                    ConnectionStatusText.Text = "Reading raids...";
                    await ReadRaids(CancellationToken.None);

                    IsReading = false;
                    ButtonAdvanceDate.Enabled = true;
                    ButtonReadRaids.Enabled = true;
                    ButtonConnect.Enabled = false;
                    ButtonDisconnect.Enabled = true;
                    ButtonViewRAM.Enabled = true;
                    ButtonDownloadEvents.Enabled = true;
                }
                catch (SocketException err)
                {
                    Disconnect();
                    // a bit hacky but it works
                    if (err.Message.Contains("failed to respond") || err.Message.Contains("actively refused"))
                    {
                        ConnectionStatusText.Text = "Unable to connect.";
                        MessageBox.Show(err.Message);
                    }
                }
            }
        }

        private static async void Disconnect()
        {
            if (SwitchConnection.Connected)
            {
                await SwitchConnection.SendAsync(SwitchCommand.DetachController(true), CancellationToken.None).ConfigureAwait(false);
                SwitchConnection.Disconnect();
            }
        }

        private static async Task<int> GetStoryProgress(CancellationToken token)
        {
            int progress = 0;
            for (int i = DifficultyFlags.Length - 1; i > 0 && progress == 0; i--)
            {
                // See https://github.com/Lincoln-LM/sv-live-map/pull/43
                var block = await ReadSaveBlock(DifficultyFlags[i].Item1, 1, DifficultyFlags[i].Item2, token);
                if (block[0] == 2) return i + 1;
            }
            return progress;
        }

        public static async Task<byte[]> ReadSaveBlock(int offset, int size, uint key, CancellationToken token)
        {
            (offset, key) = await SearchSaveBlock(offset, key, token);
            var block_ofs = await OffsetUtil.GetPointerAddress($"[{SaveBlockPointer}+{offset + 8:X}]", token);
            var block = await SwitchConnection.ReadBytesAbsoluteAsync(block_ofs, size, token);
            return DecryptBlock(key, block);
        }

        public static async Task<byte[]> ReadSaveBlockObject(int offset, uint key, CancellationToken token)
        {
            (offset, key) = await SearchSaveBlock(offset, key, token);
            var header_ofs = await OffsetUtil.GetPointerAddress($"[{SaveBlockPointer}+{offset + 8:X}]", token);
            var header = await SwitchConnection.ReadBytesAbsoluteAsync(header_ofs, 5, token);
            header = DecryptBlock(key, header);
            var size = ReadUInt32LittleEndian(header[1..]);
            var obj = await SwitchConnection.ReadBytesAbsoluteAsync(header_ofs, 5 + (int)size, token);
            return DecryptBlock(key, obj)[5..];
        }

        public static async Task<byte[]> ReadBlockDefault((int, uint) offsets, string? cache = null, bool force = false)
        {
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "cache");
            Directory.CreateDirectory(folder);
            if (force == false && cache != null && File.Exists(Path.Combine(folder, cache)))
                return File.ReadAllBytes(Path.Combine(folder, cache));
            var bin = await ReadSaveBlockObject(offsets.Item1, offsets.Item2, CancellationToken.None);
            if (cache != null)
                File.WriteAllBytes(Path.Combine(folder, cache), bin);
            return bin;
        }

        public static async Task ReadEventRaids(bool force = false)
        {
            var prio_file = Path.Combine(Directory.GetCurrentDirectory(), "cache", "raid_priority_array");
            if (File.Exists(prio_file))
            {
                (_, var version) = FlatbufferDumper.DumpDeliveryPriorities(File.ReadAllBytes(prio_file));
                var blk = await ReadBlockDefault(BCATRaidPriorityLocation, "raid_priority_array.tmp", true);
                (_, var v2) = FlatbufferDumper.DumpDeliveryPriorities(blk);
                if (version != v2)
                    force = true;
                var tmp_file = Path.Combine(Directory.GetCurrentDirectory(), "cache", "raid_priority_array.tmp");
                if (File.Exists(tmp_file))
                    File.Delete(tmp_file);
                if (v2 == 0) // raid reset
                    return;
            }
            var delivery_raid_prio = await ReadBlockDefault(BCATRaidPriorityLocation, "raid_priority_array", force);
            (Raid.DeliveryRaidPriority, var priority) = FlatbufferDumper.DumpDeliveryPriorities(delivery_raid_prio);
            if (priority == 0)
                return;

            var delivery_raid_fbs = await ReadBlockDefault(BCATRaidBinaryLocation, "raid_enemy_array", force);
            var delivery_fixed_rewards = await ReadBlockDefault(BCATRaidFixedRewardLocation, "fixed_reward_item_array", force);
            var delivery_lottery_rewards = await ReadBlockDefault(BCATRaidLotteryRewardLocation, "lottery_reward_item_array", force);

            Raid.DistTeraRaids = TeraDistribution.GetAllEncounters(delivery_raid_fbs);
            Raid.DeliveryRaidFixedRewards = FlatbufferDumper.DumpFixedRewards(delivery_fixed_rewards);
            Raid.DeliveryRaidLotteryRewards = FlatbufferDumper.DumpLotteryRewards(delivery_lottery_rewards);
        }

        public static async Task<(int, uint)> SearchSaveBlock(int base_offset, uint? key, CancellationToken token)
        {
            var key_addr = await OffsetUtil.GetPointerAddress($"{SaveBlockPointer}+{base_offset:X}", token);
            var read_key = ReadUInt32LittleEndian(await SwitchConnection.ReadBytesAbsoluteAsync(key_addr, 4, token));
            if (key == null)
                return (base_offset, read_key);
            if (read_key == key)
                return (base_offset, read_key);
            var direction = key > read_key ? 1 : -1;
            for (int offset = base_offset; offset < base_offset + 0x1000 && offset > base_offset - 0x1000; offset += direction * 0x20)
            {
                key_addr = await OffsetUtil.GetPointerAddress($"{SaveBlockPointer}+{offset:X}", token);
                read_key = ReadUInt32LittleEndian(await SwitchConnection.ReadBytesAbsoluteAsync(key_addr, 4, token));
                if (read_key == key)
                    return (offset, read_key);
            }
            throw new ArgumentOutOfRangeException("Save block not found in range +- 0x1000");
        }

        private static byte[] DecryptBlock(uint key, byte[] block)
        {
            var rng = new SCXorShift32(key);
            for (int i = 0; i < block.Length; i++)
                block[i] = (byte)(block[i] ^ rng.Next());
            return block;
        }

        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void DisplayRaid(int index)
        {
            if (Raids.Count > index)
            {
                Raid raid = Raids[index];
                var encounter = Encounters[index];

                Seed.Text = !HideSeed ? $"{raid.Seed:X8}" : "Hidden";
                EC.Text = !HideSeed ? $"{raid.EC:X8}" : "Hidden";
                PID.Text = GetPIDString(raid, encounter);
                Area.Text = $"{Areas.Area[raid.Area - 1]} - Den {raid.Den}";
                IsEvent.Checked = raid.IsEvent;

                var teratype = Raid.GetTeraType(encounter, raid);
                TeraType.Text = $"{Raid.strings.types[teratype]} ({teratype})";
                int StarCount = encounter is TeraDistribution ? encounter.Stars : Raid.GetStarCount(raid.Difficulty, Progress.SelectedIndex, raid.IsBlack);
                Difficulty.Text = raid.IsEvent ? string.Concat(Enumerable.Repeat("☆", StarCount)) : string.Concat(Enumerable.Repeat("☆", StarCount)) + $" ({raid.Difficulty})";

                if (encounter != null)
                {
                    var param = Raid.GetParam(encounter);
                    var blank = new PK9
                    {
                        Species = encounter.Species,
                        Form = encounter.Form
                    };
                    Encounter9RNG.GenerateData(blank, param, EncounterCriteria.Unrestricted, raid.Seed);
                    var img = blank.Sprite(SpriteBuilderTweak.None);
                    img = ApplyTeraColor((byte)teratype, img, SpriteBackgroundType.BottomStripe);
                    Species.Text = $"{Raid.strings.Species[encounter.Species]}{(encounter.Form != 0 ? $"-{encounter.Form}" : "")}";
                    Sprite.Image = img;
                    GemIcon.Image = PKHeX.Drawing.Misc.TypeSpriteUtil.GetTypeSpriteGem((byte)teratype);
                    Gender.Text = $"{(Gender)blank.Gender}";
                    var nature = blank.Nature;
                    Nature.Text = $"{Raid.strings.Natures[nature]}";
                    Ability.Text = $"{Raid.strings.Ability[blank.Ability]}";
                    var extra_moves = new ushort[] { 0, 0, 0, 0 };
                    for (int i = 0; i < encounter.ExtraMoves.Length; i++)
                        if (i < extra_moves.Count()) extra_moves[i] = encounter.ExtraMoves[i];
                    Move1.Text = ShowExtraMoves ? Raid.strings.Move[extra_moves[0]] : Raid.strings.Move[encounter.Move1];
                    Move2.Text = ShowExtraMoves ? Raid.strings.Move[extra_moves[1]] : Raid.strings.Move[encounter.Move2];
                    Move3.Text = ShowExtraMoves ? Raid.strings.Move[extra_moves[2]] : Raid.strings.Move[encounter.Move3];
                    Move4.Text = ShowExtraMoves ? Raid.strings.Move[extra_moves[3]] : Raid.strings.Move[encounter.Move4];
                    IVs.Text = IVsString(ToSpeedLast(blank.IVs));
                    toolTip.SetToolTip(IVs, IVsString(ToSpeedLast(blank.IVs), true));
                }
                else
                {
                    Species.Text = string.Empty;
                    Move1.Text = string.Empty;
                    Move2.Text = string.Empty;
                    Move3.Text = string.Empty;
                    Move4.Text = string.Empty;
                    IVs.Text = string.Empty;
                    Gender.Text = string.Empty;
                    Nature.Text = string.Empty;
                    Ability.Text = string.Empty;
                }

                PID.BackColor = Raid.CheckIsShiny(raid, encounter) ? Color.Gold : DefaultColor;
                IVs.BackColor = IVs.Text is "31/31/31/31/31/31" ? Color.YellowGreen : DefaultColor;
            }
            else
            {
                MessageBox.Show($"Unable to display raid at index {index}. Ensure there are no cheats running or anything else that might shift RAM (Edizon, overlays, etc.), then reboot your console and try again.");
            }
        }

        private string GetPIDString(Raid raid, ITeraRaid? enc)
        {
            var shiny_mark = " (☆)";
            if (HideSeed)
                return "Hidden";
            var pid = $"{raid.PID:X8}";
            return Raid.CheckIsShiny(raid, enc) ? pid + shiny_mark : pid;
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

        private static Image ApplyTeraColor(byte elementalType, Image img, SpriteBackgroundType type)
        {
            var color = TypeColor.GetTypeSpriteColor(elementalType);
            var thk = SpriteBuilder.ShowTeraThicknessStripe;
            var op = SpriteBuilder.ShowTeraOpacityStripe;
            var bg = SpriteBuilder.ShowTeraOpacityBackground;
            return ApplyColor(img, type, color, thk, op, bg);
        }

        private static Image ApplyColor(Image img, SpriteBackgroundType type, Color color, int thick, byte opacStripe, byte opacBack)
        {
            if (type == SpriteBackgroundType.BottomStripe)
            {
                int stripeHeight = thick; // from bottom
                if ((uint)stripeHeight > img.Height) // clamp negative & too-high values back to height.
                    stripeHeight = img.Height;

                return ImageUtil.BlendTransparentTo(img, color, opacStripe, img.Width * 4 * (img.Height - stripeHeight));
            }
            if (type == SpriteBackgroundType.TopStripe)
            {
                int stripeHeight = thick; // from top
                if ((uint)stripeHeight > img.Height) // clamp negative & too-high values back to height.
                    stripeHeight = img.Height;

                return ImageUtil.BlendTransparentTo(img, color, opacStripe, 0, (img.Width * 4 * stripeHeight) - 4);
            }
            if (type == SpriteBackgroundType.FullBackground) // full background
            {
                return ImageUtil.BlendTransparentTo(img, color, opacBack);
            }
            return img;
        }

        private static Image? GenerateMap(Raid raid, int teratype)
        {
            var original = PKHeX.Drawing.Misc.TypeSpriteUtil.GetTypeSpriteGem((byte)teratype);
            if (original == null)
                return null;
            var gem = (Image)new Bitmap(original, new Size(30, 30));
            if (den_locations == null || den_locations.Count == 0)
                return null;
            try
            {
                var x = (den_locations[$"{raid.Area}-{raid.Den}"][0] + 2.072021484) * 512 / 5000;
                var y = (den_locations[$"{raid.Area}-{raid.Den}"][2] + 5255.240018) * 512 / 5000;
                var mapimg = ImageUtil.LayerImage(map, gem, (int)x, (int)y);
                if (den_locations.ContainsKey($"{raid.Area}-{raid.Den}_"))
                {
                    x = (den_locations[$"{raid.Area}-{raid.Den}_"][0] + 2.072021484) * 512 / 5000;
                    x = (den_locations[$"{raid.Area}-{raid.Den}_"][0] + 2.072021484) * 512 / 5000;
                    mapimg = ImageUtil.LayerImage(mapimg, gem, (int)x, (int)y);
                }
                return mapimg;
            }
            catch { return null; }
        }

        private void ButtonPrevious_Click(object sender, EventArgs e)
        {
            if (Raids.Count > 0)
            {
                index = (index + Raids.Count - 1) % Raids.Count; // Wrap around
                if (ModifierKeys == Keys.Shift)
                {
                    for (int i = 0; i < Raids.Count; i++)
                    {
                        var chk = (index + Raids.Count - i) % Raids.Count;
                        if (StopAdvances || RaidFilters.Any(z => z.FilterSatisfied(Encounters[chk], Raids[chk], RaidBoost.SelectedIndex)))
                        {
                            index = chk;
                            break;
                        }
                    }
                }
                ComboIndex.SelectedIndex = index;
            }
        }

        private void ButtonNext_Click(object sender, EventArgs e)
        {
            if (Raids.Count > 0)
            {
                index = (index + Raids.Count + 1) % Raids.Count; // Wrap around
                if (ModifierKeys == Keys.Shift)
                {
                    for (int i = 0; i < Raids.Count; i++)
                    {
                        var chk = (index + Raids.Count + i) % Raids.Count;
                        if (StopAdvances || RaidFilters.Any(z => z.FilterSatisfied(Encounters[chk], Raids[chk], RaidBoost.SelectedIndex)))
                        {
                            index = chk;
                            break;
                        }
                    }
                }
                ComboIndex.SelectedIndex = index;
            }
        }

        private static new async Task Click(SwitchButton button, int delay, CancellationToken token)
        {
            await SwitchConnection.SendAsync(SwitchCommand.Click(button, true), token).ConfigureAwait(false);
            await Task.Delay(delay, token).ConfigureAwait(false);
        }


        private static async Task Touch(int x, int y, int hold, int delay, CancellationToken token)
        {
            var command = Encoding.ASCII.GetBytes($"touchHold {x} {y} {hold}\r\n");
            await SwitchConnection.SendAsync(command, token).ConfigureAwait(false);
            await Task.Delay(delay, token).ConfigureAwait(false);
        }

        private static async Task PressAndHold(SwitchButton b, int hold, int delay, CancellationToken token)
        {
            await SwitchConnection.SendAsync(SwitchCommand.Hold(b, true), token).ConfigureAwait(false);
            await Task.Delay(hold, token).ConfigureAwait(false);
            await SwitchConnection.SendAsync(SwitchCommand.Release(b, true), token).ConfigureAwait(false);
            await Task.Delay(delay, token).ConfigureAwait(false);
        }

        private static async Task<string> GetGameID(CancellationToken token) => await SwitchConnection.GetTitleID(token).ConfigureAwait(false);
        private bool StopAdvances => RaidFilters.Count == 0 || RaidFilters.All(x => x.Enabled == false);

        private async Task AdvanceDate(CancellationToken token)
        {
            ConnectionStatusText.Text = "Changing date...";
            int BaseDelay = (int)Settings.Default.CfgBaseDelay;
            await Click(LSTICK, 0_050 + BaseDelay, token).ConfigureAwait(false); // Sometimes it seems like the first command doesn't go through so send this just in case
            // HOME Menu
            await Click(HOME, (int)Settings.Default.CfgOpenHome + BaseDelay, token).ConfigureAwait(false);
            // Navigate to Settings
            if (Settings.Default.CfgUseTouch)
            {
                await Touch(840, 540, 0_050, BaseDelay, token).ConfigureAwait(false);
            }
            else
            {
                await Click(DDOWN, (int)Settings.Default.CfgNavigateToSettings + 0_100 + BaseDelay, token).ConfigureAwait(false);
                for (int i = 0; i < 5; i++) await Click(DRIGHT, (int)Settings.Default.CfgNavigateToSettings + BaseDelay, token).ConfigureAwait(false);
            }
            await Click(A, (int)Settings.Default.CfgOpenSettings + BaseDelay, token).ConfigureAwait(false);
            // Scroll to bottom
            await PressAndHold(DDOWN, (int)Settings.Default.CfgHold, BaseDelay, token).ConfigureAwait(false);

            // Navigate to "Date and Time"
            await Click(DRIGHT, 0_200 + BaseDelay, token).ConfigureAwait(false);
            // Hold down to overshoot Date/Time by one. DUP to recover.
            if (Settings.Default.CfgUseOvershoot)
            {
                await PressAndHold(DDOWN, (int)Settings.Default.CfgSystemOvershoot, 0, token).ConfigureAwait(false);
                await Click(DUP, 0_500, token).ConfigureAwait(false);
            }
            else
                // I tried using holds here but could not get the timing consistent
                // Even if this is slightly slower, it is at least consistent
                // And not missing any cycles means it's faster overall
                for (int i = 0; i < Settings.Default.CfgSystemDDownPresses; i++) 
                    await Click(DDOWN, 0_050 + BaseDelay, token).ConfigureAwait(false);
            await Click(A, (int)Settings.Default.CfgSubmenu + BaseDelay, token).ConfigureAwait(false);

            // Navigate to Change Date/Time
            if (Settings.Default.CfgUseTouch)
            {
                await Touch(840, 400, 0_050, 0_300 + BaseDelay, token).ConfigureAwait(false);
            }
            else
            {
                for (int i = 0; i < 2; i++) await Click(DDOWN, 0_200 + BaseDelay, token).ConfigureAwait(false);
                await Click(A, (int)Settings.Default.CfgDateChange + BaseDelay, token).ConfigureAwait(false);
            }

            // Change the date
            for (int i = 0; i < Settings.Default.CfgDaysToSkip; i++) await Click(DUP, 0_200 + BaseDelay, token).ConfigureAwait(false); // Not actually necessary, so we default to 0 as per #29


            for (int i = 0; i < 6; i++) await Click(DRIGHT, 0_050 + BaseDelay, token).ConfigureAwait(false);
            await Click(A, 0_500 + BaseDelay, token).ConfigureAwait(false);

            // Return to game
            await Click(HOME, (int)Settings.Default.CfgReturnHome + BaseDelay, token).ConfigureAwait(false);
            await Click(HOME, (int)Settings.Default.CfgReturnGame + BaseDelay, token).ConfigureAwait(false);
        }

        private async void ButtonAdvanceDate_Click(object sender, EventArgs e)
        {
            if (SwitchConnection.Connected)
            {
                ButtonReadRaids.Enabled = false;
                ButtonAdvanceDate.Enabled = false;
                _WindowState = WindowState;
                var prompt = false;
                do
                {
                    prev = Raids.Select(z => z.Seed).ToList();
                    await AdvanceDate(CancellationToken.None);
                    await ReadRaids(CancellationToken.None);
                } while (CheckAdvanceDate(out prompt));
                if (prompt)
                {
                    if (Settings.Default.CfgPlaySound) System.Media.SystemSounds.Asterisk.Play();
                    if (Settings.Default.CfgFocusWindow)
                    {
                        WindowState = _WindowState;
                        Activate();
                    }
                    for (int i = 0; i < Raids.Count; i++)
                    {
                        foreach (var filter in RaidFilters)
                        {
                            if (filter == null)
                                continue;
                            if (filter.FilterSatisfied(Encounters[i], Raids[i], RaidBoost.SelectedIndex))
                                NotificationHandler.SendNotifications(Encounters[i], Raids[i], filter);
                        }
                    }
                    if (Settings.Default.CfgEnableAlertWindow) MessageBox.Show(Settings.Default.CfgAlertWindowMessage, "Result found!", MessageBoxButtons.OK);
                }

                ButtonReadRaids.Enabled = true;
                ButtonAdvanceDate.Enabled = true;
            }
        }

        private bool CheckAdvanceDate(out bool prompt)
        {
            prompt = false;
            if (!CheckEnableFilters.Checked)
                return false;
            if (prev.Count != Raids.Count)
                return true;
            var sameraids = true;
            for (int i = 0; i < prev.Count; i++)
            {
                if (Raids[i].Seed != prev[i])
                    sameraids = false;
            }
            if (sameraids)
                return true;
            if (RaidFilters.Any(z => z.FilterSatisfied(Encounters, Raids, RaidBoost.SelectedIndex)))
                prompt = true;
            if (StopAdvances || prompt == true)
                return false;
            return true;
        }

        private async void ButtonReadRaids_Click(object sender, EventArgs e)
        {
            ButtonReadRaids.Enabled = false;
            ButtonAdvanceDate.Enabled = false;
            if (IsReading)
            {
                MessageBox.Show("Please wait for the current RAM read to finish.");
            }
            else
            {
                IsReading = true;
                await ReadRaids(CancellationToken.None);
                IsReading = false;
            }
            ButtonReadRaids.Enabled = true;
            ButtonAdvanceDate.Enabled = true;

        }

        private async Task ReadRaids(CancellationToken token)
        {
            ConnectionStatusText.Text = "Parsing pointer...";
            offset = await OffsetUtil.GetPointerAddress(RaidBlockPointer, CancellationToken.None);

            Raids.Clear();
            Encounters.Clear();
            RewardsList.Clear();
            index = 0;

            ConnectionStatusText.Text = "Reading raid block...";
            var Data = await SwitchConnection.ReadBytesAbsoluteAsync(offset + RaidBlock.HEADER_SIZE, (int)(RaidBlock.SIZE - RaidBlock.HEADER_SIZE), token);
            Raid raid;
            var count = Data.Length / Raid.SIZE;
            var eventct = 0;
            for (int i = 0; i < count; i++)
            {
                raid = new Raid(Data.Skip(i * Raid.SIZE).Take(Raid.SIZE).ToArray());
                var progress = raid.IsEvent ? EventProgress.SelectedIndex : Progress.SelectedIndex;
                var raid_delivery_group_id = raid.IsEvent ? TeraDistribution.GetDeliveryGroupID(eventct, Raid.DeliveryRaidPriority) : -1;
                var encounter = raid.Encounter(progress, raid_delivery_group_id);
                if (raid.IsValid)
                {
                    Raids.Add(raid);
                    Encounters.Add(encounter);
                    RewardsList.Add(Structures.Rewards.GetRewards(encounter, raid.Seed, raid.TeraType, RaidBoost.SelectedIndex));
                }
                if (raid.IsEvent)
                    eventct++;
            }

            ConnectionStatusText.Text = "Completed!";
            LabelLoadedRaids.Text = $"Raids: {Raids.Count} | Shiny: {Enumerable.Range(0, Raids.Count).Where(i => Raid.CheckIsShiny(Raids[i], Encounters[i])).Count()}";
            if (Raids.Count > 0)
            {
                ButtonPrevious.Enabled = true;
                ButtonNext.Enabled = true;
                ComboIndex.DataSource = Enumerable.Range(0, Raids.Count + 1).Select(z => $"{z + 1:D2} / {Raids.Count:D2}").ToArray();
                ComboIndex.SelectedIndex = index < Raids.Count ? index : 0;
            }
            else
            {
                ButtonPrevious.Enabled = false;
                ButtonNext.Enabled = false;
                if (Raids.Count > RaidBlock.MAX_COUNT || Raids.Count == 0) MessageBox.Show("Bad read, ensure there are no cheats running or anything else that might shift RAM (Edizon, overlays, etc.), then reboot your console and try again.");

            }
        }

        private void Progress_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.Progress = Progress.SelectedIndex;
            Settings.Default.Save();
            if (Raids.Count > 0) DisplayRaid(index);
        }

        private void EventProgress_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.EventProgress = EventProgress.SelectedIndex;
            Settings.Default.Save();
            if (Raids.Count > 0) DisplayRaid(index);
        }

        private async void ViewRAM_Click(object sender, EventArgs e)
        {
            if (SwitchConnection.Connected && ModifierKeys == Keys.Shift)
            {
                if (IsReading)
                {
                    MessageBox.Show("Please wait for the current RAM read to finish.");
                }
                else
                {
                    IsReading = true;
                    var Data = await SwitchConnection.ReadBytesAbsoluteAsync(offset, (int)RaidBlock.SIZE, CancellationToken.None);
                    RaidBlockViewer BlockViewerWindow = new(Data, offset);
                    BlockViewerWindow.ShowDialog();
                    IsReading = false;
                }
            }
            if (Raids[index] != null)
            {
                RaidBlockViewer BlockViewerWindow = new(Raids[index].Data, offset);
                BlockViewerWindow.ShowDialog();
            }
        }

        private void Game_SelectedIndexChanged(object sender, EventArgs e)
        {
            Raid.Game = Game.Text;
            Settings.Default.Game = Game.Text;
            Settings.Default.Save();
            if (Raids.Count > 0) DisplayRaid(index);
        }

        private void StopFilter_Click(object sender, EventArgs e)
        {
            var form = new FilterSettings(ref RaidFilters);
            form.ShowDialog();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.CfgLocation = Location;
            Settings.Default.Save();
            Disconnect();
        }

        private async void DownloadEvents_Click(object sender, EventArgs e)
        {
            if (!SwitchConnection.Connected)
                return;
            await ReadEventRaids(true);
        }

        private void Seed_Clicked(object sender, EventArgs e)
        {
            if (ModifierKeys != Keys.Shift)
                return;
            if (Raids.Count <= index)
                return;
            var raid = Raids[index];
            Seed.Text = HideSeed ? $"{raid.Seed:X8}" : "Hidden";
            EC.Text = HideSeed ? $"{raid.EC:X8}" : "Hidden";
            PID.Text = (HideSeed ? $"{raid.PID:X8}" : "Hidden") + $"{(raid.IsShiny ? " (☆)" : string.Empty)}";
            HideSeed = !HideSeed;
            ActiveControl = null;
        }

        private void ConfigSettings_Click(object sender, EventArgs e)
        {
            var form = new ConfigWindow();
            form.ShowDialog();
        }

        private void DisplayMap(object sender, EventArgs e)
        {
            if (Raids.Count == 0)
            {
                MessageBox.Show("Raids not loaded.");
                return;
            }
            var raid = Raids[index];
            var encounter = Encounters[index];
            var teratype = Raid.GetTeraType(encounter, raid);
            var map = GenerateMap(raid, teratype);
            if (map == null)
            {
                MessageBox.Show("Error generating map.");
                return;
            }
            var form = new MapView(map);
            form.ShowDialog();
        }

        private void Rewards_Click(object sender, EventArgs e)
        {
            if (Raids.Count == 0)
            {
                MessageBox.Show("Raids not loaded.");
                return;
            }

            var rewards = RewardsList[index];

            if (rewards == null)
            {
                MessageBox.Show("Error while displaying rewards.");
                return;
            }

            var form = new RewardsView(rewards);
            form.ShowDialog();
        }

        private void RaidBoost_SelectedIndexChanged(object sender, EventArgs e)
        {
            RewardsList.Clear();
            for (int i = 0; i < Raids.Count; i++)
            {
                var raid = Raids[i];
                var encounter = Encounters[i];
                RewardsList.Add(Structures.Rewards.GetRewards(encounter, raid.Seed, raid.TeraType, RaidBoost.SelectedIndex));
            }
        }

        Point Default = new(305, 245);
        Point ShowExtra = new(314, 245);
        private void Move_Clicked(object sender, EventArgs e)
        {
            if (Raids.Count == 0)
            {
                MessageBox.Show("Raids not loaded.");
                return;
            }
            var encounter = Encounters[index];
            if (encounter == null)
                return;
            ShowExtraMoves = !ShowExtraMoves;
            LabelMoves.Text = ShowExtraMoves ? "Extra:" : "Moves:";
            LabelMoves.Location = ShowExtraMoves ? ShowExtra : Default;
            var extra_moves = new ushort[] { 0, 0, 0, 0 };
            for (int i = 0; i < encounter.ExtraMoves.Length; i++)
                if (i < extra_moves.Count()) extra_moves[i] = encounter.ExtraMoves[i];
            Move1.Text = ShowExtraMoves ? Raid.strings.Move[extra_moves[0]] : Raid.strings.Move[encounter.Move1];
            Move2.Text = ShowExtraMoves ? Raid.strings.Move[extra_moves[1]] : Raid.strings.Move[encounter.Move2];
            Move3.Text = ShowExtraMoves ? Raid.strings.Move[extra_moves[2]] : Raid.strings.Move[encounter.Move3];
            Move4.Text = ShowExtraMoves ? Raid.strings.Move[extra_moves[3]] : Raid.strings.Move[encounter.Move4];
        }

        private void ComboIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            index = ComboIndex.SelectedIndex;
            DisplayRaid(index);
        }

        private void SendScreenshot_Click(object sender, EventArgs e)
        {
            if (!SwitchConnection.Connected)
            {
                MessageBox.Show("Cannot send a screenshot since switch is not connected.");
                return;
            }

            NotificationHandler.SendScreenshot(SwitchConnection);
        }
    }
}
