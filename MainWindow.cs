using RaidCrawler.Properties;
using RaidCrawler.Structures;
using SysBot.Base;
using System.Data;
using System.Net.Sockets;
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

            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            InputSwitchIP.Text = Settings.Default.SwitchIP;
            LabelIndex.Text = string.Empty;
            DefaultColor = IVs.BackColor;
            Progress.SelectedIndex = Settings.Default.Progress;
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
                    await ReadRaids(CancellationToken.None);
                    ButtonAdvanceDate.Enabled = true;
                    ButtonReadRaids.Enabled = true;
                    ButtonConnect.Enabled = false;
                    ButtonDisconnect.Enabled = true;
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

                int StarCount = GetStarCount(raid.Difficulty, Progress.SelectedIndex, raid.IsBlack);

                Difficulty.Text = raid.IsEvent ? "coming soon™" : string.Concat(Enumerable.Repeat("☆", StarCount)) + $" ({raid.Difficulty})";
                IVs.Text = IVsString(raid.GetIVs(raid.Seed, StarCount - 1));

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

        private static int GetStarCount(uint Difficulty, int Progress, bool IsBlack)
        {
            if (IsBlack) return 6;
            return Progress switch
            {
                0 => Difficulty switch
                {
                    > 80 => 2,
                    _ => 1,
                },
                1 => Difficulty switch
                {
                    > 70 => 3,
                    > 30 => 2,
                    _ => 1,
                },
                2 => Difficulty switch
                {
                    > 70 => 4,
                    > 40 => 3,
                    > 20 => 2,
                    _ => 1,
                },
                3 => Difficulty switch
                {
                    > 75 => 5,
                    > 40 => 4,
                    _ => 3,
                },
                4 => Difficulty switch
                {
                    > 70 => 5,
                    > 30 => 4,
                    _ => 3,
                },
                _ => 1,
            };
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
                DisplayRaid(index);
            }
        }

        private void ButtonNext_Click(object sender, EventArgs e)
        {
            if (Raids.Count > 0)
            {
                index = (index + Raids.Count + 1) % Raids.Count; // Wrap around
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
                } while (CheckContinueUntilShiny.Checked && !Raids.Where(raid => raid.IsShiny).Any());
                ButtonReadRaids.Enabled = true;
                ButtonAdvanceDate.Enabled = true;
            }
        }

        private async void ButtonReadRaids_Click(object sender, EventArgs e)
        {
            ButtonReadRaids.Enabled = false;
            ButtonAdvanceDate.Enabled = false;
            await ReadRaids(CancellationToken.None);
            ButtonReadRaids.Enabled = true;
            ButtonAdvanceDate.Enabled = true;

        }

        private async Task ReadRaids(CancellationToken token)
        {
            ConnectionStatusText.Text = "Parsing pointer...";
            ulong offset = await OffsetUtil.GetPointerAddress(Offsets.RaidBlockPointer, CancellationToken.None);

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
            else if (Raids.Count > 69 || Raids.Count == 0)
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
    }
}