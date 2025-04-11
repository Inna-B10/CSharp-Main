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
        string section = "Fiction";
        string shelf = "F5";

        // Act
        var book = new Book(id, title, author, section, shelf);

        // Assert
        Assert.Equal(id, book.Id);
        Assert.Equal(title, book.Title);
        Assert.Equal(author, book.Author);
        Assert.False(book.IsBorrowed);
        Assert.Null(book.DueDate);
        Assert.Equal(section, book.Section);
        Assert.Equal(shelf, book.Shelf);
    }
}