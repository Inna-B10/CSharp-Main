public interface IUserService
{
  UserModel RegisterUser(string userAlias, string userPassword);
  UserModel? GetUserById(string userId);
}
