using System.Text.Json.Serialization;
using web_api_template_forts_controllers_efcore.Interfaces;

namespace web_api_template_forts_controllers_efcore.Models;


/* Dette representerer et data transfer object, som skal kunne flytte data mellom en JsonBody til context.AddTask metoden vår. 
Vi kan bruke autodeserialisering av request.Context.Body for å patternmatche felt til våre properties. 
Det blir gjort svært lett via JsonPropertyName attributtene. */
public class TaskDto
{
    [JsonPropertyName("title")]
    public required string Title { get; set; }
    [JsonPropertyName("description")]
    public required string Description { get; set; }
    [JsonPropertyName("DueDate")]
    public DateTime DueDate { get; set; }

    public IUserTask InsertTask(ITaskContext context)
    {
        return context.AddTask(Title, Description, DueDate);
    }
}
