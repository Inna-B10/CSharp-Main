using System;

namespace mvc.View;

public class RenderMeny
{
    public void DisplayMeny(List<string> meny)
    {
        meny.ForEach(item => Console.WriteLine(item));
    }
    public void DisplayAvailability(string input, bool availability)
    {
        Console.WriteLine($"{input} avaliable? {availability}");
    }
}
