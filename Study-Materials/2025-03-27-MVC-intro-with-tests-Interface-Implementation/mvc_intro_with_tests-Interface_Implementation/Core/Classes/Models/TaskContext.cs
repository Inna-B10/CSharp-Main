using Core.Interfaces;
namespace Core.Classes.Models;


public class TaskContext : ITaskContext
{
    public int Count => throw new NotImplementedException();

    public IUserTask AddTask(string title, string description, DateTime dueDate)
    {
        throw new NotImplementedException();
    }

    public bool CompleteTask(int id)
    {
        throw new NotImplementedException();
    }

    public bool DeleteTask(int id)
    {
        throw new NotImplementedException();
    }

    public List<IUserTask> GetAllTasks()
    {
        throw new NotImplementedException();
    }

    public List<IUserTask> GetCompleteTasks()
    {
        throw new NotImplementedException();
    }

    public List<IUserTask> GetPendingTasks()
    {
        throw new NotImplementedException();
    }

    public IUserTask? GetTaskById(int id)
    {
        throw new NotImplementedException();
    }
}
