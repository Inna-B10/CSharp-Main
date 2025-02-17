namespace _2025_02_16_Customisert_God_Morgen;

class Program
{
    public static Dictionary<string, Dictionary<string, string>> musicDictionary = new()
{
    { "morning", new() { { "rock", "AC/DC - Highway to Hell" }, { "pop", "Dua Lipa - Don't Start Now" },
                          { "classic", "Vivaldi - Spring" }, { "electronic", "The Prodigy - Firestarter" } } },

    { "day", new() { { "rock", "Pink Floyd - Comfortably Numb" }, { "pop", "Ed Sheeran - Photograph" },
                     { "classic", "Bach - Air on the G String" }, { "electronic", "Daft Punk - Something About Us" } } },

    { "evening", new() { { "rock", "Radiohead - No Surprises" }, { "pop", "Adele - Someone Like You" },
                         { "classic", "Debussy - Clair de Lune" }, { "electronic", "Röyksopp - Remind Me" } } },

    { "night", new() { { "rock", "The Cure - Lullaby" }, { "pop", "Billie Eilish - Ocean Eyes" },
                       { "classic", "Philip Glass - Opening" }, { "electronic", "Flume - Sleepless" } } }
};

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
        if (genre == "f") return;
        string songName = musicDictionary[now][genre];
        Console.WriteLine($"`{songName}` - is a good choice!");
        Console.WriteLine("Would you like to play it now? y/n");
        string? openPlayer = GetValidInput("player");


        if (openPlayer != "y")
        {
            Console.WriteLine(GetEndMessage(now));
            return;
        }
        Console.WriteLine("Enjoy!");


    }

    public static string GetEndMessage(string now)
    {
        string endMessage = now.ToLower() switch
        {
            "morning" => "Have a nice day!",
            "day" => "Have a great rest of the day!",
            "evening" => "Enjoy your evening!",
            "night" => "Sleep well!",
            _ => "Bye bye!",
        };
        return endMessage;

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
            case "player":
                while (string.IsNullOrEmpty(input) || !"yn".Contains(input.ToLower()))
                {
                    Console.WriteLine("Invalid input! Please enter `y` for yes, `n` for no");
                    input = Console.ReadLine()?.Trim();
                }
                return input.ToLower();
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

