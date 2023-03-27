namespace RaidCrawler.WinForms.SubForms
{
    public partial class TeraRaidView : Form
    {
        // Drag and Drop
        private bool drag = false;
        private Point start = new(0, 0);

        // Progress Bar
        private double pbUnit;
        private int pbWidth, pbHeight, pbComplete;
        private Bitmap? bmp;
        private Graphics? g;
        private readonly ClientConfig config;

        public TeraRaidView(ClientConfig c)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            Location = new Point(0, 0);
            config = c;
        }

        public void StartProgress()
        {
            pbWidth = pictureBox1.Width;
            pbHeight = pictureBox1.Height;
            pbUnit = pbWidth / 100;

            pbComplete = 100;
            bmp = new Bitmap(pbWidth, pbHeight);

            decimal delays;
            delays = config.BaseDelay * 20 + config.OpenHomeDelay + config.NavigateToSettingsDelay +
                config.OpenSettingsDelay + config.HoldDuration + config.Submenu + config.DateChange +
                config.ReturnHomeDelay + config.ReturnGameDelay + 4250; // fudge time to read raids

            timer1.Interval = (int)(delays / 100);
            timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            g = Graphics.FromImage(bmp!);
            g.Clear(Color.LightSkyBlue);

            //draw progressbar
            g.FillRectangle(Brushes.CornflowerBlue, new Rectangle(0, 0, (int)(pbComplete * pbUnit), pbHeight));

            //load bitmap in picturebox picboxPB
            pictureBox1.Image = bmp;

            //update pbComplete
            //Note!
            //To keep things simple I am adding +1 to pbComplete every 50ms
            //You can change this as per your requirement :)
            if (pbComplete < 0)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(0, 5, 25)), new Rectangle(0, 0, pbWidth, pbHeight));
                pictureBox1.Image = bmp;
                g.Dispose();
                timer1.Stop();
            }
            pbComplete--;
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
