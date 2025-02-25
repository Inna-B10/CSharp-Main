// namespace _2025_02_25_UnitConverter;
// 
// class Program
// {
//   public enum LengthUnits
//   {
//     Meters, Kilometers, Miles, feet
//   }
//   public static Dictionary<LengthUnits, double> length = new()
//         {
//             {LengthUnits.Meters, 1},
//             { LengthUnits.Kilometers, 1000},
//             { LengthUnits.Miles, 1609.35},
//             { LengthUnits.feet, 0.3048}
//         };
//   static void Main()
//   {
//     var lengthConverter = new UnitConverter<LengthUnits>(length);
// 
//     Console.WriteLine("11 миль в километрах: " + lengthConverter.ConvertDirect(LengthUnits.Miles, LengthUnits.Kilometers, 11));
//   }
// }
// 
// public class UnitConverter<T>
// where T : Enum
// {
// 
//   private readonly Dictionary<T, double> conversionRates;
//   public UnitConverter(Dictionary<T, double> rates)
//   {
// 
//     if (rates == null)
//       throw new ArgumentNullException(nameof(rates));
// 
//     conversionRates = rates;
//   }
//   // readonly Dictionary<T, double> conversionRates = rates ?? throw new ArgumentNullException(nameof(rates));
// 
//   public double ConvertToBase(T from, double amount)
//   {
//     if (!conversionRates.TryGetValue(from, out _))
//     { throw new Exception("Invalid unit provided for conversion."); }
// 
//     return amount * conversionRates[from];
//   }
// 
//   public double ConvertFromBase(T to, double amount)
//   {
//     if (!conversionRates.TryGetValue(to, out _))
//     { throw new Exception("Invalid unit provided for conversion."); }
// 
//     return amount / conversionRates[to];
//   }
// 
//   public double ConvertDirect(T from, T to, double amount)
//   {
//     //amount * (fromRate / toRate)
//     double baseValue = ConvertToBase(from, amount);
//     return ConvertFromBase(to, baseValue);
//   }
// }
// 
