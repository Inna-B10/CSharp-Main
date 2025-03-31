namespace GenericsIntermediateRepetisjon.Interfaces.List;

/// <summary>
/// Dette er en interface, som skal representere metodene
/// For å "legge til" en ny node i vår liste. 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IAddible<in T>
{
    void Add(T obj);
    void AddFirst(T obj);
}