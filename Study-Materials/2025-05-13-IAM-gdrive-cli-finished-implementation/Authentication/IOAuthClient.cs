namespace Authentication;

public interface IOAuthClient
{
  Task<string> GetAccessTokenAsync();
}
