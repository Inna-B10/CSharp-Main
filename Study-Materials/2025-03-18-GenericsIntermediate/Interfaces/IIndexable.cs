namespace Generics.Interfaces;

public interface IIndexable<out T>
{
    T this[int index]{get;}
}