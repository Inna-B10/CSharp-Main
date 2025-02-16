namespace _2025_02_16_Customisert_God_Morgen;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Please, enter your name");
        string userName = GetValidInput("name");
        Console.WriteLine($"Welcome {userName}!");
        DateTime date = DateTime.Now;
        GetMessage(date.Hour);
        Console.WriteLine("What genre do you prefer?");
        Console.WriteLine("Rock, pop, classic, electronic");
        string genre = GetValidInput("genre");
        Console.WriteLine(genre);
    }

    public static void GetMessage(int time)
    {

        switch (time)
        {
            case > 17:
                Console.WriteLine("Winding down?");
                Console.WriteLine("Relaxing music helps create an atmosphere of comfort and harmony in the evening.");
                break;
            case > 12:
                Console.WriteLine("Boosting your day?");
                Console.WriteLine("Well-chosen melodies enhance productivity throughout the workday.");
                break;
            case > 5:
                Console.WriteLine("Starting the day with energy?");
                Console.WriteLine("Energetic music is the perfect way to start the day with vigor and motivation.");
                break;
            case > 0:
                Console.WriteLine("Late-night tunes?");
                Console.WriteLine("Soft, calming tunes prepare your mind for a good night's sleep.");
                break;
        }

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
