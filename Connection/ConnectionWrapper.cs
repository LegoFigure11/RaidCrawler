using System.Net.Sockets;
using System.Text;
using PKHeX.Core;
using RaidCrawler.Structures;
using SysBot.Base;
using static System.Buffers.Binary.BinaryPrimitives;
using static RaidCrawler.Structures.Offsets;
using static SysBot.Base.SwitchButton;

namespace RaidCrawler.Connection
{
    public class ConnectionWrapperAsync
    {
        public readonly ISwitchConnectionAsync Connection;
        public bool Connected { get => Connection.Connected; }
        private readonly bool CRLF;
        protected readonly Action<string> _statusUpdate;
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
                _statusUpdate("Connected!");
                return (true, "");
            }
            catch (SocketException e)
            {
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
                _statusUpdate("Disconnected!");
                return (true, "");
            }
            catch (SocketException e)
            {
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
            header_ofs = BitConverter.ToUInt64(data, 0);

            var header = await Connection.ReadBytesAbsoluteAsync(header_ofs, 5, token).ConfigureAwait(false);
            header = DecryptBlock(key, header);

            var size = ReadUInt32LittleEndian(header.AsSpan()[1..]);
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
            var start = ReadUInt64LittleEndian(data.AsSpan()[..8]);
            var end = ReadUInt64LittleEndian(data.AsSpan()[8..]);

            while (start < end)
            {
                var block_ct = (end - start) / 32;
                var mid = start + (block_ct >> 1) * 32;

                data = await Connection.ReadBytesAbsoluteAsync(mid, 4, token).ConfigureAwait(false);
                var found = ReadUInt32LittleEndian(data);
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

        public async Task AdvanceDate(Config config, CancellationToken token)
        {
            int BaseDelay = (int)config.BaseDelay;
            await Click(LSTICK, 0_050 + BaseDelay, token).ConfigureAwait(false); // Sometimes it seems like the first command doesn't go through so send this just in case

            // HOME Menu
            await Click(HOME, (int)config.OpenHome + BaseDelay, token).ConfigureAwait(false);

            // Navigate to Settings
            if (config.UseTouch)
                await Touch(840, 540, 0_250, BaseDelay, token).ConfigureAwait(false);
            else
            {
                await Click(DDOWN, (int)config.NavigateToSettings + 0_100 + BaseDelay, token).ConfigureAwait(false);
                for (int i = 0; i < 5; i++)
                    await Click(DRIGHT, (int)config.NavigateToSettings + BaseDelay, token).ConfigureAwait(false);
            }
            await Click(A, (int)config.OpenSettings + BaseDelay, token).ConfigureAwait(false);

            // Scroll to bottom
            await PressAndHold(DDOWN, (int)config.Hold, BaseDelay, token).ConfigureAwait(false);

            // Navigate to "Date and Time"
            await Click(DRIGHT, 0_200 + BaseDelay, token).ConfigureAwait(false);

            // Hold down to overshoot Date/Time by one. DUP to recover.
            if (config.UseOvershoot)
            {
                await PressAndHold(DDOWN, (int)config.SystemOvershoot, 0, token).ConfigureAwait(false);
                await Click(DUP, 0_500, token).ConfigureAwait(false);
            }
            else
            {
                for (int i = 0; i < config.SystemDDownPresses; i++)
                    await Click(DDOWN, 0_050 + BaseDelay, token).ConfigureAwait(false);
            }
            await Click(A, (int)config.Submenu + BaseDelay, token).ConfigureAwait(false);

            // Navigate to Change Date/Time
            if (config.UseTouch)
                await Touch(840, 400, 0_050, 0_300 + BaseDelay, token).ConfigureAwait(false);
            else
            {
                for (int i = 0; i < 2; i++)
                    await Click(DDOWN, 0_200 + BaseDelay, token).ConfigureAwait(false);
                await Click(A, (int)config.DateChange + BaseDelay, token).ConfigureAwait(false);
            }

            // Change the date
            for (int i = 0; i < config.DaysToSkip; i++)
                await Click(DUP, 0_200 + BaseDelay, token).ConfigureAwait(false); // Not actually necessary, so we default to 0 as per #29

            for (int i = 0; i < 6; i++)
                await Click(DRIGHT, 0_050 + BaseDelay, token).ConfigureAwait(false);
            await Click(A, 0_500 + BaseDelay, token).ConfigureAwait(false);

            // Return to game
            await Click(HOME, (int)config.ReturnHome + BaseDelay, token).ConfigureAwait(false);
            await Click(HOME, (int)config.ReturnGame + BaseDelay, token).ConfigureAwait(false);
        }
    }
}
