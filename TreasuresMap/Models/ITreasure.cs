namespace TreasuresMap.Models;

public interface ITreasure
{
    Coordinate Coordinate { get; init; }

    int Quantity { get; init; }
}