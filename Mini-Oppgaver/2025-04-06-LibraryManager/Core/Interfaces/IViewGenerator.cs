using System;

namespace Core.Interfaces;

public interface IViewGenerator
{
  void ViewMenu();
  void ViewBookDetails(IBook? book);

  void ViewAllBooks(List<IBook> books);
  //  void ViewBorrowedBooks();
  // string GetInput(string prompt);

}
