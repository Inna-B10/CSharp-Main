
namespace _2025_02_16_Customisert_God_Morgen;

class Program
{
    public static string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "music");
    static readonly string partOfDay = GetPartOfDay(DateTime.Now.Hour);
    static readonly (string color, string message, string goodbye) currentData = StylesClass.DayOptions[partOfDay];

    static void Main(string[] args)
    {
        Reset();
        GreetingUser();
        Console.WriteLine($"{StylesClass.BOLD}{currentData.message}{StylesClass.RESET_BOLD}");

        Console.WriteLine();
        string genre = GetGenre();
        if (genre == "e") return;

        string songName = MusicData.MusicDictionary[partOfDay][genre];
        if (songName == null || string.IsNullOrEmpty(songName))
        {
            Console.WriteLine("Could not get the name of the song!");
            return;
        }

        bool isShowPlayer = IsUserWantPlaySong(songName);
        if (!isShowPlayer)
        {
            Console.WriteLine($"{new string(' ', 20)}{StylesClass.INVERSE}{currentData.goodbye}{StylesClass.RESET_ALL}");
            return;
        }

        // Console.Clear();        
        string filePath = FindFile(folderPath, songName + ".mp3");

        MusicPlayer.Player(filePath);
    }

    public static void Reset()
    {
        Console.Clear();
        Console.Write($"{StylesClass.RESET_ALL}");
    }
    public static string GetPartOfDay(int hour)
    {
        return hour switch
        {
            >= 17 => "evening",
            >= 12 => "day",
            >= 5 => "morning",
            _ => "night"
        };
    }
    public static void GreetingUser()
    {
        Console.WriteLine("Please, enter your name");
        string userName = GetValidInput("name");
        Console.WriteLine($"{currentData.color}{new string(' ', 20)}{StylesClass.INVERSE} Welcome {userName}! {StylesClass.RESET_INVERSE}");
    }

    public static string GetGenre()
    {
        Console.Write("What genre do you prefer? ");
        Console.WriteLine(" [r] rock, [p] pop, [i] instrumental, [s] soundtracks or [e] to exit");

        return GetValidInput("genre");
    }

    public static bool IsUserWantPlaySong(string song)
    {
        Console.WriteLine($"{currentData.color}{StylesClass.BOLD}`{song}`{StylesClass.RESET_BOLD} - is a good choice!");
        Console.WriteLine($"Would you like to play it now? y/n");
        return GetValidInput("player") == "y";
    }
    public static string GetValidInput(string type = "name")
    {
        string? input = Console.ReadLine()?.Trim();

        switch (type)
        {
            case "player":
                while (string.IsNullOrEmpty(input) || !"yn".Contains(input.ToLower()))
                {
                    Console.WriteLine($"{StylesClass.ERROR}Invalid input! Please enter `y` for yes, `n` for no{StylesClass.RESET_ALL}{currentData.color}");
                    input = Console.ReadLine()?.Trim();
                }
                return input.ToLower();
            case "name":
                while (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine($"{StylesClass.ERROR}Invalid input! Please, enter your name{StylesClass.RESET_ALL}{currentData.color}");
                    input = Console.ReadLine()?.Trim();
                }
                return input;

            case "genre":

                while (string.IsNullOrEmpty(input) || !"rpise".Contains(input.ToLower()))
                {
                    Console.WriteLine($"{StylesClass.ERROR}Invalid input! Please select one of the genres:{StylesClass.RESET_ALL}{currentData.color}");
                    Console.WriteLine($"{StylesClass.ITALIC}Enter r for `rock`, p for `pop`, i for `instrumental`, s for `soundtracks` or e to Exit the program{StylesClass.RESET_ALL}{currentData.color}");
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

