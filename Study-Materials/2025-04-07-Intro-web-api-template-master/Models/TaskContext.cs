using Intro_web_api_template.Interfaces;

namespace Intro_web_api_template.Models;

public class TaskContext : ITaskContext
{
    private List<IUserTask> _tasks = [];
    private int _nextId;
    public int Count => _tasks.Count;

    public IUserTask AddTask(string title, string description, DateTime dueDate)
    {
        var newTask = new UserTask(++_nextId, title, description,dueDate);
        _tasks.Add(newTask);
        return newTask;
    }

    public bool CompleteTask(int id)
    {
        var task = _tasks.FirstOrDefault(task => task.Id == id);
        if (task is null) return false;
        task.MarkAsCompleted();
        return true;
    }

    public bool DeleteTask(int id)
    {
        var task = _tasks.FirstOrDefault(task => task.Id == id);
        if (task is null) return false;
        return _tasks.Remove(task);
    }

    public List<IUserTask> GetAllTasks()
    {
        return _tasks;
    }

    public List<IUserTask> GetCompleteTasks()
    {
        return [.._tasks.Where(task => task.IsCompleted)];
    }

    public List<IUserTask> GetPendingTasks()
    {
        return [.._tasks.Where(task => !task.IsCompleted)];
    }

    public IUserTask? GetTaskById(int id)
    {
        return _tasks.FirstOrDefault(task => task.Id == id);
    }
}
