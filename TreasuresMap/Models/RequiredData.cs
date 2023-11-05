namespace TreasuresMap.Models;

public class RequiredData : IRequiredData
{
    public int MapHeight { get; set; }

    public int MapWidth { get; set; }

    public ICollection<Coordinate> Mountains { get; init; } = new HashSet<Coordinate>();

    public ICollection<ITreasure> Treasures { get; init; } = new List<ITreasure>();

    public ICollection<IAdventurer> Adventurers { get; init; } = new List<IAdventurer>();
}