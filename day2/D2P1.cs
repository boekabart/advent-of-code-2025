using shared;

namespace day2;

internal record Range(long Start, long EndInclusive);

public static class D2P1
{
    public static object Part1Answer(this string input) =>
        input.ParseThings().GetResult();

    internal static IEnumerable<Range> ParseThings(this string input) =>
        input
            .Lines()
            .SelectMany(l => l.Split([','], StringSplitOptions.RemoveEmptyEntries))
            .Select(TryParseAsThing)
            .OfType<Range>()
            ;

    internal static Range? TryParseAsThing(this string line)
    {
        var s = line.Split('-');
        if (s.Length != 2)
            return null;
        if (!long.TryParse(s[0], out var start))
            return null;
        if (!long.TryParse(s[1], out var endInclusive))  
            return null;
        return new Range(start, endInclusive);
    }

    internal static long GetResult(this IEnumerable<Range> things) =>
        things
            .SelectMany(range => Enumerable.Range(0, (int)(range.EndInclusive - range.Start + 1))
                .Select(c => (long)c + range.Start))
            .Select(ZeroIfValid)
            .Sum();
    internal static long ZeroIfValid(this long number) => 
        number.IsValidId()?0:(long)number;
    internal static bool IsValidId(this long number)
    {
        var asString = number.ToString();
        if (asString.Length % 2!=0)
            return true;
        var halfLength = asString.Length / 2;
        return asString[..halfLength] != asString[halfLength..];
    }
}