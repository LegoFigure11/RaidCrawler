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
        private List<RaidFilter> RaidFilters = new();

        private int index = 0;
        private ulong offset;
        private bool IsReading = false;
        private bool HideSeed = false;

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

            Raid.GemTeraRaids = TeraEncounter.GetAllEncounters("encounter_gem_paldea.pkl");
            Raid.DistTeraRaids = TeraDistribution.GetAllEncounters("raid_enemy_array");
            var filterpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "filters.json");
            if (File.Exists(filterpath))
                RaidFilters = JsonConvert.DeserializeObject<List<RaidFilter>>(File.ReadAllText(filterpath)) ?? new List<RaidFilter>();
            Raid.Game = Settings.Default.Game;
            SpriteBuilder.ShowTeraThicknessStripe = 0x4;
            SpriteBuilder.ShowTeraOpacityStripe = 0xAF;
            SpriteBuilder.ShowTeraOpacityBackground = 0xFF;
            SpriteUtil.ChangeMode(SpriteBuilderMode.SpritesArtwork5668);

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
            ConnectionStatusText.Text = "Disconnected.";
            ButtonConnect.Enabled = true;
            ButtonDisconnect.Enabled = false;
            ButtonReadRaids.Enabled = false;
            ButtonAdvanceDate.Enabled = false;
            ButtonViewRAM.Enabled = false;
            ButtonDumpRaid.Enabled = false;
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

                    ConnectionStatusText.Text = "Reading raids...";
                    await ReadRaids(CancellationToken.None);

                    IsReading = false;
                    ButtonAdvanceDate.Enabled = true;
                    ButtonReadRaids.Enabled = true;
                    ButtonConnect.Enabled = false;
                    ButtonDisconnect.Enabled = true;
                    ButtonViewRAM.Enabled = true;
                    ButtonDumpRaid.Enabled = true;
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
                // See https://github.com/Lincoln-LM/sv-live-map/blob/da93e0edd2fb9b89d76ec0027826c9e89acdcda5/sv_live_map_core/raid_reader.py#L59
                var address = await OffsetUtil.GetPointerAddress($"{SaveBlockPointer}+{DifficultyFlags[i]:X}", token);
                var key = ReadUInt32LittleEndian(await SwitchConnection.ReadBytesAbsoluteAsync(address, 4, token));
                var decryptedKey = DecryptStoryProgressKey(key);
                address = await OffsetUtil.GetPointerAddress($"[{SaveBlockPointer}+{DifficultyFlags[i] + 8:X}]", token);
                var val = await SwitchConnection.ReadBytesAbsoluteAsync(address, 1, token);
                if ((decryptedKey ^ val[0]) == 2) progress = i + 1;
            }
            return progress;
        }

        private static uint DecryptStoryProgressKey(uint key)
        {
            var rng = new SCXorShift32(key);
            return rng.Next();
        }

        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void DisplayRaid(int index)
        {
            LabelIndex.Text = $"{index + 1:D2} / {Raids.Count:D2}";
            if (Raids.Count > index)
            {
                Raid raid = Raids[index];
                Seed.Text = !HideSeed ? $"{raid.Seed:X8}" : "Hidden";
                EC.Text = !HideSeed ? $"{raid.EC:X8}" : "Hidden";
                PID.Text = (!HideSeed ? $"{raid.PID:X8}" : "Hidden") + $"{(raid.IsShiny ? " (☆)" : string.Empty)}";
                Area.Text = $"{Areas.Area[raid.Area - 1]} - Den {raid.Den}";
                IsEvent.Checked = raid.IsEvent;

                var progress = raid.IsEvent ? EventProgress.SelectedIndex : Progress.SelectedIndex;
                ITeraRaid? encounter = raid.Encounter(progress);
                var teratype = GetTeraType(encounter, raid);
                TeraType.Text = $"{Raid.strings.types[teratype]} ({teratype})";
                int StarCount = encounter is TeraDistribution ? encounter.Stars : Raid.GetStarCount(raid.Difficulty, Progress.SelectedIndex, raid.IsBlack);
                Difficulty.Text = raid.IsEvent ? string.Concat(Enumerable.Repeat("☆", StarCount)) : string.Concat(Enumerable.Repeat("☆", StarCount)) + $" ({raid.Difficulty})";

                if (encounter != null)
                {
                    var param = GetParam(encounter);
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
                    Move1.Text = Raid.strings.Move[encounter.Move1];
                    Move2.Text = Raid.strings.Move[encounter.Move2];
                    Move3.Text = Raid.strings.Move[encounter.Move3];
                    Move4.Text = Raid.strings.Move[encounter.Move4];
                    IVs.Text = IVsString(ToSpeedLast(blank.IVs));
                    var scale = blank.Scale;
                    var height = blank.HeightScalar;
                    var weight = blank.WeightScalar;
                    HeightSize.Text = $"{height}";
                    WeightSize.Text = $"{weight}";
                    ScaleSize.Text = $"{scale}";
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

                PID.BackColor = raid.IsShiny ? Color.Gold : DefaultColor;
                IVs.BackColor = IVs.Text is "31/31/31/31/31/31" ? Color.YellowGreen : DefaultColor;
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

        private static byte GetGender(ITeraRaid enc)
        {
            if (enc is not TeraDistribution td || td.Entity is EncounterDist9)
                return (byte)PersonalTable.SV.GetFormEntry(enc.Species, enc.Form).Gender;
            if (td.Entity is EncounterMight9 em)
                return em.Gender switch
                {
                    0 => PersonalInfo.RatioMagicMale,
                    1 => PersonalInfo.RatioMagicFemale,
                    2 => PersonalInfo.RatioMagicGenderless,
                    _ => (byte)PersonalTable.SV.GetFormEntry(enc.Species, enc.Form).Gender,
                };
            return (byte)PersonalTable.SV.GetFormEntry(enc.Species, enc.Form).Gender;
        }

        private static GenerateParam9 GetParam(ITeraRaid encounter)
        {
            var gender = GetGender(encounter);
            if (encounter is TeraDistribution td && td.Entity is EncounterMight9 em)
                return new GenerateParam9(gender, em.FlawlessIVCount, 1, 0, 0, em.Scale, em.Ability, em.Shiny, em.Nature, em.IVs);
            return new GenerateParam9(gender, encounter.FlawlessIVCount, 1, 0, 0, 0, encounter.Ability, encounter.Shiny);
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

        private static int GetTeraType(ITeraRaid? encounter, Raid raid)
        {
            if (encounter == null)
                return raid.TeraType;
            if (encounter is TeraDistribution td && td.Entity is ITeraRaid9 gem)
                return (int)gem.TeraType > 1 ? (int)gem.TeraType - 2 : raid.TeraType;
            return raid.TeraType;
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
                        if (StopAdvances || RaidFilters.Any(z => z.FilterSatisfied(Raids[chk], Progress.SelectedIndex, EventProgress.SelectedIndex)))
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
                        if (StopAdvances || RaidFilters.Any(z => z.FilterSatisfied(Raids[chk], Progress.SelectedIndex, EventProgress.SelectedIndex)))
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

        private static async Task<string> GetGameID(CancellationToken token) => await SwitchConnection.GetTitleID(token).ConfigureAwait(false);
        private bool StopAdvances => RaidFilters.Count == 0 || RaidFilters.All(x => x.Enabled == false);

        private async Task AdvanceDate(CancellationToken token)
        {
            ConnectionStatusText.Text = "Changing date...";
            int BaseDelay = (int)Settings.Default.CfgBaseDelay;
            await Click(LSTICK, 0_050 + BaseDelay, token).ConfigureAwait(false); // Sometimes it seems like the first command doesn't go through so send this just in case
            // HOME Menu
            await Click(HOME, (int)Settings.Default.CfgOpenHome + BaseDelay, token).ConfigureAwait(false);
            await Click(DDOWN, (int)Settings.Default.CfgNavigateToSettings + 0_100 + BaseDelay, token).ConfigureAwait(false);
            for (int i = 0; i < 5; i++) await Click(DRIGHT, (int)Settings.Default.CfgNavigateToSettings + BaseDelay, token).ConfigureAwait(false);
            await Click(A, (int)Settings.Default.CfgOpenSettings + BaseDelay, token).ConfigureAwait(false);
            // Scroll to bottom
            await PressAndHold(DDOWN, (int)Settings.Default.CfgHold, BaseDelay, token).ConfigureAwait(false);
            // Navigate to "Date and Time"
            await Click(DRIGHT, 0_200 + BaseDelay, token).ConfigureAwait(false);
            for (int i = 0; i < Settings.Default.CfgSystemDDownPresses; i++) await Click(DDOWN, 0_050 + BaseDelay, token).ConfigureAwait(false);
            await Click(A, (int)Settings.Default.CfgSubmenu + BaseDelay, token).ConfigureAwait(false);
            // Navigate to Change Date/Time
            for (int i = 0; i < 2; i++) await Click(DDOWN, 0_200 + BaseDelay, token).ConfigureAwait(false);
            await Click(A, (int)Settings.Default.CfgDateChange + BaseDelay, token).ConfigureAwait(false);
            // Change the date
            for (int i = 0; i < Settings.Default.CfgDaysToSkip; i++) await Click(DUP, 0_200 + BaseDelay, token).ConfigureAwait(false); // Not actually necessary, so we default to 0 as per #29
            for (int i = 0; i < 6; i++) await Click(DRIGHT, 0_100 + BaseDelay, token).ConfigureAwait(false);
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
                do
                {
                    await AdvanceDate(CancellationToken.None);
                    await ReadRaids(CancellationToken.None);
                } while (!(StopAdvances || RaidFilters.Any(z => z.FilterSatisfied(Raids, Progress.SelectedIndex, EventProgress.SelectedIndex))));
                if (Settings.Default.CfgPlaySound) System.Media.SystemSounds.Asterisk.Play();
                if (Settings.Default.CfgFocusWindow)
                {
                    WindowState = _WindowState;
                    Activate();
                }
                if (Settings.Default.CfgEnableAlertWindow) MessageBox.Show(Settings.Default.CfgAlertWindowMessage, "Result found!", MessageBoxButtons.OK);

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
            offset = await OffsetUtil.GetPointerAddress(RaidBlockPointer, CancellationToken.None);

            Raids.Clear();
            index = 0;

            ConnectionStatusText.Text = "Reading raid block...";
            Raid raid;
            for (uint i = RaidBlock.HEADER_SIZE; i < RaidBlock.SIZE; i += Raid.SIZE)
            {
                ConnectionStatusText.Text = $"Reading raid block... {(int)((float)((float)i / RaidBlock.SIZE) * 100)}%";
                var Data = await SwitchConnection.ReadBytesAbsoluteAsync(offset + i, Raid.SIZE, token);
                raid = new Raid(Data);
                if (raid.IsValid) Raids.Add(raid);
            }

            ConnectionStatusText.Text = "Completed!";
            LabelLoadedRaids.Text = $"Raids: {Raids.Count} | Shiny: {Raids.Where(raid => raid.IsShiny).Count()}";
            if (Raids.Count > 0)
            {
                ButtonPrevious.Enabled = true;
                ButtonNext.Enabled = true;
                DisplayRaid(index);
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
            var form = new FilterSettings(ref RaidFilters);
            form.ShowDialog();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }

        private void ButtonDumpRaid_Click(object sender, EventArgs e)
        {
            if (Raids[index] != null)
            {
                RaidBlockViewer BlockViewerWindow = new(Raids[index].Data, offset);
                BlockViewerWindow.ShowDialog();
            }
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
    }
}