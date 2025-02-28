using _2025_02_27_Lego_database.Model;
namespace _2025_02_27_Lego_database;

class Program
{
    static void Main(string[] args)
    {
        var fileSets = new FileModel("./sets.csv");

        // Console.WriteLine(file.Rows.Where(r => r.Year > 1980).Count());

        string? input = Console.ReadLine()?.Trim();

        List<SetModel> results = fileSets.Rows.Where(r => r.Name.Contains(input, StringComparison.InvariantCultureIgnoreCase)).ToList();

        if (results.Count == 0)
        {
            Console.WriteLine("No matches found");
            //what next?
        }
        else
        {
            foreach (var item in results)
            {
                Console.WriteLine($"Name: {item.Name}, Set_num: {item.SetNum}");
            }
        }
    }
}
