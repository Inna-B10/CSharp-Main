using async_httplistener.Server;


//Vi setter opp en egen httpclient
var client = new HttpClient();

//Vi bruker vår server som en scoped variabel
using var server = new SimpleHttpServer();

//Vi gjør en request til endepunktet som vår server skal lytte etter.
var response = await client.GetStringAsync("http://localhost:9001/MyFirstServer/hello-world.txt");

//Vi skriver ut responsen vi får tilbake.
Console.WriteLine(response);



//vi kan også starte vår mer robuste og fullstendige webserver her.
var webServer = new WebServer("http://localhost:9002/", "./webroot");

//vi bruker try{}finally{} for å starte webserveren vår, så passe på den blir stoppet korrekt når brukeren er ferdig.
try
{
    webServer.Start();

    //Vi lager en liten Console.ReadLine her som pauser mainline execution mens webserveren kjører asynkront i bakgrunnen.
    Console.WriteLine("Server running, press any key to exit...");
    Console.ReadLine();
}
finally
{
    //Hvis en knapp blir trykt på, kjører finally clausen og vi stopper webserveren vår. 
    webServer.Stop();
}