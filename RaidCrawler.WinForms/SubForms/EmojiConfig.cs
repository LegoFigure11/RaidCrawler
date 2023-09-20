using System.Data;

namespace RaidCrawler.WinForms.SubForms
{
    public partial class EmojiConfig : Form
    {
        private readonly ClientConfig c = new();

        public EmojiConfig(ClientConfig c)
        {
            InitializeComponent();
            this.c = c;
            EmojiGrid.DataSource = EmojiLoad(c.Emoji);
        }

        private static DataTable EmojiLoad(Dictionary<string, string> emoji)
        {
            DataTable dt = new();
            dt.Columns.Add("Emoji", typeof(string));
            dt.Columns.Add("Emoji Value", typeof(string));
            emoji
                .ToList()
                .ForEach(
                    KeyValuePair =>
                        dt.Rows.Add(new object[] { KeyValuePair.Key, KeyValuePair.Value })
                );
            dt.Columns[0].ReadOnly = true;
            return dt;
        }

        private void EmojiGrid_Changed(object sender, EventArgs e)
        {
            var dict = new Dictionary<string, string>();
            var dt = (DataTable)EmojiGrid.DataSource;
            dt.AsEnumerable().ToList().ForEach(row => dict.Add((string)row[0], (string)row[1]));
            c.Emoji = dict;
        }
    }
}
