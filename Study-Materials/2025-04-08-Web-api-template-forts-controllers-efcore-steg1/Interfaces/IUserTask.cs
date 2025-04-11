namespace web_api_template_forts_controllers_efcore.Interfaces;

public interface IUserTask
{
    int Id {get;init;}
    string Title {get;set;}
    string Description {get;set;}
    bool IsCompleted {get;set;}
    DateTime DueDate {get;set;}
    void MarkAsCompleted();
}