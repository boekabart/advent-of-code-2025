using FluentAssertions;
using Xunit;

namespace day7;

public class D7P2Tests
{
    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 40;
        Input.ExampleInput
            .Part2Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 73007003089792L;
        Input.RawInput
            .Part2Answer()
            .Should().Be(expected);
    }
}