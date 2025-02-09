using Microsoft.AspNetCore.Mvc;

namespace Kartlegging_API_Eksempel.Controllers
{
    //Her definerer vi hvilken "route" builderen vår skal mappe denne kontrolleren til. Siden vi bruker [controller]
    //Så mappes den til navnet til kontrolleren vår - Controller. I dette tilfellet /Movie
    [Route("[controller]")]
    [ApiController]
    //Siden denne kontrolleren blir "konstruert" hver gang serveren vår motar et kall til /Movie, og builderen vår har
    //initialisert en utgave av MovieContext, så kan vi si at den skal hentes inn, og brukes som parameter i vår controller. 
    //Da blir vår MovieContext en "dependency" som blir "injected" inn i vår Controller. 
    public class MovieController(MovieContext movies) : ControllerBase
    {
        //HttpGet er en attribut som forteller builderen vår, at denne controllermetoden mottar GET requests.
        [HttpGet]
        //Vi henter ut et input parameter queryParams fra http Query, og prøver å mappe det til en MovieQueryDTO.
        public IActionResult Get([FromQuery] MovieQueryDTO queryParams)
        {
            //Vi lager en query ved å bruke MovieQueryBuilder metoden vår, og returnerer resultatet. 
            var query = MovieQueryBuilder(queryParams);
            return Ok(query.ToList());
        }
        //Her henter vi en Index fra routen Movie/{index}, og leverer den filmen som eksisterer i Movies[index].
        [HttpGet("{index}")]
        public IActionResult Get(int index)
        {
            return Ok(movies.Movies[index]);
        }
        //Her lytter vi etter Movies/random. Og leverer en tilfeldig film.
        [HttpGet("random")]
        public IActionResult GetRandom()
        {
            return Ok(movies.Movies[Random.Shared.Next(0, movies.Movies.Count)]);
        }
        //Her lytter vi etter Movie/genrestatistics og leverer en kompleks spørring mot vår kontext,
        //hvor vi returnerer top fem sjangre som eksisterer i vår json fil.
        [HttpGet("genrestatistics")]
        public IActionResult GetGenreStatistics()
        {
            return Ok(movies.Movies.SelectMany(movie => movie.Genres)
                                .GroupBy(genre => genre)
                                .Select(group => new
                                    {
                                        Genre = group.Key,
                                        Count = group.Count()
                                    })
                                .OrderByDescending(genreData => genreData.Count)
                                .Take(5)
                                .ToList());
        }

        //Her tar vi inn en MovieBuilderDTO, og prøver å generere en queryable utgave av vår kontext,
        //basert på hva verdier vi klarer å hente ut fra vår MovieQueryDTO.
        IQueryable<Movie> MovieQueryBuilder(MovieQueryDTO queryParams)
        {
            var query = movies.Movies.AsQueryable();
            if (!string.IsNullOrEmpty(queryParams.Actor))
            {
                query = query.Where(movie => movie.Cast
                                                        .Any(actorName => actorName
                                                                                .Contains(queryParams.Actor, StringComparison.InvariantCultureIgnoreCase)));
            }
            if (!string.IsNullOrEmpty(queryParams.Title))
            {
                query = query.Where(movie => movie.Title!.Contains(queryParams.Title, StringComparison.InvariantCultureIgnoreCase));
            }
            if (!string.IsNullOrEmpty(queryParams.Genre))
            {
                query = query.Where(movie => movie.Genres
                                                            .Any(genre => genre
                                                                                    .Contains(queryParams.Genre, StringComparison.InvariantCultureIgnoreCase)));
            }
            if (queryParams.To.HasValue)
            {
                query = query.Where(movie => movie.Year <= queryParams.To);
            }
            if (queryParams.From.HasValue)
            {
                query = query.Where(movie => movie.Year >= queryParams.From);
            }
            return query;
        }
    }
}
