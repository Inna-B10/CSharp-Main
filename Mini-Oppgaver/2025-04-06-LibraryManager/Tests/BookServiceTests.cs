using Core.Classes.Services;

namespace Tests;

public class BookServiceTests
{
  private readonly BookService _service;

  public BookServiceTests()
  {
    _service = new BookService();
    _service.AddBook("1984", "George Orwell", "Fiction", "F5");
    _service.AddBook("Brave New World", "Aldous Huxley", "Fiction", "F1");
    _service.AddBook("To Kill a Mockingbird", "Harper Lee", "American Literature", "A9");
  }
  //* ---------------------- Test BookService Constructor ---------------------- */
  [Fact(DisplayName = "Constructor: Should set initial data and sync Count with collection")]
  public void BookService_Test_Constructor_Initial_Values()
  {
    //Assert
    Assert.Equal(3, _service.Count);
    Assert.Equal(3, _service.GetAllBooks().Count);
    Assert.Equal(_service.Count, _service.GetAllBooks().Count);
  }

  //* ------------------------------ Test AddBook ------------------------------ */
  [Fact(DisplayName = "AddBook: Should return TRUE if book added")]
  public void AddBook_Test()
  {
    //Arrange
    int count = _service.Count;
    var title = "Pride and Prejudice";
    var author = "Jane Austen";
    var section = "Classic Literature";
    var shelf = "C2";

    //Act
    var result = _service.AddBook(title, author, section, shelf);

    //Assert
    Assert.True(result);
    Assert.Equal(_service.Count, count + 1);
  }

  //* --------------------- Test GetBookById With Valid Id --------------------- */
  [Fact(DisplayName = "GetBookById with valid ID: Should return correct book details")]
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
  [Fact(DisplayName = "GetBookById with invalid ID: Should return Null")]
  public void GetBookById_Test_With_InvalidId()
  {
    //Arrange
    int id = 100;

    //Act
    var book = _service.GetBookById(id);

    //Assert
    Assert.Null(book);
  }

  //* ------------------------ Test GetAllBorrowedBooks ------------------------ */
  [Fact(DisplayName = "GetAllBorrowedBooks: Should return list only of borrowed books with all dates")]
  public void GetAllBorrowedBooks_Test_All_Dates()
  {
    //Arrange
    int id = 1;
    _service.BorrowBook(id);

    //Act
    var borrowedBooks = _service.GetAllBorrowedBooks();

    //Assert
    Assert.All(borrowedBooks, book => Assert.True(book.IsBorrowed));
  }

  //* --------------------- Test GetBooksWithExpiredDueDate -------------------- */
  [Fact(DisplayName = "GetBooksWithExpiredDueDate: Should return list only of borrowed books with expired dueDate")]
  public void GetBooksWithExpiredDueDate_Test_With_Expired_DueDate()
  {
    //Arrange
    int id = 1;
    _service.BorrowBook(id);
    var book = _service.GetBookById(id);
    book!.DueDate = DateTime.Today.AddDays(-2);

    //Act
    var expiredBooks = _service.GetBooksWithExpiredDueDate();

    //Assert
    Assert.All(expiredBooks, book => Assert.True(book.IsBorrowed && book.DueDate < DateTime.Today));
  }

  //* ---------------------- Test BorrowBook With Valid Id --------------------- */
  [Fact(DisplayName = "BorrowBook with valid ID: Should return new dueDate")]
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
  [Fact(DisplayName = "BorrowBook with invalid ID: Should return NULL")]
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
  [Fact(DisplayName = "BorrowBook when already borrowed: Should return NULL")]
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
  [Fact(DisplayName = "ReturnBook with valid ID: Should return TRUE")]
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
  [Fact(DisplayName = "ReturnBook with invalid Id: Should return FALSE")]
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
  [Fact(DisplayName = "ReturnBook when book not borrowed: Should return FALSE")]
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
  [Fact(DisplayName = "MarkAsReturned: Should set IsBorrowed to FALSE and dueDate to NULL")]
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

  //* ---------------------- Test DeleteBook With Valid Id --------------------- */
  [Fact(DisplayName = "DeleteBook with valid ID: Should remove book from _books and return TRUE")]
  public void DeleteBook_Test_With_Valid_Id()
  {
    //Arrange
    int id = 1;
    var count = _service.Count;

    //Act
    var result = _service.DeleteBook(id);

    //Assert
    Assert.True(result);
    Assert.Null(_service.GetBookById(id));
    Assert.Equal(_service.Count, count - 1);
  }

  //* --------------------- Test DeleteBook With Invalid Id -------------------- */
  [Fact(DisplayName = "DeleteBook with invalid ID: Should return FALSE")]
  public void DeleteBook_Test_With_Invalid_Id()
  {
    //Arrange
    int invalidId = 99;
    var count = _service.Count;

    //Act
    var result = _service.DeleteBook(invalidId);

    //Assert
    Assert.False(result);
    Assert.Equal(_service.Count, count);
  }
}