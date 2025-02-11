namespace _2025_02_10_Programflyt;

class Program
{
    static void Main(string[] args)
    {
        string[] options = { "rock", "paper", "scissors" };
        // Console.WriteLine(options[0]);





        string? start = startGame();
        if (start == "n") { return; }
        Console.WriteLine("-----------------Game started-----------------");

        string randomPcChoice = pcChoice(options);
        Console.WriteLine("PC choice is: ***********");
        Console.WriteLine("What is your choice?");
        string? userChoice = getUserChoice();

        Console.WriteLine();
        Console.WriteLine($"User's choice is: {userChoice}");
        Console.WriteLine($"PC's choice is: {randomPcChoice}");




        /* ----------------------------- StartGame Game ----------------------------- */
        string? startGame()
        {
            string? input;
            bool isFirstTime = true;
            do
            {
                if (isFirstTime)
                {

                    Console.WriteLine("Do you want to play rock, paper, scissors? y/n");
                    isFirstTime = false;
                }
                else
                {
                    Console.WriteLine("Invalid input! Try again:");
                    Console.WriteLine("Do you want to play rock, paper, scissors?");
                    Console.WriteLine("Enter y for `yes` or n for `no`.");
                    Console.WriteLine();
                }
                input = Console.ReadLine()?.ToLower();
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
                    Console.WriteLine("Invalid input! Try again:");
                }
                Console.WriteLine("Enter r for `rock`, p for `paper` or s for `scissors`");
                input = Console.ReadLine()?.ToLower();
            }
            while (input != "r" && input != "p" && input != "s");

            string choice = input switch
            {
                "r" => "rock",
                "p" => "paper",
                "s" => "scissors",
            };
            return choice;
        }




    }
}
