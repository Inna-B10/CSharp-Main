using Core.Classes.Services;

namespace Tests;

public class BookServiceTests
{
  private readonly BookService _service;

  public BookServiceTests()
  {
    _service = new BookService();
    _service.AddBook("1984", "George Orwell");
    _service.AddBook("Brave New World", "Aldous Huxley");
    _service.AddBook("To Kill a Mockingbird", "Harper Lee");
  }

  [Fact]
  public void AddBook_Should_Add_Book_And_return_true()
  {
    //Arrange
    var title = "Pride and Prejudice";
    var author = "Jane Austen";

    //Act
    var result = _service.AddBook(title, author);

    //Assert
    Assert.True(result);
  }
}