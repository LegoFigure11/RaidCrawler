using Newtonsoft.Json;
using PKHeX.Core;
using PKHeX.Drawing;
using PKHeX.Drawing.PokeSprite;
using RaidCrawler.Connection;
using RaidCrawler.Structures;
using RaidCrawler.Subforms;
using SysBot.Base;
using System.Data;
using System.Diagnostics;
using static RaidCrawler.Structures.Offsets;

namespace RaidCrawler
{
    public partial class MainWindow : Form
    {
        private static CancellationTokenSource Source = new();
        private static readonly object _connectLock = new();
        private ConnectionWrapperAsync ConnectionWrapper = default!;
        private readonly SwitchConnectionConfig ConnectionConfig = new()
        { Protocol = SwitchProtocol.WiFi, IP = "192.168.0.0", Port = 6000 };

        private readonly Config Config = new();

        private readonly List<Raid> Raids = new();
        private readonly List<ITeraRaid?> Encounters = new();
        private List<uint> prev = new();
        private List<RaidFilter> RaidFilters = new();
        private static readonly Image map = Image.FromStream(new MemoryStream(Utils.GetBinaryResource("paldea.png")));
        private static Dictionary<string, float[]>? den_locations;

        // rewards
        private readonly List<List<(int, int, int)>?> RewardsList = new();

        // statistics
        public int StatDaySkipTries = 0;
        public int StatDaySkipSuccess = 0;
        public string formTitle;

        private int index = 0;
        private ulong RaidBlockOffset = 0;
        private bool IsReading = false;
        private bool HideSeed = false;
        private bool ShowExtraMoves = false;

        private Color DefaultColor;
        private FormWindowState _WindowState;
        private readonly Stopwatch stopwatch = new();
        private readonly TeraRaidView teraRaidView;

        public MainWindow()
        {
            string build = string.Empty;
#if DEBUG
            var date = File.GetLastWriteTime(System.Reflection.Assembly.GetEntryAssembly()!.Location);
            build = $" (dev-{date:yyyyMMdd})";
#endif
            var v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version!;

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

            var configpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
            if (File.Exists(configpath))
                Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configpath)) ?? new Config();
            else
            {
                Config = new Config();
                SaveConfig();
            }

            formTitle = "RaidCrawler v" + v.Major + "." + v.Minor + "." + v.Build + build + " " + Config.InstanceName;
            Text = formTitle;

            // load rewards
            Raid.BaseFixedRewards = JsonConvert.DeserializeObject<List<RaidFixedRewards>>(Utils.GetStringResource("raid_fixed_reward_item_array.json") ?? "[]");
            Raid.BaseLotteryRewards = JsonConvert.DeserializeObject<List<RaidLotteryRewards>>(Utils.GetStringResource("raid_lottery_reward_item_array.json") ?? "[]");

            Raid.Game = Config.Game;
            SpriteBuilder.ShowTeraThicknessStripe = 0x4;
            SpriteBuilder.ShowTeraOpacityStripe = 0xAF;
            SpriteBuilder.ShowTeraOpacityBackground = 0xFF;
            SpriteUtil.ChangeMode(SpriteBuilderMode.SpritesArtwork5668);

            teraRaidView = new TeraRaidView(Config);

            var protocol = Config.Protocol;
            ConnectionConfig = new()
            {
                IP = Config.IP,
                Port = protocol is SwitchProtocol.WiFi ? 6000 : Config.UsbPort,
                Protocol = Config.Protocol,
            };

            InitializeComponent();
            if (Config.Protocol is SwitchProtocol.USB)
            {
                InputSwitchIP.Visible = false;
                LabelSwitchIP.Visible = false;
                USB_Port_TB.Visible = true;
                USB_Port_label.Visible = true;
            }
            else
            {
                InputSwitchIP.Visible = true;
                LabelSwitchIP.Visible = true;
                USB_Port_TB.Visible = false;
                USB_Port_label.Visible = false;
            }
        }

        private void UpdateStatus(string status)
        {
            ToolStripStatusLabel.Text = status;
        }

        private static void ButtonEnable(object[] obj, bool enable)
        {
            for (int b = 0; b < obj.Length; b++)
            {
                if (obj[b] is not Button btn)
                    continue;

                if (btn.InvokeRequired)
                    btn.Invoke(() => { btn.Enabled = enable; });
                else btn.Enabled = enable;
            }
        }

        private void ShowDialog(object obj)
        {
            var window = (Form)obj;
            if (window is null)
                return;

            window.StartPosition = FormStartPosition.CenterParent;
            if (InvokeRequired)
                Invoke(() => { window.ShowDialog(); });
            else window.ShowDialog();
        }

        private int GetRaidBoost()
        {
            if (InvokeRequired)
                return RaidBoost.Invoke(() => { return RaidBoost.SelectedIndex; });
            else return RaidBoost.SelectedIndex;
        }

        public int GetStatDaySkipTries() => StatDaySkipTries;
        public int GetStatDaySkipSuccess() => StatDaySkipSuccess;

        private void SaveConfig()
        {
            var configpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
            string output = JsonConvert.SerializeObject(Config);
            using StreamWriter sw = new(configpath);
            sw.Write(output);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Location = Config.Location;
            if (Location.X == 0 && Location.Y == 0)
                CenterToScreen();

            InputSwitchIP.Text = Config.IP;
            USB_Port_TB.Text = Config.UsbPort.ToString();
            DefaultColor = IVs.BackColor;
            RaidBoost.SelectedIndex = 0;

            if (Config.StreamerView)
            {
                teraRaidView.Map.Image = map;
                teraRaidView.Show();
            }
        }

        private void InputSwitchIP_Changed(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Config.IP = textBox.Text;
            ConnectionConfig.IP = textBox.Text;
            SaveConfig();
        }

        private void USB_Port_Changed(object sender, EventArgs e)
        {
            if (Config.Protocol is SwitchProtocol.WiFi)
                return;

            TextBox textBox = (TextBox)sender;
            if (int.TryParse(textBox.Text, out int port) && port >= 0)
            {
                Config.UsbPort = port;
                ConnectionConfig.Port = port;
                SaveConfig();
                return;
            }
            MessageBox.Show("Please enter a valid numerical USB port.");
        }

        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            lock (_connectLock)
            {
                ConnectionWrapper = new(ConnectionConfig, UpdateStatus);
                Connect(Source.Token);
            }
        }

        private void Connect(CancellationToken token)
        {
            Task.Run(async () =>
            {
                IsReading = true;
                ButtonEnable(new[] { ButtonConnect }, false);

                (bool success, string err) = await ConnectionWrapper.Connect(token).ConfigureAwait(false);
                if (!success)
                {
                    IsReading = false;
                    ButtonEnable(new[] { ButtonConnect }, true);
                    MessageBox.Show(err);
                    return;
                }

                ToolStripStatusLabel.Text = "Detecting game version...";
                string id = await ConnectionWrapper.Connection.GetTitleID(token).ConfigureAwait(false);
                Config.Game = id switch
                {
                    ScarletID => "Scarlet",
                    VioletID => "Violet",
                    _ => "",
                };

                if (id is "")
                {
                    (success, err) = await ConnectionWrapper.DisconnectAsync(token).ConfigureAwait(false);
                    if (!success)
                    {
                        IsReading = false;
                        MessageBox.Show(err);
                        return;
                    }

                    IsReading = false;
                    MessageBox.Show("Unable to detect Pokémon Scarlet or Pokémon Violet running on your Switch!");
                    return;
                }

                ToolStripStatusLabel.Text = "Reading story progress...";
                Config.Progress = await ConnectionWrapper.GetStoryProgress(token).ConfigureAwait(false);
                Config.EventProgress = Math.Min(Config.Progress, 3);

                ToolStripStatusLabel.Text = "Reading raids...";
                await ReadRaids(token).ConfigureAwait(false);

                ToolStripStatusLabel.Text = "Reading event raid status...";
                try
                {
                    await ReadEventRaids(token).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error occurred while reading event raids: {ex.Message}");
                }

                ToolStripStatusLabel.Text = "Completed!";
                IsReading = false;
                ButtonEnable(new[] { ButtonAdvanceDate, ButtonReadRaids, ButtonDisconnect, ButtonViewRAM, ButtonDownloadEvents }, true);
            }, token);
        }

        private void Disconnect_Click(object sender, EventArgs e)
        {
            lock (_connectLock)
                Disconnect(Source.Token);
        }

        private void Disconnect(CancellationToken token)
        {
            Task.Run(async () =>
            {
                IsReading = true;
                (bool success, string err) = await ConnectionWrapper.DisconnectAsync(token).ConfigureAwait(false);
                if (!success)
                    MessageBox.Show(err);

                stopwatch.Stop();
                IsReading = false;
                ButtonEnable(new[] { ButtonConnect }, true);
                ButtonEnable(new[] { ButtonAdvanceDate, ButtonReadRaids, ButtonDisconnect, ButtonViewRAM, ButtonDownloadEvents }, false);
            }, token);
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

        private void ButtonAdvanceDate_Click(object sender, EventArgs e)
        {
            AdvanceDateClick(Source.Token);
        }

        private void AdvanceDateClick(CancellationToken token)
        {
            Task.Run(async () =>
            {
                if (ConnectionWrapper.Connected)
                {
                    SearchTimer.Start();
                    stopwatch.Reset();
                    stopwatch.Start();

                    ButtonEnable(new[] { ButtonReadRaids, ButtonAdvanceDate }, false);
                    _WindowState = WindowState;

                    var prompt = false;
                    do
                    {
                        prev = Raids.Select(z => z.Seed).ToList();
                        if (Config.StreamerView)
                            teraRaidView.StartProgress();

                        ToolStripStatusLabel.Text = "Changing date...";
                        await ConnectionWrapper.AdvanceDate(Config, token).ConfigureAwait(false);
                        await ReadRaids(token).ConfigureAwait(false);
                    } while (CheckAdvanceDate(out prompt));

                    if (prompt)
                    {
                        stopwatch.Stop();
                        var timeSpan = stopwatch.Elapsed;
                        string time = string.Format("{0:00}:{1:00}:{2:00}",
                        timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

                        if (Config.PlaySound)
                            System.Media.SystemSounds.Asterisk.Play();

                        if (Config.FocusWindow)
                        {
                            WindowState = _WindowState;
                            Activate();
                        }

                        for (int i = 0; i < Raids.Count; i++)
                        {
                            var satisfied_filters = new List<RaidFilter>();
                            foreach (var filter in RaidFilters)
                            {
                                if (filter is null)
                                    continue;

                                if (filter.FilterSatisfied(Encounters[i], Raids[i], RaidBoost.SelectedIndex))
                                {
                                    satisfied_filters.Add(filter);
                                    if (ComboIndex.InvokeRequired)
                                        ComboIndex.Invoke(() => { ComboIndex.SelectedIndex = i; });
                                    else ComboIndex.SelectedIndex = i;
                                }
                            }

                            if (satisfied_filters.Count > 0)
                                NotificationHandler.SendNotifications(Config, Encounters[i], Raids[i], satisfied_filters, time, RewardsList[i]);
                        }

                        if (Config.EnableAlertWindow)
                            MessageBox.Show(Config.AlertWindowMessage + "\n\nTime Spent: " + time, "Result found!", MessageBoxButtons.OK);
                        Text = formTitle + " [Match Found in " + time + "]";
                    }

                    ButtonEnable(new[] { ButtonReadRaids, ButtonAdvanceDate }, true);
                    SearchTimer.Stop();
                }
            }, token);
        }

        private void ButtonReadRaids_Click(object sender, EventArgs e)
        {
            if (IsReading)
                MessageBox.Show("Please wait for the current RAM read to finish.");

            Task.Run(async () =>
            {
                IsReading = true;
                ButtonEnable(new[] { ButtonReadRaids, ButtonAdvanceDate }, false);
                await ReadRaids(Source.Token).ConfigureAwait(false);

                IsReading = false;
                ButtonEnable(new[] { ButtonReadRaids, ButtonAdvanceDate }, true);
            }, Source.Token);
        }

        private void ViewRAM_Click(object sender, EventArgs e)
        {
            if (IsReading)
                MessageBox.Show("Please wait for the current RAM read to finish.");

            RaidBlockViewer window = default!;
            if (ConnectionWrapper.Connected && ModifierKeys == Keys.Shift)
            {
                IsReading = true;
                var data = ConnectionWrapper.Connection.ReadBytesAbsoluteAsync(RaidBlockOffset, (int)RaidBlock.SIZE, Source.Token).Result;
                window = new(data, RaidBlockOffset);
                IsReading = false;
            }
            else if (Raids[index] is not null)
                window = new(Raids[index].Data, RaidBlockOffset);
            ShowDialog(window);
        }

        private void StopFilter_Click(object sender, EventArgs e)
        {
            var form = new FilterSettings(ref RaidFilters);
            form.ShowDialog();
        }

        private void DownloadEvents_Click(object sender, EventArgs e)
        {
            if (!ConnectionWrapper.Connected)
                return;

            if (IsReading)
                MessageBox.Show("Please wait for the current RAM read to finish.");

            Task.Run(async () =>
            {
                IsReading = true;
                ToolStripStatusLabel.Text = "Reading event raid status...";

                await ReadEventRaids(Source.Token, true).ConfigureAwait(false);

                IsReading = false;
                ToolStripStatusLabel.Text = "Completed!";
            }, Source.Token);
        }

        private void Seed_Click(object sender, EventArgs e)
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
            var form = new ConfigWindow(Config);
            form.ShowDialog();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Config.Location = Location;
            SaveConfig();
            Disconnect(Source.Token);
            Source.Cancel();
            Source = new();
        }

        private async Task ReadEventRaids(CancellationToken token, bool force = false)
        {
            var prio_file = Path.Combine(Directory.GetCurrentDirectory(), "cache", "raid_priority_array");
            if (File.Exists(prio_file))
            {
                (_, var version) = FlatbufferDumper.DumpDeliveryPriorities(File.ReadAllBytes(prio_file));
                var blk = await ConnectionWrapper.ReadBlockDefault(BCATRaidPriorityLocation, "raid_priority_array.tmp", true, token).ConfigureAwait(false);
                (_, var v2) = FlatbufferDumper.DumpDeliveryPriorities(blk);
                if (version != v2)
                    force = true;

                var tmp_file = Path.Combine(Directory.GetCurrentDirectory(), "cache", "raid_priority_array.tmp");
                if (File.Exists(tmp_file))
                    File.Delete(tmp_file);

                if (v2 == 0) // raid reset
                    return;
            }

            var delivery_raid_prio = await ConnectionWrapper.ReadBlockDefault(BCATRaidPriorityLocation, "raid_priority_array", force, token).ConfigureAwait(false);
            (Raid.DeliveryRaidPriority, var priority) = FlatbufferDumper.DumpDeliveryPriorities(delivery_raid_prio);
            if (priority == 0)
                return;

            var delivery_raid_fbs = await ConnectionWrapper.ReadBlockDefault(BCATRaidBinaryLocation, "raid_enemy_array", force, token).ConfigureAwait(false);
            var delivery_fixed_rewards = await ConnectionWrapper.ReadBlockDefault(BCATRaidFixedRewardLocation, "fixed_reward_item_array", force, token).ConfigureAwait(false);
            var delivery_lottery_rewards = await ConnectionWrapper.ReadBlockDefault(BCATRaidLotteryRewardLocation, "lottery_reward_item_array", force, token).ConfigureAwait(false);

            Raid.DistTeraRaids = TeraDistribution.GetAllEncounters(delivery_raid_fbs);
            Raid.DeliveryRaidFixedRewards = FlatbufferDumper.DumpFixedRewards(delivery_fixed_rewards);
            Raid.DeliveryRaidLotteryRewards = FlatbufferDumper.DumpLotteryRewards(delivery_lottery_rewards);
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
                labelEvent.Visible = raid.IsEvent;

                var teratype = Raid.GetTeraType(encounter, raid);
                TeraType.Text = Raid.strings.types[teratype];
                int StarCount = encounter is TeraDistribution ? encounter.Stars : Raid.GetStarCount(raid.Difficulty, Config.Progress, raid.IsBlack);
                Difficulty.Text = string.Concat(Enumerable.Repeat("☆", StarCount));

                if (encounter is not null)
                {
                    var param = Raid.GetParam(encounter);
                    var blank = new PK9
                    {
                        Species = encounter.Species,
                        Form = encounter.Form
                    };

                    Encounter9RNG.GenerateData(blank, param, EncounterCriteria.Unrestricted, raid.Seed);
                    var img = blank.Sprite();
                    img = ApplyTeraColor((byte)teratype, img, SpriteBackgroundType.BottomStripe);
                    Species.Text = $"{Raid.strings.Species[encounter.Species]}{(encounter.Form != 0 ? $"-{encounter.Form}" : "")}";
                    Sprite.Image = img;
                    GemIcon.Image = GetDisplayGemImage(teratype, raid);
                    Gender.Text = $"{(Gender)blank.Gender}";
                    var nature = blank.Nature;
                    Nature.Text = $"{Raid.strings.Natures[nature]}";
                    Ability.Text = $"{Raid.strings.Ability[blank.Ability]}";
                    var extra_moves = new ushort[] { 0, 0, 0, 0 };
                    for (int i = 0; i < encounter.ExtraMoves.Length; i++)
                        if (i < extra_moves.Length) extra_moves[i] = encounter.ExtraMoves[i];
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
            else MessageBox.Show($"Unable to display raid at index {index}. Ensure there are no cheats running or anything else that might shift RAM (Edizon, overlays, etc.), then reboot your console and try again.");
        }

        private static Image? GetDisplayGemImage(int teratype, Raid raid)
        {
            var display_black = raid.IsBlack || raid.Flags == 3;
            var baseImg = display_black ? (Image?)Properties.Resources.ResourceManager.GetObject($"black_{teratype:D2}")
                                        : (Image?)Properties.Resources.ResourceManager.GetObject($"gem_{teratype:D2}");
            if (baseImg is null)
                return null;

            var backlayer = new Bitmap(baseImg.Width + 10, baseImg.Height + 10, baseImg.PixelFormat);
            baseImg = ImageUtil.LayerImage(backlayer, baseImg, 5, 5);
            var pixels = ImageUtil.GetPixelData((Bitmap)baseImg);
            for (int i = 0; i < pixels.Length; i += 4)
            {
                if (pixels[i + 3] == 0)
                {
                    pixels[i] = 0;
                    pixels[i + 1] = 0;
                    pixels[i + 2] = 0;
                }
            }

            baseImg = ImageUtil.GetBitmap(pixels, baseImg.Width, baseImg.Height, baseImg.PixelFormat);
            if (display_black)
            {
                var color = Color.Indigo;
                SpriteUtil.GetSpriteGlow(baseImg, color.B, color.G, color.R, out var glow, false);
                baseImg = ImageUtil.LayerImage(ImageUtil.GetBitmap(glow, baseImg.Width, baseImg.Height, baseImg.PixelFormat), baseImg, 0, 0);
            }
            else if (raid.IsEvent)
            {
                var color = Color.DarkTurquoise;
                SpriteUtil.GetSpriteGlow(baseImg, color.B, color.G, color.R, out var glow, false);
                baseImg = ImageUtil.LayerImage(ImageUtil.GetBitmap(glow, baseImg.Width, baseImg.Height, baseImg.PixelFormat), baseImg, 0, 0);
            }
            return baseImg;
        }

        private void DisplayPrettyRaid(int index)
        {
            if (Raids.Count > index)
            {
                Raid raid = Raids[index];
                var encounter = Encounters[index];

                teraRaidView.Area.Text = $"{Areas.Area[raid.Area - 1]} - Den {raid.Den}";

                var teratype = Raid.GetTeraType(encounter, raid);
                teraRaidView.TeraType.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("gem_text_" + teratype)!;

                int StarCount = encounter is TeraDistribution ? encounter.Stars : Raid.GetStarCount(raid.Difficulty, Config.Progress, raid.IsBlack);
                teraRaidView.Difficulty.Text = string.Concat(Enumerable.Repeat("⭐", StarCount));

                if (encounter != null)
                {
                    var param = Raid.GetParam(encounter);
                    var blank = new PK9
                    {
                        Species = encounter.Species,
                        Form = encounter.Form
                    };
                    Encounter9RNG.GenerateData(blank, param, EncounterCriteria.Unrestricted, raid.Seed);
                    var img = blank.Sprite();

                    teraRaidView.picBoxPokemon.Image = img;

                    teraRaidView.Species.Text = Raid.strings.Species[encounter.Species];
                    //teraRaidView.Form.Text = encounter.Form != 0 ? $"{encounter.Form}" : " ";

                    teraRaidView.Gender.Text = $"{(Gender)blank.Gender}";

                    var nature = blank.Nature;
                    teraRaidView.Nature.Text = $"{Raid.strings.Natures[nature]}";
                    teraRaidView.Ability.Text = $"{Raid.strings.Ability[blank.Ability]}";

                    teraRaidView.Moveset1.Text = Raid.strings.Move[encounter.Move1];
                    teraRaidView.Moveset2.Text = Raid.strings.Move[encounter.Move2];
                    teraRaidView.Moveset3.Text = Raid.strings.Move[encounter.Move3];
                    teraRaidView.Moveset4.Text = Raid.strings.Move[encounter.Move4];

                    var extra_moves = new ushort[] { 0, 0, 0, 0 };
                    for (int i = 0; i < encounter.ExtraMoves.Length; i++)
                        if (i < extra_moves.Length) extra_moves[i] = encounter.ExtraMoves[i];
                    teraRaidView.Moveset5.Text = (encounter.ExtraMoves.Length > 0) ? Raid.strings.Move[extra_moves[0]] : "---";
                    teraRaidView.Moveset6.Text = (encounter.ExtraMoves.Length > 1) ? Raid.strings.Move[extra_moves[1]] : "---";
                    teraRaidView.Moveset7.Text = (encounter.ExtraMoves.Length > 2) ? Raid.strings.Move[extra_moves[2]] : "---";
                    teraRaidView.Moveset8.Text = (encounter.ExtraMoves.Length > 3) ? Raid.strings.Move[extra_moves[3]] : "---";


                    var ivs = ToSpeedLast(blank.IVs);

                    // HP
                    teraRaidView.HP.Text = $"{ivs[0]:D2}";
                    teraRaidView.HP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(25)))));
                    if (teraRaidView.HP.Text is "31")
                    {
                        teraRaidView.HP.BackColor = Color.ForestGreen;
                    }
                    else if (teraRaidView.HP.Text is "00")
                    {
                        teraRaidView.HP.BackColor = Color.DarkRed;
                    }

                    // ATK
                    teraRaidView.ATK.Text = $"{ivs[1]:D2}";
                    teraRaidView.ATK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(25)))));
                    if (teraRaidView.ATK.Text is "31")
                    {
                        teraRaidView.ATK.BackColor = Color.ForestGreen;
                    }
                    else if (teraRaidView.ATK.Text is "00")
                    {
                        teraRaidView.ATK.BackColor = Color.DarkRed;
                    }

                    // DEF
                    teraRaidView.DEF.Text = $"{ivs[2]:D2}";
                    teraRaidView.DEF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(25)))));
                    if (teraRaidView.DEF.Text is "31")
                    {
                        teraRaidView.DEF.BackColor = Color.ForestGreen;
                    }
                    else if (teraRaidView.DEF.Text is "00")
                    {
                        teraRaidView.DEF.BackColor = Color.DarkRed;
                    }

                    // SPA
                    teraRaidView.SPA.Text = $"{ivs[3]:D2}";
                    teraRaidView.SPA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(25)))));
                    if (teraRaidView.SPA.Text is "31")
                    {
                        teraRaidView.SPA.BackColor = Color.ForestGreen;
                    }
                    else if (teraRaidView.SPA.Text is "00")
                    {
                        teraRaidView.SPA.BackColor = Color.DarkRed;
                    }

                    // SPD
                    teraRaidView.SPD.Text = $"{ivs[4]:D2}";
                    teraRaidView.SPD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(25)))));
                    if (teraRaidView.SPD.Text is "31")
                    {
                        teraRaidView.SPD.BackColor = Color.ForestGreen;
                    }
                    else if (teraRaidView.SPD.Text is "00")
                    {
                        teraRaidView.SPD.BackColor = Color.DarkRed;
                    }

                    // SPEED
                    teraRaidView.SPEED.Text = $"{ivs[5]:D2}";
                    teraRaidView.SPEED.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(25)))));
                    if (teraRaidView.SPEED.Text is "31")
                    {
                        teraRaidView.SPEED.BackColor = Color.ForestGreen;
                    }
                    else if (teraRaidView.SPEED.Text is "00")
                    {
                        teraRaidView.SPEED.BackColor = Color.DarkRed;
                    }


                    var map = GenerateMap(raid, teratype);
                    if (map == null)
                    {
                        MessageBox.Show("Error generating map.");
                    }
                    teraRaidView.Map.Image = map;

                    // Rewards
                    var rewards = RewardsList[index];

                    teraRaidView.textAbilityPatch.Text = "0";
                    teraRaidView.textAbilityPatch.ForeColor = Color.DimGray;
                    teraRaidView.labelAbilityPatch.ForeColor = Color.DimGray;

                    teraRaidView.textAbilityCapsule.Text = "0";
                    teraRaidView.textAbilityCapsule.ForeColor = Color.DimGray;
                    teraRaidView.labelAbilityCapsule.ForeColor = Color.DimGray;

                    teraRaidView.textBottleCap.Text = "0";
                    teraRaidView.textBottleCap.ForeColor = Color.DimGray;
                    teraRaidView.labelBottleCap.ForeColor = Color.DimGray;

                    teraRaidView.textSweetHerba.Text = "0";
                    teraRaidView.textSweetHerba.ForeColor = Color.DimGray;
                    teraRaidView.labelSweetHerba.ForeColor = Color.DimGray;

                    teraRaidView.textSaltyHerba.Text = "0";
                    teraRaidView.textSaltyHerba.ForeColor = Color.DimGray;
                    teraRaidView.labelSaltyHerba.ForeColor = Color.DimGray;

                    teraRaidView.textBitterHerba.Text = "0";
                    teraRaidView.textBitterHerba.ForeColor = Color.DimGray;
                    teraRaidView.labelBitterHerba.ForeColor = Color.DimGray;

                    teraRaidView.textSourHerba.Text = "0";
                    teraRaidView.textSourHerba.ForeColor = Color.DimGray;
                    teraRaidView.labelSourHerba.ForeColor = Color.DimGray;

                    teraRaidView.textSpicyHerba.Text = "0";
                    teraRaidView.textSpicyHerba.ForeColor = Color.DimGray;
                    teraRaidView.labelSpicyHerba.ForeColor = Color.DimGray;

                    for (int i = 0; i < rewards!.Count; i++)
                    {
                        if (rewards[i].Item1 == 645)
                        {
                            teraRaidView.textAbilityCapsule.Text = (int.Parse(teraRaidView.textAbilityCapsule.Text) + 1).ToString();
                            teraRaidView.textAbilityCapsule.ForeColor = Color.White;
                            teraRaidView.labelAbilityCapsule.ForeColor = Color.WhiteSmoke;
                        }
                        if (rewards[i].Item1 == 795)
                        {
                            teraRaidView.textBottleCap.Text = (int.Parse(teraRaidView.textBottleCap.Text) + 1).ToString();
                            teraRaidView.textBottleCap.ForeColor = Color.White;
                            teraRaidView.labelBottleCap.ForeColor = Color.WhiteSmoke;
                        }
                        if (rewards[i].Item1 == 1606)
                        {
                            teraRaidView.textAbilityPatch.Text = (int.Parse(teraRaidView.textAbilityPatch.Text) + 1).ToString();
                            teraRaidView.textAbilityPatch.ForeColor = Color.White;
                            teraRaidView.labelAbilityPatch.ForeColor = Color.WhiteSmoke;
                        }
                        if (rewards[i].Item1 == 1904)
                        {
                            teraRaidView.textSweetHerba.Text = (int.Parse(teraRaidView.textSweetHerba.Text) + 1).ToString();
                            teraRaidView.textSweetHerba.ForeColor = Color.White;
                            teraRaidView.labelSweetHerba.ForeColor = Color.WhiteSmoke;
                        }
                        if (rewards[i].Item1 == 1905)
                        {
                            teraRaidView.textSaltyHerba.Text = (int.Parse(teraRaidView.textSaltyHerba.Text) + 1).ToString();
                            teraRaidView.textSaltyHerba.ForeColor = Color.White;
                            teraRaidView.labelSaltyHerba.ForeColor = Color.WhiteSmoke;
                        }
                        if (rewards[i].Item1 == 1906)
                        {
                            teraRaidView.textSourHerba.Text = (int.Parse(teraRaidView.textSourHerba.Text) + 1).ToString();
                            teraRaidView.textSourHerba.ForeColor = Color.White;
                            teraRaidView.labelSourHerba.ForeColor = Color.WhiteSmoke;
                        }
                        if (rewards[i].Item1 == 1907)
                        {
                            teraRaidView.textBitterHerba.Text = (int.Parse(teraRaidView.textBitterHerba.Text) + 1).ToString();
                            teraRaidView.textBitterHerba.ForeColor = Color.White;
                            teraRaidView.labelBitterHerba.ForeColor = Color.WhiteSmoke;
                        }
                        if (rewards[i].Item1 == 1908)
                        {
                            teraRaidView.textSpicyHerba.Text = (int.Parse(teraRaidView.textSpicyHerba.Text) + 1).ToString();
                            teraRaidView.textSpicyHerba.ForeColor = Color.White;
                            teraRaidView.labelSpicyHerba.ForeColor = Color.WhiteSmoke;
                        }
                    }
                    teraRaidView.Shiny.Visible = Raid.CheckIsShiny(raid, encounter);
                    teraRaidView.picShinyAlert.Enabled = Raid.CheckIsShiny(raid, encounter);
                }
                else
                {
                    // TODO Clear all the fields.
                }
            }
            else MessageBox.Show($"Unable to display raid at index {index}. Ensure there are no cheats running or anything else that might shift RAM (Edizon, overlays, etc.), then reboot your console and try again.");
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
                return ImageUtil.BlendTransparentTo(img, color, opacBack);
            return img;
        }

        private static Image? GenerateMap(Raid raid, int teratype)
        {
            var original = PKHeX.Drawing.Misc.TypeSpriteUtil.GetTypeSpriteGem((byte)teratype);
            if (original is null)
                return null;

            var gem = (Image)new Bitmap(original, new Size(30, 30));
            SpriteUtil.GetSpriteGlow(gem, 0xFF, 0xFF, 0xFF, out var glow, true);
            gem = ImageUtil.LayerImage(gem, ImageUtil.GetBitmap(glow, gem.Width, gem.Height, gem.PixelFormat), 0, 0);
            if (den_locations is null || den_locations.Count == 0)
                return null;

            try
            {
                var x = (den_locations[$"{raid.Area}-{raid.Den}"][0] + 2.072021484) * 512 / 5000;
                var y = (den_locations[$"{raid.Area}-{raid.Den}"][2] + 5255.240018) * 512 / 5000;
                var mapimg = ImageUtil.LayerImage(map, gem, (int)x, (int)y);
                if (den_locations.TryGetValue($"{raid.Area}-{raid.Den}_", out float[]? value))
                {
                    x = (value[0] + 2.072021484) * 512 / 5000;
                    x = (den_locations[$"{raid.Area}-{raid.Den}_"][0] + 2.072021484) * 512 / 5000;
                    mapimg = ImageUtil.LayerImage(mapimg, gem, (int)x, (int)y);
                }
                return mapimg;
            }
            catch { return null; }
        }

        private bool StopAdvances => RaidFilters.Count == 0 || RaidFilters.All(x => x.Enabled == false);

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

            StatDaySkipTries++;
            if (!sameraids)
                StatDaySkipSuccess++;

            if (sameraids)
                return true;
            if (RaidFilters.Any(z => z.FilterSatisfied(Encounters, Raids, RaidBoost.SelectedIndex)))
                prompt = true;
            if (StopAdvances || prompt == true)
                return false;
            return true;
        }

        private async Task ReadRaids(CancellationToken token)
        {
            Raid raid;
            ToolStripStatusLabel.Text = "Parsing pointer...";
            if (RaidBlockOffset == 0)
                RaidBlockOffset = await ConnectionWrapper.Connection.PointerAll(RaidBlockPointer, token).ConfigureAwait(false);

            Raids.Clear();
            Encounters.Clear();
            RewardsList.Clear();
            index = 0;

            ToolStripStatusLabel.Text = "Reading raid block...";
            var Data = await ConnectionWrapper.Connection.ReadBytesAbsoluteAsync(RaidBlockOffset + RaidBlock.HEADER_SIZE, (int)(RaidBlock.SIZE - RaidBlock.HEADER_SIZE), token).ConfigureAwait(false);

            var count = Data.Length / Raid.SIZE;
            HashSet<int> possible_groups = new();
            if (Raid.DistTeraRaids != null)
            {
                foreach (TeraDistribution e in Raid.DistTeraRaids.Cast<TeraDistribution>())
                {
                    if (TeraDistribution.AvailableInGame(e.Entity, Config.Game))
                        possible_groups.Add(e.DeliveryGroupID);
                }
            }

            var eventct = 0;
            for (int i = 0; i < count; i++)
            {
                raid = new Raid(Data.Skip(i * Raid.SIZE).Take(Raid.SIZE).ToArray());
                var progress = raid.IsEvent ? Config.EventProgress : Config.Progress;
                var raid_delivery_group_id = raid.IsEvent ? TeraDistribution.GetDeliveryGroupID(eventct, Raid.DeliveryRaidPriority, possible_groups) : -1;
                var encounter = raid.Encounter(progress, raid_delivery_group_id);
                if (raid.IsValid)
                {
                    Raids.Add(raid);
                    Encounters.Add(encounter);
                    RewardsList.Add(Structures.Rewards.GetRewards(encounter, raid.Seed, Raid.GetTeraType(encounter, raid), RaidBoost.SelectedIndex));
                }

                if (raid.IsEvent)
                    eventct++;
            }

            ToolStripStatusLabel.Text = "Completed!";
            var filterMatchCount = Enumerable.Range(0, Raids.Count).Count(i => RaidFilters.Any(z => z.FilterSatisfied(Encounters[i], Raids[i], GetRaidBoost())));
            LabelLoadedRaids.Text = $"Matches: {filterMatchCount}";
            if (Raids.Count > 0)
            {
                ButtonEnable(new[] { ButtonPrevious, ButtonNext }, true);
                var dataSource = Enumerable.Range(0, Raids.Count + 1).Select(z => $"{z + 1:D2} / {Raids.Count:D2}").ToArray();
                if (InvokeRequired)
                    ComboIndex.Invoke(() => { ComboIndex.DataSource = dataSource; });
                else ComboIndex.DataSource = dataSource;

                var selectedIndex = index < Raids.Count ? index : 0;
                if (InvokeRequired)
                    ComboIndex.Invoke(() => { ComboIndex.SelectedIndex = selectedIndex; });
                else ComboIndex.SelectedIndex = selectedIndex;
            }
            else
            {
                ButtonEnable(new[] { ButtonPrevious, ButtonNext }, false);
                if (Raids.Count > RaidBlock.MAX_COUNT || Raids.Count == 0)
                    MessageBox.Show("Bad read, ensure there are no cheats running or anything else that might shift RAM (Edizon, overlays, etc.), then reboot your console and try again.");
            }
        }

        public void Game_SelectedIndexChanged()
        {
            Raid.Game = Config.Game;
            Config.Game = Config.Game;
            if (Raids.Count > 0)
                DisplayRaid(index);
        }

        public void Protocol_SelectedIndexChanged(SwitchProtocol protocol)
        {
            Config.Protocol = protocol;
            ConnectionConfig.Protocol = protocol;
            if (protocol is SwitchProtocol.USB)
            {
                InputSwitchIP.Visible = false;
                LabelSwitchIP.Visible = false;
                USB_Port_label.Visible = true;
                USB_Port_TB.Visible = true;
            }
            else
            {
                InputSwitchIP.Visible = true;
                LabelSwitchIP.Visible = true;
                USB_Port_label.Visible = false;
                USB_Port_TB.Visible = false;
            }
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
                RewardsList.Add(Structures.Rewards.GetRewards(encounter, raid.Seed, Raid.GetTeraType(encounter, raid), RaidBoost.SelectedIndex));
            }
        }

        Point Default = new(244, 280);
        Point ShowExtra = new(253, 280);
        private void Move_Clicked(object sender, EventArgs e)
        {
            if (Raids.Count == 0)
            {
                MessageBox.Show("Raids not loaded.");
                return;
            }

            var encounter = Encounters[index];
            if (encounter is null)
                return;

            ShowExtraMoves = !ShowExtraMoves;
            LabelMoves.Text = ShowExtraMoves ? "Extra:" : "Moves:";
            LabelMoves.Location = ShowExtraMoves ? ShowExtra : Default;
            var extra_moves = new ushort[] { 0, 0, 0, 0 };
            for (int i = 0; i < encounter.ExtraMoves.Length; i++)
                extra_moves[i] = encounter.ExtraMoves[i];

            Move1.Text = ShowExtraMoves ? Raid.strings.Move[extra_moves[0]] : Raid.strings.Move[encounter.Move1];
            Move2.Text = ShowExtraMoves ? Raid.strings.Move[extra_moves[1]] : Raid.strings.Move[encounter.Move2];
            Move3.Text = ShowExtraMoves ? Raid.strings.Move[extra_moves[2]] : Raid.strings.Move[encounter.Move3];
            Move4.Text = ShowExtraMoves ? Raid.strings.Move[extra_moves[3]] : Raid.strings.Move[encounter.Move4];
        }

        private void ComboIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            index = ComboIndex.SelectedIndex;
            DisplayRaid(index);

            if (Config.StreamerView)
                DisplayPrettyRaid(index);
        }

        private void SendScreenshot_Click(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                try
                {
                    await NotificationHandler.SendScreenshot(Config, ConnectionWrapper.Connection, Source.Token).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot send a screenshot since switch is not connected.");
                }
            }, Source.Token);
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            var timeSpan = stopwatch.Elapsed;
            string time = string.Format("{0:00}:{1:00}:{2:00}",
            timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            Text = formTitle + " [Searching for " + time + "]";
            if (Config.StreamerView)
            {
                teraRaidView.textSearchTime.Text = time;
            }
        }

        public void TestWebhook()
        {
            var filter = new RaidFilter { Name = "Test Webhook" };
            var satisfied_filters = new List<RaidFilter> { filter };

            int i = ComboIndex.SelectedIndex;
            if (i > -1 && Encounters[i] != null && Raids[i] != null)
            {
                var timeSpan = stopwatch.Elapsed;
                string time = string.Format("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                NotificationHandler.SendNotifications(Config, Encounters[i], Raids[i], satisfied_filters, time, RewardsList[i]);
            }
            else
            {
                MessageBox.Show("Please connect to your device and ensure a raid has been found.");
            }
        }
    }
}
