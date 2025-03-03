
using _2025_02_27_Lego_database.Views;

namespace _2025_02_27_Lego_database.Services;


public class DataService
{
  public static List<T> LoadData<T>(string filePath, Func<string, T> parser)
  where T : class
  {
    var rows = new List<T>();

    if (!File.Exists(filePath))
    {
      Console.Write($"{StylesClass.ERROR}Error: The file {filePath} was not found!{StylesClass.RESET_ALL}");

      Environment.Exit(0); //Forced exit from the program
    }

    var lines = File.ReadAllLines(filePath);
    foreach (var line in lines)
    {
      rows.Add(parser(line));
    }
    return rows;
  }

}