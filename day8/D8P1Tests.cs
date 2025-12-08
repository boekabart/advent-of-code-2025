using FluentAssertions;
using Xunit;

namespace day8;

public static class D8P1Tests
{
    [Fact]
    internal static void ParseInputTest()
    {
        var things = Input.ExampleInput.ParsePoints().ToArray();
        things.Should().HaveCount(20);
    }

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 40;
        Input.ExampleInput
            .Part1Answer(10)
            .Should().Be(expected);
    }

    [Fact(Skip = "Done")]
    internal static void RegressionTest()
    {
        var expected = 90036L;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}
