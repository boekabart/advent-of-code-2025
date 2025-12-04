using shared;

namespace day4;

internal record Thing(bool Data);

public static class D4P1
{
    public static object Part1Answer(this string input) =>
        input.ParseMap().CountMoveableRolls();

    internal static int CountMoveableRolls(this Map map) =>
        map.GetMoveableRolls().Count();
    internal static IEnumerable<Pos> GetMoveableRolls(this Map map) =>
        map
            .AllPositions()
            .Where(p => map.Get(p)=='@')
            .Where(p => p.CountRollsAround(map) < 4)
 /*           .ToList()
            .Select(p => map.Set(p,'x'))
            .Select(a => { Console.WriteLine(map.Dump());
                Console.WriteLine();
                return a;
            })*/;
    internal static int CountRollsAround(this Pos pos, Map map) => 
        pos
            .EightAround()
            .Where(map.Contains)
            .Where(p => map.Get(p) != '.')
            .Count();
}