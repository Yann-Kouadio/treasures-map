using TreasuresMap.Enums;
using TreasuresMap.Models;

namespace TreasuresMap;

/// <summary>
///     Used for instantiation
/// </summary>
public static class Factory
{
    public static IMap CreateMap()
    {
        var data = LoadData();

        return new Map(data);
    }

    public static IDataParser CreateParser()
    {
        return new StringParser();
    }

    public static IRequiredData CreateRequiredData()
    {
        return new RequiredData();
    }

    public static ITreasure CreateTreasure(int x, int y, int quantity)
    {
        return new Treasure
        {
            Coordinate = new Coordinate(x, y),
            Quantity = quantity
        };
    }

    public static IAdventurer CreateAdventurer(string name, int x, int y, Orientation orientation, string movement)
    {
        return new Adventurer(name, new Coordinate(x, y), orientation, movement);
    }

    public static void Play(IMap map, IAdventurer adventurer, IMovable movement)
    {
        new PlayTurn(map, adventurer, movement).Play();
    }

    public static IMovable CreateMovement()
    {
        return new Movement();
    }

    public static void DisplayResult(IMap map)
    {
        new FileWriter(map).WriteResult();
    }

    private static IRequiredData LoadData()
    {
        return new FileReader().GetData();
    }
}