namespace Core.Interfaces;

public interface IAnimalReader<out T>
{
    T Pop();
}