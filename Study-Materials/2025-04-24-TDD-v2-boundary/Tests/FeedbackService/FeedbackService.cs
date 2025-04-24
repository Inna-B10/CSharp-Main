using FeedbackService;

namespace Tests.FeedbackService;

public class FeedbackServiceTests
{
    [Fact]
    public void LeavingAFeedbackShouldReturnANewFeedbackObject()
    {
        // Arrange
        var service = new InMemoryFeedbackService();
        string userId = "1";
        string message = "Great service!";

        // Act
        var result = service.PostFeedack(userId, message);

        // Assert
        Assert.Equal(userId, result.UserId);
        Assert.Equal(message, result.Message);
        Assert.IsType<DateTime>(result.CreatedAt);
    }

    // [Fact(Skip = "true")]
    // public void ThrowsIfNoUserId()
    // {
    //     // Arrange
    //     var userService = new UserService();
    //     var service = new InMemoryFeedbackService(userService);
    //     string userId = "";
    //     string message = "Hello World";

    //     // Act
    //     Assert.Throws<ArgumentException>(() => service.PostFeedack(userId, message));
    // }
}
