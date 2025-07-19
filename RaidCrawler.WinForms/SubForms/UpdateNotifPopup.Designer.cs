namespace RaidCrawler.WinForms.SubForms
{
    partial class UpdateNotifPopup
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
            B_Close = new Button();
            B_Download = new Button();
            label1 = new Label();
            L_Version = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // B_Close
            // 
            B_Close.DialogResult = DialogResult.Cancel;
            B_Close.Location = new Point(12, 61);
            B_Close.Name = "B_Close";
            B_Close.Size = new Size(75, 25);
            B_Close.TabIndex = 1;
            B_Close.Text = "Ignore";
            B_Close.UseVisualStyleBackColor = true;
            // 
            // B_Download
            // 
            B_Download.DialogResult = DialogResult.OK;
            B_Download.Location = new Point(93, 61);
            B_Download.Name = "B_Download";
            B_Download.Size = new Size(150, 25);
            B_Download.TabIndex = 0;
            B_Download.Text = "Open download page";
            B_Download.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(123, 15);
            label1.TabIndex = 2;
            label1.Text = "New update available!";
            // 
            // L_Version
            // 
            L_Version.AutoSize = true;
            L_Version.Location = new Point(12, 26);
            L_Version.Name = "L_Version";
            L_Version.Size = new Size(149, 15);
            L_Version.TabIndex = 3;
            L_Version.Text = "Current: v1.1.1 | New v9.9.9";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 43);
            label3.Name = "label3";
            label3.Size = new Size(228, 15);
            label3.TabIndex = 4;
            label3.Text = "It is advised to update as soon as possible!";
            // 
            // UpdateNotifPopup
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(255, 93);
            Controls.Add(label3);
            Controls.Add(L_Version);
            Controls.Add(label1);
            Controls.Add(B_Download);
            Controls.Add(B_Close);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "UpdateNotifPopup";
            Text = "Update available!";
            Load += UpdateNotifPopup_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button B_Close;
        private Button B_Download;
        private Label label1;
        private Label L_Version;
        private Label label3;
    }
}
