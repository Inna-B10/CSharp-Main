using System;
using mvc.Models;
using mvc.View;

namespace mvc.Controllers;

public class CustomerController(Customers customers, RenderCustomers view)
{
    /* Denne controlleren skal styre hvordan vår app skal interacte med vår customer modell. */
    private readonly List<string> _customers = customers.GetCustomers();
    private readonly RenderCustomers _view = view;
    public void AddCustomer()
    {
        Console.WriteLine("Please enter a valid name");
        var input = Console.ReadLine();
        while (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Name was not valid, please try again");
            input = Console.ReadLine();
        }
        _customers.Add(input);
    }
    public void DisplayCustomers()
    {
        _view.DisplayCustomers(_customers);
    }
    public void RemoveCustomer()
    {   
        Console.WriteLine("Please enter a valid name");
        var input = Console.ReadLine();
        while (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Name was not valid, please try again");
            input = Console.ReadLine();
        }
        _customers.Remove(input);
    }
}
