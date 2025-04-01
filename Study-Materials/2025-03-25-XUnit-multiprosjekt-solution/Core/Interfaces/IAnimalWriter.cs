namespace Core.Interfaces;

public interface IAnimalWriter<in T>
{
    public void Push(T obj);
}