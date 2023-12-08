using PKHeX.Drawing;
using RaidCrawler.Core.Structures;

namespace RaidCrawler.WinForms.SubForms;

public partial class RewardsView : Form
{
    public RewardsView(IReadOnlyList<string> itemStrings, IReadOnlyList<string> moveStrings, IReadOnlyList<(int, int, int)> rewards)
    {
        InitializeComponent();
        var rare = PKHeX.Drawing.PokeSprite.Properties.Resources.rare_icon;
        var pictures = new PictureBox[rewards.Count];
        var labels = new Label[rewards.Count];
        for (int i = 0; i < rewards.Count; i++)
        {
            var pb = pictures[i] = new PictureBox
            {
                Size = new Size(24, 24),
                Location = new Point(12, (i * 36) + 12),
                SizeMode = PictureBoxSizeMode.CenterImage,
            };

            var label = labels[i] = new Label();
            var reward = rewards[i];
            var item = reward.Item1 switch
            {
                10000 => "Material",
                20000 => "Tera Shard",
                _ => Rewards.IsTM(reward.Item1)
                   ? Rewards.GetNameTM(reward.Item1, itemStrings, moveStrings, Rewards.TMIndexes)
                   : itemStrings[reward.Item1],
            };

            var subject = reward.Item3 switch
            {
                1 => "(Host)",
                2 => "(Client)",
                3 => "(Once)",
                _ => string.Empty,
            };

            var img = GetItem(rewards, i);

            if (img != null && Rewards.RareRewards.Contains(reward.Item1))
                img = ImageUtil.LayerImage(img, rare, 0, 0, 0.7);

            pb.Image = img;
            label.Text = $"{item} x{reward.Item2} {subject}".TrimEnd();
            label.Location = new Point(60, 12 + (i * (pb.Size.Height + 12)));
            label.Size = new Size(ClientSize.Width - 60 - 10, label.Height);
            Controls.Add(pb);
            Controls.Add(label);
        }
        ClientSize = ClientSize with { Height = 12 + (rewards.Count * (pictures[0].Size.Height + 12)) };
    }

    private static Image? GetItem(IReadOnlyList<(int, int, int)> rewards, int i)
    {
        var (rc, item) = GetItemResourceName(rewards[i].Item1);
        var manager = rc
            ? Properties.Resources.ResourceManager
            : PKHeX.Drawing.PokeSprite.Properties.Resources.ResourceManager;
        return (Image?)manager.GetObject(item);
    }

    private static (bool rc, string item) GetItemResourceName(int id) => id switch
    {
        // Handling for sprites that pkhex doesn't have
        1904 => (true, "item_1904"),
        1905 => (true, "item_1905"),
        1906 => (true, "item_1906"),
        1907 => (true, "item_1907"),
        1908 => (true, "item_1908"),
        (>= 1956 and <= 2159) or (>= 2438 and <= 2478) => (true, "material"),
        10000 => (true, "material"),

        // pkhex can give us the sprites
        20000 => (false, "aitem_1862"),
        _ => (false, Rewards.IsTM(id) ? "aitem_tm" : $"aitem_{id}"),
    };
}
