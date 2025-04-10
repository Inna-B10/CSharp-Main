using System;

namespace Core.Interfaces;

public interface IViewGenerator
{
  void ViewMenu();
  void ViewBookDetails(IBook? book);
  void ViewAllBooks(List<IBook> books);
  //  void ViewBorrowedBooks();
  //AddBook
  //BorrowBook
  //ReturnBook
  //DeleteBook

  string GetInput(string text);


}
