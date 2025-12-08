using FluentAssertions;
using Xunit;

namespace day8;

public class D8P2Tests
{
    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 25272;
        Input.ExampleInput
            .Part2Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 6083499488L;
        Input.RawInput
            .Part2Answer()
            .Should().Be(expected);
    }
}