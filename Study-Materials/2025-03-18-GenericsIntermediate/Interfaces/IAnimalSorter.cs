namespace Generics.Interfaces;

public interface IAnimalSorter<T>
{
    void SortByAge(IEnumerable<T> animals);
}