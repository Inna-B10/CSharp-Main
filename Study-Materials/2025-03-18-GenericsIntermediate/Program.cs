
/* I dette programmet skal vi se på hva Generics er for noe, og hva det lar oss gjøre.

        Generics er verktøyet i C# som lar oss designe og skrive kode, som er gjennbrukbar for flere datatyper.
    La oss se på et eksempel nedenfor hvor vi skal lage en enkel "stack" som skal lagre tall i en stack: */

using Generics.Models;

class Program
{
    public static void Main(string[] args)
    {

        /* Lag classene før du bruker de i Main. 
        
        Begynn med IntegerStack
        
         */

        var intStack = new IntegerStack(10);
        intStack.Push(23);
        var poppedNumber = intStack.Pop();
        //Her er vi 100% sikker på at vi alltid jobber med integer datatyper, 
        //og trenger ikke gjøre andre skjekker eller "guards" for å se hva datatype vi sitter igjen med etter operasjonen er utført.

        var objStack = new ObjectStack(10);
        objStack.Push(43);
        objStack.Push("Hello!");

        //Her kan det være vanskelig å garantere i runtime hva datatype vi faktisk jobber med. Tall eller streng?
        var obj = objStack.Pop();
        Console.WriteLine(obj.GetType());

        var doubleStack = new Stack<double>(10);
        doubleStack.Push(32);
        var storedDouble = doubleStack.Pop();

        var textStack = new Stack<string>(10);

        textStack.Push("World!");
        textStack.Push("Hello");

        Console.WriteLine($"{textStack.Pop()}, {textStack.Pop()}");

        //Ved å bruke Generics har vi et veldig godt verktøy for å lage gjennbrukbar kode som vi kan bruke uavhengig av datatype. 


        //La oss lage to pens, en for camels, og en for bears:

        var bearPen = new AnimalPen<Bear>(10);

        var camelPen = new AnimalPen<Camel>(10);

        //La oss lage noen bjørner:

        var bear1 = new Bear("Oluf", FoodConsumption.Carnivore, 12, "Brown");

        var bear2 = new Bear("Randi", FoodConsumption.Carnivore, 10, "Brown");

        bearPen.Push(bear1);
        bearPen.Push(bear2);

        //La oss så lage to kameler:

        var camel1 = new Camel("Kari", FoodConsumption.Herbivore, 8, 2);
        var camel2 = new Camel("Oscar", FoodConsumption.Herbivore, 10, 2);

        camelPen.Push(camel1);
        camelPen.Push(camel2);

        //La oss lage en ZooFeeder, som kan mate dyrene våre:

        //Legg merke til, siden Zoofeeder kan infere typen basert på vår IPoppable interface.
        ZooFeeder.Feed(bearPen);

        ZooFeeder.Feed(camelPen);



        // Oppretting av flere dyreinnhegninger
        var lionPen = new AnimalPen<Lion>(5);
        var elephantPen = new AnimalPen<Elephant>(3);
        var penguinPen = new AnimalPen<Penguin>(10);

        // Legger til dyr
        lionPen.Push(new Lion("Simba", FoodConsumption.Carnivore, 5, "Golden"));
        lionPen.Push(new Lion("Nala", FoodConsumption.Carnivore, 4, "Tan"));
        lionPen.Push(new Lion("Mufasa", FoodConsumption.Carnivore, 12, "Dark Brown"));

        elephantPen.Push(new Elephant("Dumbo", FoodConsumption.Herbivore, 3, 1.2));
        elephantPen.Push(new Elephant("Ellie", FoodConsumption.Herbivore, 15, 2.8));

        penguinPen.Push(new Penguin("Pingu", FoodConsumption.Carnivore, 2, false));
        penguinPen.Push(new Penguin("Emperor", FoodConsumption.Carnivore, 7, true));
        penguinPen.Push(new Penguin("Happy", FoodConsumption.Carnivore, 4, false));
        penguinPen.Push(new Penguin("Gloria", FoodConsumption.Carnivore, 6, false));

        // Mater alle dyrene
        ZooFeeder.Feed(lionPen);
        ZooFeeder.Feed(elephantPen);
        ZooFeeder.Feed(penguinPen);

        // Oppretter dyreshow med løver
        var lionShow = new AnimalShow<Lion>([
            new Lion("Leo", FoodConsumption.Carnivore, 8, "Black"),
            new Lion("Leona", FoodConsumption.Carnivore, 7, "Spotted")
        ]);

        // Oppretter dyreshow med elefanter
        var elephantShow = new AnimalShow<Elephant>([
            new Elephant("Jumbo", FoodConsumption.Herbivore, 20, 3.5),
            new Elephant("Tiny", FoodConsumption.Herbivore, 2, 0.8)
        ]);

        // Kjører dyreshow - fungerer takket være kovarianse
        ZooManager.RunShow(lionShow);
        ZooManager.RunShow(elephantShow);
        // Oppretter dyresorterere
        var animalSorter = new AnimalSorter<Animal>();

        // Organiserer utstillinger - fungerer takket være kontravarianse
        List<Lion> allLions = [
            new Lion("King", FoodConsumption.Carnivore, 10, "Golden"),
            new Lion("Queen", FoodConsumption.Carnivore, 8, "Light Brown"),
            new Lion("Prince", FoodConsumption.Carnivore, 2, "Golden")
        ];

        ZooManager.OrganizeExhibit(allLions, animalSorter); // AnimalSorter<Animal> brukes for IEnumerable<Lion>

        // Oppretter en generisk beholder for alle typer dyr
        var generalPen = new AnimalPen<Animal>(20);

        // Demonstrerer både kovarianse og kontravarianse
        ZooManager.FeedAndTrain(lionPen, generalPen);
        ZooManager.FeedAndTrain(elephantPen, generalPen);
        ZooManager.FeedAndTrain(penguinPen, generalPen);
    }
}


/// <summary>
/// Her lager vi en "stack" av tall, med størrelse stacksize
/// Vi åpner opp for å "pushe" nye tall til stacken, samt "poppe" ut tall fra stacken. 
/// </summary>
/// <param name="stackSize"></param>
public class IntegerStack(int stackSize)
{
    private int _position;
    private int[] _data = new int[stackSize];

    public void Push(int num) => _data[_position++] = num;

    public int Pop() => _data[--_position];

}

/* Hvis vi ville gjort noe lignende for en vilkårlig datatype, kan det være fristende å bruke den ukjente datatypen object.
object er datatypen alle typer i C# arver fra, men da flytter vi ansvaret fra oss, til compileren, å passe på å typesafe funksjonen vår. */

/// <summary>
/// Selv om dette er fult gyldig kode, så kan vi få problemer siden stacken her kan holde alle mulige datatyper til en hver tid, og vi har ingen garanti for at det
/// Pop metoden returnerer er dataen vi er interesert i. 
/// </summary>
/// <param name="stackSize"></param>
public class ObjectStack(int stackSize)
{
    private int _position;

    private object[] _data = new object[stackSize];

    public void Push(object obj) => _data[_position++] = obj;

    public object Pop() => _data[--_position];
    
}

/* Her kan vi heldigvis bruke Generics.
    Ved å bruke generics kan vi skrive vår Stack metode en gang, 
    men så gi den typen den skal jobbe med, når vi faktisk skal bruke den i koden.
*/

/// <summary>
/// Legg merke til at vi gir classen et Typeparameter
/// Etter navnedeklarasjonen, men før parameterene i primærkonstructoren gir vi classen vår et typeparameter
/// i <> som vi kaller for T. 
/// T er generelt standarden som blir brukt for å kalle på generiske typer,
/// du vil se samme T brukt både i C#, typescript, java og andre som støtter generiske type.
/// 
/// Det kan ofte være lurt å senere gi T et mer beskrivende navn, i tillegg.
/// 
/// Hvis du kikker på kildekoden til Dictionary for eksempel, vil du se deres T heter TKey and TValue, for å markere hvilke type tilhører hvilke verdi i Dictionaryet. 
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="stackSize"></param>
public class Stack<T>(int stackSize)
{
    private int _position;
    
    private T[] _data = new T[stackSize];

    public void Push(T obj) => _data[_position++] = obj;

    public T Pop() => _data[--_position];
}

