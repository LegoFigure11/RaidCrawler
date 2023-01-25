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
            this.toolStripStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.ButtonStopAdvance = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Sprite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GemIcon)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonAdvanceDate
            // 
            this.ButtonAdvanceDate.Enabled = false;
            this.ButtonAdvanceDate.Location = new System.Drawing.Point(167, 163);
            this.ButtonAdvanceDate.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.ButtonAdvanceDate.Name = "ButtonAdvanceDate";
            this.ButtonAdvanceDate.Size = new System.Drawing.Size(137, 45);
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
            this.CheckEnableFilters.Location = new System.Drawing.Point(167, 498);
            this.CheckEnableFilters.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CheckEnableFilters.Name = "CheckEnableFilters";
            this.CheckEnableFilters.Size = new System.Drawing.Size(141, 29);
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
            this.ButtonDisconnect.Location = new System.Drawing.Point(167, 58);
            this.ButtonDisconnect.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.ButtonDisconnect.Name = "ButtonDisconnect";
            this.ButtonDisconnect.Size = new System.Drawing.Size(139, 45);
            this.ButtonDisconnect.TabIndex = 11;
            this.ButtonDisconnect.Text = "Disconnect";
            this.ButtonDisconnect.UseVisualStyleBackColor = true;
            this.ButtonDisconnect.Click += new System.EventHandler(this.Disconnect_Click);
            // 
            // ButtonConnect
            // 
            this.ButtonConnect.Location = new System.Drawing.Point(19, 58);
            this.ButtonConnect.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.ButtonConnect.Name = "ButtonConnect";
            this.ButtonConnect.Size = new System.Drawing.Size(139, 45);
            this.ButtonConnect.TabIndex = 10;
            this.ButtonConnect.Text = "Connect";
            this.ButtonConnect.UseVisualStyleBackColor = true;
            this.ButtonConnect.Click += new System.EventHandler(this.ButtonConnect_Click);
            // 
            // InputSwitchIP
            // 
            this.InputSwitchIP.Location = new System.Drawing.Point(120, 10);
            this.InputSwitchIP.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.InputSwitchIP.Name = "InputSwitchIP";
            this.InputSwitchIP.Size = new System.Drawing.Size(183, 31);
            this.InputSwitchIP.TabIndex = 8;
            this.InputSwitchIP.Text = "www.www.www.www";
            this.InputSwitchIP.TextChanged += new System.EventHandler(this.InputSwitchIP_Changed);
            // 
            // LabelSwitchIP
            // 
            this.LabelSwitchIP.AutoSize = true;
            this.LabelSwitchIP.Location = new System.Drawing.Point(19, 15);
            this.LabelSwitchIP.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.LabelSwitchIP.Name = "LabelSwitchIP";
            this.LabelSwitchIP.Size = new System.Drawing.Size(87, 25);
            this.LabelSwitchIP.TabIndex = 6;
            this.LabelSwitchIP.Text = "Switch IP:";
            // 
            // LabelLoadedRaids
            // 
            this.LabelLoadedRaids.AutoSize = true;
            this.LabelLoadedRaids.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LabelLoadedRaids.Location = new System.Drawing.Point(17, 210);
            this.LabelLoadedRaids.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelLoadedRaids.Name = "LabelLoadedRaids";
            this.LabelLoadedRaids.Size = new System.Drawing.Size(79, 25);
            this.LabelLoadedRaids.TabIndex = 12;
            this.LabelLoadedRaids.Text = "Shiny: 0";
            // 
            // TeraType
            // 
            this.TeraType.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TeraType.Location = new System.Drawing.Point(423, 253);
            this.TeraType.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.TeraType.Name = "TeraType";
            this.TeraType.ReadOnly = true;
            this.TeraType.Size = new System.Drawing.Size(134, 29);
            this.TeraType.TabIndex = 49;
            // 
            // LabelTeraType
            // 
            this.LabelTeraType.AutoSize = true;
            this.LabelTeraType.Location = new System.Drawing.Point(331, 260);
            this.LabelTeraType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelTeraType.Name = "LabelTeraType";
            this.LabelTeraType.Size = new System.Drawing.Size(89, 25);
            this.LabelTeraType.TabIndex = 48;
            this.LabelTeraType.Text = "Tera Type:";
            this.LabelTeraType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PID
            // 
            this.PID.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PID.Location = new System.Drawing.Point(423, 113);
            this.PID.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.PID.Name = "PID";
            this.PID.ReadOnly = true;
            this.PID.Size = new System.Drawing.Size(134, 29);
            this.PID.TabIndex = 47;
            // 
            // LabelPID
            // 
            this.LabelPID.AutoSize = true;
            this.LabelPID.Location = new System.Drawing.Point(373, 117);
            this.LabelPID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelPID.Name = "LabelPID";
            this.LabelPID.Size = new System.Drawing.Size(44, 25);
            this.LabelPID.TabIndex = 46;
            this.LabelPID.Text = "PID:";
            this.LabelPID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // EC
            // 
            this.EC.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.EC.Location = new System.Drawing.Point(423, 67);
            this.EC.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.EC.Name = "EC";
            this.EC.ReadOnly = true;
            this.EC.Size = new System.Drawing.Size(134, 29);
            this.EC.TabIndex = 45;
            // 
            // LabelEC
            // 
            this.LabelEC.AutoSize = true;
            this.LabelEC.Location = new System.Drawing.Point(379, 70);
            this.LabelEC.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelEC.Name = "LabelEC";
            this.LabelEC.Size = new System.Drawing.Size(36, 25);
            this.LabelEC.TabIndex = 44;
            this.LabelEC.Text = "EC:";
            this.LabelEC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Seed
            // 
            this.Seed.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Seed.Location = new System.Drawing.Point(423, 20);
            this.Seed.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Seed.Name = "Seed";
            this.Seed.ReadOnly = true;
            this.Seed.Size = new System.Drawing.Size(134, 29);
            this.Seed.TabIndex = 43;
            this.Seed.Click += new System.EventHandler(this.Seed_Clicked);
            // 
            // LabelSeed
            // 
            this.LabelSeed.AutoSize = true;
            this.LabelSeed.Location = new System.Drawing.Point(363, 23);
            this.LabelSeed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelSeed.Name = "LabelSeed";
            this.LabelSeed.Size = new System.Drawing.Size(55, 25);
            this.LabelSeed.TabIndex = 42;
            this.LabelSeed.Text = "Seed:";
            this.LabelSeed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ButtonNext
            // 
            this.ButtonNext.Enabled = false;
            this.ButtonNext.Location = new System.Drawing.Point(213, 113);
            this.ButtonNext.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ButtonNext.Name = "ButtonNext";
            this.ButtonNext.Size = new System.Drawing.Size(64, 42);
            this.ButtonNext.TabIndex = 56;
            this.ButtonNext.Text = ">>";
            this.ButtonNext.UseVisualStyleBackColor = true;
            this.ButtonNext.Click += new System.EventHandler(this.ButtonNext_Click);
            // 
            // ButtonPrevious
            // 
            this.ButtonPrevious.Enabled = false;
            this.ButtonPrevious.Location = new System.Drawing.Point(43, 113);
            this.ButtonPrevious.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ButtonPrevious.Name = "ButtonPrevious";
            this.ButtonPrevious.Size = new System.Drawing.Size(64, 42);
            this.ButtonPrevious.TabIndex = 55;
            this.ButtonPrevious.Text = "<<";
            this.ButtonPrevious.UseVisualStyleBackColor = true;
            this.ButtonPrevious.Click += new System.EventHandler(this.ButtonPrevious_Click);
            // 
            // Area
            // 
            this.Area.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Area.Location = new System.Drawing.Point(423, 393);
            this.Area.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Area.Name = "Area";
            this.Area.ReadOnly = true;
            this.Area.Size = new System.Drawing.Size(385, 29);
            this.Area.TabIndex = 61;
            this.Area.Click += new System.EventHandler(this.DisplayMap);
            // 
            // LabelUNK_2
            // 
            this.LabelUNK_2.AutoSize = true;
            this.LabelUNK_2.Location = new System.Drawing.Point(366, 400);
            this.LabelUNK_2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelUNK_2.Name = "LabelUNK_2";
            this.LabelUNK_2.Size = new System.Drawing.Size(52, 25);
            this.LabelUNK_2.TabIndex = 60;
            this.LabelUNK_2.Text = "Area:";
            this.LabelUNK_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IVs
            // 
            this.IVs.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.IVs.Location = new System.Drawing.Point(423, 347);
            this.IVs.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.IVs.Name = "IVs";
            this.IVs.ReadOnly = true;
            this.IVs.Size = new System.Drawing.Size(385, 29);
            this.IVs.TabIndex = 69;
            // 
            // LabelIVs
            // 
            this.LabelIVs.AutoSize = true;
            this.LabelIVs.Location = new System.Drawing.Point(379, 353);
            this.LabelIVs.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelIVs.Name = "LabelIVs";
            this.LabelIVs.Size = new System.Drawing.Size(39, 25);
            this.LabelIVs.TabIndex = 68;
            this.LabelIVs.Text = "IVs:";
            this.LabelIVs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ButtonReadRaids
            // 
            this.ButtonReadRaids.Enabled = false;
            this.ButtonReadRaids.Location = new System.Drawing.Point(9, 37);
            this.ButtonReadRaids.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.ButtonReadRaids.Name = "ButtonReadRaids";
            this.ButtonReadRaids.Size = new System.Drawing.Size(129, 42);
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
            this.labelEvent.Location = new System.Drawing.Point(709, 107);
            this.labelEvent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelEvent.Name = "labelEvent";
            this.labelEvent.Size = new System.Drawing.Size(111, 25);
            this.labelEvent.TabIndex = 84;
            this.labelEvent.Text = "~~Event~~";
            this.labelEvent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelEvent.Visible = false;
            // 
            // Difficulty
            // 
            this.Difficulty.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Difficulty.Location = new System.Drawing.Point(671, 253);
            this.Difficulty.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Difficulty.Name = "Difficulty";
            this.Difficulty.ReadOnly = true;
            this.Difficulty.Size = new System.Drawing.Size(137, 29);
            this.Difficulty.TabIndex = 86;
            // 
            // LabelDifficulty
            // 
            this.LabelDifficulty.AutoSize = true;
            this.LabelDifficulty.Location = new System.Drawing.Point(579, 260);
            this.LabelDifficulty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelDifficulty.Name = "LabelDifficulty";
            this.LabelDifficulty.Size = new System.Drawing.Size(86, 25);
            this.LabelDifficulty.TabIndex = 85;
            this.LabelDifficulty.Text = "Difficulty:";
            this.LabelDifficulty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ButtonViewRAM
            // 
            this.ButtonViewRAM.Enabled = false;
            this.ButtonViewRAM.Location = new System.Drawing.Point(149, 37);
            this.ButtonViewRAM.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ButtonViewRAM.Name = "ButtonViewRAM";
            this.ButtonViewRAM.Size = new System.Drawing.Size(129, 42);
            this.ButtonViewRAM.TabIndex = 89;
            this.ButtonViewRAM.Text = "Dump Raid";
            this.ButtonViewRAM.UseVisualStyleBackColor = true;
            this.ButtonViewRAM.Click += new System.EventHandler(this.ViewRAM_Click);
            // 
            // Species
            // 
            this.Species.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Species.Location = new System.Drawing.Point(423, 160);
            this.Species.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Species.Name = "Species";
            this.Species.ReadOnly = true;
            this.Species.Size = new System.Drawing.Size(385, 29);
            this.Species.TabIndex = 93;
            // 
            // LabelSpecies
            // 
            this.LabelSpecies.AutoSize = true;
            this.LabelSpecies.Location = new System.Drawing.Point(344, 167);
            this.LabelSpecies.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelSpecies.Name = "LabelSpecies";
            this.LabelSpecies.Size = new System.Drawing.Size(75, 25);
            this.LabelSpecies.TabIndex = 92;
            this.LabelSpecies.Text = "Species:";
            this.LabelSpecies.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelMoves
            // 
            this.LabelMoves.AutoSize = true;
            this.LabelMoves.Location = new System.Drawing.Point(349, 467);
            this.LabelMoves.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelMoves.Name = "LabelMoves";
            this.LabelMoves.Size = new System.Drawing.Size(69, 25);
            this.LabelMoves.TabIndex = 94;
            this.LabelMoves.Text = "Moves:";
            this.LabelMoves.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Move1
            // 
            this.Move1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Move1.Location = new System.Drawing.Point(423, 440);
            this.Move1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Move1.Name = "Move1";
            this.Move1.ReadOnly = true;
            this.Move1.Size = new System.Drawing.Size(188, 29);
            this.Move1.TabIndex = 95;
            this.Move1.Click += new System.EventHandler(this.Move_Clicked);
            // 
            // Move2
            // 
            this.Move2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Move2.Location = new System.Drawing.Point(620, 440);
            this.Move2.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Move2.Name = "Move2";
            this.Move2.ReadOnly = true;
            this.Move2.Size = new System.Drawing.Size(188, 29);
            this.Move2.TabIndex = 96;
            this.Move2.Click += new System.EventHandler(this.Move_Clicked);
            // 
            // Move4
            // 
            this.Move4.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Move4.Location = new System.Drawing.Point(620, 487);
            this.Move4.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Move4.Name = "Move4";
            this.Move4.ReadOnly = true;
            this.Move4.Size = new System.Drawing.Size(188, 29);
            this.Move4.TabIndex = 98;
            this.Move4.Click += new System.EventHandler(this.Move_Clicked);
            // 
            // Move3
            // 
            this.Move3.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Move3.Location = new System.Drawing.Point(423, 487);
            this.Move3.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Move3.Name = "Move3";
            this.Move3.ReadOnly = true;
            this.Move3.Size = new System.Drawing.Size(188, 29);
            this.Move3.TabIndex = 97;
            this.Move3.Click += new System.EventHandler(this.Move_Clicked);
            // 
            // Nature
            // 
            this.Nature.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Nature.Location = new System.Drawing.Point(671, 300);
            this.Nature.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Nature.Name = "Nature";
            this.Nature.ReadOnly = true;
            this.Nature.Size = new System.Drawing.Size(137, 29);
            this.Nature.TabIndex = 106;
            // 
            // LabelNature
            // 
            this.LabelNature.AutoSize = true;
            this.LabelNature.Location = new System.Drawing.Point(596, 307);
            this.LabelNature.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelNature.Name = "LabelNature";
            this.LabelNature.Size = new System.Drawing.Size(69, 25);
            this.LabelNature.TabIndex = 105;
            this.LabelNature.Text = "Nature:";
            this.LabelNature.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Gender
            // 
            this.Gender.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Gender.Location = new System.Drawing.Point(423, 300);
            this.Gender.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Gender.Name = "Gender";
            this.Gender.ReadOnly = true;
            this.Gender.Size = new System.Drawing.Size(134, 29);
            this.Gender.TabIndex = 104;
            // 
            // LabelGender
            // 
            this.LabelGender.AutoSize = true;
            this.LabelGender.Location = new System.Drawing.Point(344, 307);
            this.LabelGender.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelGender.Name = "LabelGender";
            this.LabelGender.Size = new System.Drawing.Size(73, 25);
            this.LabelGender.TabIndex = 103;
            this.LabelGender.Text = "Gender:";
            this.LabelGender.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StopFilter
            // 
            this.StopFilter.Location = new System.Drawing.Point(17, 493);
            this.StopFilter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.StopFilter.Name = "StopFilter";
            this.StopFilter.Size = new System.Drawing.Size(139, 38);
            this.StopFilter.TabIndex = 107;
            this.StopFilter.Text = "Edit Filters";
            this.StopFilter.UseVisualStyleBackColor = true;
            this.StopFilter.Click += new System.EventHandler(this.StopFilter_Click);
            // 
            // Sprite
            // 
            this.Sprite.Location = new System.Drawing.Point(711, 12);
            this.Sprite.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Sprite.Name = "Sprite";
            this.Sprite.Size = new System.Drawing.Size(97, 93);
            this.Sprite.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Sprite.TabIndex = 108;
            this.Sprite.TabStop = false;
            // 
            // Ability
            // 
            this.Ability.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Ability.Location = new System.Drawing.Point(423, 207);
            this.Ability.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Ability.Name = "Ability";
            this.Ability.ReadOnly = true;
            this.Ability.Size = new System.Drawing.Size(385, 29);
            this.Ability.TabIndex = 110;
            // 
            // LabelAbility
            // 
            this.LabelAbility.AutoSize = true;
            this.LabelAbility.Location = new System.Drawing.Point(350, 212);
            this.LabelAbility.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelAbility.Name = "LabelAbility";
            this.LabelAbility.Size = new System.Drawing.Size(66, 25);
            this.LabelAbility.TabIndex = 109;
            this.LabelAbility.Text = "Ability:";
            this.LabelAbility.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GemIcon
            // 
            this.GemIcon.Location = new System.Drawing.Point(620, 12);
            this.GemIcon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GemIcon.Name = "GemIcon";
            this.GemIcon.Size = new System.Drawing.Size(80, 93);
            this.GemIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.GemIcon.TabIndex = 111;
            this.GemIcon.TabStop = false;
            // 
            // ButtonDownloadEvents
            // 
            this.ButtonDownloadEvents.Enabled = false;
            this.ButtonDownloadEvents.Location = new System.Drawing.Point(149, 83);
            this.ButtonDownloadEvents.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ButtonDownloadEvents.Name = "ButtonDownloadEvents";
            this.ButtonDownloadEvents.Size = new System.Drawing.Size(129, 42);
            this.ButtonDownloadEvents.TabIndex = 112;
            this.ButtonDownloadEvents.Text = "Pull Events";
            this.ButtonDownloadEvents.UseVisualStyleBackColor = true;
            this.ButtonDownloadEvents.Click += new System.EventHandler(this.DownloadEvents_Click);
            // 
            // ConfigSettings
            // 
            this.ConfigSettings.Location = new System.Drawing.Point(17, 541);
            this.ConfigSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ConfigSettings.Name = "ConfigSettings";
            this.ConfigSettings.Size = new System.Drawing.Size(290, 38);
            this.ConfigSettings.TabIndex = 115;
            this.ConfigSettings.Text = "Open Settings";
            this.ConfigSettings.UseVisualStyleBackColor = true;
            this.ConfigSettings.Click += new System.EventHandler(this.ConfigSettings_Click);
            // 
            // Rewards
            // 
            this.Rewards.Location = new System.Drawing.Point(149, 130);
            this.Rewards.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Rewards.Name = "Rewards";
            this.Rewards.Size = new System.Drawing.Size(129, 42);
            this.Rewards.TabIndex = 116;
            this.Rewards.Text = "Rewards";
            this.Rewards.UseVisualStyleBackColor = true;
            this.Rewards.Click += new System.EventHandler(this.Rewards_Click);
            // 
            // LabelSandwichBonus
            // 
            this.LabelSandwichBonus.AutoSize = true;
            this.LabelSandwichBonus.Location = new System.Drawing.Point(19, 451);
            this.LabelSandwichBonus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelSandwichBonus.Name = "LabelSandwichBonus";
            this.LabelSandwichBonus.Size = new System.Drawing.Size(182, 25);
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
            this.RaidBoost.Location = new System.Drawing.Point(236, 446);
            this.RaidBoost.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RaidBoost.Name = "RaidBoost";
            this.RaidBoost.Size = new System.Drawing.Size(67, 33);
            this.RaidBoost.TabIndex = 117;
            this.RaidBoost.Text = "w";
            this.RaidBoost.SelectedIndexChanged += new System.EventHandler(this.RaidBoost_SelectedIndexChanged);
            // 
            // ComboIndex
            // 
            this.ComboIndex.BackColor = System.Drawing.SystemColors.Window;
            this.ComboIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboIndex.FormattingEnabled = true;
            this.ComboIndex.Location = new System.Drawing.Point(114, 113);
            this.ComboIndex.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ComboIndex.Name = "ComboIndex";
            this.ComboIndex.Size = new System.Drawing.Size(90, 33);
            this.ComboIndex.TabIndex = 120;
            this.ComboIndex.SelectedIndexChanged += new System.EventHandler(this.ComboIndex_SelectedIndexChanged);
            // 
            // SendScreenshot
            // 
            this.SendScreenshot.Location = new System.Drawing.Point(9, 83);
            this.SendScreenshot.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SendScreenshot.Name = "SendScreenshot";
            this.SendScreenshot.Size = new System.Drawing.Size(129, 42);
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
            this.btnOpenMap.Location = new System.Drawing.Point(9, 130);
            this.btnOpenMap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOpenMap.Name = "btnOpenMap";
            this.btnOpenMap.Size = new System.Drawing.Size(129, 42);
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
            this.groupBox1.Location = new System.Drawing.Point(19, 253);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(286, 183);
            this.groupBox1.TabIndex = 125;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Raid Controls";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 598);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 20, 0);
            this.statusStrip1.Size = new System.Drawing.Size(829, 32);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 126;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatus
            // 
            this.toolStripStatus.Name = "toolStripStatus";
            this.toolStripStatus.Size = new System.Drawing.Size(60, 25);
            this.toolStripStatus.Text = "Status";
            // 
            // ButtonStopAdvance
            // 
            this.ButtonStopAdvance.Enabled = false;
            this.ButtonStopAdvance.Location = new System.Drawing.Point(26, 163);
            this.ButtonStopAdvance.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.ButtonStopAdvance.Name = "ButtonStopAdvance";
            this.ButtonStopAdvance.Size = new System.Drawing.Size(137, 45);
            this.ButtonStopAdvance.TabIndex = 127;
            this.ButtonStopAdvance.Text = "Stop";
            this.toolTip.SetToolTip(this.ButtonStopAdvance, "Stops advancing date after the current iteration ends.");
            this.ButtonStopAdvance.UseVisualStyleBackColor = true;
            this.ButtonStopAdvance.Click += new System.EventHandler(this.ButtonStopAdvance_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 630);
            this.Controls.Add(this.ButtonStopAdvance);
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
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
        private ToolStripStatusLabel toolStripStatus;
        private Button ButtonStopAdvance;
    }
}