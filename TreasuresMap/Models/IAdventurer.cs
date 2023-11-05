using TreasuresMap.Enums;

namespace TreasuresMap.Models;

public interface IAdventurer : IMove, ICollect
{
    string Name { get; }

    Coordinate Coordinate { get; }

    Orientation Orientation { get; }

    int TreasuresCount { get; }

    char[] GetMovements();

    void UpdateOrientation(Orientation orientation);

    void UpdateCoordinate(Coordinate coordinate);
}