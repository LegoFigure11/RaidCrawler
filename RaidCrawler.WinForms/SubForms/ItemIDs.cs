namespace RaidCrawler.WinForms.SubForms
{
    public partial class ItemIDs : Form
    {
        public ItemIDs(List<int> IDs)
        {
            InitializeComponent();

            foreach (int ID in IDs)
            {
                if (ID == 645)
                    CheckAbilityCapsule.Checked = true;
                else if (ID == 795)
                    CheckBottleCap.Checked = true;
                else if (ID == 1606)
                    CheckAbilityPatch.Checked = true;
                else if (ID == 1904)
                    CheckSweet.Checked = true;
                else if (ID == 1905)
                    CheckSalty.Checked = true;
                else if (ID == 1906)
                    CheckSour.Checked = true;
                else if (ID == 1907)
                    CheckBitter.Checked = true;
                else if (ID == 1908)
                    CheckSpicy.Checked = true;
            }
            PicCapsule.Image = (Image?)
                PKHeX.Drawing.PokeSprite.Properties.Resources.ResourceManager.GetObject(
                    "aitem_645"
                );
            PicCap.Image = (Image?)
                PKHeX.Drawing.PokeSprite.Properties.Resources.ResourceManager.GetObject(
                    "aitem_795"
                );
            PicPatch.Image = (Image?)
                PKHeX.Drawing.PokeSprite.Properties.Resources.ResourceManager.GetObject(
                    "aitem_1606"
                );
        }
    }
}
