using System.Formats.Asn1;
using System.Net;
using System.Text;

namespace async_httplistener.Server;

/// <summary>
/// En simpel versjon av en ekstremt enkel webserver.
/// </summary>
public class SimpleHttpServer : IDisposable
{
    /// <summary>
    /// Vi definerer en egen privat httplistener, som skal lytte etter spesifikke http requests.
    /// </summary>
    private readonly HttpListener _listener = new ();

    /// <summary>
    /// I steden for å constructe selve objektet simpleHttpserver, kan vi bare be den kjøre metoden ListenAsync når et sted ber om en ny SimpleHttpServer
    /// </summary>
    public SimpleHttpServer() => ListenAsync();

    /// <summary>
    /// Metoden vår ListenAsync er metoden som faktisk ligger passivt å lytter etter en request.
    /// </summary>
    private async void ListenAsync()
    {
        //her legger vi til hvilken URI vår server skal respondere etter, evt hvilken "socket" den skal lytte etter connections på.
        _listener.Prefixes.Add("http://localhost:9001/");

        //Her sier vi at listeneren vår skal starte å lytte.
        _listener.Start();

        //Her venter vi til vi får en request, og henter ut contexten.
        var context = await _listener.GetContextAsync();
        
        //som med httpclient, kan vi her og lese informasjon om request
        Console.WriteLine(context.Request.HttpMethod);
        Console.WriteLine(context.Request.RawUrl);
        string responseMessage = $"You asked for the following resource: {context.Request.RawUrl}";

        //Samt vi kan skrive en response.
        context.Response.ContentLength64 = Encoding.UTF8.GetByteCount(responseMessage);
        context.Response.StatusCode = (int)HttpStatusCode.OK;

        //Vi kan så skrive responsen til contexten sin output stream
        using var outputStream = context.Response.OutputStream;
        using var streamWriter = new StreamWriter(outputStream);
        await streamWriter.WriteAsync(responseMessage);
    }
    //for å kunne dispose httplisteneren trygt, må vi ha en egen dispose method, som passer på å stenge listeneren når den er brukt.
    public void Dispose() => _listener.Close();
}
