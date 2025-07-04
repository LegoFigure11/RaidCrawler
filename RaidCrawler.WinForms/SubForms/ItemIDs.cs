namespace RaidCrawler.WinForms.SubForms;

public partial class ItemIDs : Form
{
    public ItemIDs(List<int> IDs)
    {
        InitializeComponent();

        foreach (int ID in IDs)
        {
            switch (ID)
            {
                case 645:
                    CheckAbilityCapsule.Checked = true;
                    break;
                case 795:
                    CheckBottleCap.Checked = true;
                    break;
                case 1606:
                    CheckAbilityPatch.Checked = true;
                    break;
                case 1904:
                    CheckSweet.Checked = true;
                    break;
                case 1905:
                    CheckSalty.Checked = true;
                    break;
                case 1906:
                    CheckSour.Checked = true;
                    break;
                case 1907:
                    CheckBitter.Checked = true;
                    break;
                case 1908:
                    CheckSpicy.Checked = true;
                    break;
            }
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
