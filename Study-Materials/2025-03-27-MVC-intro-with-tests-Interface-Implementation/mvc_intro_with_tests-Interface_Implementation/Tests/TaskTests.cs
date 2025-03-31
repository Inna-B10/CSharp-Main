using Core.Classes.Models;
using Core.Interfaces;

namespace Tests;

public class TaskTests
{

    /// <summary>
    /// Når vi lager enkle Facts kan det være lurt å strukturere de via følgende
    /// hjelpeord:
    /// 
    /// Arrange:
    /// Definer konstanter og verdier testen din trenger for å teste 
    /// det testen skal teste. 
    /// I dette tilfellet skal vi teste at når vår constructor lager en Task, 
    /// skal verdierene til propertiene være lik de vi starter med.
    /// 
    /// Act:
    /// Her skal vi gjennomføre det vi skal teste.
    /// 
    /// Assert:
    /// Her skal vi Asserte fakta basert på testen vår. 
    /// </summary>
    [Fact]
    public void TaskConstructorSetsProperties()
    {
        //Arrange
        int id = 1;
        var title = "Test Task";
        var description = "Test Description";
        var dueDate = DateTime.Now.AddDays(1);

        //Act
        var task = new UserTask(id, title, description, dueDate);


        //Assert
        Assert.Equal(id, task.Id);
        Assert.Equal(title, task.Title);
        Assert.Equal(description, task.Description);
        Assert.Equal(dueDate, task.DueDate);
        Assert.False(task.IsCompleted); //IsCompleted skal defaulte til false.
    }

    /// <summary>
    /// Siden testen ovenfor garanterer at vår constructor fungerer,
    /// kan vi nå trygt bruke vår constructor i Arrange steget.
    /// </summary>
    [Fact]
    public void TaskMarkCompleteSetsCompleteToTrue()
    {
        //Arrange
        var task = new UserTask(1, "TestTask", "Description", DateTime.Now.AddDays(4));
        Assert.False(task.IsCompleted);

        //Act
        task.MarkAsCompleted();

        //Assert
        Assert.True(task.IsCompleted);

    }
}