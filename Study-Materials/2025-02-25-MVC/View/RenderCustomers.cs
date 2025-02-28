using System;

namespace mvc.View;

public class RenderCustomers
{
    public void DisplayCustomers(List<string> customers)
    {
        customers.ForEach(customer => Console.WriteLine(customer));
    }
}
