namespace shared;

public static class MapExtensions
{
    public static Pos Left(this Pos pos) => pos with { X = pos.X - 1 };
    public static Pos Up(this Pos pos) => pos with { Y = pos.Y - 1 };
    public static Pos Right(this Pos pos) => pos with { X = pos.X + 1 };
    public static Pos Down(this Pos pos) => pos with { Y = pos.Y + 1 };

    public static IEnumerable<Pos> FourAround(this Pos pos)
    {
        yield return pos.Left();
        yield return pos.Right();
        yield return pos.Up();
        yield return pos.Down();
    }

    public static IEnumerable<Pos> WithManhattanDistance(this Pos pos, int minDist, int maxDist = 0)
    {
        if (maxDist == 0) maxDist = minDist;
        for (int dy = -maxDist; dy <= maxDist; dy++)
        {
            var absY = Math.Abs(dy);
            for (int dx = -maxDist; dx <= maxDist; dx++)
            {
                var dist = Math.Abs(dx) + absY;
                if (dist >maxDist || dist < minDist)
                    continue;
                yield return new Pos(pos.X + dx, pos.Y + dy);
            }
        }
    }

    public static Map ParseMap(this string input) =>
        input
            .NotEmptyTrimmedLines()
            .AsMap();

    public static Map AsMap(this IEnumerable<string> lines)
    {
        var grid = lines.Select(line => line.ToArray()).ToArray();
        return new Map(grid);
    }

    public static Map<T2> Convert<T1, T2>(this Map<T1> map, Func<T1, T2> mapper) =>
        new(map.Grid.Select(line => line.Select(mapper).ToArray()).ToArray());

    public static Map<T2> StretchConvert<T1, T2>(this Map<T1> map, Func<T1, T2[]> mapper) =>
        new(map.Grid.Select(line => line.SelectMany(mapper).ToArray()).ToArray());

    public static T Get<T>(this Map<T> map, Pos pos) => map.Grid[pos.Y][pos.X];
    public static T? TryGet<T>(this Map<T> map, Pos pos) where T : class => map.Contains(pos) ? map.Get(pos) : null;

    public static T Set<T>(this Map<T> map, Pos pos, T newValue) => map.Grid[pos.Y][pos.X] = newValue;

    public static T? GetOr<T>(this Map<T> map, Pos pos, T? def) where T : struct =>
        map.Contains(pos) ? map.Get(pos) : def;

    public static bool Contains<T>(this Map<T> map, Pos pos) =>
        pos.X >= 0 && pos.X < map.Grid[0].Length && pos.Y >= 0 && pos.Y < map.Grid.Length;

    public static IEnumerable<Pos> AllPositions<T>(this Map<T> map)
    {
        for (int y = 0; y < map.Grid.Length; y++)
        for (int x = 0; x < map.Grid[y].Length; x++)
            yield return new Pos(x, y);
    }

    public static IEnumerable<Pos> FindAll<T>(this Map<T> map, T q)
    {
        for (int y = 0; y < map.Grid.Length; y++)
        for (int x = 0; x < map.Grid[y].Length; x++)
            if (q.Equals(map.Grid[y][x]))
                yield return new Pos(x, y);
    }

    public static string Dump(this Map map)
    {
        return string.Join(Environment.NewLine, map.Grid.Select(l => string.Join("", l)));
    }

}

public static class Dir
{
    public static readonly Pos Up = new Pos(0, -1);
    public static readonly Pos Down = new Pos(0, 1);
    public static readonly Pos Left = new Pos(-1, 0);
    public static readonly Pos Right = new Pos(1, 0);


    private static List<Pos> dirs = [Up, Right, Down, Left];
    public static Pos RotateRight(this Pos dir) => dirs[(dirs.IndexOf(dir) + 1) % dirs.Count];
    public static Pos RotateLeft(this Pos dir) => dirs[(dirs.Count + (dirs.IndexOf(dir) - 1) % dirs.Count) % dirs.Count];
    public static Pos Add(this Pos pos, Pos delta) => new(pos.X + delta.X, pos.Y + delta.Y);
    public static Pos Subtract(this Pos pos, Pos delta) => new(pos.X - delta.X, pos.Y - delta.Y);
    public static Pos Sign(this Pos pos) => new(Math.Sign(pos.X), Math.Sign(pos.Y));
    public static int ManhattanLen(this Pos pos) => Math.Abs(pos.X) + Math.Abs(pos.Y);
    public static int ManhattanDist(this Pos pos, Pos other) => Math.Abs(pos.X-other.X) + Math.Abs(pos.Y-other.Y);

    public static IEnumerable<Pos> Decompose(this Pos pos)
    {
        if (pos.X != 0) yield return pos with { Y = 0 };
        if (pos.Y != 0) yield return pos with { X = 0 };
    }
    public static Pos Times(this Pos pos, int mult) => new(pos.X * mult, pos.Y * mult);
    public static Pos Mod(this Pos pos, Pos mod) => new(pos.X % mod.X, pos.Y %mod.Y);
    public static Pos AbsMod(this Pos pos, Pos mod) => new(((pos.X % mod.X) + mod.X) % mod.X, ((pos.Y % mod.Y)+mod.Y )% mod.Y);
}

public record Map<T>(T[][] Grid)
{
    public Map(int sizeX, int sizeY) : this(Enumerable.Range(0, sizeY).Select(_ => new T[sizeX]).ToArray())
    {
    }
}

public record Map(char[][] Grid) : Map<char>(Grid);

public record Pos(int X, int Y);
