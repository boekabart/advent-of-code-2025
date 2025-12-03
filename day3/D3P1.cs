using shared;

namespace day3;

internal record Thing(int[] joltages);

public static class D3P1
{
    public static object Part1Answer(this string input) =>
        input.ParseThings().GetResult(2);


    internal static IEnumerable<Thing> ParseThings(this string input) =>
        input
            .Lines()
            .Select(TryParseAsThing)
            .OfType<Thing>();

    internal static Thing? TryParseAsThing(this string line)
    {
        var t = line.Trim();
        if (string.IsNullOrEmpty(t)) return null;

        var arr = line.Trim().Select(c => c.ToString()).Select(int.Parse).ToArray();
        if (arr.Length < 1) return null;
        return new Thing(arr);
    }

    internal static long GetResult(this IEnumerable<Thing> things, int n) 
        => things.Select(a => a.AsResultN(n)).Sum();
    
    internal static long AsResult(this Thing thing)
    {
        return thing.AsResultN(2);
    }

    internal static long AsResultN(this Thing thing, int count)
    {
        long result = 0L;
        var iter = thing.joltages;
        if (count > iter.Length)
            count = iter.Length;

        for (int n = count - 1; n >= 0; n--)
        {
            var maxTens = iter[..^n].Max();
            var pos = iter.IndexOf(maxTens);
            iter = iter[(pos + 1)..];

            result = 10L * result;
            result += maxTens;
        }

        return result;
    }

}