using System.Linq;
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
          List<dynamic> setsResult = new();
          string? searchSetName = null;

          while (searchSetName == null)
          {
            Console.Write("Enter set name: ");
            searchSetName = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(searchSetName))
            {
              Console.WriteLine($"{StylesClass.ERROR}Invalid input!{StylesClass.RESET_ALL}");
              searchSetName = null;
              Console.WriteLine();
            }
          }

          setsResult = sets
            .Where(s => s.Name.Contains(searchSetName, StringComparison.OrdinalIgnoreCase))
            .Select(s => new
            {
              s,
              ThemeName = themes.TryGetValue(s.ThemeId, out ThemeModel? value) ? value.Name : "Unknown",
              ParentThemeName = themes.TryGetValue(s.ThemeId, out ThemeModel? value1) && value1.ParentId.HasValue && themes.TryGetValue(value1.ParentId.Value, out ThemeModel? value2) ? value2.Name : null
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
          var yearResult = sets
            .Where(s => s.Year == year)
            .Select(s => new
            {
              s,
              ThemeName = themes.ContainsKey(s.ThemeId) ? themes[s.ThemeId].Name : "Unknown",
              ParentThemeName = themes.TryGetValue(s.ThemeId, out ThemeModel? value1) && value1.ParentId.HasValue && themes.TryGetValue(value1.ParentId.Value, out ThemeModel? value2) ? value2.Name : null
            }).Cast<dynamic>()
            .ToList();
          View.ShowSets(yearResult);
          break;

        case "3":
          string? searchThemeName = null;
          List<dynamic> themeResult = new List<dynamic>();

          var setsByTheme = sets
                .GroupBy(s => s.ThemeId)
                .ToDictionary(g => g.Key, g => g.ToList());

          while (searchThemeName == null)
          {
            Console.Write("Enter theme name: ");
            searchThemeName = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(searchThemeName))
            {
              Console.WriteLine($"{StylesClass.ERROR}Invalid input!{StylesClass.RESET_ALL}");
              searchThemeName = null;
              Console.WriteLine();
            }
          }

          var matchedThemes = themes.Values.Where(t => t.Name.Contains(searchThemeName, StringComparison.OrdinalIgnoreCase)).ToList();

          if (matchedThemes.Count > 0)
          {
            themeResult = matchedThemes.Where(theme => setsByTheme.ContainsKey(theme.Id)).SelectMany(theme => setsByTheme[theme.Id].Select(s => new
            {
              s,
              ThemeName = themes.TryGetValue(s.ThemeId, out ThemeModel? value) ? value.Name : "Unknown",
              ParentThemeName = themes.TryGetValue(s.ThemeId, out ThemeModel? value1) && value1.ParentId.HasValue && themes.TryGetValue(value1.ParentId.Value, out ThemeModel? value2) ? value2.Name : null
            }))
            .Cast<dynamic>()
            .ToList();
          }
          View.ShowSets(themeResult);
          break;

        case "4":
          List<dynamic> setNumResult = new();
          string? searchSetNum = null;

          while (searchSetNum == null)
          {
            Console.Write("Enter set number: ");
            searchSetNum = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(searchSetNum))
            {
              Console.WriteLine($"{StylesClass.ERROR}Invalid input!{StylesClass.RESET_ALL}");
              searchSetNum = null;
              Console.WriteLine();
            }
          }

          setNumResult = sets
            .Where(s => string.Equals(s.SetNum, searchSetNum, StringComparison.OrdinalIgnoreCase))
            .Select(s => new
            {
              s,
              ThemeName = themes.TryGetValue(s.ThemeId, out ThemeModel? value) ? value.Name : "Unknown",
              ParentThemeName = themes.TryGetValue(s.ThemeId, out ThemeModel? value1) && value1.ParentId.HasValue && themes.TryGetValue(value1.ParentId.Value, out ThemeModel? value2) ? value2.Name : null
            })
            .Cast<dynamic>()
              .ToList();

          View.ShowSets(setNumResult);
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
