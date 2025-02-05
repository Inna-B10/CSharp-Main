
namespace _04_02_2025_intro_programflyt;

class Program
{
    static void Main(string[] args)
    {

        /* Pseudokode eksempel: 
            Jeg vil se på en alder, så bestemme om de kommer inn på utested 1 (21+) eller utested 2(18+) eller ingen.
            
                Hvis alderen er høyere eller lik 21:
                    Skriv at de kommer inn på utested 1.
                ellers hvis alderen er høyere eller lik 18:
                    skriv at de kommer inn på utested 2.
                ellers skriv at de ikke får komme inn noen steder. 
        */
        int age = 30;
        int higherLimit = 21;
        int minimumLimit = 18;

        /* Hvis denne sammenligningen gir meg True */
        if (age >= higherLimit)
        {
            Console.WriteLine("You're free to enter either establishment.");
        }
        /* Eller hvis denne sammenligningen gir meg True */
        else if(age >= minimumLimit)
        {
            Console.WriteLine("You can enter the 18+ only.");
        }
        /* Hvis alle de over er false.  */
        else
        {
            Console.WriteLine("You cannot enter.");
        }
    }
    
}
