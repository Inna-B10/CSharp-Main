using GenericsIntermediateRepetisjon.Classes.List;
namespace GenericsIntermediateRepetisjon;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        
        var numbersLinkedList = new MyLinkedList<int>
        {
            10,
            23
        };
        foreach (var num in Enumerable.Range(1,40)) numbersLinkedList.Add(num);
        numbersLinkedList.Print();
        numbersLinkedList[10] = 48;
        var removed = numbersLinkedList.TryRemove(100);
        Console.WriteLine(removed);

        foreach (var num in numbersLinkedList)
        {
            Console.WriteLine(num);
        }
        var filteredList = numbersLinkedList.Where(num => num < 10);
        foreach (var num in filteredList)
        {
            Console.WriteLine(num);
        }
        
    }
}
