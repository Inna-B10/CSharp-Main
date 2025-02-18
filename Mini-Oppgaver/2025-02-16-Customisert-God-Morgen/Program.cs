
namespace _2025_02_16_Customisert_God_Morgen;

class Program
{
    public static string partOfTheDay = GetPartOfTheDay(DateTime.Now.Hour);
    public static string currentColor = StylesClass.GetColor(partOfTheDay);

    static void Main(string[] args)
    {
        Console.Clear();
        Console.Write($"{StylesClass.RESET_ALL}");
        Console.WriteLine("Please, enter your name");
        string userName = GetValidInput("name");
        Console.WriteLine($"{currentColor}{new string(' ', 20)}{StylesClass.INVERSE} Welcome {userName}! {StylesClass.RESET_INVERSE}");

        Console.WriteLine($"{StylesClass.BOLD}{ShowMessage(partOfTheDay)}{StylesClass.RESET_BOLD}");

        Console.WriteLine();
        Console.Write("What genre do you prefer? ");
        Console.WriteLine(" [r] rock, [p] pop, [i] instrumental, [s] soundtracks or [e] to exit");

        string genre = GetValidInput("genre");
        if (genre == "e") return;
        string songName = MusicData.MusicDictionary[partOfTheDay][genre];
        Console.WriteLine($"{currentColor}{StylesClass.BOLD}`{songName}`{StylesClass.RESET_BOLD} - is a good choice!");
        Console.WriteLine($"Would you like to play it now? y/n");
        string? openPlayer = GetValidInput("player");


        if (openPlayer != "y")
        {
            Console.WriteLine($"{new string(' ', 20)}{StylesClass.INVERSE}{GetGoodbyeMessage(partOfTheDay)}{StylesClass.RESET_ALL}");
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
    public static string ShowMessage(string part)
    {
        switch (part)
        {
            case "evening":
                return "Winding down?\nRelaxing music helps create an atmosphere of comfort and harmony in the evening.";

            case "day":
                return "Boosting your day?\nWell-chosen melodies enhance productivity throughout the workday.";

            case "morning":
                return "Starting the day with energy?\nEnergetic music is the perfect way to start the day with vigor and motivation.";
            default:
                return "Late-night tunes?\nSoft, calming tunes prepare your mind for a good night's sleep.{StylesClass.RESET_BOLD}";
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
                    Console.WriteLine($"{StylesClass.ERROR}Invalid input! Please enter `y` for yes, `n` for no{StylesClass.RESET_ALL}{currentColor}");
                    input = Console.ReadLine()?.Trim();
                }
                return input.ToLower();
            case "name":
                while (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine($"{StylesClass.ERROR}Invalid input! Please, enter your name{StylesClass.RESET_ALL}{currentColor}");
                    input = Console.ReadLine()?.Trim();
                }
                return input;

            case "genre":

                while (string.IsNullOrEmpty(input) || !"rpise".Contains(input.ToLower()))
                {
                    Console.WriteLine($"{StylesClass.ERROR}Invalid input! Please select one of the genres:{StylesClass.RESET_ALL}{currentColor}");
                    Console.WriteLine($"{StylesClass.ITALIC}Enter r for `rock`, p for `pop`, i for `instrumental`, s for `soundtracks` or e to Exit the program{StylesClass.RESET_ALL}{currentColor}");
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

