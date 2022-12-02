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
            this.SuspendLayout();
            // 
            // FocusWindow
            // 
            this.FocusWindow.AutoSize = true;
            this.FocusWindow.Location = new System.Drawing.Point(12, 52);
            this.FocusWindow.Name = "FocusWindow";
            this.FocusWindow.Size = new System.Drawing.Size(123, 19);
            this.FocusWindow.TabIndex = 1;
            this.FocusWindow.Text = "Focus RaidCrawler";
            this.FocusWindow.UseVisualStyleBackColor = true;
            // 
            // EnableAlert
            // 
            this.EnableAlert.AutoSize = true;
            this.EnableAlert.Location = new System.Drawing.Point(12, 77);
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
            this.PlayTone.Location = new System.Drawing.Point(12, 27);
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
            this.LabelMatchFound.Location = new System.Drawing.Point(12, 9);
            this.LabelMatchFound.Name = "LabelMatchFound";
            this.LabelMatchFound.Size = new System.Drawing.Size(137, 15);
            this.LabelMatchFound.TabIndex = 3;
            this.LabelMatchFound.Text = "When a match is found:";
            // 
            // AlertMessage
            // 
            this.AlertMessage.Location = new System.Drawing.Point(12, 102);
            this.AlertMessage.Name = "AlertMessage";
            this.AlertMessage.Size = new System.Drawing.Size(297, 23);
            this.AlertMessage.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(12, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Date Advance Options (all timings in ms):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Base delay to be added to all inputs:";
            // 
            // BaseDelay
            // 
            this.BaseDelay.Location = new System.Drawing.Point(241, 163);
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
            this.SystemDDownPresses.Location = new System.Drawing.Point(241, 308);
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
            this.label3.Location = new System.Drawing.Point(12, 310);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(228, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "DDOWN inputs to get to \"Date and Time\":";
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(12, 464);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(297, 23);
            this.Save.TabIndex = 12;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // NavigateToSettings
            // 
            this.NavigateToSettings.Location = new System.Drawing.Point(241, 221);
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
            this.OpenSettings.Location = new System.Drawing.Point(241, 250);
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
            this.OpenHome.Location = new System.Drawing.Point(241, 192);
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
            this.LabelDelayOpenHOME.Location = new System.Drawing.Point(12, 194);
            this.LabelDelayOpenHOME.Name = "LabelDelayOpenHOME";
            this.LabelDelayOpenHOME.Size = new System.Drawing.Size(140, 15);
            this.LabelDelayOpenHOME.TabIndex = 19;
            this.LabelDelayOpenHOME.Text = "Open Home Menu delay:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 223);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 15);
            this.label4.TabIndex = 20;
            this.label4.Text = "Navigate to settings delay:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 252);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 15);
            this.label5.TabIndex = 21;
            this.label5.Text = "Open settings delay:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 281);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(187, 15);
            this.label6.TabIndex = 23;
            this.label6.Text = "Time to hold to scroll to \"System\":";
            // 
            // Hold
            // 
            this.Hold.Location = new System.Drawing.Point(241, 279);
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
            this.label7.Location = new System.Drawing.Point(12, 339);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 15);
            this.label7.TabIndex = 25;
            this.label7.Text = "Open submenu delay:";
            // 
            // Submenu
            // 
            this.Submenu.Location = new System.Drawing.Point(241, 337);
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
            this.label8.Location = new System.Drawing.Point(12, 368);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(138, 15);
            this.label8.TabIndex = 27;
            this.label8.Text = "Open date change delay:";
            // 
            // DateChange
            // 
            this.DateChange.Location = new System.Drawing.Point(241, 366);
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
            this.label9.Location = new System.Drawing.Point(12, 397);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(160, 15);
            this.label9.TabIndex = 29;
            this.label9.Text = "Return to Home Menu delay:";
            // 
            // ReturnHome
            // 
            this.ReturnHome.Location = new System.Drawing.Point(241, 395);
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
            this.label10.Location = new System.Drawing.Point(12, 426);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(119, 15);
            this.label10.TabIndex = 31;
            this.label10.Text = "Re-open game delay:";
            // 
            // ReturnGame
            // 
            this.ReturnGame.Location = new System.Drawing.Point(241, 424);
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
            // ConfigWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 499);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.ReturnGame);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.ReturnHome);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.DateChange);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Submenu);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Hold);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LabelDelayOpenHOME);
            this.Controls.Add(this.OpenHome);
            this.Controls.Add(this.OpenSettings);
            this.Controls.Add(this.NavigateToSettings);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SystemDDownPresses);
            this.Controls.Add(this.BaseDelay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AlertMessage);
            this.Controls.Add(this.LabelMatchFound);
            this.Controls.Add(this.PlayTone);
            this.Controls.Add(this.EnableAlert);
            this.Controls.Add(this.FocusWindow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ConfigWindow";
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
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}