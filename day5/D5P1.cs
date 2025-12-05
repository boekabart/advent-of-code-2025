using shared;

namespace day5;

internal record Range(long Start, long EndInclusive);


public static class D5P1
{
    public static object Part1Answer(this string input)
    {
        var ranges = input.ParseRanges().OrderBy(r => r.Start).ToList();
        return input.ParseIds()
            .Where(id => id.IsFresh(ranges))
            .Count();
    }

    internal static IEnumerable<Range> ParseRanges(this string input) =>
        input
            .Lines()
            .Select(TryParseAsRange)
            .OfType<Range>();
    internal static IEnumerable<long> ParseIds(this string input) =>
        input
            .Lines()
            .Select(l => long.TryParse(l, out var id)?(long?)id:null)
            .OfType<long>();

    internal static Range? TryParseAsRange(this string line)
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

    internal static bool IsFresh(this long id, ICollection<Range> ranges) => 
    ranges.TakeWhile(r => r.Start <=id)
        .Any(r => r.Contains(id));

    internal static bool Contains(this Range range, long id)
        => range.Start <= id && range.EndInclusive >= id;

}