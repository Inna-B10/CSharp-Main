namespace _13_02_2025_string_stbnamespace;

class Program
{
    static void Main(string[] args)
    {

        /* Her ber vi brukeren om en kommaseparert utgave av et Person objekt. */
        Console.WriteLine("Please enter your name, age and some hobbies, separated by a comma.");
        var commaSeparatedvalue = Console.ReadLine();
        /* Her bruker vi en While løkke som kjører så lenge input stringen vår ikke har en gyldig verdi. */
        while (string.IsNullOrEmpty(commaSeparatedvalue))
        {
            /* Hvis brukeren gir en ugyldig verdi, så gir vi brukeren litt mer guide, og venter på ny input.  */
            Console.WriteLine("Please enter your Name, Age, and some hobbies separated by a comma.");
            Console.WriteLine("Example: John, 33, Coding, Weightlifting, Drawing, Aquarium, Picking Mushrooms");
            commaSeparatedvalue = Console.ReadLine();
        }
        /* Vi prøver å splitte input verdien vår på komma, som er separatoren vi gir til bruker. */
        var valueArray = commaSeparatedvalue.Split(",").ToArray();
        /* Vi looper gjennom arrayet, og "trimmer" alle verdier, aka fjerner unødvendig whitespace.  */
        for (int i = 0; i < valueArray.Length; i++)
        {
            valueArray[i] = valueArray[i].Trim();
        }
        /* Vi initialiserer et nytt personobjekt, og leverer første index som navn.  */
        var person = new Person(){
            Name = valueArray[0],
        };
        /* Vi prøver å parse ut andre element som en alder.  */
        if (int.TryParse(valueArray[1], out var age)) person.Age = age;
        else 
        {
            /* Hvis det ikke går, må vi hente en gyldig alder fra brukeren vår.  */
            Console.WriteLine("Please enter a valid age as a whole number:");
            var input = Console.ReadLine();
            int secondAge;
            while(!int.TryParse(input, out secondAge))
            {
                Console.WriteLine("Please enter a valid age as a whole number:");
                input = Console.ReadLine();
            }
            person.Age = secondAge;
        }
        /* Hvis det er resterende elemeneter i valuearrayet vårt, legger vi de inn i hobbies. Vi legger alle elementer fra index 2 og utover via spreader syntax. */
        if (valueArray.Length > 2) person.Hobbies.AddRange(valueArray[2..]);

        /* !!BONUS BIT */
        /* Spørre bruker om de vil legge inn hobbyer, så kjøre mens bruker skriver inn hobbyer. */

        Console.WriteLine(person.CreateGreeting());
    }
}
public class Person
{
    public required string Name;
    public int Age;
    public List<string> Hobbies = [];
    public string CreateGreeting()
    {
        return $"Hello {Name}, you are {Age} years old. Your favorite hobby is {Hobbies[0]}";
    }
    public string CreateFormatedGreeting()
    {
        return string.Format("Hello {0}, your are {1} years old.", [Name, Age]);
    }
    public static void SayHello()
    {
        Console.WriteLine("Hello");
    }
}

class StringHelper
{
    public static string FirstCharToUpper(string input)
    {
        return $"{input[0].ToString().ToUpper()}" + input.Skip(1).ToString();
    }
}
