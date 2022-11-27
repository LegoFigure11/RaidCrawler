using RaidCrawler.Structures;
using PKHeX.Core;

namespace RaidCrawler.Subforms
{
    public partial class FilterSettings : Form
    {
        public FilterSettings()
        {
            InitializeComponent();
            Species.DataSource = Enum.GetValues(typeof(Species));
            Nature.DataSource = Enum.GetValues(typeof(Nature));
            Species.SelectedIndex = Properties.Settings.Default.SpeciesFilter;
            Nature.SelectedIndex = Properties.Settings.Default.NatureFilter;
            Stars.SelectedIndex = Properties.Settings.Default.StarsFilter;
            SpeciesCheck.Checked = Properties.Settings.Default.SpeciesEnabled;
            NatureCheck.Checked = Properties.Settings.Default.NatureEnabled;
            StarCheck.Checked = Properties.Settings.Default.StarsEnabled;
            ShinyCheck.Checked = Properties.Settings.Default.SearchTillShiny;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SpeciesFilter = Species.SelectedIndex;
            Properties.Settings.Default.NatureFilter = Nature.SelectedIndex;
            Properties.Settings.Default.StarsFilter = Stars.SelectedIndex;
            Properties.Settings.Default.SpeciesEnabled = SpeciesCheck.Checked;
            Properties.Settings.Default.NatureEnabled = NatureCheck.Checked;
            Properties.Settings.Default.StarsEnabled = StarCheck.Checked;
            Properties.Settings.Default.SearchTillShiny = ShinyCheck.Checked;
            Properties.Settings.Default.Save();

            RaidFilters.Species = SpeciesCheck.Checked ? (Species)Species.SelectedIndex : null;
            RaidFilters.Nature = NatureCheck.Checked ? (Nature)Nature.SelectedIndex : null;
            RaidFilters.Stars = StarCheck.Checked ? Stars.SelectedIndex + 1 : null;
            RaidFilters.Shiny = ShinyCheck.Checked;

            this.Close();
        }
    }
}
