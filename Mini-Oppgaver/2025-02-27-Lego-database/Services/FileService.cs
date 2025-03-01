
namespace _2025_02_27_Lego_database.Services;


public class FileService
{
  public List<T> LoadData<T>(string filePath, Func<string, T> parser)
  where T : class
  {
    var rows = new List<T>();

    if (!File.Exists(filePath))
    {
      Console.WriteLine($"The file {filePath} was not found!");
      return rows;
    }

    var lines = File.ReadAllLines(filePath);
    foreach (var line in lines)
    {
      rows.Add(parser(line));
    }
    return rows;
  }

}