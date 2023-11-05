namespace TreasuresMap.Models;

public sealed class Map : IMap
{
    // Contains adventurers initial positions; it is only used in the initialization process to avoid retrieving initial position from _adventurers.
    private readonly HashSet<(int x, int y)> _adventurerInitialPositions;

    private readonly ICollection<IAdventurer> _adventurers;

    // Contains all the mountains where (int, int) are the coordinate
    private readonly HashSet<(int x, int y)> _mountains;

    // Contains all the treasures where (int, int) are the coordinate and the last int the quantity
    private readonly Dictionary<(int x, int y), int> _treasures;

    public Map(IRequiredData data)
    {
        if (data.MapHeight <= 0 || data.MapWidth <= 0)
            throw new ArgumentException("Map height and/or width must be greater than 0.");

        if (data.MapHeight >= int.MaxValue || data.MapWidth >= int.MaxValue)
            throw new ArgumentException("Map height and/or width should have a reasonable.");

        // Initialization
        Height = data.MapHeight;
        Width = data.MapWidth;
        _mountains = new HashSet<(int, int)>();
        _treasures = new Dictionary<(int, int), int>();
        _adventurerInitialPositions = new HashSet<(int, int)>();
        _adventurers = new List<IAdventurer>(data.Adventurers.Count);

        Initialization(data);
    }

    public int Height { get; }

    public int Width { get; }

    public ICollection<IAdventurer> GetAdventurers()
    {
        return _adventurers;
    }

    public HashSet<(int, int)> GetMountains()
    {
        return _mountains;
    }

    public Dictionary<(int, int), int> GetTreasures()
    {
        return _treasures;
    }

    public bool CoordinateOnMountain(int x, int y)
    {
        return IsCoordinatedMountain(x, y);
    }

    public bool CollectTreasure(Coordinate coordinate)
    {
        if (IsCoordinatedTreasures(coordinate.X, coordinate.Y))
        {
            _treasures[(coordinate.X, coordinate.Y)] -= 1;

            return true;
        }

        return false;
    }

    public bool IsCoordinateOnMap(int x, int y)
    {
        return IsOnMap(new Coordinate(x, y));
    }

    private void Initialization(IRequiredData data)
    {
        // Init Mountains
        foreach (var mountain in data.Mountains)
        {
            AddMountain(mountain);
        }

        // Init Treasures
        foreach (var treasure in data.Treasures)
        {
            AddTreasure(treasure);
        }

        // Init Adventurers
        foreach (var adventurer in data.Adventurers)
        {
            AddAdventurer(adventurer);
        }
    }

    private void AddMountain(Coordinate coordinate)
    {
        BaseCoordinateValidation(coordinate, "Mountain");

        _mountains.Add((coordinate.X, coordinate.Y));
    }

    private void AddTreasure(ITreasure treasure)
    {
        BaseCoordinateValidation(treasure.Coordinate, "Treasure");

        if (treasure.Quantity <= 0)
            throw new ArgumentException("Treasure quantity must be greater than 0.");

        if (treasure.Quantity >= int.MaxValue)
            throw new ArgumentException("Treasure quantity must be reasonable.");

        _treasures.Add((treasure.Coordinate.X, treasure.Coordinate.Y), treasure.Quantity);
    }

    private void AddAdventurer(IAdventurer adventurer)
    {
        BaseCoordinateValidation(adventurer.Coordinate, "Adventurer");

        _adventurerInitialPositions.Add((adventurer.Coordinate.X, adventurer.Coordinate.Y));

        _adventurers.Add(adventurer);
    }

    private void BaseCoordinateValidation(Coordinate coordinate, string text)
    {
        if (!IsOnMap(coordinate))
            throw new ArgumentException($"{text} must be on map.");

        if (!IsCoordinateFree(coordinate))
            throw new ArgumentException("Coordinate already in used.");
    }

    private bool IsOnMap(Coordinate coordinate)
    {
        return coordinate.X >= 0 && coordinate.X < Width && coordinate.Y >= 0 && coordinate.Y < Height;
    }

    private bool IsCoordinateFree(Coordinate coordinate)
    {
        return IsOnMap(coordinate) &&
               IsCoordinatedMountain(coordinate.X, coordinate.Y) == false &&
               IsCoordinatedTreasures(coordinate.X, coordinate.Y) == false &&
               _adventurerInitialPositions.Contains((coordinate.X, coordinate.Y)) == false;
    }

    private bool IsCoordinatedMountain(int x, int y)
    {
        return _mountains.Contains((x, y));
    }

    private bool IsCoordinatedTreasures(int x, int y)
    {
        return _treasures.TryGetValue((x, y), out var quantity) && quantity > 0;
    }
}