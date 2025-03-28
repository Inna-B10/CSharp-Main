using Core.Interfaces;
namespace Core.Classes.Models;

public class UserTask : IUserTask
{
    public int Id { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
    public string Title { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool IsCompleted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public DateTime DueDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void MarkAsCompleted()
    {
        throw new NotImplementedException();
    }
}
