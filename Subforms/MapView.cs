namespace RaidCrawler.Subforms
{
    public partial class MapView : Form
    {
        public MapView(Image map)
        {
            InitializeComponent();
            Map.Image = map;
        }
    }
}
