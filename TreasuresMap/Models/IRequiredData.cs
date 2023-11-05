namespace TreasuresMap.Models;

public interface IRequiredData
{
    int MapHeight { get; set; }

    int MapWidth { get; set; }

    ICollection<Coordinate> Mountains { get; }

    ICollection<ITreasure> Treasures { get; }

    ICollection<IAdventurer> Adventurers { get; }
}