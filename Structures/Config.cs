﻿namespace RaidCrawler.Structures
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
		public Dictionary<string, string> Emoji { get; set; } = new Dictionary<string, string>
        {
            { "Bug", "<:bug:1064546304048496812>" }, { "Dark", "<:dark:1064557656079085588>" }, { "Dragon", "<:dragon:1064557631890538566>"}, { "Electric", "<:electric:1064557559563943956>"},
            { "Fairy", "<:fairy:1064557682566123701>"}, { "Fighting", "<:fighting:1064546289406189648>"}, { "Fire", "<:fire:1064557482468446230>"}, { "Flying", "<:flying:1064546291239104623>"},
            { "Ghost", "<:ghost:1064546307848536115>"}, { "Grass", "<:grass:1064557534096130099>"}, { "Ground", "<:ground:1064546296725241988>"}, { "Ice", "<:ice:1064557609857863770>"},
            { "Normal", "<:normal:1064546286247886938>"}, { "Poison", "<:poison:1064546294854586400>"}, { "Psychic", "<:psychic:1064557585124049006>"}, { "Rock", "<:rock:1064546299992625242>"},
            { "Steel", "<:steel:1064557443742453790>"}, { "Water", "<:water:1064557509404270642>"}, { "Male", "<:male:1064844611341795398>"}, { "Female", "<:female:1064844510636552212>"},
            { "Shiny", "<:shiny:1064845915036323840>"}, { "Square Shiny", ":white_square_button:"}, { "Event Star", "<:bluestar:1064538604409471016>"}, { "7 Star", "<:pinkstar:1064538642934140978>"},
            { "Star", "<:yellowstar:1064538672113922109>"}, { "Health 0", "<:h0:1064842950573572126>"}, { "Health 31", "<:h31:1064726680628895784>"}, { "Attack 0", "<:a0:1064842895712075796>"},
            { "Attack 31", "<:a31:1064726668419289138>"}, { "Defense 0", "<:b0:1064842811196833832>"}, { "Defense 31", "<:b31:1064726671703429220>"}, { "SpAttack 0", "<:c0:1064842749272133752>"},
            { "SpAttack 31", "<:c31:1064726673649582121>"}, { "SpDefense 0", "<:d0:1064842668624068608>"}, { "SpDefense 31", "<:d31:1064726677176987832>"}, { "Speed 0", "<:s0:1064842545953243176>" },
            { "Speed 31", "<:s31:1064726682721865818>" }, { "Sweet Herba", "<:sweetherba:1064541764163227759>"}, { "Sour Herba", "<:sourherba:1064541770148483073>"}, { "Salty Herba", "<:saltyherba:1064541768147796038>"},
            { "Bitter Herba", "<:bitterherba:1064541773763977256>"}, { "Spicy Herba", "<:spicyherba:1064541776699994132>"}, { "Bottle Cap", "<:bottlecap:1064537470370320495>"},
            { "Ability Capsule", "<:abilitycapsule:1064541406921752737>"}, { "Ability Patch", "<:abilitypatch:1064538087763476522>"}
        };


        // Experimental
        public bool StreamerView { get; set; } = false;
        public string InstanceName { get; set; } = string.Empty;

    }
}
