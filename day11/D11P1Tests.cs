using FluentAssertions;
using Xunit;

namespace day11;

public static class D11P1Tests
{
    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 5;
        Input.ExampleInput
            .Part1Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 494;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}
