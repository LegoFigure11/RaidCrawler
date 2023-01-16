﻿using Newtonsoft.Json;
using NLog.Filters;
using RaidCrawler.Properties;

namespace RaidCrawler.Subforms
{
    public partial class ConfigWindow : Form
    {
        private readonly Structures.Config c = new();
        public ConfigWindow(Structures.Config c)
        {
            InitializeComponent();

            this.c = c;

            InstanceName.Text = c.InstanceName;

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

            IVstyle.SelectedIndex = c.IVsStyle;
            IVspacer.Text = c.IVsSpacer;
            IVverbose.Checked = c.VerboseIVs;

            denToggle.Checked = c.ToggleDen;

            EnableEmoji.Checked = c.EnableEmoji;

            ExperimentalView.Checked = c.StreamerView;
        }

        private void EnableAlert_CheckedChanged(object sender, EventArgs e)
        {
            AlertMessage.Enabled = EnableAlert.Checked;
        }

        private void EnableDiscordNotifications_CheckedChanged(object sender, EventArgs e)
        {
            DiscordWebhook.Enabled = EnableDiscordNotifications.Checked;
            DiscordMessageContent.Enabled = EnableDiscordNotifications.Checked;
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

            string output = JsonConvert.SerializeObject(c);
            using StreamWriter sw = new(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json"));
            sw.Write(output);

            Close();
        }
    }
}
