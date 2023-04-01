using SysBot.Base;

namespace RaidCrawler.Core.Interfaces
{
    public interface IWebhookConfig
    {
        SwitchProtocol Protocol { get; set; }
        bool EnableNotification { get; set; }
        bool ToggleDen { get; set; }
        string InstanceName { get; set; }
        string DiscordWebhook { get; set; }
        string DiscordMessageContent { get; set; }

        bool EnableEmoji { get; set; }
        Dictionary<string, string> Emoji { get; set; }

        bool VerboseIVs { get; set; }
        int IVsStyle { get; set; }
    }
}
