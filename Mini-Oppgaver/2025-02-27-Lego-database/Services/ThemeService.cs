using _2025_02_27_Lego_database.Models;
using _2025_02_27_Lego_database.Views;

namespace _2025_02_27_Lego_database.Services;

public class ThemeService
{
  private readonly DataService _DataService;

  public ThemeService(DataService DataService)
  {
    _DataService = DataService;
  }

  public static Dictionary<int, ThemeModel> LoadThemes(string filePath)
  {
    var themes = DataService.LoadData(filePath, ParseThemes);

    return themes.ToDictionary(t => t.Id);
  }
  public static ThemeModel? ParseThemes(string csvLine, int lineNum, string filePath)
  {
    try
    {
      var csvData = csvLine.Split(",");
      if (csvData.Length < 3)
      {
        Console.WriteLine($"{StylesClass.ERROR}File {filePath}: Line {lineNum} has invalid format and was not parsed: '{csvLine}' {StylesClass.RESET_ALL}");
        Console.WriteLine();
        return null;
      }

      return new ThemeModel(
        int.TryParse(csvData[0], out int id) ? id : 0,
        csvData[1],
        int.TryParse(csvData[2], out int parentId) ? parentId : (int?)null
      );
    }
    catch (Exception ex)
    {
      Console.WriteLine($"{StylesClass.RESET_ALL}File {filePath}: Line {lineNum} was not parsed: '{csvLine}'. Details: {ex.Message}{StylesClass.RESET_ALL}");
      Console.WriteLine();
      return null;
    }
  }
}