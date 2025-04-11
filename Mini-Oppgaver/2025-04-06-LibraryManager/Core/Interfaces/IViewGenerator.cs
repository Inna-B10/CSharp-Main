using System;

namespace Core.Interfaces;

public interface IViewGenerator
{
  void ViewMenu();
  void ViewBookDetails(IBook? book, int id);
  void ViewAllBooks(List<IBook> books);
  void ViewBorrowedBooks(List<IBook> books);
  string GetValidInput(string text);
}
