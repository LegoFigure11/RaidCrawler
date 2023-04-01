namespace RaidCrawler.WinForms.SubForms
{
    partial class TeraRaidView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TeraRaidView));
            Map = new PictureBox();
            Area = new TextBox();
            picBoxPokemon = new PictureBox();
            Difficulty = new TextBox();
            Species = new TextBox();
            Shiny = new Label();
            Gender = new TextBox();
            debug = new TextBox();
            TeraType = new PictureBox();
            Ability = new TextBox();
            Nature = new TextBox();
            groupBox1 = new GroupBox();
            label1 = new Label();
            labelAbility = new Label();
            groupBox2 = new GroupBox();
            labelSpeed = new Label();
            SPEED = new TextBox();
            labelSpD = new Label();
            SPD = new TextBox();
            labelSpA = new Label();
            SPA = new TextBox();
            labelDef = new Label();
            DEF = new TextBox();
            labelAtk = new Label();
            ATK = new TextBox();
            labelHP = new Label();
            HP = new TextBox();
            groupBox3 = new GroupBox();
            Move8 = new TextBox();
            Move6 = new TextBox();
            Move4 = new TextBox();
            Move7 = new TextBox();
            Move5 = new TextBox();
            Move3 = new TextBox();
            Move1 = new TextBox();
            Move2 = new TextBox();
            picShinyAlert = new PictureBox();
            pictureBox1 = new PictureBox();
            picBottleCap = new PictureBox();
            picSaltyHerba = new PictureBox();
            picSpicyHerba = new PictureBox();
            picSourHerba = new PictureBox();
            picSweetHerba = new PictureBox();
            picAbilityPatch = new PictureBox();
            labelAbilityPatch = new Label();
            textAbilityPatch = new TextBox();
            labelSweetHerba = new Label();
            textSweetHerba = new TextBox();
            labelSaltyHerba = new Label();
            textSaltyHerba = new TextBox();
            labelBottleCap = new Label();
            textBottleCap = new TextBox();
            labelSourHerba = new Label();
            textSourHerba = new TextBox();
            labelSpicyHerba = new Label();
            textSpicyHerba = new TextBox();
            labelSearchTime = new Label();
            textSearchTime = new TextBox();
            picAbilityCapsule = new PictureBox();
            picBitterHerba = new PictureBox();
            labelBitterHerba = new Label();
            textBitterHerba = new TextBox();
            labelAbilityCapsule = new Label();
            textAbilityCapsule = new TextBox();
            DaySkips = new Label();
            ((System.ComponentModel.ISupportInitialize)Map).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBoxPokemon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TeraType).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picShinyAlert).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBottleCap).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picSaltyHerba).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picSpicyHerba).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picSourHerba).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picSweetHerba).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picAbilityPatch).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picAbilityCapsule).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBitterHerba).BeginInit();
            SuspendLayout();
            // 
            // Map
            // 
            Map.Location = new Point(564, 12);
            Map.Name = "Map";
            Map.Size = new Size(384, 384);
            Map.SizeMode = PictureBoxSizeMode.StretchImage;
            Map.TabIndex = 64;
            Map.TabStop = false;
            // 
            // Area
            // 
            Area.BackColor = Color.FromArgb(0, 5, 25);
            Area.BorderStyle = BorderStyle.None;
            Area.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            Area.ForeColor = SystemColors.GradientInactiveCaption;
            Area.Location = new Point(564, 12);
            Area.Name = "Area";
            Area.PlaceholderText = "Area";
            Area.Size = new Size(384, 25);
            Area.TabIndex = 65;
            Area.TextAlign = HorizontalAlignment.Center;
            // 
            // picBoxPokemon
            // 
            picBoxPokemon.Location = new Point(396, 12);
            picBoxPokemon.Name = "picBoxPokemon";
            picBoxPokemon.Size = new Size(128, 128);
            picBoxPokemon.SizeMode = PictureBoxSizeMode.Zoom;
            picBoxPokemon.TabIndex = 112;
            picBoxPokemon.TabStop = false;
            // 
            // Difficulty
            // 
            Difficulty.BackColor = Color.FromArgb(0, 5, 25);
            Difficulty.BorderStyle = BorderStyle.None;
            Difficulty.Enabled = false;
            Difficulty.Font = new Font("Segoe UI Emoji", 16F, FontStyle.Bold, GraphicsUnit.Point);
            Difficulty.ForeColor = Color.LemonChiffon;
            Difficulty.Location = new Point(64, 143);
            Difficulty.Name = "Difficulty";
            Difficulty.PlaceholderText = "Difficulty";
            Difficulty.Size = new Size(244, 29);
            Difficulty.TabIndex = 114;
            Difficulty.Text = "⭐⭐⭐⭐⭐⭐⭐";
            Difficulty.TextAlign = HorizontalAlignment.Center;
            // 
            // Species
            // 
            Species.BackColor = Color.FromArgb(0, 5, 25);
            Species.BorderStyle = BorderStyle.None;
            Species.Enabled = false;
            Species.Font = new Font("Segoe UI", 32F, FontStyle.Bold, GraphicsUnit.Point);
            Species.ForeColor = SystemColors.ControlLightLight;
            Species.Location = new Point(12, 32);
            Species.Name = "Species";
            Species.PlaceholderText = "Species";
            Species.Size = new Size(348, 57);
            Species.TabIndex = 115;
            Species.TextAlign = HorizontalAlignment.Center;
            // 
            // Shiny
            // 
            Shiny.AutoSize = true;
            Shiny.Font = new Font("Consolas", 16F, FontStyle.Bold, GraphicsUnit.Point);
            Shiny.ForeColor = Color.Gold;
            Shiny.Location = new Point(110, 20);
            Shiny.Name = "Shiny";
            Shiny.Size = new Size(148, 26);
            Shiny.TabIndex = 116;
            Shiny.Text = "✨ Shiny ✨";
            Shiny.TextAlign = ContentAlignment.MiddleCenter;
            Shiny.Visible = false;
            // 
            // Gender
            // 
            Gender.BackColor = Color.FromArgb(0, 5, 25);
            Gender.BorderStyle = BorderStyle.None;
            Gender.Enabled = false;
            Gender.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            Gender.ForeColor = SystemColors.ActiveCaption;
            Gender.Location = new Point(406, 146);
            Gender.Name = "Gender";
            Gender.PlaceholderText = "Gender";
            Gender.Size = new Size(109, 22);
            Gender.TabIndex = 118;
            Gender.TextAlign = HorizontalAlignment.Center;
            // 
            // debug
            // 
            debug.BackColor = Color.FromArgb(0, 5, 25);
            debug.BorderStyle = BorderStyle.None;
            debug.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            debug.ForeColor = SystemColors.ActiveCaption;
            debug.Location = new Point(12, 506);
            debug.Name = "debug";
            debug.PlaceholderText = "debug";
            debug.Size = new Size(936, 22);
            debug.TabIndex = 119;
            debug.TextAlign = HorizontalAlignment.Center;
            // 
            // TeraType
            // 
            TeraType.Location = new Point(86, 92);
            TeraType.Name = "TeraType";
            TeraType.Size = new Size(200, 48);
            TeraType.SizeMode = PictureBoxSizeMode.StretchImage;
            TeraType.TabIndex = 120;
            TeraType.TabStop = false;
            // 
            // Ability
            // 
            Ability.BackColor = Color.FromArgb(0, 5, 25);
            Ability.BorderStyle = BorderStyle.None;
            Ability.Enabled = false;
            Ability.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            Ability.ForeColor = SystemColors.ActiveCaption;
            Ability.Location = new Point(72, 22);
            Ability.Name = "Ability";
            Ability.PlaceholderText = "Ability";
            Ability.Size = new Size(200, 25);
            Ability.TabIndex = 121;
            // 
            // Nature
            // 
            Nature.BackColor = Color.FromArgb(0, 5, 25);
            Nature.BorderStyle = BorderStyle.None;
            Nature.Enabled = false;
            Nature.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            Nature.ForeColor = SystemColors.ActiveCaption;
            Nature.Location = new Point(328, 22);
            Nature.Name = "Nature";
            Nature.PlaceholderText = "Nature";
            Nature.Size = new Size(200, 25);
            Nature.TabIndex = 122;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(labelAbility);
            groupBox1.Controls.Add(Ability);
            groupBox1.Controls.Add(Nature);
            groupBox1.ForeColor = Color.DarkGray;
            groupBox1.Location = new Point(12, 191);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(546, 65);
            groupBox1.TabIndex = 123;
            groupBox1.TabStop = false;
            groupBox1.Text = "Details";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.Silver;
            label1.Location = new Point(278, 29);
            label1.Name = "label1";
            label1.Size = new Size(46, 15);
            label1.TabIndex = 124;
            label1.Text = "Nature:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // labelAbility
            // 
            labelAbility.AutoSize = true;
            labelAbility.ForeColor = Color.Silver;
            labelAbility.Location = new Point(22, 29);
            labelAbility.Name = "labelAbility";
            labelAbility.Size = new Size(44, 15);
            labelAbility.TabIndex = 123;
            labelAbility.Text = "Ability:";
            labelAbility.TextAlign = ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(labelSpeed);
            groupBox2.Controls.Add(SPEED);
            groupBox2.Controls.Add(labelSpD);
            groupBox2.Controls.Add(SPD);
            groupBox2.Controls.Add(labelSpA);
            groupBox2.Controls.Add(SPA);
            groupBox2.Controls.Add(labelDef);
            groupBox2.Controls.Add(DEF);
            groupBox2.Controls.Add(labelAtk);
            groupBox2.Controls.Add(ATK);
            groupBox2.Controls.Add(labelHP);
            groupBox2.Controls.Add(HP);
            groupBox2.ForeColor = Color.DarkGray;
            groupBox2.Location = new Point(12, 262);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(546, 65);
            groupBox2.TabIndex = 124;
            groupBox2.TabStop = false;
            groupBox2.Text = "IVs";
            // 
            // labelSpeed
            // 
            labelSpeed.AutoSize = true;
            labelSpeed.ForeColor = Color.Silver;
            labelSpeed.Location = new Point(407, 29);
            labelSpeed.Name = "labelSpeed";
            labelSpeed.Size = new Size(42, 15);
            labelSpeed.TabIndex = 133;
            labelSpeed.Text = "Speed:";
            labelSpeed.TextAlign = ContentAlignment.MiddleRight;
            // 
            // SPEED
            // 
            SPEED.BackColor = Color.FromArgb(0, 5, 25);
            SPEED.BorderStyle = BorderStyle.None;
            SPEED.Enabled = false;
            SPEED.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            SPEED.ForeColor = Color.White;
            SPEED.Location = new Point(454, 21);
            SPEED.Name = "SPEED";
            SPEED.PlaceholderText = "00";
            SPEED.Size = new Size(30, 25);
            SPEED.TabIndex = 132;
            SPEED.TextAlign = HorizontalAlignment.Center;
            // 
            // labelSpD
            // 
            labelSpD.AutoSize = true;
            labelSpD.ForeColor = Color.Silver;
            labelSpD.Location = new Point(336, 29);
            labelSpD.Name = "labelSpD";
            labelSpD.Size = new Size(31, 15);
            labelSpD.TabIndex = 131;
            labelSpD.Text = "SpD:";
            labelSpD.TextAlign = ContentAlignment.MiddleRight;
            // 
            // SPD
            // 
            SPD.BackColor = Color.FromArgb(0, 5, 25);
            SPD.BorderStyle = BorderStyle.None;
            SPD.Enabled = false;
            SPD.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            SPD.ForeColor = Color.White;
            SPD.Location = new Point(372, 21);
            SPD.Name = "SPD";
            SPD.PlaceholderText = "00";
            SPD.Size = new Size(30, 25);
            SPD.TabIndex = 130;
            SPD.TextAlign = HorizontalAlignment.Center;
            // 
            // labelSpA
            // 
            labelSpA.AutoSize = true;
            labelSpA.ForeColor = Color.Silver;
            labelSpA.Location = new Point(265, 29);
            labelSpA.Name = "labelSpA";
            labelSpA.Size = new Size(31, 15);
            labelSpA.TabIndex = 129;
            labelSpA.Text = "SpA:";
            labelSpA.TextAlign = ContentAlignment.MiddleRight;
            // 
            // SPA
            // 
            SPA.BackColor = Color.FromArgb(0, 5, 25);
            SPA.BorderStyle = BorderStyle.None;
            SPA.Enabled = false;
            SPA.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            SPA.ForeColor = Color.White;
            SPA.Location = new Point(301, 21);
            SPA.Name = "SPA";
            SPA.PlaceholderText = "00";
            SPA.Size = new Size(30, 25);
            SPA.TabIndex = 128;
            SPA.TextAlign = HorizontalAlignment.Center;
            // 
            // labelDef
            // 
            labelDef.AutoSize = true;
            labelDef.ForeColor = Color.Silver;
            labelDef.Location = new Point(197, 29);
            labelDef.Name = "labelDef";
            labelDef.Size = new Size(28, 15);
            labelDef.TabIndex = 127;
            labelDef.Text = "Def:";
            labelDef.TextAlign = ContentAlignment.MiddleRight;
            // 
            // DEF
            // 
            DEF.BackColor = Color.FromArgb(0, 5, 25);
            DEF.BorderStyle = BorderStyle.None;
            DEF.Enabled = false;
            DEF.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            DEF.ForeColor = Color.White;
            DEF.Location = new Point(230, 21);
            DEF.Name = "DEF";
            DEF.PlaceholderText = "00";
            DEF.Size = new Size(30, 25);
            DEF.TabIndex = 126;
            DEF.TextAlign = HorizontalAlignment.Center;
            // 
            // labelAtk
            // 
            labelAtk.AutoSize = true;
            labelAtk.ForeColor = Color.Silver;
            labelAtk.Location = new Point(129, 29);
            labelAtk.Name = "labelAtk";
            labelAtk.Size = new Size(28, 15);
            labelAtk.TabIndex = 125;
            labelAtk.Text = "Atk:";
            labelAtk.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ATK
            // 
            ATK.BackColor = Color.FromArgb(0, 5, 25);
            ATK.BorderStyle = BorderStyle.None;
            ATK.Enabled = false;
            ATK.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            ATK.ForeColor = Color.White;
            ATK.Location = new Point(162, 21);
            ATK.Name = "ATK";
            ATK.PlaceholderText = "00";
            ATK.Size = new Size(30, 25);
            ATK.TabIndex = 124;
            ATK.TextAlign = HorizontalAlignment.Center;
            // 
            // labelHP
            // 
            labelHP.AutoSize = true;
            labelHP.ForeColor = Color.Silver;
            labelHP.Location = new Point(63, 29);
            labelHP.Name = "labelHP";
            labelHP.Size = new Size(26, 15);
            labelHP.TabIndex = 123;
            labelHP.Text = "HP:";
            labelHP.TextAlign = ContentAlignment.MiddleRight;
            // 
            // HP
            // 
            HP.BackColor = Color.FromArgb(0, 5, 25);
            HP.BorderStyle = BorderStyle.None;
            HP.Enabled = false;
            HP.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            HP.ForeColor = Color.White;
            HP.Location = new Point(94, 21);
            HP.Name = "HP";
            HP.PlaceholderText = "00";
            HP.Size = new Size(30, 25);
            HP.TabIndex = 121;
            HP.TextAlign = HorizontalAlignment.Center;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(Move8);
            groupBox3.Controls.Add(Move6);
            groupBox3.Controls.Add(Move4);
            groupBox3.Controls.Add(Move7);
            groupBox3.Controls.Add(Move5);
            groupBox3.Controls.Add(Move3);
            groupBox3.Controls.Add(Move1);
            groupBox3.Controls.Add(Move2);
            groupBox3.ForeColor = Color.DarkGray;
            groupBox3.Location = new Point(12, 333);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(546, 157);
            groupBox3.TabIndex = 125;
            groupBox3.TabStop = false;
            groupBox3.Text = "Raid Moveset";
            // 
            // Move8
            // 
            Move8.BackColor = Color.FromArgb(0, 5, 25);
            Move8.BorderStyle = BorderStyle.None;
            Move8.Enabled = false;
            Move8.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            Move8.ForeColor = Color.LightSlateGray;
            Move8.Location = new Point(278, 115);
            Move8.Name = "Move8";
            Move8.PlaceholderText = "Move8";
            Move8.Size = new Size(200, 25);
            Move8.TabIndex = 128;
            // 
            // Move6
            // 
            Move6.BackColor = Color.FromArgb(0, 5, 25);
            Move6.BorderStyle = BorderStyle.None;
            Move6.Enabled = false;
            Move6.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            Move6.ForeColor = Color.LightSlateGray;
            Move6.Location = new Point(278, 84);
            Move6.Name = "Move6";
            Move6.PlaceholderText = "Move6";
            Move6.Size = new Size(200, 25);
            Move6.TabIndex = 127;
            // 
            // Move4
            // 
            Move4.BackColor = Color.FromArgb(0, 5, 25);
            Move4.BorderStyle = BorderStyle.None;
            Move4.Enabled = false;
            Move4.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            Move4.ForeColor = SystemColors.ActiveCaption;
            Move4.Location = new Point(278, 53);
            Move4.Name = "Move4";
            Move4.PlaceholderText = "Move4";
            Move4.Size = new Size(200, 25);
            Move4.TabIndex = 126;
            // 
            // Move7
            // 
            Move7.BackColor = Color.FromArgb(0, 5, 25);
            Move7.BorderStyle = BorderStyle.None;
            Move7.Enabled = false;
            Move7.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            Move7.ForeColor = Color.LightSlateGray;
            Move7.Location = new Point(22, 115);
            Move7.Name = "Move7";
            Move7.PlaceholderText = "Move7";
            Move7.Size = new Size(200, 25);
            Move7.TabIndex = 125;
            // 
            // Move5
            // 
            Move5.BackColor = Color.FromArgb(0, 5, 25);
            Move5.BorderStyle = BorderStyle.None;
            Move5.Enabled = false;
            Move5.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            Move5.ForeColor = Color.LightSlateGray;
            Move5.Location = new Point(22, 84);
            Move5.Name = "Move5";
            Move5.PlaceholderText = "Move5";
            Move5.Size = new Size(200, 25);
            Move5.TabIndex = 124;
            // 
            // Move3
            // 
            Move3.BackColor = Color.FromArgb(0, 5, 25);
            Move3.BorderStyle = BorderStyle.None;
            Move3.Enabled = false;
            Move3.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            Move3.ForeColor = SystemColors.ActiveCaption;
            Move3.Location = new Point(22, 53);
            Move3.Name = "Move3";
            Move3.PlaceholderText = "Move3";
            Move3.Size = new Size(200, 25);
            Move3.TabIndex = 123;
            // 
            // Move1
            // 
            Move1.BackColor = Color.FromArgb(0, 5, 25);
            Move1.BorderStyle = BorderStyle.None;
            Move1.Enabled = false;
            Move1.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            Move1.ForeColor = SystemColors.ActiveCaption;
            Move1.Location = new Point(22, 22);
            Move1.Name = "Move1";
            Move1.PlaceholderText = "Move1";
            Move1.Size = new Size(200, 25);
            Move1.TabIndex = 121;
            // 
            // Move2
            // 
            Move2.BackColor = Color.FromArgb(0, 5, 25);
            Move2.BorderStyle = BorderStyle.None;
            Move2.Enabled = false;
            Move2.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            Move2.ForeColor = SystemColors.ActiveCaption;
            Move2.Location = new Point(278, 22);
            Move2.Name = "Move2";
            Move2.PlaceholderText = "Move2";
            Move2.Size = new Size(200, 25);
            Move2.TabIndex = 122;
            // 
            // picShinyAlert
            // 
            picShinyAlert.BackColor = Color.Transparent;
            picShinyAlert.Enabled = false;
            picShinyAlert.Image = (Image)resources.GetObject("picShinyAlert.Image");
            picShinyAlert.Location = new Point(300, 92);
            picShinyAlert.Name = "picShinyAlert";
            picShinyAlert.Size = new Size(79, 77);
            picShinyAlert.SizeMode = PictureBoxSizeMode.Zoom;
            picShinyAlert.TabIndex = 160;
            picShinyAlert.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Location = new Point(12, 501);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(639, 27);
            pictureBox1.TabIndex = 126;
            pictureBox1.TabStop = false;
            // 
            // picBottleCap
            // 
            picBottleCap.Image = Properties.Resources.item_0795_hires;
            picBottleCap.Location = new Point(560, 448);
            picBottleCap.Name = "picBottleCap";
            picBottleCap.Size = new Size(48, 48);
            picBottleCap.SizeMode = PictureBoxSizeMode.StretchImage;
            picBottleCap.TabIndex = 138;
            picBottleCap.TabStop = false;
            // 
            // picSaltyHerba
            // 
            picSaltyHerba.Image = Properties.Resources.item_1905_hires;
            picSaltyHerba.Location = new Point(857, 402);
            picSaltyHerba.Name = "picSaltyHerba";
            picSaltyHerba.Size = new Size(48, 48);
            picSaltyHerba.SizeMode = PictureBoxSizeMode.StretchImage;
            picSaltyHerba.TabIndex = 137;
            picSaltyHerba.TabStop = false;
            // 
            // picSpicyHerba
            // 
            picSpicyHerba.Image = Properties.Resources.item_1908_hires;
            picSpicyHerba.Location = new Point(857, 448);
            picSpicyHerba.Name = "picSpicyHerba";
            picSpicyHerba.Size = new Size(48, 48);
            picSpicyHerba.SizeMode = PictureBoxSizeMode.StretchImage;
            picSpicyHerba.TabIndex = 131;
            picSpicyHerba.TabStop = false;
            // 
            // picSourHerba
            // 
            picSourHerba.Image = Properties.Resources.item_1906_hires;
            picSourHerba.Location = new Point(759, 448);
            picSourHerba.Name = "picSourHerba";
            picSourHerba.Size = new Size(48, 48);
            picSourHerba.SizeMode = PictureBoxSizeMode.StretchImage;
            picSourHerba.TabIndex = 130;
            picSourHerba.TabStop = false;
            // 
            // picSweetHerba
            // 
            picSweetHerba.Image = Properties.Resources.item_1904_hires;
            picSweetHerba.Location = new Point(759, 402);
            picSweetHerba.Name = "picSweetHerba";
            picSweetHerba.Size = new Size(48, 48);
            picSweetHerba.SizeMode = PictureBoxSizeMode.StretchImage;
            picSweetHerba.TabIndex = 129;
            picSweetHerba.TabStop = false;
            // 
            // picAbilityPatch
            // 
            picAbilityPatch.Image = Properties.Resources.item_1606_hires;
            picAbilityPatch.Location = new Point(560, 402);
            picAbilityPatch.Name = "picAbilityPatch";
            picAbilityPatch.Size = new Size(48, 48);
            picAbilityPatch.SizeMode = PictureBoxSizeMode.StretchImage;
            picAbilityPatch.TabIndex = 128;
            picAbilityPatch.TabStop = false;
            // 
            // labelAbilityPatch
            // 
            labelAbilityPatch.AutoSize = true;
            labelAbilityPatch.ForeColor = Color.Silver;
            labelAbilityPatch.Location = new Point(614, 404);
            labelAbilityPatch.Name = "labelAbilityPatch";
            labelAbilityPatch.Size = new Size(37, 15);
            labelAbilityPatch.TabIndex = 135;
            labelAbilityPatch.Text = "Patch";
            labelAbilityPatch.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textAbilityPatch
            // 
            textAbilityPatch.BackColor = Color.FromArgb(0, 5, 25);
            textAbilityPatch.BorderStyle = BorderStyle.None;
            textAbilityPatch.Enabled = false;
            textAbilityPatch.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            textAbilityPatch.ForeColor = Color.White;
            textAbilityPatch.Location = new Point(616, 419);
            textAbilityPatch.Name = "textAbilityPatch";
            textAbilityPatch.PlaceholderText = "00";
            textAbilityPatch.Size = new Size(30, 25);
            textAbilityPatch.TabIndex = 134;
            textAbilityPatch.TextAlign = HorizontalAlignment.Center;
            textAbilityPatch.TextChanged += Rewards_TextChanged;
            // 
            // labelSweetHerba
            // 
            labelSweetHerba.AutoSize = true;
            labelSweetHerba.ForeColor = Color.Silver;
            labelSweetHerba.Location = new Point(813, 404);
            labelSweetHerba.Name = "labelSweetHerba";
            labelSweetHerba.Size = new Size(38, 15);
            labelSweetHerba.TabIndex = 143;
            labelSweetHerba.Text = "Sweet";
            labelSweetHerba.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textSweetHerba
            // 
            textSweetHerba.BackColor = Color.FromArgb(0, 5, 25);
            textSweetHerba.BorderStyle = BorderStyle.None;
            textSweetHerba.Enabled = false;
            textSweetHerba.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            textSweetHerba.ForeColor = Color.White;
            textSweetHerba.Location = new Point(817, 419);
            textSweetHerba.Name = "textSweetHerba";
            textSweetHerba.PlaceholderText = "00";
            textSweetHerba.Size = new Size(30, 25);
            textSweetHerba.TabIndex = 142;
            textSweetHerba.TextAlign = HorizontalAlignment.Center;
            textSweetHerba.TextChanged += Rewards_TextChanged;
            // 
            // labelSaltyHerba
            // 
            labelSaltyHerba.AutoSize = true;
            labelSaltyHerba.ForeColor = Color.Silver;
            labelSaltyHerba.Location = new Point(910, 404);
            labelSaltyHerba.Name = "labelSaltyHerba";
            labelSaltyHerba.Size = new Size(32, 15);
            labelSaltyHerba.TabIndex = 145;
            labelSaltyHerba.Text = "Salty";
            labelSaltyHerba.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textSaltyHerba
            // 
            textSaltyHerba.BackColor = Color.FromArgb(0, 5, 25);
            textSaltyHerba.BorderStyle = BorderStyle.None;
            textSaltyHerba.Enabled = false;
            textSaltyHerba.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            textSaltyHerba.ForeColor = Color.White;
            textSaltyHerba.Location = new Point(911, 419);
            textSaltyHerba.Name = "textSaltyHerba";
            textSaltyHerba.PlaceholderText = "00";
            textSaltyHerba.Size = new Size(30, 25);
            textSaltyHerba.TabIndex = 144;
            textSaltyHerba.TextAlign = HorizontalAlignment.Center;
            textSaltyHerba.TextChanged += Rewards_TextChanged;
            // 
            // labelBottleCap
            // 
            labelBottleCap.AutoSize = true;
            labelBottleCap.ForeColor = Color.Silver;
            labelBottleCap.Location = new Point(618, 452);
            labelBottleCap.Name = "labelBottleCap";
            labelBottleCap.Size = new Size(28, 15);
            labelBottleCap.TabIndex = 147;
            labelBottleCap.Text = "Cap";
            labelBottleCap.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textBottleCap
            // 
            textBottleCap.BackColor = Color.FromArgb(0, 5, 25);
            textBottleCap.BorderStyle = BorderStyle.None;
            textBottleCap.Enabled = false;
            textBottleCap.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            textBottleCap.ForeColor = Color.White;
            textBottleCap.Location = new Point(616, 467);
            textBottleCap.Name = "textBottleCap";
            textBottleCap.PlaceholderText = "00";
            textBottleCap.Size = new Size(30, 25);
            textBottleCap.TabIndex = 146;
            textBottleCap.TextAlign = HorizontalAlignment.Center;
            textBottleCap.TextChanged += Rewards_TextChanged;
            // 
            // labelSourHerba
            // 
            labelSourHerba.AutoSize = true;
            labelSourHerba.ForeColor = Color.Silver;
            labelSourHerba.Location = new Point(817, 452);
            labelSourHerba.Name = "labelSourHerba";
            labelSourHerba.Size = new Size(31, 15);
            labelSourHerba.TabIndex = 149;
            labelSourHerba.Text = "Sour";
            labelSourHerba.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textSourHerba
            // 
            textSourHerba.BackColor = Color.FromArgb(0, 5, 25);
            textSourHerba.BorderStyle = BorderStyle.None;
            textSourHerba.Enabled = false;
            textSourHerba.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            textSourHerba.ForeColor = Color.White;
            textSourHerba.Location = new Point(817, 467);
            textSourHerba.Name = "textSourHerba";
            textSourHerba.PlaceholderText = "00";
            textSourHerba.Size = new Size(30, 25);
            textSourHerba.TabIndex = 148;
            textSourHerba.TextAlign = HorizontalAlignment.Center;
            textSourHerba.TextChanged += Rewards_TextChanged;
            // 
            // labelSpicyHerba
            // 
            labelSpicyHerba.AutoSize = true;
            labelSpicyHerba.ForeColor = Color.Silver;
            labelSpicyHerba.Location = new Point(908, 450);
            labelSpicyHerba.Name = "labelSpicyHerba";
            labelSpicyHerba.Size = new Size(35, 15);
            labelSpicyHerba.TabIndex = 151;
            labelSpicyHerba.Text = "Spicy";
            labelSpicyHerba.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textSpicyHerba
            // 
            textSpicyHerba.BackColor = Color.FromArgb(0, 5, 25);
            textSpicyHerba.BorderStyle = BorderStyle.None;
            textSpicyHerba.Enabled = false;
            textSpicyHerba.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            textSpicyHerba.ForeColor = Color.White;
            textSpicyHerba.Location = new Point(911, 467);
            textSpicyHerba.Name = "textSpicyHerba";
            textSpicyHerba.PlaceholderText = "00";
            textSpicyHerba.Size = new Size(30, 25);
            textSpicyHerba.TabIndex = 150;
            textSpicyHerba.TextAlign = HorizontalAlignment.Center;
            textSpicyHerba.TextChanged += Rewards_TextChanged;
            // 
            // labelSearchTime
            // 
            labelSearchTime.AutoSize = true;
            labelSearchTime.BackColor = Color.Transparent;
            labelSearchTime.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            labelSearchTime.ForeColor = Color.LightSlateGray;
            labelSearchTime.Location = new Point(682, 502);
            labelSearchTime.Name = "labelSearchTime";
            labelSearchTime.Size = new Size(189, 25);
            labelSearchTime.TabIndex = 152;
            labelSearchTime.Text = "Search Time Elapsed:";
            labelSearchTime.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textSearchTime
            // 
            textSearchTime.BackColor = Color.FromArgb(0, 5, 25);
            textSearchTime.BorderStyle = BorderStyle.None;
            textSearchTime.Enabled = false;
            textSearchTime.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            textSearchTime.ForeColor = Color.LightSlateGray;
            textSearchTime.Location = new Point(868, 502);
            textSearchTime.Name = "textSearchTime";
            textSearchTime.PlaceholderText = "00";
            textSearchTime.Size = new Size(80, 25);
            textSearchTime.TabIndex = 153;
            textSearchTime.Text = "00:00:00";
            textSearchTime.TextAlign = HorizontalAlignment.Center;
            // 
            // picAbilityCapsule
            // 
            picAbilityCapsule.Image = Properties.Resources.item_0645_hires;
            picAbilityCapsule.Location = new Point(656, 402);
            picAbilityCapsule.Name = "picAbilityCapsule";
            picAbilityCapsule.Size = new Size(48, 48);
            picAbilityCapsule.SizeMode = PictureBoxSizeMode.StretchImage;
            picAbilityCapsule.TabIndex = 154;
            picAbilityCapsule.TabStop = false;
            // 
            // picBitterHerba
            // 
            picBitterHerba.Image = Properties.Resources.item_1907_hires;
            picBitterHerba.Location = new Point(656, 448);
            picBitterHerba.Name = "picBitterHerba";
            picBitterHerba.Size = new Size(48, 48);
            picBitterHerba.SizeMode = PictureBoxSizeMode.StretchImage;
            picBitterHerba.TabIndex = 155;
            picBitterHerba.TabStop = false;
            // 
            // labelBitterHerba
            // 
            labelBitterHerba.AutoSize = true;
            labelBitterHerba.ForeColor = Color.Silver;
            labelBitterHerba.Location = new Point(713, 452);
            labelBitterHerba.Name = "labelBitterHerba";
            labelBitterHerba.Size = new Size(35, 15);
            labelBitterHerba.TabIndex = 159;
            labelBitterHerba.Text = "Bitter";
            labelBitterHerba.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textBitterHerba
            // 
            textBitterHerba.BackColor = Color.FromArgb(0, 5, 25);
            textBitterHerba.BorderStyle = BorderStyle.None;
            textBitterHerba.Enabled = false;
            textBitterHerba.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            textBitterHerba.ForeColor = Color.White;
            textBitterHerba.Location = new Point(714, 467);
            textBitterHerba.Name = "textBitterHerba";
            textBitterHerba.PlaceholderText = "00";
            textBitterHerba.Size = new Size(30, 25);
            textBitterHerba.TabIndex = 158;
            textBitterHerba.TextAlign = HorizontalAlignment.Center;
            // 
            // labelAbilityCapsule
            // 
            labelAbilityCapsule.AutoSize = true;
            labelAbilityCapsule.ForeColor = Color.Silver;
            labelAbilityCapsule.Location = new Point(707, 404);
            labelAbilityCapsule.Name = "labelAbilityCapsule";
            labelAbilityCapsule.Size = new Size(49, 15);
            labelAbilityCapsule.TabIndex = 157;
            labelAbilityCapsule.Text = "Capsule";
            labelAbilityCapsule.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textAbilityCapsule
            // 
            textAbilityCapsule.BackColor = Color.FromArgb(0, 5, 25);
            textAbilityCapsule.BorderStyle = BorderStyle.None;
            textAbilityCapsule.Enabled = false;
            textAbilityCapsule.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            textAbilityCapsule.ForeColor = Color.White;
            textAbilityCapsule.Location = new Point(715, 419);
            textAbilityCapsule.Name = "textAbilityCapsule";
            textAbilityCapsule.PlaceholderText = "00";
            textAbilityCapsule.Size = new Size(30, 25);
            textAbilityCapsule.TabIndex = 156;
            textAbilityCapsule.TextAlign = HorizontalAlignment.Center;
            // 
            // DaySkips
            // 
            DaySkips.Anchor = AnchorStyles.None;
            DaySkips.AutoSize = true;
            DaySkips.BackColor = Color.Transparent;
            DaySkips.ForeColor = SystemColors.ButtonShadow;
            DaySkips.Location = new Point(700, 508);
            DaySkips.Name = "DaySkips";
            DaySkips.Size = new Size(133, 15);
            DaySkips.TabIndex = 161;
            DaySkips.Text = "Day Skip Successes 0 / 0";
            DaySkips.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TeraRaidView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 5, 25);
            ClientSize = new Size(960, 540);
            Controls.Add(DaySkips);
            Controls.Add(labelBitterHerba);
            Controls.Add(textBitterHerba);
            Controls.Add(labelAbilityCapsule);
            Controls.Add(textAbilityCapsule);
            Controls.Add(picBitterHerba);
            Controls.Add(picAbilityCapsule);
            Controls.Add(textSearchTime);
            Controls.Add(labelSpicyHerba);
            Controls.Add(textSpicyHerba);
            Controls.Add(labelSourHerba);
            Controls.Add(textSourHerba);
            Controls.Add(labelBottleCap);
            Controls.Add(textBottleCap);
            Controls.Add(labelSaltyHerba);
            Controls.Add(textSaltyHerba);
            Controls.Add(labelSweetHerba);
            Controls.Add(textSweetHerba);
            Controls.Add(labelAbilityPatch);
            Controls.Add(textAbilityPatch);
            Controls.Add(picBottleCap);
            Controls.Add(picSaltyHerba);
            Controls.Add(picSpicyHerba);
            Controls.Add(picSourHerba);
            Controls.Add(picSweetHerba);
            Controls.Add(picAbilityPatch);
            Controls.Add(pictureBox1);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(TeraType);
            Controls.Add(debug);
            Controls.Add(Gender);
            Controls.Add(Shiny);
            Controls.Add(Species);
            Controls.Add(Difficulty);
            Controls.Add(picBoxPokemon);
            Controls.Add(Area);
            Controls.Add(labelSearchTime);
            Controls.Add(Map);
            Controls.Add(picShinyAlert);
            ForeColor = SystemColors.ActiveCaptionText;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "TeraRaidView";
            Text = "TeraRaidView";
            DoubleClick += TeraRaidView_DoubleClick;
            MouseDown += TeraRaidView_MouseDown;
            MouseMove += TeraRaidView_MouseMove;
            MouseUp += TeraRaidView_MouseUp;
            ((System.ComponentModel.ISupportInitialize)Map).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBoxPokemon).EndInit();
            ((System.ComponentModel.ISupportInitialize)TeraType).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picShinyAlert).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBottleCap).EndInit();
            ((System.ComponentModel.ISupportInitialize)picSaltyHerba).EndInit();
            ((System.ComponentModel.ISupportInitialize)picSpicyHerba).EndInit();
            ((System.ComponentModel.ISupportInitialize)picSourHerba).EndInit();
            ((System.ComponentModel.ISupportInitialize)picSweetHerba).EndInit();
            ((System.ComponentModel.ISupportInitialize)picAbilityPatch).EndInit();
            ((System.ComponentModel.ISupportInitialize)picAbilityCapsule).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBitterHerba).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        public TextBox Area;
        public TextBox Difficulty;
        public TextBox Species;
        public PictureBox picBoxPokemon;
        public TextBox Gender;
        public TextBox debug;
        public PictureBox TeraType;
        public TextBox Ability;
        public TextBox Nature;
        private GroupBox groupBox1;
        private Label label1;
        private Label labelAbility;
        public PictureBox Map;
        private GroupBox groupBox2;
        private Label labelHP;
        public TextBox HP;
        private GroupBox groupBox3;
        public TextBox Move1;
        public TextBox Move2;
        private Label labelSpeed;
        public TextBox SPEED;
        private Label labelSpD;
        public TextBox SPD;
        private Label labelSpA;
        public TextBox SPA;
        private Label labelDef;
        public TextBox DEF;
        private Label labelAtk;
        public TextBox ATK;
        public TextBox Move8;
        public TextBox Move6;
        public TextBox Move4;
        public TextBox Move7;
        public TextBox Move5;
        public TextBox Move3;
        private PictureBox pictureBox1;
        public TextBox textAbilityPatch;
        public TextBox textSweetHerba;
        public TextBox textSaltyHerba;
        public TextBox textBottleCap;
        public TextBox textSourHerba;
        public TextBox textSpicyHerba;
        public Label labelSearchTime;
        public TextBox textSearchTime;
        public TextBox textBitterHerba;
        public TextBox textAbilityCapsule;
        public PictureBox picBottleCap;
        public PictureBox picSaltyHerba;
        public PictureBox picSpicyHerba;
        public PictureBox picSourHerba;
        public PictureBox picSweetHerba;
        public PictureBox picAbilityPatch;
        public Label labelAbilityPatch;
        public Label labelSweetHerba;
        public Label labelSaltyHerba;
        public Label labelBottleCap;
        public Label labelSourHerba;
        public Label labelSpicyHerba;
        public PictureBox picAbilityCapsule;
        public PictureBox picBitterHerba;
        public Label labelBitterHerba;
        public Label labelAbilityCapsule;
        public PictureBox picShinyAlert;
        public Label Shiny;
        public Label DaySkips;
    }
}
