using FluentAssertions;
using Xunit;

namespace day5;

public class D5P2Tests
{
    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 14;
        Input.ExampleInput
            .Part2Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 344323629240733L;
        Input.RawInput
            .Part2Answer()
            .Should().Be(expected);
    }
}