using FluentAssertions;
using Xunit;

namespace day7;

public static class D7P1Tests
{
    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 21;
        Input.ExampleInput
            .Part1Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 1581;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}
