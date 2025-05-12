var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Welcome to the Task Tracker");

app.Run("http://0.0.0.0:80");
