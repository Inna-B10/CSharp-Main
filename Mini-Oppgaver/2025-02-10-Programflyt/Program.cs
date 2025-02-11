namespace _2025_02_10_Programflyt;

class Program
{
    static void Main(string[] args)
    {
        string startGame()
        {
            Console.WriteLine("Do you want to play rock, paper, scissors? y/n");
            return Console.ReadLine().ToLower();
        }

        string userInput = startGame();
        if (userInput == "n") { return; }
        else if (userInput != "n" && userInput != "y")
        {
            Console.WriteLine("Invalid input! Choose y for `yes` or n for `no`.");
            startGame();
        }

        Console.WriteLine("yes");
    }
}
