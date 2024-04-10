namespace RaidCrawler.WinForms.SubForms
{
    partial class TickModifier
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
            TB_Tick = new TextBox();
            L_Tick = new Label();
            B_Read = new Button();
            B_Write = new Button();
            B_NTP = new Button();
            SuspendLayout();
            // 
            // TB_Tick
            // 
            TB_Tick.Font = new Font("Consolas", 9F);
            TB_Tick.Location = new Point(50, 7);
            TB_Tick.Margin = new Padding(4, 3, 4, 3);
            TB_Tick.Name = "TB_Tick";
            TB_Tick.Size = new Size(162, 22);
            TB_Tick.TabIndex = 45;
            // 
            // L_Tick
            // 
            L_Tick.AutoSize = true;
            L_Tick.Location = new Point(12, 9);
            L_Tick.Name = "L_Tick";
            L_Tick.Size = new Size(31, 15);
            L_Tick.TabIndex = 44;
            L_Tick.Text = "Tick:";
            L_Tick.TextAlign = ContentAlignment.MiddleRight;
            // 
            // B_Read
            // 
            B_Read.Location = new Point(12, 35);
            B_Read.Name = "B_Read";
            B_Read.Size = new Size(96, 27);
            B_Read.TabIndex = 130;
            B_Read.Text = "Read";
            B_Read.UseVisualStyleBackColor = true;
            B_Read.Click += B_Read_Click;
            // 
            // B_Write
            // 
            B_Write.Location = new Point(116, 35);
            B_Write.Name = "B_Write";
            B_Write.Size = new Size(96, 27);
            B_Write.TabIndex = 131;
            B_Write.Text = "Write";
            B_Write.UseVisualStyleBackColor = true;
            B_Write.Click += B_Write_Click;
            // 
            // B_NTP
            // 
            B_NTP.Location = new Point(12, 65);
            B_NTP.Name = "B_NTP";
            B_NTP.Size = new Size(200, 27);
            B_NTP.TabIndex = 132;
            B_NTP.Text = "Reset Time (NTP)";
            B_NTP.UseVisualStyleBackColor = true;
            B_NTP.Click += B_NTP_Click;
            // 
            // TickModifier
            // 
            AcceptButton = B_Write;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(224, 96);
            Controls.Add(B_NTP);
            Controls.Add(B_Write);
            Controls.Add(B_Read);
            Controls.Add(TB_Tick);
            Controls.Add(L_Tick);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "TickModifier";
            ShowIcon = false;
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "TickModifier";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox TB_Tick;
        private Label L_Tick;
        private Button B_Read;
        private Button B_Write;
        private Button B_NTP;
    }
}