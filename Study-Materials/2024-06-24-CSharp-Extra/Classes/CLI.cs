using System;

public class CLI
{
    private IOHandler ioHandler;

    public CLI()
    {
        ioHandler = new IOHandler();
    }

    public void Run()
    {
        Console.WriteLine("Welcome to the File Handler CLI");
        Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
        Console.WriteLine("Enter the command you wish to run (Read JSON: J, Read CSV: C, Read txt: T, Clear: clear, Write JSON: WJ, Write CSV, WC), followed by the <file path>: ");

        while (true)
        {
            Console.WriteLine("> ");
            string? input = Console.ReadLine();
            //Console.WriteLine(input);
            if (string.IsNullOrWhiteSpace(input))
            {
                continue;
            }
            string[] stringArgs = input.Split(" ", 2);
            if (stringArgs.Length < 2)
            {
                Console.WriteLine("Invalid command entered! \n Usage: <command> <file path>");
                continue;
            }

            string command = stringArgs[0].ToLower();
            string filePath = stringArgs[1];
            string? content = stringArgs.Length > 2 ? stringArgs[2] : null;

            switch (command)
            {
                case "j":
                    ioHandler.ReadJSONFile(filePath);
                    Console.WriteLine(filePath);
                    break;
                case "c":
                    ioHandler.ReadCSVFile(filePath);
                    break;
                case "t":
                    ioHandler.ReadTextFile(filePath);
                    break;
                case "wj":
                    if (content == null)
                    {
                        Console.WriteLine("No content provided! Please provide some data.");
                    }
                    else
                    {
                        ioHandler.WriteJSONFile(filePath, content);
                    }
                    break;

                case "clear":
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Unknown command entered!");
                    break;
            }
        }
    }
}