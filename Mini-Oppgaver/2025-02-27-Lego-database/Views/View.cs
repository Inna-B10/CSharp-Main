using _2025_02_27_Lego_database.Models;

namespace _2025_02_27_Lego_database.Views;

public class View
{
  public static void ShowMenu()
  {
    Console.WriteLine("Menu:");
    Console.WriteLine("[1] Search sets by name");
    Console.WriteLine("[0] Exit");
    Console.WriteLine("Enter your choice: ");
  }

  public static void ShowSets(List<SetModel> sets)
  {
    if (sets.Count == 0)
    {
      Console.WriteLine("No matches found");
      //what next?
    }
    else
    {
      foreach (var set in sets)
      {
        Console.WriteLine($"Name: {set.Name}, Set_num: {set.SetNum}");
      }
    }
  }
}