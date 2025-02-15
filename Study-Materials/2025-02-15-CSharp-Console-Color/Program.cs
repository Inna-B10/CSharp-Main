//https://stackoverflow.com/questions/7937256/custom-text-color-in-c-sharp-console-application
using System;
using System.Drawing;
namespace _2025_02_15_CSharp_Console_Color;

class Program
{        //After reset, all colors are set back to default
    public const string RESET = "\x1b[0m";
    public const string ITALIC = "\x1B[3m";
    public const string BOLD = "\x1B[1m";
    public const string UNDERLINE = "\x1B[4m";
    // public const string BLINK = "\x1B[5m";
    // public const string BLINKRAPID = "\x1B[6m";
    public const string DEFAULTFORE = "\x1B[39m";
    public const string DEFAULTBACK = "\x1B[49m";
    public const string REVERSE = "\x1B[7m";
    public const string STRIKETHROUGH = "\x1B[9m";
    public const string GREEN = "\x1B[32m";
    public const string BLUE_BG = "\x1B[44m";

    static void Main(string[] args)
    {

        // Console.WriteLine($"This is {Color.Gold.ToAscii(true)}GOLD text and " +
        //                   $"now with a {Color.Red.ToAscii(false)}RED background{RESET}. " +
        //                   "After reset, all colors are set back to default.");
        // Console.WriteLine($"some text {Color.YellowGreen.ToAscii(false)} in YellowGreen color{RESET} ");

        /* -------------------------------- Variant 1 ------------------------------- */
        Color myColor = Color.Gold; //Использование предопределённых цветов
        Console.WriteLine($"{myColor.ToAscii(false)} Variant 1:Стандартные цвета из System.Drawing.Color{RESET}");

        /* -------------------------------- Variant 2 ------------------------------- */
        Color myColor2 = Color.FromArgb(255, 128, 64);
        Console.WriteLine($"{ITALIC}{myColor2.ToAscii(true)}italic style \x1B[23m + Variant 2: Создание цвета вручную (RGB) {RESET}");

        /* -------------------------------- Variant 3 ------------------------------- */
        Color myColor3 = Color.FromArgb(0, 255, 128, 64); //например для WPF, WinForms, но не консоль!
        Console.WriteLine($"{myColor3.ToAscii(false)} Variant 3: Создание цвета с прозрачностью (ARGB){BOLD}{UNDERLINE}(не консоль!){RESET}");

        /* -------------------------------- Variant 4 ------------------------------- */
        /*В C# нет встроенной функции для парсинга HEX-цветов в System.Drawing.Color, но можно написать свою*/
        Color myColor4 = FromHex("#FF00FF");
        Console.WriteLine($"{myColor4.ToAscii(false)} Variant 4: Создание цвета из HEX-строки, используя свою функции для парсинга HEX-цветов{RESET}");


        Console.WriteLine($"{REVERSE}{STRIKETHROUGH}Инверсия цветов + перечеркнуть{RESET}");

        Console.WriteLine("\x1B[48;5;46m Зелёный фон\x1B[0m");

        Console.WriteLine($"{GREEN}{BLUE_BG}Зелёный текст на синем фоне{RESET}");

        Console.WriteLine($"{UNDERLINE}underlined text\x1B[0m");
    }

    // for Variant 4 для парсинга HEX-цветов
    public static Color FromHex(string hex)
    {
        return ColorTranslator.FromHtml(hex);
    }
}



public static class ColorExt
{
    /// <summary>
    /// Color extension to convert Color to Ascii text to be used within a Console.Write/WriteLine.
    /// </summary>
    public static string ToAscii(this Color clr, bool isForeground)
    {
        var present = isForeground ? 38 : 48;
        return $"\x1b[{present};2;{clr.R};{clr.G};{clr.B}m";
    }
}




