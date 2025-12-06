using FluentAssertions;
using Xunit;

namespace day6;

public static class D6P1Tests
{
    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 4277556L;
        Input.ExampleInput
            .Part1Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 6957525317641L;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}
