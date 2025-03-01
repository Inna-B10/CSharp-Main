using _2025_02_27_Lego_database.Models;

namespace _2025_02_27_Lego_database.Services;

public class ThemeService
{
  private readonly FileService _fileService;

  public ThemeService(FileService fileService)
  {
    _fileService = fileService;
  }

  public Dictionary<int, ThemeModel> LoadThemes(string filePath)
  {
    var themes = _fileService.LoadData(filePath, ParseThemes);

    return themes.ToDictionary(t => t.Id);
  }
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