namespace Authentication;

public class GoogleOAuthClient : IOAuthClient
{
  string clientId = "33387960267-6hsk0n52bp3ijjqf1v53klde3spnbeeg.apps.googleusercontent.com";
  string clientSecret = "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";
  string authenticationUri = "https://accounts.google.com/o/oauth2/auth";
  string tokenExchangeUri = "https://oauth2.googleapis.com/token;

  string? tokens;

  public async Task<string> GetTokenAsync()
  {
    if (tokens != null)
    {
      return tokens;
    }
    else
    {
      tokens = await DoAuthenticationFlowAsync();
      return tokens;
    }
  }

  private async Task<string> DoAuthenticationFlowAsync()
  {
    // 1. Setup flow specific variables

    // 2. Guide users to authentication page

    // 3. Wait to we get response on callback endpoint

    // 4. Verifiy that it us who sent the request

    // 5. Authentication us, code for tokens exchange
  }

  private async Task<string> ExchangeCodeForTokensAsync(string requestBody)
  {
    // Construct Request

    // Return Tokens
  }
}
