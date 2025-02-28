namespace Intro_linq;
using Intro_linq.Models;
class Program
{
    static void Main(string[] args)
    {
        var planets = new List<Planet>
        {
            new Planet { Name = "Mercury", Size = 4879, Position = 1 },
            new Planet { Name = "Venus", Size = 12104, Position = 2 },
            new Planet { Name = "Earth", Size = 12742, Position = 3 },
            new Planet { Name = "Mars", Size = 6779, Position = 4 },
            new Planet { Name = "Jupiter", Size = 139820, Position = 5 },
            new Planet { Name = "Saturn", Size = 116460, Position = 6 },
            new Planet { Name = "Uranus", Size = 50724, Position = 7 },
            new Planet { Name = "Neptune", Size = 49244, Position = 8 }
        };

        /* La oss si vi skal lage en ny liste over alle navnene til hver planet. */
         List<string> PlanetNames = [];
        foreach (var planet in planets)
        {
            PlanetNames.Add(planet.Name);
        } 
        //Eksempel på bruk av linq metoder for å "selecte" hvert navn til en ny liste, isteden for å loope.
        var planetNames = planets.Select(p => p.Name).ToList(); 


        //eksempel på det faktiske query språket vårt for å gjøre samme operasjon.
        var planetNamesWithLinQLang = (from planet in planets
                                                    select planet.Name).ToList();
/* 
        Eksempel på en lambda funksjon, som tar in en generisk Planet, og returnerer en bool
        Typedeklareringen Func<Planet, bool> forteller compileren at dette er en funksjon, som tar in venstresiden (Planet)
        som parameter, og returnerer høyresiden (bool) som returnverdi.

        vi gir den et navn som alle andre variabler FilterPlanets
        Vi bruker så Assignmentoperatoren = for å gi funksjonen vår en FunctionBody.

        Vi starter med en generisk representant av vårt input parameter planet.

        vi sier så at den planeten skal brukes i en påfølgende operasjon via => (lambda operator)

        så definerer vi operasjonen planeten skal brukes i: planet.Size < 7000;
        Som vi husker fra tidligere så er sammenligningsoperasjoner en operasjon som returnerer en boolean verdi,
        og det er verdien av denne opearsjonen som blir returnert av Func<Planet, Bool>
        */
        Func<Planet, bool> FilterPlanets = planet => planet.Size < 7000;

        /*
        Vi kan da bruke denne FilterPlanets funksjonen vår i operasjoner hvor operasjonen skal gjøres på en Planet
        og returnere en bool.

        Et eksempel på en slik metode er LinQ metoden Where()
        hvor metoden tar inn et element fra kolleksjonen den skal kjøre en spørring mot, 
        og kjører en boolean check mot elementet, for å se om elementet skal tas videre eller ikke.

        Legg merke til at vi ikke trenger å definere en placeholder for Planets som parameter i Where() metoden, vi trenger
        bare å legge inn funksjonsnavnet, siden funksjonen vår allerede sier at vi skal ta inn
        en planet, og returnere en bool, som er nøyaktig det .Where() på planets trenger. 
        */
        var smallPlanets = planets.Where(FilterPlanets).ToList(); 
        smallPlanets.ForEach(p => Console.WriteLine(p.Name));




        var numbers = new List<int>
        {
            12, 47, 3, 85, 6, 23, 78, 19, 56, 32,
            17, 29, 90, 44, 15, 62, 7, 39, 54, 81,
            10, 99, 27, 41, 68, 72, 13, 58, 76, 8,
            34, 25, 91, 49, 86, 14, 20, 64, 37, 50
        };

        /* Her definerer vi to nye lambda funksjoner, som skal gjøre operasjoner mot tall
        Den første skal se på et vilkårlig tall, og returnere en boolean verdi basert på 
        om tallet er Even eller Odd. */
        Func<int, bool>IsEven = num => num % 2 == 0;

        /* Den andre verdien leverer en boolean verdi basert på om et vilkårlig tall er større enn 80.*/
        Func<int, bool> IsGreaterThan80 = num => num > 80;

        /*Her skriver vi ut i terminalen vår alle tallene i numbers som er even. Legg merke til at vi ikke definerer en liste
        for å holde verdien av .Where() metoden vår. Vi lar den være anonym eller scoped i statementen. 
        Da vil runtimen vår vite at listen den definerer i statementen, kan etter statementen er ferdig å kjøre, 
        kastes vekk (garbage collected). */
        numbers.Where(IsEven).ToList().ForEach(Console.WriteLine);

        /* For å få tilsvarende effekt uten å bruke link, måtte vi ha definert en scoped metode,
        som definerer et scoped list i metodebody slik at runtimen ved at listen kan garbage collectes etter scopet er kjørt.*/
        PrintEventNumbers(numbers);

        /* Vi kan også chaine spørringer og metoder etterhverandre, 
        og sakte men sikkert bygge opp en kompleks spørring mot en kolleksjon */
        var query = numbers.Where(IsEven);
        query = query.Where(IsGreaterThan80);

        /*Legg merke til at IsEven funskjonen ikke blir kjørt før vi faktisk konsumerer spørringen ved å lage
        data av den (aka kjører en ToList() metode, eller looper gjennom).
        IEnumerable<int> datatypen som query er, representerer mer et løfte at en eller annen
        gang skal disse metodene kjøres.
        Men de blir ikke kjørt før vi faktisk bruker de til å lage data, som sett nedenfor. */
        query.ToList().ForEach(Console.WriteLine);


        /* Vi kan også lage mer kompliserte lambdafunksjoner, som denne her som tar inn et vilkårlig tall
        og returnere en gitt streng basert på tallets størrelse.  */
        Func<int, string> GenerateKeys = num => num switch
        {
            < 30 => "Less than 30",
            < 50 => "Less than 50, greater than 30",
            < 80 => "Less than 80, greater than 50",
            >= 80 => "Greater than 80"
        };

        /* Her bruker vi lambdafunskjonen over, for å gruppere hvert nummer i numbers basert på kravet satt i GenerateKeys. */
        numbers.GroupBy(GenerateKeys).ToList().ForEach(group => Console.WriteLine($"The number of numbers {group.Key} is: {group.Count()} and all the numbers are: {string.Join(", ", group)}"));

        Console.WriteLine(numbers.Sum());
        


        var names = new List<string>
        {
            "Alice", "Bob", "Charlie", "Diana", "Eve",
            "Frank", "Grace", "Hank", "Ivy", "Jack",
            "Alice", "Eve", "Bob", "Charlie", "Grace",
            "Diana", "Eve", "Frank", "Ivy", "Jack",
            "Alice", "Bob", "Diana", "Charlie", "Frank",
            "Hank", "Ivy", "Eve", "Grace", "Jack",
            "Alice", "Hank", "Diana", "Bob", "Frank",
            "Charlie", "Ivy", "Jack", "Eve", "Grace"
        };
        /* Her henter vi først ut interfacen knyttet til names, via AsEnumerable()
        Vi sier så at her skal vi bare hente hver Distincte (unike) instans av elementet i names,
        før vi kollapser spørringen i ToList(), og looper gjennom med ForEach. */
        names.AsEnumerable().Distinct().ToList().ForEach(Console.WriteLine);
       /*  names.GroupBy(name => name).ToList().ForEach(group => Console.WriteLine($"The name {group.Key} occurs {group.Count()} times in names")); */

    }
    public static void PrintEventNumbers(List<int> numbers)
    {
        List<int> evenNumbers = [];
        foreach(var number in numbers)
        {
            if (number % 2 == 0)
            {
                evenNumbers.Add(number);
            }
        }
        foreach(var num in evenNumbers)
        {
            Console.WriteLine(num);
        }
    }
}
