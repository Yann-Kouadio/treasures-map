using TreasuresMap.Enums;
using TreasuresMap.Models;
using Xunit;

namespace TreasuresMap.UnitTests;

public sealed class AdventurerTest
{
    private readonly IMovable _movement = new Movement();

    [Fact]
    public void TryGetTreasure_ShouldPass()
    {
        var adventurer = new Adventurer("TEST", new Coordinate(2, 1), Orientation.S, "A");

        var map = new Map(new RequiredData
        {
            MapHeight = 4,
            MapWidth = 4,
            Adventurers = new List<IAdventurer> { adventurer },
            Treasures = new List<ITreasure> { new Treasure { Coordinate = new Coordinate(2, 2), Quantity = 1 } }
        });

        adventurer.Move(map, _movement, 'A');
        adventurer.TryGetTreasure(map);

        Assert.Equal(1, adventurer.TreasuresCount);
    }
}