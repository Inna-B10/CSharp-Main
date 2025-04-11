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

  //* ------------------------------ Test AddBook ------------------------------ */
  [Fact(DisplayName = "AddBook: Should return TRUE if book added")]
  public void AddBook_Test()
  {
    //Arrange
    var title = "Pride and Prejudice";
    var author = "Jane Austen";

    //Act
    var result = _service.AddBook(title, author);

    //Assert
    Assert.True(result);
  }

  //* --------------------- Test GetBookById With Valid Id --------------------- */
  [Fact(DisplayName = "GetBookById: Should return correct book details")]
  public void GetBookById_Test_With_ValidId()
  {
    //Arrange
    int id = 1;

    //Act
    var book = _service.GetBookById(id);

    //Assert
    Assert.NotNull(book);
    Assert.Equal(id, book.Id);
  }

  //* -------------------- Test GetBookById With Invalid Id -------------------- */
  [Fact(DisplayName = "GetBookById: Should return Null if book not found")]
  public void GetBookById_Test_With_InvalidId()
  {
    //Arrange
    int id = 100;

    //Act
    var book = _service.GetBookById(id);

    //Assert
    Assert.Null(book);
  }

  /* -------------------------- Test GetBorrowedBooks ------------------------- */
  //   [Fact(DisplayName = "Should return list only of borrowed books")]
  //   public void GetBorrowedBooks_Test()
  //   {
  // //Arrange
  // int id = 1;
  // _service.BorrowBook(id);
  // 
  // //Act
  // var borrowedBooks= _service.GetBorrowedBooks();
  // 
  // //Assert
  // Assert.All(borrowedBooks, book=>Assert.True(book.IsBorrowed));
  //   }

  //* ---------------------- Test BorrowBook With Valid Id --------------------- */
  [Fact(DisplayName = "BorrowBook: Should return new dueDate")]
  public void BorrowBook_Test_With_ValidId()
  {
    //Arrange
    int id = 1;
    var expectedDueDate = DateTime.Today.AddDays(14);

    //Act
    var actualDueDate = _service.BorrowBook(id);

    //Assert
    Assert.Equal(expectedDueDate, actualDueDate);
  }

  //* --------------------- Test BorrowBook With Invalid Id -------------------- */
  [Fact(DisplayName = "BorrowBook: Should return NULL if book not found")]
  public void BorrowBook_Test_With_InvalidId()
  {
    //Arrange
    int invalidId = 99;

    //Act
    var result = _service.BorrowBook(invalidId);

    //Assert
    Assert.Null(result);
  }

  //* ------------------ Test BorrowBook When Already Borrowed ----------------- */
  [Fact(DisplayName = "BorrowBook: Should return NULL if book already borrowed")]
  public void BorrowBook_Test_When_Already_Borrowed()
  {
    //Arrange
    int id = 2;
    var book = _service.GetBookById(id);
    book!.IsBorrowed = true;

    //Act
    var result = _service.BorrowBook(id);

    //Assert
    Assert.Null(result);
  }

  //* --------------------------- Test MarkAsBorrowed -------------------------- */
  [Fact(DisplayName = "MarkAsBorrowed: Should set IsBorrowed TRUE, set and return new dueDate ")]
  public void MarkAsBorrowed_Test()
  {
    // Arrange
    int id = 2;
    var book = _service.GetBookById(id);
    book!.IsBorrowed = false;
    var dueDate = DateTime.Today.AddDays(14);

    // Act
    _service.MarkAsBorrowed(book);

    // Assert
    Assert.True(book.IsBorrowed);
    Assert.Equal(dueDate, book.DueDate);
  }

  //* ---------------------- Test ReturnBook With Valid Id --------------------- */
  [Fact(DisplayName = "ReturnBook: Should return TRUE")]
  public void ReturnBook_Test_With_ValidId()
  {
    //Arrange
    int id = 2;
    var book = _service.GetBookById(id);
    book!.IsBorrowed = true;

    //Act
    var result = _service.ReturnBook(id);

    //Assert
    Assert.True(result);
  }

  //* --------------------- Test ReturnBook With Invalid Id -------------------- */
  [Fact(DisplayName = "ReturnBook: Should return FALSE if book not found")]
  public void ReturnBook_Test_With_InvalidId()
  {
    //Arrange
    int id = 99;

    //Act
    var result = _service.ReturnBook(id);

    //Assert
    Assert.False(result);
  }

  //* ----------------- Test ReturnBook When Book Not Borrowed ----------------- */
  [Fact(DisplayName = "ReturnBook: Should return FALSE if book not borrowed")]
  public void ReturnBook_Test_When_Book_Avlabile()
  {
    //Arrange
    int id = 2;
    var book = _service.GetBookById(id);
    book!.IsBorrowed = false;

    //Act
    var result = _service.ReturnBook(id);

    //Assert
    Assert.False(result);
  }

  //* --------------------------- Test MarkAsReturned -------------------------- */
  [Fact(DisplayName = "Should set IsBorrowed to FALSE and dueDate to NULL")]
  public void MarkAsReturned_Test()
  {
    //Arrange
    int id = 3;
    var book = _service.GetBookById(id);
    book!.IsBorrowed = true;

    //Act
    _service.MarkAsReturned(book);

    //Assert
    Assert.False(book.IsBorrowed);
    Assert.Null(book.DueDate);
  }
}