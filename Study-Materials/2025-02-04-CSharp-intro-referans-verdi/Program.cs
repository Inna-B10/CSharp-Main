namespace _04_02_2025_intro_referans_verdi;

class Program
{
    static void Main(string[] args)
    {

        /* Når vi bruker en integer (eller andre verdityper) for å lage en annen, kopierer vi verdiene. */
        int myInt = 10;
        int secondInt = myInt;
        myInt += 10;
        /* Vi ser at selv om vi brukte verdien til myInt for å lage secondInt, så påvirker de ikke hverandre etterpå. */
        Console.WriteLine(myInt);
        Console.WriteLine(secondInt);

        /* Det samme gjelder ikke for referansedatatyper som lister.  */
        List<int> numbers = [1,2,3,4,6,7,8,9];
        List<int> secondNumbers = numbers;
        numbers[3] = 10;
        /* Siden de peker på samme verdi i minnet, så påvirker vi den ene ved å endre den andre.  */
        foreach (var number in numbers) Console.WriteLine(number);
        foreach (var number in secondNumbers) Console.WriteLine(number);
    }
}
