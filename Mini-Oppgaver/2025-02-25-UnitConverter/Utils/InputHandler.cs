using System.Text.RegularExpressions;

namespace _2025_02_25_UnitConverter.Utils;

public static partial class InputHandler
{
  [GeneratedRegex(@"^(\d+(\.\d+)?)([A-Za-z]+)=([A-Za-z]+)$")]
  private static partial Regex ExpressionRegex();

  /* ------------------------------ GetUserChoice ----------------------------- */
  public static string GetUserChoice()
  {
    Console.WriteLine("What do you want to convert? [1] length, [2] currency");
    return GetValidInput("choice");
  }

  /* ------------------------------ GetValidInput ----------------------------- */
  public static string GetValidInput(string type)
  {
    string? input;
    do
    {
      input = Console.ReadLine()?.Replace(" ", "");
      if (!Validation.IsValidInput(input, type))
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

  /* ------------------------------- ParseInput ------------------------------- */
  public static string[] ParseInput(string expression, string type)
  {
    expression = expression.Replace(",", ".");
    //NB  var match = Regex.Match(expression, @"^(\d+(\.\d+)?)([A-Za-z]+)=([A-Za-z]+)$");
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
}