namespace RaidCrawler.WinForms.SubForms
{
    public partial class TeraRaidView : Form
    {
        // Drag and Drop
        private bool drag = false;
        private Point start = new(0, 0);

        // Progress Bar
        private readonly int pbWidth, pbHeight;
        private readonly Bitmap bmp;
        private float pbComplete;
        private float pbUnit;
        private Graphics? g;

        public TeraRaidView()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            Location = new Point(0, 0);
            pbWidth = pictureBox1.Width;
            pbHeight = pictureBox1.Height;
            pbComplete = -1;
            bmp = new Bitmap(pbWidth, pbHeight);
        }

        public void UpdateProgressBar(int steps)
        {
            if (pbComplete <= 0)
            {
                pbComplete = pbWidth;
                pbUnit = (float)pbWidth / steps;
            }

            g = Graphics.FromImage(bmp);
            g.Clear(Color.LightSkyBlue);

            //draw progressbar
            pbComplete -= pbUnit;
            Math.Floor(pbComplete);
            g.FillRegion(Brushes.CornflowerBlue, new Region(new RectangleF(0, 0, pbComplete, pbHeight)));

            //load bitmap in picturebox picboxPB
            pictureBox1.Image = bmp;

            if (pbComplete <= 0)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(0, 5, 25)), new Rectangle(0, 0, pbWidth, pbHeight));
                pictureBox1.Image = bmp;
                g.Dispose();
                pbComplete = -1;
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
                Location = new Point(p.X - start.X, p.Y - start.Y);
            }
        }

        private void Rewards_TextChanged(object sender, EventArgs e)
        {
            ForeColor = Color.DarkGray;
            if (int.TryParse(Text, out int value))
            {
                if (value > 0)
                    ForeColor = Color.White;

                if (value > 2)
                    BackColor = Color.ForestGreen;
            }
        }

        private void TeraRaidView_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }

        private void TeraRaidView_DoubleClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
