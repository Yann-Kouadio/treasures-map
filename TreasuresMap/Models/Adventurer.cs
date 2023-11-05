using TreasuresMap.Enums;

namespace TreasuresMap.Models;

public sealed class Adventurer : IAdventurer
{
    public Adventurer(string name, Coordinate coordinate, Orientation orientation, string movement)
    {
        Name = name;
        Coordinate = coordinate;
        Orientation = orientation;
        Movement = movement;
    }

    private string Movement { get; }
    public string Name { get; }

    public Coordinate Coordinate { get; private set; }

    public Orientation Orientation { get; private set; }

    public int TreasuresCount { get; private set; }

    public char[] GetMovements()
    {
        return Movement.ToUpperInvariant().ToCharArray();
    }

    public void UpdateOrientation(Orientation orientation)
    {
        Orientation = orientation;
    }

    public void UpdateCoordinate(Coordinate coordinate)
    {
        Coordinate = coordinate;
    }

    public bool Move(IMap map, IMovable movable, char action)
    {
        return movable.Move(map, this, action);
    }

    public void TryGetTreasure(IMap map)
    {
        if (map.CollectTreasure(Coordinate))
        {
            TreasuresCount += 1;
        }
    }
}