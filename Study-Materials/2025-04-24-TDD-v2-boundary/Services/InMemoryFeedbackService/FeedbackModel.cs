public class FeedbackModel
{
  public required string UserId { get; init; }
  public required string Message { get; init; }
  public DateTime CreatedAt { get; } = DateTime.Now;
}
