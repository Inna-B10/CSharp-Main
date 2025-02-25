using System.Globalization;
using System.Text.RegularExpressions;

namespace _2025_02_25_UnitConverter;

partial class Program
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
    var culture = new CultureInfo("no-NO");

    // If the fractional part == 0, display without the decimal part, otherwise with two signs
    return value % 1 == 0
        ? value.ToString("N0", culture)  // "N0" -> format with thousands separators, no decimal part
        : value.ToString("G", culture); // "N2" -> 2 decimal places (can be changed to "G" for auto)
  }

  static void Main()
  {

    Console.WriteLine("Hello, user!");
    UserChoice();

    //         try
    //         {
    //             var lengthConverter = new UnitConverter<LengthUnits>(length);
    //             Console.WriteLine("2 miles in meters: " + FormatNumber(lengthConverter.ConvertToBase(LengthUnits.miles, 2)));
    //             Console.WriteLine("100 meters in feet: " + FormatNumber(lengthConverter.ConvertFromBase(LengthUnits.feet, 100)));
    //             Console.WriteLine("5 kilometers in miles: " + FormatNumber(lengthConverter.ConvertDirect(LengthUnits.kilometers, LengthUnits.miles, 5)));
    // 
    //             var currencyConverter = new UnitConverter<CurrencyUnits>(currency);
    //             double result = currencyConverter.ConvertCurrencies(CurrencyUnits.EUR, CurrencyUnits.NOK, 15);
    //             string convertedValue = FormatNumber(result);
    //             Console.WriteLine("15 Euro in Norwegian Kroner : " + convertedValue);
    //         }
    //         catch (ArgumentNullException arNull)
    //         {
    //             Console.WriteLine("Error: " + arNull.Message);
    //         }
    //         catch (Exception ex)
    //         {
    //             Console.WriteLine("Error: " + ex.Message);
    //         }

  }
  public static string ConvertUnitName(string part)
  {
    return part switch
    {
      "m" => "meters",
      "k" => "kilometers",
      "l" => "miles",
      "f" => "feet",
      _ => ""
    };
  }
  public static string[] ParseInput(string expression, string type)
  {
    expression = expression.Replace(",", ".");
    // var match = Regex.Match(expression, @"^(\d+(\.\d+)?)([A-Za-z]+)=([A-Za-z]+)$");
    var match = ExpressionRegex().Match(expression);
    string amount = match.Groups[1].Value;
    string from = type == "length" ? ConvertUnitName(match.Groups[3].Value) : match.Groups[3].Value;
    string to = type == "length" ? ConvertUnitName(match.Groups[4].Value) : match.Groups[4].Value;

    return [amount, from, to];
  }
  public static void LengthConverter()
  {
    Console.WriteLine("4 values are available for conversion");
    Console.WriteLine("[m] meters, [k] kilometers, [l] miles, [f] feet");
    Console.WriteLine("valid format: from=to, f.ex to convert 5 kilometers to meters write 5k=m");

    string userInput = GetValidInput("length");
    string[] expressionParts = ParseInput(userInput, "length");
    if (
    Enum.TryParse(expressionParts[1], true, out LengthUnits fromUnit) &&
    Enum.TryParse(expressionParts[2], true, out LengthUnits toUnit) &&
    double.TryParse(expressionParts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double amount))
    {
      try
      {
        var lengthConverter = new UnitConverter<LengthUnits>(length);
        string result = FormatNumber(lengthConverter.ConvertDirect(fromUnit, toUnit, amount));
        Console.WriteLine($"{FormatNumber(amount)} {fromUnit} in {toUnit}: {result}");
      }
      catch (ArgumentNullException arNull)
      {
        Console.WriteLine("Error: " + arNull.Message);
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error: " + ex.Message);
      }
    }
    else
    {
      Console.WriteLine("Unexpected error during conversion.");
      return;
    }
  }

  public static void CurrencyConverter()
  {
    Console.WriteLine("Currencies available for conversion: USD, EUR, NOK, ILS, GBP");
    Console.WriteLine("Valid format: from=to, f.ex to convert 10 eur to nok write 10eur=nok");
    string userInput = GetValidInput("currency");
    Console.WriteLine(userInput);
    string[] expressionParts = ParseInput(userInput, "currency");
    if (
      Enum.TryParse(expressionParts[1], true, out CurrencyUnits fromUnit) &&
      Enum.TryParse(expressionParts[2], true, out CurrencyUnits toUnit) &&
      double.TryParse(expressionParts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double amount))
    {
      try
      {
        var currencyConverter = new UnitConverter<CurrencyUnits>(currency);
        string result = FormatNumber(currencyConverter.ConvertCurrencies(fromUnit, toUnit, amount));
        Console.WriteLine($"{FormatNumber(amount)} {fromUnit} in {toUnit}: {result}");
      }
      catch (ArgumentNullException arNull)
      {
        Console.WriteLine("Error: " + arNull.Message);
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error: " + ex.Message);
      }
    }
  }

  public static void UserChoice()
  {
    Console.WriteLine("What do you want to convert? [1] length, [2] currency");
    string choice = GetValidInput("choice");

    if (choice == "error")
    {
      Console.WriteLine("Some error occurred. The program is terminating.");
      return;
    }
    else if (choice == "length")
    {
      LengthConverter();
    }
    else
      CurrencyConverter();
  }
  public static string GetValidInput(string type)
  {
    string? input;
    do
    {
      input = Console.ReadLine();
      input = input?.Replace(" ", "");
      if (!IsValidInput(input, type))
      {
        Console.WriteLine(GetErrorMessage(type));
        input = null;
      }
    } while (input == null);

    if (type == "choice")
    {
      return input switch
      {
        "1" => "length",
        "2" => "currency",
        _ => "error"
      };
    }
    return input;
  }
  public static bool IsCurrencyInputValid(string? input)
  {
    if (string.IsNullOrWhiteSpace(input)) return false;

    string[] parts = input.Split("=");
    if (parts.Length != 2) return false;

    return IsValidFormat(parts[0]) && Enum.TryParse(parts[1], true, out CurrencyUnits _);
  }
  private static bool IsValidFormat(string part)
  {
    // var match = Regex.Match(part, @"^(\d+(\.\d+)?)([A-Za-z]+)$");
    var match = isAmountExistRegex().Match(part);

    if (!match.Success) return false;

    string currencyCode = match.Groups[3].Value;
    return Enum.TryParse(currencyCode, true, out CurrencyUnits _);
  }
  public static bool IsValidInput(string? input, string type)
  {
    return type switch
    {
      "choice" => !string.IsNullOrWhiteSpace(input) && "12".Contains(input),
      // "length" => !string.IsNullOrWhiteSpace(input) && Regex.IsMatch(input, @"^(\d+([.,]\d+)?)[mfklMFKL]=[mfklMFKL]$"),
      "length" => !string.IsNullOrWhiteSpace(input) && LengthRegex().IsMatch(input),
      "currency" => IsCurrencyInputValid(input),
      _ => false
    };
  }
  public static string GetErrorMessage(string type)
  {
    return type switch
    {
      "choice" => "Invalid input!Press 1 for length converting or 2 for currency converting",
      "length" => "Invalid input! Available length: [m] meters, [k] kilometers, [l] miles, [f] feet. Valid format: 5k=m",
      "currency" => "Invalid input! Available currency: USD, EUR, NOK, ILS, GBP. Valid format: 1000nok=usd",
      _ => "Unknown error"
    };
  }

  [GeneratedRegex(@"^(\d+([.,]\d+)?)[mfklMFKL]=[mfklMFKL]$")]
  private static partial Regex LengthRegex();

  [GeneratedRegex(@"^(\d+([.,]\d+)?)([A-Za-z]+)$")]
  private static partial Regex isAmountExistRegex();
  [GeneratedRegex(@"^(\d+(\.\d+)?)([A-Za-z]+)=([A-Za-z]+)$")]
  private static partial Regex ExpressionRegex();
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
  public double ConvertCurrencies(T from, T to, double amount)
  {
    if (!conversionRates.TryGetValue(from, out _) || (!conversionRates.TryGetValue(to, out _)))
      throw new Exception($"Conversion for '{from}' or/and '{to}' is undefined.");

    return amount * (conversionRates[to] / conversionRates[from]);
  }
}


