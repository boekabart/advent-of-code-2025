using FluentAssertions;
using Xunit;

namespace day2;

public static class D2P1Tests
{
    [Fact]
    internal static void ParseTests()
    {
        ParseInputLineTest("998-1012", new Range(998, 1012));
        ParseInputLineTest("2121212118-2121212124", new Range(2121212118, 2121212124));
    }

    internal static void ParseInputLineTest(string line, Range? expectedThing)
    {
        var actualThing = line.TryParseAsThing();
        actualThing.Should().Be(expectedThing);
    }

    [Fact]
    internal static void ParseInputTest()
    {
        var things = Input.ExampleInput.ParseThings().ToArray();
        things.Should().HaveCount(11);
    }
    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 1227775554L;
        Input.ExampleInput
            .Part1Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 34826702005L;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}
