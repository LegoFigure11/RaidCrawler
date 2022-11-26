namespace RaidCrawler
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.ButtonDisconnect = new System.Windows.Forms.Button();
            this.ButtonConnect = new System.Windows.Forms.Button();
            this.ConnectionStatusText = new System.Windows.Forms.Label();
            this.InputSwitchIP = new System.Windows.Forms.TextBox();
            this.LabelStatus = new System.Windows.Forms.Label();
            this.LabelSwitchIP = new System.Windows.Forms.Label();
            this.LabelLoadedRaids = new System.Windows.Forms.Label();
            this.TeraType = new System.Windows.Forms.TextBox();
            this.LabelTeraType = new System.Windows.Forms.Label();
            this.PID = new System.Windows.Forms.TextBox();
            this.LabelPID = new System.Windows.Forms.Label();
            this.EC = new System.Windows.Forms.TextBox();
            this.LabelEC = new System.Windows.Forms.Label();
            this.Seed = new System.Windows.Forms.TextBox();
            this.LabelSeed = new System.Windows.Forms.Label();
            this.LabelIndex = new System.Windows.Forms.Label();
            this.ButtonNext = new System.Windows.Forms.Button();
            this.ButtonPrevious = new System.Windows.Forms.Button();
            this.Area = new System.Windows.Forms.TextBox();
            this.LabelUNK_2 = new System.Windows.Forms.Label();
            this.IVs = new System.Windows.Forms.TextBox();
            this.LabelIVs = new System.Windows.Forms.Label();
            this.ButtonReadRaids = new System.Windows.Forms.Button();
            this.ButtonAdvanceDate = new System.Windows.Forms.Button();
            this.CheckContinueUntilShiny = new System.Windows.Forms.CheckBox();
            this.IsEvent = new System.Windows.Forms.CheckBox();
            this.LabelIsEvent = new System.Windows.Forms.Label();
            this.Difficulty = new System.Windows.Forms.TextBox();
            this.LabelDifficulty = new System.Windows.Forms.Label();
            this.Progress = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ButtonViewRAM = new System.Windows.Forms.Button();
            this.Species = new System.Windows.Forms.TextBox();
            this.LabelSpecies = new System.Windows.Forms.Label();
            this.LabelMoves = new System.Windows.Forms.Label();
            this.Move1 = new System.Windows.Forms.TextBox();
            this.Move2 = new System.Windows.Forms.TextBox();
            this.Move4 = new System.Windows.Forms.TextBox();
            this.Move3 = new System.Windows.Forms.TextBox();
            this.LabelGame = new System.Windows.Forms.Label();
            this.Game = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // ButtonDisconnect
            // 
            this.ButtonDisconnect.Enabled = false;
            this.ButtonDisconnect.Location = new System.Drawing.Point(117, 51);
            this.ButtonDisconnect.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ButtonDisconnect.Name = "ButtonDisconnect";
            this.ButtonDisconnect.Size = new System.Drawing.Size(97, 27);
            this.ButtonDisconnect.TabIndex = 11;
            this.ButtonDisconnect.Text = "Disconnect";
            this.ButtonDisconnect.UseVisualStyleBackColor = true;
            this.ButtonDisconnect.Click += new System.EventHandler(this.Disconnect_Click);
            // 
            // ButtonConnect
            // 
            this.ButtonConnect.Location = new System.Drawing.Point(13, 51);
            this.ButtonConnect.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ButtonConnect.Name = "ButtonConnect";
            this.ButtonConnect.Size = new System.Drawing.Size(97, 27);
            this.ButtonConnect.TabIndex = 10;
            this.ButtonConnect.Text = "Connect";
            this.ButtonConnect.UseVisualStyleBackColor = true;
            this.ButtonConnect.Click += new System.EventHandler(this.ButtonConnect_Click);
            // 
            // ConnectionStatusText
            // 
            this.ConnectionStatusText.AutoSize = true;
            this.ConnectionStatusText.Location = new System.Drawing.Point(84, 32);
            this.ConnectionStatusText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ConnectionStatusText.Name = "ConnectionStatusText";
            this.ConnectionStatusText.Size = new System.Drawing.Size(89, 15);
            this.ConnectionStatusText.TabIndex = 9;
            this.ConnectionStatusText.Text = "Not connected.";
            // 
            // InputSwitchIP
            // 
            this.InputSwitchIP.Location = new System.Drawing.Point(84, 6);
            this.InputSwitchIP.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.InputSwitchIP.Name = "InputSwitchIP";
            this.InputSwitchIP.Size = new System.Drawing.Size(129, 23);
            this.InputSwitchIP.TabIndex = 8;
            this.InputSwitchIP.Text = "www.www.www.www";
            this.InputSwitchIP.TextChanged += new System.EventHandler(this.InputSwitchIP_Changed);
            // 
            // LabelStatus
            // 
            this.LabelStatus.AutoSize = true;
            this.LabelStatus.Location = new System.Drawing.Point(30, 32);
            this.LabelStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelStatus.Name = "LabelStatus";
            this.LabelStatus.Size = new System.Drawing.Size(42, 15);
            this.LabelStatus.TabIndex = 7;
            this.LabelStatus.Text = "Status:";
            // 
            // LabelSwitchIP
            // 
            this.LabelSwitchIP.AutoSize = true;
            this.LabelSwitchIP.Location = new System.Drawing.Point(13, 9);
            this.LabelSwitchIP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelSwitchIP.Name = "LabelSwitchIP";
            this.LabelSwitchIP.Size = new System.Drawing.Size(58, 15);
            this.LabelSwitchIP.TabIndex = 6;
            this.LabelSwitchIP.Text = "Switch IP:";
            // 
            // LabelLoadedRaids
            // 
            this.LabelLoadedRaids.AutoSize = true;
            this.LabelLoadedRaids.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelLoadedRaids.Location = new System.Drawing.Point(220, 9);
            this.LabelLoadedRaids.Name = "LabelLoadedRaids";
            this.LabelLoadedRaids.Size = new System.Drawing.Size(89, 15);
            this.LabelLoadedRaids.TabIndex = 12;
            this.LabelLoadedRaids.Text = "Loaded Raids: 0";
            // 
            // TeraType
            // 
            this.TeraType.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TeraType.Location = new System.Drawing.Point(531, 50);
            this.TeraType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TeraType.Name = "TeraType";
            this.TeraType.ReadOnly = true;
            this.TeraType.Size = new System.Drawing.Size(97, 22);
            this.TeraType.TabIndex = 49;
            // 
            // LabelTeraType
            // 
            this.LabelTeraType.AutoSize = true;
            this.LabelTeraType.Location = new System.Drawing.Point(466, 52);
            this.LabelTeraType.Name = "LabelTeraType";
            this.LabelTeraType.Size = new System.Drawing.Size(58, 15);
            this.LabelTeraType.TabIndex = 48;
            this.LabelTeraType.Text = "Tera Type:";
            // 
            // PID
            // 
            this.PID.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PID.Location = new System.Drawing.Point(533, 80);
            this.PID.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PID.Name = "PID";
            this.PID.ReadOnly = true;
            this.PID.Size = new System.Drawing.Size(95, 22);
            this.PID.TabIndex = 47;
            // 
            // LabelPID
            // 
            this.LabelPID.AutoSize = true;
            this.LabelPID.Location = new System.Drawing.Point(496, 82);
            this.LabelPID.Name = "LabelPID";
            this.LabelPID.Size = new System.Drawing.Size(28, 15);
            this.LabelPID.TabIndex = 46;
            this.LabelPID.Text = "PID:";
            // 
            // EC
            // 
            this.EC.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.EC.Location = new System.Drawing.Point(357, 78);
            this.EC.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.EC.Name = "EC";
            this.EC.ReadOnly = true;
            this.EC.Size = new System.Drawing.Size(95, 22);
            this.EC.TabIndex = 45;
            // 
            // LabelEC
            // 
            this.LabelEC.AutoSize = true;
            this.LabelEC.Location = new System.Drawing.Point(232, 80);
            this.LabelEC.Name = "LabelEC";
            this.LabelEC.Size = new System.Drawing.Size(118, 15);
            this.LabelEC.TabIndex = 44;
            this.LabelEC.Text = "Encryption Constant:";
            // 
            // Seed
            // 
            this.Seed.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Seed.Location = new System.Drawing.Point(357, 50);
            this.Seed.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Seed.Name = "Seed";
            this.Seed.ReadOnly = true;
            this.Seed.Size = new System.Drawing.Size(95, 22);
            this.Seed.TabIndex = 43;
            // 
            // LabelSeed
            // 
            this.LabelSeed.AutoSize = true;
            this.LabelSeed.Location = new System.Drawing.Point(315, 52);
            this.LabelSeed.Name = "LabelSeed";
            this.LabelSeed.Size = new System.Drawing.Size(35, 15);
            this.LabelSeed.TabIndex = 42;
            this.LabelSeed.Text = "Seed:";
            // 
            // LabelIndex
            // 
            this.LabelIndex.AutoSize = true;
            this.LabelIndex.Location = new System.Drawing.Point(89, 85);
            this.LabelIndex.Name = "LabelIndex";
            this.LabelIndex.Size = new System.Drawing.Size(48, 15);
            this.LabelIndex.TabIndex = 57;
            this.LabelIndex.Text = "ww/ww";
            // 
            // ButtonNext
            // 
            this.ButtonNext.Enabled = false;
            this.ButtonNext.Location = new System.Drawing.Point(146, 80);
            this.ButtonNext.Name = "ButtonNext";
            this.ButtonNext.Size = new System.Drawing.Size(33, 23);
            this.ButtonNext.TabIndex = 56;
            this.ButtonNext.Text = ">>";
            this.ButtonNext.UseVisualStyleBackColor = true;
            this.ButtonNext.Click += new System.EventHandler(this.ButtonNext_Click);
            // 
            // ButtonPrevious
            // 
            this.ButtonPrevious.Enabled = false;
            this.ButtonPrevious.Location = new System.Drawing.Point(47, 80);
            this.ButtonPrevious.Name = "ButtonPrevious";
            this.ButtonPrevious.Size = new System.Drawing.Size(33, 23);
            this.ButtonPrevious.TabIndex = 55;
            this.ButtonPrevious.Text = "<<";
            this.ButtonPrevious.UseVisualStyleBackColor = true;
            this.ButtonPrevious.Click += new System.EventHandler(this.ButtonPrevious_Click);
            // 
            // Area
            // 
            this.Area.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Area.Location = new System.Drawing.Point(357, 106);
            this.Area.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Area.Name = "Area";
            this.Area.ReadOnly = true;
            this.Area.Size = new System.Drawing.Size(270, 22);
            this.Area.TabIndex = 61;
            // 
            // LabelUNK_2
            // 
            this.LabelUNK_2.AutoSize = true;
            this.LabelUNK_2.Location = new System.Drawing.Point(316, 110);
            this.LabelUNK_2.Name = "LabelUNK_2";
            this.LabelUNK_2.Size = new System.Drawing.Size(34, 15);
            this.LabelUNK_2.TabIndex = 60;
            this.LabelUNK_2.Text = "Area:";
            // 
            // IVs
            // 
            this.IVs.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.IVs.Location = new System.Drawing.Point(357, 162);
            this.IVs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.IVs.Name = "IVs";
            this.IVs.ReadOnly = true;
            this.IVs.Size = new System.Drawing.Size(270, 22);
            this.IVs.TabIndex = 69;
            // 
            // LabelIVs
            // 
            this.LabelIVs.AutoSize = true;
            this.LabelIVs.Location = new System.Drawing.Point(325, 164);
            this.LabelIVs.Name = "LabelIVs";
            this.LabelIVs.Size = new System.Drawing.Size(25, 15);
            this.LabelIVs.TabIndex = 68;
            this.LabelIVs.Text = "IVs:";
            // 
            // ButtonReadRaids
            // 
            this.ButtonReadRaids.Enabled = false;
            this.ButtonReadRaids.Location = new System.Drawing.Point(13, 109);
            this.ButtonReadRaids.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ButtonReadRaids.Name = "ButtonReadRaids";
            this.ButtonReadRaids.Size = new System.Drawing.Size(97, 27);
            this.ButtonReadRaids.TabIndex = 80;
            this.ButtonReadRaids.Text = "Read Raids";
            this.ButtonReadRaids.UseVisualStyleBackColor = true;
            this.ButtonReadRaids.Click += new System.EventHandler(this.ButtonReadRaids_Click);
            // 
            // ButtonAdvanceDate
            // 
            this.ButtonAdvanceDate.Enabled = false;
            this.ButtonAdvanceDate.Location = new System.Drawing.Point(117, 110);
            this.ButtonAdvanceDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ButtonAdvanceDate.Name = "ButtonAdvanceDate";
            this.ButtonAdvanceDate.Size = new System.Drawing.Size(97, 27);
            this.ButtonAdvanceDate.TabIndex = 81;
            this.ButtonAdvanceDate.Text = "Advance Date";
            this.ButtonAdvanceDate.UseVisualStyleBackColor = true;
            this.ButtonAdvanceDate.Click += new System.EventHandler(this.ButtonAdvanceDate_Click);
            // 
            // CheckContinueUntilShiny
            // 
            this.CheckContinueUntilShiny.AutoSize = true;
            this.CheckContinueUntilShiny.Location = new System.Drawing.Point(13, 233);
            this.CheckContinueUntilShiny.Name = "CheckContinueUntilShiny";
            this.CheckContinueUntilShiny.Size = new System.Drawing.Size(199, 19);
            this.CheckContinueUntilShiny.TabIndex = 82;
            this.CheckContinueUntilShiny.Text = "Keep advancing date until shiny?";
            this.CheckContinueUntilShiny.UseVisualStyleBackColor = true;
            // 
            // IsEvent
            // 
            this.IsEvent.AutoCheck = false;
            this.IsEvent.AutoSize = true;
            this.IsEvent.Location = new System.Drawing.Point(533, 137);
            this.IsEvent.Name = "IsEvent";
            this.IsEvent.Size = new System.Drawing.Size(15, 14);
            this.IsEvent.TabIndex = 83;
            this.IsEvent.UseVisualStyleBackColor = true;
            // 
            // LabelIsEvent
            // 
            this.LabelIsEvent.AutoSize = true;
            this.LabelIsEvent.Location = new System.Drawing.Point(483, 136);
            this.LabelIsEvent.Name = "LabelIsEvent";
            this.LabelIsEvent.Size = new System.Drawing.Size(41, 15);
            this.LabelIsEvent.TabIndex = 84;
            this.LabelIsEvent.Text = "Event?";
            // 
            // Difficulty
            // 
            this.Difficulty.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Difficulty.Location = new System.Drawing.Point(357, 134);
            this.Difficulty.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Difficulty.Name = "Difficulty";
            this.Difficulty.ReadOnly = true;
            this.Difficulty.Size = new System.Drawing.Size(95, 22);
            this.Difficulty.TabIndex = 86;
            // 
            // LabelDifficulty
            // 
            this.LabelDifficulty.AutoSize = true;
            this.LabelDifficulty.Location = new System.Drawing.Point(292, 136);
            this.LabelDifficulty.Name = "LabelDifficulty";
            this.LabelDifficulty.Size = new System.Drawing.Size(58, 15);
            this.LabelDifficulty.TabIndex = 85;
            this.LabelDifficulty.Text = "Difficulty:";
            // 
            // Progress
            // 
            this.Progress.FormattingEnabled = true;
            this.Progress.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.Progress.Location = new System.Drawing.Point(164, 204);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(48, 23);
            this.Progress.TabIndex = 87;
            this.Progress.Text = "w";
            this.Progress.SelectedIndexChanged += new System.EventHandler(this.Progress_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 207);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 15);
            this.label1.TabIndex = 88;
            this.label1.Text = "Story Progress Level:";
            // 
            // ButtonViewRAM
            // 
            this.ButtonViewRAM.Enabled = false;
            this.ButtonViewRAM.Location = new System.Drawing.Point(531, 9);
            this.ButtonViewRAM.Name = "ButtonViewRAM";
            this.ButtonViewRAM.Size = new System.Drawing.Size(97, 23);
            this.ButtonViewRAM.TabIndex = 89;
            this.ButtonViewRAM.Text = "View Block";
            this.ButtonViewRAM.UseVisualStyleBackColor = true;
            this.ButtonViewRAM.Click += new System.EventHandler(this.ViewRAM_Click);
            // 
            // Species
            // 
            this.Species.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Species.Location = new System.Drawing.Point(357, 192);
            this.Species.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Species.Name = "Species";
            this.Species.ReadOnly = true;
            this.Species.Size = new System.Drawing.Size(270, 22);
            this.Species.TabIndex = 93;
            // 
            // LabelSpecies
            // 
            this.LabelSpecies.AutoSize = true;
            this.LabelSpecies.Location = new System.Drawing.Point(301, 194);
            this.LabelSpecies.Name = "LabelSpecies";
            this.LabelSpecies.Size = new System.Drawing.Size(49, 15);
            this.LabelSpecies.TabIndex = 92;
            this.LabelSpecies.Text = "Species:";
            // 
            // LabelMoves
            // 
            this.LabelMoves.AutoSize = true;
            this.LabelMoves.Location = new System.Drawing.Point(301, 235);
            this.LabelMoves.Name = "LabelMoves";
            this.LabelMoves.Size = new System.Drawing.Size(45, 15);
            this.LabelMoves.TabIndex = 94;
            this.LabelMoves.Text = "Moves:";
            // 
            // Move1
            // 
            this.Move1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Move1.Location = new System.Drawing.Point(357, 220);
            this.Move1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Move1.Name = "Move1";
            this.Move1.ReadOnly = true;
            this.Move1.Size = new System.Drawing.Size(133, 22);
            this.Move1.TabIndex = 95;
            // 
            // Move2
            // 
            this.Move2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Move2.Location = new System.Drawing.Point(494, 220);
            this.Move2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Move2.Name = "Move2";
            this.Move2.ReadOnly = true;
            this.Move2.Size = new System.Drawing.Size(133, 22);
            this.Move2.TabIndex = 96;
            // 
            // Move4
            // 
            this.Move4.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Move4.Location = new System.Drawing.Point(494, 245);
            this.Move4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Move4.Name = "Move4";
            this.Move4.ReadOnly = true;
            this.Move4.Size = new System.Drawing.Size(133, 22);
            this.Move4.TabIndex = 98;
            // 
            // Move3
            // 
            this.Move3.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Move3.Location = new System.Drawing.Point(357, 245);
            this.Move3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Move3.Name = "Move3";
            this.Move3.ReadOnly = true;
            this.Move3.Size = new System.Drawing.Size(133, 22);
            this.Move3.TabIndex = 97;
            // 
            // LabelGame
            // 
            this.LabelGame.AutoSize = true;
            this.LabelGame.Location = new System.Drawing.Point(13, 182);
            this.LabelGame.Name = "LabelGame";
            this.LabelGame.Size = new System.Drawing.Size(41, 15);
            this.LabelGame.TabIndex = 100;
            this.LabelGame.Text = "Game:";
            // 
            // Game
            // 
            this.Game.FormattingEnabled = true;
            this.Game.Items.AddRange(new object[] {
            "Scarlet",
            "Violet"});
            this.Game.Location = new System.Drawing.Point(116, 179);
            this.Game.Name = "Game";
            this.Game.Size = new System.Drawing.Size(96, 23);
            this.Game.TabIndex = 99;
            this.Game.Text = "w";
            this.Game.SelectedIndexChanged += new System.EventHandler(this.Game_SelectedIndexChanged);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 275);
            this.Controls.Add(this.LabelGame);
            this.Controls.Add(this.Game);
            this.Controls.Add(this.Move4);
            this.Controls.Add(this.Move3);
            this.Controls.Add(this.Move2);
            this.Controls.Add(this.Move1);
            this.Controls.Add(this.LabelMoves);
            this.Controls.Add(this.Species);
            this.Controls.Add(this.LabelSpecies);
            this.Controls.Add(this.ButtonViewRAM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Progress);
            this.Controls.Add(this.Difficulty);
            this.Controls.Add(this.LabelDifficulty);
            this.Controls.Add(this.LabelIsEvent);
            this.Controls.Add(this.IsEvent);
            this.Controls.Add(this.CheckContinueUntilShiny);
            this.Controls.Add(this.ButtonAdvanceDate);
            this.Controls.Add(this.ButtonReadRaids);
            this.Controls.Add(this.IVs);
            this.Controls.Add(this.LabelIVs);
            this.Controls.Add(this.Area);
            this.Controls.Add(this.LabelUNK_2);
            this.Controls.Add(this.LabelIndex);
            this.Controls.Add(this.ButtonNext);
            this.Controls.Add(this.ButtonPrevious);
            this.Controls.Add(this.TeraType);
            this.Controls.Add(this.LabelTeraType);
            this.Controls.Add(this.PID);
            this.Controls.Add(this.LabelPID);
            this.Controls.Add(this.EC);
            this.Controls.Add(this.LabelEC);
            this.Controls.Add(this.Seed);
            this.Controls.Add(this.LabelSeed);
            this.Controls.Add(this.LabelLoadedRaids);
            this.Controls.Add(this.ButtonDisconnect);
            this.Controls.Add(this.ButtonConnect);
            this.Controls.Add(this.ConnectionStatusText);
            this.Controls.Add(this.InputSwitchIP);
            this.Controls.Add(this.LabelStatus);
            this.Controls.Add(this.LabelSwitchIP);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button ButtonDisconnect;
        private Button ButtonConnect;
        private Label ConnectionStatusText;
        private TextBox InputSwitchIP;
        private Label LabelStatus;
        private Label LabelSwitchIP;
        private Label LabelLoadedRaids;
        private TextBox TeraType;
        private Label LabelTeraType;
        private TextBox PID;
        private Label LabelPID;
        private TextBox EC;
        private Label LabelEC;
        private TextBox Seed;
        private Label LabelSeed;
        private Label LabelIndex;
        private Button ButtonNext;
        private Button ButtonPrevious;
        private TextBox Area;
        private Label LabelUNK_2;
        private TextBox IVs;
        private Label LabelIVs;
        private Button ButtonReadRaids;
        private Button ButtonAdvanceDate;
        private CheckBox CheckContinueUntilShiny;
        private CheckBox IsEvent;
        private Label LabelIsEvent;
        private TextBox Difficulty;
        private Label LabelDifficulty;
        private ComboBox Progress;
        private Label label1;
        private Button ButtonViewRAM;
        private TextBox Species;
        private Label LabelSpecies;
        private Label LabelMoves;
        private TextBox Move1;
        private TextBox Move2;
        private TextBox Move4;
        private TextBox Move3;
        private Label LabelGame;
        private ComboBox Game;
    }
}