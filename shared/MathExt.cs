using FluentAssertions;
using Xunit;

namespace shared;

public static class MathExt
{
    public static int GGD(this int a, int b)
    {
        // Euclidisch algoritme
        while (b != 0)
        {
            int temp = a % b;
            a = b;
            b = temp;
        }
        return a;
    }

    [Fact]
    internal static void TestGGD()
    {
        0.GGD(10).Should().Be(10);
        10.GGD(0).Should().Be(10);

        10.GGD(10).Should().Be(10);

        100.GGD(10).Should().Be(10);
        10.GGD(100).Should().Be(10);

        42.GGD(35).Should().Be(7);
        35.GGD(42).Should().Be(7);
    }

    public static long GGD(this long a, long b)
    {
        // Euclidisch algoritme
        while (b != 0)
        {
            long temp = a % b;
            a = b;
            b = temp;
        }
        return a;
    }
}