namespace Authentication;

public interface IOAuthClient
{
  Task<string> GetTokenAsync();
}
