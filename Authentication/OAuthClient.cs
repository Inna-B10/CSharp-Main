
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Authentication;

public class TokenResponse
{
  [JsonPropertyName("access_token")]
  public required string AccessToken { get; init; }

  [JsonPropertyName("refresh_token")]
  public required string RefreshToken { get; init; }

  [JsonPropertyName("token_type")]
  public required string TokenType { get; init; }

  [JsonPropertyName("expires_in")]
  public required int ExpiresIn { get; init; }
}

public class OAuthClient : IOAuthClient
{
  private readonly string clientId = "33387960267-oaugs2akpvj4731842j0gsi5tcrsajtv.apps.googleusercontent.com";
  private readonly string clientSecret = "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";
  private readonly string authorizationEndpoint = "https://accounts.google.com/o/oauth2/auth";
  private readonly string tokenEndpoint = "https://oauth2.googleapis.com/token";
  private readonly string scopes = string.Join(" ", new[] {
    "https://www.googleapis.com/auth/drive"
  });

  private TokenResponse? tokens;

  public async Task<string> GetAccessTokenAsync()
  {
    if (tokens != null)
    {
      return tokens.AccessToken;
    }

    tokens = await DoAuthenticationFlow();

    return tokens.AccessToken;
  }

  public async Task<TokenResponse> DoAuthenticationFlow()
  {
    // Setup PKCE challenge
    var state = GenerateRandomDataBase64Encode();
    var codeVerifier = GenerateRandomDataBase64Encode();
    var challenge = GenerateCodeChallenge(codeVerifier);
    var challengeMethod = "S256";

    // 1. Setup redirect endpoint
    string redirectUri = $"http://{IPAddress.Loopback}:{GetRandomUnusedPort()}/";
    var httpListner = new HttpListener();
    httpListner.Prefixes.Add(redirectUri);
    httpListner.Start();

    // 3. Create OAuth authorization request
    var authenticationUrl = $"{authorizationEndpoint}" +
                            $"?response_type=code" +
                            $"&client_id={Uri.EscapeDataString(clientId)}" +
                            $"&redirect_uri={Uri.EscapeDataString(redirectUri)}" +
                            $"&state={state}" +
                            $"&code_challenge={challenge}" +
                            $"&code_challenge_method={challengeMethod}" +
                            $"&scope={Uri.EscapeDataString(scopes)}";

    // 4. Guide User to complete request in browser
    OpenInBrowser(authenticationUrl);

    // 5. Await OAuth response
    var (code, incomingState) = await BrowserResponse(httpListner);

    // 6. Ensure it was us who made the request (PKCE)
    if (incomingState != state)
    {
      throw new Exception("Received request with invalid state");
    }

    // 7. Exchange code for access tokens
    var requestBody = new Dictionary<string, string> {
      {"code", code},
      {"code_verifier", codeVerifier},
      {"client_id", clientId},
      {"client_secret", clientSecret},
      {"redirect_uri", redirectUri},
      {"grant_type", "authorization_code"},
    };

    return await ExchangeCodeForTokensAsync(requestBody);
  }

  private async Task<TokenResponse> ExchangeCodeForTokensAsync(Dictionary<string, string> requestBody)
  {
    var http = new HttpClient();
    // Construct request
    var requestContent = new FormUrlEncodedContent(requestBody);
    var response = await http.PostAsync(tokenEndpoint, requestContent);

    string responseBody = await response.Content.ReadAsStringAsync();

    var data = JsonSerializer.Deserialize<TokenResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    if (data == null)
    {
      throw new Exception("Failed parsing response from Code exchange!");
    }

    return data;
  }

  private static string GenerateRandomDataBase64Encode()
  {
    var randomBytes = RandomNumberGenerator.GetBytes(32);
    return Convert
      .ToBase64String(randomBytes)
      .Replace('+', '-')
      .Replace('/', '_');
  }

  private static string GenerateCodeChallenge(string codeVerifier)
  {
    using var sha256 = SHA256.Create();
    var bytes = sha256.ComputeHash(Encoding.ASCII.GetBytes(codeVerifier));
    return Convert
      .ToBase64String(bytes)
      .TrimEnd('=')
      .Replace('+', '-')
      .Replace('/', '_');
  }

  private static void OpenInBrowser(string url)
  {
    try
    {
      Process.Start("open", url);
    }
    catch
    {
      Console.WriteLine("Open the followign URL in a browser:");
      Console.WriteLine(url);
    }
  }

  private static int GetRandomUnusedPort()
  {
    var listener = new TcpListener(IPAddress.Loopback, 0);
    listener.Start();
    var port = ((IPEndPoint)listener.LocalEndpoint).Port;
    listener.Stop();
    return port;
  }

  private static async Task<(string code, string state)> BrowserResponse(HttpListener httpListner)
  {
    var context = await httpListner.GetContextAsync();
    var response = context.Response;
    string responseString = "<html><head><meta http-equiv='refresh' content='10;url=https://google.com'></head><body>Please return to the app.</body></html>";
    byte[] buffer = Encoding.UTF8.GetBytes(responseString);
    response.ContentLength64 = buffer.Length;
    var responseOutput = response.OutputStream;
    await responseOutput.WriteAsync(buffer, 0, buffer.Length);
    responseOutput.Close();
    httpListner.Stop();

    var request = context.Request;
    var code = request.QueryString.Get("code");
    var incomingState = request.QueryString.Get("state");

    if (code == null || incomingState == null)
    {
      throw new Exception($"Malformed authorization response. {request.QueryString}");
    }

    return (code, incomingState);
  }
}