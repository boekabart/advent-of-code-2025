using shared;

namespace day7;

public static class D7P2
{
    public static object Part2Answer(this string input)
    {
        var map = input.ParseMap();
        var ways = map.Convert(c => c == 'S' ? 1L : 0);
        foreach (var pos in map.AllPositions().OrderBy(pos => pos.Y).Where(pos => pos.Y > 0))
        {
            var upLeft = pos.UpLeft();
            var up = pos.Up();
            var upRight = pos.UpRight();
            var waysToGetHere = 0L;
            if (map.Get(up)!='^')
                waysToGetHere += ways.Get(up);
            if (map.Contains(upLeft) && map.Get(upLeft) == '^')
                waysToGetHere += ways.Get(upLeft);
            if (map.Contains(upRight) && map.Get(upRight) == '^')
                waysToGetHere += ways.Get(upRight);
            ways.Set(pos, waysToGetHere);
        }

        //Console.WriteLine(ways.Dump());

        var maxY = map.AllPositions().MaxBy(pos => pos.Y)!.Y;
        return ways.AllPositions().Where(pos => pos.Y == maxY).Sum(pos => ways.Get(pos));
    }

    public static object Part2Answer_Poging1(this string input)
    {
        var map = input.ParseMap();
        var ways = map.Convert(c => c == 'S' ? 1L : 0);
        var startPos = map.FindAll('S').Single();
        HashSet<(Pos pos, string path)> todo = [];
        HashSet<(Pos pos, string path)> history = [];
        var start = (startPos, "");
        if (history.Add(start))
            todo.Add(start);
        List<string> paths = [];
        var dumpTime = DateTimeOffset.UtcNow;
        while (todo.Any())
        {
            var now = DateTimeOffset.UtcNow;
            var since = now - dumpTime;
            if (since.TotalSeconds > 5)
            {
                dumpTime = now;
                Console.WriteLine($"{paths.Count} paths, {todo.Count} todo, {history.Count} doneish");
            }
            var pair = todo.First();
            todo.Remove(pair);

            var down = pair.pos.Down();
            if (!map.Contains(down))
            {
                paths.Add(pair.path);
                continue;
            }

            var cel = map.Get(down);
            switch (cel)
            {   
                case '^':
                    var left = (down.Left(), pair.path + "L");
                    if (history.Add(left))
                        todo.Add(left);
                    var right = (down.Right(), pair.path + "R");
                    if (history.Add(right))
                        todo.Add(right);
                    break;
                case '.':   
                case '|':
                    map.Set(down, '|'); 
                    var moreDown = (down, pair.path);
                    if (history.Add(moreDown))
                        todo.Add(moreDown);
                    break;
                default:
                    Console.WriteLine($"Didn't expect cel {cel} at {down}");
                    break;
            }
        }
        Console.WriteLine(map.Dump());
        foreach (var d in history)
        {
            ways.Set(d.pos, 1 + ways.Get(d.pos));
        }

        Console.WriteLine(ways.Dump());

        return paths.Count;
    }

    public static string Dump(this Map<long> map)
    {
        return string.Join(Environment.NewLine, map.Grid.Select(l => string.Join(" ", l.Select(ll => ll.ToString("D3")))));
    }
}
