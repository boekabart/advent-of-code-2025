using FluentAssertions;
using Xunit;

namespace day9;

public static class D9P1Tests
{

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 50;
        Input.ExampleInput
            .Part1Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 4781235324L;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}
