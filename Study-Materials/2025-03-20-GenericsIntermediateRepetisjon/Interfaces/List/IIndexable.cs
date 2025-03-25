namespace GenericsIntermediateRepetisjon.Interfaces.List;


/// <summary>
/// Dette er interfacen som representerer at vår liste kan Indexes. 
/// </summary>
/// <typeparam name="T"></typeparam>Y
public interface IIndexable<T>
{
    T? this[int index] {get; set;}
}