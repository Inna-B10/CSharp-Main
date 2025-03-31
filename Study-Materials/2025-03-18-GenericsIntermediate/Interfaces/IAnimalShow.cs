namespace Generics.Interfaces;

public interface IAnimalShow<out T>
{
    T GetNextPerformer();
    int PerformerCount {get;}
}