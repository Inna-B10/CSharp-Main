using System.Globalization;

namespace _2025_02_25_UnitConverter;

class Program
{
    public enum LengthUnits
    {
        meters, kilometers, miles, feet
    }
    public enum CurrencyUnits
    {
        USD, EUR, NOK
    }
    public static Dictionary<LengthUnits, double> length = new()
        {
            { LengthUnits.meters, 1 },
            { LengthUnits.kilometers, 1000 },
            { LengthUnits.miles, 1609.34 },
            { LengthUnits.feet, 0.3048 }
        };
    public static Dictionary<CurrencyUnits, double> currency = new()
        {
            { CurrencyUnits.USD, 1 },
            { CurrencyUnits.EUR, 0.95 },
            { CurrencyUnits.NOK, 11.10 }
        };
    public static string FormatNumber(double value)
    {
        return value.ToString("n", new CultureInfo("no-NO"));
    }
    static void Main()
    {
        try
        {
            var lengthConverter = new UnitConverter<LengthUnits>(length);
            Console.WriteLine("10 miles in meters: " + FormatNumber(lengthConverter.ConvertToBase(LengthUnits.miles, 10)));
            Console.WriteLine("5 kilometers in meters: " + FormatNumber(lengthConverter.ConvertToBase(LengthUnits.kilometers, 5)));

            var currencyConverter = new UnitConverter<CurrencyUnits>(currency);
            double result = currencyConverter.ConvertToBase(CurrencyUnits.NOK, 100);
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
            throw new Exception("Invalid unit provided for conversion.");

        return value * quantity;
    }
}


