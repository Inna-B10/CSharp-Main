using System.Globalization;
using _2025_02_25_UnitConverter.Utils;

namespace _2025_02_25_UnitConverter.Converters;

public static class LengthConverter
{
  public static readonly Dictionary<LengthUnits, double> length = new()
        {
            { LengthUnits.meters, 1 },
            { LengthUnits.kilometers, 1000 },
            { LengthUnits.miles, 1609.34 },
            { LengthUnits.feet, 0.3048 }
        };

  /* ----------------------------- LengthConverter ---------------------------- */
  public static void Converter()
  {
    Console.WriteLine("Available: [m] meters, [k] kilometers, [l] miles, [f] feet");
    Console.WriteLine("valid format: from=to, f.ex to convert 5 kilometers to meters write 5k=m");

    string userInput = InputHandler.GetValidInput("length");
    string[] expressionParts = InputHandler.ParseInput(userInput, "length");

    if (
    Enum.TryParse(expressionParts[1], true, out LengthUnits fromUnit) &&
    Enum.TryParse(expressionParts[2], true, out LengthUnits toUnit) &&
    double.TryParse(expressionParts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double amount))
    {
      try
      {
        var lengthConverter = new UnitConverter<LengthUnits>(length);
        string result = Formatter.FormatNumber(lengthConverter.ConvertDirect(fromUnit, toUnit, amount));
        Console.WriteLine($"{Formatter.FormatNumber(amount)} {fromUnit} in {toUnit}: {result}");
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
}