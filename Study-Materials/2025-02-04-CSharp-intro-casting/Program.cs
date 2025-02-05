namespace _04_02_2025_intro_casting;

class Program
{
    static void Main(string[] args)
    {
        double myDouble = 10;
        double secondDouble = 20;

        /* Her ser vi at vi har en implicit type vi kan bruke, hvor datamaskinen inserter typen for oss, basert på verdien på høyre siden av = . */
        var sum = myDouble + secondDouble;

        int myInt = 30;

        /* Vi ser her også at datamaskinen vår, for å passe på å bevare data så godt som mulig, vil gjøre om vår int til en double, siden en int alltid passer i en double. */
        var sum2 = myDouble + myInt;

        long bigInt = 40;

        var sum3 = myInt + bigInt;

        int number1 = 9;
        int number2 = 10;

        /* Vi kan explicit få datamaskinen vår til å behandle en datatype som en annen, ved å Caste den til en annen type midlertidig.
            Her caster vi number1 til en double, da leser datamaskinen dette som at vi deler en double på en int, så number2 blir også castet.
         */
        var division = (double)number1/number2;
        Console.WriteLine(division);


        double someDecimalNumber = 3.999999;


        /* Vi kan også caste fra en double til en int. men siden en double ikke "passer" fint inn i en integer, mister vi informasjon.
            Datamaskinen ignorerer alle tall bak .
            tallet blir "trunkert".
        */
        var sum4 = number1 + (int)someDecimalNumber;
        Console.WriteLine(sum4);
    }
}
