using Core.Classes.Controllers;
using Core.Classes.Models;
using Core.Classes.Services;
using Core.Classes.Views;

namespace App;
class Program
{
  static void Main(string[] args)
  {
    var startProgram = new BookController(new BookService(), new ViewGenerator());
    startProgram.Run();

  }
}
