using FluentAssertions;
using Xunit;

namespace day4;

public class D4P2Tests
{
    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 43;
        Input.ExampleInput
            .Part2Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 8409;
        Input.RawInput
            .Part2Answer()
            .Should().Be(expected);
    }
}