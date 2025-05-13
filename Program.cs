using System.Net.Http.Json;
using Authentication;

IOAuthClient oAuth = new GoogleOAuthClient();
var token = await oAuth.GetTokenAsync();

var http = new HttpClient();
http.DefaultRequestHeaders.Add("Authentication", $"Bearer: {token}");

var url = "https://www.googleapis.com/drive/v3/files"
var data = await http.GetFromJsonAsync<Dictionary<string, object>>(url)
  ?? throw new Exception("Failed parsing json");

foreach (var (key, value) in data)
{
  Console.WriteLine($"{key}: {value}");
}
