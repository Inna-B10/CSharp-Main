using _2025_02_25_UnitConverter.Converters;
using _2025_02_25_UnitConverter.Utils;

namespace _2025_02_25_UnitConverter;

partial class Program
{
  /* ---------------------------------- Main ---------------------------------- */
  static void Main()
  {
    Console.WriteLine("Hello, user!");
    string choice = InputHandler.GetUserChoice();

    if (choice == "error")
    {
      Console.WriteLine("Some error occurred. The program is terminating.");
      return;
    }
    if (choice == "length") LengthConverter.Converter();

    if (choice == "currency") CurrencyConverter.Converter();
  }
}



