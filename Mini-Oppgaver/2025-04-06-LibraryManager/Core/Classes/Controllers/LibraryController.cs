using Core.Classes.Services;
using Core.Classes.Views;
using Core.Interfaces;

namespace Core.Classes.Controllers;

public class LibraryController(BookService service, ViewGenerator view) : ILibraryController
{
  private readonly BookService _service = service;
  private readonly ViewGenerator _view = view;
  public void Run()
  {
    while (true)
    {
      Console.Clear();
      _view.ViewMenu();
      int userChoice;
      while (true)
      {
        var input = _view.GetValidInput("Enter your choice: ");
        if (int.TryParse(input, out userChoice))
        {
          break;
        }
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("Invalid input. Please enter a number.");
        Console.ResetColor();
      }
      switch (userChoice)
      {
        case 1:
          AddBook();
          break;
        case 2:
          BorrowBook();
          break;
        case 3:
          ReturnBook();
          break;
        case 4:
          DeleteBook();
          break;
        case 5:
          GetAllBooks();
          break;
        case 6:
          GetBookById();
          break;
        case 7:
          GetBorrowedBooks();
          break;
        case 0:
          return;
        default:
          Console.ForegroundColor = ConsoleColor.DarkRed;
          Console.WriteLine("Invalid Choice. Press any key to continue....");
          Console.ResetColor();
          Console.ReadKey();
          break;
      }
    }
  }

  /* --------------------------------- AddBook -------------------------------- */
  private void AddBook()
  {
    var title = _view.GetValidInput("Enter book's title: ");
    var author = _view.GetValidInput("Enter author's name: ");

    var result = _service.AddBook(title, author);
    if (result == true)
    {
      Console.WriteLine($"Book was added successfully! \nPress any key to continue...");
      Console.ReadKey();
    }
    else
    {
      Console.WriteLine($"Couldn't to add a new book. \nPress any key to continue...");
      Console.ReadKey();
    }
  }

  /* ------------------------------- BorrowBook ------------------------------- */
  private void BorrowBook()
  {
    string input = _view.GetValidInput("Enter valid numeric id of the book you want to borrow: ");
    int id;
    while (!int.TryParse(input, out id))
    {
      input = _view.GetValidInput("Invalid input. Please enter valid numeric whole number: ");
    }
    var result = _service.BorrowBook(id);
    if (result == null)
    {
      Console.WriteLine("Cannot register current book as borrowed");
    }
    else
    {
      Console.WriteLine($"The book with id {id} is borrowed due {result}");
    }
    Console.WriteLine("Press any key to continue....");
    Console.ReadKey();
  }

  /* ------------------------------- ReturnBook ------------------------------- */
  private void ReturnBook()
  {
    Console.WriteLine("Press any key to continue....");
    Console.ReadKey();
  }

  /* ------------------------------- DeleteBook ------------------------------- */
  private void DeleteBook()
  {
    Console.WriteLine("Press any key to continue....");
    Console.ReadKey();
  }

  /* ------------------------------- GetAllBooks ------------------------------ */
  private void GetAllBooks()
  {
    var books = _service.GetAllBooks();
    _view.ViewAllBooks(books);
    Console.WriteLine("Press any key to continue....");
    Console.ReadKey();
  }

  /* ------------------------------- GetBookById ------------------------------ */
  private void GetBookById()
  {
    var bookId = _view.GetValidInput("Enter book Id to get details: ");
    int id;
    while (!int.TryParse(bookId, out id))
    {
      bookId = _view.GetValidInput("Invalid input. Enter numeric whole number: ");
    }
    var book = _service.GetBookById(id);

    _view.ViewBookDetails(book, id);
    Console.WriteLine("Press any key to continue....");
    Console.ReadKey();
  }

  /* ---------------------------- GetBorrowedBooks --------------------------- */
  private void GetBorrowedBooks()
  {
    var books = _service.GetBorrowedBooks();
    _view.ViewBorrowedBooks(books);
  }

}