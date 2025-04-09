using System;

namespace Core.Interfaces;

public interface IBookService
{
  bool AddBook(IBook book);
  List<IBook> GetAllBooks();
  IBook? GetBookById(int id);
  bool BorrowBook(int id, DateTime dueDate);
  bool ReturnBook(int id);
}
