using linq_oppgavegjennomgang.Model;

namespace linq_oppgavegjennomgang;

class Program
{
    static void Main(string[] args)
    {
        var file = new CsvFile("./complete.csv");
        Console.WriteLine(file.Rows.Where(r => string.Equals(r.Shape, "cylinder")).Count());
        

        var ValidShapes = file.Rows.Select(r => r.Shape).ToList();

        var input = Console.ReadLine();
        Console.WriteLine(ValidShapes.Contains(input));

        Console.WriteLine(LevenShtein.Lev("cylinder", "cyliner"));
    }
}
