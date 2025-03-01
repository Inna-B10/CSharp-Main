namespace _2025_02_27_Lego_database;
using _2025_02_27_Lego_database.Model;
using _2025_02_27_Lego_database.Services;

class Program
{
    static void Main(string[] args)
    {
        var fileService = new FileService();
        var setService = new SetService();
        var themeService = new ThemeService();

        List<SetModel> sets = fileService.LoadData("./sets.csv", setService.ParseSets);
        List<ThemeModel> themes = fileService.LoadData("./themes.csv", themeService.ParseThemes);

        // Console.WriteLine(file.Rows.Where(r => r.Year > 1980).Count());

        string? inputSet = Console.ReadLine()?.Trim();

        List<SetModel> setResults = sets.Where(r => r.Name.Contains(inputSet, StringComparison.InvariantCultureIgnoreCase)).ToList();

        if (setResults.Count == 0)
        {
            Console.WriteLine("No matches found");
            //what next?
        }
        else
        {
            foreach (var set in setResults)
            {
                Console.WriteLine($"Name: {set.Name}, Set_num: {set.SetNum}");
            }
        }
        string? inputTheme = Console.ReadLine()?.Trim();

        List<ThemeModel> themeResults = [.. themes.Where(r => r.Name.Contains(inputTheme, StringComparison.OrdinalIgnoreCase))];

        if (themeResults.Count == 0)
        {
            Console.WriteLine("No matches found");
        }
        else
        {
            foreach (var theme in themeResults)
            {
                Console.WriteLine($"Category name: {theme.Name}");
            }
        }
    }
}
