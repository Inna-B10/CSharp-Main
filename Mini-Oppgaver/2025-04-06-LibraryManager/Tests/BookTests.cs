using Core.Classes.Models;
namespace Tests;

public class BookTests
{
    //* ----------------------------- Set_Properties ----------------------------- */
    [Fact(DisplayName = "Book_Constructor: Should set properties correctly")]
    public void Book_Constructor_Test()
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
}