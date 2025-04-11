using System;
using Core.Interfaces;

namespace Core.Classes.Views;

public class ViewGenerator : IViewGenerator
{

  /* -------------------------------- ViewMenu -------------------------------- */
  public void ViewMenu()
  {
    Console.Clear();
    Console.WriteLine("""
        ===================  Main Menu =============

                1. Add New Book
                2. Borrow Book
                3. Return Book
                4. Delete Book
                5. View All Books
                6. View Book Details
                7. View Borrowed Books
                0. Exit
        
        ============================================        
        """);
  }

  /* ----------------------------- ViewBookDetails ---------------------------- */
  public void ViewBookDetails(IBook? book, int id)
  {
    Console.Clear();
    if (book is null)
    {
      Console.WriteLine($"No book with ID {id} was found.");
      return;
    }
    var dateContext = book.IsBorrowed ? $", Due: {book.DueDate}" : "";

    Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}, Borrowed: {book.IsBorrowed}{dateContext}");
    Console.WriteLine($"""
        ================= Book details ================
                Title: {book.Title}
                Id: {book.Id}
                Author: {book.Author}
                Status: {(book.IsBorrowed ? "Borrowed" : "Available")}
                {(book.IsBorrowed ?
                $"Due Date: {book.DueDate}" : "")}
        ================================================
    """);
  }

  /* ------------------------------ ViewAllBooks ------------------------------ */
  public void ViewAllBooks(List<IBook> books)
  {
    Console.Clear();

    if (books.Count == 0)
    {
      Console.WriteLine("No records found! Press any key to continue...");
      Console.ReadKey();
      return;
    }
    Console.WriteLine("""
    | Id  | Title                          | Author                         | Status    | Due Date     |
    ----------------------------------------------------------------------------------------------------
    """);
    foreach (var book in books)
    {
      var status = book.IsBorrowed ? "Borrowed" : "Available";

      Console.WriteLine($"| {book.Id,-3} | {book.Title,-30} | {book.Author,-30} | {status} | {book.DueDate,12} |");
    }
    Console.WriteLine(
"-------------------------------------------- End of list -------------------------------------------"
    );
  }

  /* ---------------------------- ViewBorrowedBooks --------------------------- */
  public void ViewBorrowedBooks(List<IBook> books)
  {
    Console.Clear();

    if (books.Count == 0)
    {
      Console.WriteLine("No borrowed books found! Press any key to continue...");
      Console.ReadKey();
      return;
    }
    Console.WriteLine("""
    | Id  | Title                          | Author                         | Status    | Due Date     |
    ----------------------------------------------------------------------------------------------------
    """);
    foreach (var book in books)
    {
      var status = book.IsBorrowed ? "Borrowed" : "Available";

      Console.WriteLine($"| {book.Id,-3} | {book.Title,-30} | {book.Author,-30} | {status} | {book.DueDate,12} |");
    }
    Console.WriteLine(
"-------------------------------------------- End of list -------------------------------------------"
    );
  }
  /* ------------------------------ GetValidInput ----------------------------- */
  public string GetValidInput(string text)
  {
    string input;
    string cleanedInput;
    do
    {
      Console.Write(text);
      input = Console.ReadLine()?.Trim() ?? "";
      cleanedInput = new(input.Where(c => !char.IsControl(c)).ToArray());
      if (string.IsNullOrEmpty(cleanedInput))
      {
        Console.WriteLine("Invalid input. Please try again.");
      }

    } while (string.IsNullOrEmpty(cleanedInput));
    return cleanedInput.Trim();
  }

}
