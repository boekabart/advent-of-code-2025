namespace day8;

public static class D8P2
{
    public static object Part2Answer(this string input)
    {
        var positions = input.ParsePoints().ToList();
        var pQueue = positions.QueueByDistance();

        var hall = new D8P1.Hall(positions);
        while (pQueue.TryDequeue(out var pair, out _))
        {
            hall.Connect(pair.one, pair.two);
            //Console.WriteLine($"{pair} @ {pair.one.Distance(pair.two)} - {hall.PosByCircuit.Count}");

            if (hall.PosByCircuit.Count == 1)
                return pair.one.X * pair.two.X;
        }

        throw new Exception("Did not expect that connecting all pairs would lead to more than one circuit");
    }
}
