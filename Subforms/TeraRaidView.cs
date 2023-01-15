using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RaidCrawler.Properties;

namespace RaidCrawler.Subforms
{
    public partial class TeraRaidView : Form
    {
        // Drag and Drop
        bool drag = false;
        Point start = new Point(0, 0);

        // Progress Bar
        double pbUnit;
        int pbWidth, pbHeight, pbComplete;
        Bitmap bmp;
        Graphics g;

        public TeraRaidView()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
        }

        public void startProgress()
        {
            pbWidth = pictureBox1.Width;
            pbHeight = pictureBox1.Height;
            pbUnit = pbWidth / 100;

            pbComplete = 100;
            bmp = new Bitmap(pbWidth, pbHeight);

            decimal delays;
            delays = Settings.Default.CfgBaseDelay * 20 + Settings.Default.CfgOpenHome + Settings.Default.CfgNavigateToSettings +
                Settings.Default.CfgOpenSettings + Settings.Default.CfgHold + Settings.Default.CfgSubmenu + Settings.Default.CfgDateChange +
                Settings.Default.CfgReturnHome + Settings.Default.CfgReturnGame + 4250; // fudge time to read raids

            timer1.Interval = (int)(delays/100);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            g = Graphics.FromImage(bmp);
            g.Clear(Color.LightSkyBlue);

            //draw progressbar
            g.FillRectangle(Brushes.CornflowerBlue, new Rectangle(0, 0, (int)(pbComplete * pbUnit), pbHeight));

            //load bitmap in picturebox picboxPB
            pictureBox1.Image = bmp;

            //update pbComplete
            //Note!
            //To keep things simple I am adding +1 to pbComplete every 50ms
            //You can change this as per your requirement :)
            pbComplete--;
            if (pbComplete < 0)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(0, 5, 25)), new Rectangle(0, 0, pbWidth, pbHeight));
                pictureBox1.Image = bmp;
                g.Dispose();
                timer1.Stop();
            }

        }

        private void TeraRaidView_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start = new Point(e.X, e.Y);
        }

        private void TeraRaidView_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start.X, p.Y - start.Y);
            }
        }

        private void rewards_TextChanged(object sender, EventArgs e)
        {
            int value;
            this.ForeColor = Color.DarkGray;
            // this.BackColor = System.Drawing.Color.FromArgb(0, 5, 25);

            if (Int32.TryParse(this.Text, out value))
            {
                if (value > 0)
                    this.ForeColor = Color.White;
                if (value > 2)
                    this.BackColor = Color.ForestGreen;

            }
        }

        private void TeraRaidView_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }

        private void TeraRaidView_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
