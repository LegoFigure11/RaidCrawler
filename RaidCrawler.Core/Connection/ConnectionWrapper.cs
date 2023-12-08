using PKHeX.Core;
using RaidCrawler.Core.Interfaces;
using RaidCrawler.Core.Structures;
using SysBot.Base;
using System.Net.Sockets;
using System.Text;
using static SysBot.Base.SwitchButton;

namespace RaidCrawler.Core.Connection;

public class ConnectionWrapperAsync(SwitchConnectionConfig Config, Action<string> StatusUpdate) : Offsets
{
    public readonly ISwitchConnectionAsync Connection = Config.Protocol switch
    {
        SwitchProtocol.USB => new SwitchUSBAsync(Config.Port),
        _ => new SwitchSocketAsync(Config),
    };

    public bool Connected => IsConnected;
    private bool IsConnected { get; set; }
    private readonly bool CRLF = Config.Protocol is SwitchProtocol.WiFi;
    private static ulong BaseBlockKeyPointer;

    public async Task<(bool, string)> Connect(CancellationToken token)
    {
        if (Connected)
            return (true, "");

        try
        {
            StatusUpdate("Connecting...");
            Connection.Connect();
            BaseBlockKeyPointer = await Connection
                .PointerAll(BlockKeyPointer.ToArray(), token)
                .ConfigureAwait(false);
            IsConnected = true;
            StatusUpdate("Connected!");
            return (true, "");
        }
        catch (SocketException e)
        {
            IsConnected = false;
            return (false, e.Message);
        }
    }

    public async Task<(bool, string)> DisconnectAsync(CancellationToken token)
    {
        if (!Connected)
            return (true, "");

        try
        {
            StatusUpdate("Disconnecting controller...");
            await Connection
                .SendAsync(SwitchCommand.DetachController(CRLF), token)
                .ConfigureAwait(false);

            StatusUpdate("Disconnecting...");
            Connection.Disconnect();
            IsConnected = false;
            StatusUpdate("Disconnected!");
            return (true, "");
        }
        catch (SocketException e)
        {
            IsConnected = false;
            return (false, e.Message);
        }
    }

    public async Task<int> GetStoryProgress(CancellationToken token)
    {
        for (int i = DifficultyFlags.Length - 1; i >= 0; i--)
        {
            // See https://github.com/Lincoln-LM/sv-live-map/pull/43
            var block = await ReadSaveBlock(DifficultyFlags[i], 1, token).ConfigureAwait(false);
            if (block[0] == 2)
                return i + 1;
        }
        return 0;
    }

    private async Task<byte[]> ReadSaveBlock(uint key, int size, CancellationToken token)
    {
        var block_ofs = await SearchSaveKey(key, token).ConfigureAwait(false);
        var data = await Connection
            .ReadBytesAbsoluteAsync(block_ofs + 8, 0x8, token)
            .ConfigureAwait(false);
        block_ofs = BitConverter.ToUInt64(data, 0);

        var block = await Connection
            .ReadBytesAbsoluteAsync(block_ofs, size, token)
            .ConfigureAwait(false);
        return DecryptBlock(key, block);
    }

    private async Task<byte[]> ReadSaveBlockObject(uint key, CancellationToken token)
    {
        var header_ofs = await SearchSaveKey(key, token).ConfigureAwait(false);
        var data = await Connection
            .ReadBytesAbsoluteAsync(header_ofs + 8, 8, token)
            .ConfigureAwait(false);
        header_ofs = BitConverter.ToUInt64(data);

        var header = await Connection
            .ReadBytesAbsoluteAsync(header_ofs, 5, token)
            .ConfigureAwait(false);
        header = DecryptBlock(key, header);

        var size = BitConverter.ToUInt32(header.AsSpan()[1..]);
        var obj = await Connection
            .ReadBytesAbsoluteAsync(header_ofs, (int)size + 5, token)
            .ConfigureAwait(false);
        return DecryptBlock(key, obj)[5..];
    }

    public async Task<byte[]> ReadBlockDefault(uint key, string? cache, bool force, CancellationToken token)
    {
        var folder = Path.Combine(Directory.GetCurrentDirectory(), "cache");
        Directory.CreateDirectory(folder);

        var path = Path.Combine(folder, cache ?? "");
        if (!force && cache is not null && File.Exists(path))
            return await File.ReadAllBytesAsync(path, token);

        var bin = await ReadSaveBlockObject(key, token).ConfigureAwait(false);
        await File.WriteAllBytesAsync(path, bin, token);
        return bin;
    }

    private async Task<ulong> SearchSaveKey(uint key, CancellationToken token)
    {
        var data = await Connection
            .ReadBytesAbsoluteAsync(BaseBlockKeyPointer + 8, 16, token)
            .ConfigureAwait(false);
        var start = BitConverter.ToUInt64(data.AsSpan()[..8]);
        var end = BitConverter.ToUInt64(data.AsSpan()[8..]);

        while (start < end)
        {
            var block_ct = (end - start) / 48;
            var mid = start + ((block_ct >> 1) * 48);

            data = await Connection.ReadBytesAbsoluteAsync(mid, 4, token).ConfigureAwait(false);
            var found = BitConverter.ToUInt32(data);
            if (found == key)
                return mid;

            if (found >= key)
                end = mid;
            else
                start = mid + 48;
        }
        return start;
    }

    private static byte[] DecryptBlock(uint key, byte[] block)
    {
        var rng = new SCXorShift32(key);
        for (int i = 0; i < block.Length; i++)
            block[i] = (byte)(block[i] ^ rng.Next());
        return block;
    }

    private async Task Click(SwitchButton button, int delay, CancellationToken token)
    {
        await Connection
            .SendAsync(SwitchCommand.Click(button, CRLF), token)
            .ConfigureAwait(false);
        await Task.Delay(delay, token).ConfigureAwait(false);
    }

    private async Task Touch(int x, int y, int hold, int delay, CancellationToken token)
    {
        var command = Encoding.ASCII.GetBytes(
            $"touchHold {x} {y} {hold}{(CRLF ? "\r\n" : "")}"
        );
        await Connection.SendAsync(command, token).ConfigureAwait(false);
        await Task.Delay(delay, token).ConfigureAwait(false);
    }

    private async Task SetStick(
        SwitchStick stick,
        short x,
        short y,
        int hold,
        int delay,
        CancellationToken token
    )
    {
        await Connection
            .SendAsync(SwitchCommand.SetStick(stick, x, y, CRLF), token)
            .ConfigureAwait(false);
        await Task.Delay(hold, token).ConfigureAwait(false);
        await Connection
            .SendAsync(SwitchCommand.SetStick(stick, 0, 0, CRLF), token)
            .ConfigureAwait(false);
        await Task.Delay(delay, token).ConfigureAwait(false);
    }

    private async Task PressAndHold(
        SwitchButton b,
        int hold,
        int delay,
        CancellationToken token
    )
    {
        await Connection.SendAsync(SwitchCommand.Hold(b, CRLF), token).ConfigureAwait(false);
        await Task.Delay(hold, token).ConfigureAwait(false);
        await Connection.SendAsync(SwitchCommand.Release(b, CRLF), token).ConfigureAwait(false);
        await Task.Delay(delay, token).ConfigureAwait(false);
    }

    private async Task SkipHour(int hours, int delay, CancellationToken token)
    {
        var command = Encoding.ASCII.GetBytes($"timeSkipForward{(CRLF ? "\r\n" : "")}");
        for (int i = 0; i < hours; i++)
        {
            await Connection.SendAsync(command, token).ConfigureAwait(false);
            await Task.Delay(delay, token).ConfigureAwait(false);
        }
    }

    public async Task ResetTime(CancellationToken token)
    {
        var command = Encoding.ASCII.GetBytes($"resetTime{(CRLF ? "\r\n" : "")}");
        await Connection.SendAsync(command, token).ConfigureAwait(false);
    }

    // Thank you to Anubis for sharing a more optimized routine, as well as CloseGame(), StartGame(), and SaveGame()!
    public async Task AdvanceDate(
        IDateAdvanceConfig config,
        int skips,
        CancellationToken token,
        Action<int>? action = null
    )
    {
        // Not great, but when adding/removing clicks, make sure to account for command count for an accurate StreamerView progress bar.
        int steps =
            (config.UseTouch ? 19 : 25)
            + (config.UseOvershoot ? 2 : config.SystemDownPresses)
            + (config.DodgeSystemUpdate ? 2 : 0)
            + config.DaysToSkip;

        if (config.ZyroMethod)
            steps = 3;

        StatusUpdate("Changing date...");
        var BaseDelay = config.BaseDelay;

        if (config.ZyroMethod)
        {
            if (skips % 2 == 0)
            {
                await SkipHour(24, 0, token).ConfigureAwait(false);
            }
            else
            {
                await ResetTime(token).ConfigureAwait(false);
            }
            await Task.Delay(3_000, token).ConfigureAwait(false);
        }
        else
        {
            if (!config.UseMapTrick)
            {
                // Sometimes the first command drops, click twice with shorter delays for good measure.
                await Click(B, 0_100, token).ConfigureAwait(false);
                UpdateProgressBar(action, steps);

                await Click(B, 0_100, token).ConfigureAwait(false);
                UpdateProgressBar(action, steps);
            }
            else
            {
                await Click(ZL, 0_100, token).ConfigureAwait(false);
                await Click(ZL, 1_500, token).ConfigureAwait(false);
                UpdateProgressBar(action, steps);

                await Click(ZR, 2_000, token).ConfigureAwait(false);
                UpdateProgressBar(action, steps);
            }

            // HOME Menu
            await Click(HOME, config.OpenHomeDelay + BaseDelay, token).ConfigureAwait(false);
            UpdateProgressBar(action, steps);

            // Navigate to Settings
            if (config.UseTouch)
            {
                await Touch(0_840, 0_540, 0_050, 0, token).ConfigureAwait(false);
                UpdateProgressBar(action, steps);
            }
            else
            {
                await Click(DDOWN, config.NavigateToSettingsDelay + BaseDelay, token)
                    .ConfigureAwait(false);
                UpdateProgressBar(action, steps);

                for (int i = 0; i < 5; i++)
                {
                    await Click(DRIGHT, config.NavigateToSettingsDelay + BaseDelay, token)
                        .ConfigureAwait(false);
                    UpdateProgressBar(action, steps);
                }
            }

            await Click(A, config.OpenSettingsDelay + BaseDelay, token).ConfigureAwait(false);
            UpdateProgressBar(action, steps);

            // Scroll to bottom
            if (config.UseSetStick)
                await SetStick(SwitchStick.LEFT, 0, -30_000, config.HoldDuration, 0_100 + BaseDelay, token).ConfigureAwait(false);
            else
                await PressAndHold(DDOWN, config.HoldDuration, 0_100 + BaseDelay, token).ConfigureAwait(false);
            UpdateProgressBar(action, steps);

            // Navigate to "Date and Time"
            StatusUpdate("Navigating to \"Date and Time\"...");
            await Click(A, 0_300 + BaseDelay, token).ConfigureAwait(false);
            UpdateProgressBar(action, steps);

            // Hold down to overshoot Date/Time by one. DUP to recover.
            if (config.UseOvershoot)
            {
                if (config.UseSetStick)
                    await SetStick(SwitchStick.LEFT, 0, -30_000, config.SystemOvershoot, 0_100 + BaseDelay, token).ConfigureAwait(false);
                else
                    await PressAndHold(DDOWN, config.SystemOvershoot, 0_100 + BaseDelay, token).ConfigureAwait(false);
                UpdateProgressBar(action, steps);

                await Click(DUP, 0_500 + BaseDelay, token).ConfigureAwait(false);
                UpdateProgressBar(action, steps);
            }
            else
            {
                for (int i = 0; i < config.SystemDownPresses; i++)
                {
                    await Click(DDOWN, 0_100 + BaseDelay, token).ConfigureAwait(false);
                    UpdateProgressBar(action, steps);
                }
            }

            // Enter Date/Time
            await Click(A, config.Submenu + BaseDelay, token).ConfigureAwait(false);
            UpdateProgressBar(action, steps);

            // Open Date/Time settings
            if (config.UseTouch)
            {
                await Touch(0_950, 0_400, 0_050, 0, token).ConfigureAwait(false);
                UpdateProgressBar(action, steps);
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    await Click(DDOWN, 0_100 + BaseDelay, token).ConfigureAwait(false);
                    UpdateProgressBar(action, steps);
                }
            }

            await Click(A, config.DateChange + BaseDelay, token).ConfigureAwait(false);
            UpdateProgressBar(action, steps);

            for (int i = 0; i < config.DaysToSkip; i++)
            {
                await Click(DUP, 0_100 + BaseDelay, token).ConfigureAwait(false);
                UpdateProgressBar(action, steps);
            }

            for (int i = 0; i < 6; i++)
            {
                await Click(DRIGHT, (i < 5 ? 0_050 : 0_100) + BaseDelay, token)
                    .ConfigureAwait(false);
                UpdateProgressBar(action, steps);
            }

            await Click(A, 0_150 + config.DateChange + BaseDelay, token).ConfigureAwait(false);
            UpdateProgressBar(action, steps);

            // Return to game
            await Click(HOME, config.ReturnHomeDelay + BaseDelay, token).ConfigureAwait(false);
            UpdateProgressBar(action, steps);

            await Click(HOME, (config.DodgeSystemUpdate ? 0_500 : config.ReturnGameDelay) + BaseDelay, token).ConfigureAwait(false);
            UpdateProgressBar(action, steps);

            if (config.DodgeSystemUpdate)
            {
                // Attempt to dodge an update prompt.
                await Click(DUP, 0_600 + BaseDelay, token).ConfigureAwait(false);
                UpdateProgressBar(action, steps);

                await Click(A, config.ReturnGameDelay + BaseDelay, token).ConfigureAwait(false);
                UpdateProgressBar(action, steps);
            }

            StatusUpdate("Back in the game...");
        }
    }

    public async Task CloseGame(CancellationToken token)
    {
        // Close out of the game
        StatusUpdate("Closing the game!");
        await Click(B, 0_500, token).ConfigureAwait(false);
        await Click(HOME, 2_000, token).ConfigureAwait(false);
        await Click(X, 1_000, token).ConfigureAwait(false);
        await Click(A, 5_500, token).ConfigureAwait(false);
        StatusUpdate("Closed out of the game!");
    }

    public async Task StartGame(IDateAdvanceConfig config, CancellationToken token)
    {
        // Open game.
        StatusUpdate("Starting the game!");
        await Click(A, 1_000, token).ConfigureAwait(false);

        // Attempt to dodge an update prompt;
        await Click(DUP, 0_600, token).ConfigureAwait(false);
        await Click(A, 1_000, token).ConfigureAwait(false);

        // Allow time for profile check if required
        await Task.Delay(config.RelaunchDelay, token).ConfigureAwait(false);

        // If they have DLC on the system and can't use it, requires an UP + A to start the game.
        // Should be harmless otherwise since they'll be in loading screen.
        await Click(DUP, 0_600, token).ConfigureAwait(false);
        await Click(A, 0_600, token).ConfigureAwait(false);

        // Switch Logo and game load screen
        await Task.Delay(17_000, token).ConfigureAwait(false);

        for (int i = 0; i < 20; i++)
            await Click(A, 1_000, token).ConfigureAwait(false);

        // Particularly slow switches need more time to load the overworld
        await Task.Delay(config.ExtraOverworldWait, token).ConfigureAwait(false);

        StatusUpdate("Back in the overworld! Refreshing the base block key pointer...");
        BaseBlockKeyPointer = await Connection
            .PointerAll(BlockKeyPointer.ToArray(), token)
            .ConfigureAwait(false);
    }

    public async Task SaveGame(IDateAdvanceConfig config, CancellationToken token)
    {
        StatusUpdate("Saving the game...");
        // B out in case we're in some menu.
        for (int i = 0; i < 4; i++)
            await Click(B, 0_500, token).ConfigureAwait(false);

        // Open the menu and save.
        await Click(X, 1_000, token).ConfigureAwait(false);
        await Click(R, 1_000, token).ConfigureAwait(false);
        await Click(A, 1_000, token).ConfigureAwait(false);
        await Click(A, 1_000, token).ConfigureAwait(false);
        await Click(A, 3_000 + config.SaveGameDelay, token).ConfigureAwait(false);

        // Return to overworld.
        for (int i = 0; i < 4; i++)
            await Click(B, 0_500, token).ConfigureAwait(false);
        StatusUpdate("Game saved!");
    }

    private static void UpdateProgressBar(Action<int>? action, int steps) => action?.Invoke(steps);
}
