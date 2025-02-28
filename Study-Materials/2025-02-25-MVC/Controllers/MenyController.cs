using System;
using mvc.Models;
using mvc.View;

namespace mvc.Controllers;
/* Denne controlleren styrer hvordan vår app skal interacte med vår meny modell. */
public class MenyController (MenyItems meny, RenderMeny view)
{
    private readonly RenderMeny _view = view;
    private readonly List<string> _meny = meny.GetMenyItems();
    public void GetAllMenyItems()
    {
        _view.DisplayMeny(_meny);
    }
    public void IsPizzaAvaliable()
    {
        var input = Console.ReadLine();
        _view.DisplayAvailability(input, _meny.Contains(input));
    }

}
