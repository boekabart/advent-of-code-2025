namespace day5;

public static class D5P2
{
    public static object Part2Answer(this string input)
    {
        var ranges = input.ParseRanges().OrderBy(r => r.Start).ToList();

        var mergedRanges = ranges.Merge().ToList();
        return mergedRanges.Select(CountIds).Sum();
    }

    internal static IEnumerable<Range> Merge(this ICollection<Range> ranges)
    {
        var iter = ranges.First();
        foreach (var r in ranges.Skip(1))
        {
            if (!iter.Intersects(r))
            {
                yield return iter;
                iter = r;
            }
            else
            {
                if (iter.EndInclusive < r.EndInclusive)
                    iter = iter with { EndInclusive = r.EndInclusive };
            }
        }

        yield return iter;
    }

    internal static long CountIds(this Range r) =>
        1 + r.EndInclusive - r.Start;

    internal static bool Intersects(this Range r, Range r2)
    {
        if (r.EndInclusive < r2.Start)
            return false;
        if (r2.EndInclusive < r.Start)
            return false;
        return true;
    }

}
