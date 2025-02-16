namespace _2025_02_16_Customisert_God_Morgen;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Please, enter your name");
        string userName = GetValidInput("name");
        Console.WriteLine($"Welcome {userName}!");
    }

    public static string GetValidInput(string type = "name")
    {
        string? input = Console.ReadLine()?.Trim();

        while (string.IsNullOrEmpty(input))
        {
            switch (type)
            {
                case "name":
                    Console.WriteLine("Invalid input! Please, enter your name");
                    input = Console.ReadLine()?.Trim();
                    break;
                case "genre":
                    Console.WriteLine("Invalid input! Please select one of the genres:");
                    Console.WriteLine("Rock, Pop, Classic or Electronic");
                    input = Console.ReadLine()?.Trim();
                    break;
            }
        }
        return input;
    }
}
