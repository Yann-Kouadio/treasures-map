namespace TreasuresMap.Models;

public interface IMap
{
    int Height { get; }

    int Width { get; }

    ICollection<IAdventurer> GetAdventurers();

    HashSet<(int x, int y)> GetMountains();

    Dictionary<(int x, int y), int> GetTreasures();

    bool CoordinateOnMountain(int x, int y);

    bool CollectTreasure(Coordinate coordinate);

    bool IsCoordinateOnMap(int x, int y);
}