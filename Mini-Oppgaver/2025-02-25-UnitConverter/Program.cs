namespace _2025_02_25_UnitConverter;



public class UnitConverter<TFrom>(Dictionary<TFrom, double> rates)
where TFrom : notnull
{
    private readonly Dictionary<TFrom, double> conversionRates = rates ?? throw new ArgumentNullException(nameof(rates));

    public double ConvertToMeters(TFrom from, double quantity)
    {
        if (!conversionRates.TryGetValue(from, out double value))
            throw new ArgumentException($"Конверсия из '{from}' не определена.");

        return value * quantity;
    }
}

class Program
{
    public static Dictionary<string, double> LengthUnits = new()
        {
            { "meters", 1 },
            { "kilometers", 1000 },
            { "miles", 1609.34 },
            { "feet", 0.3048 }
        };
    static void Main()
    {
        try
        {

            var lengthConverter = new UnitConverter<string>(LengthUnits);
            Console.WriteLine("10 миль в метрах: " + lengthConverter.ConvertToMeters("mils", 10));
            Console.WriteLine("5 километров в метрах: " + lengthConverter.ConvertToMeters("kilometers", 5));
        }
        catch (ArgumentException arEx)
        {
            Console.WriteLine("Error: " + arEx.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            throw;
        }

        Console.WriteLine("Hello");
    }
}


