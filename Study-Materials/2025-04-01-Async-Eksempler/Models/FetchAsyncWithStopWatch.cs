using System.Diagnostics;

namespace Async_Eksempler.Models;

public static class FetchAsyncWithStopWatch
{

    /// <summary>
    /// Vår test metode tar inn en instans av en httpClient, slik at vi kan kjøre requests på den.
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    public async static Task CompareSequentialVsParallellRequests(HttpClient client)
    {
        /* Her er en intern liste av endepunkter, hvert av de representerer en placeholder post i jsonplaceholder apiet.  */
        List<string> endpoints = [
            "https://jsonplaceholder.typicode.com/posts/1",
            "https://jsonplaceholder.typicode.com/posts/2",
            "https://jsonplaceholder.typicode.com/posts/3",
            "https://jsonplaceholder.typicode.com/posts/4",
            "https://jsonplaceholder.typicode.com/posts/5"
        ];

        //Vi starter en enkel intern stopwatch og starter den.
        var sequentialStopWatch = Stopwatch.StartNew();


        Console.WriteLine("Starting Sequential Fetching of urls... ");

        /* Vi looper gjennom hvert endepunkt og awaiter resultatet for en Get request mot endepunktet. */
        foreach (var endpoint in endpoints)
        {
            await FetchFromEndpoint(endpoint, client);
        }

        //etter sekvensene er ferdig stopper vi stopwatchen vår, og rapporterer hvor lang tid det tok. 
        sequentialStopWatch.Stop();
        Console.WriteLine($"Sequential Operation took: {sequentialStopWatch.ElapsedMilliseconds} ms to complete");


        //Vi setter så opp en ny stopwatch og starter den
        var parallellStopWatch = Stopwatch.StartNew();

        //Vi mapper hvert endepunkt til en egen task, og bruker await whenall for å kjøre alle tasks parallellt (så lenge de kan alle legges i en egen thread i threadpoolen.)
        Console.WriteLine("Starting Parallell Fetching of urls...");
        List<Task> tasks = [.. endpoints.Select(endpoint => FetchFromEndpoint(endpoint, client))];
        await Task.WhenAll(tasks);

        //vi stopper stopwatch og rapporterer hvor lang tid det tok.
        parallellStopWatch.Stop();
        Console.WriteLine($"Parallell Operation took: {parallellStopWatch.ElapsedMilliseconds} ms to complete");

        Console.WriteLine($"The Parallell execution was {(double)sequentialStopWatch.ElapsedMilliseconds/parallellStopWatch.ElapsedMilliseconds:f2} times faster");
    }

    /// <summary>
    /// Dette er en hjelpemetode som gjør en Getrequest mot et endepunkt via en HttpClient
    /// Den skriver content som en string til consol, så lenge responsen er Ok()
    /// </summary>
    /// <param name="endpoint"></param>
    /// <param name="client"></param>
    /// <returns></returns>
    private async static Task FetchFromEndpoint(string endpoint, HttpClient client)
    {
        try 
        {
            var response = await client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Recieved this from {endpoint}: {content}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Something went wrong reading from {endpoint}: {e.Message}");
        }
    }

}