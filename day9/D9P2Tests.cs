using FluentAssertions;
using Xunit;

namespace day9;

public class D9P2Tests
{
    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 24;
        Input.ExampleInput
            .Part2Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 1566935900L;
        Input.RawInput
            .Part2Answer()
            .Should().Be(expected);
    }
}