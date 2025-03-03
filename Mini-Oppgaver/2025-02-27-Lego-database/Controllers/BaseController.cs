using System.Linq;
using _2025_02_27_Lego_database.Models;
using _2025_02_27_Lego_database.Services;
using _2025_02_27_Lego_database.Views;

namespace _2025_02_27_Lego_database.Controllers;

public class BaseController
{
  private readonly List<SetModel> sets;
  private readonly Dictionary<int, ThemeModel> themes;

  public BaseController(string setsFilePath, string themesFilePath)
  {
    sets = DataService.LoadData(setsFilePath, SetService.ParseSetLine); //path, function parser

    themes = ThemeService.LoadThemes(themesFilePath);
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
        //search sets by set name
        case "1":
          string? searchSetName = null;

          //get input
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

          //get data in format SetModel + fields ThemeName and ParentThemeName via variable ParentId from ThemeModel
          var setsResult = sets
            .Where(s => s.SetName.Contains(searchSetName, StringComparison.OrdinalIgnoreCase))
            .Select(s => new
            {
              s,
              ThemeName = themes.TryGetValue(s.ThemeId, out ThemeModel? value) ? value.ThemeName : "Unknown",
              ParentThemeName = themes.TryGetValue(s.ThemeId, out ThemeModel? value1) && value1.ParentId.HasValue && themes.TryGetValue(value1.ParentId.Value, out ThemeModel? value2) ? value2.ThemeName : null
            })
            .Cast<dynamic>() //dynamic because additional fields ThemeName, ParentThemeName
              .ToList();

          View.ShowSets(setsResult);
          break;

        //search sets by year
        case "2":
          string? searchSetYear;
          int year = 0;

          //get input
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

          //get data
          var yearResult = sets
            .Where(s => s.Year == year)
            .Select(s => new
            {
              s,
              ThemeName = themes.TryGetValue(s.ThemeId, out ThemeModel? value) ? value.ThemeName : "Unknown",
              ParentThemeName = themes.TryGetValue(s.ThemeId, out ThemeModel? value1) && value1.ParentId.HasValue && themes.TryGetValue(value1.ParentId.Value, out ThemeModel? value2) ? value2.ThemeName : null
            }).Cast<dynamic>()
            .ToList();

          View.ShowSets(yearResult);
          break;

        //search sets by theme name
        case "3":
          string? searchThemeName = null;
          List<dynamic> themeResult = []; //if no matched result, return empty list

          //group sets by ThemeId for effective search (ThemeId as key)
          var setsByTheme = sets
                .GroupBy(s => s.ThemeId)
                .ToDictionary(g => g.Key, g => g.ToList());

          //get input
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

          //get all themes name that match search term
          var matchedThemes = themes.Values.Where(t => t.ThemeName.Contains(searchThemeName, StringComparison.OrdinalIgnoreCase)).ToList();

          if (matchedThemes.Count > 0)
          //get sets details from list setsByTheme where key is ThemeId from matchedThemes
          //NB not including search term in ParentThemeName
          {
            themeResult = matchedThemes.Where(theme => setsByTheme.ContainsKey(theme.ThemeId)).SelectMany(theme => setsByTheme[theme.ThemeId].Select(s => new
            {
              s,
              ThemeName = themes.TryGetValue(s.ThemeId, out ThemeModel? value) ? value.ThemeName : "Unknown",
              ParentThemeName = themes.TryGetValue(s.ThemeId, out ThemeModel? value1) && value1.ParentId.HasValue && themes.TryGetValue(value1.ParentId.Value, out ThemeModel? value2) ? value2.ThemeName : null
            }))
            .Cast<dynamic>()
            .ToList();
          }

          View.ShowSets(themeResult);
          break;

        //search sets by set number
        case "4":
          string? searchSetNum = null;

          //get input
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

          //get data
          var setNumResult = sets
            .Where(s => string.Equals(s.SetNum, searchSetNum, StringComparison.OrdinalIgnoreCase))
            .Select(s => new
            {
              s,
              ThemeName = themes.TryGetValue(s.ThemeId, out ThemeModel? value) ? value.ThemeName : "Unknown",
              ParentThemeName = themes.TryGetValue(s.ThemeId, out ThemeModel? value1) && value1.ParentId.HasValue && themes.TryGetValue(value1.ParentId.Value, out ThemeModel? value2) ? value2.ThemeName : null
            })
            .Cast<dynamic>()
            .ToList();

          View.ShowSets(setNumResult);
          break;

        //exit
        case "0":
          Console.WriteLine("The program is terminating. Exit program.");
          Console.WriteLine();
          isRunning = false;
          break;

        //error
        default:
          Console.WriteLine($"{StylesClass.ERROR}Invalid input!{StylesClass.RESET_ALL}");
          Console.WriteLine();
          break;
      }
    }
  }
}
