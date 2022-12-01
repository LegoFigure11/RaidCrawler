using PKHeX.Core;
using RaidCrawler.Structures;

namespace RaidCrawler.Subforms
{
    public partial class FilterSettings : Form
    {
        private static Color Highlight = Color.YellowGreen;
        private static Color DefaultColor;
        public FilterSettings()
        {
            InitializeComponent();
            var settings = Properties.Settings.Default;
            Species.DataSource = Enum.GetValues(typeof(Species)).Cast<Species>().Where(z => z != PKHeX.Core.Species.MAX_COUNT).ToArray();
            Nature.DataSource = Enum.GetValues(typeof(Nature));
            TeraType.DataSource = Enum.GetValues(typeof(MoveType)).Cast<MoveType>().Where(z => z != MoveType.Any).ToArray();
            Species.SelectedIndex = settings.SpeciesFilter;
            Nature.SelectedIndex = settings.NatureFilter;
            Stars.SelectedIndex = settings.StarsFilter;
            TeraType.SelectedIndex = settings.TeraFilter;
            SpeciesCheck.Checked = settings.SpeciesEnabled;
            NatureCheck.Checked = settings.NatureEnabled;
            StarCheck.Checked = settings.StarsEnabled;
            TeraCheck.Checked = settings.TeraEnabled;
            ShinyCheck.Checked = settings.SearchTillShiny;
            SatisfyAny.Checked = settings.SatisfyAny;
            SpeciesFixed.Checked = settings.SpeciesFixed;
            NatureFixed.Checked = settings.NatureFixed;
            StarFixed.Checked = settings.StarFixed;
            TeraFixed.Checked = settings.TeraFixed;

            // highlight fixed components
            Species.BackColor = settings.SpeciesFixed ? Highlight : DefaultColor;
            Nature.BackColor = settings.NatureFixed ? Highlight : DefaultColor;
            Stars.BackColor = settings.StarFixed ? Highlight : DefaultColor;
            TeraType.BackColor = settings.TeraFixed ? Highlight : DefaultColor;

            var ivbin = settings.IVBin;
            HP.Checked = (ivbin & 1) == 1;
            Atk.Checked = ((ivbin >> 1) & 1) == 1;
            Def.Checked = ((ivbin >> 2) & 1) == 1;
            SpA.Checked = ((ivbin >> 3) & 1) == 1;
            SpD.Checked = ((ivbin >> 4) & 1) == 1;
            Spe.Checked = ((ivbin >> 5) & 1) == 1;

            var ivvals = settings.IVVals;
            IVHP.Value = ivvals & 31;
            IVATK.Value = (ivvals >> 5) & 31;
            IVDEF.Value = (ivvals >> 10) & 31;
            IVSPA.Value = (ivvals >> 15) & 31;
            IVSPD.Value = (ivvals >> 20) & 31;
            IVSPE.Value = (ivvals >> 25) & 31;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            var ivbin = ToInt(HP.Checked) << 0 | ToInt(Atk.Checked) << 1 | ToInt(Def.Checked) << 2 |
                        ToInt(SpA.Checked) << 3 | ToInt(SpD.Checked) << 4 | ToInt(Spe.Checked) << 5;
            var ivvals = (int)IVHP.Value << 0 | (int)IVATK.Value << 5 | (int)IVDEF.Value << 10 |
                         (int)IVSPA.Value << 15 | (int)IVSPD.Value << 20 | (int)IVSPE.Value << 25;

            Properties.Settings.Default.SpeciesFilter = Species.SelectedIndex;
            Properties.Settings.Default.NatureFilter = Nature.SelectedIndex;
            Properties.Settings.Default.StarsFilter = Stars.SelectedIndex;
            Properties.Settings.Default.TeraFilter = TeraType.SelectedIndex;
            Properties.Settings.Default.SpeciesEnabled = SpeciesCheck.Checked;
            Properties.Settings.Default.NatureEnabled = NatureCheck.Checked;
            Properties.Settings.Default.StarsEnabled = StarCheck.Checked;
            Properties.Settings.Default.TeraEnabled = TeraCheck.Checked;
            Properties.Settings.Default.SearchTillShiny = ShinyCheck.Checked;
            Properties.Settings.Default.SatisfyAny = SatisfyAny.Checked;
            Properties.Settings.Default.SpeciesFixed = SpeciesFixed.Checked;
            Properties.Settings.Default.NatureFixed = NatureFixed.Checked;
            Properties.Settings.Default.StarFixed = StarFixed.Checked;
            Properties.Settings.Default.TeraFixed = TeraFixed.Checked;
            Properties.Settings.Default.IVBin = ivbin;
            Properties.Settings.Default.IVVals = ivvals;
            Properties.Settings.Default.Save();

            RaidFilters.Species = SpeciesCheck.Checked ? (Species)Species.SelectedIndex : null;
            RaidFilters.Nature = NatureCheck.Checked ? (Nature)Nature.SelectedIndex : null;
            RaidFilters.Stars = StarCheck.Checked ? Stars.SelectedIndex + 1 : null;
            RaidFilters.TeraType = TeraCheck.Checked ? (MoveType)TeraType.SelectedIndex : null;
            RaidFilters.Shiny = ShinyCheck.Checked;
            RaidFilters.SatisfyAny = SatisfyAny.Checked;
            RaidFilters.SpeciesFixed = SpeciesFixed.Checked;
            RaidFilters.NatureFixed = NatureFixed.Checked;
            RaidFilters.StarFixed = StarFixed.Checked;
            RaidFilters.TeraFixed = TeraFixed.Checked;
            RaidFilters.IVBin = ivbin;
            RaidFilters.IVVals = ivvals;

            this.Close();
        }

        private static int ToInt(bool b) => b ? 1 : 0;

        private void SpeciesFixed_CheckedChanged(object sender, EventArgs e)
        {
            Species.BackColor = SpeciesFixed.Checked ? Highlight : DefaultColor;
        }

        private void NatureFixed_CheckedChanged(object sender, EventArgs e)
        {
            Nature.BackColor = NatureFixed.Checked ? Highlight : DefaultColor;
        }

        private void StarFixed_CheckedChanged(object sender, EventArgs e)
        {
            Stars.BackColor = StarFixed.Checked ? Highlight : DefaultColor;
        }

        private void TeraFixed_CheckedChanged(object sender, EventArgs e)
        {
            TeraType.BackColor = TeraFixed.Checked ? Highlight : DefaultColor;
        }
    }
}
