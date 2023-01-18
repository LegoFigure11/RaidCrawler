using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using RaidCrawler.Structures;

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

        private DataTable EmojiLoad(Dictionary<string, string> emoji)
        {
            DataTable d = new DataTable();
            d.Columns.Add("Emoji", typeof(string));
            d.Columns.Add("Emoji Value", typeof(string));
            emoji.ToList().ForEach(KeyValuePair => d.Rows.Add(new object[] { KeyValuePair.Key, KeyValuePair.Value }));
            d.Columns[0].ReadOnly = true;
            return d;
        }

        private Dictionary<string, string> EmojiSave(DataTable emoji)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            emoji.AsEnumerable().ToList().ForEach(row => d.Add(row[0] as string, row[1] as string));
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
