using TreasuresMap.Models;

namespace TreasuresMap;

public sealed class FileReader
{
    public IRequiredData GetData()
    {
        var result = Factory.CreateRequiredData();

        // Read data from file
        // Console.WriteLine("Please enter the file path:");
        // var filePath = Console.ReadLine();

        var filePath = $"{AppDomain.CurrentDomain.BaseDirectory}/data.txt";

        // Uncomment if file path is loaded from console
        // if (string.IsNullOrWhiteSpace(filePath))
        //     throw new ApplicationException("Invalid file path");

        var parser = Factory.CreateParser();

        using var reader = new StreamReader(filePath);

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            parser.Parse(line, result);
        }

        return result;
    }
}