using PKHeX.Core;
using System.Numerics;
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

    public static Version? GetLatestVersion()
    {
        const string endpoint = "https://api.github.com/repos/LegoFigure11/RaidCrawler/releases/latest";
        var response = NetUtil.GetStringFromURL(new Uri(endpoint));
        if (response is null) return null;

        const string tag = "tag_name";
        var index = response.IndexOf(tag, StringComparison.Ordinal);
        if (index == -1) return null;

        var first = response.IndexOf('"', index + tag.Length + 1) + 1;
        if (first == 0) return null;

        var second = response.IndexOf('"', first);
        if (second == -1) return null;

        var tagString = response.AsSpan()[first..second].TrimStart('v');

        var patchIndex = tagString.IndexOf('-');
        if (patchIndex != -1) tagString = tagString.ToString().Remove(patchIndex).AsSpan();

        return !Version.TryParse(tagString, out var latestVersion) ? null : latestVersion;
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
