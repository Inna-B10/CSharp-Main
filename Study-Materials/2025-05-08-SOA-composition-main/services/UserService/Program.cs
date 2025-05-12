var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Welcome to the User Service");
app.MapGet("/{UserId}", (int UserId) => $"Welcome user {UserId}");

app.Run("http://0.0.0.0:80");
