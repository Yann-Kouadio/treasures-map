namespace TreasuresMap.Models;

public sealed class Treasure : ITreasure
{
    public Coordinate Coordinate { get; init; } = null!;

    public int Quantity { get; init; }
}