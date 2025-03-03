namespace _2025_02_27_Lego_database.Views;

public class View
{
  public static void ShowMenu()
  {
    Console.WriteLine("Search sets by:");
    Console.WriteLine("[1] set name");
    Console.WriteLine("[2] year");
    Console.WriteLine("[3] theme name");
    Console.WriteLine("[4] set number");
    Console.WriteLine("[0] Exit");
    Console.Write("Enter your choice: ");
  }

  public static void ShowSets(List<dynamic> sets)
  {
    int count = sets.Count;
    if (count == 0)
    {
      Console.WriteLine($"{StylesClass.ERROR}No matches found{StylesClass.RESET_ALL}");
      Console.WriteLine();
    }
    else
    {
      foreach (var set in sets)
      {
        string cleanedName = set.s.SetName.Trim().Trim('"'); //delete " in the beginning if exists / in the name

        Console.WriteLine($"{StylesClass.VIOLET}Name: {cleanedName}, {StylesClass.GREEN}Set_num: {set.s.SetNum}, {StylesClass.CORAL}Year: {set.s.Year}, {StylesClass.GRAY}Num parts: {set.s.NumParts}, {StylesClass.RED}Theme: {set.ThemeName}"
         + (set.ParentThemeName != null ? $", {StylesClass.BLUE}Parent theme: {set.ParentThemeName}" : ""));
      }
      Console.WriteLine();
      Console.WriteLine($"Total found: {count} set(s)");
      Console.WriteLine(StylesClass.RESET_ALL);
    }
  }
}