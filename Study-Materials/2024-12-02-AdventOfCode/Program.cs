internal class Program
{
    public static void Main()
    {
        //en textfil som inneholder noen å tusen rader med to hele tall, separert med en \t.
        //Importere listen som en arrat av text, hver rad er et element med to nummerpar. 
        //Genere to lister av tall.
        //sortere begge listene, lavest først. 
        //mål forskjell mellom hver tallgruppe i listene:
        //(liste1[0] - liste2[0]) og lagre den absolutte avstanden. 
        //sluttverdien er totalsummen av alle avstander.
        var text = File.ReadAllLines("numbers.txt");
        var numArr1 = new int[text.Length];
        var numArr2 = new int[text.Length];
        var resultArray = new int[text.Length];
        //for hvert element i text, ta ut et tallpar, splitt parret på \t karater, og parse hvert til int, før de lagres i hvert sitt tall array.
        for (int i = 0; i < text.Length; i++)
        {
            var strArr = text[i].Split("   ");
            //Oversetter fra string til int.
            numArr1[i] = int.Parse(strArr[0]);
            numArr2[i] = int.Parse(strArr[1]);
        }

        /* //LØSNING 1
        var sortedNumArr1 = numArr1.OrderBy(n => n).ToArray();
        var sortedNumArr2 = numArr2.OrderBy(n => n).ToArray();
        for (int i = 0; i < text.Length; i++)
        {
            resultArray[i] = Math.Abs(sortedNumArr1[i] - sortedNumArr2[i]);
        }
        Console.WriteLine(resultArray.Sum()); */
        var numArr2NumberCountDict = new Dictionary<int, int>();
        //Itererer gjennom numArr2, og teller hvor mange ganger et tall eksisterer i numArr2, og legger tall og count som key/values i dictionary over.
        foreach (var number in numArr2)
        {
            if (!numArr2NumberCountDict.ContainsKey(number))
            {
                numArr2NumberCountDict[number] = 1;
            }
            else
            {
                numArr2NumberCountDict[number]++;
            }
        }
        for (int i = 0; i < text.Length; i++)
        {
            //for hvert element, sjekk om elementet har en count i dictionaryet, og legg elementet * count fra dictionary in i result. hvis count = 0, legg inn 0
            var count = numArr2NumberCountDict.TryGetValue(numArr1[i], out var result);
            if (!count) resultArray[i] = 0;
            else
            {
                resultArray[i] = result * numArr1[i];
            }
        }
        Console.WriteLine(resultArray.Sum());
    }
}