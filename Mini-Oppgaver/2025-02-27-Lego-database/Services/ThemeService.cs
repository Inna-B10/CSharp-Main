using _2025_02_27_Lego_database.Models;

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
  public static ThemeModel ParseThemes(string csvLine)
  {
    var csvData = csvLine.Split(",");

    return new ThemeModel(
      int.TryParse(csvData[0], out int id) ? id : 0,
      csvData[1],
      int.TryParse(csvData[2], out int parentId) ? parentId : (int?)null
    );
  }
}