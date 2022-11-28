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

            var ivbin = Properties.Settings.Default.IVBin;
            HP.Checked = (ivbin & 1) == 1;
            Atk.Checked = ((ivbin >> 1) & 1) == 1;
            Def.Checked = ((ivbin >> 2) & 1) == 1;
            SpA.Checked = ((ivbin >> 3) & 1) == 1;
            SpD.Checked = ((ivbin >> 4) & 1) == 1;
            Spe.Checked = ((ivbin >> 5) & 1) == 1;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            var ivbin = ToInt(HP.Checked) << 0 | ToInt(Atk.Checked) << 1 | ToInt(Def.Checked) << 2 |
                        ToInt(SpA.Checked) << 3 | ToInt(SpD.Checked) << 4 | ToInt(Spe.Checked) << 5;

            Properties.Settings.Default.SpeciesFilter = Species.SelectedIndex;
            Properties.Settings.Default.NatureFilter = Nature.SelectedIndex;
            Properties.Settings.Default.StarsFilter = Stars.SelectedIndex;
            Properties.Settings.Default.SpeciesEnabled = SpeciesCheck.Checked;
            Properties.Settings.Default.NatureEnabled = NatureCheck.Checked;
            Properties.Settings.Default.StarsEnabled = StarCheck.Checked;
            Properties.Settings.Default.SearchTillShiny = ShinyCheck.Checked;
            Properties.Settings.Default.IVBin = ivbin;
            Properties.Settings.Default.Save();

            RaidFilters.Species = SpeciesCheck.Checked ? (Species)Species.SelectedIndex : null;
            RaidFilters.Nature = NatureCheck.Checked ? (Nature)Nature.SelectedIndex : null;
            RaidFilters.Stars = StarCheck.Checked ? Stars.SelectedIndex + 1 : null;
            RaidFilters.Shiny = ShinyCheck.Checked;
            RaidFilters.IVBin = ivbin;

            this.Close();
        }

        private static int ToInt(bool b) => b ? 1 : 0;
    }
}
