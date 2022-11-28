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
            this.Species = new System.Windows.Forms.ComboBox();
            this.SpeciesCheck = new System.Windows.Forms.CheckBox();
            this.NatureCheck = new System.Windows.Forms.CheckBox();
            this.Nature = new System.Windows.Forms.ComboBox();
            this.StarCheck = new System.Windows.Forms.CheckBox();
            this.Stars = new System.Windows.Forms.ComboBox();
            this.ShinyCheck = new System.Windows.Forms.CheckBox();
            this.Save = new System.Windows.Forms.Button();
            this.PerfectIVFilterGroup = new System.Windows.Forms.GroupBox();
            this.HP = new System.Windows.Forms.CheckBox();
            this.Atk = new System.Windows.Forms.CheckBox();
            this.SpA = new System.Windows.Forms.CheckBox();
            this.Def = new System.Windows.Forms.CheckBox();
            this.SpD = new System.Windows.Forms.CheckBox();
            this.Spe = new System.Windows.Forms.CheckBox();
            this.PerfectIVFilterGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // Species
            // 
            this.Species.FormattingEnabled = true;
            this.Species.Location = new System.Drawing.Point(104, 12);
            this.Species.Name = "Species";
            this.Species.Size = new System.Drawing.Size(121, 23);
            this.Species.TabIndex = 0;
            // 
            // SpeciesCheck
            // 
            this.SpeciesCheck.AutoSize = true;
            this.SpeciesCheck.Location = new System.Drawing.Point(15, 14);
            this.SpeciesCheck.Name = "SpeciesCheck";
            this.SpeciesCheck.Size = new System.Drawing.Size(65, 19);
            this.SpeciesCheck.TabIndex = 1;
            this.SpeciesCheck.Text = "Species";
            this.SpeciesCheck.UseVisualStyleBackColor = true;
            // 
            // NatureCheck
            // 
            this.NatureCheck.AutoSize = true;
            this.NatureCheck.Location = new System.Drawing.Point(15, 43);
            this.NatureCheck.Name = "NatureCheck";
            this.NatureCheck.Size = new System.Drawing.Size(62, 19);
            this.NatureCheck.TabIndex = 3;
            this.NatureCheck.Text = "Nature";
            this.NatureCheck.UseVisualStyleBackColor = true;
            // 
            // Nature
            // 
            this.Nature.FormattingEnabled = true;
            this.Nature.Location = new System.Drawing.Point(104, 41);
            this.Nature.Name = "Nature";
            this.Nature.Size = new System.Drawing.Size(121, 23);
            this.Nature.TabIndex = 2;
            // 
            // StarCheck
            // 
            this.StarCheck.AutoSize = true;
            this.StarCheck.Location = new System.Drawing.Point(15, 72);
            this.StarCheck.Name = "StarCheck";
            this.StarCheck.Size = new System.Drawing.Size(51, 19);
            this.StarCheck.TabIndex = 5;
            this.StarCheck.Text = "Stars";
            this.StarCheck.UseVisualStyleBackColor = true;
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
            this.Stars.Location = new System.Drawing.Point(104, 70);
            this.Stars.Name = "Stars";
            this.Stars.Size = new System.Drawing.Size(121, 23);
            this.Stars.TabIndex = 4;
            // 
            // ShinyCheck
            // 
            this.ShinyCheck.AutoSize = true;
            this.ShinyCheck.Location = new System.Drawing.Point(15, 97);
            this.ShinyCheck.Name = "ShinyCheck";
            this.ShinyCheck.Size = new System.Drawing.Size(119, 19);
            this.ShinyCheck.TabIndex = 6;
            this.ShinyCheck.Text = "Search until shiny";
            this.ShinyCheck.UseVisualStyleBackColor = true;
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(12, 185);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(210, 23);
            this.Save.TabIndex = 9;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // PerfectIVFilterGroup
            // 
            this.PerfectIVFilterGroup.Controls.Add(this.Spe);
            this.PerfectIVFilterGroup.Controls.Add(this.SpD);
            this.PerfectIVFilterGroup.Controls.Add(this.SpA);
            this.PerfectIVFilterGroup.Controls.Add(this.Def);
            this.PerfectIVFilterGroup.Controls.Add(this.Atk);
            this.PerfectIVFilterGroup.Controls.Add(this.HP);
            this.PerfectIVFilterGroup.Location = new System.Drawing.Point(12, 122);
            this.PerfectIVFilterGroup.Name = "PerfectIVFilterGroup";
            this.PerfectIVFilterGroup.Size = new System.Drawing.Size(213, 57);
            this.PerfectIVFilterGroup.TabIndex = 10;
            this.PerfectIVFilterGroup.TabStop = false;
            this.PerfectIVFilterGroup.Text = "Perfect IV Filters";
            // 
            // HP
            // 
            this.HP.AutoSize = true;
            this.HP.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.HP.Location = new System.Drawing.Point(3, 19);
            this.HP.Name = "HP";
            this.HP.Size = new System.Drawing.Size(27, 33);
            this.HP.TabIndex = 0;
            this.HP.Text = "HP";
            this.HP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.HP.UseVisualStyleBackColor = true;
            // 
            // Atk
            // 
            this.Atk.AutoSize = true;
            this.Atk.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Atk.Location = new System.Drawing.Point(36, 19);
            this.Atk.Name = "Atk";
            this.Atk.Size = new System.Drawing.Size(29, 33);
            this.Atk.TabIndex = 1;
            this.Atk.Text = "Atk";
            this.Atk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Atk.UseVisualStyleBackColor = true;
            // 
            // SpA
            // 
            this.SpA.AutoSize = true;
            this.SpA.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.SpA.Location = new System.Drawing.Point(106, 19);
            this.SpA.Name = "SpA";
            this.SpA.Size = new System.Drawing.Size(32, 33);
            this.SpA.TabIndex = 3;
            this.SpA.Text = "SpA";
            this.SpA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SpA.UseVisualStyleBackColor = true;
            // 
            // Def
            // 
            this.Def.AutoSize = true;
            this.Def.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Def.Location = new System.Drawing.Point(71, 19);
            this.Def.Name = "Def";
            this.Def.Size = new System.Drawing.Size(29, 33);
            this.Def.TabIndex = 2;
            this.Def.Text = "Def";
            this.Def.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Def.UseVisualStyleBackColor = true;
            // 
            // SpD
            // 
            this.SpD.AutoSize = true;
            this.SpD.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.SpD.Location = new System.Drawing.Point(144, 19);
            this.SpD.Name = "SpD";
            this.SpD.Size = new System.Drawing.Size(32, 33);
            this.SpD.TabIndex = 4;
            this.SpD.Text = "SpD";
            this.SpD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SpD.UseVisualStyleBackColor = true;
            // 
            // Spe
            // 
            this.Spe.AutoSize = true;
            this.Spe.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Spe.Location = new System.Drawing.Point(182, 19);
            this.Spe.Name = "Spe";
            this.Spe.Size = new System.Drawing.Size(30, 33);
            this.Spe.TabIndex = 5;
            this.Spe.Text = "Spe";
            this.Spe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Spe.UseVisualStyleBackColor = true;
            // 
            // FilterSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 220);
            this.Controls.Add(this.PerfectIVFilterGroup);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.ShinyCheck);
            this.Controls.Add(this.StarCheck);
            this.Controls.Add(this.Stars);
            this.Controls.Add(this.NatureCheck);
            this.Controls.Add(this.Nature);
            this.Controls.Add(this.SpeciesCheck);
            this.Controls.Add(this.Species);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FilterSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FilterSettings";
            this.PerfectIVFilterGroup.ResumeLayout(false);
            this.PerfectIVFilterGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox Species;
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
    }
}