using Generics.Interfaces;

namespace Generics.Models;

public class AnimalShow<T>(List<T> performers): IAnimalShow<T>
{
    private List<T> _performers = performers;

    public T GetNextPerformer()
    {
        // Returnerer en utøver og roterer listen
        var performer = _performers[0];
        _performers.RemoveAt(0);
        _performers.Add(performer);
        return performer;
    }

    public int PerformerCount => _performers.Count;
}