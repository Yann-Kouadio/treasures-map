using TreasuresMap.Models;

namespace TreasuresMap;

public class PlayTurn
{
    private readonly IAdventurer _adventurer;
    private readonly IMap _map;
    private readonly IMovable _movement;

    public PlayTurn(IMap map, IAdventurer adventurer, IMovable movement)
    {
        _map = map;
        _adventurer = adventurer;
        _movement = movement;
    }

    public void Play()
    {
        foreach (var action in _adventurer.GetMovements())
        {
            var hasMoved = _adventurer.Move(_map, _movement, action);

            if (hasMoved)
            {
                _adventurer.TryGetTreasure(_map);
            }
        }
    }
}