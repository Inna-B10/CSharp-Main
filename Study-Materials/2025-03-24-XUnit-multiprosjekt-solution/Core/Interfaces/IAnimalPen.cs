namespace Core.Interfaces;

public interface IAnimalPen<T>: IAnimalReader<T>, IAnimalWriter<T>
{
    public int Count {get;}
}