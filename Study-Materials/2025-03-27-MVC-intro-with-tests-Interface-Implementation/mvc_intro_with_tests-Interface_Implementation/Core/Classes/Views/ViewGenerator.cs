using Core.Interfaces;
using Core.Classes.Models;

namespace Core.Classes.Views;

public class ViewGenerator : IViewGenerator
{
    public void DisplayMainMenu()
    {
        throw new NotImplementedException();
    }

    public void DisplayMessage(string message, ConsoleColor color)
    {
        throw new NotImplementedException();
    }

    public void DisplayTaskDetails(IUserTask? task)
    {
        throw new NotImplementedException();
    }

    public void DisplayTasks(List<IUserTask> tasks, HeaderOption header)
    {
        throw new NotImplementedException();
    }

    public string GetInput(string prompt, string errorMessage)
    {
        throw new NotImplementedException();
    }

    public void WaitForKey()
    {
        throw new NotImplementedException();
    }
}
