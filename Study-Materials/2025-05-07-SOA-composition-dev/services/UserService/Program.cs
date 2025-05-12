var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Welcome to the User Service");

app.Run("http://0.0.0.0:80");
