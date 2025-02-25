// namespace _2025_02_25_UnitConverter;
// 
// 
// 
// public class UnitConverter<TFrom>(Dictionary<TFrom, double> rates)
// where TFrom : notnull
// {
// 
//     private readonly Dictionary<TFrom, double> conversionRates = rates ?? throw new ArgumentNullException(nameof(rates));
// 
//     public double ConvertToMeters(TFrom from, double amount)
//     {
//         // foreach (var kvp in rates)
//         // {
//         //     Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}"); // Выводит содержимое словаря
//         // }
// 
//         if (!conversionRates.TryGetValue(from, out double value))
//             throw new ArgumentException($"Конверсия из '{from}' не определена.");
// 
//         return value * amount;
//     }
// }
// 
// class Program
// {
//     public static Dictionary<string, double> LengthUnits = new()
//         {
//             { "meters", 1 },
//             { "kilometers", 1000 },
//             { "miles", 1609.34 },
//             { "feet", 0.3048 }
//         };
//     static void Main()
//     {
//         var lengthConverter = new UnitConverter<string>(LengthUnits);
// 
//         Console.WriteLine("10 миль в метрах: " + lengthConverter.ConvertToMeters("miles", 10));
//         Console.WriteLine("5 километров в метрах: " + lengthConverter.ConvertToMeters("kilometers", 5));
//     }
// }
// 
// 
