using FluentAssertions;
using Xunit;

namespace day3;

public class D3P2Tests
{
    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 3121910778619L;
        Input.ExampleInput
            .Part2Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 168794698570517L;
        Input.RawInput
            .Part2Answer()
            .Should().Be(expected);
    }
}