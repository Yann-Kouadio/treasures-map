using TreasuresMap.Enums;
using TreasuresMap.Models;
using Xunit;

namespace TreasuresMap.UnitTests;

public sealed class MapTest
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(10)]
    [InlineData(11)]
    [InlineData(12)]
    [InlineData(13)]
    public void Constructor_ShouldInvalidDataFailed(int testNumber)
    {
        Assert.Throws<ArgumentException>(() => InitializationTest(testNumber));
    }

    [Theory]
    [InlineData(0, 1, false)]
    [InlineData(1, 1, true)]
    public void CoordinateOnMountain_ShouldPass(int coordinateX, int coordinateY, bool result)
    {
        var map = new Map(new RequiredData
        {
            MapWidth = 2,
            MapHeight = 2,
            Mountains = new List<Coordinate> { new(1, 1) }
        });

        Assert.Equal(result, map.CoordinateOnMountain(coordinateX, coordinateY));
    }

    [Theory]
    [InlineData(1, 1, 2, true, 1)]
    [InlineData(1, 1, 1, true, 0)]
    [InlineData(0, 1, 4, false, 4)]
    public void CollectTreasure_ShouldPass(int coordinateX, int coordinateY, int quantity, bool collectResult,
        int quantityResult)
    {
        var map = new Map(new RequiredData
        {
            MapWidth = 2,
            MapHeight = 2,
            Treasures = new List<ITreasure> { new Treasure { Coordinate = new Coordinate(1, 1), Quantity = quantity } }
        });

        Assert.Equal(collectResult, map.CollectTreasure(new Coordinate(coordinateX, coordinateY)));
        Assert.Equal(quantityResult, map.GetTreasures()[(1, 1)]);
    }

    private static IMap InitializationTest(int testNumber)
    {
        RequiredData? data = null;
        switch (testNumber)
        {
            case 1:
                data = new RequiredData { MapWidth = -1 };
                break;
            case 2:
                data = new RequiredData { MapHeight = -1 };
                break;
            case 3:
                data = new RequiredData { MapWidth = 0 };
                break;
            case 4:
                data = new RequiredData { MapHeight = 0 };
                break;
            case 5:
                data = new RequiredData { MapHeight = int.MaxValue };
                break;
            case 6:
                data = new RequiredData { MapWidth = int.MaxValue };
                break;

            // Invalid coordinate
            case 7:
                data = new RequiredData
                {
                    MapHeight = 1,
                    MapWidth = 1,
                    Mountains = new List<Coordinate> { new(2, 2) }
                };
                break;
            case 8:
                data = new RequiredData
                {
                    MapHeight = 1,
                    MapWidth = 1,
                    Treasures = new List<ITreasure> { new Treasure { Coordinate = new Coordinate(2, 2), Quantity = 0 } }
                };
                break;
            case 9:
                data = new RequiredData
                {
                    MapHeight = 1,
                    MapWidth = 1,
                    Adventurers = new List<IAdventurer>
                        { new Adventurer("TEST", new Coordinate(2, 2), Orientation.E, "A") }
                };
                break;

            // Cannot used same coordinate
            case 10:
                data = new RequiredData
                {
                    MapHeight = 1,
                    MapWidth = 1,
                    Mountains = new List<Coordinate> { new(1, 1) },
                    Treasures =
                        new List<ITreasure> { new Treasure { Coordinate = new Coordinate(1, 1), Quantity = 0 } },
                    Adventurers = new List<IAdventurer>
                        { new Adventurer("TEST", new Coordinate(1, 1), Orientation.E, "A") }
                };
                break;

            // Invalid treasure quantity
            case 11:
                data = new RequiredData
                {
                    MapHeight = 1,
                    MapWidth = 1,
                    Treasures = new List<ITreasure>
                        { new Treasure { Coordinate = new Coordinate(1, 1), Quantity = -1 } }
                };
                break;
            case 12:
                data = new RequiredData
                {
                    MapHeight = 1,
                    MapWidth = 1,
                    Treasures = new List<ITreasure> { new Treasure { Coordinate = new Coordinate(1, 1), Quantity = 0 } }
                };
                break;
            case 13:
                data = new RequiredData
                {
                    MapHeight = 1,
                    MapWidth = 1,
                    Treasures = new List<ITreasure>
                        { new Treasure { Coordinate = new Coordinate(2, 2), Quantity = int.MaxValue } }
                };
                break;
        }

        return
            data is not null
                ? new Map(data)
                : throw new Exception("test number out of range");
    }
}