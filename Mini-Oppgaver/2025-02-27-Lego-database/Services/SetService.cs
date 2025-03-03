
using _2025_02_27_Lego_database.Models;
using _2025_02_27_Lego_database.Views;

namespace _2025_02_27_Lego_database.Services;

public class SetService
{
  public static SetModel? ParseSets(string csvLine, int lineNum, string filePath)
  {
    try
    {
      var csvData = csvLine.Split(",");

      if (csvData.Length < 6)
      {
        Console.WriteLine($"{StylesClass.ERROR}File {filePath}: Line {lineNum} has invalid format and was not parsed: '{csvLine}' {StylesClass.RESET_ALL}");
        Console.WriteLine();
        return null;
      }

      return new SetModel(
        csvData[0],
        csvData[1],
        int.TryParse(csvData[2], out int year) ? year : 0,
        int.TryParse(csvData[3], out int themeId) ? themeId : 0,
        int.TryParse(csvData[4], out int numParts) ? numParts : 0,
    csvData[5]);
    }
    catch (Exception ex)
    {
      Console.WriteLine($"{StylesClass.RESET_ALL}File {filePath}: Line {lineNum} was not parsed: '{csvLine}'. Details: {ex.Message}{StylesClass.RESET_ALL}");
      Console.WriteLine();
      return null;
    }
  }
}