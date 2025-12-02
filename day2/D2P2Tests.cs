using FluentAssertions;
using Xunit;

namespace day2;

public class D2P2Tests
{
    [InlineData(222, false)]
    [InlineData(11, false)]
    [InlineData(123123, false)]
    [InlineData(123123123, false)]
    [InlineData(13131313, false)]
    [InlineData(131313131, true)]
    [Theory]
    internal static void ParseInputTest(long number, bool expected)
    {
        number.IsValidId2().Should().Be(expected);
    }

    [Fact]
    internal static void AcceptanceTest()
    {
        var expected = 4174379265L;
        Input.ExampleInput
            .Part2Answer()
            .Should().Be(expected);
    }

    [Fact]
    internal static void RegressionTest()
    {
        var expected = 43287141963L;
        Input.RawInput
            .Part2Answer()
            .Should().Be(expected);
    }
}