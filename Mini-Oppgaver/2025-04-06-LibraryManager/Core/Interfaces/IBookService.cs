using System;

namespace Core.Interfaces;

public interface IBookService
{
  bool AddBook(string title, string author);
  DateTime? BorrowBook(int id);
  bool ReturnBook(int id);
  bool DeleteBook(int id);
  List<IBook> GetAllBooks();
  IBook? GetBookById(int id);
  List<IBook> GetBorrowedBooks();
  DateTime MarkAsBorrowed(IBook book);
  void MarkAsReturned(IBook book);
}
