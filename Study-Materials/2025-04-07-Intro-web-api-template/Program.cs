/* Her lager vi en referanse til et builder objekt, som kan samle alle konfigurasjonsobjekter samt en ting som heter depencency containeren under en fellesreferanse, slik at vi kan fylle de ut før vi faktisk "bygger" appen vår.
Måten dette fungerer på er via en designpattern som heter builder prinsippet, hvor du stykker opp constructoren av et objekt i flere byggesteg. */
using Intro_web_api_template.Interfaces;
using Intro_web_api_template.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
/* Ett av stegene vi har tilgjengelige for oss, er at vi kan legge til et sett med Services til applikasjonen vår. 
Mangen av disse servicene er tilgjengelige via det som heter dependency injection. Vi skal kikke på hvordan dette ser ut senere når vi introduserer Controllers inn i applikasjonen vår.*/
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


/* Her legger vi til en service som skal representere vår TaskContext. 
Singleton betyr at det bare skal eksistere en "lifetime" av dette objektet, så lenge appen vår kjøres.
Aka hver gang vi refererer til en TaskContext i en metode i appen vår, refererer vi til samme instans av TaskContext.

Vi legger en instans av TaskContext inn i vår Dependency Container.   */
builder.Services.AddSingleton<ITaskContext, TaskContext>();

var app = builder.Build();

/* I templaten har vi tilgjengeliggjort statiske filer mye lettere via den high-level metoden UseStaticFiles(), som by default tilgjengeliggjør alle filer i wwwroot folderen i egne routes. 
Den ene linjen kode her, erstatter hele koden fra forrige uke for å matche en inkommende url path med en eksisterende fil i webroute folderen vår.  */
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


/* La oss prøve å legge til en egendefinert Route. Vi kan gjøre dette via å velge hvilken http metode vi vil lytte etter, og velge den korresponderende Map metoden på app.
I vårt tilfelle la oss prøve å lage en metode som Mapper en request til default access routen vår. */
app.MapGet("/", ()=> "Hello, gang!");
/* Vi har nå bedd vår app, om å lytte etter get requests til default routen sin (tenk prefixUrl fra forrige uke). Når den får en get request til den routen, skal den returne teksten Hello, gang! */

/* La oss sette opp modellene fra TaskManager appen vår fra to uker siden. */
/* Vi kan tilgjengeliggjøre de samme modellene i vår api. se linje 20 */

/* Legg merke til at lytterene nedenfor "lytter" på en tillegsbit til prefixUrlen vår. 
Det kan være lurt at denne "suffixen" er en god, og berskrivende endepunkt som forklarer tydlig til brukeren vår hva resurs de jobber mot.*/

/* Legg også merke til at vi refererer til en TaskContext som parameter i alle etterfølgende metoder. 
Det vi gjør her er å hente vår Singleton TaskContext objekt fra dependency containeren vår, og injekter den inn i et endepunkt når den blir callet.  */

/* La oss lage et endepunkt som kan legge til en Task. Legg merke til vi bruker http metoden POST
som er standardmetoden som referer til at en klient prøver å "poste" en ny resurs til vårt endepunkt. */

app.MapPost("/taskmanager", (string title, string description, DateTime dueDate, ITaskContext context ) => context.AddTask(title, description, dueDate));

/* La oss lage et endepunkt som kan gette alle tasks. */

app.MapGet("/taskmanager", (ITaskContext context) => context.GetAllTasks());

/* Vi lager endepunkter for å hente pending og complete */

app.MapGet("/taskmanager/complete", (ITaskContext context) => context.GetCompleteTasks());

app.MapGet("/taskmanager/pending", (ITaskContext context) => context.GetPendingTasks());

/* Vi kan lage endepunkter som kan fetche enkelt tasks */
/* Legg merke til at vi kan hente ut id fra route via {id}, husk tilbake til hvordan rawurl kunne brukes til å lese routen vår bak prefixUrlen vår.  */
app.MapGet("/taskmanager/{id}", (int id, ITaskContext context) => context.GetTaskById(id));


/* Hvis en klient ønsker å endre en resurs vi tilgjengeliggjør, bruker de gjerne metoden Patch. Siden vi her ønsker å complete en task via en id, kan vi appende en {id} til vår taskmanager/complete route. */
app.MapPatch("/taskmanager/complete/{id}", (int id, ITaskContext context) => context.CompleteTask(id));

/* Det samme hvis vi skal slette en resurs, når en klient ønsker å slette en resurs vi tilgjengeliggjør, så bruker de gjerne http metoden DELETE */
app.MapDelete("/taskmanager/{id}", (int id, ITaskContext context) => context.DeleteTask(id));


/* For å teste appen vår på dette tidspunktet kan det være greit å bruke run daemonen tilgjengelig for oss i dotnet dev tools.
Daemonen lar oss lytte etter filendringer, og softrestarte programmet vårt. Vi slipper å stenge og starte applikasjonen vår på nytt hver gang vi gjør endringer.
Den er tilgjengelig via dotnet watch

for eksempel for å kjøre denne applikasjonen via watch bruker vi 

dotnet watch run
 */

 /* Legg merke til at når vi starter applikasjonen på denne måten, så åpnes apiet vårt via "swagger test".

 Det er middleware tilgjengeliggjort for oss her:
 if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

og som lager en testsuite for oss, hvor vi kan teste de forskjellige endepunktene vi tilgjengeliggjør i apiet vårt.
  */
/* Dette er middleware for å redirekte http requests til https request, om de korekte sertifikatene er satt opp. */
app.UseHttpsRedirection();
/* Her kjører vi applikasjonen vår. Fungerer veldig likt hvordan vi kjørte en webserver i forrige uke.
Den aktiverer en Listener på vår current url, men håndterer redirection og routing for oss. */
app.Run();

