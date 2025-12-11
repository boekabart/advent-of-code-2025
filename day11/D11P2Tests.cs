using FluentAssertions;
using Xunit;

namespace day11;

public class D11P2Tests
{
    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 2;
        Input.ExampleInput2
            .Part2Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 296006754704850L;
        Input.RawInput
            .Part2Answer()
            .Should().Be(expected);
    }
}