namespace RaidCrawler.Subforms
{
    partial class RaidBlockViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RaidBlockViewer));
            this.RAM = new System.Windows.Forms.TextBox();
            this.AbsoluteAddress = new System.Windows.Forms.TextBox();
            this.LabelAbsoluteAddress = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // RAM
            // 
            this.RAM.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RAM.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RAM.Location = new System.Drawing.Point(12, 12);
            this.RAM.Multiline = true;
            this.RAM.Name = "RAM";
            this.RAM.Size = new System.Drawing.Size(342, 342);
            this.RAM.TabIndex = 0;
            this.RAM.Text = "00 11 22 33 44 55 66 77 88 99 AA BB CC DD EE FF";
            // 
            // AbsoluteAddress
            // 
            this.AbsoluteAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AbsoluteAddress.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AbsoluteAddress.Location = new System.Drawing.Point(235, 360);
            this.AbsoluteAddress.Name = "AbsoluteAddress";
            this.AbsoluteAddress.ReadOnly = true;
            this.AbsoluteAddress.Size = new System.Drawing.Size(119, 22);
            this.AbsoluteAddress.TabIndex = 1;
            this.AbsoluteAddress.Text = "0123456789ABCDEF";
            // 
            // LabelAbsoluteAddress
            // 
            this.LabelAbsoluteAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelAbsoluteAddress.AutoSize = true;
            this.LabelAbsoluteAddress.Location = new System.Drawing.Point(127, 362);
            this.LabelAbsoluteAddress.Name = "LabelAbsoluteAddress";
            this.LabelAbsoluteAddress.Size = new System.Drawing.Size(102, 15);
            this.LabelAbsoluteAddress.TabIndex = 2;
            this.LabelAbsoluteAddress.Text = "Absolute Address:";
            // 
            // RaidBlockViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 391);
            this.Controls.Add(this.LabelAbsoluteAddress);
            this.Controls.Add(this.AbsoluteAddress);
            this.Controls.Add(this.RAM);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RaidBlockViewer";
            this.Text = "RaidBlockViewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox RAM;
        private TextBox AbsoluteAddress;
        private Label LabelAbsoluteAddress;
    }
}