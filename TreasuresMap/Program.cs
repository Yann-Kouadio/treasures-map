using TreasuresMap;

//// Input file is 'data.txt'
//// Output file is saved in bin/debug(or release)/.../result.txt
try
{
    var map = Factory.CreateMap();
    var movement = Factory.CreateMovement();

    foreach (var adventurer in map.GetAdventurers())
    {
        Factory.Play(map, adventurer, movement);
    }

    Factory.DisplayResult(map);
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}