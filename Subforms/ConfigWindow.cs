using Newtonsoft.Json;
using RaidCrawler.Structures;

namespace RaidCrawler.Subforms
{
    public partial class ConfigWindow : Form
    {
        private readonly Config c = new();


        public ConfigWindow(Config c)
        {
            var mainForm = Application.OpenForms.OfType<MainWindow>().Single();
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
            BaseDelay.Value = c.BaseDelay;
            OpenHome.Value = c.OpenHome;
            NavigateToSettings.Value = c.NavigateToSettings;
            OpenSettings.Value = c.OpenSettings;
            Hold.Value = c.Hold;
            SystemDDownPresses.Value = c.SystemDDownPresses;
            SystemOvershoot.Value = c.SystemOvershoot;
            Submenu.Value = c.Submenu;
            DateChange.Value = c.DateChange;
            DaysToSkip.Value = c.DaysToSkip;
            ReturnHome.Value = c.ReturnHome;
            ReturnGame.Value = c.ReturnGame;

            SystemDDownPresses.Enabled = !UseOvershoot.Checked;
            SystemOvershoot.Enabled = UseOvershoot.Checked;

            IVstyle.SelectedIndex = c.IVsStyle;
            IVspacer.Text = c.IVsSpacer;
            IVverbose.Checked = c.VerboseIVs;

            denToggle.Checked = c.ToggleDen;

            EnableEmoji.Checked = c.EnableEmoji;

            ExperimentalView.Checked = c.StreamerView;

            labelAppVersion.Text = "v" + v.Major + "." + v.Minor + "." + v.Build + "-" + shaField?.GetValue(null);
            labelAppVersion.Left = (tabAbout.Width - labelAppVersion.Width) / 2;
            labelAppName.Left = ((tabAbout.Width - labelAppName.Width) / 2) + (picAppIcon.Width / 2) + 2;
            picAppIcon.Left = labelAppName.Left - picAppIcon.Width - 2;
            linkLabel1.Left = (tabAbout.Width - linkLabel1.Width) / 2;

            labelDaySkip.Text = "Day Skip Successes " + mainForm.GetStatDaySkipSuccess() + " / " + mainForm.GetStatDaySkipTries() + " Total";
        }

        private void EnableAlert_CheckedChanged(object sender, EventArgs e)
        {
            AlertMessage.Enabled = EnableAlert.Checked;
        }

        private void EnableDiscordNotifications_CheckedChanged(object sender, EventArgs e)
        {
            DiscordWebhook.Enabled = EnableDiscordNotifications.Checked;
            DiscordMessageContent.Enabled = EnableDiscordNotifications.Checked;
            labelWebhooks.Text = "Webhooks are " + (DiscordWebhook.Enabled ? "enabled." : "disabled.");
        }

        private void Save_Click(object sender, EventArgs e)
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
            c.BaseDelay = BaseDelay.Value;
            c.OpenHome = OpenHome.Value;
            c.NavigateToSettings = NavigateToSettings.Value;
            c.OpenSettings = OpenSettings.Value;
            c.Hold = Hold.Value;
            c.SystemDDownPresses = SystemDDownPresses.Value;
            c.SystemOvershoot = SystemOvershoot.Value;
            c.Submenu = Submenu.Value;
            c.DateChange = DateChange.Value;
            c.DaysToSkip = DaysToSkip.Value;
            c.ReturnHome = ReturnHome.Value;
            c.ReturnGame = ReturnGame.Value;

            c.IVsStyle = IVstyle.SelectedIndex;
            c.IVsSpacer = IVspacer.Text;
            c.VerboseIVs = IVverbose.Checked;

            c.EnableEmoji = EnableEmoji.Checked;

            c.ToggleDen = denToggle.Checked;
            c.StreamerView = ExperimentalView.Checked;

            c.Protocol = (SysBot.Base.SwitchProtocol)Protocol_dropdown.SelectedIndex;

            string output = JsonConvert.SerializeObject(c);
            using StreamWriter sw = new(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json"));
            sw.Write(output);

            Close();
        }

        private void UseOvershoot_CheckedChanged(object sender, EventArgs e)
        {
            SystemDDownPresses.Enabled = !UseOvershoot.Checked;
            SystemOvershoot.Enabled = UseOvershoot.Checked;
        }

        private void BtnTestWebHook_Click(object sender, EventArgs e)
        {
            c.InstanceName = InstanceName.Text;
            c.DiscordMessageContent = DiscordMessageContent.Text;
            c.IVsStyle = IVstyle.SelectedIndex;
            c.IVsSpacer = IVspacer.Text;
            c.VerboseIVs = IVverbose.Checked;
            c.EnableEmoji = EnableEmoji.Checked;
            c.ToggleDen = denToggle.Checked;

            var mainForm = Application.OpenForms.OfType<MainWindow>().Single();
            mainForm.TestWebhook();
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(((LinkLabel)sender).Text) { UseShellExecute = true });
        }

        private void Game_SelectedIndexChanged(object sender, EventArgs e)
        {
            var mainForm = Application.OpenForms.OfType<MainWindow>().Single();
            mainForm.Game_SelectedIndexChanged();
        }

        private void EmojiConfig_Click(object sender, EventArgs e)
        {
            EmojiConfig config = new(c);
            if (config.ShowDialog() == DialogResult.OK)
            {
                config.Show();
            }
        }

        private void Protocol_Changed(object sender, EventArgs e)
        {
            c.Protocol = (SysBot.Base.SwitchProtocol)Protocol_dropdown.SelectedIndex;
            var mainForm = Application.OpenForms.OfType<MainWindow>().Single();
            mainForm.Protocol_SelectedIndexChanged(c.Protocol);
        }
    }
}
