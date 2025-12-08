using shared;

namespace day8;

internal record Point3D(long X, long Y, long Z);

public static class D8P1
{
    public static object Part1Answer(this string input, int n = 1000)
    {
        var positions = input.ParsePoints().ToList();
        SortedList<double, (Point3D one, Point3D two)> sortedDistances = [];
        Dictionary<(Point3D one, Point3D two), double> distances = [];
        foreach (var one in positions)
        {
            foreach (var two in positions)
            {
                if (one == two)
                    continue;

                var key1 = (one, two);
                if(distances.ContainsKey(key1))
                    continue;

                var dist = one.Distance(two);

                var key2 = (two, one);
                distances[key1] = distances[key2] = dist;
                sortedDistances.Add(dist, key1);
            }
        }

        var circuitByPos = positions.WithIndex().ToDictionary(pair => pair.item, pair => pair.index);
        var posByCircuit = positions.WithIndex().ToDictionary(pair => pair.index, pair => new List<Point3D> { pair.item });
        for (int q = 0; q < n; q++)
        {
            var pair = sortedDistances.Skip(q).First().Value;
            var circuit1 = circuitByPos[pair.one];
            var circuit2 = circuitByPos[pair.two];

            if (circuit1 == circuit2)
                continue;

            // Merge the circuits
            foreach (var pos in posByCircuit[circuit2])
            {
                posByCircuit[circuit1].Add(pos);
                circuitByPos[pos] = circuit1;
            }

            posByCircuit.Remove(circuit2);
        }
        return posByCircuit.ProductOfLongest3();
    }

    internal static long ProductOfLongest3(this Dictionary<int, List<Point3D>> posByCircuit)
    {
        var biggest3 = posByCircuit.Values
            .Select(l => (long)l.Count)
            .OrderDescending()
            .Take(3)
            .ToList();
        var v = biggest3[0] * biggest3[1] * biggest3[2];
        Console.WriteLine($"Biggest 3: {biggest3[0]} * {biggest3[1]} * {biggest3[2]} = {v}");
        return v;
    }

    internal static double Distance(this Point3D one, Point3D two)
        => Math.Sqrt(
            (one.X - two.X) * (one.X - two.X) +
            (one.Y - two.Y) * (one.Y - two.Y) +
            (one.Z - two.Z) * (one.Z - two.Z)
        );


    extension(string input)
    {
        internal IEnumerable<Point3D> ParsePoints() =>
            input
                .Lines()
                .Select(TryParseAsPoint)
                .OfType<Point3D>();

        internal Point3D? TryParseAsPoint()
        {
            var s = input.Split(',');
            if (s.Length == 3)
                return new(long.Parse(s[0]), long.Parse(s[1]), long.Parse(s[2]));
            return null;
        }
    }
}