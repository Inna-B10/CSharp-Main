using System.Net;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Authentication;

public class GoogleOAuthClient : IOAuthClient
{
  string clientId = "33387960267-6hsk0n52bp3ijjqf1v53klde3spnbeeg.apps.googleusercontent.com";
  string clientSecret = "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";
  string authorizationEndpoint = "https://accounts.google.com/o/oauth2/auth";
  string tokenExchangeUri = "https://oauth2.googleapis.com/token";

  Dictionary<string, object>? tokens;

  public async Task<string> GetTokenAsync()
  {
    if (tokens != null)
    {
      var accessToken = tokens["access_token"].ToString()
        ?? throw new Exception("Token not found");
      return accessToken;
    }
    else
    {
      tokens = await DoAuthenticationFlowAsync();
      var accessToken = tokens["access_token"].ToString()
        ?? throw new Exception("Token not found");
      return accessToken;
    }
  }

  private async Task<Dictionary<string, object>> DoAuthenticationFlowAsync()
  {
    // 1. Setup flow specific variables
    var state = GenerateRandomDataBase64Encode();
    var codeVerifier = GenerateRandomDataBase64Encode();
    var codeChallenge = GenerateCodeChallenge(codeVerifier);
    var challengeMethod = "S256";
    var scopes = string.Join(" ", new[]{
      "https://www.googleapis.com/auth/drive",
    });

    // 1.1 Setup a local http listner for the response
    var redirectUri = "http://localhost:5000/callback/";
    var httpListner = new HttpListener();
    httpListner.Prefixes.Add(redirectUri);
    httpListner.Start();

    var authenticationUrl = $"{authorizationEndpoint}" +
                            $"?response_type=code" +
                            $"&client_id={Uri.EscapeDataString(clientId)}" +
                            $"&redirect_uri={Uri.EscapeDataString(redirectUri)}" +
                            $"&state={state}" +
                            $"&code_challenge={codeChallenge}" +
                            $"&code_challenge_method={challengeMethod}" +
                            $"&scope={Uri.EscapeDataString(scopes)}";

    // 2. Guide users to authentication page
    Console.WriteLine("Please visit:\n" + authenticationUrl);

    // 3. Wait to we get response on callback endpoint
    var context = await httpListner.GetContextAsync();
    var incommingState = context.Request.QueryString.Get("state");
    var code = context.Request.QueryString.Get("code");
    if (code == null)
    {
      throw new Exception("Malformed response");
    }

    // 4. Verifiy that it us who sent the request
    if (incommingState != state)
    {
      throw new Exception("Mismatching state in OAuth client");
    }

    // 5. Authentication us, code for tokens exchange
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

  private async Task<Dictionary<string, object>> ExchangeCodeForTokensAsync(Dictionary<string, string> payload)
  {
    // Construct Request
    var httpClient = new HttpClient();
    var requestBody = new FormUrlEncodedContent(payload);
    var response = await httpClient.PostAsync(tokenExchangeUri, requestBody);
    var data = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
    if (data == null)
    {
      throw new Exception("Failed parsing token response");
    }
    // Return Tokens
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
}
