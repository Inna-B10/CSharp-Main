using System.Data.Common;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api_template_forts_controllers_efcore.Interfaces;
using web_api_template_forts_controllers_efcore.Models;

namespace web_api_template_forts_controllers_efcore.Controllers;

/// <summary>
/// Her implementerer vi vår egen controller.
/// Vi automapper igjen url til controlleren lik navnet til controlleren,
/// som vi så var mulig i eksempel templaten vår.
/// dvs at RawUrl /task refererer til denne resursen. 
/// Legg merke til at vi kan ta inn referanser til context og andre ting via primærconstructoren, om vi skulle ønske det.
/// bl.a. hvis vi ønsker logging, via primærconstructoren til Controlleren vår.
/// Da er disse tilgjengelig i metodene nedenfor.
/// 
/// Det kan også være viktig å vite at disse Controllerene eksisterer i et Transient Scope.
/// Det vil si objektet eksisterer i øyeblikket appen vår matcher en request mot en mappet controller-sti
/// og da kan metodene våre konsumere elementer fra primærconstructoren direkte.
/// 
/// Vi kan tenke oss at app caller vår controller slik:
/// 
/// Det kommer en forespørsel mot GET /tasks/Complete:
/// return new TaskController().GetComplete() (ish);
/// 
/// 
/// </summary>
/// <param name="context"></param>
/// <param name="logger"></param>
[ApiController]
[Route("/[Controller]")]
public class TaskController(ITaskContext context, ILogger<TaskController> logger): ControllerBase
{
    /* Legg merke til at metodene våre også markerer hvilken http metode de skal matche, via Attributtene sine. */
    [HttpGet]
    /* Det kan være lurt å matche navnet på metoden i controlleren, med navnet på http metode + subroute. */
    public async Task<IActionResult> Get([FromQuery] QueryDto queryDto) 
    {
        try 
        {
            return Ok(await queryDto.BuildQuery(context));
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    /* Legg merke til at også her er hvert endepunkt gjort om til asynkrone Tasks, som nå kan kjøres på sin egen threadpool. Da ungår vi at forespørsler kan blokkere hverandre. 
    Siden vi vet at hoved-threaden vår "tilgjengeliggjøres" og kan behandle andre operasjoner, mens den awaiter resultatet for hver oppgave. */

    [HttpGet("/complete")]
    /* Se navngivningen på denne metoden. */
    public async Task<IActionResult> GetComplete(){
        try
        {
            return Ok(await context.GetCompleteTasks());
        }
        catch(Exception ex)
        {
            logger.LogError(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    
    /* Nedenfor ser dere en simpel måte å legge på hvilke potensielle statuskoder endepunktet leverer tilbake, det kan gjøre autodokumentasjonen fra swagger hakket bedre.  */
    [HttpGet("/pending")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPending()
    {
        try 
        {
            return Ok(await context.GetPendingTasks());
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    /* Vi kan ta inn elementer parsed fra RawUrl her også, som vi kunne med vår minimal-api struktur. */
    [HttpGet("{id}")]
    /* Legg og merke til metodeoverloadingen her, en controller basert api er en naturlig plass hvor overloading eksisterer.  */
    public async Task<IActionResult> Get(int id)
    {
        try 
        {
            return Ok(await context.GetPendingTasks());
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPatch("/complete/{id}")]
    public async Task<IActionResult> CompleteId(int id)
    {
        try
        {
            return Ok(await context.CompleteTask(id));
        }
        catch(DbUpdateException ex)
        {
            logger.LogCritical(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TaskDto taskDto)
    {
        try
        {
            return Ok(await taskDto.InsertTask(context));
        }
        catch (JsonException ex)
        {
            logger.LogError(ex.Message);
            return BadRequest();
        }
        //Vi kan og nå spesifisere enda mer hvilke errorer vi vil gi tilbake.
        catch (DbUpdateException ex)
        {
            logger.LogCritical(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            return Ok(await context.DeleteTask(id));
        }
        catch (DbUpdateException ex)
        {
            logger.LogCritical(ex.Message);
            return StatusCode(500, ex.Message);
        }
    } 

    [HttpGet("Log")]
    public IActionResult GetLog()
    {
        logger.LogInformation("I'm called!");
        return Ok();
    }
}