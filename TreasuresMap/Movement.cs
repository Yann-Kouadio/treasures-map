using TreasuresMap.Enums;
using TreasuresMap.Models;

namespace TreasuresMap;

public class Movement : IMovable
{
    public bool Move(IMap map, IAdventurer adventurer, char action)
    {
        switch (action)
        {
            case 'A':
                return TryToMove(map, adventurer);
            case 'G':
                adventurer.UpdateOrientation((Orientation)(((int)adventurer.Orientation + 3) % 4));
                return false;
            case 'D':
                adventurer.UpdateOrientation((Orientation)(((int)adventurer.Orientation + 1) % 4));
                return false;
            default:
                return false;
        }
    }

    private bool TryToMove(IMap map, IAdventurer adventurer)
    {
        return adventurer.Orientation switch
        {
            Orientation.N => ValidMovement(adventurer.Coordinate.X, adventurer.Coordinate.Y - 1, map, adventurer),
            Orientation.E => ValidMovement(adventurer.Coordinate.X + 1, adventurer.Coordinate.Y, map, adventurer),
            Orientation.S => ValidMovement(adventurer.Coordinate.X, adventurer.Coordinate.Y + 1, map, adventurer),
            Orientation.O => ValidMovement(adventurer.Coordinate.X - 1, adventurer.Coordinate.Y, map, adventurer),
            _ => false
        };
    }

    private bool ValidMovement(int x, int y, IMap map, IAdventurer adventurer)
    {
        if (!map.IsCoordinateOnMap(x, y))
            return false;

        var isMountain = map.CoordinateOnMountain(x, y);

        if (isMountain)
            return false;

        adventurer.UpdateCoordinate(new Coordinate(x, y));

        return true;
    }
}