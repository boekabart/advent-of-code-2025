using shared;

namespace day4;

public static class D4P2
{
    public static object Part2Answer(this string input)
    {
        var map = input.ParseMap();
        var tally = 0;
        while (true)
        {
            var movableRolls = map.GetMoveableRolls().ToList();
            if (!movableRolls.Any())
                break;
            tally += movableRolls.Count;
            foreach (var pos in movableRolls)
                map.Set(pos, '.');
        }

        return tally;
    }
}
