namespace UserService;

public class InMemoryUserService : IUserService
{
  readonly Dictionary<string, UserModel> _users = [];

  public UserModel? GetUserById(string userId)
  {
    _users.TryGetValue(userId, out UserModel? user);
    return user ?? null;
  }

  public UserModel RegisterUser(string userAlias, string userPassword)
  {
    var newUser = new UserModel();
    _users.Add(newUser.UserId.ToString(), newUser);
    return newUser;
  }
}
