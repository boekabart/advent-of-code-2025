namespace day11;

public static class D11P2
{
    public static object Part2Answer(this string input)
    {
        var room = input.ParseThings().ToDictionary(t => t.A, t => t.B);

        var cache = new Dictionary<string, Dictionary<string, long>>();

        const string you = "svr";
        const string wp1 = "fft";
        const string wp2 = "dac";
        const string tgt = "out";

        (string Src, string Tgt)[][] todo =
        [
            [
                (you, wp1),
                (wp1, wp2),
                (wp2, tgt)
            ],
            [
                (you, wp2),
                (wp2, wp1),
                (wp2, tgt)
            ]
        ];
        return todo.Select(set =>
                set.AsParallel()
                    .Select(pair => pair.Src.NumberOfPathsTo(pair.Tgt, room, cache))
                    .Times())
            .Sum();
    }

    internal static long Times(this IEnumerable<long> values) =>
        values.Aggregate(1L, (a, b) => a * b);

    extension(string src)
    {
        internal long NumberOfPathsTo(string tgt, Dictionary<string, string[]> room,
            Dictionary<string, Dictionary<string, long>> caches)
        {
            Dictionary<string, long> cache;
            lock (caches)
            {
                if (!caches.ContainsKey(tgt))
                    caches.Add(tgt, []);
                cache = caches[tgt];
            }

            var a = src.NumberOfPathsTo(tgt, room, cache);
            Console.WriteLine($"{src} > {tgt} : {a}");
            return a;
        }

        internal long NumberOfPathsTo(string tgt, Dictionary<string, string[]> room, Dictionary<string, long> cache)
        {
            if (src == tgt)
                return 1;
            if (cache.TryGetValue(src, out var value))
                return value;

            var total = room
                .TryGetValue(src, out var routes)
                ? routes.Sum(t => t.NumberOfPathsTo(tgt, room, cache))
                : 0;
            cache[src] = total;
            return total;
        }
    }
}
