namespace FeedbackService;

public interface IInMemoryFeedbackService
{
  FeedbackModel PostFeedack(string userId, string message);
}
