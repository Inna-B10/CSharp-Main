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
        USD, EUR, NOK, ILS, GBP
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
            { CurrencyUnits.NOK, 11.10 },
            { CurrencyUnits.ILS, 3.59 },
            { CurrencyUnits.GBP, 0.79 },

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
            Console.WriteLine("2 miles in meters: " + FormatNumber(lengthConverter.ConvertToBase(LengthUnits.miles, 2)));
            Console.WriteLine("100 meters in feet: " + FormatNumber(lengthConverter.ConvertFromBase(LengthUnits.feet, 100)));
            Console.WriteLine("5 kilometers in miles: " + FormatNumber(lengthConverter.ConvertDirect(LengthUnits.kilometers, LengthUnits.miles, 5)));

            var currencyConverter = new UnitConverter<CurrencyUnits>(currency);
            double result = currencyConverter.ConvertCurrency(CurrencyUnits.EUR, CurrencyUnits.NOK, 15);
            string convertedValue = FormatNumber(result);
            Console.WriteLine("15 Euro in Norwegian Kroner : " + convertedValue);
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

public class UnitConverter<T>(Dictionary<T, double> rates)
where T : Enum
{
    private readonly Dictionary<T, double> conversionRates = rates ?? throw new ArgumentNullException(nameof(rates));

    public double ConvertToBase(T from, double amount)
    {
        if (!conversionRates.TryGetValue(from, out double value))
            throw new Exception($"Conversion for '{from}' is undefined.");

        return amount * value;
    }
    public double ConvertFromBase(T to, double amount)
    {
        if (!conversionRates.TryGetValue(to, out double value))
            throw new Exception($"Conversion for '{to}' is undefined.");

        return amount / value;
    }
    public double ConvertDirect(T from, T to, double amount)
    {
        if (!conversionRates.TryGetValue(from, out _) || (!conversionRates.TryGetValue(to, out _)))
            throw new Exception($"Conversion for '{from}' or/and '{to}' is undefined.");

        double baseValue = ConvertToBase(from, amount);
        return ConvertFromBase(to, baseValue);
    }
    public double ConvertCurrency(T from, T to, double amount)
    {
        if (!conversionRates.TryGetValue(from, out _) || (!conversionRates.TryGetValue(to, out _)))
            throw new Exception($"Conversion for '{from}' or/and '{to}' is undefined.");

        return amount * (conversionRates[to] / conversionRates[from]);
    }


}


