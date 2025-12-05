using FluentAssertions;
using Xunit;

namespace day5;

public static class D5P1Tests
{
    [Fact]
    internal static void ParseInputTest()
    {
        var things = Input.ExampleInput.ParseRanges().ToArray();
        things.Should().HaveCount(4);
        var ids = Input.ExampleInput.ParseIds().ToArray();
        ids.Should().HaveCount(6);
    }

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 3;
        Input.ExampleInput
            .Part1Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 690;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}
