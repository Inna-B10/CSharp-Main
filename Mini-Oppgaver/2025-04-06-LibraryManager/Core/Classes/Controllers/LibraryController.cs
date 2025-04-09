using Core.Classes.Services;
using Core.Classes.Views;
using Core.Interfaces;

namespace Core.Classes.Controllers;

public class LibraryController(BookService service, ViewGenerator view) : ILibraryController
{
  private readonly BookService _service = service;
  private readonly ViewGenerator _view = view;
  public void Run()
  {
    _view.ViewMenu();
  }
}