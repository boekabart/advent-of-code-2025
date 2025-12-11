using shared;

namespace day11;

internal record Thing(string A, string[] B);

public static class D11P1
{
    public static object Part1Answer(this string input)
    {
        var room = input.ParseThings().ToDictionary(t => t.A, t => t.B);
        return "you".NumberOfPathsToOut(room, []);
    }

    internal static int NumberOfPathsToOut(this string src, Dictionary<string, string[]> room, Dictionary<string, int> cache)
    {
        if (src == "out") return 1;
        if (cache.TryGetValue(src, out var value)) return value;
        var total = room[src].Sum(t => t.NumberOfPathsToOut(room, cache));
        cache[src] = total;
        return total;
    }

    internal static IEnumerable<Thing> ParseThings(this string input) =>
        input
            .Lines()
            .Select(TryParseAsThing)
            .OfType<Thing>();

    internal static Thing? TryParseAsThing(this string line)
    {
        var s = line.Split([':', ' '], StringSplitOptions.RemoveEmptyEntries);
        if (s.Length < 2)
            return null;
        return new Thing(s[0], s[1..]);
    }
}