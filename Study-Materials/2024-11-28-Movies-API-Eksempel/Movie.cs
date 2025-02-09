using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kartlegging_API_Eksempel;

//Her har vi en modell av dataen vi vil hente fra hvert objekt i movies.json.
public class Movie
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    [JsonPropertyName("year")]
    public int Year { get; set; }
    [JsonPropertyName("cast")]
    public required List<string> Cast { get; set; }
    [JsonPropertyName("genres")]
    public required List<string> Genres { get; set; }
}
//Her har vi en modell av hvordan vår eksisterende data ser ut, samt hvordan å hente de inn.
public class MovieContext
{
    public required List<Movie> Movies {get;set;}
    public MovieContext(){
        var jsonString = File.ReadAllText("movies.json");
        Movies = JsonSerializer.Deserialize<List<Movie>>(jsonString) ?? [];
    }
}
