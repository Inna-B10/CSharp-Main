using System;
using System.Globalization;

namespace _2025_02_05_Definere_variabler;

class Program
{
    static decimal num1 = 1.99999999999m;
    static decimal num2 = 2.99999999999999999m;
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, and welcome to the program!");

        int catAge = 3;
        Console.WriteLine("My cat's name is Kåre. He has the following age: {0}", catAge);
        Console.WriteLine("-----------------------------------------------------");

        /* ----------------------------- Sum Two Numbers ---------------------------- */
        Console.WriteLine($"I have the following numbers:  {num1} and {num2}");
        Console.WriteLine("I want to sum these numbers as:");
        Console.WriteLine();

        Sum();
        Console.WriteLine();

        Sum(1);
        Console.WriteLine();

        Sum(2);
        Console.WriteLine();

        Sum(3);
        Console.WriteLine();

        Sum(4);
        Console.WriteLine();

        /* -------------------------- Get Numbers From User ------------------------- */
        Console.WriteLine("-----------------------------------------------------");
        Console.WriteLine("enter your first number");
        string userNum1 = Console.ReadLine();

        object result1 = ConvertNumber(userNum1);

        Console.WriteLine("enter your second number");
        string userNum2 = Console.ReadLine();

        object result2 = ConvertNumber(userNum2);

        Console.WriteLine($"Type: {result1.GetType()}, Value: {result1}");
        Console.WriteLine($"Type: {result2.GetType()}, Value: {result2}");

        Console.WriteLine();
        if (result1.GetType() != typeof(string) && result2.GetType() != typeof(string))
        {
            // func. ConvertNumber() return object, first convert!
            // decimal sum = Convert.ToDecimal(result1) + Convert.ToDecimal(result2);

            decimal sum = (decimal)result1 + (decimal)result2;
            Console.WriteLine($"Sum of {result1} and {result2} is: {sum}");
        }
        else
        {
            Console.WriteLine("Cannot operate with non-number types!");
        }



        static void Sum(int type = 0)
        {
            switch (type)
            {
                case 1:
                    Console.WriteLine("float, float");
                    Console.Write($"sum of {(float)num1} and {(double)num2}");
                    Console.WriteLine(" = {0}", ((float)num1) + ((double)num2));
                    Console.WriteLine($"the type of sum is {(((float)num1) + ((double)num2)).GetType()}");
                    break;

                case 2:
                    Console.WriteLine("double, double");
                    Console.Write($"sum of {(double)num1} and {(double)num2}");
                    Console.WriteLine(" = {0}", ((double)num1) + ((double)num2));
                    Console.WriteLine($"the type of sum is {(((double)num1) + ((double)num2)).GetType()}");
                    break;
                case 3:
                    Console.WriteLine("float, double");
                    Console.Write($"sum of {(float)num1} and {(double)num2}");
                    Console.WriteLine(" = {0}", ((float)num1) + ((double)num2));
                    Console.WriteLine($"the type of sum is {(((float)num1) + ((double)num2)).GetType()}");
                    break;
                case 4:
                    Console.WriteLine("double, float");
                    Console.Write($"sum of {(double)num1} and {(float)num2}");
                    Console.WriteLine(" = {0}", ((double)num1) + ((float)num2));
                    Console.WriteLine($"the type of sum is {(((double)num1) + ((float)num2)).GetType()}");
                    break;
                default:
                    Console.WriteLine("decimal, decimal");
                    Console.Write($"sum of {num1} and {num2}");
                    Console.WriteLine(" = {0}", num1 + num2);
                    Console.WriteLine($"the type of sum is {(num1 + num2).GetType()}");
                    break;

            }
        }

        static object ConvertNumber(string userNum)
        {
            // Convert comma to dot if exists (42,5 -> 42.5)
            if (userNum.Contains(','))
                userNum = userNum.Replace(',', '.');

            //NB NumberStyles.Any, CultureInfo.InvariantCulture,
            /* Pass CultureInfo.InvariantCulture to TryParse.
            By default, TryParse uses the OS regional settings, but we can explicitly specify the English format(where the separator is a period .) for numbers like 42.5 or 42,5 */

            //first check if it is number
            if (decimal.TryParse(userNum, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal decimalNumber))
            {
                /*//check if it is possible to convert decimal to something else
                if (int.TryParse(userNum, NumberStyles.Any, CultureInfo.InvariantCulture, out int intNumber))
                    return intNumber;
                if (long.TryParse(userNum, NumberStyles.Any, CultureInfo.InvariantCulture, out long longNumber))
                    return longNumber;
                if (float.TryParse(userNum, NumberStyles.Any, CultureInfo.InvariantCulture, out float floatNumber))
                    return floatNumber;
                if (double.TryParse(userNum, NumberStyles.Any, CultureInfo.InvariantCulture, out double doubleNumber))
                    return doubleNumber;*/

                return decimalNumber; //if not int/long/float/double
            }

            return "not a number";
        }
    }
}
