namespace GenericsIntermediateRepetisjon.Interfaces.List;

/// <summary>
/// Dette er interfacen som viser til resten av programmet vårt at det går ann å fjerne ting fra listen vår.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRemoveable<in T>
{
    bool TryRemove(T obj);
    void Remove(T obj);
}
