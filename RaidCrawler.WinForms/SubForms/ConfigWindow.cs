using System.Text.Json;

namespace RaidCrawler.WinForms.SubForms;

public partial class ConfigWindow : Form
{
    private readonly ClientConfig c;

    public ConfigWindow(ClientConfig c)
    {
        this.c = c;
        var assembly = System.Reflection.Assembly.GetEntryAssembly();
        var v = assembly?.GetName().Version!;
        var gitVersionInformationType = assembly?.GetType("GitVersionInformation");
        var shaField = gitVersionInformationType?.GetField("ShortSha");

        InitializeComponent();
        ThemeComboBox.SelectedItem = c.Theme;
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

        ZyroMethod.Checked = c.ZyroMethod;

        UseTouch.Checked = c.UseTouch;
        UseOvershoot.Checked = c.UseOvershoot;
        SaveGame.Checked = c.SaveOnMatch;
        DodgeSystemUpdate.Checked = c.DodgeSystemUpdate;
        UseSetStick.Checked = c.UseSetStick;
        UseMapTrick.Checked = c.UseMapTrick;

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
        RelaunchDelay.Value = c.RelaunchDelay;
        ExtraOverworldWait.Value = c.ExtraOverworldWait;
        PaldeaScanCheck.Checked = c.PaldeaScan;
        KitakamiScanCheck.Checked = c.KitakamiScan;
        BlueberryScanCheck.Checked = c.BlueberryScan;

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
        ApplyTheme(c.Theme);
    }

    private void ApplyTheme(string theme)
    {
        if (theme == "Dark")
            SetDarkTheme();
        else
            SetLightTheme();
    }

    private void ThemeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Null check for safety
        if (this.ThemeComboBox.SelectedItem == null) return;

        string selectedTheme = this.ThemeComboBox.SelectedItem.ToString();
        if (selectedTheme == "Dark")
        {
            this.SetDarkTheme();
        }
        else
        {
            this.SetLightTheme();
        }
        // Save the selected theme to the configuration
        this.c.Theme = ThemeComboBox.SelectedItem.ToString();
        SaveConfig();
    }

    private void SaveConfig()
    {
        var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
        File.WriteAllText(configPath, JsonSerializer.Serialize(this.c));
    }

    private void SetDarkTheme()
    {
        // backgroundColor: This is the primary background color applied to the main areas of the form or application window.
        // It is the color that most of the background elements of the UI will use.
        Color backgroundColor = Color.FromArgb(85, 85, 85);

        // textColor: This is the color applied to the text across the application. It is meant to contrast well
        // against the background colors for readability.
        Color textColor = Color.FromArgb(255, 255, 255);

        // controlBackgroundColor: This color is used as the background for various controls such as Panels or GroupBoxes.
        // It can be the same as the general background color or slightly different to create a layered visual effect.
        Color controlBackgroundColor = Color.FromArgb(85, 85, 85);

        // controlDarkColor: This color is a darker shade used for certain UI controls that may need to be differentiated from
        // other elements, often used for input fields like TextBoxes or ComboBoxes to indicate interactivity.
        Color controlDarkColor = Color.FromArgb(75, 75, 75);

        // borderColor: Used for the borders of controls that require it, such as buttons or text boxes. This color
        // is meant to be subtle yet visible enough to define the boundaries of interactive elements.
        Color borderColor = Color.FromArgb(100, 100, 102);

        // buttonHighlightColor: This is a highlight color used to indicate interactive UI elements such as buttons when they
        // are hovered over or focused. It provides a visual cue to the user that the element is interactive.
        Color buttonHighlightColor = Color.FromArgb(139, 0, 0);


        this.BackColor = backgroundColor;
        this.ForeColor = textColor;

        // Apply to all controls on the form
        foreach (Control ctrl in this.Controls)
        {
            ApplyDarkTheme(ctrl, backgroundColor, textColor, controlBackgroundColor, controlDarkColor, borderColor, buttonHighlightColor);
        }
    }

    private void ApplyDarkTheme(Control control, Color backgroundColor, Color textColor, Color controlBackgroundColor, Color controlDarkColor, Color borderColor, Color buttonHighlightColor)
    {
        control.BackColor = controlBackgroundColor;
        control.ForeColor = textColor;

        if (control is Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = controlDarkColor;
            btn.ForeColor = textColor;
            btn.FlatAppearance.BorderColor = borderColor;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.MouseOverBackColor = buttonHighlightColor;
        }
        else if (control is TextBox txtBox)
        {
            txtBox.BorderStyle = BorderStyle.FixedSingle;
            txtBox.BackColor = controlDarkColor;
            txtBox.ForeColor = textColor;
        }
        else if (control is ComboBox cmbBox)
        {
            cmbBox.FlatStyle = FlatStyle.Flat;
            cmbBox.BackColor = controlDarkColor;
            cmbBox.ForeColor = textColor;
            // Note: The ComboBox drop-down list part cannot be styled this way.
        }
        else if (control is Label lbl)
        {
            lbl.ForeColor = textColor;
        }
        else if (control is GroupBox grpBox)
        {
            grpBox.ForeColor = textColor;
            grpBox.BackColor = backgroundColor;
            // Note: GroupBox doesn't support border color change directly.
        }
        else if (control is Panel pnl)
        {
            pnl.BackColor = controlDarkColor;
        }
        else if (control is DataGridView dgv)
        {
            dgv.BackgroundColor = controlDarkColor;
            dgv.ForeColor = textColor;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = controlBackgroundColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = textColor;
            dgv.DefaultCellStyle.BackColor = controlDarkColor;
            dgv.DefaultCellStyle.ForeColor = textColor;
            dgv.EnableHeadersVisualStyles = false;
        }

        // Apply theme to child controls (recursive call)
        foreach (Control childControl in control.Controls)
        {
            ApplyDarkTheme(childControl, backgroundColor, textColor, controlBackgroundColor, controlDarkColor, borderColor, buttonHighlightColor);
        }
    }

    private void SetLightTheme()
    {
        Color backgroundColor = SystemColors.Control;
        Color textColor = SystemColors.ControlText;
        Color controlColor = SystemColors.Window;
        Color borderColor = SystemColors.ActiveBorder;

        this.BackColor = backgroundColor;
        this.ForeColor = textColor;

        // Apply to all controls on the form
        foreach (Control ctrl in this.Controls)
        {
            ApplyLightTheme(ctrl, backgroundColor, textColor, controlColor, borderColor);
        }
    }

    private void ApplyLightTheme(Control control, Color backgroundColor, Color textColor, Color controlColor, Color borderColor)
    {
        control.BackColor = controlColor;
        control.ForeColor = textColor;

        if (control is Button btn)
        {
            btn.FlatStyle = FlatStyle.Standard;
            btn.FlatAppearance.BorderColor = borderColor;
        }
        else if (control is TextBox txtBox)
        {
            txtBox.BorderStyle = BorderStyle.Fixed3D;
            txtBox.BackColor = backgroundColor;
            txtBox.ForeColor = textColor;
        }
        // Add similar conditions for other control types like ComboBox, Label, etc.

        foreach (Control childControl in control.Controls)
        {
            ApplyLightTheme(childControl, backgroundColor, textColor, controlColor, borderColor);
        }
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

        c.ZyroMethod = ZyroMethod.Checked;

        c.UseTouch = UseTouch.Checked;
        c.UseOvershoot = UseOvershoot.Checked;
        c.SaveOnMatch = SaveGame.Checked;
        c.DodgeSystemUpdate = DodgeSystemUpdate.Checked;
        c.UseSetStick = UseSetStick.Checked;
        c.UseMapTrick = UseMapTrick.Checked;

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
        c.RelaunchDelay = (int)RelaunchDelay.Value;
        c.ExtraOverworldWait = (int)ExtraOverworldWait.Value;
        c.PaldeaScan = PaldeaScanCheck.Checked;
        c.KitakamiScan = KitakamiScanCheck.Checked;
        c.BlueberryScan = BlueberryScanCheck.Checked;

        c.IVsStyle = IVstyle.SelectedIndex;
        c.VerboseIVs = IVverbose.Checked;

        c.EnableEmoji = EnableEmoji.Checked;

        c.ToggleDen = denToggle.Checked;
        c.StreamerView = ExperimentalView.Checked;

        c.Protocol = (SysBot.Base.SwitchProtocol)Protocol_dropdown.SelectedIndex;

        string output = JsonSerializer.Serialize(c, Options);
        using StreamWriter sw =
            new(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json"));
        sw.Write(output);
    }

    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

    private void UseOvershoot_CheckedChanged(object sender, EventArgs e)
    {
        SystemDDownPresses.Enabled = !UseOvershoot.Checked;
        SystemOvershoot.Enabled = UseOvershoot.Checked;
    }

    private void UseSetStick_CheckedChanged(object sender, EventArgs e)
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
                UseShellExecute = true,
            }
        );
    }

    private void Game_SelectedIndexChanged(object sender, EventArgs e)
    {
        var game = (string)Game.SelectedItem!;
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
