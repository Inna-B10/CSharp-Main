namespace web_api_template_forts_controllers_efcore.Interfaces;

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