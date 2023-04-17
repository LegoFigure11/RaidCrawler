using PKHeX.Core;
using PKHeX.Drawing;
using PKHeX.Drawing.PokeSprite;
using RaidCrawler.Core.Connection;
using RaidCrawler.Core.Discord;
using RaidCrawler.Core.Structures;
using RaidCrawler.WinForms.SubForms;
using SysBot.Base;
using System.Data;
using System.Diagnostics;
using System.Text.Json;
using static RaidCrawler.Core.Structures.Offsets;

namespace RaidCrawler.WinForms
{
    public partial class MainWindow : Form
    {
        private static CancellationTokenSource Source = new();
        private static CancellationTokenSource DateAdvanceSource = new();

        private static readonly object _connectLock = new();
        private static readonly object _readLock = new();

        private readonly ClientConfig Config = new();
        private ConnectionWrapperAsync ConnectionWrapper = default!;
        private readonly SwitchConnectionConfig ConnectionConfig = new()
        { Protocol = SwitchProtocol.WiFi, IP = "192.168.0.0", Port = 6000 };

        private readonly Raid RaidContainer;

        private List<RaidFilter> RaidFilters = new();
        private static readonly Image map = Image.FromStream(new MemoryStream(Utils.GetBinaryResource("paldea.png")));
        private static Dictionary<string, float[]>? den_locations;

        // statistics
        public int StatDaySkipTries = 0;
        public int StatDaySkipSuccess = 0;
        public string formTitle;

        private ulong RaidBlockOffset = 0;
        private bool IsReading = false;
        private bool HideSeed = false;
        private bool ShowExtraMoves = false;

        private Color DefaultColor;
        private FormWindowState _WindowState;
        private readonly Stopwatch stopwatch = new();
        private TeraRaidView? teraRaidView;

        private bool StopAdvances => !Config.EnableFilters || RaidFilters.Count == 0 || RaidFilters.All(x => !x.Enabled);

        public MainWindow()
        {
            string build = string.Empty;
#if DEBUG
            var date = File.GetLastWriteTime(AppContext.BaseDirectory);
            build = $" (dev-{date:yyyyMMdd})";
#endif
            var v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version!;
            var filterpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "filters.json");
            if (File.Exists(filterpath))
                RaidFilters = JsonSerializer.Deserialize<List<RaidFilter>>(File.ReadAllText(filterpath)) ?? new List<RaidFilter>();
            den_locations = JsonSerializer.Deserialize<Dictionary<string, float[]>>(Utils.GetStringResource("den_locations.json") ?? "{}");

            var configpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
            if (File.Exists(configpath))
            {
                var text = File.ReadAllText(configpath);
                Config = JsonSerializer.Deserialize<ClientConfig>(text)!;
            }
            else Config = new();

            formTitle = "RaidCrawler v" + v.Major + "." + v.Minor + "." + v.Build + build + " " + Config.InstanceName;
            Text = formTitle;

            // load raids
            RaidContainer = new(Config.Game);

            SpriteBuilder.ShowTeraThicknessStripe = 0x4;
            SpriteBuilder.ShowTeraOpacityStripe = 0xAF;
            SpriteBuilder.ShowTeraOpacityBackground = 0xFF;
            SpriteUtil.ChangeMode(SpriteBuilderMode.SpritesArtwork5668);

            var protocol = Config.Protocol;
            ConnectionConfig = new()
            {
                IP = Config.IP,
                Port = protocol is SwitchProtocol.WiFi ? 6000 : Config.UsbPort,
                Protocol = Config.Protocol,
            };

            InitializeComponent();

            btnOpenMap.Enabled = false;
            Rewards.Enabled = false;
            SendScreenshot.Enabled = false;
            CheckEnableFilters.Checked = Config.EnableFilters;

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

        private void ButtonEnable(object[] obj, bool enable)
        {
            lock (_readLock)
            {
                for (int b = 0; b < obj.Length; b++)
                {
                    if (obj[b] is not Button btn)
                        continue;

                    if (InvokeRequired)
                        Invoke(() => { btn.Enabled = enable; });
                    else btn.Enabled = enable;
                }

                IsReading = !enable;
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

        private void ShowMessageBox(string msg, string caption = "")
        {
            caption = caption == "" ? "RaidCrawler Error" : caption;
            if (InvokeRequired)
                Invoke(() => { MessageBox.Show(msg, caption, MessageBoxButtons.OK); });
            else MessageBox.Show(msg, caption, MessageBoxButtons.OK);
        }

        private int GetRaidBoost()
        {
            if (InvokeRequired)
                return Invoke(() => { return RaidBoost.SelectedIndex; });
            return RaidBoost.SelectedIndex;
        }

        public int GetStatDaySkipTries() => StatDaySkipTries;
        public int GetStatDaySkipSuccess() => StatDaySkipSuccess;

        private void MainWindow_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            InputSwitchIP.Text = Config.IP;
            USB_Port_TB.Text = Config.UsbPort.ToString();
            DefaultColor = IVs.BackColor;
            RaidBoost.SelectedIndex = 0;
            ToggleStreamerView();
        }

        private void InputSwitchIP_Changed(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Config.IP = textBox.Text;
            ConnectionConfig.IP = textBox.Text;
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
                return;
            }

            ShowMessageBox("Please enter a valid numerical USB port.");
        }

        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            lock (_connectLock)
            {
                if (ConnectionWrapper is not null && ConnectionWrapper.Connected)
                    return;

                ConnectionWrapper = new(ConnectionConfig, UpdateStatus);
                Connect(Source.Token);
            }
        }

        private void Connect(CancellationToken token)
        {
            Task.Run(async () =>
            {
                ButtonEnable(new[] { ButtonConnect, SendScreenshot, btnOpenMap, Rewards }, false);
                try
                {
                    (bool success, string err) = await ConnectionWrapper.Connect(token).ConfigureAwait(false);
                    if (!success)
                    {
                        ButtonEnable(new[] { ButtonConnect }, true);
                        ShowMessageBox(err);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    ButtonEnable(new[] { ButtonConnect }, true);
                    ShowMessageBox(ex.Message);
                    return;
                }

                UpdateStatus("Detecting game version...");
                string id = await ConnectionWrapper.Connection.GetTitleID(token).ConfigureAwait(false);
                var game = id switch
                {
                    ScarletID => "Scarlet",
                    VioletID => "Violet",
                    _ => "",
                };

                if (game is "")
                {
                    try
                    {
                        (bool success, string err) = await ConnectionWrapper.DisconnectAsync(token).ConfigureAwait(false);
                        if (!success)
                        {
                            ButtonEnable(new[] { ButtonConnect }, true);
                            ShowMessageBox(err);
                            return;
                        }
                    }
                    catch { }
                    finally
                    {
                        ButtonEnable(new[] { ButtonConnect }, true);
                        ShowMessageBox("Unable to detect Pokémon Scarlet or Pokémon Violet running on your Switch!");
                    }
                    return;
                }

                Config.Game = game;
                RaidContainer.SetGame(Config.Game);

                UpdateStatus("Reading story progress...");
                Config.Progress = await ConnectionWrapper.GetStoryProgress(token).ConfigureAwait(false);
                Config.EventProgress = Math.Min(Config.Progress, 3);

                UpdateStatus("Reading event raid status...");
                try
                {
                    await ReadEventRaids(token).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    ButtonEnable(new[] { ButtonConnect }, true);
                    ShowMessageBox($"Error occurred while reading event raids: {ex.Message}");
                    return;
                }

                UpdateStatus("Reading raids...");
                try
                {
                    await ReadRaids(token).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    ButtonEnable(new[] { ButtonConnect }, true);
                    ShowMessageBox($"Error occurred while reading raids: {ex.Message}");
                    return;
                }

                ButtonEnable(new[] { ButtonAdvanceDate, ButtonReadRaids, ButtonDisconnect, ButtonViewRAM, ButtonDownloadEvents, SendScreenshot, btnOpenMap, Rewards }, true);
                if (InvokeRequired)
                    Invoke(() => { ComboIndex.Enabled = true; ComboIndex.SelectedIndex = 0; });
                else ComboIndex.SelectedIndex = 0;

                UpdateStatus("Completed!");
            }, token);
        }

        private void Disconnect_Click(object sender, EventArgs e)
        {
            lock (_connectLock)
            {
                if (ConnectionWrapper is null || !ConnectionWrapper.Connected)
                    return;

                Disconnect(Source.Token);
            }
        }

        private void Disconnect(CancellationToken token)
        {
            Task.Run(async () =>
            {
                ButtonEnable(new[] { ButtonAdvanceDate, ButtonReadRaids, ButtonDisconnect, ButtonViewRAM, ButtonDownloadEvents, SendScreenshot }, false);
                try
                {
                    (bool success, string err) = await ConnectionWrapper.DisconnectAsync(token).ConfigureAwait(false);
                    if (!success)
                        ShowMessageBox(err);
                }
                catch (Exception ex)
                {
                    ShowMessageBox(ex.Message);
                }

                Source.Cancel();
                DateAdvanceSource.Cancel();
                Source = new();
                DateAdvanceSource = new();
                RaidBlockOffset = 0;
                ButtonEnable(new[] { ButtonConnect }, true);
            }, token);
        }

        private void ButtonPrevious_Click(object sender, EventArgs e)
        {
            var count = RaidContainer.Container.GetRaidCount();
            if (count > 0)
            {
                var index = (ComboIndex.SelectedIndex + count - 1) % count; // Wrap around
                if (ModifierKeys == Keys.Shift)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var chk = (index + count - i) % count;
                        if (StopAdvances || RaidFilters.Any(z => z.FilterSatisfied(RaidContainer.Container.Encounters[chk], RaidContainer.Container.Raids[chk], RaidBoost.SelectedIndex)))
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
            var count = RaidContainer.Container.GetRaidCount();
            if (count > 0)
            {
                var index = (ComboIndex.SelectedIndex + count + 1) % count; // Wrap around
                if (ModifierKeys == Keys.Shift)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var chk = (index + count + i) % count;
                        if (StopAdvances || RaidFilters.Any(z => z.FilterSatisfied(RaidContainer.Container.Encounters[chk], RaidContainer.Container.Raids[chk], RaidBoost.SelectedIndex)))
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
            if (ConnectionWrapper is null || !ConnectionWrapper.Connected)
                return;

            ButtonAdvanceDate.Visible = false;
            StopAdvance_Button.Visible = true;
            Task.Run(async () => await AdvanceDateClick(DateAdvanceSource.Token).ConfigureAwait(false), Source.Token);
        }

        private async Task AdvanceDateClick(CancellationToken token)
        {
            try
            {
                ButtonEnable(new[] { ButtonViewRAM, ButtonAdvanceDate, ButtonDisconnect, ButtonDownloadEvents, SendScreenshot, ButtonReadRaids }, false);
                Invoke(() => Label_DayAdvance.Visible = true);
                SearchTimer.Start();
                stopwatch.Start();
                _WindowState = WindowState;

                var stop = false;
                var raids = RaidContainer.Container.Raids;
                while (!stop)
                {
                    var advanceTextInit = $"Day Skip Successes {GetStatDaySkipSuccess()} / {GetStatDaySkipTries() + 1}";
                    Invoke(() => Label_DayAdvance.Text = advanceTextInit);
                    if (teraRaidView is not null)
                        Invoke(() => teraRaidView.DaySkips.Text = $"Day Skip Successes {GetStatDaySkipSuccess()} / {GetStatDaySkipTries() + 1}");

                    var previousSeeds = raids.Select(z => z.Seed).ToList();
                    UpdateStatus("Changing date...");

                    bool streamer = Config.StreamerView && teraRaidView is not null;
                    await ConnectionWrapper.AdvanceDate(Config, token, streamer ? teraRaidView!.UpdateProgressBar : null).ConfigureAwait(false);
                    await ReadRaids(token).ConfigureAwait(false);
                    raids = RaidContainer.Container.Raids;

                    Invoke(DisplayRaid);
                    if (streamer)
                        Invoke(DisplayPrettyRaid);

                    stop = StopAdvanceDate(previousSeeds);

                    var advanceText = $"Day Skip Successes {GetStatDaySkipSuccess()} / {GetStatDaySkipTries()}";
                    Invoke(() => Label_DayAdvance.Text = advanceText);
                    if (teraRaidView is not null)
                        Invoke(() => teraRaidView.DaySkips.Text = advanceText);
                }

                stopwatch.Stop();
                SearchTimer.Stop();
                var timeSpan = stopwatch.Elapsed;
                string time = string.Format("{0:00}:{1:00}:{2:00}:{3:00}",
                timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

                if (Config.PlaySound)
                    System.Media.SystemSounds.Asterisk.Play();

                if (Config.FocusWindow)
                    Invoke(() => { WindowState = _WindowState; Activate(); });

                if (Config.EnableFilters)
                {
                    var encounters = RaidContainer.Container.Encounters;
                    var rewards = RaidContainer.Container.Rewards;
                    var boost = Invoke(() => { return RaidBoost.SelectedIndex; });
                    for (int i = 0; i < raids.Count; i++)
                    {
                        var satisfied_filters = new List<RaidFilter>();
                        foreach (var filter in RaidFilters)
                        {
                            if (filter is null)
                                continue;

                            if (filter.FilterSatisfied(encounters[i], raids[i], boost))
                            {
                                satisfied_filters.Add(filter);
                                if (InvokeRequired)
                                    Invoke(() => { ComboIndex.SelectedIndex = i; });
                                else ComboIndex.SelectedIndex = i;
                            }
                        }

                        if (satisfied_filters.Count > 0)
                        {
                            // Save game on match.
                            await ConnectionWrapper.SaveGame(token).ConfigureAwait(false);

                            var teraType = raids[i].GetTeraType(encounters[i]);
                            var color = TypeColor.GetTypeSpriteColor((byte)teraType);
                            var hexColor = $"{color.R:X2}{color.G:X2}{color.B:X2}";
                            var blank = new PK9
                            {
                                Species = encounters[i].Species,
                                Form = encounters[i].Form
                            };

                            var spriteName = GetSpriteNameForUrl(blank, raids[i].CheckIsShiny(encounters[i]));
                            await NotificationHandler.SendNotifications(Config, encounters[i], raids[i], satisfied_filters, time, rewards[i], hexColor, spriteName, Source.Token).ConfigureAwait(false);
                        }
                    }

                    if (Config.EnableAlertWindow)
                        ShowMessageBox($"{Config.AlertWindowMessage}\n\nTime Spent: {time}", "Result found!");
                    Invoke(() => Text = $"{formTitle} [Match Found in {time}]");
                }
            }
            catch
            {
                UpdateStatus("Date advance stopped.");
                SearchTimer.Stop();
            }

            if (InvokeRequired)
            {
                Invoke(() => { ButtonAdvanceDate.Visible = true; });
                Invoke(() => { StopAdvance_Button.Visible = false; });
            }
            else
            {
                ButtonAdvanceDate.Visible = true;
                StopAdvance_Button.Visible = false;
            }

            ButtonEnable(new[] { ButtonViewRAM, ButtonAdvanceDate, ButtonDisconnect, ButtonDownloadEvents, SendScreenshot, ButtonReadRaids }, true);
            DateAdvanceSource = new();
        }

        private void StopAdvanceButton_Click(object sender, EventArgs e)
        {
            StopAdvance_Button.Visible = false;
            ButtonAdvanceDate.Visible = true;
            DateAdvanceSource.Cancel();
            DateAdvanceSource = new();
            teraRaidView?.ResetProgressBar();

            stopwatch.Stop();
            SearchTimer.Stop();
        }

        private void ButtonReadRaids_Click(object sender, EventArgs e)
        {
            Task.Run(async () => await ReadRaidsAsync(Source.Token).ConfigureAwait(false), Source.Token);
        }

        private async Task ReadRaidsAsync(CancellationToken token)
        {
            if (IsReading)
            {
                ShowMessageBox("Please wait for the current RAM read to finish.");
                return;
            }

            ButtonEnable(new[] { ButtonViewRAM, ButtonAdvanceDate, ButtonDisconnect, ButtonDownloadEvents, SendScreenshot, ButtonReadRaids }, false);
            try
            {
                await ReadRaids(token).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ShowMessageBox($"Error occurred while reading raids: {ex.Message}");
            }

            ButtonEnable(new[] { ButtonViewRAM, ButtonAdvanceDate, ButtonDisconnect, ButtonDownloadEvents, SendScreenshot, ButtonReadRaids }, true);
        }

        private void ViewRAM_Click(object sender, EventArgs e)
        {
            if (IsReading)
            {
                ShowMessageBox("Please wait for the current RAM read to finish.");
                return;
            }

            ButtonEnable(new[] { ButtonViewRAM }, false);
            RaidBlockViewer window = default!;

            if (ConnectionWrapper is not null && ConnectionWrapper.Connected && ModifierKeys == Keys.Shift)
            {
                try
                {
                    var data = ConnectionWrapper.Connection.ReadBytesAbsoluteAsync(RaidBlockOffset, (int)RaidBlock.SIZE, Source.Token).Result;
                    window = new(data, RaidBlockOffset);
                }
                catch (Exception ex)
                {
                    ButtonEnable(new[] { ButtonViewRAM }, true);
                    ShowMessageBox(ex.Message);
                    return;
                }
            }
            else if (RaidContainer.Container.Raids.Count > ComboIndex.SelectedIndex)
            {
                var data = RaidContainer.Container.Raids[ComboIndex.SelectedIndex].GetData();
                window = new(data, RaidBlockOffset);
            }

            ShowDialog(window);
            ButtonEnable(new[] { ButtonViewRAM }, true);
        }

        private void StopFilter_Click(object sender, EventArgs e)
        {
            var form = new FilterSettings(ref RaidFilters);
            ShowDialog(form);
        }

        private void DownloadEvents_Click(object sender, EventArgs e)
        {
            if (ConnectionWrapper is null || !ConnectionWrapper.Connected)
                return;

            if (IsReading)
            {
                ShowMessageBox("Please wait for the current RAM read to finish.");
                return;
            }

            Task.Run(async () => { await DownloadEventsAsync(Source.Token).ConfigureAwait(false); }, Source.Token);
        }

        private async Task DownloadEventsAsync(CancellationToken token)
        {
            ButtonEnable(new[] { ButtonViewRAM, ButtonAdvanceDate, ButtonDisconnect, ButtonDownloadEvents, SendScreenshot, ButtonReadRaids }, false);
            UpdateStatus("Reading event raid status...");

            try
            {
                await ReadEventRaids(token, true).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ShowMessageBox($"Error occurred while reading event raids: {ex.Message}");
            }

            ButtonEnable(new[] { ButtonViewRAM, ButtonAdvanceDate, ButtonDisconnect, ButtonDownloadEvents, SendScreenshot, ButtonReadRaids }, true);
            UpdateStatus("Completed!");
        }

        private void Seed_Click(object sender, EventArgs e)
        {
            if (ModifierKeys == Keys.Shift && RaidContainer.Container.Raids.Count > ComboIndex.SelectedIndex)
            {
                var raid = RaidContainer.Container.Raids[ComboIndex.SelectedIndex];
                Seed.Text = HideSeed ? $"{raid.Seed:X8}" : "Hidden";
                EC.Text = HideSeed ? $"{raid.EC:X8}" : "Hidden";
                PID.Text = (HideSeed ? $"{raid.PID:X8}" : "Hidden") + $"{(raid.IsShiny ? " (☆)" : string.Empty)}";
                HideSeed = !HideSeed;
                ActiveControl = null;
            }
        }

        private void ConfigSettings_Click(object sender, EventArgs e)
        {
            var form = new ConfigWindow(Config);
            ShowDialog(form);
        }

        private void EnableFilters_Click(object sender, EventArgs e)
        {
            Config.EnableFilters = CheckEnableFilters.Checked;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            var configpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
            JsonSerializerOptions options = new() { WriteIndented = true };
            string output = JsonSerializer.Serialize(Config, options);
            using StreamWriter sw = new(configpath);
            sw.Write(output);

            if (ConnectionWrapper is not null && ConnectionWrapper.Connected)
            {
                try
                {
                    _ = ConnectionWrapper.DisconnectAsync(Source.Token).Result;
                }
                catch { }
            }

            Source.Cancel();
            DateAdvanceSource.Cancel();
            Source = new();
            DateAdvanceSource = new();
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
            (var group_id, var priority) = FlatbufferDumper.DumpDeliveryPriorities(delivery_raid_prio);
            if (priority == 0)
                return;

            var delivery_raid_fbs = await ConnectionWrapper.ReadBlockDefault(BCATRaidBinaryLocation, "raid_enemy_array", force, token).ConfigureAwait(false);
            var delivery_fixed_rewards = await ConnectionWrapper.ReadBlockDefault(BCATRaidFixedRewardLocation, "fixed_reward_item_array", force, token).ConfigureAwait(false);
            var delivery_lottery_rewards = await ConnectionWrapper.ReadBlockDefault(BCATRaidLotteryRewardLocation, "lottery_reward_item_array", force, token).ConfigureAwait(false);

            RaidContainer.DistTeraRaids = TeraDistribution.GetAllEncounters(delivery_raid_fbs);
            RaidContainer.DeliveryRaidPriority = group_id;
            RaidContainer.DeliveryRaidFixedRewards = FlatbufferDumper.DumpFixedRewards(delivery_fixed_rewards);
            RaidContainer.DeliveryRaidLotteryRewards = FlatbufferDumper.DumpLotteryRewards(delivery_lottery_rewards);
        }

        private void DisplayRaid()
        {
            int index = ComboIndex.SelectedIndex;
            var raids = RaidContainer.Container.Raids;
            if (raids.Count > index)
            {
                Raid raid = raids[index];
                var encounter = RaidContainer.Container.Encounters[index];

                Seed.Text = !HideSeed ? $"{raid.Seed:X8}" : "Hidden";
                EC.Text = !HideSeed ? $"{raid.EC:X8}" : "Hidden";
                PID.Text = GetPIDString(raid, encounter);
                Area.Text = $"{Areas.GetArea((int)(raid.Area - 1))} - Den {raid.Den}";
                labelEvent.Visible = raid.IsEvent;

                var teratype = raid.GetTeraType(encounter);
                TeraType.Text = RaidContainer.Strings.types[teratype];

                int StarCount = encounter is TeraDistribution ? encounter.Stars : raid.GetStarCount(raid.Difficulty, Config.Progress, raid.IsBlack);
                Difficulty.Text = string.Concat(Enumerable.Repeat("☆", StarCount));

                var param = encounter.GetParam();
                var blank = new PK9
                {
                    Species = encounter.Species,
                    Form = encounter.Form
                };

                Encounter9RNG.GenerateData(blank, param, EncounterCriteria.Unrestricted, raid.Seed);
                var img = blank.Sprite();
                img = ApplyTeraColor((byte)teratype, img, SpriteBackgroundType.BottomStripe);

                var form = ShowdownParsing.GetStringFromForm(encounter.Form, RaidContainer.Strings, encounter.Species, EntityContext.Gen9);
                if (form.Length > 0 && form[0] != '-')
                    form = form.Insert(0, "-");

                Species.Text = $"{RaidContainer.Strings.Species[encounter.Species]}{form}";
                Sprite.Image = img;
                GemIcon.Image = GetDisplayGemImage(teratype, raid);
                Gender.Text = $"{(Gender)blank.Gender}";

                var nature = blank.Nature;
                Nature.Text = $"{RaidContainer.Strings.Natures[nature]}";
                Ability.Text = $"{RaidContainer.Strings.Ability[blank.Ability]}";

                var extra_moves = new ushort[] { 0, 0, 0, 0 };
                for (int i = 0; i < encounter.ExtraMoves.Length; i++)
                {
                    if (i < extra_moves.Length)
                        extra_moves[i] = encounter.ExtraMoves[i];
                }

                Move1.Text = ShowExtraMoves ? RaidContainer.Strings.Move[extra_moves[0]] : RaidContainer.Strings.Move[encounter.Move1];
                Move2.Text = ShowExtraMoves ? RaidContainer.Strings.Move[extra_moves[1]] : RaidContainer.Strings.Move[encounter.Move2];
                Move3.Text = ShowExtraMoves ? RaidContainer.Strings.Move[extra_moves[2]] : RaidContainer.Strings.Move[encounter.Move3];
                Move4.Text = ShowExtraMoves ? RaidContainer.Strings.Move[extra_moves[3]] : RaidContainer.Strings.Move[encounter.Move4];

                IVs.Text = IVsString(Utils.ToSpeedLast(blank.IVs));
                toolTip.SetToolTip(IVs, IVsString(Utils.ToSpeedLast(blank.IVs), true));

                PID.BackColor = raid.CheckIsShiny(encounter) ? Color.Gold : DefaultColor;
                IVs.BackColor = IVs.Text is "31/31/31/31/31/31" ? Color.YellowGreen : DefaultColor;
            }
            else ShowMessageBox($"Unable to display raid at index {index}. Ensure there are no cheats running or anything else that might shift RAM (Edizon, overlays, etc.), then reboot your console and try again.");
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

        private void DisplayPrettyRaid()
        {
            if (teraRaidView is null)
            {
                ShowMessageBox("Something went terribly wrong: teraRaidView is not initialized.");
                return;
            }

            int index = ComboIndex.SelectedIndex;
            var raids = RaidContainer.Container.Raids;
            if (raids.Count > index)
            {
                Raid raid = raids[index];
                var encounter = RaidContainer.Container.Encounters[index];

                teraRaidView.Area.Text = $"{Areas.GetArea((int)(raid.Area - 1))} - Den {raid.Den}";

                var teratype = raid.GetTeraType(encounter);
                teraRaidView.TeraType.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("gem_text_" + teratype)!;

                int StarCount = encounter is TeraDistribution ? encounter.Stars : raid.GetStarCount(raid.Difficulty, Config.Progress, raid.IsBlack);
                teraRaidView.Difficulty.Text = string.Concat(Enumerable.Repeat("⭐", StarCount));

                if (encounter is not null)
                {
                    var param = encounter.GetParam();
                    var blank = new PK9
                    {
                        Species = encounter.Species,
                        Form = encounter.Form
                    };

                    Encounter9RNG.GenerateData(blank, param, EncounterCriteria.Unrestricted, raid.Seed);
                    var img = blank.Sprite();

                    teraRaidView.picBoxPokemon.Image = img;
                    var form = Utils.GetFormString(blank.Species, blank.Form, raid.Strings);

                    teraRaidView.Species.Text = $"{RaidContainer.Strings.Species[encounter.Species]}{form}";
                    teraRaidView.Gender.Text = $"{(Gender)blank.Gender}";

                    var nature = blank.Nature;
                    teraRaidView.Nature.Text = $"{RaidContainer.Strings.Natures[nature]}";
                    teraRaidView.Ability.Text = $"{RaidContainer.Strings.Ability[blank.Ability]}";

                    teraRaidView.Move1.Text = encounter.Move1 > 0 ? RaidContainer.Strings.Move[encounter.Move1] : "---";
                    teraRaidView.Move2.Text = encounter.Move2 > 0 ? RaidContainer.Strings.Move[encounter.Move2] : "---";
                    teraRaidView.Move3.Text = encounter.Move3 > 0 ? RaidContainer.Strings.Move[encounter.Move3] : "---";
                    teraRaidView.Move4.Text = encounter.Move4 > 0 ? RaidContainer.Strings.Move[encounter.Move4] : "---";

                    var length = encounter.ExtraMoves.Length < 4 ? 4 : encounter.ExtraMoves.Length;
                    var extra_moves = new ushort[length];
                    for (int i = 0; i < encounter.ExtraMoves.Length; i++)
                        extra_moves[i] = encounter.ExtraMoves[i];

                    teraRaidView.Move5.Text = extra_moves[0] > 0 ? RaidContainer.Strings.Move[extra_moves[0]] : "---";
                    teraRaidView.Move6.Text = extra_moves[1] > 0 ? RaidContainer.Strings.Move[extra_moves[1]] : "---";
                    teraRaidView.Move7.Text = extra_moves[2] > 0 ? RaidContainer.Strings.Move[extra_moves[2]] : "---";
                    teraRaidView.Move8.Text = extra_moves[3] > 0 ? RaidContainer.Strings.Move[extra_moves[3]] : "---";

                    var ivs = Utils.ToSpeedLast(blank.IVs);

                    // HP
                    teraRaidView.HP.Text = $"{ivs[0]:D2}";
                    teraRaidView.HP.BackColor = Color.FromArgb(0, 5, 25);
                    if (teraRaidView.HP.Text is "31")
                        teraRaidView.HP.BackColor = Color.ForestGreen;
                    else if (teraRaidView.HP.Text is "00")
                        teraRaidView.HP.BackColor = Color.DarkRed;

                    // ATK
                    teraRaidView.ATK.Text = $"{ivs[1]:D2}";
                    teraRaidView.ATK.BackColor = Color.FromArgb(0, 5, 25);
                    if (teraRaidView.ATK.Text is "31")
                        teraRaidView.ATK.BackColor = Color.ForestGreen;
                    else if (teraRaidView.ATK.Text is "00")
                        teraRaidView.ATK.BackColor = Color.DarkRed;

                    // DEF
                    teraRaidView.DEF.Text = $"{ivs[2]:D2}";
                    teraRaidView.DEF.BackColor = Color.FromArgb(0, 5, 25);
                    if (teraRaidView.DEF.Text is "31")
                        teraRaidView.DEF.BackColor = Color.ForestGreen;
                    else if (teraRaidView.DEF.Text is "00")
                        teraRaidView.DEF.BackColor = Color.DarkRed;

                    // SPA
                    teraRaidView.SPA.Text = $"{ivs[3]:D2}";
                    teraRaidView.SPA.BackColor = Color.FromArgb(0, 5, 25);
                    if (teraRaidView.SPA.Text is "31")
                        teraRaidView.SPA.BackColor = Color.ForestGreen;
                    else if (teraRaidView.SPA.Text is "00")
                        teraRaidView.SPA.BackColor = Color.DarkRed;

                    // SPD
                    teraRaidView.SPD.Text = $"{ivs[4]:D2}";
                    teraRaidView.SPD.BackColor = Color.FromArgb(0, 5, 25);
                    if (teraRaidView.SPD.Text is "31")
                        teraRaidView.SPD.BackColor = Color.ForestGreen;
                    else if (teraRaidView.SPD.Text is "00")
                        teraRaidView.SPD.BackColor = Color.DarkRed;

                    // SPEED
                    teraRaidView.SPEED.Text = $"{ivs[5]:D2}";
                    teraRaidView.SPEED.BackColor = Color.FromArgb(0, 5, 25);
                    if (teraRaidView.SPEED.Text is "31")
                        teraRaidView.SPEED.BackColor = Color.ForestGreen;
                    else if (teraRaidView.SPEED.Text is "00")
                        teraRaidView.SPEED.BackColor = Color.DarkRed;


                    var map = GenerateMap(raid, teratype);
                    if (map is null)
                        ShowMessageBox("Error generating map.");
                    teraRaidView.Map.Image = map;

                    // Rewards
                    var rewards = RaidContainer.Container.Rewards[index];

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

                    for (int i = 0; i < rewards.Count; i++)
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

                    var shiny = raid.CheckIsShiny(encounter);
                    teraRaidView.Shiny.Visible = shiny;
                    teraRaidView.picShinyAlert.Enabled = shiny;
                }
                else
                {
                    // TODO Clear all the fields.
                }
            }
            else ShowMessageBox($"Unable to display raid at index {index}. Ensure there are no cheats running or anything else that might shift RAM (Edizon, overlays, etc.), then reboot your console and try again.");
        }

        private string GetPIDString(Raid raid, ITeraRaid? enc)
        {
            if (HideSeed)
                return "Hidden";

            var shiny_mark = " (☆)";
            var pid = $"{raid.PID:X8}";
            return raid.CheckIsShiny(enc) ? pid + shiny_mark : pid;
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

            double x, y;
            try
            {
                if (den_locations.TryGetValue($"{raid.Area}-{raid.Den}_", out float[]? value))
                {
                    x = (value[0] + 2.072021484) * 512 / 5000;
                    y = (value[2] + 5255.240018) * 512 / 5000;
                    return ImageUtil.LayerImage(map, gem, (int)x, (int)y);
                }

                x = (den_locations[$"{raid.Area}-{raid.Den}"][0] + 2.072021484) * 512 / 5000;
                y = (den_locations[$"{raid.Area}-{raid.Den}"][2] + 5255.240018) * 512 / 5000;
                return ImageUtil.LayerImage(map, gem, (int)x, (int)y);
            }
            catch { return null; }
        }

        private bool StopAdvanceDate(List<uint> previousSeeds)
        {
            var raids = RaidContainer.Container.Raids;
            var curSeeds = raids.Select(x => x.Seed).ToArray();
            var sameraids = curSeeds.Except(previousSeeds).ToArray().Length == 0;

            StatDaySkipTries++;
            if (sameraids)
                return false;

            StatDaySkipSuccess++;
            if (!Config.EnableFilters)
                return true;

            for (int i = 0; i < RaidFilters.Count; i++)
            {
                var index = 0;
                if (InvokeRequired)
                    index = Invoke(() => { return RaidBoost.SelectedIndex; });
                else index = RaidBoost.SelectedIndex;

                var encounters = RaidContainer.Container.Encounters;
                if (RaidFilters[i].FilterSatisfied(encounters, raids, index))
                    return true;
            }

            return StopAdvances;
        }

        private async Task ReadRaids(CancellationToken token)
        {
            if (RaidBlockOffset == 0)
            {
                UpdateStatus("Caching the raid block pointer...");
                RaidBlockOffset = await ConnectionWrapper.Connection.PointerAll(ConnectionWrapper.RaidBlockPointer, token).ConfigureAwait(false);
            }

            RaidContainer.Container.ClearRaids();
            RaidContainer.Container.ClearEncounters();
            RaidContainer.Container.ClearRewards();

            UpdateStatus("Reading raid block...");
            var data = await ConnectionWrapper.Connection.ReadBytesAbsoluteAsync(RaidBlockOffset + RaidBlock.HEADER_SIZE, (int)(RaidBlock.SIZE - RaidBlock.HEADER_SIZE), token).ConfigureAwait(false);
            var failed = RaidContainer.ReadAllRaids(data, Config.Progress, Config.EventProgress, GetRaidBoost());
            if (failed > 0)
                ShowMessageBox($"There were {failed} raids that failed to be read.\nMore info can be found in the \"raid_dbg.txt\" file.");

            var raids = RaidContainer.Container.Raids;
            var encounters = RaidContainer.Container.Encounters;
            UpdateStatus("Completed!");

            var filterMatchCount = Enumerable.Range(0, raids.Count).Count(c => RaidFilters.Any(z => z.FilterSatisfied(encounters[c], raids[c], GetRaidBoost())));
            if (InvokeRequired)
                Invoke(() => { LabelLoadedRaids.Text = $"Matches: {filterMatchCount}"; });
            else LabelLoadedRaids.Text = $"Matches: {filterMatchCount}";

            if (raids.Count > 0)
            {
                ButtonEnable(new[] { ButtonPrevious, ButtonNext }, true);
                var dataSource = Enumerable.Range(0, raids.Count).Select(z => $"{z + 1:D} / {raids.Count:D}").ToArray();
                if (InvokeRequired)
                    Invoke(() => { ComboIndex.DataSource = dataSource; });
                else ComboIndex.DataSource = dataSource;

                if (InvokeRequired)
                    Invoke(() => { ComboIndex.SelectedIndex = ComboIndex.SelectedIndex < raids.Count ? ComboIndex.SelectedIndex : 0; });
                else ComboIndex.SelectedIndex = ComboIndex.SelectedIndex < raids.Count ? ComboIndex.SelectedIndex : 0;
            }
            else
            {
                ButtonEnable(new[] { ButtonPrevious, ButtonNext }, false);
                if (raids.Count > RaidBlock.MAX_COUNT || raids.Count == 0)
                    ShowMessageBox("Bad read, ensure there are no cheats running or anything else that might shift RAM (Edizon, overlays, etc.), then reboot your console and try again.");
            }
        }

        public void Game_SelectedIndexChanged(string name)
        {
            Config.Game = name;
            RaidContainer.SetGame(name);
            if (RaidContainer.Container.Raids.Count > 0)
                DisplayRaid();
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
                ConnectionConfig.Port = Config.UsbPort;
            }
            else
            {
                InputSwitchIP.Visible = true;
                LabelSwitchIP.Visible = true;
                USB_Port_label.Visible = false;
                USB_Port_TB.Visible = false;
                ConnectionConfig.Port = 6000;
            }
        }

        private void DisplayMap(object sender, EventArgs e)
        {
            var raids = RaidContainer.Container.Raids;
            if (raids.Count == 0)
            {
                ShowMessageBox("Raids not loaded.");
                return;
            }

            var raid = raids[ComboIndex.SelectedIndex];
            var encounter = RaidContainer.Container.Encounters[ComboIndex.SelectedIndex];
            var teratype = raid.GetTeraType(encounter);
            var map = GenerateMap(raid, teratype);
            if (map is null)
            {
                ShowMessageBox("Error generating map.");
                return;
            }

            var form = new MapView(map);
            ShowDialog(form);
        }

        private void Rewards_Click(object sender, EventArgs e)
        {
            if (RaidContainer.Container.Raids.Count == 0)
            {
                ShowMessageBox("Raids not loaded.");
                return;
            }

            var rewards = RaidContainer.Container.Rewards[ComboIndex.SelectedIndex];
            if (rewards is null)
            {
                ShowMessageBox("Error while displaying rewards.");
                return;
            }

            var form = new RewardsView(RaidContainer.Strings.Item, rewards);
            ShowDialog(form);
        }

        private void RaidBoost_SelectedIndexChanged(object sender, EventArgs e)
        {
            RaidContainer.Container.ClearRewards();
            var raids = RaidContainer.Container.Raids;
            var encounters = RaidContainer.Container.Encounters;

            List<List<(int, int, int)>> newRewards = new();
            for (int i = 0; i < raids.Count; i++)
            {
                var raid = raids[i];
                var encounter = encounters[i];
                newRewards.Add(encounter.GetRewards(raid, RaidBoost.SelectedIndex));
            }
            RaidContainer.Container.SetRewards(newRewards);
        }

        private void Move_Clicked(object sender, EventArgs e)
        {
            if (RaidContainer.Container.Raids.Count == 0)
            {
                ShowMessageBox("Raids not loaded.");
                return;
            }

            var encounter = RaidContainer.Container.Encounters[ComboIndex.SelectedIndex];
            if (encounter is null)
                return;

            ShowExtraMoves ^= true;
            LabelMoves.Text = ShowExtraMoves ? "Extra:" : "Moves:";
            LabelMoves.Location = new(LabelMoves.Location.X + (ShowExtraMoves ? 9 : -9), LabelMoves.Location.Y);

            var length = encounter.ExtraMoves.Length < 4 ? 4 : encounter.ExtraMoves.Length;
            var extra_moves = new ushort[length];
            for (int i = 0; i < encounter.ExtraMoves.Length; i++)
                extra_moves[i] = encounter.ExtraMoves[i];

            Move1.Text = ShowExtraMoves ? RaidContainer.Strings.Move[extra_moves[0]] : RaidContainer.Strings.Move[encounter.Move1];
            Move2.Text = ShowExtraMoves ? RaidContainer.Strings.Move[extra_moves[1]] : RaidContainer.Strings.Move[encounter.Move2];
            Move3.Text = ShowExtraMoves ? RaidContainer.Strings.Move[extra_moves[2]] : RaidContainer.Strings.Move[encounter.Move3];
            Move4.Text = ShowExtraMoves ? RaidContainer.Strings.Move[extra_moves[3]] : RaidContainer.Strings.Move[encounter.Move4];
        }

        private void ComboIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RaidContainer.Container.Raids.Count == 0)
                return;

            DisplayRaid();
            if (Config.StreamerView)
                DisplayPrettyRaid();
        }

        private void SendScreenshot_Click(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                try
                {
                    await NotificationHandler.SendScreenshot(Config, ConnectionWrapper.Connection, Source.Token).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    ShowMessageBox($"Could not send the screenshot: {ex.Message}");
                }
            }, Source.Token);
        }

        private void SearchTimer_Elapsed(object sender, EventArgs e)
        {
            if (!stopwatch.IsRunning)
                return;

            var timeSpan = stopwatch.Elapsed;
            string time = string.Format("{0:00}:{1:00}:{2:00}:{3:00}",
            timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

            Invoke(() => Text = formTitle + " [Searching for " + time + "]");
            if (Config.StreamerView && teraRaidView is not null)
                Invoke(() => teraRaidView.textSearchTime.Text = time);
        }

        public void TestWebhook()
        {
            Task.Run(async () => await TestWebhookAsync(Source.Token).ConfigureAwait(false), Source.Token);
        }

        private async Task TestWebhookAsync(CancellationToken token)
        {
            var filter = new RaidFilter { Name = "Test Webhook" };
            var satisfied_filters = new List<RaidFilter> { filter };

            int i = -1;
            if (InvokeRequired)
                i = Invoke(() => { return ComboIndex.SelectedIndex; });
            else i = ComboIndex.SelectedIndex;

            var raids = RaidContainer.Container.Raids;
            var encounters = RaidContainer.Container.Encounters;
            var rewards = RaidContainer.Container.Rewards;
            if (i > -1 && encounters[i] is not null && raids[i] is not null)
            {
                var timeSpan = stopwatch.Elapsed;
                string time = string.Format("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                var teraType = raids[i].GetTeraType(encounters[i]);
                var color = TypeColor.GetTypeSpriteColor((byte)teraType);
                var hexColor = $"{color.R:X2}{color.G:X2}{color.B:X2}";

                var blank = new PK9
                {
                    Species = encounters[i].Species,
                    Form = encounters[i].Form,
                    Gender = encounters[i].Gender,
                };
                blank.SetSuggestedFormArgument();

                var spriteName = GetSpriteNameForUrl(blank, raids[i].CheckIsShiny(encounters[i]));
                await NotificationHandler.SendNotifications(Config, encounters[i], raids[i], satisfied_filters, time, rewards[i], hexColor, spriteName, token).ConfigureAwait(false);
            }
            else ShowMessageBox("Please connect to your device and ensure a raid has been found.");
        }

        public void ToggleStreamerView()
        {
            if (Config.StreamerView)
            {
                teraRaidView = new();
                teraRaidView.Map.Image = map;
                teraRaidView.Show();
            }
            else if (!Config.StreamerView && teraRaidView is not null)
                teraRaidView.Close();
        }

        private static string GetSpriteNameForUrl(PK9 pk, bool shiny)
        {
            // Since we're later using this for URL assembly later, we need dashes instead of underscores for forms.
            var spriteName = SpriteName.GetResourceStringSprite(pk.Species, pk.Form, pk.Gender, pk.FormArgument, EntityContext.Gen9, shiny)[1..];
            return spriteName.Replace('_', '-').Insert(0, "_");
        }
    }
}
