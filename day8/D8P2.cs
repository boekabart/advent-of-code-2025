using shared;

namespace day8;

public static class D8P2
{
    public static object Part2Answer(this string input)
    {
        var positions = input.ParsePoints().ToList();
        SortedList<double, (Point3D one, Point3D two)> sortedDistances = new(input.Length * input.Length / 2);
        Dictionary<(Point3D one, Point3D two), double> distances = [];
        foreach (var one in positions)
        {
            foreach (var two in positions)
            {
                if (one == two)
                    continue;

                var key1 = (one, two);
                if (distances.ContainsKey(key1))
                    continue;

                var dist = one.Distance(two);

                var key2 = (two, one);
                distances[key1] = distances[key2] = dist;
                sortedDistances.Add(dist, key1);
            }
        }

        var circuitByPos = positions.WithIndex().ToDictionary(pair => pair.item, pair => pair.index);
        var posByCircuit = positions.WithIndex().ToDictionary(pair => pair.index, pair => new List<Point3D> { pair.item });

        foreach (var kvp in sortedDistances)
        {
            var pairOfPoints = kvp.Value;
            var circuit1 = circuitByPos[pairOfPoints.one];
            var circuit2 = circuitByPos[pairOfPoints.two];

            if (circuit1 == circuit2)
                continue;

            // Merge the circuits
            foreach (var pos in posByCircuit[circuit2])
            {
                posByCircuit[circuit1].Add(pos);
                circuitByPos[pos] = circuit1;
            }

            posByCircuit.Remove(circuit2);

            if (posByCircuit.Count == 1)
                return pairOfPoints.one.X * pairOfPoints.two.X;
        }

        throw new Exception("Did not expect that connecting all pairs would lead to more than one circuit");
    }
}
