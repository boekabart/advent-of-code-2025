using FluentAssertions;
using Xunit;

namespace day10;

public static class D10P1Tests
{
    [Fact]
    internal static void ParseInputTest()
    {
        var things = Input.ExampleInput.ParseThings().ToArray();
        things.Should().HaveCount(3);

        var first = things[0];
        first.Lights.Should().Equal([false, true, true, false]);
        first.Wirings.Should().HaveCount(6);
        first.Wirings[1].Should().Equal([1, 3]);
        first.Joltages.Should().Equal([3, 5, 4, 7]);
    }

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 7;
        Input.ExampleInput
            .Part1Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 475;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}
