using web_api_template_forts_controllers_efcore.Models;

namespace web_api_template_forts_controllers_efcore.Interfaces;


/* For å støtte Asynkron programmering, er nå interfacen vår endret til å levere et sett med Tasks, 
som skal queues opp på threadpoolet vårt. */
public interface ITaskContext
{
    int Count {get;}
    Task<List<UserTask>> GetAllTasks();
    Task<UserTask?> GetTaskById(int id);
    Task<List<UserTask>> GetPendingTasks();
    Task<List<UserTask>> GetCompleteTasks();
    Task<bool> CompleteTask(int id);
    Task<bool> DeleteTask(int id);
    Task<UserTask> AddTask(string title, string description, DateTime dueDate);
}