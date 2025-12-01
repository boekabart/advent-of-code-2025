using shared;

namespace day1;


public static class D1P1
{
    public static object Part1Answer(this string input)
    {
        var lists = ParseThings(input).ToList();
        return lists.GetResult1();
    }

    internal static IEnumerable<int> ParseThings(this string input) =>
        input
            .Lines()
            .Select(TryParseAsThing)
            .OfType<int>();

    internal static int? TryParseAsThing(this string line)
    {
        if (int.TryParse(line.Replace("R", "").Replace("L", "-"), out var delta))
            return delta;
        return null;
    }

    internal static int GetResult1(this IEnumerable<int> things) => things.Aggregate((50, 0), (prev, delta) =>
    {
        var q = (prev.Item1 + delta) % 100;
        return (q, prev.Item2 + (q == 0 ? 1 : 0));
    }).Item2;
}