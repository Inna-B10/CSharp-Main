
namespace _2025_02_16_Customisert_God_Morgen;

class Program
{
    public static string partOfTheDay = GetPartOfTheDay(DateTime.Now.Hour);
    public static string currentColor = StylesClass.GetColor(partOfTheDay);

    static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine($"{StylesClass.RESET}");
        Console.WriteLine("Please, enter your name");
        string userName = GetValidInput("name");
        Console.WriteLine($"{StylesClass.REVERSE}{currentColor}Welcome {userName}!\x1B[27m");
        ShowMessage(partOfTheDay);


        Console.Write($"What genre do you prefer? ");
        Console.WriteLine(" Rock, pop, instrumental or soundtracks");
        Console.WriteLine($"{StylesClass.ITALIC}Enter r for `rock`, p for `pop`, i for `instrumental`, s for `soundtracks` or e to exit the program{StylesClass.RESET}");

        string genre = GetValidInput("genre");
        if (genre == "e") return;
        string songName = MusicData.MusicDictionary[partOfTheDay][genre];
        Console.WriteLine($"{currentColor}{StylesClass.BOLD}`{songName}`\x1B[22m - is a good choice!");
        Console.WriteLine($"Would you like to play it now? y/n");
        string? openPlayer = GetValidInput("player");


        if (openPlayer != "y")
        {
            Console.WriteLine($"{StylesClass.REVERSE}{GetGoodbyeMessage(partOfTheDay)}{StylesClass.RESET}");
            return;
        }
        // Console.Clear();        
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "music");
        string filePath = FindFile(folderPath, songName + ".mp3");

        MusicPlayer.Player(filePath);
    }

    public static string GetGoodbyeMessage(string now)
    {
        string goodbyeMessage = now.ToLower() switch
        {
            "morning" => "Have a nice day!",
            "day" => "Have a great rest of the day!",
            "evening" => "Enjoy your evening!",
            "night" => "Sleep well!",
            _ => "Bye bye!",
        };
        return goodbyeMessage;

    }
    public static string GetPartOfTheDay(int time)
    {
        return time switch
        {
            >= 17 => "evening",
            >= 12 => "day",
            >= 5 => "morning",
            _ => "night"
        };
    }
    public static void ShowMessage(string part)
    {
        switch (part)
        {
            case "evening":
                Console.WriteLine("Winding down?");
                Console.WriteLine("Relaxing music helps create an atmosphere of comfort and harmony in the evening.");
                break;
            case "day":
                Console.WriteLine("Boosting your day?");
                Console.WriteLine("Well-chosen melodies enhance productivity throughout the workday.");
                break;
            case "morning":
                Console.WriteLine("Starting the day with energy?");
                Console.WriteLine("Energetic music is the perfect way to start the day with vigor and motivation.");
                break;
            default:
                Console.WriteLine($"{StylesClass.BOLD}Late-night tunes?");
                Console.WriteLine("Soft, calming tunes prepare your mind for a good night's sleep.\x1B[22m");
                break;
        }
    }
    public static string GetValidInput(string type = "name")
    {
        string? input = Console.ReadLine()?.Trim();

        switch (type)
        {
            case "player":
                while (string.IsNullOrEmpty(input) || !"yn".Contains(input.ToLower()))
                {
                    Console.WriteLine($"{StylesClass.ERROR}Invalid input! Please enter `y` for yes, `n` for no{StylesClass.RESET}");
                    input = Console.ReadLine()?.Trim();
                }
                return input.ToLower();
            case "name":
                while (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine($"{StylesClass.ERROR}Invalid input! Please, enter your name{StylesClass.RESET}");
                    input = Console.ReadLine()?.Trim();
                }
                return input;

            case "genre":

                while (string.IsNullOrEmpty(input) || !"rpise".Contains(input.ToLower()))
                {
                    Console.WriteLine($"{StylesClass.ERROR}Invalid input! Please select one of the genres:{StylesClass.RESET}");
                    Console.WriteLine($"{StylesClass.ITALIC}Enter r for `rock`, p for `pop`, i for `instrumental`, s for `soundtracks` or e to Exit the program{StylesClass.RESET}");
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
        return "";
    }
}

