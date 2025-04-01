using Async_Eksempler.Models;
/* En ting som er greit å vite om HttpClients, er at det bør bli instansiert som en global variabel, men som en singleton.
Det har med måten den låses til en nettverks-socket i operativsystemet, og hvis man initialiserer flere, kan det fort skape problemer med låste sockets på systemet.  */
var client = new HttpClient();

/* C# sin innebygde HttpClient er et solid verktøy som håndterer alle standardiserte HttpMetoder,
og behandler Requests og Responses via den standardiserte http protokollen.
*/
/* For eksempel, for å "Get"te en response, kan man bruke GetAsync, som fungerer som en awaitable task. */
var response = await client.GetAsync("https://www.example.com/");
/* det lar oss hente en resurs fra en spesifikk url.*/
/* på responseobjektet kan vi nå lese av ganske mye om resultatet.

Det mest relevante vil være å se om responsen er en success eller ikke. */
Console.WriteLine(response.StatusCode);
/* Statuskoden vår kan også Castes til en Integer, om nødvendig.*/
Console.WriteLine((int)response.StatusCode);

/* Vi har også tilgang til en boolean som ser på statuskoden, og for denne gitte metoden, om det er en success eller ikke. */
Console.WriteLine(response.IsSuccessStatusCode);

/* Vi kan enforce at responsen må være OK via følgende metode, som thrower en exception hvis vi får en ikke-ok respons. */

response.EnsureSuccessStatusCode();

/* EnsureSuccessStatusCode passer på at, for den gitte http metoden, så får vi tilbake en standard og forventet Statuskode som viser at 
operasjonen var vellykket.

For vår Get, er den forventete statuskoden OK. */

/* Vi kan også hente ut, fra responsen, data om vår request. La oss kikke litt på den */
var request = response.RequestMessage;

/* Vi kan her se hvilken av httpprotokollens metode vi kjørte mot endepunktet vårt. */
Console.WriteLine(request?.Method);

/* Vi kan også se hvilken av http protokollene vi brukte */
Console.WriteLine(request?.Version);


/* Som alle standardiserte filer, har også en HttpResponse en Header med metadata om responsen. Her kan vi bl.a. finne informasjon om hva content vi har tatt i mot og hvor mye. */
var headers = response.Content.Headers;
Console.WriteLine(headers.ContentType);
Console.WriteLine(headers.ContentLength);

/* Vi ser her at vi mottar faktisk tekst i html format. La oss skrive dette til en html fil.
Dette kan også gjøres asynkront ved å åpen opp en filstrøm, en sekvensiell strøm av bits som skal plaseres på en lokasjon. */
using var fileStream = File.Create("result.html");
await response.Content.CopyToAsync(fileStream);


/* Vi har nå hentet ned og lagret en faktisk nettside fra nettet. */

/* En felle det kan være lett å falle i når man jobber med async await er å ikke ta i bruk evnen til å jobbe parallellt i koden din.
La oss kikke på et eksempel med vår HttpClient. */


/* Vi kan sette opp en static async method med en intern stopwatch som kan måle forskjell i processing tid for begge tilfellene. */
await FetchAsyncWithStopWatch.CompareSequentialVsParallellRequests(client);