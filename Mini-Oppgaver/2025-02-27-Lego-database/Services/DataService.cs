
using System.Linq;
using _2025_02_27_Lego_database.Views;

namespace _2025_02_27_Lego_database.Services;


public class DataService
{
  public static List<T> LoadData<T>(string filePath, Func<string, int, string, T?> parser)
  where T : class
  {
    var rows = new List<T>();

    if (!File.Exists(filePath))
    {
      Console.Write($"{StylesClass.ERROR}Error: The file {filePath} was not found!{StylesClass.RESET_ALL}");

      Environment.Exit(0); //Forced exit from the program
    }

    var lines = File.ReadAllLines(filePath);
    for (int i = 0; i < lines.Length; i++)
    {
      var item = parser(lines[i], i + 1, filePath); //lines in file are numbered from 1!
      if (item != null)
      {
        rows.Add(item);
      }
    }

    //NB alternative
    /*var lines = File.ReadAllLines(filePath)
                     // .Select((line, index) => (line, index + 1))
                     // .Select(tuple => parser(tuple.Item1, tuple.Item2))  // tuple.Item1 = строка, tuple.Item2 = номер строки

                     .Select((line, index) => (lineText: line, lineNumber: index + 1)) //Add a line number (numbering starts from 1) and specify field names explicitly
                     .Select(tuple => parser(tuple.lineText, tuple.lineNumber))  //Passing to parser
                     .Where(line => line is not null)
                     .Select(line => line!);
     rows.AddRange(lines);*/

    return rows;
  }

}