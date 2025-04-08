using web_api_template_forts_controllers_efcore.Interfaces;

namespace web_api_template_forts_controllers_efcore.Models;


public class UserTask(int id, string title, string description, DateTime dueDate): IUserTask
{
    public int Id {get; init;} = id;
    public string Title {get;set;} = title;

    public string Description {get;set;} = description;
    public bool IsCompleted {get;set;}
    public DateTime DueDate {get;set;} = dueDate;
    
    public void MarkAsCompleted()
    {
        IsCompleted = true;
    }
}