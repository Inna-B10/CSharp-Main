namespace _2025_02_16_Customisert_God_Morgen;

class Program
{
    public static Dictionary<string, Dictionary<string, string>> musicDictionary = new()
{
    { "morning", new() { { "rock", "Gorky_Park--Welcome_to_the_Gorky_Park" }, { "pop", "Sean_Paul--Temperture" },
                          { "instrumental", "Piano_Duel" }, { "soundtracks", "Hey_Mister" } } },

    { "day", new() { { "rock", "Kipelov--Power_of_Fire" }, { "pop", "Gnarls_Barkley--Crazy" },
                     { "instrumental", "Piano_Duet" }, { "soundtracks", "Dream_in_Bali" } } },

    { "evening", new() { { "rock", "Nate_Smith--Rather_Be_Lonely" }, { "pop", "Brad_Paisley--The_world" },
                         { "instrumental", "Secret" }, { "soundtracks", "Boiling" } } },

    { "night", new() { { "rock", "Scorpions--The_Winds_of_Change" }, { "pop", "Tanita_Tikaram--Twist_in_my_Sobriety" },
                       { "instrumental", "Dancing_with_Father" }, { "soundtracks", "You_Let_Me_Go_With_a_Smile" } } }
};

    static void Main(string[] args)
    {
        Console.WriteLine("Please, enter your name");
        string userName = GetValidInput("name");
        Console.WriteLine($"Welcome {userName}!");
        DateTime date = DateTime.Now;
        string now = GetTimesOfDay(date.Hour);

        Console.WriteLine("What genre do you prefer?");
        Console.WriteLine("Rock, pop, instrumental, soundtracks");
        Console.WriteLine("Enter r for `Rock`, p for `pop`, i for `instrumental`, s for `soundtracks` or e to exit the program");

        string genre = GetValidInput("genre");
        if (genre == "e") return;
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

        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "music");
        // string path = Directory.GetCurrentDirectory() + "\\music\\01. Highway To Hell.mp3";
        string filePath = FindFile(folderPath, songName + ".mp3");

        Console.WriteLine(folderPath);
        Console.WriteLine(filePath);


        // MusicPlayer.Player(filePath);


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

                while (string.IsNullOrEmpty(input) || !"rpise".Contains(input.ToLower()))
                {
                    Console.WriteLine("Invalid input! Please select one of the genres:");
                    Console.WriteLine("Enter r for `Rock`, p for `Pop`, i for `instrumental`, s for `soundtracks` or e to Exit the program");
                    input = Console.ReadLine()?.Trim();
                }
                return input.ToLower() switch
                {
                    "r" => "rock",
                    "p" => "pop",
                    "i" => "instrumental",
                    "s" => "soundtracks",
                    _ => "e"
                };
            default:
                throw new ArgumentException("Unknown input type");
        }
    }
    static string FindFile(string folderPath, string fileName)
    {
        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath, fileName, SearchOption.TopDirectoryOnly);

            if (files.Length > 0)
            {
                return files[0];
            }
        }

        return null;
    }
}

