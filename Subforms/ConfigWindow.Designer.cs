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
            ((System.ComponentModel.ISupportInitialize)(this.BaseDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SystemDDownPresses)).BeginInit();
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
            this.label2.Size = new System.Drawing.Size(134, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Date Advance Options:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Base delay to be added to all inputs (ms):";
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
            this.SystemDDownPresses.Location = new System.Drawing.Point(241, 192);
            this.SystemDDownPresses.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.SystemDDownPresses.Name = "SystemDDownPresses";
            this.SystemDDownPresses.Size = new System.Drawing.Size(68, 23);
            this.SystemDDownPresses.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 194);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(197, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "DDOWN inputs to navigate settings:";
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(12, 230);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(297, 23);
            this.Save.TabIndex = 12;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // ConfigWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 259);
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
    }
}