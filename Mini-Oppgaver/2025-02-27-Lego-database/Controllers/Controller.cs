using System.ComponentModel.Design;
using _2025_02_27_Lego_database.Models;
using _2025_02_27_Lego_database.Services;
using _2025_02_27_Lego_database.Views;

namespace _2025_02_27_Lego_database.Controllers;

public class ControllerBase
{
  private List<SetModel> sets;
  private List<ThemeModel> themes;

  public ControllerBase(string setsFilePath, string themesFilePath)
  {
    var fileService = new FileService();
    var setService = new SetService();
    var themeService = new ThemeService();

    sets = fileService.LoadData("./sets.csv", setService.ParseSets);

    themes = fileService.LoadData("./themes.csv", themeService.ParseThemes);
  }

  public void Start()
  {
    bool isRunning = true;

    while (isRunning)
    {
      View.ShowMenu();
      string? menuChoice = Console.ReadLine()?.Trim();

      switch (menuChoice)
      {
        case "1":
          Console.Write("Enter set name: ");
          string? searchSetName = Console.ReadLine()?.Trim();
          List<SetModel> setsResult = sets.Where(s => s.Name.Contains(searchSetName, StringComparison.OrdinalIgnoreCase)).ToList();
          View.ShowSets(setsResult);
          break;

        case "0":
          Console.WriteLine("The program is terminating. Exit.");
          isRunning = false;
          break;

        default:
          Console.WriteLine("Invalid input!");
          break;
      }
    }
  }
}