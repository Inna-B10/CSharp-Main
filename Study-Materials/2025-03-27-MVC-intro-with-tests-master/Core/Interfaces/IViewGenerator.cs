using Core.Classes.Models;

namespace Core.Interfaces;

public interface IViewGenerator
{
    void DisplayMainMenu();

    void DisplayTasks(List<IUserTask> tasks, HeaderOption header);

    void DisplayTaskDetails(IUserTask? task);

    string GetInput(string prompt, string errorMessage);

    void DisplayMessage(string message, ConsoleColor color);

    void WaitForKey();
}