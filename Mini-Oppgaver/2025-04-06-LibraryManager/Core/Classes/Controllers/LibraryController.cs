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
        var input = _view.GetInput("Enter your choice: ");
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
          ViewBorrowedBooks();
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

  private void ViewBorrowedBooks()
  {
    throw new NotImplementedException();
  }

  private void GetBookById()
  {
    throw new NotImplementedException();
  }

  private void GetAllBooks()
  {
    throw new NotImplementedException();
  }

  private void DeleteBook()
  {
    throw new NotImplementedException();
  }

  private void ReturnBook()
  {
    throw new NotImplementedException();
  }

  private void BorrowBook()
  {
    throw new NotImplementedException();
  }

  private void AddBook()
  {
    throw new NotImplementedException();
  }
}