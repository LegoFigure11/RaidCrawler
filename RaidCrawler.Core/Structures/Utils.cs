using PKHeX.Core;
using System.Reflection;

namespace RaidCrawler.Core.Structures;

public static class Utils
{
    private static readonly Assembly thisAssembly;
    private static readonly Dictionary<string, string> resourceNameMap;

    static Utils()
    {
        thisAssembly = Assembly.GetExecutingAssembly();
        resourceNameMap = BuildLookup(thisAssembly.GetManifestResourceNames());
    }

    private static Dictionary<string, string> BuildLookup(IReadOnlyCollection<string> manifestNames)
    {
        var result = new Dictionary<string, string>(manifestNames.Count);
        foreach (var resName in manifestNames)
        {
            var fileName = GetFileName(resName);
            result.TryAdd(fileName, resName);
        }
        return result;
    }

    private static string GetFileName(string resName)
    {
        var period = resName.LastIndexOf('.', resName.Length - 6);
        var start = period + 1;
        System.Diagnostics.Debug.Assert(start != 0);

        // text file fetch excludes ".txt" (mixed case...); other extensions are used (all lowercase).
        return resName.EndsWith(".txt", StringComparison.Ordinal)
            ? resName[start..^4].ToLowerInvariant()
            : resName[start..];
    }

    public static byte[] GetBinaryResource(string name)
    {
        if (!resourceNameMap.TryGetValue(name, out var resName))
            return [];

        using var resource = thisAssembly.GetManifestResourceStream(resName);
        if (resource is null)
            return [];

        var buffer = new byte[resource.Length];
        _ = resource.Read(buffer, 0, (int)resource.Length);
        return buffer;
    }

    public static string? GetStringResource(string name)
    {
        if (!resourceNameMap.TryGetValue(name.ToLowerInvariant(), out var resourceName))
            return null;

        using var resource = thisAssembly.GetManifestResourceStream(resourceName);
        if (resource is null)
            return null;

        using var reader = new StreamReader(resource);
        return reader.ReadToEnd();
    }

    public static string GetFormString(ushort species, byte form, GameStrings formStrings, EntityContext context = EntityContext.Gen9)
    {
        var result = ShowdownParsing.GetStringFromForm(form, formStrings, species, context);
        if (result.Length > 0 && result[0] != '-')
            return result.Insert(0, "-");
        return result;
    }

    public static int[] ToSpeedLast(ReadOnlySpan<int> ivs) => [ivs[0], ivs[1], ivs[2], ivs[4], ivs[5], ivs[3]];
}
