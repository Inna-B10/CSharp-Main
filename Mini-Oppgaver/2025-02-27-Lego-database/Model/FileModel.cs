
namespace _2025_02_27_Lego_database.Model;

public class FileModel<T>
where T : class
{
  public List<T> Rows { get; set; } = [];
  public FileModel(string filePath, Func<string, T> parser)
  {
    if (!File.Exists(filePath))
    {
      Console.WriteLine($"The file {filePath} was not found!");
      return;
    }

    var lines = File.ReadAllLines(filePath);
    foreach (var line in lines)
    {
      Rows.Add(parser(line));
    }
  }
}