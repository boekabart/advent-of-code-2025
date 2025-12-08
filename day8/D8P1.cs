using shared;

namespace day8;

internal record Point3D(long X, long Y, long Z);

public static class D8P1
{
    public static object Part1Answer(this string input, int n = 1000)
    {
        var positions = input.ParsePoints().ToList();
        var pQueue = QueueByDistance(positions, n);

        var hall = new Hall(positions);
        while (pQueue.TryDequeue(out var pair, out _))
        {
            hall.Connect(pair.one, pair.two);
        }

        return hall.PosByCircuit.ProductOfLongest3();
    }

    internal static PriorityQueue<(Point3D one, Point3D two), double> QueueByDistance(this List<Point3D> positions, int n = 0)
    {
        var size = n > 0 ? n : positions.Count * (positions.Count - 1) / 2;
        var pQueue = new PriorityQueue<(Point3D one, Point3D two), double>(size);

        // First enqueue by negative distance, so that we can 'dequeue' items that are too many
        foreach (var pairOne in positions.WithIndex())
        {
            var one = pairOne.item;
            foreach (var two in positions[(pairOne.index+1)..])
            {
                var dist = one.Distance(two);

                if (n == 0 || pQueue.Count < n)
                    pQueue.Enqueue((one, two), -dist);
                else
                    _ = pQueue.EnqueueDequeue((one, two), -dist);
            }
        }

        // Then flip around at the end
        var finalQueue = new PriorityQueue<(Point3D one, Point3D two), double>(pQueue.Count);
        while (pQueue.TryDequeue(out var pair, out var minDist))
            finalQueue.Enqueue(pair, -minDist);

        return finalQueue;
    }

    internal class Hall
    {
        private readonly Dictionary<Point3D, int> _circuitByPos;
        public Dictionary<int, List<Point3D>> PosByCircuit { get; }

        public void Connect(Point3D one, Point3D two)
        {
            var circuit1 = _circuitByPos[one];
            var circuit2 = _circuitByPos[two];

            if (circuit1 == circuit2)
                return;

            // Merge the circuits
            foreach (var pos in PosByCircuit[circuit2])
            {
                PosByCircuit[circuit1].Add(pos);
                _circuitByPos[pos] = circuit1;
            }

            PosByCircuit.Remove(circuit2);
        }

        public Hall(List<Point3D> positions)
        {
            _circuitByPos = positions.WithIndex().ToDictionary(pair => pair.item, pair => pair.index);
            PosByCircuit = positions.WithIndex().ToDictionary(pair => pair.index, pair => new List<Point3D> { pair.item });
        }

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