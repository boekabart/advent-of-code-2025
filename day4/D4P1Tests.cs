using FluentAssertions;
using Xunit;

namespace day4;

public static class D4P1Tests
{

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 13;
        Input.ExampleInput
            .Part1Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 1464;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}
