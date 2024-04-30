public class NumberList
{
    private List<int> numbers;

    public NumberList()
    {
        numbers = new List<int>();
    }

    public void AppendNumber(int number)
    {
        numbers.Add(number);
    }

    public void RemoveNumber(int number)
    {
        numbers.Remove(number);
    }

    public void DisplayNumbers()
    {
        Console.WriteLine("Numbers in our list:\n");
        foreach (int num in numbers)
        {
            Console.WriteLine(num);
        }
    }
}