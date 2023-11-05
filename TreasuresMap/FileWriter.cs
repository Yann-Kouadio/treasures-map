using TreasuresMap.Models;

namespace TreasuresMap;

public class FileWriter
{
    private readonly IMap _map;

    public FileWriter(IMap map)
    {
        _map = map;
    }

    public void WriteResult()
    {
        var filePath = $"{AppDomain.CurrentDomain.BaseDirectory}/result.txt";

        using var writer = new StreamWriter(filePath);

        writer.WriteLine($"C - {_map.Width} - {_map.Height}");

        var mountains = _map.GetMountains();
        var treasures = _map.GetTreasures();
        var adventurers = _map.GetAdventurers();

        foreach (var mountain in mountains)
        {
            writer.WriteLine($"M - {mountain.x} - {mountain.y}");
        }

        writer.WriteLine("# {T comme Trésor} - {Axe horizontal} - {Axe vertical} - {Nb. de trésors restants}");

        foreach (var treasure in treasures.Where(treasure => treasure.Value > 0))
        {
            writer.WriteLine($"T - {treasure.Key.x} - {treasure.Key.y} - {treasure.Value}");
        }

        writer.WriteLine(
            "# {A comme Aventurier} - {Nom de l’aventurier} - {Axe horizontal} - {Axe\r\nvertical} - {Orientation} - {Nb. trésors ramassés}");

        foreach (var adventurer in adventurers)
        {
            writer.WriteLine(
                $"A - {adventurer.Name} - {adventurer.Coordinate.X} - {adventurer.Coordinate.Y} - {adventurer.Orientation} - {adventurer.TreasuresCount}");
        }

        writer.WriteLine("");
        writer.WriteLine("Que l’on peut représenter sous la forme suivante :");

        for (var y = 0; y < _map.Height; y++)
        {
            for (var x = 0; x < _map.Width; x++)
            {
                var key = new ValueTuple<int, int>(x, y);

                if (mountains.Contains(key))
                {
                    writer.Write("M".PadRight(15));
                    continue;
                }

                if (treasures.TryGetValue(key, out var quantity))
                {
                    if (quantity > 0)
                    {
                        writer.Write($"T({quantity})".PadRight(15));
                        continue;
                    }
                }

                if (adventurers.Any())
                {
                    var adventurer = adventurers.FirstOrDefault(a => a.Coordinate.X == x && a.Coordinate.Y == y);

                    if (adventurer != null)
                    {
                        writer.Write($"A({adventurer.Name})".PadRight(15));

                        continue;
                    }
                }

                writer.Write(".".PadRight(15));
            }

            writer.WriteLine();
        }
    }
}