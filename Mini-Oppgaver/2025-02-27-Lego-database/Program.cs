namespace _2025_02_27_Lego_database;

using _2025_02_27_Lego_database.Controllers;

class Program
{
    static void Main(string[] args)
    {

        //         string? inputTheme = Console.ReadLine()?.Trim();
        // 
        //         List<ThemeModel> themeResults = [.. themes.Where(r => r.Name.Contains(inputTheme, StringComparison.OrdinalIgnoreCase))];
        // 
        //         if (themeResults.Count == 0)
        //         {
        //             Console.WriteLine("No matches found");
        //         }
        //         else
        //         {
        //             foreach (var theme in themeResults)
        //             {
        //                 Console.WriteLine($"Category name: {theme.Name}");
        //             }
        //         }

        string setsFilePath = "./sets.csv";
        string themesFilePath = "./themes.csv";

        ControllerBase controller = new ControllerBase(setsFilePath, themesFilePath);

        controller.Start();
    }
}
