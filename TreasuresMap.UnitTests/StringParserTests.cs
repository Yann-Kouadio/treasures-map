using TreasuresMap.Enums;
using TreasuresMap.Models;
using Xunit;

namespace TreasuresMap.UnitTests;

public sealed class StringParserTests
{
    private readonly IDataParser _parser = new StringParser();

    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(3, 3)]
    [InlineData(4, 4)]
    public void Parse_ShouldParse(int testNumber, int resultNumber)
    {
        var actual = new RequiredData();
        _parser.Parse(ShouldParseTestData(testNumber), actual);

        Assert.Equivalent(ShouldParseResultData(resultNumber), actual, true);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public void Parse_ShouldFailed(int testNumber)
    {
        var actual = new RequiredData();

        Assert.Throws<ArgumentException>(() => _parser.Parse(ShouldFailedTestData(testNumber), actual));
    }

    private static string ShouldParseTestData(int numb)
    {
        var result = numb switch
        {
            1 => "C - 3 - 4",
            2 => "M - 1 - 0",
            3 => "T - 0 - 3 - 2",
            4 => "A - Lara - 1 - 1 - S - AADADAGGA",
            _ => ""
        };

        return result;
    }

    private static IRequiredData ShouldParseResultData(int numb)
    {
        var result = new RequiredData();

        switch (numb)
        {
            case 1:
                result.MapWidth = 3;
                result.MapHeight = 4;
                break;
            case 2:
                result.Mountains.Add(new Coordinate(1, 0));
                break;
            case 3:
                result.Treasures.Add(new Treasure
                {
                    Coordinate = new Coordinate(0, 3),
                    Quantity = 2
                });
                break;
            case 4:
                result.Adventurers.Add(new Adventurer("Lara", new Coordinate(1, 1), Orientation.S, "AADADAGGA"));
                break;
        }

        return result;
    }

    private static string ShouldFailedTestData(int numb)
    {
        var result = numb switch
        {
            1 => "C - 3",
            2 => "M - 1",
            3 => "T - 0 - 3",
            4 => "A - Lara - 1 - 1 - S",
            _ => ""
        };

        return result;
    }
}