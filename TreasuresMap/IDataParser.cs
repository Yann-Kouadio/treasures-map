using TreasuresMap.Models;

namespace TreasuresMap;

public interface IDataParser
{
    void Parse(string data, IRequiredData result);
}