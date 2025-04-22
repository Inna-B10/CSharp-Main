/* Her lager vi en referanse til et builder objekt, som kan samle alle konfigurasjonsobjekter samt en ting som heter depencency containeren under en fellesreferanse, slik at vi kan fylle de ut før vi faktisk "bygger" appen vår.
Måten dette fungerer på er via en designpattern som heter builder prinsippet, hvor du stykker opp constructoren av et objekt i flere byggesteg. */
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
/* Ett av stegene vi har tilgjengelige for oss, er at vi kan legge til et sett med Services til applikasjonen vår. 
Mangen av disse servicene er tilgjengelige via det som heter dependency injection. Vi skal kikke på hvordan dette ser ut senere når vi introduserer Controllers inn i applikasjonen vår.*/
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

/* Dette er middleware for å redirekte http requests til https request, om de korekte sertifikatene er satt opp. */
app.UseHttpsRedirection();
/* Her kjører vi applikasjonen vår. Fungerer veldig likt hvordan vi kjørte en webserver i forrige uke.
Den aktiverer en Listener på vår current url, men håndterer redirection og routing for oss. */
app.Run();

