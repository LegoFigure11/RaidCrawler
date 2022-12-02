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
            this.SpeciesCheck = new System.Windows.Forms.CheckBox();
            this.NatureCheck = new System.Windows.Forms.CheckBox();
            this.Nature = new System.Windows.Forms.ComboBox();
            this.StarCheck = new System.Windows.Forms.CheckBox();
            this.Stars = new System.Windows.Forms.ComboBox();
            this.ShinyCheck = new System.Windows.Forms.CheckBox();
            this.Save = new System.Windows.Forms.Button();
            this.PerfectIVFilterGroup = new System.Windows.Forms.GroupBox();
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
            this.SatisfyAny = new System.Windows.Forms.CheckBox();
            this.SpeciesFixed = new System.Windows.Forms.CheckBox();
            this.NatureFixed = new System.Windows.Forms.CheckBox();
            this.StarFixed = new System.Windows.Forms.CheckBox();
            this.LabelFix = new System.Windows.Forms.Label();
            this.LabelEnabled = new System.Windows.Forms.Label();
            this.TeraFixed = new System.Windows.Forms.CheckBox();
            this.TeraCheck = new System.Windows.Forms.CheckBox();
            this.TeraType = new System.Windows.Forms.ComboBox();
            this.Species = new System.Windows.Forms.CheckedListBox();
            this.PerfectIVFilterGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IVSPE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IVSPD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IVSPA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IVDEF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IVATK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IVHP)).BeginInit();
            this.SuspendLayout();
            // 
            // SpeciesCheck
            // 
            this.SpeciesCheck.AutoSize = true;
            this.SpeciesCheck.Location = new System.Drawing.Point(15, 25);
            this.SpeciesCheck.Name = "SpeciesCheck";
            this.SpeciesCheck.Size = new System.Drawing.Size(65, 19);
            this.SpeciesCheck.TabIndex = 1;
            this.SpeciesCheck.Text = "Species";
            this.SpeciesCheck.UseVisualStyleBackColor = true;
            this.SpeciesCheck.CheckedChanged += new System.EventHandler(this.SpeciesCheck_CheckedChanged);
            // 
            // NatureCheck
            // 
            this.NatureCheck.AutoSize = true;
            this.NatureCheck.Location = new System.Drawing.Point(15, 184);
            this.NatureCheck.Name = "NatureCheck";
            this.NatureCheck.Size = new System.Drawing.Size(62, 19);
            this.NatureCheck.TabIndex = 3;
            this.NatureCheck.Text = "Nature";
            this.NatureCheck.UseVisualStyleBackColor = true;
            this.NatureCheck.CheckedChanged += new System.EventHandler(this.NatureCheck_CheckedChanged);
            // 
            // Nature
            // 
            this.Nature.FormattingEnabled = true;
            this.Nature.Location = new System.Drawing.Point(104, 182);
            this.Nature.Name = "Nature";
            this.Nature.Size = new System.Drawing.Size(121, 23);
            this.Nature.TabIndex = 2;
            // 
            // StarCheck
            // 
            this.StarCheck.AutoSize = true;
            this.StarCheck.Location = new System.Drawing.Point(15, 213);
            this.StarCheck.Name = "StarCheck";
            this.StarCheck.Size = new System.Drawing.Size(51, 19);
            this.StarCheck.TabIndex = 5;
            this.StarCheck.Text = "Stars";
            this.StarCheck.UseVisualStyleBackColor = true;
            this.StarCheck.CheckedChanged += new System.EventHandler(this.StarCheck_CheckedChanged);
            // 
            // Stars
            // 
            this.Stars.FormattingEnabled = true;
            this.Stars.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.Stars.Location = new System.Drawing.Point(104, 211);
            this.Stars.Name = "Stars";
            this.Stars.Size = new System.Drawing.Size(121, 23);
            this.Stars.TabIndex = 4;
            // 
            // ShinyCheck
            // 
            this.ShinyCheck.AutoSize = true;
            this.ShinyCheck.Location = new System.Drawing.Point(15, 275);
            this.ShinyCheck.Name = "ShinyCheck";
            this.ShinyCheck.Size = new System.Drawing.Size(119, 19);
            this.ShinyCheck.TabIndex = 6;
            this.ShinyCheck.Text = "Search until shiny";
            this.ShinyCheck.UseVisualStyleBackColor = true;
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(12, 419);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(228, 23);
            this.Save.TabIndex = 9;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // PerfectIVFilterGroup
            // 
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
            this.PerfectIVFilterGroup.Location = new System.Drawing.Point(9, 300);
            this.PerfectIVFilterGroup.Name = "PerfectIVFilterGroup";
            this.PerfectIVFilterGroup.Size = new System.Drawing.Size(231, 90);
            this.PerfectIVFilterGroup.TabIndex = 10;
            this.PerfectIVFilterGroup.TabStop = false;
            this.PerfectIVFilterGroup.Text = "IV Filters";
            // 
            // IVSPE
            // 
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
            this.Spe.Location = new System.Drawing.Point(194, 51);
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
            this.SpD.Location = new System.Drawing.Point(154, 51);
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
            this.SpA.Location = new System.Drawing.Point(115, 51);
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
            this.Def.Location = new System.Drawing.Point(79, 51);
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
            this.Atk.Location = new System.Drawing.Point(41, 51);
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
            this.HP.Location = new System.Drawing.Point(3, 51);
            this.HP.Name = "HP";
            this.HP.Size = new System.Drawing.Size(27, 33);
            this.HP.TabIndex = 0;
            this.HP.Text = "HP";
            this.HP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.HP.UseVisualStyleBackColor = true;
            this.HP.CheckedChanged += new System.EventHandler(this.HP_CheckedChanged);
            // 
            // SatisfyAny
            // 
            this.SatisfyAny.AutoSize = true;
            this.SatisfyAny.Location = new System.Drawing.Point(15, 396);
            this.SatisfyAny.Name = "SatisfyAny";
            this.SatisfyAny.Size = new System.Drawing.Size(226, 19);
            this.SatisfyAny.TabIndex = 11;
            this.SatisfyAny.Text = "Stop if any optional filters are satisfied";
            this.SatisfyAny.UseVisualStyleBackColor = true;
            // 
            // SpeciesFixed
            // 
            this.SpeciesFixed.AutoSize = true;
            this.SpeciesFixed.Location = new System.Drawing.Point(231, 27);
            this.SpeciesFixed.Name = "SpeciesFixed";
            this.SpeciesFixed.Size = new System.Drawing.Size(15, 14);
            this.SpeciesFixed.TabIndex = 12;
            this.SpeciesFixed.UseVisualStyleBackColor = true;
            this.SpeciesFixed.CheckedChanged += new System.EventHandler(this.SpeciesFixed_CheckedChanged);
            // 
            // NatureFixed
            // 
            this.NatureFixed.AutoSize = true;
            this.NatureFixed.Location = new System.Drawing.Point(231, 186);
            this.NatureFixed.Name = "NatureFixed";
            this.NatureFixed.Size = new System.Drawing.Size(15, 14);
            this.NatureFixed.TabIndex = 13;
            this.NatureFixed.UseVisualStyleBackColor = true;
            this.NatureFixed.CheckedChanged += new System.EventHandler(this.NatureFixed_CheckedChanged);
            // 
            // StarFixed
            // 
            this.StarFixed.AutoSize = true;
            this.StarFixed.Location = new System.Drawing.Point(231, 215);
            this.StarFixed.Name = "StarFixed";
            this.StarFixed.Size = new System.Drawing.Size(15, 14);
            this.StarFixed.TabIndex = 14;
            this.StarFixed.UseVisualStyleBackColor = true;
            this.StarFixed.CheckedChanged += new System.EventHandler(this.StarFixed_CheckedChanged);
            // 
            // LabelFix
            // 
            this.LabelFix.AutoSize = true;
            this.LabelFix.Location = new System.Drawing.Point(212, 5);
            this.LabelFix.Name = "LabelFix";
            this.LabelFix.Size = new System.Drawing.Size(35, 15);
            this.LabelFix.TabIndex = 15;
            this.LabelFix.Text = "Fixed";
            // 
            // LabelEnabled
            // 
            this.LabelEnabled.AutoSize = true;
            this.LabelEnabled.Location = new System.Drawing.Point(12, 5);
            this.LabelEnabled.Name = "LabelEnabled";
            this.LabelEnabled.Size = new System.Drawing.Size(42, 15);
            this.LabelEnabled.TabIndex = 16;
            this.LabelEnabled.Text = "Enable";
            // 
            // TeraFixed
            // 
            this.TeraFixed.AutoSize = true;
            this.TeraFixed.Location = new System.Drawing.Point(231, 244);
            this.TeraFixed.Name = "TeraFixed";
            this.TeraFixed.Size = new System.Drawing.Size(15, 14);
            this.TeraFixed.TabIndex = 19;
            this.TeraFixed.UseVisualStyleBackColor = true;
            this.TeraFixed.CheckedChanged += new System.EventHandler(this.TeraFixed_CheckedChanged);
            // 
            // TeraCheck
            // 
            this.TeraCheck.AutoSize = true;
            this.TeraCheck.Location = new System.Drawing.Point(15, 242);
            this.TeraCheck.Name = "TeraCheck";
            this.TeraCheck.Size = new System.Drawing.Size(74, 19);
            this.TeraCheck.TabIndex = 18;
            this.TeraCheck.Text = "Tera Type";
            this.TeraCheck.UseVisualStyleBackColor = true;
            this.TeraCheck.CheckedChanged += new System.EventHandler(this.TeraCheck_CheckedChanged);
            // 
            // TeraType
            // 
            this.TeraType.FormattingEnabled = true;
            this.TeraType.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.TeraType.Location = new System.Drawing.Point(104, 240);
            this.TeraType.Name = "TeraType";
            this.TeraType.Size = new System.Drawing.Size(121, 23);
            this.TeraType.TabIndex = 17;
            // 
            // Species
            // 
            this.Species.FormattingEnabled = true;
            this.Species.Location = new System.Drawing.Point(104, 27);
            this.Species.Name = "Species";
            this.Species.Size = new System.Drawing.Size(120, 148);
            this.Species.TabIndex = 20;
            // 
            // FilterSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 472);
            this.Controls.Add(this.Species);
            this.Controls.Add(this.TeraFixed);
            this.Controls.Add(this.TeraCheck);
            this.Controls.Add(this.TeraType);
            this.Controls.Add(this.LabelEnabled);
            this.Controls.Add(this.LabelFix);
            this.Controls.Add(this.StarFixed);
            this.Controls.Add(this.NatureFixed);
            this.Controls.Add(this.SpeciesFixed);
            this.Controls.Add(this.SatisfyAny);
            this.Controls.Add(this.PerfectIVFilterGroup);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.ShinyCheck);
            this.Controls.Add(this.StarCheck);
            this.Controls.Add(this.Stars);
            this.Controls.Add(this.NatureCheck);
            this.Controls.Add(this.Nature);
            this.Controls.Add(this.SpeciesCheck);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FilterSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FilterSettings";
            this.PerfectIVFilterGroup.ResumeLayout(false);
            this.PerfectIVFilterGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IVSPE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IVSPD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IVSPA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IVDEF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IVATK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IVHP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private CheckBox SpeciesCheck;
        private CheckBox NatureCheck;
        private ComboBox Nature;
        private CheckBox StarCheck;
        private ComboBox Stars;
        private CheckBox ShinyCheck;
        private Button Save;
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
        private CheckBox SatisfyAny;
        private CheckBox SpeciesFixed;
        private CheckBox NatureFixed;
        private CheckBox StarFixed;
        private Label LabelFix;
        private Label LabelEnabled;
        private CheckBox TeraFixed;
        private CheckBox TeraCheck;
        private ComboBox TeraType;
        private CheckedListBox Species;
    }
}