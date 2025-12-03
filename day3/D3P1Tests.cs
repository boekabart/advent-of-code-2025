using FluentAssertions;
using Xunit;

namespace day3;

public static class D3P1Tests
{
   

    [Fact]
    internal static void ParseInputTest()
    {
        var things = Input.ExampleInput.ParseThings().ToArray();
        things.Should().HaveCount(4);
    }

    [Fact]
    internal static void AsResultTest()
    {
        "234234234234278".TryParseAsThing()!.AsResult().Should().Be(78);
    }
    [Fact]
    internal static void AsResultCornerCaseTest()
    {
        "2".TryParseAsThing()!.AsResult().Should().Be(2);
    }

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 357;
        Input.ExampleInput
            .Part1Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 17095;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}
