using shared;

namespace day11;

internal record Thing(bool Data);

public static class D11P1
{
    public static object Part1Answer(this string input) =>
        new NotImplementedException();

    internal static IEnumerable<Thing> ParseThings(this string input) =>
        input
            .Lines()
            .Select(TryParseAsThing)
            .OfType<Thing>();

    internal static Thing? TryParseAsThing(this string line)
    {
        return null;
    }

    internal static int GetResult(this IEnumerable<Thing> things) => things.Select(AsResult).Sum();
    internal static int AsResult(this Thing thing) => 0;
}