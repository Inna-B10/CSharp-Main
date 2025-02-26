using System.Globalization;

namespace _2025_02_25_UnitConverter.Utils;

public static class Formatter
{
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