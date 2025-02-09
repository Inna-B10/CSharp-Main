public class Person
{
    // fields
    public string? Name;
    public int Age;
    public List<string>? Hobbies;


    // methods(functions)
    public string Greeting()
    {
        return $"Hello there {Name}! You are {Age} years old!";
    }

    public void BetterGreeting()
    {
        if (Hobbies == null || Hobbies.Count == 0)
        {
            Console.WriteLine($"Hello there {Name}! You are {Age} years old! And you currently have no hobbies listed!");
            return;
        }
        // add hobbies internally in the method, from the data we recieve via the object
        string hobbies;

        if (Hobbies.Count == 1)
        {
            hobbies = Hobbies[0];
        }
        else
        {
            hobbies = string.Join(", ", Hobbies.Take(Hobbies.Count - 1)) + " & " + Hobbies.Last();
        }
        Console.WriteLine($"Hello there {Name}! You are {Age} years old! And you enjoy {hobbies}");
    }
}