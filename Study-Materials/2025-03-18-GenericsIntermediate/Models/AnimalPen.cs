using Generics.Interfaces;

namespace Generics.Models;

//Siden AnimalPen skal implementere både pushing og popping metodene,
//må den inherite både IAnimalPopper og IAnimalPusher.
public class AnimalPen<T>(int animalCount): IAnimalPopper<T>, IAnimalPusher<T>
{
    private int _position;

    private T[] _pen {get;init;} = new T[animalCount];

    public void Push(T animal) => _pen[_position++] = animal;

    public T Pop() => _pen[Math.Max(0, --_position)];
    
    public int Count{get => _position+1;}

    public T this [int index] => _pen[index];

}