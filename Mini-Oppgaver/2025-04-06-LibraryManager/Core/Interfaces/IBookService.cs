using System;

namespace Core.Interfaces;

public interface IBookService
{
  bool AddBook(string title, string author);
  List<IBook> GetAllBooks();
  IBook? GetBookById(int id);
  bool BorrowBook(int id, DateTime dueDate);
  bool ReturnBook(int id);
}
