using FluentAssertions;
using Xunit;

namespace day10;

public static class D10P1Tests
{
    [InlineData("",null)]
    [Theory]
    internal static void ParseInputLineTest(string line, Thing? expectedThing)
    {
        var actualThing = line.TryParseAsThing();
        actualThing.Should().Be(expectedThing);
    }

    [Fact]
    internal static void ParseInputTest()
    {
        var things = Input.ExampleInput.ParseThings().ToArray();
        things.Should().HaveCount(0);
    }

    [Fact(Skip="ToDo")]
    internal static void AcceptanceTest()
    {
        var expected = 42;
        Input.ExampleInput
            .Part1Answer()
            .Should().Be(expected);
    }

    [Fact(Skip = "ToDo")]
    internal static void RegressionTest()
    {
        var expected = 42;
        Input.RawInput
            .Part1Answer()
            .Should().Be(expected);
    }
}
