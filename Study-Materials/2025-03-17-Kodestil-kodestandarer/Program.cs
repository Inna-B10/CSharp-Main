using System.Runtime.CompilerServices;
using System.Text;

namespace Kodestil_kodestandarer;

class Program
{
    static void Main(string[] args)
    {
        //Navngi variabler:

        //Vi bruker camelCase for å navni variabler som bare skal eksistere i scopet vi jobber i.

        //Som regel trenger vi bare definere datatypen på en side av deklarasjonstegnet:
        int num = 23;

        //Det er også gyldig å skrive følgende:
        var number = 42;
        //Men da må vi stole på at datamaskinen implicit velger korrekt datatype fra verdien på venstre siden.
        //I tilfellet over, velger datamaskinen at 42 er en integer. 

        //Det samme gjelder når vi definerer kolleksjoner. 
        //Så lenge datatypen kan trygt hentes fra høyre siden av deklarasjonstegnet,
        //Er det helt trygt å bruke var på venstre siden.
        var numList = new List<int>();
        var arr = new int[3];


        //Men hvis vi skal definere en statisk kolleksjon med forhåndsdefinerte verdier,
        //Er det annbefalt fra Microsoft å definere type på venstre side av definisjonstegnet,
        //Så bruke kolleksjonsekpressionen [] for å definere startverdiene av kolleksjonen.
        List<string> names = ["Anders", "John", "Kevin", "Håkon"];

        //Legg merke til at dette kan skape problemer hvis du definerer arrays:
        int[] numArr = [3];
        //I motsettning til arr på linje 23, har vi her definert et array på lengde 1,
        //hvor numArr[0] = 3, 
        //mens på linje 23, definerer vi et array på lengde 3, med default verdier for ints
        //i hver posisjon. 


        //I expressions, spesielt i LinQ spørringer, delegater eller andre lambda funksjoner
        //er det lurt å være så beskrivende som mulig.
        //Det samme gjelder også variabelnavn generelt.
        //Vell og merke, med variabelnavn er det ingen full standard, men vi annbefaler å være så beskrivende som mulig, siden andre enn deg skal
        //lese koden din. 
        
        //Vell og merke, når det kommer til variabelnavn er det ganske mye debatt.


        //Se forskjellen mellom følgende spørringer:

        var res = names.Where(n=>n.StartsWith("a", StringComparison.InvariantCultureIgnoreCase));
        //Det kan være vanskelig å vite både hva res faktisk er, og hva n kan være. 
        //for deg som koder, og som har logikken i hodet, er det lett å følge, men det kan likevell være lett å miste helhetsbildet, når vi ikke bruker gode beskrivende navn.
        //Denne måten å skrive spørringer på vil dere se ofte i større sql kodebaser, hvor statistikkerene sparer tid. Der er det veldig vanlig å bare bruke
        //single letter naming for å gi alias til tabellene de gjør spørringer mot.
        //Men det kan være lurt, i allfall mens dere er ny til lambda expressions, å prøve å være så beskrivende som mulig, slik at dere har oversikt over hva dere jobber med til en hver tid.

        var namesStartingWithA = names.Where(name=>name.StartsWith("a", StringComparison.InvariantCultureIgnoreCase));
        //Her er det ganske åpenbart hva spørringen vil gjøre, og hva hvert element i names er. 
        //Legg merke til hvor beskrivende standardbiblioteket er i navngivningen sin. 
        //f.eks se på stringcomparison structen. Det er veldig selvforklarende ved å lese navnet på valgt enumverdi, hva som kommer til å skje.
        //StringComparison navnet, gjør det åpenbart at denne enumen er her for å gi valg for hvordan strings skal sammenlignes,
        //InvariantCultureIgnoreCase forteller veldig tydlig hva dette valget gjør.


        //En annen ting dere gjerne kan gjøre, spesielt nå når dere er ny, er å bruke explicit types, isteden for var. 

        IEnumerable<string>? namesStartingWithJ = names.Where(name => name.StartsWith("j", StringComparison.InvariantCultureIgnoreCase));
        //Her er det veldig åpenbart at namesStartingWithJ fremdeles representerer en Interface, og har ikke blitt "kollapset" til en faktisk spørring mot datasettet names enda. 
        //Dette valget er mer smak og behag, men det er definitivt et verktøy som kan hjelpe hvis man føler man mister oversikt over hva man jobber med til en hver tid. 
        //Hovedregelen er å bare bruke var når det er tydlig for leseren hva type variablen får av expressionen på
        //høyre siden av definisjonstegnet.

        //Strings:


        //Skal du concatenate korte strenger, bruk string interpolation:

        string firstTwoNamesOfNames = $"{names[0]}, {names[1]}";

        //Skal du loope gjennom, og concatenate flere strenger sammen til en lengre streng, gjør det til vane å bruke StringBuilder.
        var allTheNames = new StringBuilder();
        names.ForEach(name => allTheNames.Append(name));
        Console.WriteLine(allTheNames);

        Dictionary<string, int> nameAndAge = new (){
            {"John", 33},
            {"Anders", 42},
            {"Kevin", 28},
            {"Håkon", 29},
            {"Andrea", 37},
            {"Elisabeth", 25},
            {"Eivind", 30}
        };


        //Skal du printe ut verdier for flere ting i en loop, bør du igjen bruke string interpolation.
        foreach (var nameAgePair in nameAndAge)
        {
            Console.WriteLine($"{nameAgePair.Key} is {nameAgePair.Value} years old");
        }

        //Hvis du trenger special characters og spesiell tekst formatering. 
        //Bruk string literals:

        var text = """
        Hello, I am formatted text.

            I can contain "special characters"
        normally not allowed in a string.
        Like \n and \t and \b without escaping them.
        """;
        Console.WriteLine(text);
        //Legg merke til at formateringen er beholdt i terminalen. 

        //Tilsvarende formatering i en standard streng vil se noelunde slik ut:
        var nonLiteralText = "Hello, I am formated text.\n\n\tI can contain \"special characters\"\nnormally not allowed in a string.\nLike\\n and \\t and \\b without escaping them.";
        Console.WriteLine(nonLiteralText);
        //Den er tung og vanskelig å lese for andre kodere.
    }
}
