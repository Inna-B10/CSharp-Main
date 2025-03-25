using GenericsIntermediateRepetisjon.Interfaces.Node;

namespace GenericsIntermediateRepetisjon.Classes.Node;

/// <summary>
/// Dette er en node, som representerer en vilkårlig node i vår linked list.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="data"></param>
/// <param name="next"></param>
public class Node<T>(T? data, Node<T>? next = null) : INode<T>
{
    public T? Data {get;set;} = data;
    public Node<T>? Next {get;set;} = next;
}