namespace Generics.Interfaces;

public interface IPoppable<out T>
{
    T Pop();
}