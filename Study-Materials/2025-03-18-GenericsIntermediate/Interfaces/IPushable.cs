namespace Generics.Interfaces;

public interface IPushable<in T>
{
    void Push(T obj);
}