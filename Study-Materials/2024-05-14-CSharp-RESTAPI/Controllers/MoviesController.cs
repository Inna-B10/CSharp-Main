using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http;


[ApiController]
[Route("movies")]

public class MoviesController : ControllerBase
{
    private static List<Movies> _movies = new List<Movies> {
            new Movies {Id = 1, Title = "The Shawshank Redemption", Director = "Frank Darabont", ReleaseYear = 1994, ImageURL = "images/1.png"},
            new Movies {Id = 2, Title = "The Godfather", Director = "Francis Ford Coppola", ReleaseYear = 1972, ImageURL = "images/2.png"},
            new Movies {Id = 3, Title = "Schindlers List", Director = "Steven Spielberg", ReleaseYear = 1993, ImageURL = "images/3.png"},
            new Movies {Id = 4, Title = "Pulp Fiction", Director = "Quentin Tarantino", ReleaseYear = 1994, ImageURL = "images/4.png"},
            new Movies {Id = 5, Title = "The Good, The Bad and The Ugly", Director = "Sergio Leone", ReleaseYear = 1966, ImageURL = "images/5.png"},
            new Movies {Id = 6, Title = "Fight Club", Director = "David Fincher", ReleaseYear = 1999, ImageURL = "images/6.png"},
            new Movies {Id = 7, Title = "One Flew Over The Cuckoo's Nest", Director = "Milos Foreman", ReleaseYear = 1975, ImageURL = "images/7.png"}
        };

    [HttpGet]
    public IEnumerable<Movies> Get()
    {
        return _movies;
    }

    [HttpPost]
    public IActionResult Post([FromBody] Movies movies)
    {
        if (movies == null)
        {
            return BadRequest("Movie is null!");
        }

        _movies.Add(movies);
        return CreatedAtAction(nameof(Post), new { id = movies.Id, title = movies.Title, director = movies.Director, releaseYear = movies.ReleaseYear, movies.ImageURL }, movies);
    }
}