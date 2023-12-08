using RaidCrawler.Core.Discord;

namespace RaidCrawler.WinForms;

public static class ErrorHandler
{
    public static async Task DisplayMessageBox(this Form form, NotificationHandler webhook, string msg, CancellationToken token, string caption = "")
    {
        caption = caption.Length == 0 ? "RaidCrawler Error" : caption;
        await webhook.SendErrorNotification(msg, caption, token).ConfigureAwait(false);
        if (form.InvokeRequired)
            form.Invoke(() => MessageBox.Show(msg, caption, MessageBoxButtons.OK));
        else
            MessageBox.Show(msg, caption, MessageBoxButtons.OK);
    }
}
