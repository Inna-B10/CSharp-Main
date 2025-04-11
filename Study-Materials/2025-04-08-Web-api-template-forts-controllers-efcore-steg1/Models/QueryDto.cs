using web_api_template_forts_controllers_efcore.Interfaces;

namespace web_api_template_forts_controllers_efcore.Models;


/* Dette er et Data Transfer Object som skal hjelpe oss å hente potensielle queryparametere fra vår httpquery (alt bak ? etter url).
Legg merke til nullabilitien til hver property, det er for at vi ikke kan garantere for hvilke parametere brukeren vil bruke. 
Vi ser i bl.a. swagger at disse automatisk er tilgjengelig for oss, via OpenApi sin autodokumentasjon.  */
public class QueryDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? BeforeDate { get; set; }
    public DateTime? AfterDate { get; set; }

    /// <summary>
    /// Denne metoden tar inn en ITaskContext interface, og bygger en spørring mot GetAllTasks basert på hvilke properties i DTOen som er i bruk.
    /// Hvis dto ikke har noen verdi i properties, er query returnert som GetAllTasks();
    /// 
    /// AsQueryable() lar oss bygge opp en query som sett videre. 
    /// Vi kan se for oss at vi potensielt bygger opp en lang chain av conditionals.
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public IQueryable<IUserTask> BuildQuery(ITaskContext context)
    {
        var query = context.GetAllTasks().AsQueryable();

        if (!string.IsNullOrWhiteSpace(Title)) query = query.Where(task => task.Title.Contains(Title, StringComparison.InvariantCultureIgnoreCase));

        if (!string.IsNullOrWhiteSpace(Description)) query = query.Where(task => task.Description.Contains(Description, StringComparison.InvariantCultureIgnoreCase));

        if (BeforeDate.HasValue) query = query.Where(task => task.DueDate < BeforeDate);

        if (AfterDate.HasValue) query = query.Where(task => task.DueDate > AfterDate);

        return query;
    }
}
