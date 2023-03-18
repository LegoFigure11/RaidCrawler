using PKHeX.Core;
using SysBot.Base;

namespace RaidCrawler.Structures
{
    internal class Offsets
    {
        public const string ScarletID = "0100A3D008C5C000";
        public const string VioletID = "01008F6008C5E000";

        public const string RaidBlockPointer = "[[main+44A98C8]+180]+40";
        public const string SaveBlockPointer = "[[[[main+44AAC88]+E0]+80]+08]"; // Thanks Lincoln-LM!
        public const string BlockKeyPointers = "[[[[[main+449EEE8]+D8]]]+30]";
        public static uint[] DifficultyFlags = { 0xEC95D8EF, 0xA9428DFE, 0x9535F471, 0x6E7F8220 };
        public static uint BCATRaidBinaryLocation = 0x520A1B0; // Thanks Lincoln-LM!
        public static uint BCATRaidPriorityLocation = 0x95451E4; // Thanks Lincoln-LM!
        public static uint BCATRaidFixedRewardLocation = 0x7D6C2B82;
        public static uint BCATRaidLotteryRewardLocation = 0xA52B4811;
    }

    internal class OffsetUtil
    {
        readonly SwitchSocketAsync SwitchConnection;
        public OffsetUtil(SwitchSocketAsync c)
        {
            SwitchConnection = c;
        }

        // From LiveHex
        // MIT License
        // https://github.com/architdate/PKHeX-Plugins
        public async Task<ulong> GetPointerAddress(string pointer, CancellationToken token, bool heaprealtive = false)
        {
            var ptr = pointer;
            if (string.IsNullOrWhiteSpace(ptr) || ptr.IndexOfAny(new char[] { '-', '/', '*' }) != -1)
                return 0;
            while (ptr.Contains("]]"))
                ptr = ptr.Replace("]]", "]+0]");
            uint? finadd = null;
            if (!ptr.EndsWith("]"))
            {
                finadd = Util.GetHexValue(ptr.Split('+').Last());
                ptr = ptr[..ptr.LastIndexOf('+')];
            }
            var jumps = ptr.Replace("main", "").Replace("[", "").Replace("]", "").Split(new[] { "+" }, StringSplitOptions.RemoveEmptyEntries);
            if (jumps.Length == 0)
                return 0;

            var initaddress = Util.GetHexValue(jumps[0].Trim());
            ulong address = BitConverter.ToUInt64(await SwitchConnection.ReadBytesMainAsync(initaddress, 0x8, token).ConfigureAwait(false), 0);
            foreach (var j in jumps)
            {
                var val = Util.GetHexValue(j.Trim());
                if (val == initaddress)
                    continue;
                address = BitConverter.ToUInt64(await SwitchConnection.ReadBytesAbsoluteAsync(address + val, 0x8, token).ConfigureAwait(false), 0);
            }
            if (finadd != null) address += (ulong)finadd;
            if (heaprealtive)
            {
                ulong heap = await SwitchConnection.GetHeapBaseAsync(token);
                address -= heap;
            }
            return address;
        }
    }
}
