using shared;

namespace day7;

internal record Thing(bool Data);

public static class D7P1
{
    public static object Part1Answer(this string input)
    {
        var map = input.ParseMap();
        var startPos = map.FindAll('S').Single();
        long result = 0;
        Stack<Pos> todo = [];
        todo.Push(startPos);
        while (todo.TryPop(out var pos))
        {
            var down = pos.Down();
            if (!map.Contains(down))
                continue;
            var cel = map.Get(down);
            switch (cel)
            {
                case '^':
                    result++;
                    todo.Push(down.Left());
                    todo.Push(down.Right());
                    break;
                case '.':
                    map.Set(down, '|');
                    todo.Push(down);
                    break;
                case '|':
                    break;
                default:
                    Console.WriteLine($"Didn't expect cel {cel} at {down}");
                    break;
            }
        }
        //Console.WriteLine(map.Dump());

        return result;
    }
}