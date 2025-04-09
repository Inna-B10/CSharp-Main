using Core.Classes;
using Core.Classes.Models;
using Core.Interfaces;

namespace Tests;

public class BookTests
{
    [Fact]
    public void Book_Constructor_Should_Set_Properties_Correctly()
    {
        // Arrange
        int id = 1;
        string title = "1984";
        string author = "George Orwell";

        // Act
        var book = new Book(id, title, author);

        // Assert
        Assert.Equal(id, book.Id);
        Assert.Equal(title, book.Title);
        Assert.Equal(author, book.Author);
        Assert.False(book.IsBorrowed);
        Assert.Null(book.DueDate);
    }

    [Fact(DisplayName = "Должен установить флаг IsBorrowed в true")]
    public void MarkAsBorrowed_Should_Set_IsBorrowed_True_And_Set_DueDate()
    {
        // Arrange
        var book = new Book(2, "Brave New World", "Aldous Huxley");
        Assert.False(book.IsBorrowed);
        // var dueDate = new DateTime(2025, 4, 15);
        var dueDate = DateTime.Now.AddDays(14);

        // Act
        book.MarkAsBorrowed(dueDate);

        // Assert
        Assert.True(book.IsBorrowed);
        Assert.Equal(dueDate, book.DueDate);

    }
    [Fact]
    public void MarkAsReturned_Should_Set_IsBorrowed_False_And_Clear_DueDate()
    {
        //Arrange
        var book = new Book(3, "Fahrenheit 451", "Ray Bradbury");
        var dueDate = DateTime.Now.AddDays(14);
        Assert.False(book.IsBorrowed);
        book.MarkAsBorrowed(dueDate);

        //Act
        book.MarkAsReturned();

        //Assert
        Assert.False(book.IsBorrowed);
        Assert.Null(book.DueDate);
    }
}