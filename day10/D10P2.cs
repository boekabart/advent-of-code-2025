using shared;

namespace day10;

public static class D10P2
{
    public static object Part2Answer(this string input)
    {
        var machines = input.ParseThings();

        var minima = machines.Select(HowManyPresses2);

        return minima.Sum();
    }

    internal static int HowManyPresses2(this Machine machine)
    {
        List<List<Dictionary<int, int>>> rr = [];
        List<HashSet<int>> relavantButtonIndices = [];
        for (int joltNo = 0; joltNo < machine.Joltages.Length; joltNo++)
            relavantButtonIndices.Add(machine.RelevantButtonIndices(joltNo).ToHashSet());

        for (int n = machine.Joltages.Min();; n++)
        {
            foreach (var co in D10P1.ButtonCombis(machine.Wirings.Length, n))
            {
                var ok = true;
                foreach (var pair in relavantButtonIndices.WithIndex())
                {
                    var rbi = pair.item;
                    var joltage = machine.Joltages[pair.index];
                    if (co.Count(rbi.Contains) != joltage)
                    {
                        ok = false;
                        break;
                    }
                }

                if (ok)
                {
                    Console.WriteLine(n);
                    return n;
                }
            }
        }
    }

    internal static int HowManyPresses2a(this Machine machine)
    {
        List<List<Dictionary<int, int>>> rr = [];
        for (int joltNo = 0; joltNo < machine.Joltages.Length; joltNo++)
        {
            var relavantButtonIndices = machine.RelevantButtonIndices(joltNo).ToArray();
            var p4j = machine.WhichPresses(joltNo)
                .Select(combi => relavantButtonIndices.ToDictionary(r => r, r => combi.Count(c => c == r))).ToList();
            rr.Add(p4j);
        }
        Console.WriteLine("Got the options");
        foreach (var r in rr)
        {
            Console.WriteLine(
                $"{r.Count} | ex: "+
                string.Join(" ; ", r[0].OrderBy(p => p.Key).Select(p => $"{p.Value} x {p.Key}")));

        }
        int maxSum = int.MaxValue;
        Dictionary<int, int>? bestCombi = null;

        foreach (var combi in rr.Combis())
        {
            Dictionary<int, int> neededButtons = [];
            int sum = 0;
            var ok = true;
            for (int j = 0; j < combi.Length; j++)
            {
                var c = combi[j];
                var forThis = rr[j][c];
                foreach (var n in neededButtons)
                {
                    if (forThis.TryGetValue(n.Key, out var v) && v != n.Value)
                    {
                        ok = false;
                        break;
                    }
                }

                if (!ok)
                    break;

                foreach (var n in forThis)
                {
                    if (neededButtons.TryGetValue(n.Key, out var v))
                    {
                        if (v != n.Value)
                        {
                            ok = false;
                            break;
                        }
                    }
                    else
                    {
                        neededButtons[n.Key] = n.Value;
                        sum += n.Value;
                        if (sum >= maxSum)
                        {
                            ok = false;
                            break;
                        }
                    }
                }

                if (!ok)
                    break;
            }
            if (!ok)
                continue;

            bestCombi = neededButtons;
            maxSum = sum;
            Console.WriteLine(
                $"~ {maxSum}: " +
                string.Join(" ; ", bestCombi.OrderBy(p => p.Key).Select(p => $"{p.Value} x {p.Key}")));
        }

        Console.WriteLine(
            $"! {maxSum}: "+
            string.Join(" ; ",bestCombi.OrderBy(p => p.Key).Select(p => $"{p.Value} x {p.Key}")));
        return maxSum;
    }

    internal static IEnumerable<int[]> Combis<T>(this List<List<T>> lol)
    {
        int n = lol.Count;
        var a = new int[n];
        while (true)
        {
            yield return a.ToArray();
            int nn = n - 1;
            while (nn >= 0)
            {
                if (++a[nn] < lol[nn].Count)
                    break;

                a[nn] = 0;
                nn--;
            }

            if (nn < 0)
                break;
        }
    }

    internal static IEnumerable<int> RelevantButtonIndices(this Machine machine, int joltNo)
        => machine.Wirings.WithIndex().Where(arr => arr.item.Any(v => v == joltNo)).Select(pair => pair.index);

    internal static IEnumerable<int[]> WhichPresses(this Machine machine, int joltNo)
    {
        var reference = machine.Joltages[joltNo];
        var relevantWirings = machine.RelevantButtonIndices(joltNo).ToArray();

        foreach (var combi in D10P1.ButtonCombis(relevantWirings.Length, reference))
        {
            var iter = 0;
            foreach (var w in combi)
            {
                var wiring = machine.Wirings[relevantWirings[w]];
                foreach (var lightIndex in wiring)
                    if (lightIndex == joltNo)
                        iter++;
            }

            if (iter != reference)
                continue;

            yield return combi.Select(w => relevantWirings[w]).ToArray();
        }
    }

}
