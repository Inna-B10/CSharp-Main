namespace _2025_02_25_UnitConverter.Converters;

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