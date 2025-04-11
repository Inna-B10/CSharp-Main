using Microsoft.EntityFrameworkCore;
using web_api_template_forts_controllers_efcore.Interfaces;
using web_api_template_forts_controllers_efcore.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/* Legg merke til to nye linjer i templaten vår. 
Vi har dette steget, hvor vi ligger alle controllerene våre i dependencycontaineren vår. */
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


/* Vi har nå gjort om vår context til en databasecontext knyttet til EF code. 
Legg merke til her at vi legger ved noe som heter en connectionString. Dette er måten ef-core knytter seg til databasen, og den vil være forskjellig for forskjellige databasetyper.
Vi bruker SQLite her, som er en ekstremt lettvektig form av sql database. Dere vil se når vi bruker kommandoene dotnet ef migration add og dotnet ef database update, at vi genererer denne databasefilen, med et sett med schemaer basert på vår modell. 
Vi kan bruke extentionene SQLite i vscode for å faktisk studere databasen vår.

Her går det fult ann å sette opp for andre databasetyper også. Da må vi ha rett ef-core verktøy for databasen. */
builder.Services.AddDbContext<ITaskContext, TaskContext>(options => {
    options.UseSqlite("Data Source=Tasks.db");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

/* På dette steget mapper vi våre controllere til vår spesifikk prefix, slik at korrekt RawUrl etter prefiksen kan triggre korrekt controller. 
Man har gjerne en controller pr "resurs" man vil tilgjengeliggjøre. Har man en login eller user resurs, har vi som regel en login eller user controller for å håndtere denne separat. 

I vårt tilfelle i bare dette template steget har vi en WeatherForecastController som styrer resursforespørsler mot WeatherForecast modellen.  */
app.MapControllers();

app.Run();
