using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Velkommen til værapplikasjonen!");
            Console.Write("Skriv inn byen du vil sjekke været for: ");
            string? city = Console.ReadLine();

            try
            {
                string apiKey = "ba8c368b4da97e6fffb1475142af5421"; 
                string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Håndter responsen og vis værinformasjonen
                    Console.WriteLine("\nVærinformasjon for " + city + ":");
                    Console.WriteLine("-------------------------------------");
                    Console.WriteLine(responseBody);
                    Console.ReadKey();
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Feil ved henting av værinformasjon: {e.Message}");
                Console.ReadKey();
            }
        }
    }
}
