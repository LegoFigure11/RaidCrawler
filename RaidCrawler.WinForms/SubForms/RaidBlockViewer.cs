namespace RaidCrawler.WinForms.SubForms;

public partial class RaidBlockViewer : Form
{
    public RaidBlockViewer(byte[] data, ulong offset)
    {
        InitializeComponent();
        AbsoluteAddress.Text = $"{offset:X8}";
        RAM.Text = string.Join(" ", data.Select(bytes => $"{bytes:X2}"));
    }
}
