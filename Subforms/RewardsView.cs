using RaidCrawler.Structures;

namespace RaidCrawler.Subforms
{
    public partial class RewardsView : Form
    {
        public RewardsView(List<(int, int, int)> rewards)
        {
            InitializeComponent();
            PictureBox[] pictures = new PictureBox[rewards.Count];
            Label[] labels = new Label[rewards.Count];
            for (int i = 0; i < rewards.Count; i++)
            {
                pictures[i] = new PictureBox();
                pictures[i].Size = new Size(20, 20);
                pictures[i].Location = new Point(12, 12 + i * (pictures[i].Size.Height + 10));
                pictures[i].SizeMode = PictureBoxSizeMode.StretchImage;
                labels[i] = new Label();
                var item = rewards[i].Item1 switch
                {
                    10000 => "Material",
                    20000 => "Tera Shard",
                    _ => Raid.strings.Item[rewards[i].Item1]
                };
                var subject = rewards[i].Item3 switch
                {
                    1 => "(Host)",
                    2 => "(Client)",
                    3 => "(Once)",
                    _ => string.Empty
                };
                pictures[i].Image = rewards[i].Item1 switch
                {
                    10000 => (Image?)Properties.Resources.ResourceManager.GetObject("material"),
                    20000 => (Image?)PKHeX.Drawing.PokeSprite.Properties.Resources.ResourceManager.GetObject("aitem_1862"),
                    _ => (Image?)PKHeX.Drawing.PokeSprite.Properties.Resources.ResourceManager.GetObject($"bitem_{rewards[i].Item1}")
                };
                labels[i].Text = $"{item} x{rewards[i].Item2} {subject}".TrimEnd();
                labels[i].Location = new Point(60, 12 + i * (pictures[i].Size.Height + 10));
                Controls.Add(pictures[i]);
                Controls.Add(labels[i]);
            }
            ClientSize = new Size(ClientSize.Width, 12 + rewards.Count * (pictures[0].Size.Height + 10));
        }
    }
}
