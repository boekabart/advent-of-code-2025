using System.Drawing;
using static day9.D9P1;

namespace day9;

//internal record Line(Point3D A, Point3D B);

internal record Rect(long MinX, long MinY, long MaxX, long MaxY);
public static class D9P2
{
    public static object Part2Answer(this string input)
    {
        bool debug = input.Length < 1000;
        var positions = input.ParsePoints().ToList();
        var lines = positions.AsLines().ToList();
        var outsidePoints = lines.Select(ShiftOutside).SelectMany(Points).ToHashSet();
        foreach (var pointOnLine in lines.SelectMany(Points))
            outsidePoints.Remove(pointOnLine);

        if (debug)
        {
            foreach (var p in outsidePoints)
                Console.WriteLine(p);

            Console.WriteLine("---");
        }

        var pQueue = positions.QueueByDistance();
        while (pQueue.TryDequeue(out var pair, out var d))
        {
            var inverse = (one: new Point3D(pair.one.X, pair.two.Y),
                two: new Point3D(pair.two.X, pair.one.Y));
            Point3D[] rectPoints = [pair.one, inverse.one, pair.two, inverse.two];
            var rectLines = rectPoints.AsLines();
            var rectOutlinePoints = rectLines.SelectMany(Points);

            if (rectOutlinePoints.Any(outsidePoints.Contains))
            {
                if (debug)
                {
                    var conflict = rectOutlinePoints.First(outsidePoints.Contains);
                    Console.WriteLine($"Rect {d} {pair} touched empty air at {conflict}");
                }

                continue;
            }

            Console.WriteLine(pair);
            return pair.one.SquareSize(pair.two);
        }

        throw new NotImplementedException();
    }

    internal static IEnumerable<(Point3D A, Point3D B)> AsLines(this IList<Point3D> points)
        => points.Select((p, i) => (p, points[(i + 1) % points.Count]));

    internal static Rect AsRect(this (Point3D A, Point3D B) rect)
    {
        var minX = Math.Min(rect.A.X, rect.B.X);
        var minY = Math.Min(rect.A.Y, rect.B.Y);
        var maxX = Math.Max(rect.A.X, rect.B.X);
        var maxY = Math.Max(rect.A.Y, rect.B.Y);
        return new Rect(minX, minY, maxX, maxY);
    }

    internal static IEnumerable<Point3D> Points(this (Point3D A, Point3D B) line)
    {
        var r = line.AsRect();
        for (var x = r.MinX; x <= r.MaxX; x++)
        for (var y = r.MinY; y <= r.MaxY; y++)
            yield return new Point3D(x, y);
    }

    internal static (Point3D A, Point3D B) ShiftOutside(this (Point3D A, Point3D B) line)
    {
        var (dX, dY) = line.A.X > line.B.X /* going left */ ? (0, 1) :
            line.A.X < line.B.X /* going right */ ? (0, -1) :
            line.A.Y > line.B.Y /* going up */ ? (-1, 0) :
            line.A.Y < line.B.Y /* going down */ ? (1, 0) :
            throw new InvalidOperationException();
        return new(new(line.A.X + dX, line.A.Y + dY),
            new(line.B.X + dX, line.B.Y + dY));
    }

    internal static bool RectContains(Rect rect, Point3D point)
    {
        return point.X > rect.MinX && point.Y > rect.MinY &&
               point.X < rect.MaxX && point.Y < rect.MaxY;
    }

}
