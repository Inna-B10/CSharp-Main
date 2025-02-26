using System.Text.RegularExpressions;
namespace _2025_02_25_UnitConverter.Utils;

public static partial class Validation
{
  [GeneratedRegex(@"^(\d+([.,]\d+)?)[mfklMFKL]=[mfklMFKL]$")]
  private static partial Regex LengthRegex();

  [GeneratedRegex(@"^(\d+([.,]\d+)?)([A-Za-z]+)$")]
  private static partial Regex AmountExistRegex();

  /* ------------------------------ IsValidInput ------------------------------ */
  public static bool IsValidInput(string? input, string type)
  {
    return type switch
    {
      "choice" => !string.IsNullOrWhiteSpace(input) && "12".Contains(input),
      //NB  "length" => !string.IsNullOrWhiteSpace(input) && Regex.IsMatch(input, @"^(\d+([.,]\d+)?)[mfklMFKL]=[mfklMFKL]$"),
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
    var match = AmountExistRegex().Match(part);

    if (!match.Success) return false;

    string currencyCode = match.Groups[3].Value;
    return Enum.TryParse(currencyCode, true, out CurrencyUnits _);
  }
}