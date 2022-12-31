using PKHeX.Core;
using SysBot.Base;

namespace RaidCrawler.Structures
{
    internal class Offsets
    {
        public const string ScarletID = "0100A3D008C5C000";
        public const string VioletID = "01008F6008C5E000";

        public const string RaidBlockPointer = "[[main+4384B18]+180]+40";
        public const string SaveBlockPointer = "[[[main+4385F30]+80]+8]"; // Thanks Lincoln-LM!
        public static (int, uint)[] DifficultyFlags = { ( 0x2BF20, 0xEC95D8EF ), (0x1F400, 0xA9428DFE), (0x1B640, 0x9535F471), (0x13EC0, 0x6E7F8220) }; // Thanks Lincoln-LM!
        public static (int, uint) BCATRaidBinaryLocation = (0x1040, 0x520A1B0); // Thanks Lincoln-LM!
        public static (int, uint) BCATRaidPriorityLocation = (0x1860, 0x95451E4); // Thanks Lincoln-LM!
        public static (int, uint) BCATRaidFixedRewardLocation = (0x16D40, 0x7D6C2B82);
        public static (int, uint) BCATRaidLotteryRewardLocation = (0x1E6A0, 0xA52B4811);
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
