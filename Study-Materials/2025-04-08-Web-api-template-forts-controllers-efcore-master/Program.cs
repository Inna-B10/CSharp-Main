var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/* Legg merke til to nye linjer i templaten vår. 
Vi har dette steget, hvor vi ligger alle controllerene våre i dependencycontaineren vår. */
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
