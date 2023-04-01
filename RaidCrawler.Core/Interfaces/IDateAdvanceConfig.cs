﻿namespace RaidCrawler.Core.Interfaces
{
    public interface IDateAdvanceConfig
    {
        bool UseTouch { get; set; }
        bool UseOvershoot { get; set; }
        int OpenHomeDelay { get; set; }
        int NavigateToSettingsDelay { get; set; }
        int OpenSettingsDelay { get; set; }
        int HoldDuration { get; set; }
        int SystemDownPresses { get; set; }
        int SystemOvershoot { get; set; }
        int Submenu { get; set; }
        int DateChange { get; set; }
        int DaysToSkip { get; set; }
        int ReturnHomeDelay { get; set; }
        int ReturnGameDelay { get; set; }
    }
}
