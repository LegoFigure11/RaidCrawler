namespace RaidCrawler.Structures
{
    public class Config
    {
        // General
        public string SwitchIP { get; set; } = "192.168.0.0";
        public string Game { get; set; } = "Scarlet";
        public int Progress { get; set; } = 0;
        public int EventProgress { get; set; } = 0;
        public Point Location { get; set; } = new(0, 0);

        // Match
        public bool FocusWindow { get; set; } = true;
        public bool PlaySound { get; set; } = true;
        public bool EnableAlertWindow { get; set; } = true;
        public string AlertWindowMessage { get; set; } = "Match found! Hold Shift and click one of the arrow keys to jump to the matching result.";
        public bool EnableNotification { get; set; } = false;
        public string DiscordWebhook { get; set; } = string.Empty;
        public string DiscordMessageContent { get; set; } = string.Empty;

        // Date Advance
        public bool UseTouch { get; set; } = true;
        public bool UseOvershoot { get; set; } = true;

        public decimal BaseDelay { get; set; } = 0;
        public decimal OpenHome { get; set; } = 1800;
        public decimal NavigateToSettings { get; set; } = 100;
        public decimal OpenSettings { get; set; } = 1000;
        public decimal Hold { get; set; } = 1700;
        public decimal SystemDDownPresses { get; set; } = 38;
        public decimal Submenu { get; set; } = 2200;
        public decimal DateChange { get; set; } = 500;
        public decimal DaysToSkip { get; set; } = 0;
        public decimal ReturnHome { get; set; } = 2500;
        public decimal ReturnGame { get; set; } = 4000;
        public decimal SystemOvershoot { get; set; } = 950;

        // Webhook
        public bool EnableEmoji { get; set; } = true;
        public bool VerboseIVs { get; set; } = false;
        public int IVsStyle { get; set; } = 0;
        public string IVsSpacer { get; set; } = " ";
        public bool ToggleDen { get; set; } = true;


        // Experimental
        public bool StreamerView { get; set; } = false;
        public string InstanceName { get; set; } = string.Empty;

    }
}
