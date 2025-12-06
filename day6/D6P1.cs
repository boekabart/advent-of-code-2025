using shared;

namespace day6;

public static class D6P1
{
    public static object Part1Answer(this string input)
    { 
        var array = input
            .NotEmptyTrimmedLines()
            .Select(l => l.Split([' '], StringSplitOptions.RemoveEmptyEntries))
            .ToArray();

        var width = array.Max(line => line.Length);

        long sum = 0L;

        for (int x = 0; x < width; x++)
        {
            var y = 0;

            List<long> operands = [];
            while (true)
            {
                var row = array[y];
                y++;
                if (row.Length <= x)
                    continue;

                var cel = row[x];

                if (cel == "+")
                {
                    sum += operands.Sum();
                    break;
                }

                if (cel == "*")
                {
                    sum += operands.Times();
                    break;
                }

                if (long.TryParse(cel, out var value))
                    operands.Add(value);
            }
        }

        return sum;
    }


    internal static long Times(this IEnumerable<long> values) =>
        values.Aggregate(1L, (a, b) => a * b);
}