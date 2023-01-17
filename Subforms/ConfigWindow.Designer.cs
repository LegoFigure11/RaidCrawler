namespace RaidCrawler.Subforms
{
    partial class ConfigWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FocusWindow = new System.Windows.Forms.CheckBox();
            this.EnableAlert = new System.Windows.Forms.CheckBox();
            this.PlayTone = new System.Windows.Forms.CheckBox();
            this.LabelMatchFound = new System.Windows.Forms.Label();
            this.AlertMessage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BaseDelay = new System.Windows.Forms.NumericUpDown();
            this.SystemDDownPresses = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.Save = new System.Windows.Forms.Button();
            this.NavigateToSettings = new System.Windows.Forms.NumericUpDown();
            this.OpenSettings = new System.Windows.Forms.NumericUpDown();
            this.OpenHome = new System.Windows.Forms.NumericUpDown();
            this.LabelDelayOpenHOME = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Hold = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.Submenu = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.DateChange = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.ReturnHome = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.ReturnGame = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.DaysToSkip = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.UseTouch = new System.Windows.Forms.CheckBox();
            this.DiscordWebhook = new System.Windows.Forms.TextBox();
            this.EnableDiscordNotifications = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.ExperimentalView = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label16 = new System.Windows.Forms.Label();
            this.SystemOvershoot = new System.Windows.Forms.NumericUpDown();
            this.UseOvershoot = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label21 = new System.Windows.Forms.Label();
            this.DiscordMessageContent = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnTestWebHook = new System.Windows.Forms.Button();
            this.denToggle = new System.Windows.Forms.CheckBox();
            this.label22 = new System.Windows.Forms.Label();
            this.IVstyle = new System.Windows.Forms.ComboBox();
            this.IVspacer = new System.Windows.Forms.TextBox();
            this.IVverbose = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.EnableEmoji = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.InstanceName = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.labelWebhooks = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.BaseDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SystemDDownPresses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NavigateToSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OpenSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OpenHome)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Submenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateChange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReturnHome)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReturnGame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DaysToSkip)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SystemOvershoot)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // FocusWindow
            // 
            this.FocusWindow.AutoSize = true;
            this.FocusWindow.Location = new System.Drawing.Point(6, 46);
            this.FocusWindow.Name = "FocusWindow";
            this.FocusWindow.Size = new System.Drawing.Size(123, 19);
            this.FocusWindow.TabIndex = 1;
            this.FocusWindow.Text = "Focus RaidCrawler";
            this.FocusWindow.UseVisualStyleBackColor = true;
            // 
            // EnableAlert
            // 
            this.EnableAlert.AutoSize = true;
            this.EnableAlert.Location = new System.Drawing.Point(6, 71);
            this.EnableAlert.Name = "EnableAlert";
            this.EnableAlert.Size = new System.Drawing.Size(293, 19);
            this.EnableAlert.TabIndex = 2;
            this.EnableAlert.Text = "Show an alert window with the following message:";
            this.EnableAlert.UseVisualStyleBackColor = true;
            this.EnableAlert.CheckedChanged += new System.EventHandler(this.EnableAlert_CheckedChanged);
            // 
            // PlayTone
            // 
            this.PlayTone.AutoSize = true;
            this.PlayTone.Location = new System.Drawing.Point(6, 21);
            this.PlayTone.Name = "PlayTone";
            this.PlayTone.Size = new System.Drawing.Size(84, 19);
            this.PlayTone.TabIndex = 0;
            this.PlayTone.Text = "Play a tone";
            this.PlayTone.UseVisualStyleBackColor = true;
            // 
            // LabelMatchFound
            // 
            this.LabelMatchFound.AutoSize = true;
            this.LabelMatchFound.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LabelMatchFound.Location = new System.Drawing.Point(6, 3);
            this.LabelMatchFound.Name = "LabelMatchFound";
            this.LabelMatchFound.Size = new System.Drawing.Size(137, 15);
            this.LabelMatchFound.TabIndex = 3;
            this.LabelMatchFound.Text = "When a match is found:";
            // 
            // AlertMessage
            // 
            this.AlertMessage.Location = new System.Drawing.Point(6, 96);
            this.AlertMessage.Name = "AlertMessage";
            this.AlertMessage.Size = new System.Drawing.Size(327, 23);
            this.AlertMessage.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(6, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Advance Date Options (all timings in ms):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Base delay to be added to all inputs:";
            // 
            // BaseDelay
            // 
            this.BaseDelay.Location = new System.Drawing.Point(265, 68);
            this.BaseDelay.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.BaseDelay.Name = "BaseDelay";
            this.BaseDelay.Size = new System.Drawing.Size(68, 23);
            this.BaseDelay.TabIndex = 9;
            // 
            // SystemDDownPresses
            // 
            this.SystemDDownPresses.Location = new System.Drawing.Point(265, 213);
            this.SystemDDownPresses.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.SystemDDownPresses.Name = "SystemDDownPresses";
            this.SystemDDownPresses.Size = new System.Drawing.Size(68, 23);
            this.SystemDDownPresses.TabIndex = 10;
            this.SystemDDownPresses.Value = new decimal(new int[] {
            38,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(228, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "DDOWN inputs to get to \"Date and Time\":";
            // 
            // Save
            // 
            this.Save.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Save.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Save.Location = new System.Drawing.Point(0, 444);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(349, 32);
            this.Save.TabIndex = 12;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // NavigateToSettings
            // 
            this.NavigateToSettings.Location = new System.Drawing.Point(265, 126);
            this.NavigateToSettings.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.NavigateToSettings.Name = "NavigateToSettings";
            this.NavigateToSettings.Size = new System.Drawing.Size(68, 23);
            this.NavigateToSettings.TabIndex = 16;
            this.NavigateToSettings.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // OpenSettings
            // 
            this.OpenSettings.Location = new System.Drawing.Point(265, 155);
            this.OpenSettings.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.OpenSettings.Name = "OpenSettings";
            this.OpenSettings.Size = new System.Drawing.Size(68, 23);
            this.OpenSettings.TabIndex = 17;
            this.OpenSettings.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // OpenHome
            // 
            this.OpenHome.Location = new System.Drawing.Point(265, 97);
            this.OpenHome.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.OpenHome.Name = "OpenHome";
            this.OpenHome.Size = new System.Drawing.Size(68, 23);
            this.OpenHome.TabIndex = 18;
            this.OpenHome.Value = new decimal(new int[] {
            1800,
            0,
            0,
            0});
            // 
            // LabelDelayOpenHOME
            // 
            this.LabelDelayOpenHOME.AutoSize = true;
            this.LabelDelayOpenHOME.Location = new System.Drawing.Point(6, 99);
            this.LabelDelayOpenHOME.Name = "LabelDelayOpenHOME";
            this.LabelDelayOpenHOME.Size = new System.Drawing.Size(140, 15);
            this.LabelDelayOpenHOME.TabIndex = 19;
            this.LabelDelayOpenHOME.Text = "Open Home Menu delay:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 15);
            this.label4.TabIndex = 20;
            this.label4.Text = "Navigate to settings delay:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 15);
            this.label5.TabIndex = 21;
            this.label5.Text = "Open settings delay:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 186);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(187, 15);
            this.label6.TabIndex = 23;
            this.label6.Text = "Time to hold to scroll to \"System\":";
            // 
            // Hold
            // 
            this.Hold.Location = new System.Drawing.Point(265, 184);
            this.Hold.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Hold.Name = "Hold";
            this.Hold.Size = new System.Drawing.Size(68, 23);
            this.Hold.TabIndex = 22;
            this.Hold.Value = new decimal(new int[] {
            1700,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 244);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 15);
            this.label7.TabIndex = 25;
            this.label7.Text = "Open submenu delay:";
            // 
            // Submenu
            // 
            this.Submenu.Location = new System.Drawing.Point(265, 242);
            this.Submenu.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Submenu.Name = "Submenu";
            this.Submenu.Size = new System.Drawing.Size(68, 23);
            this.Submenu.TabIndex = 24;
            this.Submenu.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 273);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(138, 15);
            this.label8.TabIndex = 27;
            this.label8.Text = "Open date change delay:";
            // 
            // DateChange
            // 
            this.DateChange.Location = new System.Drawing.Point(265, 271);
            this.DateChange.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.DateChange.Name = "DateChange";
            this.DateChange.Size = new System.Drawing.Size(68, 23);
            this.DateChange.TabIndex = 26;
            this.DateChange.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 331);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(160, 15);
            this.label9.TabIndex = 29;
            this.label9.Text = "Return to Home Menu delay:";
            // 
            // ReturnHome
            // 
            this.ReturnHome.Location = new System.Drawing.Point(265, 329);
            this.ReturnHome.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.ReturnHome.Name = "ReturnHome";
            this.ReturnHome.Size = new System.Drawing.Size(68, 23);
            this.ReturnHome.TabIndex = 28;
            this.ReturnHome.Value = new decimal(new int[] {
            2500,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 360);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(119, 15);
            this.label10.TabIndex = 31;
            this.label10.Text = "Re-open game delay:";
            // 
            // ReturnGame
            // 
            this.ReturnGame.Location = new System.Drawing.Point(265, 358);
            this.ReturnGame.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.ReturnGame.Name = "ReturnGame";
            this.ReturnGame.Size = new System.Drawing.Size(68, 23);
            this.ReturnGame.TabIndex = 30;
            this.ReturnGame.Value = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 302);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(179, 15);
            this.label11.TabIndex = 33;
            this.label11.Text = "Number of days/months to skip:";
            // 
            // DaysToSkip
            // 
            this.DaysToSkip.Location = new System.Drawing.Point(265, 300);
            this.DaysToSkip.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.DaysToSkip.Name = "DaysToSkip";
            this.DaysToSkip.Size = new System.Drawing.Size(68, 23);
            this.DaysToSkip.TabIndex = 32;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(28, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(248, 15);
            this.label12.TabIndex = 34;
            this.label12.Text = "Use touch screen inputs (faster, experimental)";
            // 
            // UseTouch
            // 
            this.UseTouch.AutoSize = true;
            this.UseTouch.Location = new System.Drawing.Point(8, 25);
            this.UseTouch.Name = "UseTouch";
            this.UseTouch.Size = new System.Drawing.Size(15, 14);
            this.UseTouch.TabIndex = 36;
            this.UseTouch.UseVisualStyleBackColor = true;
            // 
            // DiscordWebhook
            // 
            this.DiscordWebhook.Location = new System.Drawing.Point(6, 147);
            this.DiscordWebhook.Name = "DiscordWebhook";
            this.DiscordWebhook.Size = new System.Drawing.Size(327, 23);
            this.DiscordWebhook.TabIndex = 37;
            // 
            // EnableDiscordNotifications
            // 
            this.EnableDiscordNotifications.AutoSize = true;
            this.EnableDiscordNotifications.Location = new System.Drawing.Point(6, 124);
            this.EnableDiscordNotifications.Name = "EnableDiscordNotifications";
            this.EnableDiscordNotifications.Size = new System.Drawing.Size(303, 19);
            this.EnableDiscordNotifications.TabIndex = 38;
            this.EnableDiscordNotifications.Text = "Send alerts to Discord webhooks (comma separated)";
            this.EnableDiscordNotifications.UseVisualStyleBackColor = true;
            this.EnableDiscordNotifications.CheckedChanged += new System.EventHandler(this.EnableDiscordNotifications_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label13.Location = new System.Drawing.Point(6, 3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 15);
            this.label13.TabIndex = 39;
            this.label13.Text = "Experimental:";
            // 
            // ExperimentalView
            // 
            this.ExperimentalView.AutoSize = true;
            this.ExperimentalView.Location = new System.Drawing.Point(6, 21);
            this.ExperimentalView.Name = "ExperimentalView";
            this.ExperimentalView.Size = new System.Drawing.Size(241, 19);
            this.ExperimentalView.TabIndex = 40;
            this.ExperimentalView.Text = "Experimental Raid View  (requires restart)";
            this.ExperimentalView.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(349, 444);
            this.tabControl1.TabIndex = 41;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.LabelMatchFound);
            this.tabPage1.Controls.Add(this.FocusWindow);
            this.tabPage1.Controls.Add(this.EnableAlert);
            this.tabPage1.Controls.Add(this.EnableDiscordNotifications);
            this.tabPage1.Controls.Add(this.PlayTone);
            this.tabPage1.Controls.Add(this.DiscordWebhook);
            this.tabPage1.Controls.Add(this.AlertMessage);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(341, 416);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Match";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.SystemOvershoot);
            this.tabPage2.Controls.Add(this.UseOvershoot);
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.BaseDelay);
            this.tabPage2.Controls.Add(this.UseTouch);
            this.tabPage2.Controls.Add(this.SystemDDownPresses);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.NavigateToSettings);
            this.tabPage2.Controls.Add(this.DaysToSkip);
            this.tabPage2.Controls.Add(this.OpenSettings);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.OpenHome);
            this.tabPage2.Controls.Add(this.ReturnGame);
            this.tabPage2.Controls.Add(this.LabelDelayOpenHOME);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.ReturnHome);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.Hold);
            this.tabPage2.Controls.Add(this.DateChange);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.Submenu);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(341, 416);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Advance Date";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 389);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(236, 15);
            this.label16.TabIndex = 40;
            this.label16.Text = "Time to hold to overshoot \"Date and Time\":";
            // 
            // SystemOvershoot
            // 
            this.SystemOvershoot.Location = new System.Drawing.Point(265, 387);
            this.SystemOvershoot.Maximum = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            this.SystemOvershoot.Name = "SystemOvershoot";
            this.SystemOvershoot.Size = new System.Drawing.Size(68, 23);
            this.SystemOvershoot.TabIndex = 39;
            this.SystemOvershoot.Value = new decimal(new int[] {
            950,
            0,
            0,
            0});
            // 
            // UseOvershoot
            // 
            this.UseOvershoot.AutoSize = true;
            this.UseOvershoot.Location = new System.Drawing.Point(8, 46);
            this.UseOvershoot.Name = "UseOvershoot";
            this.UseOvershoot.Size = new System.Drawing.Size(15, 14);
            this.UseOvershoot.TabIndex = 38;
            this.UseOvershoot.UseVisualStyleBackColor = true;
            this.UseOvershoot.CheckedChanged += new System.EventHandler(this.UseOvershoot_CheckedChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(28, 45);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(261, 15);
            this.label15.TabIndex = 37;
            this.label15.Text = "Use overshoot instead of DDOWN inputs (faster)";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.labelWebhooks);
            this.tabPage4.Controls.Add(this.label21);
            this.tabPage4.Controls.Add(this.DiscordMessageContent);
            this.tabPage4.Controls.Add(this.label14);
            this.tabPage4.Controls.Add(this.btnTestWebHook);
            this.tabPage4.Controls.Add(this.denToggle);
            this.tabPage4.Controls.Add(this.label22);
            this.tabPage4.Controls.Add(this.IVstyle);
            this.tabPage4.Controls.Add(this.IVspacer);
            this.tabPage4.Controls.Add(this.IVverbose);
            this.tabPage4.Controls.Add(this.label20);
            this.tabPage4.Controls.Add(this.label19);
            this.tabPage4.Controls.Add(this.label18);
            this.tabPage4.Controls.Add(this.EnableEmoji);
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(341, 416);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Webhook";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label21.Location = new System.Drawing.Point(8, 3);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(100, 15);
            this.label21.TabIndex = 43;
            this.label21.Text = "General Settings";
            // 
            // DiscordMessageContent
            // 
            this.DiscordMessageContent.Location = new System.Drawing.Point(7, 40);
            this.DiscordMessageContent.Name = "DiscordMessageContent";
            this.DiscordMessageContent.Size = new System.Drawing.Size(327, 23);
            this.DiscordMessageContent.TabIndex = 42;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 22);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(285, 15);
            this.label14.TabIndex = 41;
            this.label14.Text = "Message Content (ping with <@numerical_user_id>)";
            // 
            // btnTestWebHook
            // 
            this.btnTestWebHook.Location = new System.Drawing.Point(229, 385);
            this.btnTestWebHook.Name = "btnTestWebHook";
            this.btnTestWebHook.Size = new System.Drawing.Size(104, 23);
            this.btnTestWebHook.TabIndex = 22;
            this.btnTestWebHook.Text = "Test Webhook";
            this.btnTestWebHook.UseVisualStyleBackColor = true;
            this.btnTestWebHook.Click += new System.EventHandler(this.btnTestWebHook_Click);
            // 
            // denToggle
            // 
            this.denToggle.AutoSize = true;
            this.denToggle.Checked = true;
            this.denToggle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.denToggle.Location = new System.Drawing.Point(10, 236);
            this.denToggle.Name = "denToggle";
            this.denToggle.Size = new System.Drawing.Size(79, 19);
            this.denToggle.TabIndex = 21;
            this.denToggle.Text = "Show Den";
            this.denToggle.UseVisualStyleBackColor = true;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label22.Location = new System.Drawing.Point(8, 218);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(103, 15);
            this.label22.TabIndex = 20;
            this.label22.Text = "Location Settings";
            // 
            // IVstyle
            // 
            this.IVstyle.FormattingEnabled = true;
            this.IVstyle.Items.AddRange(new object[] {
            "Emoji",
            "Highlighted Numerical",
            "Numerical"});
            this.IVstyle.Location = new System.Drawing.Point(8, 148);
            this.IVstyle.Name = "IVstyle";
            this.IVstyle.Size = new System.Drawing.Size(121, 23);
            this.IVstyle.TabIndex = 8;
            // 
            // IVspacer
            // 
            this.IVspacer.Location = new System.Drawing.Point(8, 192);
            this.IVspacer.Name = "IVspacer";
            this.IVspacer.Size = new System.Drawing.Size(100, 23);
            this.IVspacer.TabIndex = 7;
            this.IVspacer.Text = "\" \"";
            // 
            // IVverbose
            // 
            this.IVverbose.AutoSize = true;
            this.IVverbose.Location = new System.Drawing.Point(8, 108);
            this.IVverbose.Name = "IVverbose";
            this.IVverbose.Size = new System.Drawing.Size(85, 19);
            this.IVverbose.TabIndex = 5;
            this.IVverbose.Text = "Verbose IVs";
            this.IVverbose.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(8, 174);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(54, 15);
            this.label20.TabIndex = 3;
            this.label20.Text = "IV spacer";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(8, 130);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(44, 15);
            this.label19.TabIndex = 2;
            this.label19.Text = "IV style";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label18.Location = new System.Drawing.Point(8, 90);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(68, 15);
            this.label18.TabIndex = 1;
            this.label18.Text = "IV Settings";
            // 
            // EnableEmoji
            // 
            this.EnableEmoji.AutoSize = true;
            this.EnableEmoji.Checked = true;
            this.EnableEmoji.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EnableEmoji.Location = new System.Drawing.Point(8, 69);
            this.EnableEmoji.Name = "EnableEmoji";
            this.EnableEmoji.Size = new System.Drawing.Size(94, 19);
            this.EnableEmoji.TabIndex = 0;
            this.EnableEmoji.Text = "Enable Emoji";
            this.EnableEmoji.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.InstanceName);
            this.tabPage3.Controls.Add(this.label17);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.ExperimentalView);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(341, 416);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Experimental";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // InstanceName
            // 
            this.InstanceName.Location = new System.Drawing.Point(6, 61);
            this.InstanceName.Name = "InstanceName";
            this.InstanceName.Size = new System.Drawing.Size(327, 23);
            this.InstanceName.TabIndex = 42;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 43);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(89, 15);
            this.label17.TabIndex = 41;
            this.label17.Text = "Instance Name:";
            // 
            // labelWebhooks
            // 
            this.labelWebhooks.AutoSize = true;
            this.labelWebhooks.Location = new System.Drawing.Point(7, 389);
            this.labelWebhooks.Name = "labelWebhooks";
            this.labelWebhooks.Size = new System.Drawing.Size(85, 15);
            this.labelWebhooks.TabIndex = 44;
            this.labelWebhooks.Text = "Webhooks are ";
            // 
            // ConfigWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 476);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.Save);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ConfigWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RaidCrawler Settings";
            ((System.ComponentModel.ISupportInitialize)(this.BaseDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SystemDDownPresses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NavigateToSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OpenSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OpenHome)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Submenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateChange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReturnHome)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReturnGame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DaysToSkip)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SystemOvershoot)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CheckBox FocusWindow;
        private CheckBox EnableAlert;
        private CheckBox PlayTone;
        private Label LabelMatchFound;
        private TextBox AlertMessage;
        private Label label2;
        private Label label1;
        private NumericUpDown BaseDelay;
        private NumericUpDown SystemDDownPresses;
        private Label label3;
        private Button Save;
        private NumericUpDown NavigateToSettings;
        private NumericUpDown OpenSettings;
        private NumericUpDown OpenHome;
        private Label LabelDelayOpenHOME;
        private Label label4;
        private Label label5;
        private Label label6;
        private NumericUpDown Hold;
        private Label label7;
        private NumericUpDown Submenu;
        private Label label8;
        private NumericUpDown DateChange;
        private Label label9;
        private NumericUpDown ReturnHome;
        private Label label10;
        private NumericUpDown ReturnGame;
        private Label label11;
        private NumericUpDown DaysToSkip;
        private Label label12;
        private CheckBox UseTouch;
        private TextBox DiscordWebhook;
        private CheckBox EnableDiscordNotifications;
        private Label label13;
        private CheckBox ExperimentalView;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private Label label16;
        private NumericUpDown SystemOvershoot;
        private CheckBox UseOvershoot;
        private Label label15;
        private TextBox InstanceName;
        private Label label17;
        private TabPage tabPage4;
        private CheckBox EnableEmoji;
        private Label label20;
        private Label label19;
        private Label label18;
        private ComboBox IVstyle;
        private TextBox IVspacer;
        private CheckBox IVverbose;
        private CheckBox denToggle;
        private Label label22;
        private Button btnTestWebHook;
        private Label label21;
        private TextBox DiscordMessageContent;
        private Label label14;
        private Label labelWebhooks;
    }
}