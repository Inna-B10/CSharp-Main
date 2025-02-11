namespace _2025_02_10_Programflyt;

class Program
{
    static void Main(string[] args)
    {
        string[] options = { "rock", "paper", "scissors" };

        string? start = startGame();
        if (start == "n") return;

        string? userChoice;

        do
        {
            Console.WriteLine("-----------------Game started-----------------");

            string randomPcChoice = pcChoice(options);
            Console.WriteLine("PC's choice is: *********************");
            Console.WriteLine("What is your choice?");
            userChoice = getUserChoice();
            if (userChoice == "e") return;

            Console.WriteLine();
            Console.WriteLine("--------------------Result-------------------");
            Console.WriteLine($"User's choice is: {userChoice}");
            Console.WriteLine($"PC's choice is: {randomPcChoice}");
            string winner = checkWinner(userChoice, randomPcChoice);

            Console.WriteLine();
            Console.WriteLine(winner);
            if (winner == "The user wins!")
            {
                Console.WriteLine("Congratulation!");
            }
            Console.WriteLine("----------------Game finished----------------");
            Console.WriteLine();
        } while (userChoice != "e");
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
                    Console.WriteLine("Invalid input! Try again:");
                }
                Console.WriteLine();
                Console.WriteLine("Do you want to play rock, paper, scissors?");
                Console.WriteLine("Enter y for `yes` or n for `no`.");
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
                Console.WriteLine("Enter r for `rock`, p for `paper`, s for `scissors` or e to exit the game.");
                input = Console.ReadLine()?.ToLower();
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
}
