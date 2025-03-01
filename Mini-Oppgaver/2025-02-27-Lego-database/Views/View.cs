namespace _2025_02_27_Lego_database.Views;

public class View
{
  public static void ShowMenu()
  {
    Console.WriteLine("Menu:");
    Console.WriteLine("[1] Search sets by set name");
    Console.WriteLine("[2] Search sets by year");
    Console.WriteLine("[3] Search sets by theme name");
    Console.WriteLine("[4] Search sets by set number");
    Console.WriteLine("[0] Exit");
    Console.Write("Enter your choice: ");
  }

  public static void ShowSets(List<dynamic> sets)
  {
    if (sets.Count == 0)
    {
      Console.WriteLine($"{StylesClass.ERROR}No matches found{StylesClass.RESET_ALL}");
      Console.WriteLine();
    }
    else
    {
      foreach (var set in sets)
      {
        string cleanedName = set.s.Name.Trim().Trim('"'); //delete " in the beginning if exists / in the name
        Console.WriteLine($"{StylesClass.VIOLET}Name: {cleanedName}, {StylesClass.GREEN}Set_num: {set.s.SetNum}, {StylesClass.CORAL}Year: {set.s.Year}, {StylesClass.GRAY}Num parts: {set.s.NumParts}, {StylesClass.RED}Theme: {set.ThemeName}"
         + (set.ParentThemeName != null ? $", {StylesClass.BLUE}Parent theme: {set.ParentThemeName}" : ""));
      }
      Console.WriteLine(StylesClass.RESET_ALL);
    }
  }
}