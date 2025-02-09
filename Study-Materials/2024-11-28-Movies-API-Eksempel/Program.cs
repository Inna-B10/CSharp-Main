using Kartlegging_API_Eksempel;


//Denne apien er et enkelt eksempel på en Model View Controller API.
//Her lager vi en Model: Movies.cs som er en model av vår data i movies.json.
//Vi har en MovieController.cs som kontrollerer dataflyten fra vår data hentet fra movies.json,
//og returnerer innsyn(viewData) i dataen vi har basert på requests. 

//Mesteparten av setupen her, er laget av en template som er i DotNet -> dotnet new webapi -controller.


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Her forteller vi til builderen av vår API at den må initialisere en utgave av vår MovieContext og holde den i memory,
//tilgjengelig for alle controllerene som trenger den. 
builder.Services.AddSingleton<MovieContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
