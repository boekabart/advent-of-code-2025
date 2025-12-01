using FluentAssertions;
using Xunit;

namespace day1;

public static class D1P1Tests
{
    [Fact]
    internal static void ParseInputLineTest()
    {
        "R4".TryParseAsThing().Should().Be(4);
        "L99".TryParseAsThing().Should().Be(-99);
        "L0".TryParseAsThing().Should().Be(0);
        "".TryParseAsThing().Should().BeNull();
        "  ".TryParseAsThing().Should().BeNull();
    }

    [Fact]
    internal static void ParseInputTest()
    {
        var things = Input.ExampleInput.ParseThings().ToArray();
        things.Should().HaveCount(10);
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
        var expected = 999;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}
