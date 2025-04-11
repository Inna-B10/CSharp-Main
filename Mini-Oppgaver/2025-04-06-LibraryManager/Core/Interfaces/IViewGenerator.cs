using System;

namespace Core.Interfaces;

public interface IViewGenerator
{
  void ViewMenu();
  void ViewBookDetails(IBook? book, int id);
  void ViewBooksList(List<IBook> books);
  string GetValidInput(string text);
}
