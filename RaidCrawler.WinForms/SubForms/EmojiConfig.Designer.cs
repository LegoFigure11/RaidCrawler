namespace RaidCrawler.WinForms.SubForms
{
    partial class EmojiConfig
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
            this.EmojiGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.EmojiGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // EmojiGrid
            // 
            this.EmojiGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.EmojiGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EmojiGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EmojiGrid.Location = new System.Drawing.Point(0, 0);
            this.EmojiGrid.Name = "EmojiGrid";
            this.EmojiGrid.RowTemplate.Height = 25;
            this.EmojiGrid.Size = new System.Drawing.Size(374, 450);
            this.EmojiGrid.TabIndex = 0;
            this.EmojiGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.EmojiGrid_Changed);
            // 
            // EmojiConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 450);
            this.Controls.Add(this.EmojiGrid);
            this.Name = "EmojiConfig";
            this.ShowIcon = false;
            this.Text = "EmojiConfig";
            ((System.ComponentModel.ISupportInitialize)(this.EmojiGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView EmojiGrid;
    }
}