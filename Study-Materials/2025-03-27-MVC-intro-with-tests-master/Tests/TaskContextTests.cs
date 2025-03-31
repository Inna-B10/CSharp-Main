using Core.Classes;
using Core.Interfaces;

namespace Tests;

public class TaskContextTests
{
    //En felles Arrange av en context vi kan bruke i alle testene våre.
    private readonly TaskContext _context;

    public TaskContextTests()
    {
        _context = new TaskContext();

        _context.AddTask("Test task", "Test Description", DateTime.Now.AddDays(1));
        _context.AddTask("Test task", "Test Description", DateTime.Now.AddDays(2));
        _context.AddTask("Test task", "Test Description", DateTime.Now.AddDays(3));
        _context.AddTask("Test task", "Test Description", DateTime.Now.AddDays(4));
    }

    [Fact]
    public void TaskContextConstructorInitialValues()
    {
        //Assert
        Assert.Equal(4, _context.Count);
        Assert.Equal(4, _context.GetAllTasks().Count);
    }

    [Fact]
    public void TaskContextTestAddUserTask()
    {
        //Arrange
        int initialCount = _context.Count;
        var title = "New Test Task";
        var description = "Test Description";
        var dueDate = DateTime.Now.AddDays(4);

        //Act
        var addedTask = _context.AddTask(title, description, dueDate);

        //Assert
        Assert.NotNull(addedTask);
        Assert.Equal(title, addedTask.Title);
        Assert.Equal(description, addedTask.Description);
        Assert.Equal(dueDate, addedTask.DueDate);
        Assert.Equal(initialCount+1, _context.Count);
    }

    [Fact]
    public void GetTaskWithValidIdReturnCorrectTask()
    {
        //Arrange
        int id = 1;
        

        //Act
        var task = _context.GetTaskById(id);

        //Assert
        Assert.NotNull(task);
        Assert.Equal(id, task.Id);
    }

    [Fact]
    public void GetTaskWithInvalidId()
    {
        //Arrange
        int invalidId = 9999;

        //Act
        var task = _context.GetTaskById(invalidId);

        //Assert
        Assert.Null(task);
    }

    [Fact]
    public void CompleteTaskWithValidId()
    {
        //Arrange
        var task = _context.GetTaskById(1);
        Assert.False(task!.IsCompleted);

        //Act
        var result = _context.CompleteTask(1);

        //Assert
        Assert.True(result);
        Assert.True(task.IsCompleted);
    }

    [Fact]
    public void CompleteTaskWithInvalidId()
    {
        //Arrange
        int invalidId = 9999;

        //Act
        var result = _context.CompleteTask(invalidId);

        //Assert
        Assert.False(result);
    }

    [Fact]
    public void DeleteTaskByValidId()
    {
        //Arrange
        var taskId = 1;
        var count = _context.Count;

        //Act
        var result = _context.DeleteTask(taskId);

        //Assert
        Assert.True(result);
        Assert.Equal(count -1, _context.Count);
        Assert.Null(_context.GetTaskById(taskId));
    }

    [Fact]
    public void DeleteTaskByInvalidId()
    {
        //Arrange
        var invalidId = 9999;
        var count = _context.Count;

        //Act
        var result = _context.DeleteTask(invalidId);

        //Assert
        Assert.False(result);
        Assert.Equal(count, _context.Count);
    }

    [Fact]
    public void GetCompletedTasksReturnsOnlyCompletedTasks()
    {
        //Arrange
        _context.CompleteTask(1);


        //Act
        var completedTask = _context.GetCompleteTasks();


        //Assert
        Assert.True(completedTask.Count > 0);
        Assert.All(completedTask, task => Assert.True(task.IsCompleted));
    }

    [Fact]
    public void GetPendingTasksReturnsOnlyPendingTasks()
    {
        //Arrange
        _context.CompleteTask(1);
        
        //Act
        var pendingTasks = _context.GetPendingTasks();

        //Assert
        Assert.All(pendingTasks, t => Assert.False(t.IsCompleted));
    }
}