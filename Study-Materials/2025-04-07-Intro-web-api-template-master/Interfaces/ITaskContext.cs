namespace Intro_web_api_template.Interfaces;

public interface ITaskContext
{
    int Count {get;}
    List<IUserTask> GetAllTasks();
    IUserTask? GetTaskById(int id);
    List<IUserTask> GetPendingTasks();

    List<IUserTask> GetCompleteTasks();

    bool CompleteTask(int id);
    bool DeleteTask(int id);
    IUserTask AddTask(string title, string description, DateTime dueDate);
}