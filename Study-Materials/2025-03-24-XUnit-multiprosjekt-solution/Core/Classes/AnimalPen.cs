using System;
using Core.Interfaces;

namespace Core.Classes;

public class AnimalPen<T>(int size) : IAnimalPen<T>
{
    private T[] _pen = new T[size];
    private int _position;

    public int Count => _position;

    public T Pop()
    {
        if(_position == 0) throw new IndexOutOfRangeException("No animals in the pen");
        return _pen[--_position];
    }

    public void Push(T obj)
    {
        if (_position >= _pen.Length) throw new IndexOutOfRangeException("No more room in the animalPen");
        _pen[_position++] = obj;
    }
}