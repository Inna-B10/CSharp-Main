using GenericsIntermediateRepetisjon.Classes.Node;

namespace GenericsIntermediateRepetisjon.Interfaces.Node;

public interface INode<T>
{
    public T? Data {get;set;}
    public Node<T>? Next {get;set;}
}