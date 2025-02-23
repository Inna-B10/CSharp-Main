using System.ComponentModel;
using System.Numerics;
using System.Text.RegularExpressions;

namespace _2025_02_21_Simple_Calculator;

class Program
{
    public static char[] validOperand = ['+', '-', '/', '*'];
    static void Main(string[] args)
    {
        string? input;
        bool firstRound = true;
        do
        {
            if (firstRound) firstRound = false; else Console.WriteLine("Invalid input!");
            Console.WriteLine("Please enter a mathematical expression");
            Console.WriteLine("Acceptable operands: [-] [+] [/] [*]");
            input = Console.ReadLine()?.Trim();
        }
        while (!IsValidInput(input));


        input = input?.Replace(" ", "");
        Console.WriteLine(input);




        // double firstNumber = 0;
        //         try
        //         {
        // 
        //             double result = Calculator<double>.Calc(4, 0, '%');
        //             Console.WriteLine(result);
        //         }
        //         catch (Exception ex)
        //         {
        //             Console.WriteLine(ex.Message);
        //             throw;
        //         }
    }

    class Calculator<T>
    where T : INumber<T>
    {
        public static T Calc(T a, T b, char operand)
        {
            return operand switch
            {
                '+' => a + b,
                '-' => a - b,
                '*' => a * b,
                '/' => b != T.Zero ? a / b : throw new Exception("Cannot divide by 0"),
                _ => throw new Exception("Invalid operand.")

            };
        }
    }

    public static bool IsValidInput(string? expression)
    {
        if (string.IsNullOrEmpty(expression)) return false;
        if (Regex.IsMatch(expression, "[+\\-*/]{2,}")) return false;
        if (Regex.IsMatch(expression, "^[+\\-*/]")) return false;
        if (Regex.IsMatch(expression, "[+\\-*/]$")) return false;

        return true;
    }

}
