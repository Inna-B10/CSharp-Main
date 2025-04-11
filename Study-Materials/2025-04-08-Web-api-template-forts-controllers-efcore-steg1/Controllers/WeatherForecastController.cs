using Microsoft.AspNetCore.Mvc;
using web_api_template_forts_controllers_efcore.Models;

namespace web_api_template_forts_controllers_efcore.Controllers;

/*
Legg merke til hjelpeattributtene over classen her.
Vi brukte atributter som dette i xunit også. 
Det er attributter vi kan gi classen 
for å spesifisere enda mer til vår compiler hva type arbeid denne classen kan være
Enten for å passe informasjon til et senere build eller map steg, eller for å sette begrensinger på hva denne classen kan brukes til.
I vårt tilfelle her ser vi at vi har begrenset denne kontrolleren til å være en api controller
og at den kun skal mappes til routen /[controller], eller i vårt tilfelle /WeatherForecast
Legg merke til at routen vår ligner på RawUrl konseptet vi så på i forrige uke, det betyr at når noen vil ha weatherforecast resursen vår
er det denne controlleren sin jobb å passe på at rett resurs er levert tilbake etter endt forespørsel.
*/
[ApiController]
[Route("/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
