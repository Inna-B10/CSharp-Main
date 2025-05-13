using System.Net.Http.Json;

internal class Program
{

  private static async Task Main(string[] args)
  {
    var oauthClient = new Authentication.OAuthClient();
    var token = await oauthClient.GetAccessTokenAsync();

    var http = new HttpClient();
    http.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    var response = await http.GetAsync("https://www.googleapis.com/drive/v3/about?fields=*");
    var data = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>()
      ?? throw new Exception("Failed to parse response");

    foreach (var (key, value) in data)
    {
      Console.WriteLine($"{key}: {value}");
    }
  }
}
