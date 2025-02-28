using _2025_02_27_Lego_database.Model;
namespace _2025_02_27_Lego_database;

class Program
{
    static void Main(string[] args)
    {
        var fileSets = new FileModel<SetModel>("./sets.csv", line => new SetModel(line));
        var fileThemes = new FileModel<ThemeModel>("./themes.csv", line => new ThemeModel(line));

        // Console.WriteLine(file.Rows.Where(r => r.Year > 1980).Count());

        string? inputSet = Console.ReadLine()?.Trim();
        string? inputTheme = Console.ReadLine()?.Trim();

        List<SetModel> setResults = fileSets.Rows.Where(r => r.Name.Contains(inputSet, StringComparison.InvariantCultureIgnoreCase)).ToList();

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
        List<ThemeModel> themeResults = [.. fileThemes.Rows.Where(r => r.Name.Contains(inputTheme, StringComparison.OrdinalIgnoreCase))];

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
