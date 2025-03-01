using System.Drawing;
using _2025_02_27_Lego_database.Models;

namespace _2025_02_27_Lego_database.Views;

public class View
{
  public static void ShowMenu()
  {
    Console.WriteLine("Menu:");
    Console.WriteLine("[1] Search sets by set name");
    Console.WriteLine("[0] Exit");
    Console.Write("Enter your choice: ");
  }

  public static void ShowSets(List<SetModel> sets)
  {
    if (sets.Count == 0)
    {
      Console.WriteLine("No matches found");
      Console.WriteLine();
    }
    else
    {
      foreach (var set in sets)
      {
        string cleanedName = set.Name.Trim().Trim('"'); //delete " in the beginning if exists / in the name
        Console.WriteLine($"{StylesClass.BLUE}Name: {cleanedName}, {StylesClass.GREEN}Set_num: {set.SetNum}{StylesClass.RESET_ALL}");
      }
      Console.WriteLine();
    }
  }
}