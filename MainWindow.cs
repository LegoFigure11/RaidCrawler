using RaidCrawler.Properties;
using RaidCrawler.Structures;
using RaidCrawler.Subforms;
using SysBot.Base;
using System.Data;
using System.Net.Sockets;
using PKHeX.Core;
using static SysBot.Base.SwitchButton;

namespace RaidCrawler
{
    public partial class MainWindow : Form
    {

        private readonly static SwitchConnectionConfig Config = new() { Protocol = SwitchProtocol.WiFi, IP = Settings.Default.SwitchIP, Port = 6000 };
        private readonly static SwitchSocketAsync SwitchConnection = new(Config);

        private readonly static OffsetUtil OffsetUtil = new(SwitchConnection);

        private readonly List<Raid> Raids = new();

        private int index = 0;
        private ulong offset;
        private bool IsReading = false;

        private Color DefaultColor;

        public MainWindow()
        {
            string build = string.Empty;
#if DEBUG
            var date = File.GetLastWriteTime(System.Reflection.Assembly.GetEntryAssembly()!.Location);
            build = $" (dev-{date:yyyyMMdd})";
#endif
            var v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version!;
            Text = "RaidCrawler v" + v.Major + "." + v.Minor + "." + v.Build + build;

            Raid.GemTeraRaids = TeraEncounter.GetAllEncounters("encounter_gem_paldea.pkl");
            Raid.DistTeraRaids = TeraDistribution.GetAllEncounters("encounter_dist_paldea.pkl");
            Raid.Game = Settings.Default.Game;

            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            InputSwitchIP.Text = Settings.Default.SwitchIP;
            LabelIndex.Text = string.Empty;
            DefaultColor = IVs.BackColor;
            Progress.SelectedIndex = Settings.Default.Progress;
            EventProgress.SelectedIndex = Settings.Default.EventProgress;
            Game.SelectedIndex = Game.FindString(Settings.Default.Game);
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
                    await ReadRaids(CancellationToken.None);
                    IsReading = false;
                    ButtonAdvanceDate.Enabled = true;
                    ButtonReadRaids.Enabled = true;
                    ButtonConnect.Enabled = false;
                    ButtonDisconnect.Enabled = true;
                    ButtonViewRAM.Enabled = true;
                }
                catch (SocketException err)
                {
                    Disconnect(true);
                    // a bit hacky but it works
                    if (err.Message.Contains("failed to respond") || err.Message.Contains("actively refused"))
                    {
                        ConnectionStatusText.Text = "Unable to connect.";
                        MessageBox.Show(err.Message);
                    }
                }
            }
        }

        private void Disconnect(bool SkipCheckForExistingConnection = false)
        {
            if (SwitchConnection.Connected || SkipCheckForExistingConnection)
            {
                SwitchConnection.Disconnect();
                ConnectionStatusText.Text = "Disconnected.";
                ButtonConnect.Enabled = true;
                ButtonDisconnect.Enabled = false;
                ButtonReadRaids.Enabled = false;
                ButtonAdvanceDate.Enabled = false;
                ButtonViewRAM.Enabled = false;
            }
        }

        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void DisplayRaid(int index)
        {
            LabelIndex.Text = $"{index + 1:D2} / {Raids.Count:D2}";
            if (Raids.Count >= index)
            {
                Raid raid = Raids[index];
                Seed.Text = $"{raid.Seed:X8}";
                EC.Text = $"{raid.EC:X8}";
                PID.Text = $"{raid.PID:X8}";
                TeraType.Text = raid.TeraType;
                Area.Text = $"{Areas.Area[raid.Area - 1]} - Den {raid.Den}";
                IsEvent.Checked = raid.IsEvent;

                int StarCount = Raid.GetStarCount(raid.Difficulty, Progress.SelectedIndex, raid.IsBlack);
                ITeraRaid? encounter = raid.IsEvent ? TeraDistribution.GetEncounter(raid.Seed, EventProgress.SelectedIndex) : TeraEncounter.GetEncounter(raid.Seed, Progress.SelectedIndex, raid.IsBlack);
                Species.Text = encounter == null ? "Unknown" : $"{Raid.strings.Species[encounter.Species]} ({encounter.Species})";
                if (encounter != null)
                {
                    Move1.Text = Raid.strings.Move[encounter.Move1];
                    Move2.Text = Raid.strings.Move[encounter.Move2];
                    Move3.Text = Raid.strings.Move[encounter.Move3];
                    Move4.Text = Raid.strings.Move[encounter.Move4];
                }
                else
                {
                    Move1.Text = string.Empty;
                    Move2.Text = string.Empty;
                    Move3.Text = string.Empty;
                    Move4.Text = string.Empty;
                }

                Difficulty.Text = raid.IsEvent ? string.Concat(Enumerable.Repeat("☆", StarCount)) : string.Concat(Enumerable.Repeat("☆", StarCount)) + $" ({raid.Difficulty})";
                IVs.Text = IVsString(raid.GetIVs(raid.Seed, StarCount - 1));
                
                if (encounter != null)
                {
                    var pi = PersonalTable.SV.GetFormEntry(encounter.Species, encounter.Form);
                    var param = new GenerateParam9((byte)pi.Gender, (byte)(StarCount - 1), 1, 0, 0, 0, encounter.Ability, encounter.Shiny);
                    var blank = new PK9();
                    blank.Species = encounter.Species;
                    blank.Form = encounter.Form;
                    Encounter9RNG.GenerateData(blank, param, EncounterCriteria.Unrestricted, raid.Seed);
                    Gender.Text = $"{(Gender)blank.Gender}";
                    Nature.Text = $"{Raid.strings.Natures[blank.Nature]}";
                }
                else
                {
                    Gender.Text = string.Empty;
                    Nature.Text = string.Empty;
                }

                if (raid.IsShiny)
                {
                    PID.BackColor = Color.Gold;
                    PID.Text += " (☆)";
                }
                else
                {
                    PID.BackColor = DefaultColor;
                }

                if (IVs.Text is "31/31/31/31/31/31")
                {
                    IVs.BackColor = Color.YellowGreen;
                }
                else
                {
                    IVs.BackColor = DefaultColor;
                }
            }
            else
            {
                MessageBox.Show($"Unable to display raid at index {index}. Ensure there are no cheats running or anything else that might shift RAM (Edizon, overlays, etc.), then reboot your console and try again.");
            }
        }

        private static string IVsString(int[] ivs)
        {
            string s = string.Empty;
            for (int i = 0; i < ivs.Length; i++)
            {
                s += $"{ivs[i]:D2}";
                if (i < 5)
                    s += "/";
            }
            return s;
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
                        if (RaidFilters.FilterSatisfied(Raids[chk], Progress.SelectedIndex, EventProgress.SelectedIndex))
                        {
                            index = chk;
                            break;
                        }
                    }
                }
                DisplayRaid(index);
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
                        if (RaidFilters.FilterSatisfied(Raids[chk], Progress.SelectedIndex, EventProgress.SelectedIndex))
                        {
                            index = chk;
                            break;
                        }
                    }
                }
                DisplayRaid(index);
            }
        }

        private static new async Task Click(SwitchButton button, int delay, CancellationToken token)
        {
            await SwitchConnection.SendAsync(SwitchCommand.Click(button, true), token).ConfigureAwait(false);
            await Task.Delay(delay, token).ConfigureAwait(false);
        }

        private static async Task PressAndHold(SwitchButton b, int hold, int delay, CancellationToken token)
        {
            await SwitchConnection.SendAsync(SwitchCommand.Hold(b, true), token).ConfigureAwait(false);
            await Task.Delay(hold, token).ConfigureAwait(false);
            await SwitchConnection.SendAsync(SwitchCommand.Release(b, true), token).ConfigureAwait(false);
            await Task.Delay(delay, token).ConfigureAwait(false);
        }

        private async Task AdvanceDate(CancellationToken token)
        {
            ConnectionStatusText.Text = "Changing date...";
            await Click(LSTICK, 0_050, token).ConfigureAwait(false); // Sometimes it seems like the first command doesn't go through so send this just in case
            // HOME Menu
            await Click(HOME, 1_800, token).ConfigureAwait(false);
            await Click(DDOWN, 0_200, token).ConfigureAwait(false);
            for (int i = 0; i < 5; i++) await Click(DRIGHT, 0_100, token).ConfigureAwait(false);
            await Click(A, 1_000, token).ConfigureAwait(false);
            // Scroll to bottom
            await PressAndHold(DDOWN, 1_700, 0, token).ConfigureAwait(false);
            await Click(DRIGHT, 0_200, token).ConfigureAwait(false);
            for (int i = 0; i < 40; i++) await Click(DDOWN, 0_100, token).ConfigureAwait(false);
            await Click(A, 0_800, token).ConfigureAwait(false);
            for (int i = 0; i < 2; i++) await Click(DDOWN, 0_200, token).ConfigureAwait(false);
            await Click(A, 0_500, token).ConfigureAwait(false);
            await Click(DUP, 0_200, token).ConfigureAwait(false);
            for (int i = 0; i < 6; i++) await Click(DRIGHT, 0_100, token).ConfigureAwait(false);
            await Click(A, 0_500, token).ConfigureAwait(false);
            await Click(HOME, 2_500, token).ConfigureAwait(false);
            await Click(HOME, 4_000, token).ConfigureAwait(false);
        }

        private async void ButtonAdvanceDate_Click(object sender, EventArgs e)
        {
            if (SwitchConnection.Connected)
            {
                ButtonReadRaids.Enabled = false;
                ButtonAdvanceDate.Enabled = false;
                do
                {
                    await AdvanceDate(CancellationToken.None);
                    await ReadRaids(CancellationToken.None);
                } while (!RaidFilters.FilterSatisfied(Raids, Progress.SelectedIndex, EventProgress.SelectedIndex));
                ButtonReadRaids.Enabled = true;
                ButtonAdvanceDate.Enabled = true;
            }
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
            offset = await OffsetUtil.GetPointerAddress(Offsets.RaidBlockPointer, CancellationToken.None);

            Raids.Clear();
            index = 0;

            ConnectionStatusText.Text = "Reading raid block...";
            Raid raid;
            for (uint i = RaidBlock.HEADER_SIZE; i < RaidBlock.SIZE; i += Raid.SIZE)
            {
                ConnectionStatusText.Text = $"Reading raid block... {i / Raid.SIZE}%";
                var Data = await SwitchConnection.ReadBytesAbsoluteAsync(offset + i, Raid.SIZE, token);
                raid = new Raid(Data);
                if (raid.IsValid) Raids.Add(raid);
            }

            ConnectionStatusText.Text = "Completed!";
            LabelLoadedRaids.Text = $"Loaded Raids: {Raids.Count} | Shiny Raids: {Raids.Where(raid => raid.IsShiny).Count()}";
            if (Raids.Count > 0)
            {
                ButtonPrevious.Enabled = true;
                ButtonNext.Enabled = true;
            }
            else
            {
                ButtonPrevious.Enabled = false;
                ButtonNext.Enabled = false;
            }

            if (Raids.Count > 0)
            {
                DisplayRaid(index);
            }
            else if (Raids.Count > RaidBlock.MAX_COUNT || Raids.Count == 0)
            {
                MessageBox.Show("Bad read, ensure there are no cheats running or anything else that might shift RAM (Edizon, overlays, etc.), then reboot your console and try again.");
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
            if (SwitchConnection.Connected)
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
            var form = new FilterSettings();
            form.ShowDialog();
        }
    }
}