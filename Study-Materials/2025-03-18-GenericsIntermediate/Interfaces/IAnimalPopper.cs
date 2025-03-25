namespace Generics.Interfaces;

public interface IAnimalPopper<out T>: IPoppable<T>, IIndexable<T>, ICountable;