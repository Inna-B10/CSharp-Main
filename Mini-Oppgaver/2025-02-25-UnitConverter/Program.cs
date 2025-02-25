using System.Globalization;
using System.Text.RegularExpressions;

namespace _2025_02_25_UnitConverter;

partial class Program
{
  /* ---------------------------------- Regex --------------------------------- */
  [GeneratedRegex(@"^(\d+([.,]\d+)?)[mfklMFKL]=[mfklMFKL]$")]
  private static partial Regex LengthRegex();

  [GeneratedRegex(@"^(\d+([.,]\d+)?)([A-Za-z]+)$")]
  private static partial Regex isAmountExistRegex();
  [GeneratedRegex(@"^(\d+(\.\d+)?)([A-Za-z]+)=([A-Za-z]+)$")]
  private static partial Regex ExpressionRegex();
  /* ---------------------------------- Enums --------------------------------- */
  public enum LengthUnits
  {
    meters, kilometers, miles, feet
  }
  public enum CurrencyUnits
  {
    USD, EUR, NOK, ILS, GBP
  }
  /* ------------------------------ Dictionaries ------------------------------ */
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
  /* ---------------------------------- Main ---------------------------------- */
  static void Main()
  {
    Console.WriteLine("Hello, user!");
    string choice = GetUserChoice();

    if (choice == "error")
    {
      Console.WriteLine("Some error occurred. The program is terminating.");
      return;
    }
    if (choice == "length") LengthConverter();

    if (choice == "currency") CurrencyConverter();
  }
  /* ------------------------------ GetUserChoice ----------------------------- */
  public static string GetUserChoice()
  {
    Console.WriteLine("What do you want to convert? [1] length, [2] currency");
    return GetValidInput("choice");
  }
  /* ----------------------------- LengthConverter ---------------------------- */
  public static void LengthConverter()
  {
    Console.WriteLine("4 values are available for conversion");
    Console.WriteLine("[m] meters, [k] kilometers, [l] miles, [f] feet");
    Console.WriteLine("valid format: from=to, f.ex to convert 5 kilometers to meters write 5k=m");
    // Converter(choice);

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
  /* ---------------------------- CurrencyConverter --------------------------- */
  public static void CurrencyConverter()
  {
    Console.WriteLine("Currencies available for conversion: USD, EUR, NOK, ILS, GBP");
    Console.WriteLine("Valid format: from=to, f.ex to convert 10 eur to nok write 10eur=nok");
    // Converter(choice);
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
      catch (Exception ex)
      {
        Console.WriteLine("Error: " + ex.Message);
      }
    }
  }
  /* ------------------------------ GetValidInput ----------------------------- */
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
  /* ----------------------------- GetErrorMessage ---------------------------- */
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
  /* ------------------------------ IsValidInput ------------------------------ */
  public static bool IsValidInput(string? input, string type)
  {
    return type switch
    {
      "choice" => !string.IsNullOrWhiteSpace(input) && "12".Contains(input),
      //NB "length" => !string.IsNullOrWhiteSpace(input) && Regex.IsMatch(input, @"^(\d+([.,]\d+)?)[mfklMFKL]=[mfklMFKL]$"),
      "length" => !string.IsNullOrWhiteSpace(input) && LengthRegex().IsMatch(input),
      "currency" => IsCurrencyInputValid(input),
      _ => false
    };
  }
  /* -------------------------- IsCurrencyInputValid -------------------------- */
  public static bool IsCurrencyInputValid(string? input)
  {
    if (string.IsNullOrWhiteSpace(input)) return false;

    string[] parts = input.Split("=");
    if (parts.Length != 2) return false;

    return IsValidFormat(parts[0]) && Enum.TryParse(parts[1], true, out CurrencyUnits _);
  }
  /* ------------------------------ IsValidFormat ----------------------------- */
  private static bool IsValidFormat(string part)
  {
    //NB var match = Regex.Match(part, @"^(\d+(\.\d+)?)([A-Za-z]+)$");
    var match = isAmountExistRegex().Match(part);

    if (!match.Success) return false;

    string currencyCode = match.Groups[3].Value;
    return Enum.TryParse(currencyCode, true, out CurrencyUnits _);
  }
  /* ------------------------------- ParseInput ------------------------------- */
  public static string[] ParseInput(string expression, string type)
  {
    expression = expression.Replace(",", ".");
    //NB var match = Regex.Match(expression, @"^(\d+(\.\d+)?)([A-Za-z]+)=([A-Za-z]+)$");
    var match = ExpressionRegex().Match(expression);
    string amount = match.Groups[1].Value;
    string from = type == "length" ? ConvertUnitName(match.Groups[3].Value) : match.Groups[3].Value;
    string to = type == "length" ? ConvertUnitName(match.Groups[4].Value) : match.Groups[4].Value;

    return [amount, from, to];
  }
  /* ----------------------------- ConvertUnitName ---------------------------- */
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
  /* ------------------------------ FormatNumber ------------------------------ */
  public static string FormatNumber(double value)
  {
    var culture = new CultureInfo("no-NO");

    // If the fractional part == 0, display without the decimal part
    return value % 1 == 0
        ? value.ToString("N0", culture)  //NB "N0" -> format with thousands separators, no decimal part
        : value.ToString("G", culture);  //NB "N2" or"N3" -> 2 or 3 decimal places ("G" for auto)
  }
}
/* --------------------------- Class UnitConverter -------------------------- */
public class UnitConverter<T>(Dictionary<T, double> rates)
where T : Enum
{
  private readonly Dictionary<T, double> conversionRates = rates ?? throw new Exception("Error: Value cannot be null. (Parameter 'rates')");
  /*  ConvertToBase ----------------------------- */
  public double ConvertToBase(T from, double amount)
  {
    if (!conversionRates.TryGetValue(from, out double value))
      throw new Exception($"Conversion for '{from}' is undefined.");

    return amount * value;
  }
  /*  ConvertFromBase ---------------------------- */
  public double ConvertFromBase(T to, double amount)
  {
    if (!conversionRates.TryGetValue(to, out double value))
      throw new Exception($"Conversion for '{to}' is undefined.");

    return amount / value;
  }
  /*  ConvertDirect ----------------------------- */
  public double ConvertDirect(T from, T to, double amount)
  {
    if (!conversionRates.TryGetValue(from, out _) || (!conversionRates.TryGetValue(to, out _)))
      throw new Exception($"Conversion for '{from}' or/and '{to}' is undefined.");

    double baseValue = ConvertToBase(from, amount);
    return ConvertFromBase(to, baseValue);
  }
  /*  ConvertCurrencies --------------------------- */
  public double ConvertCurrencies(T from, T to, double amount)
  {
    if (!conversionRates.TryGetValue(from, out _) || (!conversionRates.TryGetValue(to, out _)))
      throw new Exception($"Conversion for '{from}' or/and '{to}' is undefined.");

    return amount * (conversionRates[to] / conversionRates[from]);
  }
}


