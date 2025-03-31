using System.Net.Http.Headers;
using Asynkron.Models;


// See https://aka.ms/new-console-template for more information


//Når vi ber vår klient å lage en ny tråd, kan vi også mate den en metode / oppgave den skal jobbe med. 
var newThread = new Thread(WriteNewThread);
newThread.Start();

//Etter vi har laget en tråd, og startet execution på den tråden, kan vi nå fortsette å gjøre arbeid på hovedtråden vår. 
for (int i = 0; i < 1000; i++) Console.WriteLine(i);


//Hjelpemetode som skal kjøres på sidetråden. 
static void WriteNewThread()
{
    for (int i  = 1000; i > 0; i--) Console.WriteLine(i);
}

//!!Thread har en del resursoverhead vs noen av de andre metodene for anynkron programmering som vi skal kikke på nedenfor.
//!!Når vi bruker Thread nøkkelordet, er programmet vårt nødt å designere en ny "stack" og heap fra grunn av. 
//!!Bruker vi andre metoder, som Task og async/await, tar vi nytte av en innebygget ThreadPool, et sett med ferdiginitierte execution threads
//!!Som er ferdig spunnet opp, og tilgjengeliggjort for oss. 

//Vi kan se for oss at vi får vårt program til å lage et eget subprogram som kjører parallellt på maskinen vår.

//pga måten Console.WriteLine fungerer på, vil det komme ut i "chuncks" av resultat.
//Siden Console.WriteLine kjører synkront kryss threads. 

//Det er en executionpath som kjører uavhenging av de andre vi har, så dette er IKKE det samme som conditional branching.

AsyncronCounter.CountTwoThreads();


//Vi kan nå waite for resultatet av vår task completion source method.
AsyncronCounter.CountTwoTasks().Wait();

//Vi kan her også awaite vår CountWithAsyncAwait.
await AsyncronCounter.CountWithAsyncAwait();