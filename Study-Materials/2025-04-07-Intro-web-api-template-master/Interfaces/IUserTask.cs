namespace Intro_web_api_template.Interfaces;

public interface IUserTask
{
    int Id {get; init;}
    string Title {get;set;}
    string Description {get;set;}
    bool IsCompleted {get;set;}
    DateTime DueDate {get;set;}
    void MarkAsCompleted();
}