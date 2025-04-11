using System.ComponentModel.DataAnnotations;
using web_api_template_forts_controllers_efcore.Interfaces;

namespace web_api_template_forts_controllers_efcore.Models;


public class UserTask(string title, string description, DateTime dueDate): IUserTask
{
    /* Her kan vi markere Id som en Primary Key i databasen vår. */
    [Key]
    public int Id {get; init;}
    public string Title {get;set;} = title;

    public string Description {get;set;} = description;
    public bool IsCompleted {get;set;}
    public DateTime DueDate {get;set;} = dueDate;
}