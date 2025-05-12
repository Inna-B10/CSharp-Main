var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Welcome to the Task Tracker");
app.MapGet("/{UserId}", (int UserId) => $"Tasks for user: {UserId}");

app.Run("http://0.0.0.0:80");
