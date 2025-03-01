using _2025_02_27_Lego_database.Models;
using _2025_02_27_Lego_database.Services;
using _2025_02_27_Lego_database.Views;

namespace _2025_02_27_Lego_database.Controllers;

public class ControllerBase
{
  private List<SetModel> sets;
  private Dictionary<int, ThemeModel> themes;

  public ControllerBase(string setsFilePath, string themesFilePath)
  {
    var fileService = new FileService();
    var setService = new SetService();
    var themeService = new ThemeService(fileService);

    sets = fileService.LoadData("./sets.csv", setService.ParseSets);

    themes = themeService.LoadThemes(themesFilePath);
  }

  public void Start()
  {
    bool isRunning = true;

    while (isRunning)
    {
      View.ShowMenu();
      string? menuChoice = Console.ReadLine()?.Trim();
      Console.WriteLine();

      switch (menuChoice)
      {
        case "1":
          Console.Write("Enter set name: ");

          string? searchSetName = Console.ReadLine()?.Trim();

          var setsResult = sets
            .Where(s => s.Name.Contains(searchSetName, StringComparison.OrdinalIgnoreCase))
            .Select(s => new
            {
              s,
              ThemeName = themes.ContainsKey(s.ThemeId) ? themes[s.ThemeId].Name : "Unknown",
              ParentThemeName = themes.ContainsKey(s.ThemeId) && themes[s.ThemeId].ParentId.HasValue && themes.ContainsKey(themes[s.ThemeId].ParentId.Value)
              ? themes[themes[s.ThemeId].ParentId.Value].Name : null
            })
            .Cast<dynamic>()
              .ToList();

          View.ShowSets(setsResult);
          break;

        case "0":
          Console.WriteLine("The program is terminating. Exit program.");
          Console.WriteLine();
          isRunning = false;
          break;

        default:
          Console.WriteLine($"{StylesClass.ERROR}Invalid input!{StylesClass.RESET_ALL}");
          Console.WriteLine();
          break;
      }
    }
  }
}