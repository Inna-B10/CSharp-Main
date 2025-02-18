
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
            Console.WriteLine($"{new string(' ', 20)}{StylesClass.INVERSE} {currentData.goodbye} {StylesClass.RESET_ALL}");
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
        Console.WriteLine($"Would you like to play it now? [y] yes, [n] no");
        return GetValidInput("player") == "y";
    }
    public static string GetValidInput(string type)
    {
        string? input;
        do
        {
            input = Console.ReadLine()?.Trim();
            if (!IsValidInput(input, type))
            {
                Console.WriteLine(GetErrorMessage(type));
                input = null;
            }
        } while (input == null);
        if (type == "genre")
        {
            input = input.ToLower() switch
            {
                "r" => "rock",
                "p" => "pop",
                "i" => "instrumental",
                "s" => "soundtracks",
                _ => "e"
            };
        }
        return input;
    }
    public static bool IsValidInput(string? input, string type)
    {
        return type switch
        {
            "name" => !string.IsNullOrEmpty(input),
            "genre" => !string.IsNullOrEmpty(input) && "rpise".Contains(input.ToLower()),
            "player" => !string.IsNullOrEmpty(input) && "yn".Contains(input.ToLower()),
            _ => false
        };
    }

    public static string GetErrorMessage(string type)
    {
        return type switch
        {
            "name" => $"{StylesClass.ERROR}Invalid input! Please, enter your name{StylesClass.RESET_ALL}",
            "genre" => $"{StylesClass.ERROR}Invalid input! Enter r for `rock`, p for `pop`, i for `instrumental`, s for `soundtracks` or e to exit the program{StylesClass.RESET_ALL}",
            "player" => $"{StylesClass.ERROR}Invalid input! Please enter `y` for yes, `n` for no{StylesClass.RESET_ALL}",
            _ => $"{StylesClass.ERROR}Unknown error{StylesClass.RESET_ALL}"
        };
    }
    public static string FindFile(string folderPath, string fileName)
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

