﻿using System.Net.Sockets;
using System.Text;
using PKHeX.Core;
using SysBot.Base;
using RaidCrawler.Core.Interfaces;
using RaidCrawler.Core.Structures;
using static SysBot.Base.SwitchButton;

namespace RaidCrawler.Core.Connection
{
    public class ConnectionWrapperAsync : Offsets
    {
        public readonly ISwitchConnectionAsync Connection;
        public bool Connected { get => Connection is not null && IsConnected; }
        private bool IsConnected { get; set; }
        private readonly bool CRLF;
        private readonly Action<string> _statusUpdate;
        private static ulong BaseBlockKeyPointer = 0;

        public ConnectionWrapperAsync(SwitchConnectionConfig config, Action<string> statusUpdate)
        {
            Connection = config.Protocol switch
            {
                SwitchProtocol.USB => new SwitchUSBAsync(config.Port),
                _ => new SwitchSocketAsync(config),
            };

            CRLF = config.Protocol is SwitchProtocol.WiFi;
            _statusUpdate = statusUpdate;
        }

        public async Task<(bool, string)> Connect(CancellationToken token)
        {
            if (Connected)
                return (true, "");

            try
            {
                _statusUpdate("Connecting...");
                Connection.Connect();
                BaseBlockKeyPointer = await Connection.PointerAll(BlockKeyPointer, token).ConfigureAwait(false);
                IsConnected = true;
                _statusUpdate("Connected!");
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
                _statusUpdate("Disconnecting controller...");
                await Connection.SendAsync(SwitchCommand.DetachController(true), token).ConfigureAwait(false);

                _statusUpdate("Disconnecting...");
                Connection.Disconnect();
                IsConnected = false;
                _statusUpdate("Disconnected!");
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
            for (int i = DifficultyFlags.Count - 1; i >= 0; i--)
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
            var data = await Connection.ReadBytesAbsoluteAsync(block_ofs + 8, 0x8, token).ConfigureAwait(false);
            block_ofs = BitConverter.ToUInt64(data, 0);

            var block = await Connection.ReadBytesAbsoluteAsync(block_ofs, size, token).ConfigureAwait(false);
            return DecryptBlock(key, block);
        }

        private async Task<byte[]> ReadSaveBlockObject(uint key, CancellationToken token)
        {
            var header_ofs = await SearchSaveKey(key, token).ConfigureAwait(false);
            var data = await Connection.ReadBytesAbsoluteAsync(header_ofs + 8, 8, token).ConfigureAwait(false);
            header_ofs = BitConverter.ToUInt64(data);

            var header = await Connection.ReadBytesAbsoluteAsync(header_ofs, 5, token).ConfigureAwait(false);
            header = DecryptBlock(key, header);

            var size = BitConverter.ToUInt32(header.AsSpan()[1..]);
            var obj = await Connection.ReadBytesAbsoluteAsync(header_ofs, (int)size + 5, token).ConfigureAwait(false);
            return DecryptBlock(key, obj)[5..];
        }

        public async Task<byte[]> ReadBlockDefault(uint key, string? cache, bool force, CancellationToken token)
        {
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "cache");
            Directory.CreateDirectory(folder);

            var path = Path.Combine(folder, cache ?? "");
            if (force is false && cache is not null && File.Exists(path))
                return File.ReadAllBytes(path);

            var bin = await ReadSaveBlockObject(key, token).ConfigureAwait(false);
            File.WriteAllBytes(path, bin);
            return bin;
        }

        public async Task<ulong> SearchSaveKey(uint key, CancellationToken token)
        {
            var data = await Connection.ReadBytesAbsoluteAsync(BaseBlockKeyPointer + 8, 16, token).ConfigureAwait(false);
            var start = BitConverter.ToUInt64(data.AsSpan()[..8]);
            var end = BitConverter.ToUInt64(data.AsSpan()[8..]);

            while (start < end)
            {
                var block_ct = (end - start) / 32;
                var mid = start + (block_ct >> 1) * 32;

                data = await Connection.ReadBytesAbsoluteAsync(mid, 4, token).ConfigureAwait(false);
                var found = BitConverter.ToUInt32(data);
                if (found == key)
                    return mid;

                if (found >= key)
                    end = mid;
                else start = mid + 32;
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

        public async Task Click(SwitchButton button, int delay, CancellationToken token)
        {
            await Connection.SendAsync(SwitchCommand.Click(button, CRLF), token).ConfigureAwait(false);
            await Task.Delay(delay, token).ConfigureAwait(false);
        }

        public async Task Touch(int x, int y, int hold, int delay, CancellationToken token)
        {
            var command = Encoding.ASCII.GetBytes($"touchHold {x} {y} {hold}{(CRLF ? "\r\n" : "")}");
            await Connection.SendAsync(command, token).ConfigureAwait(false);
            await Task.Delay(delay, token).ConfigureAwait(false);
        }

        public async Task PressAndHold(SwitchButton b, int hold, int delay, CancellationToken token)
        {
            await Connection.SendAsync(SwitchCommand.Hold(b, CRLF), token).ConfigureAwait(false);
            await Task.Delay(hold, token).ConfigureAwait(false);
            await Connection.SendAsync(SwitchCommand.Release(b, CRLF), token).ConfigureAwait(false);
            await Task.Delay(delay, token).ConfigureAwait(false);
        }

        public async Task AdvanceDate(IDateAdvanceConfig config, CancellationToken token)
        {
            int delay = config.BaseDelay;
            await Click(LSTICK, 0_050 + delay, token).ConfigureAwait(false); // Sometimes it seems like the first command doesn't go through so send this just in case

            // HOME Menu
            await Click(HOME, config.OpenHomeDelay + delay, token).ConfigureAwait(false);

            // Navigate to Settings
            if (config.UseTouch)
                await Touch(840, 540, 0_250, delay, token).ConfigureAwait(false);
            else
            {
                await Click(DDOWN, config.NavigateToSettingsDelay + 0_100 + delay, token).ConfigureAwait(false);
                for (int i = 0; i < 5; i++)
                    await Click(DRIGHT, config.NavigateToSettingsDelay + delay, token).ConfigureAwait(false);
            }
            await Click(A, config.OpenSettingsDelay + delay, token).ConfigureAwait(false);

            // Scroll to bottom
            await PressAndHold(DDOWN, config.HoldDuration, delay, token).ConfigureAwait(false);

            // Navigate to "Date and Time"
            await Click(DRIGHT, 0_200 + delay, token).ConfigureAwait(false);

            // Hold down to overshoot Date/Time by one. DUP to recover.
            if (config.UseOvershoot)
            {
                await PressAndHold(DDOWN, config.SystemOvershoot, 0, token).ConfigureAwait(false);
                await Click(DUP, 0_500, token).ConfigureAwait(false);
            }
            else
            {
                for (int i = 0; i < config.SystemDownPresses; i++)
                    await Click(DDOWN, 0_050 + delay, token).ConfigureAwait(false);
            }
            await Click(A, config.Submenu + delay, token).ConfigureAwait(false);

            // Navigate to Change Date/Time
            if (config.UseTouch)
                await Touch(840, 400, 0_050, 0_300 + delay, token).ConfigureAwait(false);
            else
            {
                for (int i = 0; i < 2; i++)
                    await Click(DDOWN, 0_200 + delay, token).ConfigureAwait(false);
                await Click(A, config.DateChange + delay, token).ConfigureAwait(false);
            }

            // Change the date
            for (int i = 0; i < config.DaysToSkip; i++)
                await Click(DUP, 0_200 + delay, token).ConfigureAwait(false); // Not actually necessary, so we default to 0 as per #29

            for (int i = 0; i < 6; i++)
                await Click(DRIGHT, 0_050 + delay, token).ConfigureAwait(false);
            await Click(A, 0_500 + delay, token).ConfigureAwait(false);

            // Return to game
            await Click(HOME, config.ReturnHomeDelay + delay, token).ConfigureAwait(false);
            await Click(HOME, config.ReturnGameDelay + delay, token).ConfigureAwait(false);
        }
    }
}
