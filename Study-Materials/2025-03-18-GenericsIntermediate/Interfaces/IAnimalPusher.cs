namespace Generics.Interfaces;

public interface IAnimalPusher<in T>: IPushable<T>, ICountable;