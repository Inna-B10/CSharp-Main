using _2025_02_27_Lego_database.Model;

namespace _2025_02_27_Lego_database.Services;

public class ThemeService
{
  public ThemeModel ParseThemes(string csvLine)
  {
    var csvData = csvLine.Split(",");

    return new ThemeModel(
      int.TryParse(csvData[0], out int id) ? id : 0,
      csvData[1],
      int.TryParse(csvData[2], out int parentId) ? parentId : (int?)null
    );
  }
}