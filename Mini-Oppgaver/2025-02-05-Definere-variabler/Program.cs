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
        // string userNum = Console.ReadLine();
    }
}
