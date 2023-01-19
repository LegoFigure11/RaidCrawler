namespace RaidCrawler.Subforms
{
    partial class FilterSettings
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
            this.components = new System.ComponentModel.Container();
            this.Species = new System.Windows.Forms.ComboBox();
            this.SpeciesCheck = new System.Windows.Forms.CheckBox();
            this.Form = new System.Windows.Forms.NumericUpDown();
            this.FormCheck = new System.Windows.Forms.CheckBox();
            this.NatureCheck = new System.Windows.Forms.CheckBox();
            this.Nature = new System.Windows.Forms.ComboBox();
            this.StarCheck = new System.Windows.Forms.CheckBox();
            this.Stars = new System.Windows.Forms.ComboBox();
            this.ShinyCheck = new System.Windows.Forms.CheckBox();
            this.Add = new System.Windows.Forms.Button();
            this.PerfectIVFilterGroup = new System.Windows.Forms.GroupBox();
            this.SpeComp = new System.Windows.Forms.ComboBox();
            this.SpaComp = new System.Windows.Forms.ComboBox();
            this.SpdComp = new System.Windows.Forms.ComboBox();
            this.DefComp = new System.Windows.Forms.ComboBox();
            this.AtkComp = new System.Windows.Forms.ComboBox();
            this.HPComp = new System.Windows.Forms.ComboBox();
            this.IVSPE = new System.Windows.Forms.NumericUpDown();
            this.IVSPD = new System.Windows.Forms.NumericUpDown();
            this.IVSPA = new System.Windows.Forms.NumericUpDown();
            this.IVDEF = new System.Windows.Forms.NumericUpDown();
            this.IVATK = new System.Windows.Forms.NumericUpDown();
            this.IVHP = new System.Windows.Forms.NumericUpDown();
            this.Spe = new System.Windows.Forms.CheckBox();
            this.SpD = new System.Windows.Forms.CheckBox();
            this.SpA = new System.Windows.Forms.CheckBox();
            this.Def = new System.Windows.Forms.CheckBox();
            this.Atk = new System.Windows.Forms.CheckBox();
            this.HP = new System.Windows.Forms.CheckBox();
            this.TeraCheck = new System.Windows.Forms.CheckBox();
            this.TeraType = new System.Windows.Forms.ComboBox();
            this.ActiveFilters = new System.Windows.Forms.CheckedListBox();
            this.FilterName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Remove = new System.Windows.Forms.Button();
            this.StarsComp = new System.Windows.Forms.ComboBox();
            this.RewardsComp = new System.Windows.Forms.ComboBox();
            this.CheckRewards = new System.Windows.Forms.CheckBox();
            this.Rewards = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RewardsCount = new System.Windows.Forms.NumericUpDown();
            this.ButtonOpenRewardsList = new System.Windows.Forms.Button();
            this.GenderCheck = new System.Windows.Forms.CheckBox();
            this.Gender = new System.Windows.Forms.ComboBox();
            this.LabelBatchFilters = new System.Windows.Forms.Label();
            this.BatchFilters = new System.Windows.Forms.TextBox();
            this.SquareCheck = new System.Windows.Forms.CheckBox();
            this.Tooltip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Form)).BeginInit();
            this.PerfectIVFilterGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IVSPE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IVSPD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IVSPA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IVDEF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IVATK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IVHP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RewardsCount)).BeginInit();
            this.SuspendLayout();
            // 
            // Species
            // 
            this.Species.Enabled = false;
            this.Species.FormattingEnabled = true;
            this.Species.Location = new System.Drawing.Point(104, 55);
            this.Species.Name = "Species";
            this.Species.Size = new System.Drawing.Size(121, 23);
            this.Species.TabIndex = 0;
            // 
            // SpeciesCheck
            // 
            this.SpeciesCheck.AutoSize = true;
            this.SpeciesCheck.Location = new System.Drawing.Point(15, 57);
            this.SpeciesCheck.Name = "SpeciesCheck";
            this.SpeciesCheck.Size = new System.Drawing.Size(65, 19);
            this.SpeciesCheck.TabIndex = 1;
            this.SpeciesCheck.Text = "Species";
            this.SpeciesCheck.UseVisualStyleBackColor = true;
            this.SpeciesCheck.CheckedChanged += new System.EventHandler(this.SpeciesCheck_CheckedChanged);
            // 
            // Form
            // 
            this.Form.Enabled = false;
            this.Form.Location = new System.Drawing.Point(104, 82);
            this.Form.Name = "Form";
            this.Form.Size = new System.Drawing.Size(121, 23);
            this.Form.TabIndex = 0;
            // 
            // FormCheck
            // 
            this.FormCheck.AutoSize = true;
            this.FormCheck.Location = new System.Drawing.Point(15, 84);
            this.FormCheck.Name = "FormCheck";
            this.FormCheck.Size = new System.Drawing.Size(54, 19);
            this.FormCheck.TabIndex = 1;
            this.FormCheck.Text = "Form";
            this.FormCheck.UseVisualStyleBackColor = true;
            this.FormCheck.CheckedChanged += new System.EventHandler(this.FormCheck_CheckedChanged);
            // 
            // NatureCheck
            // 
            this.NatureCheck.AutoSize = true;
            this.NatureCheck.Location = new System.Drawing.Point(15, 113);
            this.NatureCheck.Name = "NatureCheck";
            this.NatureCheck.Size = new System.Drawing.Size(62, 19);
            this.NatureCheck.TabIndex = 3;
            this.NatureCheck.Text = "Nature";
            this.NatureCheck.UseVisualStyleBackColor = true;
            this.NatureCheck.CheckedChanged += new System.EventHandler(this.NatureCheck_CheckedChanged);
            // 
            // Nature
            // 
            this.Nature.Enabled = false;
            this.Nature.FormattingEnabled = true;
            this.Nature.Location = new System.Drawing.Point(104, 111);
            this.Nature.Name = "Nature";
            this.Nature.Size = new System.Drawing.Size(121, 23);
            this.Nature.TabIndex = 2;
            // 
            // StarCheck
            // 
            this.StarCheck.AutoSize = true;
            this.StarCheck.Location = new System.Drawing.Point(15, 142);
            this.StarCheck.Name = "StarCheck";
            this.StarCheck.Size = new System.Drawing.Size(51, 19);
            this.StarCheck.TabIndex = 5;
            this.StarCheck.Text = "Stars";
            this.StarCheck.UseVisualStyleBackColor = true;
            this.StarCheck.CheckedChanged += new System.EventHandler(this.StarCheck_CheckedChanged);
            // 
            // Stars
            // 
            this.Stars.Enabled = false;
            this.Stars.FormattingEnabled = true;
            this.Stars.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.Stars.Location = new System.Drawing.Point(104, 140);
            this.Stars.Name = "Stars";
            this.Stars.Size = new System.Drawing.Size(62, 23);
            this.Stars.TabIndex = 4;
            // 
            // ShinyCheck
            // 
            this.ShinyCheck.AutoSize = true;
            this.ShinyCheck.Location = new System.Drawing.Point(15, 285);
            this.ShinyCheck.Name = "ShinyCheck";
            this.ShinyCheck.Size = new System.Drawing.Size(60, 19);
            this.ShinyCheck.TabIndex = 6;
            this.ShinyCheck.Text = "Shiny?";
            this.ShinyCheck.UseVisualStyleBackColor = true;
            this.ShinyCheck.CheckedChanged += new System.EventHandler(this.ShinyCheck_CheckedChanged);
            // 
            // Add
            // 
            this.Add.Location = new System.Drawing.Point(12, 440);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(111, 23);
            this.Add.TabIndex = 9;
            this.Add.Text = "Add Filter";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Filter_Click);
            // 
            // PerfectIVFilterGroup
            // 
            this.PerfectIVFilterGroup.Controls.Add(this.SpeComp);
            this.PerfectIVFilterGroup.Controls.Add(this.SpaComp);
            this.PerfectIVFilterGroup.Controls.Add(this.SpdComp);
            this.PerfectIVFilterGroup.Controls.Add(this.DefComp);
            this.PerfectIVFilterGroup.Controls.Add(this.AtkComp);
            this.PerfectIVFilterGroup.Controls.Add(this.HPComp);
            this.PerfectIVFilterGroup.Controls.Add(this.IVSPE);
            this.PerfectIVFilterGroup.Controls.Add(this.IVSPD);
            this.PerfectIVFilterGroup.Controls.Add(this.IVSPA);
            this.PerfectIVFilterGroup.Controls.Add(this.IVDEF);
            this.PerfectIVFilterGroup.Controls.Add(this.IVATK);
            this.PerfectIVFilterGroup.Controls.Add(this.IVHP);
            this.PerfectIVFilterGroup.Controls.Add(this.Spe);
            this.PerfectIVFilterGroup.Controls.Add(this.SpD);
            this.PerfectIVFilterGroup.Controls.Add(this.SpA);
            this.PerfectIVFilterGroup.Controls.Add(this.Def);
            this.PerfectIVFilterGroup.Controls.Add(this.Atk);
            this.PerfectIVFilterGroup.Controls.Add(this.HP);
            this.PerfectIVFilterGroup.Location = new System.Drawing.Point(12, 310);
            this.PerfectIVFilterGroup.Name = "PerfectIVFilterGroup";
            this.PerfectIVFilterGroup.Size = new System.Drawing.Size(231, 124);
            this.PerfectIVFilterGroup.TabIndex = 10;
            this.PerfectIVFilterGroup.TabStop = false;
            this.PerfectIVFilterGroup.Text = "IV Filters";
            // 
            // SpeComp
            // 
            this.SpeComp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SpeComp.Enabled = false;
            this.SpeComp.FormattingEnabled = true;
            this.SpeComp.Items.AddRange(new object[] {
            "=",
            ">",
            ">=",
            "<=",
            "<"});
            this.SpeComp.Location = new System.Drawing.Point(193, 54);
            this.SpeComp.Name = "SpeComp";
            this.SpeComp.Size = new System.Drawing.Size(32, 23);
            this.SpeComp.TabIndex = 17;
            // 
            // SpaComp
            // 
            this.SpaComp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SpaComp.Enabled = false;
            this.SpaComp.FormattingEnabled = true;
            this.SpaComp.Items.AddRange(new object[] {
            "=",
            ">",
            ">=",
            "<=",
            "<"});
            this.SpaComp.Location = new System.Drawing.Point(117, 54);
            this.SpaComp.Name = "SpaComp";
            this.SpaComp.Size = new System.Drawing.Size(32, 23);
            this.SpaComp.TabIndex = 15;
            // 
            // SpdComp
            // 
            this.SpdComp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SpdComp.Enabled = false;
            this.SpdComp.FormattingEnabled = true;
            this.SpdComp.Items.AddRange(new object[] {
            "=",
            ">",
            ">=",
            "<=",
            "<"});
            this.SpdComp.Location = new System.Drawing.Point(155, 54);
            this.SpdComp.Name = "SpdComp";
            this.SpdComp.Size = new System.Drawing.Size(32, 23);
            this.SpdComp.TabIndex = 16;
            // 
            // DefComp
            // 
            this.DefComp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DefComp.Enabled = false;
            this.DefComp.FormattingEnabled = true;
            this.DefComp.Items.AddRange(new object[] {
            "=",
            ">",
            ">=",
            "<=",
            "<"});
            this.DefComp.Location = new System.Drawing.Point(79, 55);
            this.DefComp.Name = "DefComp";
            this.DefComp.Size = new System.Drawing.Size(32, 23);
            this.DefComp.TabIndex = 14;
            // 
            // AtkComp
            // 
            this.AtkComp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AtkComp.Enabled = false;
            this.AtkComp.FormattingEnabled = true;
            this.AtkComp.Items.AddRange(new object[] {
            "=",
            ">",
            ">=",
            "<=",
            "<"});
            this.AtkComp.Location = new System.Drawing.Point(41, 54);
            this.AtkComp.Name = "AtkComp";
            this.AtkComp.Size = new System.Drawing.Size(32, 23);
            this.AtkComp.TabIndex = 13;
            // 
            // HPComp
            // 
            this.HPComp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.HPComp.Enabled = false;
            this.HPComp.FormattingEnabled = true;
            this.HPComp.Items.AddRange(new object[] {
            "=",
            ">",
            ">=",
            "<=",
            "<"});
            this.HPComp.Location = new System.Drawing.Point(3, 54);
            this.HPComp.Name = "HPComp";
            this.HPComp.Size = new System.Drawing.Size(32, 23);
            this.HPComp.TabIndex = 12;
            // 
            // IVSPE
            // 
            this.IVSPE.Enabled = false;
            this.IVSPE.Location = new System.Drawing.Point(193, 22);
            this.IVSPE.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.IVSPE.Name = "IVSPE";
            this.IVSPE.Size = new System.Drawing.Size(32, 23);
            this.IVSPE.TabIndex = 11;
            this.IVSPE.Value = new decimal(new int[] {
            31,
            0,
            0,
            0});
            // 
            // IVSPD
            // 
            this.IVSPD.Enabled = false;
            this.IVSPD.Location = new System.Drawing.Point(155, 22);
            this.IVSPD.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.IVSPD.Name = "IVSPD";
            this.IVSPD.Size = new System.Drawing.Size(32, 23);
            this.IVSPD.TabIndex = 10;
            this.IVSPD.Value = new decimal(new int[] {
            31,
            0,
            0,
            0});
            // 
            // IVSPA
            // 
            this.IVSPA.Enabled = false;
            this.IVSPA.Location = new System.Drawing.Point(117, 22);
            this.IVSPA.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.IVSPA.Name = "IVSPA";
            this.IVSPA.Size = new System.Drawing.Size(32, 23);
            this.IVSPA.TabIndex = 9;
            this.IVSPA.Value = new decimal(new int[] {
            31,
            0,
            0,
            0});
            // 
            // IVDEF
            // 
            this.IVDEF.Enabled = false;
            this.IVDEF.Location = new System.Drawing.Point(79, 22);
            this.IVDEF.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.IVDEF.Name = "IVDEF";
            this.IVDEF.Size = new System.Drawing.Size(32, 23);
            this.IVDEF.TabIndex = 8;
            this.IVDEF.Value = new decimal(new int[] {
            31,
            0,
            0,
            0});
            // 
            // IVATK
            // 
            this.IVATK.Enabled = false;
            this.IVATK.Location = new System.Drawing.Point(41, 22);
            this.IVATK.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.IVATK.Name = "IVATK";
            this.IVATK.Size = new System.Drawing.Size(32, 23);
            this.IVATK.TabIndex = 7;
            this.IVATK.Value = new decimal(new int[] {
            31,
            0,
            0,
            0});
            // 
            // IVHP
            // 
            this.IVHP.Enabled = false;
            this.IVHP.Location = new System.Drawing.Point(3, 22);
            this.IVHP.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.IVHP.Name = "IVHP";
            this.IVHP.Size = new System.Drawing.Size(32, 23);
            this.IVHP.TabIndex = 6;
            this.IVHP.Value = new decimal(new int[] {
            31,
            0,
            0,
            0});
            // 
            // Spe
            // 
            this.Spe.AutoSize = true;
            this.Spe.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Spe.Location = new System.Drawing.Point(194, 84);
            this.Spe.Name = "Spe";
            this.Spe.Size = new System.Drawing.Size(30, 33);
            this.Spe.TabIndex = 5;
            this.Spe.Text = "Spe";
            this.Spe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Spe.UseVisualStyleBackColor = true;
            this.Spe.CheckedChanged += new System.EventHandler(this.Spe_CheckedChanged);
            // 
            // SpD
            // 
            this.SpD.AutoSize = true;
            this.SpD.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.SpD.Location = new System.Drawing.Point(154, 84);
            this.SpD.Name = "SpD";
            this.SpD.Size = new System.Drawing.Size(32, 33);
            this.SpD.TabIndex = 4;
            this.SpD.Text = "SpD";
            this.SpD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SpD.UseVisualStyleBackColor = true;
            this.SpD.CheckedChanged += new System.EventHandler(this.SpD_CheckedChanged);
            // 
            // SpA
            // 
            this.SpA.AutoSize = true;
            this.SpA.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.SpA.Location = new System.Drawing.Point(115, 84);
            this.SpA.Name = "SpA";
            this.SpA.Size = new System.Drawing.Size(32, 33);
            this.SpA.TabIndex = 3;
            this.SpA.Text = "SpA";
            this.SpA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SpA.UseVisualStyleBackColor = true;
            this.SpA.CheckedChanged += new System.EventHandler(this.SpA_CheckedChanged);
            // 
            // Def
            // 
            this.Def.AutoSize = true;
            this.Def.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Def.Location = new System.Drawing.Point(79, 84);
            this.Def.Name = "Def";
            this.Def.Size = new System.Drawing.Size(29, 33);
            this.Def.TabIndex = 2;
            this.Def.Text = "Def";
            this.Def.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Def.UseVisualStyleBackColor = true;
            this.Def.CheckedChanged += new System.EventHandler(this.Def_CheckedChanged);
            // 
            // Atk
            // 
            this.Atk.AutoSize = true;
            this.Atk.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Atk.Location = new System.Drawing.Point(41, 84);
            this.Atk.Name = "Atk";
            this.Atk.Size = new System.Drawing.Size(29, 33);
            this.Atk.TabIndex = 1;
            this.Atk.Text = "Atk";
            this.Atk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Atk.UseVisualStyleBackColor = true;
            this.Atk.CheckedChanged += new System.EventHandler(this.Atk_CheckedChanged);
            // 
            // HP
            // 
            this.HP.AutoSize = true;
            this.HP.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.HP.Location = new System.Drawing.Point(3, 84);
            this.HP.Name = "HP";
            this.HP.Size = new System.Drawing.Size(27, 33);
            this.HP.TabIndex = 0;
            this.HP.Text = "HP";
            this.HP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.HP.UseVisualStyleBackColor = true;
            this.HP.CheckedChanged += new System.EventHandler(this.HP_CheckedChanged);
            // 
            // TeraCheck
            // 
            this.TeraCheck.AutoSize = true;
            this.TeraCheck.Location = new System.Drawing.Point(15, 171);
            this.TeraCheck.Name = "TeraCheck";
            this.TeraCheck.Size = new System.Drawing.Size(74, 19);
            this.TeraCheck.TabIndex = 18;
            this.TeraCheck.Text = "Tera Type";
            this.TeraCheck.UseVisualStyleBackColor = true;
            this.TeraCheck.CheckedChanged += new System.EventHandler(this.TeraCheck_CheckedChanged);
            // 
            // TeraType
            // 
            this.TeraType.Enabled = false;
            this.TeraType.FormattingEnabled = true;
            this.TeraType.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.TeraType.Location = new System.Drawing.Point(104, 169);
            this.TeraType.Name = "TeraType";
            this.TeraType.Size = new System.Drawing.Size(121, 23);
            this.TeraType.TabIndex = 17;
            // 
            // ActiveFilters
            // 
            this.ActiveFilters.FormattingEnabled = true;
            this.ActiveFilters.Location = new System.Drawing.Point(257, 23);
            this.ActiveFilters.Name = "ActiveFilters";
            this.ActiveFilters.Size = new System.Drawing.Size(185, 256);
            this.ActiveFilters.TabIndex = 20;
            this.ActiveFilters.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ActiveFilters_ItemCheck);
            this.ActiveFilters.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ActiveFilters_DrawItem);
            this.ActiveFilters.SelectedIndexChanged += new System.EventHandler(this.ActiveFilters_SelectedIndexChanged);
            // 
            // FilterName
            // 
            this.FilterName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FilterName.Location = new System.Drawing.Point(57, 23);
            this.FilterName.Name = "FilterName";
            this.FilterName.Size = new System.Drawing.Size(168, 23);
            this.FilterName.TabIndex = 21;
            this.FilterName.TextChanged += new System.EventHandler(this.FilterName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 22;
            this.label1.Text = "Name";
            // 
            // Remove
            // 
            this.Remove.Location = new System.Drawing.Point(132, 440);
            this.Remove.Name = "Remove";
            this.Remove.Size = new System.Drawing.Size(111, 23);
            this.Remove.TabIndex = 23;
            this.Remove.Text = "Remove Filter";
            this.Remove.UseVisualStyleBackColor = true;
            this.Remove.Click += new System.EventHandler(this.Remove_Click);
            // 
            // StarsComp
            // 
            this.StarsComp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.StarsComp.Enabled = false;
            this.StarsComp.FormattingEnabled = true;
            this.StarsComp.Items.AddRange(new object[] {
            "=",
            ">",
            ">=",
            "<=",
            "<"});
            this.StarsComp.Location = new System.Drawing.Point(172, 140);
            this.StarsComp.Name = "StarsComp";
            this.StarsComp.Size = new System.Drawing.Size(53, 23);
            this.StarsComp.TabIndex = 18;
            // 
            // RewardsComp
            // 
            this.RewardsComp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RewardsComp.Enabled = false;
            this.RewardsComp.FormattingEnabled = true;
            this.RewardsComp.IntegralHeight = false;
            this.RewardsComp.Items.AddRange(new object[] {
            "=",
            ">",
            ">=",
            "<=",
            "<"});
            this.RewardsComp.Location = new System.Drawing.Point(172, 256);
            this.RewardsComp.Name = "RewardsComp";
            this.RewardsComp.Size = new System.Drawing.Size(53, 23);
            this.RewardsComp.TabIndex = 27;
            // 
            // CheckRewards
            // 
            this.CheckRewards.AutoSize = true;
            this.CheckRewards.Location = new System.Drawing.Point(15, 229);
            this.CheckRewards.Name = "CheckRewards";
            this.CheckRewards.Size = new System.Drawing.Size(70, 19);
            this.CheckRewards.TabIndex = 26;
            this.CheckRewards.Text = "Rewards";
            this.CheckRewards.UseVisualStyleBackColor = true;
            this.CheckRewards.CheckedChanged += new System.EventHandler(this.CheckRewards_CheckedChanged);
            // 
            // Rewards
            // 
            this.Rewards.Enabled = false;
            this.Rewards.Location = new System.Drawing.Point(104, 227);
            this.Rewards.Name = "Rewards";
            this.Rewards.Size = new System.Drawing.Size(96, 23);
            this.Rewards.TabIndex = 28;
            this.Rewards.Text = "645,795,1606,1904,1905,1906,1907,1908";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 259);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 29;
            this.label2.Text = "Reward Count";
            // 
            // RewardsCount
            // 
            this.RewardsCount.Enabled = false;
            this.RewardsCount.Location = new System.Drawing.Point(104, 256);
            this.RewardsCount.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.RewardsCount.Name = "RewardsCount";
            this.RewardsCount.Size = new System.Drawing.Size(62, 23);
            this.RewardsCount.TabIndex = 30;
            // 
            // ButtonOpenRewardsList
            // 
            this.ButtonOpenRewardsList.Location = new System.Drawing.Point(206, 227);
            this.ButtonOpenRewardsList.Name = "ButtonOpenRewardsList";
            this.ButtonOpenRewardsList.Size = new System.Drawing.Size(19, 23);
            this.ButtonOpenRewardsList.TabIndex = 31;
            this.ButtonOpenRewardsList.Text = "?";
            this.ButtonOpenRewardsList.UseVisualStyleBackColor = true;
            this.ButtonOpenRewardsList.Click += new System.EventHandler(this.ButtonOpenRewardsList_Click);
            // 
            // GenderCheck
            // 
            this.GenderCheck.AutoSize = true;
            this.GenderCheck.Location = new System.Drawing.Point(15, 200);
            this.GenderCheck.Name = "GenderCheck";
            this.GenderCheck.Size = new System.Drawing.Size(64, 19);
            this.GenderCheck.TabIndex = 33;
            this.GenderCheck.Text = "Gender";
            this.GenderCheck.UseVisualStyleBackColor = true;
            this.GenderCheck.CheckedChanged += new System.EventHandler(this.GenderCheck_CheckedChanged);
            // 
            // Gender
            // 
            this.Gender.Enabled = false;
            this.Gender.FormattingEnabled = true;
            this.Gender.Items.AddRange(new object[] {
            "Male",
            "Female",
            "Genderless"});
            this.Gender.Location = new System.Drawing.Point(104, 198);
            this.Gender.Name = "Gender";
            this.Gender.Size = new System.Drawing.Size(121, 23);
            this.Gender.TabIndex = 32;
            // 
            // LabelBatchFilters
            // 
            this.LabelBatchFilters.AutoSize = true;
            this.LabelBatchFilters.Location = new System.Drawing.Point(257, 285);
            this.LabelBatchFilters.Name = "LabelBatchFilters";
            this.LabelBatchFilters.Size = new System.Drawing.Size(71, 15);
            this.LabelBatchFilters.TabIndex = 34;
            this.LabelBatchFilters.Text = "Batch Filters";
            // 
            // BatchFilters
            // 
            this.BatchFilters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BatchFilters.Location = new System.Drawing.Point(257, 305);
            this.BatchFilters.Multiline = true;
            this.BatchFilters.Name = "BatchFilters";
            this.BatchFilters.Size = new System.Drawing.Size(185, 157);
            this.BatchFilters.TabIndex = 35;
            // 
            // SquareCheck
            // 
            this.SquareCheck.AutoSize = true;
            this.SquareCheck.Location = new System.Drawing.Point(104, 285);
            this.SquareCheck.Name = "SquareCheck";
            this.SquareCheck.Size = new System.Drawing.Size(69, 19);
            this.SquareCheck.TabIndex = 36;
            this.SquareCheck.Text = "XOR = 0";
            this.Tooltip.SetToolTip(this.SquareCheck, "If checked, only stop on \"Square\" shinies.\r\nThere is no display difference " +
        "for these in SV, but they will appear\r\nas Square shiny in SwSh or any futu" +
        "re game that supports them.");
            this.SquareCheck.UseVisualStyleBackColor = true;
            // 
            // FilterSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 474);
            this.Controls.Add(this.SquareCheck);
            this.Controls.Add(this.BatchFilters);
            this.Controls.Add(this.LabelBatchFilters);
            this.Controls.Add(this.GenderCheck);
            this.Controls.Add(this.Gender);
            this.Controls.Add(this.ButtonOpenRewardsList);
            this.Controls.Add(this.RewardsCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Rewards);
            this.Controls.Add(this.RewardsComp);
            this.Controls.Add(this.CheckRewards);
            this.Controls.Add(this.StarsComp);
            this.Controls.Add(this.Remove);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FilterName);
            this.Controls.Add(this.ActiveFilters);
            this.Controls.Add(this.TeraCheck);
            this.Controls.Add(this.TeraType);
            this.Controls.Add(this.PerfectIVFilterGroup);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.ShinyCheck);
            this.Controls.Add(this.StarCheck);
            this.Controls.Add(this.Stars);
            this.Controls.Add(this.NatureCheck);
            this.Controls.Add(this.Nature);
            this.Controls.Add(this.FormCheck);
            this.Controls.Add(this.Form);
            this.Controls.Add(this.SpeciesCheck);
            this.Controls.Add(this.Species);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FilterSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FilterSettings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Save_Click);
            ((System.ComponentModel.ISupportInitialize)(this.Form)).EndInit();
            this.PerfectIVFilterGroup.ResumeLayout(false);
            this.PerfectIVFilterGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IVSPE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IVSPD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IVSPA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IVDEF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IVATK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IVHP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RewardsCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox Species;
        private CheckBox SpeciesCheck;
        private NumericUpDown Form;
        private CheckBox FormCheck;
        private CheckBox NatureCheck;
        private ComboBox Nature;
        private CheckBox StarCheck;
        private ComboBox Stars;
        private CheckBox ShinyCheck;
        private Button Add;
        private GroupBox PerfectIVFilterGroup;
        private CheckBox Spe;
        private CheckBox SpD;
        private CheckBox SpA;
        private CheckBox Def;
        private CheckBox Atk;
        private CheckBox HP;
        private NumericUpDown IVSPE;
        private NumericUpDown IVSPD;
        private NumericUpDown IVSPA;
        private NumericUpDown IVDEF;
        private NumericUpDown IVATK;
        private NumericUpDown IVHP;
        private CheckBox TeraCheck;
        private ComboBox TeraType;
        private CheckedListBox ActiveFilters;
        private TextBox FilterName;
        private Label label1;
        private Button Remove;
        private ComboBox HPComp;
        private ComboBox SpeComp;
        private ComboBox SpaComp;
        private ComboBox SpdComp;
        private ComboBox DefComp;
        private ComboBox AtkComp;
        private ComboBox StarsComp;
        private ComboBox RewardsComp;
        private CheckBox CheckRewards;
        private TextBox Rewards;
        private Label label2;
        private NumericUpDown RewardsCount;
        private Button ButtonOpenRewardsList;
        private CheckBox GenderCheck;
        private ComboBox Gender;
        private Label LabelBatchFilters;
        private TextBox BatchFilters;
        private CheckBox SquareCheck;
        private ToolTip Tooltip;
    }
}