namespace TreasuresMap.Models;

public interface IMove
{
    bool Move(IMap map, IMovable movable, char action);
}