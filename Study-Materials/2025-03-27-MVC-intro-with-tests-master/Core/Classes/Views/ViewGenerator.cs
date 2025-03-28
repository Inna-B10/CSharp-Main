using Core.Interfaces;
using Core.Classes.Models;

namespace Core.Classes.Views;

public class ViewGenerator : IViewGenerator
{
    public void DisplayMainMenu()
    {
        Console.Clear();
        Console.WriteLine("""
        ========== Task Manager Main Menu ==========

                1. View All Tasks
                2. View Pending Tasks
                3. View Completed Tasks
                4. Add New Task
                5. View Task Detail
                6. Complete a Task
                7. Delete a Task
                0. Exit
        
        ============================================

        Enter your choice:
        """);
    }

    public void DisplayMessage(string message, ConsoleColor color)
    {
        Console.Clear();
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void DisplayTaskDetails(IUserTask? task)
    {
        Console.Clear();
        if (task is null)
        {
            Console.WriteLine("Task not found.");
            return;
        }
        Console.WriteLine($"""
        ========== Task Details: {task.Title} ==========

                Id: {task.Id}
                Description: {task.Description}
                Due Date: {task.DueDate.ToShortDateString()}
                Status: {(task.IsCompleted ? "Complete" : "Pending")}
        
        ================================================
        """);
    }

    public void DisplayTasks(List<IUserTask> tasks, HeaderOption header)
    {
        Console.Clear();
        Console.WriteLine(CreateHeader(header));
        if (tasks.Count == 0)
        {
            Console.WriteLine("No Tasks Found.");
            return;
        }
        Console.WriteLine("""
        |  ID  |  Title                           |  Due Date  |  Status  |
        -------------------------------------------------------------------
        """);
        foreach(var task in tasks)
        {
            var status = task.IsCompleted ? "Complete" : "Pending";

            Console.WriteLine($"|  {task.Id, -3}  |  {task.Title, -27}  |  {task.DueDate.ToShortDateString()}  |  {status}  |");

        }
        Console.WriteLine("-------------------------------------------------------------------");
    }

    public string GetInput(string prompt, string errorMessage)
    {
        Console.WriteLine(prompt);
        var input = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine(errorMessage);
            input = Console.ReadLine();
        }
        return input;
    }

    public void WaitForKey()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private static string CreateHeader(HeaderOption option)
    {
        var headerBase = option switch
        {
            HeaderOption.AllTasks => "All Tasks",
            HeaderOption.PendingTasks => "Pending Tasks",
            HeaderOption.CompleteTasks => "Complete Tasks",
            _ => throw new NotImplementedException()
        };
        return $"==================== {headerBase} ===================";
    }

}
