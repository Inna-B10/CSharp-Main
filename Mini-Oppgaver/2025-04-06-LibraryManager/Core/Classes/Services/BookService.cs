using System;
using System.ComponentModel;
using Core.Classes.Models;
using Core.Interfaces;

namespace Core.Classes.Services;

public class BookService : IBookService
{
  private readonly List<IBook> _books = [];
  public int Count => _books.Count;
  private int _nextId;

  //* --------------------------------- AddBook -------------------------------- */
  public bool AddBook(string title, string author)
  {
    try
    {
      var newBook = new Book(++_nextId, title, author);
      _books.Add(newBook);
      return true;
    }
    catch
    {
      Console.WriteLine("Failed to add a new book!");
      return false;
    }
  }

  //* ------------------------------- BorrowBook ------------------------------- */
  public DateTime? BorrowBook(int id)
  {
    var book = _books.FirstOrDefault(b => b.Id == id);
    if (book is null || book.IsBorrowed)
      return null;
    DateTime newDueDate = MarkAsBorrowed(book);
    return newDueDate;
  }

  //* ------------------------------- ReturnBook ------------------------------- */
  public bool ReturnBook(int id)
  {
    var book = _books.FirstOrDefault(b => b.Id == id);
    if (book is null || !book.IsBorrowed) return false;

    MarkAsReturned(book);
    return true;
  }

  //* ------------------------------- DeleteBook ------------------------------- */
  public bool DeleteBook(int id)
  {
    var book = _books.FirstOrDefault(book => book.Id == id);
    if (book is null) return false;
    return _books.Remove(book);
  }
  //* ------------------------------- GetAllBooks ------------------------------ */
  public List<IBook> GetAllBooks()
  {
    return _books;
  }

  //* ------------------------------- GetBookById ------------------------------ */
  public IBook? GetBookById(int id)
  {
    return _books.FirstOrDefault(b => b.Id == id);
  }

  //* ---------------------------- GetAllBorrowedBooks ---------------------------- */
  public List<IBook> GetAllBorrowedBooks()
  {
    return [.. _books
    .Where(book => book.IsBorrowed)
    .OrderBy(book => book.DueDate)];
  }

  //* ----------------------- GetBooksWithExpiredDueDate ----------------------- */
  public List<IBook> GetBooksWithExpiredDueDate()
  {
    return [.. _books
    .Where(book => book.IsBorrowed && book.DueDate < DateTime.Today)
    .OrderBy(book => book.DueDate)];
  }

  //* ----------------------------- MarkAsBorrowed ----------------------------- */
  public DateTime MarkAsBorrowed(IBook book)
  {
    book.IsBorrowed = true;
    book.DueDate = DateTime.Today.AddDays(14);
    return (DateTime)book.DueDate;
  }

  //* ----------------------------- MarkAsReturned ----------------------------- */
  public void MarkAsReturned(IBook book)
  {
    book.IsBorrowed = false;
    book.DueDate = null;
  }
}
