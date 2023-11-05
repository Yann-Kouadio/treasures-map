using TreasuresMap.Enums;
using TreasuresMap.Models;

namespace TreasuresMap;

public sealed class StringParser : IDataParser
{
    public void Parse(string data, IRequiredData result)
    {
        if (string.IsNullOrWhiteSpace(data))
        {
            throw new ArgumentException("Data to parse cannot be null or empty");
        }

        var elements = data.Split('-').Select(x => x.Trim()).ToArray();

        switch (elements.First().ToUpperInvariant())
        {
            case "C":
                if (elements.Length != 3)
                    throw new ArgumentException("Invalid 'C' format.");

                if (!int.TryParse(elements[1], out var mapWidth) || !int.TryParse(elements[2], out var mapHeight))
                    throw new ArgumentException("Invalid 'C' data format.");

                result.MapWidth = mapWidth;
                result.MapHeight = mapHeight;

                break;

            case "M":
                if (elements.Length != 3)
                    throw new ArgumentException("Invalid 'M' format.");

                if (!int.TryParse(elements[1], out var mountainX) || !int.TryParse(elements[2], out var mountainY))
                    throw new ArgumentException("Invalid 'M' data format.");

                result.Mountains.Add(new Coordinate(mountainX, mountainY));

                break;

            case "T":
                if (elements.Length != 4)
                    throw new ArgumentException("Invalid 'T' format.");

                if (!int.TryParse(elements[1], out var treasureX) ||
                    !int.TryParse(elements[2], out var treasureY) ||
                    !int.TryParse(elements[3], out var treasureQuantity))
                {
                    throw new ArgumentException("Invalid 'T' data format.");
                }

                result.Treasures.Add(Factory.CreateTreasure(treasureX, treasureY, treasureQuantity));

                break;

            case "A":
                if (elements.Length != 6)
                    throw new ArgumentException("Invalid 'A' format.");

                var adventurerName = elements[1];

                var validEnum = Enum.TryParse<Orientation>(elements[4], out var adventurerOrientation) &&
                                Enum.IsDefined(typeof(Orientation), adventurerOrientation);

                var adventurerMovement = elements[5];

                if (string.IsNullOrWhiteSpace(adventurerName) ||
                    !int.TryParse(elements[2], out var adventurerX) ||
                    !int.TryParse(elements[3], out var adventurerY) ||
                    !validEnum ||
                    string.IsNullOrWhiteSpace(adventurerMovement))
                {
                    throw new ArgumentException("Invalid 'A' data format.");
                }

                result.Adventurers.Add(Factory.CreateAdventurer(adventurerName, adventurerX, adventurerY,
                    adventurerOrientation, adventurerMovement));

                break;
        }
    }
}