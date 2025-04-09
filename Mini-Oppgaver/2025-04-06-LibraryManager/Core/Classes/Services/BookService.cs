using System;
using Core.Interfaces;

namespace Core.Classes.Services;

public class BookService : IBookService
{
  private readonly List<IBook> _books = [];
  public bool AddBook(IBook book)
  {
    if (!_books.Any(b => b.Id == book.Id)) return false;

    _books.Add(book);
    return true;
  }

  public List<IBook> GetAllBooks()
  {
    return _books;
  }

  public IBook? GetBookById(int id)
  {
    return _books.FirstOrDefault(b => b.Id == id);
  }

  public bool BorrowBook(int id, DateTime dueDate)
  {
    var book = GetBookById(id);
    if (book is null || book.IsBorrowed) return false;
    book.MarkAsBorrowed(dueDate);
    return true;
  }

  public bool ReturnBook(int id)
  {
    var book = GetBookById(id);
    if (book is null || !book.IsBorrowed) return false;

    book.MarkAsReturned();
    return true;
  }
}
