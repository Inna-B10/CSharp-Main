namespace KH_Codealong;

class Program
{
    static void Main(string[] args)
    {
        // classes
        Person person = new Person();
        person.Name = "John Doe";
        person.Age = 35;
        person.Hobbies = new List<string> {
            "Programming", "Skiing", "Gaming", "Watching movies"
        };

        Console.WriteLine(person.Greeting());
        person.BetterGreeting();

        // wait for the enter key before the program continues 
        Console.ReadKey();

        // datatypes
        int x = 10;
        double y = 5.5;
        bool isAllowedIn = false;
        char a = 'a';
        string helloWorld = "Hello, World!";
        Console.WriteLine(x + y);
        Console.WriteLine(helloWorld);
        Console.WriteLine(a);


        // data structures

        // int array
        int[] wholeNumbers = { 1, 2, 3, 4, 5, 6 };

        // list
        List<string> handleListe = new List<string>() {
            "Melk",
            "Egg",
            "Brød",
            "Brus",
            "Snop",
            "Middag"
        };

        for (int i = 0; i < wholeNumbers.Length; i++)
        {
            Console.WriteLine($"index of element in the array wholeNumbers: {wholeNumbers[i]}");
        }

        foreach (string vare in handleListe)
        {
            Console.WriteLine($"Husk å kjøpe: {vare}");
        }

        // user input
        Console.WriteLine("Hvor gammel er du?");
        string? age = Console.ReadLine();

        // type converting & control-flow
        if (Convert.ToInt32(age) < 18 && isAllowedIn == false)
        {
            Console.WriteLine($"DEBUG: Value of age input:{age}");
            Console.WriteLine("Du får ikke komme inn på denne baren.");
        }
        else if (Convert.ToInt32(age) >= 18 && !isAllowedIn)
        {
            Console.WriteLine($"DEBUG: Value of age input:{age}");
            Console.WriteLine("Du får komme inn!");
        }
        else
        {
            Console.WriteLine($"DEBUG: Value of age input:{age}");
            Console.WriteLine("@@@@@\n DEBUG: No output");
        }
    }
}
