
namespace _2025_02_27_Lego_database.Model;

public class FileModel
{
  public List<SetModel> Rows { get; set; } = [];
  public FileModel(string filePath)
  {
    if (!File.Exists(filePath))
    {
      Console.WriteLine($"The file {filePath} was not found!");
      return;
    }
    var lines = File.ReadAllLines(filePath);
    foreach (var line in lines)
    {
      Rows.Add(new SetModel(line));
    }
  }

}