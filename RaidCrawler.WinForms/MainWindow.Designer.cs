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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ButtonAdvanceDate = new System.Windows.Forms.Button();
            this.CheckEnableFilters = new System.Windows.Forms.CheckBox();
            this.ButtonDisconnect = new System.Windows.Forms.Button();
            this.ButtonConnect = new System.Windows.Forms.Button();
            this.InputSwitchIP = new System.Windows.Forms.TextBox();
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
            this.ButtonNext = new System.Windows.Forms.Button();
            this.ButtonPrevious = new System.Windows.Forms.Button();
            this.Area = new System.Windows.Forms.TextBox();
            this.LabelUNK_2 = new System.Windows.Forms.Label();
            this.IVs = new System.Windows.Forms.TextBox();
            this.LabelIVs = new System.Windows.Forms.Label();
            this.ButtonReadRaids = new System.Windows.Forms.Button();
            this.labelEvent = new System.Windows.Forms.Label();
            this.Difficulty = new System.Windows.Forms.TextBox();
            this.LabelDifficulty = new System.Windows.Forms.Label();
            this.ButtonViewRAM = new System.Windows.Forms.Button();
            this.Species = new System.Windows.Forms.TextBox();
            this.LabelSpecies = new System.Windows.Forms.Label();
            this.LabelMoves = new System.Windows.Forms.Label();
            this.Move1 = new System.Windows.Forms.TextBox();
            this.Move2 = new System.Windows.Forms.TextBox();
            this.Move4 = new System.Windows.Forms.TextBox();
            this.Move3 = new System.Windows.Forms.TextBox();
            this.Nature = new System.Windows.Forms.TextBox();
            this.LabelNature = new System.Windows.Forms.Label();
            this.Gender = new System.Windows.Forms.TextBox();
            this.LabelGender = new System.Windows.Forms.Label();
            this.StopFilter = new System.Windows.Forms.Button();
            this.Sprite = new System.Windows.Forms.PictureBox();
            this.Ability = new System.Windows.Forms.TextBox();
            this.LabelAbility = new System.Windows.Forms.Label();
            this.GemIcon = new System.Windows.Forms.PictureBox();
            this.ButtonDownloadEvents = new System.Windows.Forms.Button();
            this.ConfigSettings = new System.Windows.Forms.Button();
            this.Rewards = new System.Windows.Forms.Button();
            this.LabelSandwichBonus = new System.Windows.Forms.Label();
            this.RaidBoost = new System.Windows.Forms.ComboBox();
            this.ComboIndex = new System.Windows.Forms.ComboBox();
            this.SendScreenshot = new System.Windows.Forms.Button();
            this.SearchTimer = new System.Windows.Forms.Timer(this.components);
            this.btnOpenMap = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.USB_Port_label = new System.Windows.Forms.Label();
            this.USB_Port_TB = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Sprite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GemIcon)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonAdvanceDate
            // 
            this.ButtonAdvanceDate.Enabled = false;
            this.ButtonAdvanceDate.Location = new System.Drawing.Point(117, 98);
            this.ButtonAdvanceDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ButtonAdvanceDate.Name = "ButtonAdvanceDate";
            this.ButtonAdvanceDate.Size = new System.Drawing.Size(96, 27);
            this.ButtonAdvanceDate.TabIndex = 81;
            this.ButtonAdvanceDate.Text = "Advance Date";
            this.toolTip.SetToolTip(this.ButtonAdvanceDate, "Advance Date performs one (1) time set.\r\n\r\nIf Stop Filters are defined, Advance D" +
        "ate\r\ncontinues advancing the date until a stop\r\nfilter has been hit.");
            this.ButtonAdvanceDate.UseVisualStyleBackColor = true;
            this.ButtonAdvanceDate.Click += new System.EventHandler(this.ButtonAdvanceDate_Click);
            // 
            // CheckEnableFilters
            // 
            this.CheckEnableFilters.AutoSize = true;
            this.CheckEnableFilters.Checked = true;
            this.CheckEnableFilters.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckEnableFilters.Location = new System.Drawing.Point(117, 276);
            this.CheckEnableFilters.Name = "CheckEnableFilters";
            this.CheckEnableFilters.Size = new System.Drawing.Size(95, 19);
            this.CheckEnableFilters.TabIndex = 119;
            this.CheckEnableFilters.Text = "Enable Filters";
            this.toolTip.SetToolTip(this.CheckEnableFilters, "Enable Filters enables or disables all filters\r\nentirely.\r\n\r\nEnabled - Advance Da" +
        "te will continue until\r\na match occurs from a filter.\r\n\r\nDisabled - Advance Date" +
        " will only advance\r\none (1) day.");
            this.CheckEnableFilters.UseVisualStyleBackColor = true;
            // 
            // ButtonDisconnect
            // 
            this.ButtonDisconnect.Enabled = false;
            this.ButtonDisconnect.Location = new System.Drawing.Point(117, 35);
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
            this.ButtonConnect.Location = new System.Drawing.Point(13, 35);
            this.ButtonConnect.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ButtonConnect.Name = "ButtonConnect";
            this.ButtonConnect.Size = new System.Drawing.Size(97, 27);
            this.ButtonConnect.TabIndex = 10;
            this.ButtonConnect.Text = "Connect";
            this.ButtonConnect.UseVisualStyleBackColor = true;
            this.ButtonConnect.Click += new System.EventHandler(this.ButtonConnect_Click);
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
            this.LabelLoadedRaids.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LabelLoadedRaids.Location = new System.Drawing.Point(12, 103);
            this.LabelLoadedRaids.Name = "LabelLoadedRaids";
            this.LabelLoadedRaids.Size = new System.Drawing.Size(67, 15);
            this.LabelLoadedRaids.TabIndex = 12;
            this.LabelLoadedRaids.Text = "Matches: 0";
            // 
            // TeraType
            // 
            this.TeraType.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TeraType.Location = new System.Drawing.Point(296, 152);
            this.TeraType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TeraType.Name = "TeraType";
            this.TeraType.ReadOnly = true;
            this.TeraType.Size = new System.Drawing.Size(95, 22);
            this.TeraType.TabIndex = 49;
            // 
            // LabelTeraType
            // 
            this.LabelTeraType.AutoSize = true;
            this.LabelTeraType.Location = new System.Drawing.Point(232, 156);
            this.LabelTeraType.Name = "LabelTeraType";
            this.LabelTeraType.Size = new System.Drawing.Size(58, 15);
            this.LabelTeraType.TabIndex = 48;
            this.LabelTeraType.Text = "Tera Type:";
            this.LabelTeraType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PID
            // 
            this.PID.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PID.Location = new System.Drawing.Point(296, 68);
            this.PID.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PID.Name = "PID";
            this.PID.ReadOnly = true;
            this.PID.Size = new System.Drawing.Size(95, 22);
            this.PID.TabIndex = 47;
            // 
            // LabelPID
            // 
            this.LabelPID.AutoSize = true;
            this.LabelPID.Location = new System.Drawing.Point(261, 70);
            this.LabelPID.Name = "LabelPID";
            this.LabelPID.Size = new System.Drawing.Size(28, 15);
            this.LabelPID.TabIndex = 46;
            this.LabelPID.Text = "PID:";
            this.LabelPID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // EC
            // 
            this.EC.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.EC.Location = new System.Drawing.Point(296, 40);
            this.EC.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.EC.Name = "EC";
            this.EC.ReadOnly = true;
            this.EC.Size = new System.Drawing.Size(95, 22);
            this.EC.TabIndex = 45;
            // 
            // LabelEC
            // 
            this.LabelEC.AutoSize = true;
            this.LabelEC.Location = new System.Drawing.Point(265, 42);
            this.LabelEC.Name = "LabelEC";
            this.LabelEC.Size = new System.Drawing.Size(24, 15);
            this.LabelEC.TabIndex = 44;
            this.LabelEC.Text = "EC:";
            this.LabelEC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Seed
            // 
            this.Seed.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Seed.Location = new System.Drawing.Point(296, 12);
            this.Seed.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Seed.Name = "Seed";
            this.Seed.ReadOnly = true;
            this.Seed.Size = new System.Drawing.Size(95, 22);
            this.Seed.TabIndex = 43;
            this.Seed.Click += new System.EventHandler(this.Seed_Click);
            // 
            // LabelSeed
            // 
            this.LabelSeed.AutoSize = true;
            this.LabelSeed.Location = new System.Drawing.Point(254, 14);
            this.LabelSeed.Name = "LabelSeed";
            this.LabelSeed.Size = new System.Drawing.Size(35, 15);
            this.LabelSeed.TabIndex = 42;
            this.LabelSeed.Text = "Seed:";
            this.LabelSeed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ButtonNext
            // 
            this.ButtonNext.Enabled = false;
            this.ButtonNext.Location = new System.Drawing.Point(149, 68);
            this.ButtonNext.Name = "ButtonNext";
            this.ButtonNext.Size = new System.Drawing.Size(45, 25);
            this.ButtonNext.TabIndex = 56;
            this.ButtonNext.Text = ">>";
            this.ButtonNext.UseVisualStyleBackColor = true;
            this.ButtonNext.Click += new System.EventHandler(this.ButtonNext_Click);
            // 
            // ButtonPrevious
            // 
            this.ButtonPrevious.Enabled = false;
            this.ButtonPrevious.Location = new System.Drawing.Point(30, 68);
            this.ButtonPrevious.Name = "ButtonPrevious";
            this.ButtonPrevious.Size = new System.Drawing.Size(45, 25);
            this.ButtonPrevious.TabIndex = 55;
            this.ButtonPrevious.Text = "<<";
            this.ButtonPrevious.UseVisualStyleBackColor = true;
            this.ButtonPrevious.Click += new System.EventHandler(this.ButtonPrevious_Click);
            // 
            // Area
            // 
            this.Area.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Area.Location = new System.Drawing.Point(296, 236);
            this.Area.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Area.Name = "Area";
            this.Area.ReadOnly = true;
            this.Area.Size = new System.Drawing.Size(271, 22);
            this.Area.TabIndex = 61;
            this.Area.Click += new System.EventHandler(this.DisplayMap);
            // 
            // LabelUNK_2
            // 
            this.LabelUNK_2.AutoSize = true;
            this.LabelUNK_2.Location = new System.Drawing.Point(256, 240);
            this.LabelUNK_2.Name = "LabelUNK_2";
            this.LabelUNK_2.Size = new System.Drawing.Size(34, 15);
            this.LabelUNK_2.TabIndex = 60;
            this.LabelUNK_2.Text = "Area:";
            this.LabelUNK_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IVs
            // 
            this.IVs.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.IVs.Location = new System.Drawing.Point(296, 208);
            this.IVs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.IVs.Name = "IVs";
            this.IVs.ReadOnly = true;
            this.IVs.Size = new System.Drawing.Size(271, 22);
            this.IVs.TabIndex = 69;
            // 
            // LabelIVs
            // 
            this.LabelIVs.AutoSize = true;
            this.LabelIVs.Location = new System.Drawing.Point(265, 212);
            this.LabelIVs.Name = "LabelIVs";
            this.LabelIVs.Size = new System.Drawing.Size(25, 15);
            this.LabelIVs.TabIndex = 68;
            this.LabelIVs.Text = "IVs:";
            this.LabelIVs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ButtonReadRaids
            // 
            this.ButtonReadRaids.Enabled = false;
            this.ButtonReadRaids.Location = new System.Drawing.Point(6, 22);
            this.ButtonReadRaids.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ButtonReadRaids.Name = "ButtonReadRaids";
            this.ButtonReadRaids.Size = new System.Drawing.Size(90, 25);
            this.ButtonReadRaids.TabIndex = 80;
            this.ButtonReadRaids.Text = "Read Raids";
            this.ButtonReadRaids.UseVisualStyleBackColor = true;
            this.ButtonReadRaids.Click += new System.EventHandler(this.ButtonReadRaids_Click);
            // 
            // labelEvent
            // 
            this.labelEvent.AutoSize = true;
            this.labelEvent.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.labelEvent.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelEvent.Location = new System.Drawing.Point(496, 64);
            this.labelEvent.Name = "labelEvent";
            this.labelEvent.Size = new System.Drawing.Size(73, 15);
            this.labelEvent.TabIndex = 84;
            this.labelEvent.Text = "~~Event~~";
            this.labelEvent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelEvent.Visible = false;
            // 
            // Difficulty
            // 
            this.Difficulty.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Difficulty.Location = new System.Drawing.Point(470, 152);
            this.Difficulty.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Difficulty.Name = "Difficulty";
            this.Difficulty.ReadOnly = true;
            this.Difficulty.Size = new System.Drawing.Size(97, 22);
            this.Difficulty.TabIndex = 86;
            // 
            // LabelDifficulty
            // 
            this.LabelDifficulty.AutoSize = true;
            this.LabelDifficulty.Location = new System.Drawing.Point(405, 156);
            this.LabelDifficulty.Name = "LabelDifficulty";
            this.LabelDifficulty.Size = new System.Drawing.Size(58, 15);
            this.LabelDifficulty.TabIndex = 85;
            this.LabelDifficulty.Text = "Difficulty:";
            this.LabelDifficulty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ButtonViewRAM
            // 
            this.ButtonViewRAM.Enabled = false;
            this.ButtonViewRAM.Location = new System.Drawing.Point(104, 22);
            this.ButtonViewRAM.Name = "ButtonViewRAM";
            this.ButtonViewRAM.Size = new System.Drawing.Size(90, 25);
            this.ButtonViewRAM.TabIndex = 89;
            this.ButtonViewRAM.Text = "Dump Raid";
            this.ButtonViewRAM.UseVisualStyleBackColor = true;
            this.ButtonViewRAM.Click += new System.EventHandler(this.ViewRAM_Click);
            // 
            // Species
            // 
            this.Species.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Species.Location = new System.Drawing.Point(296, 96);
            this.Species.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Species.Name = "Species";
            this.Species.ReadOnly = true;
            this.Species.Size = new System.Drawing.Size(271, 22);
            this.Species.TabIndex = 93;
            // 
            // LabelSpecies
            // 
            this.LabelSpecies.AutoSize = true;
            this.LabelSpecies.Location = new System.Drawing.Point(241, 100);
            this.LabelSpecies.Name = "LabelSpecies";
            this.LabelSpecies.Size = new System.Drawing.Size(49, 15);
            this.LabelSpecies.TabIndex = 92;
            this.LabelSpecies.Text = "Species:";
            this.LabelSpecies.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelMoves
            // 
            this.LabelMoves.AutoSize = true;
            this.LabelMoves.Location = new System.Drawing.Point(244, 280);
            this.LabelMoves.Name = "LabelMoves";
            this.LabelMoves.Size = new System.Drawing.Size(45, 15);
            this.LabelMoves.TabIndex = 94;
            this.LabelMoves.Text = "Moves:";
            this.LabelMoves.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Move1
            // 
            this.Move1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Move1.Location = new System.Drawing.Point(296, 264);
            this.Move1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Move1.Name = "Move1";
            this.Move1.ReadOnly = true;
            this.Move1.Size = new System.Drawing.Size(133, 22);
            this.Move1.TabIndex = 95;
            this.Move1.Click += new System.EventHandler(this.Move_Clicked);
            // 
            // Move2
            // 
            this.Move2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Move2.Location = new System.Drawing.Point(434, 264);
            this.Move2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Move2.Name = "Move2";
            this.Move2.ReadOnly = true;
            this.Move2.Size = new System.Drawing.Size(133, 22);
            this.Move2.TabIndex = 96;
            this.Move2.Click += new System.EventHandler(this.Move_Clicked);
            // 
            // Move4
            // 
            this.Move4.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Move4.Location = new System.Drawing.Point(434, 292);
            this.Move4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Move4.Name = "Move4";
            this.Move4.ReadOnly = true;
            this.Move4.Size = new System.Drawing.Size(133, 22);
            this.Move4.TabIndex = 98;
            this.Move4.Click += new System.EventHandler(this.Move_Clicked);
            // 
            // Move3
            // 
            this.Move3.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Move3.Location = new System.Drawing.Point(296, 292);
            this.Move3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Move3.Name = "Move3";
            this.Move3.ReadOnly = true;
            this.Move3.Size = new System.Drawing.Size(133, 22);
            this.Move3.TabIndex = 97;
            this.Move3.Click += new System.EventHandler(this.Move_Clicked);
            // 
            // Nature
            // 
            this.Nature.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Nature.Location = new System.Drawing.Point(470, 180);
            this.Nature.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Nature.Name = "Nature";
            this.Nature.ReadOnly = true;
            this.Nature.Size = new System.Drawing.Size(97, 22);
            this.Nature.TabIndex = 106;
            // 
            // LabelNature
            // 
            this.LabelNature.AutoSize = true;
            this.LabelNature.Location = new System.Drawing.Point(417, 184);
            this.LabelNature.Name = "LabelNature";
            this.LabelNature.Size = new System.Drawing.Size(46, 15);
            this.LabelNature.TabIndex = 105;
            this.LabelNature.Text = "Nature:";
            this.LabelNature.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Gender
            // 
            this.Gender.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Gender.Location = new System.Drawing.Point(296, 180);
            this.Gender.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Gender.Name = "Gender";
            this.Gender.ReadOnly = true;
            this.Gender.Size = new System.Drawing.Size(95, 22);
            this.Gender.TabIndex = 104;
            // 
            // LabelGender
            // 
            this.LabelGender.AutoSize = true;
            this.LabelGender.Location = new System.Drawing.Point(241, 184);
            this.LabelGender.Name = "LabelGender";
            this.LabelGender.Size = new System.Drawing.Size(48, 15);
            this.LabelGender.TabIndex = 103;
            this.LabelGender.Text = "Gender:";
            this.LabelGender.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StopFilter
            // 
            this.StopFilter.Location = new System.Drawing.Point(12, 273);
            this.StopFilter.Name = "StopFilter";
            this.StopFilter.Size = new System.Drawing.Size(97, 23);
            this.StopFilter.TabIndex = 107;
            this.StopFilter.Text = "Edit Filters";
            this.StopFilter.UseVisualStyleBackColor = true;
            this.StopFilter.Click += new System.EventHandler(this.StopFilter_Click);
            // 
            // Sprite
            // 
            this.Sprite.Location = new System.Drawing.Point(498, 7);
            this.Sprite.Name = "Sprite";
            this.Sprite.Size = new System.Drawing.Size(68, 56);
            this.Sprite.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Sprite.TabIndex = 108;
            this.Sprite.TabStop = false;
            // 
            // Ability
            // 
            this.Ability.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Ability.Location = new System.Drawing.Point(296, 124);
            this.Ability.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Ability.Name = "Ability";
            this.Ability.ReadOnly = true;
            this.Ability.Size = new System.Drawing.Size(271, 22);
            this.Ability.TabIndex = 110;
            // 
            // LabelAbility
            // 
            this.LabelAbility.AutoSize = true;
            this.LabelAbility.Location = new System.Drawing.Point(245, 127);
            this.LabelAbility.Name = "LabelAbility";
            this.LabelAbility.Size = new System.Drawing.Size(44, 15);
            this.LabelAbility.TabIndex = 109;
            this.LabelAbility.Text = "Ability:";
            this.LabelAbility.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GemIcon
            // 
            this.GemIcon.Location = new System.Drawing.Point(434, 7);
            this.GemIcon.Name = "GemIcon";
            this.GemIcon.Size = new System.Drawing.Size(56, 56);
            this.GemIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.GemIcon.TabIndex = 111;
            this.GemIcon.TabStop = false;
            // 
            // ButtonDownloadEvents
            // 
            this.ButtonDownloadEvents.Enabled = false;
            this.ButtonDownloadEvents.Location = new System.Drawing.Point(104, 50);
            this.ButtonDownloadEvents.Name = "ButtonDownloadEvents";
            this.ButtonDownloadEvents.Size = new System.Drawing.Size(90, 25);
            this.ButtonDownloadEvents.TabIndex = 112;
            this.ButtonDownloadEvents.Text = "Pull Events";
            this.ButtonDownloadEvents.UseVisualStyleBackColor = true;
            this.ButtonDownloadEvents.Click += new System.EventHandler(this.DownloadEvents_Click);
            // 
            // ConfigSettings
            // 
            this.ConfigSettings.Location = new System.Drawing.Point(12, 302);
            this.ConfigSettings.Name = "ConfigSettings";
            this.ConfigSettings.Size = new System.Drawing.Size(203, 23);
            this.ConfigSettings.TabIndex = 115;
            this.ConfigSettings.Text = "Open Settings";
            this.ConfigSettings.UseVisualStyleBackColor = true;
            this.ConfigSettings.Click += new System.EventHandler(this.ConfigSettings_Click);
            // 
            // Rewards
            // 
            this.Rewards.Location = new System.Drawing.Point(104, 78);
            this.Rewards.Name = "Rewards";
            this.Rewards.Size = new System.Drawing.Size(90, 25);
            this.Rewards.TabIndex = 116;
            this.Rewards.Text = "Rewards";
            this.Rewards.UseVisualStyleBackColor = true;
            this.Rewards.Click += new System.EventHandler(this.Rewards_Click);
            // 
            // LabelSandwichBonus
            // 
            this.LabelSandwichBonus.AutoSize = true;
            this.LabelSandwichBonus.Location = new System.Drawing.Point(13, 248);
            this.LabelSandwichBonus.Name = "LabelSandwichBonus";
            this.LabelSandwichBonus.Size = new System.Drawing.Size(120, 15);
            this.LabelSandwichBonus.TabIndex = 118;
            this.LabelSandwichBonus.Text = "Raid Sandwich Boost:";
            // 
            // RaidBoost
            // 
            this.RaidBoost.FormattingEnabled = true;
            this.RaidBoost.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.RaidBoost.Location = new System.Drawing.Point(165, 245);
            this.RaidBoost.Name = "RaidBoost";
            this.RaidBoost.Size = new System.Drawing.Size(48, 23);
            this.RaidBoost.TabIndex = 117;
            this.RaidBoost.Text = "w";
            this.RaidBoost.SelectedIndexChanged += new System.EventHandler(this.RaidBoost_SelectedIndexChanged);
            // 
            // ComboIndex
            // 
            this.ComboIndex.BackColor = System.Drawing.SystemColors.Window;
            this.ComboIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboIndex.FormattingEnabled = true;
            this.ComboIndex.Location = new System.Drawing.Point(80, 68);
            this.ComboIndex.Name = "ComboIndex";
            this.ComboIndex.Size = new System.Drawing.Size(64, 23);
            this.ComboIndex.TabIndex = 120;
            this.ComboIndex.SelectedIndexChanged += new System.EventHandler(this.ComboIndex_SelectedIndexChanged);
            // 
            // SendScreenshot
            // 
            this.SendScreenshot.Location = new System.Drawing.Point(6, 50);
            this.SendScreenshot.Name = "SendScreenshot";
            this.SendScreenshot.Size = new System.Drawing.Size(90, 25);
            this.SendScreenshot.TabIndex = 121;
            this.SendScreenshot.Text = "Screenshot";
            this.SendScreenshot.UseVisualStyleBackColor = true;
            this.SendScreenshot.Click += new System.EventHandler(this.SendScreenshot_Click);
            // 
            // SearchTimer
            // 
            this.SearchTimer.Tick += new System.EventHandler(this.SearchTimer_Tick);
            // 
            // btnOpenMap
            // 
            this.btnOpenMap.Location = new System.Drawing.Point(6, 78);
            this.btnOpenMap.Name = "btnOpenMap";
            this.btnOpenMap.Size = new System.Drawing.Size(90, 25);
            this.btnOpenMap.TabIndex = 124;
            this.btnOpenMap.Text = "Open Map";
            this.btnOpenMap.UseVisualStyleBackColor = true;
            this.btnOpenMap.Click += new System.EventHandler(this.DisplayMap);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ButtonViewRAM);
            this.groupBox1.Controls.Add(this.ButtonDownloadEvents);
            this.groupBox1.Controls.Add(this.btnOpenMap);
            this.groupBox1.Controls.Add(this.SendScreenshot);
            this.groupBox1.Controls.Add(this.Rewards);
            this.groupBox1.Controls.Add(this.ButtonReadRaids);
            this.groupBox1.Location = new System.Drawing.Point(13, 129);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 110);
            this.groupBox1.TabIndex = 125;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Raid Controls";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.ToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 335);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(580, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 126;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(42, 17);
            this.StatusLabel.Text = "Status:";
            // 
            // ToolStripStatusLabel
            // 
            this.ToolStripStatusLabel.Name = "ToolStripStatusLabel";
            this.ToolStripStatusLabel.Size = new System.Drawing.Size(89, 17);
            this.ToolStripStatusLabel.Text = "Not connected.";
            // 
            // USB_Port_label
            // 
            this.USB_Port_label.AutoSize = true;
            this.USB_Port_label.Location = new System.Drawing.Point(13, 9);
            this.USB_Port_label.Name = "USB_Port_label";
            this.USB_Port_label.Size = new System.Drawing.Size(56, 15);
            this.USB_Port_label.TabIndex = 127;
            this.USB_Port_label.Text = "USB Port:";
            // 
            // USB_Port_TB
            // 
            this.USB_Port_TB.Location = new System.Drawing.Point(84, 6);
            this.USB_Port_TB.Name = "USB_Port_TB";
            this.USB_Port_TB.Size = new System.Drawing.Size(129, 23);
            this.USB_Port_TB.TabIndex = 128;
            this.USB_Port_TB.Text = "w";
            this.USB_Port_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.USB_Port_TB.TextChanged += new System.EventHandler(this.USB_Port_Changed);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 357);
            this.Controls.Add(this.USB_Port_TB);
            this.Controls.Add(this.USB_Port_label);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ComboIndex);
            this.Controls.Add(this.CheckEnableFilters);
            this.Controls.Add(this.LabelSandwichBonus);
            this.Controls.Add(this.LabelLoadedRaids);
            this.Controls.Add(this.RaidBoost);
            this.Controls.Add(this.ConfigSettings);
            this.Controls.Add(this.GemIcon);
            this.Controls.Add(this.Ability);
            this.Controls.Add(this.LabelAbility);
            this.Controls.Add(this.Sprite);
            this.Controls.Add(this.StopFilter);
            this.Controls.Add(this.Nature);
            this.Controls.Add(this.LabelNature);
            this.Controls.Add(this.Gender);
            this.Controls.Add(this.LabelGender);
            this.Controls.Add(this.Move4);
            this.Controls.Add(this.Move3);
            this.Controls.Add(this.Move2);
            this.Controls.Add(this.Move1);
            this.Controls.Add(this.LabelMoves);
            this.Controls.Add(this.Species);
            this.Controls.Add(this.LabelSpecies);
            this.Controls.Add(this.Difficulty);
            this.Controls.Add(this.LabelDifficulty);
            this.Controls.Add(this.labelEvent);
            this.Controls.Add(this.ButtonAdvanceDate);
            this.Controls.Add(this.IVs);
            this.Controls.Add(this.LabelIVs);
            this.Controls.Add(this.Area);
            this.Controls.Add(this.LabelUNK_2);
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
            this.Controls.Add(this.ButtonDisconnect);
            this.Controls.Add(this.ButtonConnect);
            this.Controls.Add(this.InputSwitchIP);
            this.Controls.Add(this.LabelSwitchIP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Sprite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GemIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Timer SearchTimer;
        private Button btnOpenMap;
        private GroupBox groupBox1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel StatusLabel;
        private ToolStripStatusLabel ToolStripStatusLabel;
        private Label USB_Port_label;
        private TextBox USB_Port_TB;
    }
}