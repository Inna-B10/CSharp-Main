//https://stackoverflow.com/questions/7937256/custom-text-color-in-c-sharp-console-application
// trying different variations to style text in console (colors, styles)
using System;
using System.Drawing;
namespace _2025_02_10_Programflyt;
public static class ColorExt
{
    /// <summary>
    /// Color extension to convert Color to Ascii text to be used within a Console.Write/WriteLine.
    /// </summary>
    public static string ToAscii(this System.Drawing.Color clr, bool isForeground)
    {
        var present = isForeground ? 38 : 48;
        return $"\x1b[{present};2;{clr.R};{clr.G};{clr.B}m";
    }
}

class Program
{
    public const string RESET = "\x1b[0m";
    public const string ITALIC = "\x1b[3m";
    public const string BOLD = "\x1B[1m";
    public const string REVERSE = "\x1B[7m";
    public const string UNDERLINE = "\x1B[4m";
    public const string ERROR = "\x1B[38;5;196m\x1B[48;5;189m";


    static void Main(string[] args)
    {


        string[] options = ["rock", "paper", "scissors"];

        Console.Write(new string(' ', 10) + REVERSE + "  Hello!  " + RESET);
        Console.WriteLine();

        Console.WriteLine($"{Color.Gold.ToAscii(false)}Do you want to play rock, paper, scissors?{RESET}");

        string? start = startGame();
        if (start == "n") return;

        string? userChoice;

        do
        {
            /* ------------------------------ Game Started ------------------------------ */
            Console.Write(RESET);
            // Console.Clear();

            Console.WriteLine(new string(' ', 10) + $"{REVERSE} New game started {RESET}");

            string randomPcChoice = pcChoice(options);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{BOLD}PC's choice is: *********************");
            Console.ResetColor();
            Console.WriteLine($"{Color.Gold.ToAscii(false)}What is your choice?{RESET}");


            userChoice = getUserChoice();
            if (userChoice == "e") return;

            /* --------------------------------- Result --------------------------------- */
            Console.WriteLine();
            Console.WriteLine(new string(' ', 10) + $"{REVERSE} Result {RESET}");
            Console.WriteLine($"{FromHex("#00CC00").ToAscii(true)}PC's choice is: {BOLD}{randomPcChoice} \x1B[22m");
            Console.WriteLine($"User's choice is: {BOLD}{userChoice} \x1B[22m");
            string winner = checkWinner(userChoice, randomPcChoice);

            Console.WriteLine();
            Console.Write($"Game finished: {Color.FromArgb(255, 0, 255).ToAscii(true)} {BOLD}{UNDERLINE}{winner}\x1B[22m\x1B[24m  ");
            if (winner == "The user wins!")
            {
                Console.WriteLine($"{REVERSE}{Color.FromArgb(255, 0, 255).ToAscii(true)} Congratulation! {RESET}");
            }
            else { Console.WriteLine(); }
            /* ------------------------------ Game Finished ----------------------------- */
            Console.WriteLine();
        } while (userChoice != "e");


        Console.WriteLine($"This is {Color.Gold.ToAscii(true)}GOLD text and " +
                          $"now with a {Color.Red.ToAscii(false)}RED background{RESET}. " +
                          $"After reset, all colors are set back to default.");
        return;

        /* ------------------------------- Start Game ------------------------------- */
        string? startGame()
        {
            string? input;
            bool isFirstTime = true;
            do
            {
                if (isFirstTime)
                {
                    isFirstTime = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"{ERROR}Invalid input! Please try again:{RESET}");
                }

                Console.WriteLine($"{ITALIC}Enter y for `yes` or n for `no`.{RESET}");
                input = Console.ReadLine()?.ToLower().Trim();
            }
            while (input != "n" && input != "y");
            return input;
        }
        /* -------------------------------- PC Choice ------------------------------- */
        string pcChoice(string[] options)
        {
            Random random = new Random();
            //generate random index
            int index = random.Next(options.Length);
            return options[index];
        }

        /* ------------------------------ User's Choice ----------------------------- */
        string getUserChoice()
        {
            string? input;
            bool isFirstTime = true;
            do
            {
                if (isFirstTime) { isFirstTime = false; }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"\x1B[38;5;196m\x1B[48;5;189mInvalid input! Please try again:{RESET}");
                }
                Console.WriteLine($"{ITALIC}Enter r for `rock`, p for `paper`, s for `scissors` or e to exit the game.{RESET}");
                input = Console.ReadLine()?.ToLower().Trim();
            }
            while (input != "r" && input != "p" && input != "s" && input != "e");

            string choice = input switch
            {
                "r" => "rock",
                "p" => "paper",
                "s" => "scissors",
                _ => "e"
            };
            return choice;
        }

        /* ---------------------------- Check The Winner ---------------------------- */
        string checkWinner(string userChoice, string randomPcChoice)
        {
            if (userChoice == randomPcChoice)
            {
                return "Draw!";
            }
            switch (userChoice)
            {
                case "rock":
                    if (randomPcChoice == "scissors")
                    {
                        return "The user wins!";
                    }
                    else
                    {
                        return "The PC wins!";
                    }
                case "paper":
                    if (randomPcChoice == "rock")
                    {
                        return "The user wins!";
                    }
                    else
                    {
                        return "The PC wins!";
                    }
                case "scissors":
                    if (randomPcChoice == "paper")
                    {
                        return "The user wins!";
                    }
                    else
                    {
                        return "The PC wins!";
                    }
                default: return "Some error happened";
            }
        }
    }
    /* --------------------- Convert Colors From HEX Format --------------------- */
    public static Color FromHex(string hex)
    {
        return ColorTranslator.FromHtml(hex);
    }
}
