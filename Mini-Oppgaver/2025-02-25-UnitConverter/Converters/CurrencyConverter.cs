using System.Globalization;
using _2025_02_25_UnitConverter.Utils;

namespace _2025_02_25_UnitConverter.Converters;

public static class CurrencyConverter
{
  public static readonly Dictionary<CurrencyUnits, double> currency = new()
        {
            { CurrencyUnits.USD, 1 },
            { CurrencyUnits.EUR, 0.95 },
            { CurrencyUnits.NOK, 11.10 },
            { CurrencyUnits.ILS, 3.59 },
            { CurrencyUnits.GBP, 0.79 },
        };

  /* ---------------------------- CurrencyConverter --------------------------- */
  public static void Converter()
  {
    Console.WriteLine("Available currencies: USD, EUR, NOK, ILS, GBP");
    Console.WriteLine("Valid format: from=to, f.ex to convert 10 eur to nok write 10eur=nok");

    string userInput = InputHandler.GetValidInput("currency");
    string[] expressionParts = InputHandler.ParseInput(userInput, "currency");

    if (
      Enum.TryParse(expressionParts[1], true, out CurrencyUnits fromUnit) &&
      Enum.TryParse(expressionParts[2], true, out CurrencyUnits toUnit) &&
      double.TryParse(expressionParts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double amount))
    {
      try
      {
        var currencyConverter = new UnitConverter<CurrencyUnits>(currency);
        string result = Formatter.FormatNumber(currencyConverter.ConvertCurrencies(fromUnit, toUnit, amount));
        Console.WriteLine($"{Formatter.FormatNumber(amount)} {fromUnit} in {toUnit}: {result}");
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error: " + ex.Message);
      }
    }
  }
}