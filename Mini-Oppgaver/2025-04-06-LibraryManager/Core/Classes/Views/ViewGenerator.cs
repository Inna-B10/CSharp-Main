using System;
using Core.Interfaces;

namespace Core.Classes.Views;

public class ViewGenerator : IViewGenerator
{

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

  public void ViewBookDetails(IBook? book)
  {
    Console.Clear();
    if (book is null)
    {
      Console.WriteLine("The book not found");
      return;
    }
    var dateContext = book.IsBorrowed ? $", Due: {book.DueDate}" : "";

    Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}, Borrowed: {book.IsBorrowed}{dateContext}");
    Console.WriteLine($"""
        ========== Book Title: {book.Title} ============

                Id: {book.Id}
                Author: {book.Author}
                Status: {(book.IsBorrowed ? "Borrowed" : "Available")}
                {(book.IsBorrowed ?
                $"Due Date: {book.DueDate}" : "")}
        ================================================
    """);
  }

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
    | Id | Title                        | Author             | Status | Due Date |
    ------------------------------------------------------------------------------
    """);
    foreach (var book in books)
    {
      var status = book.IsBorrowed ? "Borrowed" : "Available";

      Console.WriteLine($"| {book.Id,-3} | {book.Title,-30} | {book.Author,20} | {status} | {book.DueDate} |");
    }
    Console.WriteLine(
"------------------------------------ End -------------------------------------"
    );
  }

  public string GetInput(string text)
  {
    Console.Write(text);
    return Console.ReadLine() ?? "";
  }
}
