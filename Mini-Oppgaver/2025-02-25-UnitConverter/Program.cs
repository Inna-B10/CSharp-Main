using System.Globalization;

namespace _2025_02_25_UnitConverter;

class Program
{
    public static Dictionary<string, double> LengthUnits = new()
        {
            { "meters", 1 },
            { "kilometers", 1000 },
            { "miles", 1609.34 },
            { "feet", 0.3048 }
        };
    public static Dictionary<string, double> CurrencyUnits = new()
        {
            { "USD", 1 },
            { "EUR", 0.95 },
            { "GBP", 0.79 },
            { "NOK", 11.10 }
        };
    public static string FormatNumber(double value)
    {
        return value.ToString("n", new CultureInfo("no-NO"));
    }
    static void Main()
    {
        try
        {
            var lengthConverter = new UnitConverter<string>(LengthUnits);
            Console.WriteLine("10 miles in meters: " + FormatNumber(lengthConverter.ConvertToBase("miles", 10)));
            Console.WriteLine("5 kilometers in meters: " + FormatNumber(lengthConverter.ConvertToBase("kilometers", 5)));

            var currencyConverter = new UnitConverter<string>(CurrencyUnits);
            double result = currencyConverter.ConvertToBase("NOK", 100);
            string convertedValue = FormatNumber(result);
            Console.WriteLine("100 Norwegian Kroner in Dollars: " + convertedValue);
        }
        catch (ArgumentNullException arNull)
        {
            Console.WriteLine("Error: " + arNull.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        Console.WriteLine("Hello");
    }
}

public class UnitConverter<TFrom>(Dictionary<TFrom, double> rates)
where TFrom : notnull
{
    private readonly Dictionary<TFrom, double> conversionRates = rates ?? throw new ArgumentNullException(nameof(rates));

    public double ConvertToBase(TFrom from, double quantity)
    {
        if (!conversionRates.TryGetValue(from, out double value))
            throw new Exception($"Конверсия из '{from}' не определена.");

        return value * quantity;
    }
}


