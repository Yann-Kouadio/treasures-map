namespace TreasuresMap.Models;

public interface IMovable
{
    bool Move(IMap map, IAdventurer adventurer, char action);
}