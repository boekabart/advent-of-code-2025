using shared;

namespace day10;

internal record Machine(bool[] Lights, int[][] Wirings, int[] Joltages);


public static class D10P1
{
    public static object Part1Answer(this string input)
    {
        
        //foreach(var c in ButtonCombis(3).Take(50)) Console.WriteLine(string.Join(",",c));
        var machines = input.ParseThings();

        var minima = machines.Select(HowManyPresses);

        return minima.Sum();
    }

    internal static int HowManyPresses(this Machine machine)
    {
        var bas = machine.Lights.Length - 1;
        var reference = 0;
        foreach (var l in machine.Lights.WithIndex().Where(l => l.item))
            reference += 1 << (bas-l.index);

        foreach (var combi in ButtonCombis(machine.Wirings.Length))
        {
            var iter = 0;
            List<int> variants = [];
            foreach (var w in combi)
            {
                var wiring = machine.Wirings[w];
                foreach (var lightIndex in wiring)
                    iter ^= 1 << (bas-lightIndex);
                variants.Add(iter);
            }

            if (iter != reference)
                continue;
            /*
            Console.WriteLine(combi.Length.ToString()+"\t"+iter.ToString(machine)
                              + string.Join(",",combi)
                              + "     " + 0.ToString(machine)
                              + string.Join("> ", combi.WithIndex().Select(c => string.Join("+", machine.Wirings[c.item]) + variants[c.index].ToString(machine)))
                              );
            Console.WriteLine(combi.Length);*/
            return combi.Length;
        }
        Console.WriteLine("AIAIAI");
        return -1;
    }

    internal static string ToString(this int lights, Machine machine)
        => " [" + lights.ToString($"B{machine.Lights.Length}").Replace("0", ".").Replace("1", "#") + "] ";

    internal static IEnumerable<Machine> ParseThings(this string input) =>
        input
            .Lines()
            .Select(TryParseAsThing)
            .OfType<Machine>();

    internal static Machine? TryParseAsThing(this string line)
    {
        var parts = line.Split(['[', ']', '{', '}'], StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 3) return null;
        var lightsString = parts[0].Trim();
        var lights = lightsString.Select(c => c == '#').ToArray();
        var joltages = parts[2].Split([',', ' '], StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        var wiringParts = parts[1].Split(['(', ')', ' '], StringSplitOptions.RemoveEmptyEntries);
        var wirings = wiringParts
            .Select(w => w.Split([',', ' '], StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray())
            .ToArray();
        return new Machine(lights, wirings, joltages);
    }

    internal static IEnumerable<int[]> ButtonCombis(int numberOfWirings)
    {
        for (int n = 1; n<=160; n++)
        {
            var a = new int[n];
            while (true)
            {
                yield return a.ToArray();
                int nn = n - 1;
                while (nn >= 0)
                {
                    if (++a[nn] < numberOfWirings)
                    {
                        for (int nnn = nn + 1; nnn < n; nnn++)
                            a[nnn] = a[nn];
                        break;
                    }

                    a[nn] = 0;
                    nn--;
                }

                if (nn < 0)
                    break;
            }
        }

        throw new NotImplementedException();
    }

    internal static IEnumerable<int[]> ButtonCombis(int numberOfWirings, int numberOfPresses)
    {
        int n = numberOfPresses;
        var a = new int[n];
        while (true)
        {
            yield return a.ToArray();
            int nn = n - 1;
            while (nn >= 0)
            {
                if (++a[nn] < numberOfWirings)
                {
                    for (int nnn = nn + 1; nnn < n; nnn++)
                        a[nnn] = a[nn];
                    break;
                }

                a[nn] = 0;
                nn--;
            }

            if (nn < 0)
                break;
        }
    }

    internal static int GetResult(this IEnumerable<Machine> things) => things.Select(AsResult).Sum();
    internal static int AsResult(this Machine machine) => 0;
}