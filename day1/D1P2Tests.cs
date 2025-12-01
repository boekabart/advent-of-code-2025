using FluentAssertions;
using Xunit;

namespace day1;

public class D1P2Tests
{
    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 31;
        Input.ExampleInput
            .Part2Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 17191599;
        Input.RawInput
            .Part2Answer()
            .Should().Be(expected);
    }
}