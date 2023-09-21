using System.Text.Json;

namespace RaidCrawler.WinForms.SubForms
{
    public partial class ConfigWindow : Form
    {
        private readonly ClientConfig c = new();

        public ConfigWindow(ClientConfig c)
        {
            var assembly = System.Reflection.Assembly.GetEntryAssembly();
            var v = assembly?.GetName().Version!;
            var gitVersionInformationType = assembly?.GetType("GitVersionInformation");
            var shaField = gitVersionInformationType?.GetField("ShortSha");

            InitializeComponent();

            this.c = c;

            InstanceName.Text = c.InstanceName;
            StoryProgress.SelectedIndex = c.Progress;
            EventProgress.SelectedIndex = c.EventProgress;
            Game.SelectedIndex = Game.FindString(c.Game);
            Protocol_dropdown.SelectedIndex = (int)c.Protocol;

            PlayTone.Checked = c.PlaySound;
            FocusWindow.Checked = c.FocusWindow;
            EnableAlert.Checked = c.EnableAlertWindow;
            AlertMessage.Text = c.AlertWindowMessage;
            AlertMessage.Enabled = EnableAlert.Checked;
            EnableDiscordNotifications.Checked = c.EnableNotification;
            DiscordWebhook.Text = c.DiscordWebhook;
            DiscordWebhook.Enabled = EnableDiscordNotifications.Checked;
            DiscordMessageContent.Text = c.DiscordMessageContent;
            DiscordMessageContent.Enabled = EnableDiscordNotifications.Checked;

            UseTouch.Checked = c.UseTouch;
            UseOvershoot.Checked = c.UseOvershoot;
            SaveGame.Checked = c.SaveOnMatch;
            DodgeSystemUpdate.Checked = c.DodgeSystemUpdate;
            UseSetStick.Checked = c.UseSetStick;

            OpenHome.Value = c.OpenHomeDelay;
            NavigateToSettings.Value = c.NavigateToSettingsDelay;
            OpenSettings.Value = c.OpenSettingsDelay;
            Hold.Value = c.HoldDuration;
            SystemDDownPresses.Value = c.SystemDownPresses;
            SystemOvershoot.Value = c.SystemOvershoot;
            Submenu.Value = c.Submenu;
            DateChange.Value = c.DateChange;
            DaysToSkip.Value = c.DaysToSkip;
            ReturnHome.Value = c.ReturnHomeDelay;
            ReturnGame.Value = c.ReturnGameDelay;
            BaseDelay.Value = c.BaseDelay;
            SaveGameDelay.Value = c.SaveGameDelay;
            SystemReset.Value = c.SystemReset;
            PaldeaScanCheck.Checked = c.PaldeaScan;
            KitakamiScanCheck.Checked = c.KitakamiScan;

            SystemDDownPresses.Enabled = !UseOvershoot.Checked;
            SystemOvershoot.Enabled = UseOvershoot.Checked;
            SaveGameDelay.Enabled = SaveGame.Checked;

            IVstyle.SelectedIndex = c.IVsStyle;
            IVverbose.Checked = c.VerboseIVs;

            denToggle.Checked = c.ToggleDen;

            EnableEmoji.Checked = c.EnableEmoji;

            ExperimentalView.Checked = c.StreamerView;

            labelAppVersion.Text =
                "v" + v.Major + "." + v.Minor + "." + v.Build + "-" + shaField?.GetValue(null);
            labelAppVersion.Left = (tabAbout.Width - labelAppVersion.Width) / 2;
            labelAppName.Left =
                ((tabAbout.Width - labelAppName.Width) / 2) + (picAppIcon.Width / 2) + 2;
            picAppIcon.Left = labelAppName.Left - picAppIcon.Width - 2;
            linkLabel1.Left = (tabAbout.Width - linkLabel1.Width) / 2;

            labelWebhooks.Text =
                "Webhooks are " + (DiscordWebhook.Enabled ? "enabled." : "disabled.");
        }

        private void EnableAlert_CheckedChanged(object sender, EventArgs e)
        {
            AlertMessage.Enabled = EnableAlert.Checked;
        }

        private void EnableDiscordNotifications_Click(object sender, EventArgs e)
        {
            DiscordWebhook.Enabled = EnableDiscordNotifications.Checked;
            DiscordMessageContent.Enabled = EnableDiscordNotifications.Checked;
            labelWebhooks.Text =
                "Webhooks are " + (DiscordWebhook.Enabled ? "enabled." : "disabled.");
        }

        private void Config_Closing(object sender, EventArgs e)
        {
            c.InstanceName = InstanceName.Text;

            c.PlaySound = PlayTone.Checked;
            c.FocusWindow = FocusWindow.Checked;
            c.EnableAlertWindow = EnableAlert.Checked;
            c.AlertWindowMessage = AlertMessage.Text;
            c.EnableNotification = EnableDiscordNotifications.Checked;
            c.DiscordWebhook = DiscordWebhook.Text;
            c.DiscordMessageContent = DiscordMessageContent.Text;

            c.UseTouch = UseTouch.Checked;
            c.UseOvershoot = UseOvershoot.Checked;
            c.SaveOnMatch = SaveGame.Checked;
            c.DodgeSystemUpdate = DodgeSystemUpdate.Checked;
            c.UseSetStick = UseSetStick.Checked;

            c.OpenHomeDelay = (int)OpenHome.Value;
            c.NavigateToSettingsDelay = (int)NavigateToSettings.Value;
            c.OpenSettingsDelay = (int)OpenSettings.Value;
            c.HoldDuration = (int)Hold.Value;
            c.SystemDownPresses = (int)SystemDDownPresses.Value;
            c.SystemOvershoot = (int)SystemOvershoot.Value;
            c.SystemReset = (int)SystemReset.Value;
            c.Submenu = (int)Submenu.Value;
            c.DateChange = (int)DateChange.Value;
            c.DaysToSkip = (int)DaysToSkip.Value;
            c.ReturnHomeDelay = (int)ReturnHome.Value;
            c.ReturnGameDelay = (int)ReturnGame.Value;
            c.BaseDelay = (int)BaseDelay.Value;
            c.SaveGameDelay = (int)SaveGameDelay.Value;
            c.PaldeaScan = PaldeaScanCheck.Checked;
            c.KitakamiScan = KitakamiScanCheck.Checked;

            c.IVsStyle = IVstyle.SelectedIndex;
            c.VerboseIVs = IVverbose.Checked;

            c.EnableEmoji = EnableEmoji.Checked;

            c.ToggleDen = denToggle.Checked;
            c.StreamerView = ExperimentalView.Checked;

            c.Protocol = (SysBot.Base.SwitchProtocol)Protocol_dropdown.SelectedIndex;

            JsonSerializerOptions options = new() { WriteIndented = true };
            string output = JsonSerializer.Serialize(c, options);
            using StreamWriter sw =
                new(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json"));
            sw.Write(output);
        }

        private void UseOvershoot_CheckedChanged(object sender, EventArgs e)
        {
            SystemDDownPresses.Enabled = !UseOvershoot.Checked;
            SystemOvershoot.Enabled = UseOvershoot.Checked;
        }

        private void UseSetStick_CheckedChanged(Object sender, EventArgs e)
        {
            UseSetStick.Enabled = !UseSetStick.Checked;
        }

        private void SaveGame_CheckedChanged(object sender, EventArgs e)
        {
            SaveGameDelay.Enabled = SaveGame.Checked;
        }

        private void BtnTestWebHook_Click(object sender, EventArgs e)
        {
            c.InstanceName = InstanceName.Text;
            c.DiscordMessageContent = DiscordMessageContent.Text;
            c.IVsStyle = IVstyle.SelectedIndex;
            c.VerboseIVs = IVverbose.Checked;
            c.EnableEmoji = EnableEmoji.Checked;
            c.ToggleDen = denToggle.Checked;

            var mainForm = Application.OpenForms.OfType<MainWindow>().Single();
            mainForm.TestWebhook();
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(
                new System.Diagnostics.ProcessStartInfo(((LinkLabel)sender).Text)
                {
                    UseShellExecute = true
                }
            );
        }

        private void Game_SelectedIndexChanged(object sender, EventArgs e)
        {
            var game = (string)Game.SelectedItem;
            var mainForm = Application.OpenForms.OfType<MainWindow>().Single();
            mainForm.Game_SelectedIndexChanged(game);
        }

        private void EmojiConfig_Click(object sender, EventArgs e)
        {
            EmojiConfig config = new(c);
            if (config.ShowDialog() == DialogResult.OK)
                config.Show();
        }

        private void Protocol_Changed(object sender, EventArgs e)
        {
            c.Protocol = (SysBot.Base.SwitchProtocol)Protocol_dropdown.SelectedIndex;
            var mainForm = Application.OpenForms.OfType<MainWindow>().Single();
            mainForm.Protocol_SelectedIndexChanged(c.Protocol);
        }

        private void StreamerView_Clicked(object sender, EventArgs e)
        {
            c.StreamerView = ExperimentalView.Checked;
            var mainForm = Application.OpenForms.OfType<MainWindow>().Single();
            mainForm.ToggleStreamerView();
        }
    }
}
