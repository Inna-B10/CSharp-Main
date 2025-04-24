using UserService;

namespace Tests.UserService;

public class UserService
{
    [Fact]
    public void ReturnsRegisteredUser()
    {
        // Arrange
        var userAlias = "Alan";
        var userPassword = "password";
        var userService = new InMemoryUserService();

        // Act
        var result = userService.RegisterUser(userAlias, userPassword);

        // Assert
        Assert.IsType<Guid>(result.UserId);
    }

    [Fact]
    public void CanQueryUserById()
    {
        // Arrange
        var userAlias = "Alan";
        var userPassword = "password";
        var userService = new InMemoryUserService();

        // Act
        var user = userService.RegisterUser(userAlias, userPassword);
        var result = userService.GetUserById(user.UserId.ToString());

        // Assert
        Assert.NotNull(result);
    }
}
