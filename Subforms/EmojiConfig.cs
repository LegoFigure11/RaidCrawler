using Newtonsoft.Json;
using RaidCrawler.Structures;
using System.Data;

namespace RaidCrawler.Subforms
{
    public partial class EmojiConfig : Form
    {
        private readonly Config c = new();
        public EmojiConfig(Config c)
        {
            InitializeComponent();

            this.c = c;

            dataGridView1.DataSource = EmojiLoad(c.Emoji);
        }

        private static DataTable EmojiLoad(Dictionary<string, string> emoji)
        {
            DataTable d = new();
            d.Columns.Add("Emoji", typeof(string));
            d.Columns.Add("Emoji Value", typeof(string));
            emoji.ToList().ForEach(KeyValuePair => d.Rows.Add(new object[] { KeyValuePair.Key, KeyValuePair.Value }));
            d.Columns[0].ReadOnly = true;
            return d;
        }

        private static Dictionary<string, string> EmojiSave(DataTable emoji)
        {
            Dictionary<string, string> d = new();
            emoji.AsEnumerable().ToList().ForEach(row => d.Add((string)row[0], (string)row[1]));
            return d;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            c.Emoji = EmojiSave((DataTable)dataGridView1.DataSource);

            string output = JsonConvert.SerializeObject(c);
            using StreamWriter sw = new(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json"));
            sw.Write(output);

            Close();
        }
    }
}
