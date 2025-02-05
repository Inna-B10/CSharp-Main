namespace _03_02_2025_intro_runtime;

class Program
{
    static void Main(string[] args)
    {

        /* Her definerer vi en variabel som heter helloWorld.
        Vi sier den skal ha datatypen string, og gir den verdien "Hello, World!"
        
        Når vi definerer en variabel i C# må vi gi den:
            1. datatypen
            2. et variabelnavn
            3. en verdi den skal holde.  */
        string helloWorld = "Hello, World!";
        Console.WriteLine(helloWorld);
        Console.WriteLine("Second Line in Program.Main");

        //for å definere et helt tall, bruk datatypen int.
        int myAge = 33;

        //for å definere et desimaltall, bruk datatypen double.
        double myHeight = 1.87;
        Console.WriteLine(myAge);
        Console.WriteLine(myHeight);

        //Andre måter å definere tall på. 
        float myFloat = 1.43f;  
        decimal myDecimal = 4.85m;
        Console.WriteLine(myFloat);
        Console.WriteLine(myDecimal);


        //Alle primærdatatypene våre har også en maksverdi. 
        int tooLargeNumber = int.MaxValue;
        Console.WriteLine(tooLargeNumber); 

        //Det samme gjelder desimaltall, legger vi til et desimaltall til på begge disse, klarer ikke datamaskinen lengre å se forskjell på dette tallet og tallet 1. 
        float tooManyDesimalPoints = 0.9999999f;
        double tooManyDesimalsDouble = 0.9999999999999999;
        Console.WriteLine(tooManyDesimalPoints);
        Console.WriteLine(tooManyDesimalsDouble);
    }
}
