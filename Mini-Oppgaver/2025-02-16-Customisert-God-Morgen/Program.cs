namespace _2025_02_16_Customisert_God_Morgen;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Please, enter your name");
        string userName = GetValidInput("name");
        Console.WriteLine($"Welcome {userName}!");
        DateTime date = DateTime.Now;
        string now = GetTimesOfDay(date.Hour);
        Console.WriteLine("What genre do you prefer?");
        Console.WriteLine("Rock, pop, classic, electronic");
        Console.WriteLine("Enter r for `Rock`, p for `pop`, c for `classic`, e for `electronic` or f to exit the program");

        string genre = GetValidInput("genre");
        Console.WriteLine(genre);
    }

    public static string GetTimesOfDay(int time)
    {
        string nowIs = "";
        switch (time)
        {
            case > 17:
                nowIs = "evening";
                Console.WriteLine("Winding down?");
                Console.WriteLine("Relaxing music helps create an atmosphere of comfort and harmony in the evening.");
                break;
            case > 12:
                nowIs = "day";
                Console.WriteLine("Boosting your day?");
                Console.WriteLine("Well-chosen melodies enhance productivity throughout the workday.");
                break;
            case > 5:
                nowIs = "morning";
                Console.WriteLine("Starting the day with energy?");
                Console.WriteLine("Energetic music is the perfect way to start the day with vigor and motivation.");
                break;
            case > 0:
                nowIs = "night";
                Console.WriteLine("Late-night tunes?");
                Console.WriteLine("Soft, calming tunes prepare your mind for a good night's sleep.");
                break;
        }
        return nowIs;
    }
    public static string GetValidInput(string type = "name")
    {
        string? input = Console.ReadLine()?.Trim();

        switch (type)
        {
            case "name":
                while (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Invalid input! Please, enter your name");
                    input = Console.ReadLine()?.Trim();
                }
                return input;

            case "genre":

                while (string.IsNullOrEmpty(input) || !"rpcef".Contains(input.ToLower()))
                {
                    Console.WriteLine("Invalid input! Please select one of the genres:");
                    Console.WriteLine("Enter r for `Rock`, p for `Pop`, c for `Classic`, e for `Electronic` or f to Exit the program");
                    input = Console.ReadLine()?.Trim();
                }
                return input.ToLower() switch
                {
                    "r" => "rock",
                    "p" => "pop",
                    "c" => "classic",
                    "e" => "electronic",
                    _ => "f"
                };
            default:
                throw new ArgumentException("Unknown input type");
        }
    }
}

