namespace FeedbackService;

public class InMemoryFeedbackService : IInMemoryFeedbackService
{
  public FeedbackModel PostFeedack(string userId, string message)
  {


    return new FeedbackModel { Message = message, UserId = userId };
  }
}
