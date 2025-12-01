namespace day1;

public static class D1P2
{
    public static object Part2Answer(this string input)
    {
//        input = Input.ExampleInput;
        var lists = D1P1.ParseThings(input).ToList();
        return lists.GetResult2();
    }

    internal static int GetResult2(this IEnumerable<int> things) => things.Aggregate((1_000_000_050, 0), (prev, delta) =>
    {
        var q = (prev.Item1 + delta);
        int a = q / 100;
        int b = prev.Item1 / 100;
        var nuls = Math.Abs( a-b);
        return (q, prev.Item2 + nuls);
    }).Item2;
}

