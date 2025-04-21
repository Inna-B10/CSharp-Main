using System;

public class GenerateAsciiArt
{
    public void GenerateAscii()
    {
        Console.WriteLine("Create new ASCII art!");
        Console.WriteLine("Choose a new shape:");
        Console.WriteLine("1: Heart");
        Console.WriteLine("2: Star");
        Console.WriteLine("3: Smileyface");
        Console.WriteLine("Enter your choice: ");

        int choice;
        if (int.TryParse(Console.ReadLine(), out choice))
        {
            switch (choice)
            {
                case 1:
                    MakeHeartShape();
                    break;
                case 2:
                    MakeStarShape();
                    break;
                case 3:
                    MakeSmileyShape();
                    break;
                default:
                    Console.WriteLine("This is not a valid shape!");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid choice!");
        }
    }
    public void MakeHeartShape()
    {
        Console.WriteLine(" ♥ ♥");
        Console.WriteLine("♥   ♥");
        Console.WriteLine(" ♥ ♥");
        Console.WriteLine("  ♥ ");
    }

    public void MakeStarShape()
    {
        Console.WriteLine("    *");
        Console.WriteLine("    * ");
        Console.WriteLine("   *  * ");
        Console.WriteLine("  *    * ");
    }

    public void MakeSmileyShape()
    {
        Console.WriteLine("  :)  ");
        Console.WriteLine("      :)  ");
        Console.WriteLine(" \\     /");
        Console.WriteLine("  \\___/ ");
    }
}