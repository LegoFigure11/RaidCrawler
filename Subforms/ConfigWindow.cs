using RaidCrawler.Properties;

namespace RaidCrawler.Subforms
{
    public partial class ConfigWindow : Form
    {
        public ConfigWindow()
        {
            InitializeComponent();

            PlayTone.Checked = Settings.Default.CfgPlaySound;
            FocusWindow.Checked = Settings.Default.CfgFocusWindow;
            EnableAlert.Checked = Settings.Default.CfgEnableAlertWindow;
            AlertMessage.Text = Settings.Default.CfgAlertWindowMessage;
            AlertMessage.Enabled = EnableAlert.Checked;

            BaseDelay.Value = Settings.Default.CfgBaseDelay;
            SystemDDownPresses.Value = Settings.Default.CfgSystemDDownPresses;
        }

        private void EnableAlert_CheckedChanged(object sender, EventArgs e)
        {
            AlertMessage.Enabled = EnableAlert.Checked;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            Settings.Default.CfgPlaySound = PlayTone.Checked;
            Settings.Default.CfgFocusWindow = FocusWindow.Checked;
            Settings.Default.CfgEnableAlertWindow = EnableAlert.Checked;
            Settings.Default.CfgAlertWindowMessage = AlertMessage.Text;

            Settings.Default.CfgBaseDelay = BaseDelay.Value;
            Settings.Default.CfgSystemDDownPresses = SystemDDownPresses.Value;

            Settings.Default.Save();

            Close();
        }
    }
}
