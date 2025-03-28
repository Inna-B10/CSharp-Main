using Core.Interfaces;
using Core.Classes.Models;
using Core.Classes.Views;

namespace Core.Classes.Controllers;


public class TaskController(TaskContext context, ViewGenerator view) : ITaskController
{
    private TaskContext _context = context;
    private ViewGenerator _view = view;
    public void Run()
    {
        var running = true;
        while (running)
        {
            _view.DisplayMainMenu();
                    var choice = _view.GetInput("","");

                    switch (choice)
                    {
                        case "1":
                            ViewAllTasks();
                            break;
                        case "2":
                            ViewPendingTasks();
                            break;
                        case "3":
                            ViewCompletedTasks();
                            break;
                        case "4":
                            AddTask();
                            break;
                        case "5":
                            ViewTaskDetail();
                            break;
                        case "6":
                            CompleteTask();
                            break;
                        case "7":
                            DeleteTask();
                            break;
                        case "0":
                            running = false;
                            break;
                        default:
                            _view.DisplayMessage("Invalid Choice. Please try again.", ConsoleColor.DarkRed);
                            _view.WaitForKey();
                            break;
                    }
        }
        _view.DisplayMessage("Thank you for using the Task Manager Software!", ConsoleColor.DarkBlue);
    }

    private void ViewAllTasks()
    {
        var tasks = _context.GetAllTasks();
        _view.DisplayTasks(tasks, HeaderOption.AllTasks);
        _view.WaitForKey();
    }

    private void ViewPendingTasks()
    {
        var tasks = _context.GetPendingTasks();
        _view.DisplayTasks(tasks, HeaderOption.PendingTasks);
        _view.WaitForKey();
    }

    private void ViewCompletedTasks()
    {
        var tasks = _context.GetCompleteTasks();
        _view.DisplayTasks(tasks, HeaderOption.CompleteTasks);
        _view.WaitForKey();
    }

    private void AddTask()
    {
        var title = _view.GetInput("Enter Task Title: ", "Title cannot be empty, please enter a valid title");
        var description = _view.GetInput("Enter a short description for the Task.", "This field cannot be empty, please try again.");
        var dueDateString = _view.GetInput("Enter the Due Date for the Task (MM/DD/YYYY).", "This field cannot be empty, please try again.");

        if (!DateTime.TryParse(dueDateString, out var dueDate))
        {
            dueDate = DateTime.Now.AddDays(7);
            _view.DisplayMessage($"Invalid Date Format. Setting Due Date to: {dueDate.ToShortDateString()}", ConsoleColor.DarkRed);
        }

        var task = _context.AddTask(title, description, dueDate);
        _view.DisplayMessage($"Task with Id {task.Id} successfully added.", ConsoleColor.DarkGreen);
        _view.WaitForKey();
    }

    private void ViewTaskDetail()
    {
        var idString = _view.GetInput("Enter a valid Task Id to get info about Task.", "Please Enter a valid numeric Id.");

        int id;
        while(!int.TryParse(idString, out id))
        {
            idString = _view.GetInput("Please enter a valid Task Id", "Please enter a valid numeric whole number.");
        }

        var task = _context.GetTaskById(id);

        _view.DisplayTaskDetails(task);
        _view.WaitForKey();
    }

    private void CompleteTask()
    {
        var idString = _view.GetInput("Enter a valid Task Id you want to complete.", "Please Enter a valid numeric Id.");

        int id;
        while(!int.TryParse(idString, out id))
        {
            idString = _view.GetInput("Please enter a valid Task Id", "Please enter a valid numeric whole number.");
        }

        var success = _context.CompleteTask(id);

        if (success)
        {
            _view.DisplayMessage($"Task {id} marked as Complete!", ConsoleColor.DarkGreen);
        }
        else 
        {
            _view.DisplayMessage($"Task with Id {id} could not be found.", ConsoleColor.DarkRed);
        }
        _view.WaitForKey();
    }

    private void DeleteTask()
    {
        var idString = _view.GetInput("Enter a valid Task Id for the task you want to delete.", "Please Enter a valid numeric Id.");

        int id;
        while(!int.TryParse(idString, out id))
        {
            idString = _view.GetInput("Please enter a valid Task Id", "Please enter a valid numeric whole number.");
        }

        var success = _context.DeleteTask(id);

        if (success)
        {
            _view.DisplayMessage($"Task {id} successfully deleted.", ConsoleColor.DarkGreen);
        }
        else 
        {
            _view.DisplayMessage($"Task with Id {id} Could not be found.", ConsoleColor.DarkRed);
        }
        _view.WaitForKey();
    }
}
