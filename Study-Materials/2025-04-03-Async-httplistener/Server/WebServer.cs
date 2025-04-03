using System.Net;
using System.Text;

namespace async_httplistener.Server;



/// <summary>
/// En mer fullstendig webserver som kjører asyncront, hver request blir queuet opp mot det innebygde Threadpoolet .NET tilgjengeliggjør for oss. 
/// Denne webserveren vil prøve å levere ut en fil som lever i webroot mappen i prosjektet vårt.
/// Vi kan kjøre vår Start() method, og behandle innkommende requests så lenge vi ønsker at vår server skal kjøre.
/// </summary>
public class WebServer
{

    //Vi definerer på nytt en intern httpListener
    private HttpListener _listener;

    //samt en basefolder vår server kan lete etter resurser på.
    private string _baseFolder;

    /// <summary>
    /// Når vi lager en ny webserver, må vi mate den en uri vi kan mate vår listener, samt en basefolder hvor den kan lete etter resurser.
    /// </summary>
    /// <param name="uriPrefix"></param>
    /// <param name="baseFolder"></param>
    public WebServer(string uriPrefix, string baseFolder)
    {
        _listener = new();
        _listener.Prefixes.Add(uriPrefix);
        _baseFolder = baseFolder;
    }

    /// <summary>
    /// Vi setter så opp en privat hjelpemetode som skal prosessere en innkommende request.
    /// Vi vil at vår metode skal lete etter en spesifikk resurs tilgjengelig for vår webserver.
    /// </summary>
    /// <param name="context"></param>
    private async void ProcessRequestAsync(HttpListenerContext context)
    {
        try
        {
            //Vi så i forrige eksempel at vi kan hente ut alt bak uriPrefixed via RawUrl propertien på Request objektet.
            //Vi kan bruke dette for å lage begynnelsen for et filnavn
            var filename = Path.GetFileName(context.Request.RawUrl);
            //Vi kan kombinere dette med basefolderen vår, for å prøve å finne filstien til en fil i prosjektet vårt. 
            var filePath = Path.Combine(_baseFolder, filename!);

            //For å kunne skrive en respons, kan det på dette tidspunktet være greit å lage klar en variabel som kan holde denne responsmeldingen.
            byte[] responseMessage;

            //Hvis filen ikke eksisterer
            if (!File.Exists(filePath))
            {
                //Vi kan skrive til consollen vår at filen ikke er funnet.
                Console.WriteLine($"Resource not found: {filePath}");
                //Vi kan sette statuskoden på vår response til requesten, lik standard respons for fil ikke funnet.
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                //Vi kan også skrive en melding som følger med responsen.
                responseMessage = Encoding.UTF8.GetBytes("Sorry, that resource does not exist.");
            }

            //Hvis filen finnes
            else 
            {
                //Vi skriver til responsen at operasjonen er vellyket via OK
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                //Vi leser alle bytes i filen vi fant inn i responseMessage.
                responseMessage = await File.ReadAllBytesAsync(filePath);
            }

            //Vi definerer lengden på vår response via responseMessage.Length.
            //Siden responseMessage.Length er vår RequestBody for en successfull Get request.
            context.Response.ContentLength64 = responseMessage.Length;

            //Vi henter ut outputstreamen fra responsen
            using var outputStream = context.Response.OutputStream;

            //og skriver vår responseMessage til outputstreamen.
            await outputStream.WriteAsync(responseMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing request: {ex.Message}");
        }
    }

    //Dette er metoden som starter vår webserver.
    public async void Start()
    {
        //Først starter den lytteren.
        _listener.Start();

        //Så lager vi en evigkjørende loop.
        while (true)
        {
            try
            {
                //Den venter på en request
                var context = await _listener.GetContextAsync();
                //og starter opp en ny Task som behandler requesten.
                //Det kan hende her dere blir annbefalt å awaite denne operasjonen, men vi er ikke så interessert i resultatet herfra, og vil egentlig bare fortsette execution av while loopen, mens tasken foregår i bakgrunnen.
                Task.Run(()=>ProcessRequestAsync(context));
            }
            //hvis vi catcher en exception, som kan skje i vår httplistener, så breaker vi ut av loopen.
            catch (HttpListenerException){break;}
            catch (InvalidOperationException){break;}
        }
    }
    //En hjelpemetode som stanser lytteren vår.
    public void Stop() => _listener.Stop();
}