namespace day2;

public static class D2P2
{
    public static object Part2Answer(this string input) =>
        input.ParseThings().GetResult2();


    internal static long GetResult2(this IEnumerable<Range> things) =>
        things
            .SelectMany(range => Enumerable.Range(0, (int)(range.EndInclusive - range.Start + 1))
                .Select(c => (long)c + range.Start))
            .Select(ZeroIfValid2)
            .Sum();
    internal static long ZeroIfValid2(this long number) =>
        number.IsValidId2() ? 0 : (long)number;
    internal static bool IsValidId2(this long number)
    {
        var asString = number.ToString();
        var l = asString.Length;
        for (int parts = 2; parts <= l; parts++)
        {
            if (l % parts != 0)
                continue;
            var partLength = l / parts;
            var part = asString[..partLength];
            var allMatch = true;
            for (int i = 1; i < parts; i++)
            {
                var part2 = asString[(i * partLength)..((i + 1) * partLength)];
                if (part2 != part)
                {
                    allMatch = false;
                    break;
                }
            }

            if (allMatch)
                return false;
        }

        return true;
    }
}
