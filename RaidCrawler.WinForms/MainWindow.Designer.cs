namespace RaidCrawler.WinForms
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            toolTip = new ToolTip(components);
            ButtonAdvanceDate = new Button();
            CheckEnableFilters = new CheckBox();
            ButtonDisconnect = new Button();
            ButtonConnect = new Button();
            InputSwitchIP = new TextBox();
            LabelSwitchIP = new Label();
            LabelLoadedRaids = new Label();
            TeraType = new TextBox();
            LabelTeraType = new Label();
            PID = new TextBox();
            LabelPID = new Label();
            EC = new TextBox();
            LabelEC = new Label();
            Seed = new TextBox();
            LabelSeed = new Label();
            ButtonNext = new Button();
            ButtonPrevious = new Button();
            Area = new TextBox();
            LabelUNK_2 = new Label();
            IVs = new TextBox();
            LabelIVs = new Label();
            ButtonReadRaids = new Button();
            labelEvent = new Label();
            Difficulty = new TextBox();
            LabelDifficulty = new Label();
            ButtonViewRAM = new Button();
            Species = new TextBox();
            LabelSpecies = new Label();
            LabelMoves = new Label();
            Move1 = new TextBox();
            Move2 = new TextBox();
            Move4 = new TextBox();
            Move3 = new TextBox();
            Nature = new TextBox();
            LabelNature = new Label();
            Gender = new TextBox();
            LabelGender = new Label();
            StopFilter = new Button();
            Sprite = new PictureBox();
            Ability = new TextBox();
            LabelAbility = new Label();
            GemIcon = new PictureBox();
            ButtonDownloadEvents = new Button();
            ConfigSettings = new Button();
            Rewards = new Button();
            LabelSandwichBonus = new Label();
            RaidBoost = new ComboBox();
            ComboIndex = new ComboBox();
            SendScreenshot = new Button();
            SearchTimer = new System.Timers.Timer(1);
            btnOpenMap = new Button();
            groupBox1 = new GroupBox();
            statusStrip1 = new StatusStrip();
            StatusLabel = new ToolStripStatusLabel();
            ToolStripStatusLabel = new ToolStripStatusLabel();
            USB_Port_label = new Label();
            USB_Port_TB = new TextBox();
            StopAdvance_Button = new Button();
            ((System.ComponentModel.ISupportInitialize)Sprite).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GemIcon).BeginInit();
            groupBox1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // ButtonAdvanceDate
            // 
            ButtonAdvanceDate.Enabled = false;
            ButtonAdvanceDate.Location = new Point(117, 98);
            ButtonAdvanceDate.Margin = new Padding(4, 3, 4, 3);
            ButtonAdvanceDate.Name = "ButtonAdvanceDate";
            ButtonAdvanceDate.Size = new Size(96, 27);
            ButtonAdvanceDate.TabIndex = 81;
            ButtonAdvanceDate.Text = "Advance Date";
            toolTip.SetToolTip(ButtonAdvanceDate, "Advance Date performs one (1) time set.\r\n\r\nIf Stop Filters are defined, Advance Date\r\ncontinues advancing the date until a stop\r\nfilter has been hit.");
            ButtonAdvanceDate.UseVisualStyleBackColor = true;
            ButtonAdvanceDate.Click += ButtonAdvanceDate_Click;
            // 
            // CheckEnableFilters
            // 
            CheckEnableFilters.AutoSize = true;
            CheckEnableFilters.Checked = true;
            CheckEnableFilters.CheckState = CheckState.Checked;
            CheckEnableFilters.Location = new Point(117, 276);
            CheckEnableFilters.Name = "CheckEnableFilters";
            CheckEnableFilters.Size = new Size(95, 19);
            CheckEnableFilters.TabIndex = 119;
            CheckEnableFilters.Text = "Enable Filters";
            toolTip.SetToolTip(CheckEnableFilters, "Enable Filters enables or disables all filters\r\nentirely.\r\n\r\nEnabled - Advance Date will continue until\r\na match occurs from a filter.\r\n\r\nDisabled - Advance Date will only advance\r\none (1) day.");
            CheckEnableFilters.UseVisualStyleBackColor = true;
            CheckEnableFilters.Click += EnableFilters_Click;
            // 
            // ButtonDisconnect
            // 
            ButtonDisconnect.Enabled = false;
            ButtonDisconnect.Location = new Point(117, 35);
            ButtonDisconnect.Margin = new Padding(4, 3, 4, 3);
            ButtonDisconnect.Name = "ButtonDisconnect";
            ButtonDisconnect.Size = new Size(97, 27);
            ButtonDisconnect.TabIndex = 11;
            ButtonDisconnect.Text = "Disconnect";
            ButtonDisconnect.UseVisualStyleBackColor = true;
            ButtonDisconnect.Click += Disconnect_Click;
            // 
            // ButtonConnect
            // 
            ButtonConnect.Location = new Point(13, 35);
            ButtonConnect.Margin = new Padding(4, 3, 4, 3);
            ButtonConnect.Name = "ButtonConnect";
            ButtonConnect.Size = new Size(97, 27);
            ButtonConnect.TabIndex = 10;
            ButtonConnect.Text = "Connect";
            ButtonConnect.UseVisualStyleBackColor = true;
            ButtonConnect.Click += ButtonConnect_Click;
            // 
            // InputSwitchIP
            // 
            InputSwitchIP.Location = new Point(84, 6);
            InputSwitchIP.Margin = new Padding(4, 3, 4, 3);
            InputSwitchIP.Name = "InputSwitchIP";
            InputSwitchIP.Size = new Size(129, 23);
            InputSwitchIP.TabIndex = 8;
            InputSwitchIP.Text = "www.www.www.www";
            InputSwitchIP.TextChanged += InputSwitchIP_Changed;
            // 
            // LabelSwitchIP
            // 
            LabelSwitchIP.AutoSize = true;
            LabelSwitchIP.Location = new Point(13, 9);
            LabelSwitchIP.Margin = new Padding(4, 0, 4, 0);
            LabelSwitchIP.Name = "LabelSwitchIP";
            LabelSwitchIP.Size = new Size(58, 15);
            LabelSwitchIP.TabIndex = 6;
            LabelSwitchIP.Text = "Switch IP:";
            // 
            // LabelLoadedRaids
            // 
            LabelLoadedRaids.AutoSize = true;
            LabelLoadedRaids.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            LabelLoadedRaids.Location = new Point(12, 103);
            LabelLoadedRaids.Name = "LabelLoadedRaids";
            LabelLoadedRaids.Size = new Size(67, 15);
            LabelLoadedRaids.TabIndex = 12;
            LabelLoadedRaids.Text = "Matches: 0";
            // 
            // TeraType
            // 
            TeraType.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            TeraType.Location = new Point(296, 152);
            TeraType.Margin = new Padding(4, 3, 4, 3);
            TeraType.Name = "TeraType";
            TeraType.ReadOnly = true;
            TeraType.Size = new Size(95, 22);
            TeraType.TabIndex = 49;
            // 
            // LabelTeraType
            // 
            LabelTeraType.AutoSize = true;
            LabelTeraType.Location = new Point(232, 156);
            LabelTeraType.Name = "LabelTeraType";
            LabelTeraType.Size = new Size(58, 15);
            LabelTeraType.TabIndex = 48;
            LabelTeraType.Text = "Tera Type:";
            LabelTeraType.TextAlign = ContentAlignment.MiddleRight;
            // 
            // PID
            // 
            PID.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            PID.Location = new Point(296, 68);
            PID.Margin = new Padding(4, 3, 4, 3);
            PID.Name = "PID";
            PID.ReadOnly = true;
            PID.Size = new Size(95, 22);
            PID.TabIndex = 47;
            // 
            // LabelPID
            // 
            LabelPID.AutoSize = true;
            LabelPID.Location = new Point(261, 70);
            LabelPID.Name = "LabelPID";
            LabelPID.Size = new Size(28, 15);
            LabelPID.TabIndex = 46;
            LabelPID.Text = "PID:";
            LabelPID.TextAlign = ContentAlignment.MiddleRight;
            // 
            // EC
            // 
            EC.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            EC.Location = new Point(296, 40);
            EC.Margin = new Padding(4, 3, 4, 3);
            EC.Name = "EC";
            EC.ReadOnly = true;
            EC.Size = new Size(95, 22);
            EC.TabIndex = 45;
            // 
            // LabelEC
            // 
            LabelEC.AutoSize = true;
            LabelEC.Location = new Point(265, 42);
            LabelEC.Name = "LabelEC";
            LabelEC.Size = new Size(24, 15);
            LabelEC.TabIndex = 44;
            LabelEC.Text = "EC:";
            LabelEC.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Seed
            // 
            Seed.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Seed.Location = new Point(296, 12);
            Seed.Margin = new Padding(4, 3, 4, 3);
            Seed.Name = "Seed";
            Seed.ReadOnly = true;
            Seed.Size = new Size(95, 22);
            Seed.TabIndex = 43;
            Seed.Click += Seed_Click;
            // 
            // LabelSeed
            // 
            LabelSeed.AutoSize = true;
            LabelSeed.Location = new Point(254, 14);
            LabelSeed.Name = "LabelSeed";
            LabelSeed.Size = new Size(35, 15);
            LabelSeed.TabIndex = 42;
            LabelSeed.Text = "Seed:";
            LabelSeed.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ButtonNext
            // 
            ButtonNext.Enabled = false;
            ButtonNext.Location = new Point(149, 68);
            ButtonNext.Name = "ButtonNext";
            ButtonNext.Size = new Size(45, 25);
            ButtonNext.TabIndex = 56;
            ButtonNext.Text = ">>";
            ButtonNext.UseVisualStyleBackColor = true;
            ButtonNext.Click += ButtonNext_Click;
            // 
            // ButtonPrevious
            // 
            ButtonPrevious.Enabled = false;
            ButtonPrevious.Location = new Point(30, 68);
            ButtonPrevious.Name = "ButtonPrevious";
            ButtonPrevious.Size = new Size(45, 25);
            ButtonPrevious.TabIndex = 55;
            ButtonPrevious.Text = "<<";
            ButtonPrevious.UseVisualStyleBackColor = true;
            ButtonPrevious.Click += ButtonPrevious_Click;
            // 
            // Area
            // 
            Area.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Area.Location = new Point(296, 236);
            Area.Margin = new Padding(4, 3, 4, 3);
            Area.Name = "Area";
            Area.ReadOnly = true;
            Area.Size = new Size(271, 22);
            Area.TabIndex = 61;
            Area.Click += DisplayMap;
            // 
            // LabelUNK_2
            // 
            LabelUNK_2.AutoSize = true;
            LabelUNK_2.Location = new Point(256, 240);
            LabelUNK_2.Name = "LabelUNK_2";
            LabelUNK_2.Size = new Size(34, 15);
            LabelUNK_2.TabIndex = 60;
            LabelUNK_2.Text = "Area:";
            LabelUNK_2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // IVs
            // 
            IVs.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            IVs.Location = new Point(296, 208);
            IVs.Margin = new Padding(4, 3, 4, 3);
            IVs.Name = "IVs";
            IVs.ReadOnly = true;
            IVs.Size = new Size(271, 22);
            IVs.TabIndex = 69;
            // 
            // LabelIVs
            // 
            LabelIVs.AutoSize = true;
            LabelIVs.Location = new Point(265, 212);
            LabelIVs.Name = "LabelIVs";
            LabelIVs.Size = new Size(25, 15);
            LabelIVs.TabIndex = 68;
            LabelIVs.Text = "IVs:";
            LabelIVs.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ButtonReadRaids
            // 
            ButtonReadRaids.Enabled = false;
            ButtonReadRaids.Location = new Point(6, 22);
            ButtonReadRaids.Margin = new Padding(4, 3, 4, 3);
            ButtonReadRaids.Name = "ButtonReadRaids";
            ButtonReadRaids.Size = new Size(90, 25);
            ButtonReadRaids.TabIndex = 80;
            ButtonReadRaids.Text = "Read Raids";
            ButtonReadRaids.UseVisualStyleBackColor = true;
            ButtonReadRaids.Click += ButtonReadRaids_Click;
            // 
            // labelEvent
            // 
            labelEvent.AutoSize = true;
            labelEvent.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            labelEvent.ForeColor = SystemColors.ControlText;
            labelEvent.Location = new Point(496, 64);
            labelEvent.Name = "labelEvent";
            labelEvent.Size = new Size(73, 15);
            labelEvent.TabIndex = 84;
            labelEvent.Text = "~~Event~~";
            labelEvent.TextAlign = ContentAlignment.MiddleLeft;
            labelEvent.Visible = false;
            // 
            // Difficulty
            // 
            Difficulty.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Difficulty.Location = new Point(470, 152);
            Difficulty.Margin = new Padding(4, 3, 4, 3);
            Difficulty.Name = "Difficulty";
            Difficulty.ReadOnly = true;
            Difficulty.Size = new Size(97, 22);
            Difficulty.TabIndex = 86;
            // 
            // LabelDifficulty
            // 
            LabelDifficulty.AutoSize = true;
            LabelDifficulty.Location = new Point(405, 156);
            LabelDifficulty.Name = "LabelDifficulty";
            LabelDifficulty.Size = new Size(58, 15);
            LabelDifficulty.TabIndex = 85;
            LabelDifficulty.Text = "Difficulty:";
            LabelDifficulty.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ButtonViewRAM
            // 
            ButtonViewRAM.Enabled = false;
            ButtonViewRAM.Location = new Point(104, 22);
            ButtonViewRAM.Name = "ButtonViewRAM";
            ButtonViewRAM.Size = new Size(90, 25);
            ButtonViewRAM.TabIndex = 89;
            ButtonViewRAM.Text = "Dump Raid";
            ButtonViewRAM.UseVisualStyleBackColor = true;
            ButtonViewRAM.Click += ViewRAM_Click;
            // 
            // Species
            // 
            Species.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Species.Location = new Point(296, 96);
            Species.Margin = new Padding(4, 3, 4, 3);
            Species.Name = "Species";
            Species.ReadOnly = true;
            Species.Size = new Size(271, 22);
            Species.TabIndex = 93;
            // 
            // LabelSpecies
            // 
            LabelSpecies.AutoSize = true;
            LabelSpecies.Location = new Point(241, 100);
            LabelSpecies.Name = "LabelSpecies";
            LabelSpecies.Size = new Size(49, 15);
            LabelSpecies.TabIndex = 92;
            LabelSpecies.Text = "Species:";
            LabelSpecies.TextAlign = ContentAlignment.MiddleRight;
            // 
            // LabelMoves
            // 
            LabelMoves.AutoSize = true;
            LabelMoves.Location = new Point(244, 280);
            LabelMoves.Name = "LabelMoves";
            LabelMoves.Size = new Size(45, 15);
            LabelMoves.TabIndex = 94;
            LabelMoves.Text = "Moves:";
            LabelMoves.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Move1
            // 
            Move1.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Move1.Location = new Point(296, 264);
            Move1.Margin = new Padding(4, 3, 4, 3);
            Move1.Name = "Move1";
            Move1.ReadOnly = true;
            Move1.Size = new Size(133, 22);
            Move1.TabIndex = 95;
            Move1.Click += Move_Clicked;
            // 
            // Move2
            // 
            Move2.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Move2.Location = new Point(434, 264);
            Move2.Margin = new Padding(4, 3, 4, 3);
            Move2.Name = "Move2";
            Move2.ReadOnly = true;
            Move2.Size = new Size(133, 22);
            Move2.TabIndex = 96;
            Move2.Click += Move_Clicked;
            // 
            // Move4
            // 
            Move4.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Move4.Location = new Point(434, 292);
            Move4.Margin = new Padding(4, 3, 4, 3);
            Move4.Name = "Move4";
            Move4.ReadOnly = true;
            Move4.Size = new Size(133, 22);
            Move4.TabIndex = 98;
            Move4.Click += Move_Clicked;
            // 
            // Move3
            // 
            Move3.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Move3.Location = new Point(296, 292);
            Move3.Margin = new Padding(4, 3, 4, 3);
            Move3.Name = "Move3";
            Move3.ReadOnly = true;
            Move3.Size = new Size(133, 22);
            Move3.TabIndex = 97;
            Move3.Click += Move_Clicked;
            // 
            // Nature
            // 
            Nature.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Nature.Location = new Point(470, 180);
            Nature.Margin = new Padding(4, 3, 4, 3);
            Nature.Name = "Nature";
            Nature.ReadOnly = true;
            Nature.Size = new Size(97, 22);
            Nature.TabIndex = 106;
            // 
            // LabelNature
            // 
            LabelNature.AutoSize = true;
            LabelNature.Location = new Point(417, 184);
            LabelNature.Name = "LabelNature";
            LabelNature.Size = new Size(46, 15);
            LabelNature.TabIndex = 105;
            LabelNature.Text = "Nature:";
            LabelNature.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Gender
            // 
            Gender.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Gender.Location = new Point(296, 180);
            Gender.Margin = new Padding(4, 3, 4, 3);
            Gender.Name = "Gender";
            Gender.ReadOnly = true;
            Gender.Size = new Size(95, 22);
            Gender.TabIndex = 104;
            // 
            // LabelGender
            // 
            LabelGender.AutoSize = true;
            LabelGender.Location = new Point(241, 184);
            LabelGender.Name = "LabelGender";
            LabelGender.Size = new Size(48, 15);
            LabelGender.TabIndex = 103;
            LabelGender.Text = "Gender:";
            LabelGender.TextAlign = ContentAlignment.MiddleRight;
            // 
            // StopFilter
            // 
            StopFilter.Location = new Point(12, 273);
            StopFilter.Name = "StopFilter";
            StopFilter.Size = new Size(97, 23);
            StopFilter.TabIndex = 107;
            StopFilter.Text = "Edit Filters";
            StopFilter.UseVisualStyleBackColor = true;
            StopFilter.Click += StopFilter_Click;
            // 
            // Sprite
            // 
            Sprite.Location = new Point(498, 7);
            Sprite.Name = "Sprite";
            Sprite.Size = new Size(68, 56);
            Sprite.SizeMode = PictureBoxSizeMode.CenterImage;
            Sprite.TabIndex = 108;
            Sprite.TabStop = false;
            // 
            // Ability
            // 
            Ability.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Ability.Location = new Point(296, 124);
            Ability.Margin = new Padding(4, 3, 4, 3);
            Ability.Name = "Ability";
            Ability.ReadOnly = true;
            Ability.Size = new Size(271, 22);
            Ability.TabIndex = 110;
            // 
            // LabelAbility
            // 
            LabelAbility.AutoSize = true;
            LabelAbility.Location = new Point(245, 127);
            LabelAbility.Name = "LabelAbility";
            LabelAbility.Size = new Size(44, 15);
            LabelAbility.TabIndex = 109;
            LabelAbility.Text = "Ability:";
            LabelAbility.TextAlign = ContentAlignment.MiddleRight;
            // 
            // GemIcon
            // 
            GemIcon.Location = new Point(434, 7);
            GemIcon.Name = "GemIcon";
            GemIcon.Size = new Size(56, 56);
            GemIcon.SizeMode = PictureBoxSizeMode.Zoom;
            GemIcon.TabIndex = 111;
            GemIcon.TabStop = false;
            // 
            // ButtonDownloadEvents
            // 
            ButtonDownloadEvents.Enabled = false;
            ButtonDownloadEvents.Location = new Point(104, 50);
            ButtonDownloadEvents.Name = "ButtonDownloadEvents";
            ButtonDownloadEvents.Size = new Size(90, 25);
            ButtonDownloadEvents.TabIndex = 112;
            ButtonDownloadEvents.Text = "Pull Events";
            ButtonDownloadEvents.UseVisualStyleBackColor = true;
            ButtonDownloadEvents.Click += DownloadEvents_Click;
            // 
            // ConfigSettings
            // 
            ConfigSettings.Location = new Point(12, 302);
            ConfigSettings.Name = "ConfigSettings";
            ConfigSettings.Size = new Size(203, 23);
            ConfigSettings.TabIndex = 115;
            ConfigSettings.Text = "Open Settings";
            ConfigSettings.UseVisualStyleBackColor = true;
            ConfigSettings.Click += ConfigSettings_Click;
            // 
            // Rewards
            // 
            Rewards.Location = new Point(104, 78);
            Rewards.Name = "Rewards";
            Rewards.Size = new Size(90, 25);
            Rewards.TabIndex = 116;
            Rewards.Text = "Rewards";
            Rewards.UseVisualStyleBackColor = true;
            Rewards.Click += Rewards_Click;
            // 
            // LabelSandwichBonus
            // 
            LabelSandwichBonus.AutoSize = true;
            LabelSandwichBonus.Location = new Point(13, 248);
            LabelSandwichBonus.Name = "LabelSandwichBonus";
            LabelSandwichBonus.Size = new Size(120, 15);
            LabelSandwichBonus.TabIndex = 118;
            LabelSandwichBonus.Text = "Raid Sandwich Boost:";
            // 
            // RaidBoost
            // 
            RaidBoost.FormattingEnabled = true;
            RaidBoost.Items.AddRange(new object[] { "0", "1", "2", "3" });
            RaidBoost.Location = new Point(165, 245);
            RaidBoost.Name = "RaidBoost";
            RaidBoost.Size = new Size(48, 23);
            RaidBoost.TabIndex = 117;
            RaidBoost.Text = "w";
            RaidBoost.SelectedIndexChanged += RaidBoost_SelectedIndexChanged;
            // 
            // ComboIndex
            // 
            ComboIndex.BackColor = SystemColors.Window;
            ComboIndex.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboIndex.FormattingEnabled = true;
            ComboIndex.Location = new Point(80, 68);
            ComboIndex.Name = "ComboIndex";
            ComboIndex.Size = new Size(64, 23);
            ComboIndex.TabIndex = 120;
            ComboIndex.Enabled = false;
            ComboIndex.SelectedIndexChanged += ComboIndex_SelectedIndexChanged;
            // 
            // SendScreenshot
            // 
            SendScreenshot.Location = new Point(6, 50);
            SendScreenshot.Name = "SendScreenshot";
            SendScreenshot.Size = new Size(90, 25);
            SendScreenshot.TabIndex = 121;
            SendScreenshot.Text = "Screenshot";
            SendScreenshot.UseVisualStyleBackColor = true;
            SendScreenshot.Click += SendScreenshot_Click;
            // 
            // SearchTimer
            // 
            SearchTimer.Elapsed += SearchTimer_Elapsed;
            // 
            // btnOpenMap
            // 
            btnOpenMap.Location = new Point(6, 78);
            btnOpenMap.Name = "btnOpenMap";
            btnOpenMap.Size = new Size(90, 25);
            btnOpenMap.TabIndex = 124;
            btnOpenMap.Text = "Open Map";
            btnOpenMap.UseVisualStyleBackColor = true;
            btnOpenMap.Click += DisplayMap;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ButtonViewRAM);
            groupBox1.Controls.Add(ButtonDownloadEvents);
            groupBox1.Controls.Add(btnOpenMap);
            groupBox1.Controls.Add(SendScreenshot);
            groupBox1.Controls.Add(Rewards);
            groupBox1.Controls.Add(ButtonReadRaids);
            groupBox1.Location = new Point(13, 129);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(200, 110);
            groupBox1.TabIndex = 125;
            groupBox1.TabStop = false;
            groupBox1.Text = "Raid Controls";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { StatusLabel, ToolStripStatusLabel });
            statusStrip1.Location = new Point(0, 335);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(580, 22);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 126;
            statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Size = new Size(42, 17);
            StatusLabel.Text = "Status:";
            // 
            // ToolStripStatusLabel
            // 
            ToolStripStatusLabel.Name = "ToolStripStatusLabel";
            ToolStripStatusLabel.Size = new Size(89, 17);
            ToolStripStatusLabel.Text = "Not connected.";
            // 
            // USB_Port_label
            // 
            USB_Port_label.AutoSize = true;
            USB_Port_label.Location = new Point(13, 9);
            USB_Port_label.Name = "USB_Port_label";
            USB_Port_label.Size = new Size(56, 15);
            USB_Port_label.TabIndex = 127;
            USB_Port_label.Text = "USB Port:";
            // 
            // USB_Port_TB
            // 
            USB_Port_TB.Location = new Point(84, 6);
            USB_Port_TB.Name = "USB_Port_TB";
            USB_Port_TB.Size = new Size(129, 23);
            USB_Port_TB.TabIndex = 128;
            USB_Port_TB.Text = "w";
            USB_Port_TB.TextAlign = HorizontalAlignment.Center;
            USB_Port_TB.TextChanged += USB_Port_Changed;
            // 
            // StopAdvance_Button
            // 
            StopAdvance_Button.Location = new Point(117, 98);
            StopAdvance_Button.Name = "StopAdvance_Button";
            StopAdvance_Button.Size = new Size(96, 27);
            StopAdvance_Button.TabIndex = 129;
            StopAdvance_Button.Text = "Stop";
            StopAdvance_Button.Visible = false;
            StopAdvance_Button.UseVisualStyleBackColor = true;
            StopAdvance_Button.Click += new EventHandler(StopAdvanceButton_Click);
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(580, 357);
            Controls.Add(StopAdvance_Button);
            Controls.Add(USB_Port_TB);
            Controls.Add(USB_Port_label);
            Controls.Add(statusStrip1);
            Controls.Add(groupBox1);
            Controls.Add(ComboIndex);
            Controls.Add(CheckEnableFilters);
            Controls.Add(LabelSandwichBonus);
            Controls.Add(LabelLoadedRaids);
            Controls.Add(RaidBoost);
            Controls.Add(ConfigSettings);
            Controls.Add(GemIcon);
            Controls.Add(Ability);
            Controls.Add(LabelAbility);
            Controls.Add(Sprite);
            Controls.Add(StopFilter);
            Controls.Add(Nature);
            Controls.Add(LabelNature);
            Controls.Add(Gender);
            Controls.Add(LabelGender);
            Controls.Add(Move4);
            Controls.Add(Move3);
            Controls.Add(Move2);
            Controls.Add(Move1);
            Controls.Add(LabelMoves);
            Controls.Add(Species);
            Controls.Add(LabelSpecies);
            Controls.Add(Difficulty);
            Controls.Add(LabelDifficulty);
            Controls.Add(labelEvent);
            Controls.Add(ButtonAdvanceDate);
            Controls.Add(IVs);
            Controls.Add(LabelIVs);
            Controls.Add(Area);
            Controls.Add(LabelUNK_2);
            Controls.Add(ButtonNext);
            Controls.Add(ButtonPrevious);
            Controls.Add(TeraType);
            Controls.Add(LabelTeraType);
            Controls.Add(PID);
            Controls.Add(LabelPID);
            Controls.Add(EC);
            Controls.Add(LabelEC);
            Controls.Add(Seed);
            Controls.Add(LabelSeed);
            Controls.Add(ButtonDisconnect);
            Controls.Add(ButtonConnect);
            Controls.Add(InputSwitchIP);
            Controls.Add(LabelSwitchIP);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainWindow";
            FormClosing += MainWindow_FormClosing;
            Load += MainWindow_Load;
            ((System.ComponentModel.ISupportInitialize)Sprite).EndInit();
            ((System.ComponentModel.ISupportInitialize)GemIcon).EndInit();
            groupBox1.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolTip toolTip;
        private Button ButtonDisconnect;
        private Button ButtonConnect;
        private TextBox InputSwitchIP;
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
        private Button ButtonNext;
        private Button ButtonPrevious;
        private TextBox Area;
        private Label LabelUNK_2;
        private TextBox IVs;
        private Label LabelIVs;
        private Button ButtonReadRaids;
        private Button ButtonAdvanceDate;
        private Label labelEvent;
        private TextBox Difficulty;
        private Label LabelDifficulty;
        private Button ButtonViewRAM;
        private TextBox Species;
        private Label LabelSpecies;
        private Label LabelMoves;
        private TextBox Move1;
        private TextBox Move2;
        private TextBox Move4;
        private TextBox Move3;
        private TextBox Nature;
        private Label LabelNature;
        private TextBox Gender;
        private Label LabelGender;
        private Button StopFilter;
        private PictureBox Sprite;
        private TextBox Ability;
        private Label LabelAbility;
        private PictureBox GemIcon;
        private Button ButtonDownloadEvents;
        private Button ConfigSettings;
        private Button Rewards;
        private Label LabelSandwichBonus;
        private ComboBox RaidBoost;
        private CheckBox CheckEnableFilters;
        private ComboBox ComboIndex;
        private Button SendScreenshot;
        private System.Timers.Timer SearchTimer;
        private Button btnOpenMap;
        private GroupBox groupBox1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel StatusLabel;
        private ToolStripStatusLabel ToolStripStatusLabel;
        private Label USB_Port_label;
        private TextBox USB_Port_TB;
        private Button StopAdvance_Button;
    }
}