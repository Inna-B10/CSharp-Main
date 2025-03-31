namespace GenericsIntermediateRepetisjon.Interfaces.List;

/// <summary>
/// Denne interfacen representerer muligheten for å se om listen "container" data.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IContains<in T>
{
    bool Contains(T obj);
}