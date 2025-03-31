using Generics.Interfaces;

namespace Generics.Models;

public class AnimalSorter<T>: IAnimalSorter<T> where T : Animal
{
    public void SortByAge(IEnumerable<T> animals)
    {
        animals.ToList().Sort((a, b) => a.Age.CompareTo(b.Age));
        Console.WriteLine("Animals sorted by age:");
        foreach (var animal in animals)
        {
            Console.WriteLine($"- {animal.Name} ({animal.Age} years)");
        }
    }
}