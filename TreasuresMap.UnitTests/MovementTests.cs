using TreasuresMap.Enums;
using TreasuresMap.Models;
using Xunit;

namespace TreasuresMap.UnitTests;

public sealed class MovementTests
{
    private readonly IMovable _movement = new Movement();

    [Theory]
    [InlineData("AA")]
    [InlineData("aa")]
    [InlineData("aA")]
    public void Move_ShouldMove(string actions)
    {
        var adventurer = new Adventurer("test", new Coordinate(1, 1), Orientation.E, actions);

        var map = new Map(new RequiredData
        {
            MapHeight = 4,
            MapWidth = 4,
            Adventurers = new List<IAdventurer> { adventurer }
        });

        Move(map, adventurer);

        // 
        Assert.Equal(new Coordinate(3, 1), adventurer.Coordinate);
    }

    [Fact]
    public void Move_ShouldIgnoreUnknownMovement()
    {
        var adventurer = new Adventurer("test", new Coordinate(1, 1), Orientation.E, "YYQKx");

        var map = new Map(new RequiredData
        {
            MapHeight = 4,
            MapWidth = 4,
            Adventurers = new List<IAdventurer> { adventurer }
        });

        Move(map, adventurer);

        // 
        Assert.Equal(new Coordinate(1, 1), adventurer.Coordinate);
        Assert.Equal(Orientation.E, adventurer.Orientation);
    }

    [Fact]
    public void Move_ShouldNotMoveOutSideTheMap()
    {
        var adventurer = new Adventurer("test", new Coordinate(1, 1), Orientation.E, "AAAAAA");

        var map = new Map(new RequiredData
        {
            MapHeight = 4,
            MapWidth = 4,
            Adventurers = new List<IAdventurer> { adventurer }
        });

        Move(map, adventurer);

        // 
        Assert.Equal(new Coordinate(3, 1), adventurer.Coordinate);
    }

    [Theory]
    [InlineData(Orientation.N, "G", Orientation.O)]
    [InlineData(Orientation.S, "D", Orientation.O)]
    [InlineData(Orientation.S, "GD", Orientation.S)]
    [InlineData(Orientation.E, "GG", Orientation.O)]
    [InlineData(Orientation.S, "DD", Orientation.N)]
    [InlineData(Orientation.O, "GGGD", Orientation.E)]
    public void Move_ShouldTurn(Orientation startingOrientation, string actions, Orientation result)
    {
        var adventurer = new Adventurer("test", new Coordinate(1, 1), startingOrientation, actions);

        var map = new Map(new RequiredData
        {
            MapHeight = 4,
            MapWidth = 4,
            Adventurers = new List<IAdventurer> { adventurer }
        });

        Move(map, adventurer);

        // 
        Assert.Equal(result, adventurer.Orientation);
    }

    [Fact]
    public void Move_ShouldMoveBasedOnOrientation()
    {
        var adventurer = new Adventurer("test", new Coordinate(1, 1), Orientation.N, "DAADADA");

        var map = new Map(new RequiredData
        {
            MapHeight = 4,
            MapWidth = 4,
            Adventurers = new List<IAdventurer> { adventurer }
        });

        Move(map, adventurer);

        // 
        Assert.Equal(new Coordinate(2, 2), adventurer.Coordinate);
        Assert.Equal(Orientation.O, adventurer.Orientation);
    }

    [Fact]
    public void Move_ShouldNotCrossMountain()
    {
        var adventurer = new Adventurer("test", new Coordinate(1, 1), Orientation.E, "AAA");

        var map = new Map(new RequiredData
        {
            MapHeight = 4,
            MapWidth = 4,
            Adventurers = new List<IAdventurer> { adventurer },
            Mountains = new List<Coordinate> { new(2, 1) }
        });

        Move(map, adventurer);

        // 
        Assert.Equal(new Coordinate(1, 1), adventurer.Coordinate);
    }

    [Theory]
    [InlineData("GGAGAADA", 1, 3)]
    [InlineData("GAGA", 0, 2)]
    public void Move_ShouldGoAroundMountain(string actions, int resultX, int resultY)
    {
        var adventurer = new Adventurer("test", new Coordinate(1, 1), Orientation.N, actions);

        var map = new Map(new RequiredData
        {
            MapHeight = 4,
            MapWidth = 4,
            Adventurers = new List<IAdventurer> { adventurer },
            Mountains = new List<Coordinate> { new(2, 2) }
        });

        Move(map, adventurer);

        // 
        Assert.Equal(new Coordinate(resultX, resultY), adventurer.Coordinate);
    }

    private void Move(IMap map, IAdventurer adventurer)
    {
        foreach (var action in adventurer.GetMovements())
        {
            adventurer.Move(map, _movement, action);
        }
    }
}