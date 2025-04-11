using Microsoft.EntityFrameworkCore;
using web_api_template_forts_controllers_efcore.Interfaces;

namespace web_api_template_forts_controllers_efcore.Models;

public class TaskContext(DbContextOptions<TaskContext> options) : DbContext(options), ITaskContext
{

    /* Det som var en privat samling, er nå en public DbSet, som er en Ef-core unik datastruktur, som reflekterer hvordan samlingen er lagret i "set" */
    public DbSet<UserTask> Tasks {get;set;}
    public int Count => Tasks.Count();

    public async Task<UserTask> AddTask(string title, string description, DateTime dueDate)
    {
        /* Legg også merke til at _nextId er vekke, samt id constructoren i UserTask. Id håndteringen er nå flyttet til databasen i steden for.  */
        var newTask = new UserTask( title, description, dueDate);
        await Tasks.AddAsync(newTask);
        await SaveChangesAsync();
        return newTask;
    }

    public async Task<bool> CompleteTask(int id)
    {
        var task = await Tasks.FirstOrDefaultAsync(task => task.Id == id);
        if (task is null) return false;
        task.IsCompleted = true;
        await SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteTask(int id)
    {
        var task = await Tasks.FirstOrDefaultAsync(task => task.Id == id);
        if (task is null) return false;
        Tasks.Remove(task);
        await SaveChangesAsync();
        return true;
    }

    /* I de etterfølgende metodene skal vi jo bare "lese" tasks, vi skal ikke endre de på noen måte. Da kan vi hente ut Tasks.AsNoTracking(), det betyr at vi sier til EF core
    at denne hentingen av data, trenger ingen tracker overhead.  */
    public async Task<List<UserTask>> GetAllTasks()
    {
        return await Tasks.AsNoTracking().ToListAsync();
    }

    public async Task<List<UserTask>> GetCompleteTasks()
    {
        return await Tasks.AsNoTracking().Where(task => task.IsCompleted).ToListAsync();
    }

    public async Task<List<UserTask>> GetPendingTasks()
    {
        return await Tasks.AsNoTracking().Where(task => !task.IsCompleted).ToListAsync();
    }

    public async Task<UserTask?> GetTaskById(int id)
    {
        return await Tasks.AsNoTracking().FirstOrDefaultAsync(task => task.Id == id);
    }
}
