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

        case "2":
          string? searchSetYear;
          int year = 0;
          while (year == 0)
          {
            Console.Write("Enter year of release (1949-2025): ");
            searchSetYear = Console.ReadLine()?.Trim();

            if (!int.TryParse(searchSetYear, out year) || year < 1949 || year > 2025)
            {
              Console.WriteLine($"{StylesClass.ERROR}Invalid input!{StylesClass.RESET_ALL}");
              year = 0;
              Console.WriteLine();
            }
          }
          var yearResult = sets.Where(s => s.Year == year).Select(s => new
          {
            s,
            ThemeName = themes.ContainsKey(s.ThemeId) ? themes[s.ThemeId].Name : "Unknown",
            ParentThemeName = themes.ContainsKey(s.ThemeId) && themes[s.ThemeId].ParentId.HasValue && themes.ContainsKey(themes[s.ThemeId].ParentId.Value)
            ? themes[themes[s.ThemeId].ParentId.Value].Name : null
          }).Cast<dynamic>()
            .ToList();
          View.ShowSets(yearResult);
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