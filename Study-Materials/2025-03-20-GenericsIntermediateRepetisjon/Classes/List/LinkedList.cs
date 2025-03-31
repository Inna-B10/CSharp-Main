using System.Collections;
using System.Text.Json;
using GenericsIntermediateRepetisjon.Classes.Node;
using GenericsIntermediateRepetisjon.Interfaces.List;

namespace GenericsIntermediateRepetisjon.Classes.List;


/// <summary>
/// Dette er vår basic iterasjon av en linked list.
/// Det er en ganske standardisert datastruktur. 
/// Den fungerer som om vi har en løkke av datanoder, 
/// eller et perleskjede, hvor hver perle er en pakke av data.
/// Head representerer starten på skjedet, og vi kan telle perlene langs skjeden til vi finner
/// perlen vi vil ha. 
/// </summary>
/// <typeparam name="T"></typeparam>
public class MyLinkedList<T> : IAddible<T>, IContains<T>, IPrintable, IRemoveable<T>, ICountable, IIndexable<T>, IEnumerable<T?>
{
    /// <summary>
    /// Dette representerer startpunktet på listen vår, den kan gjøres private, så lenge vi passer på å lage gode
    /// metoder som enkapsulererer listen på, og som tillater brukeren av listen å hente, lagre, sette og edite data. 
    /// </summary>
    private Node<T>? _head {get;set;} 

    /// <summary>
    /// Dette er antal "perler" eller noder i listen vår. 
    /// </summary>
    public int Count {get;set;}

    /// <summary>
    /// Måte å definere en liste på, uten data i forhånd.
    /// </summary>
    public MyLinkedList()
    {
        _head = null;
        Count = 0;
    }
    
    /// <summary>
    /// Måte å definere en liste på, med data som "head"
    /// </summary>
    /// <param name="obj"></param>
    public MyLinkedList(T obj)
    {
        _head = new Node<T>(obj);
        Count = 1;
    }

    /// <summary>
    /// Dette er en metode som lar oss "indexe" inn i listen vår, som om det er et array. 
    /// Vi har laget metoder som lar oss hente verdier fra listen fra en spesifikk index, samt sette verdier i listen til en spesifikk index. 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public T? this[int index] 
    { 
        get => GetValue(index);
        set => SetValue(value, index); 
    }

    /// <summary>
    /// Dette er en metode som appender en node til slutten av listen vår. 
    /// </summary>
    /// <param name="obj"></param>
    public void Add(T obj)
    {
        var newNode = new Node<T>(obj);
        if (_head == null ) _head = newNode;
        else 
        {
            var current = _head;
            while(current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
        Count ++;
    }
    /// <summary>
    /// Denne metoden setter en node i starten av listen vår. 
    /// </summary>
    /// <param name="obj"></param>
    public void AddFirst(T obj)
    {
        if (_head == null) _head = new Node<T>(obj);
        else 
        {
            _head = new Node<T>(obj, _head);
        }
        Count ++;
    }

    /// <summary>
    /// Dette er en metode som skjekker om listen vår inneholder et spesifikt datapunkt.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public bool Contains(T obj)
    {
        var current = _head;
        while (current != null)
        {
            if (current.Data != null && current.Data.Equals(obj))
            {
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    /// <summary>
    /// Dette er en metode som printer ut alt innholdet i listen vår til Console.
    /// </summary>
    public void Print()
    {
        if (_head == null)
        {
            Console.WriteLine("List contains no items");
            return;
        };
        var current = _head;
        while (current != null)
        {
            Console.WriteLine(JsonSerializer.Serialize(current.Data));
            if (current.Next != null)
            {
                Console.WriteLine("->");
            }
            current = current.Next;
        }
        Console.WriteLine();
    }


    /// <summary>
    /// Dette er en metode som fjerner en node fra listen vår, 
    /// Den thrower exceptions hvis den ikke får det til. 
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public void Remove(T obj)
    {
        if (_head == null) throw new ArgumentOutOfRangeException($"List is currently empty. Cannot remove {obj}");
        if (!Contains(obj)) throw new ArgumentException($"Can't remove {obj} from list.");
        if (_head.Data != null && _head.Data.Equals(obj))
        {
            _head = _head.Next;
            Count --;
            return;
        }

        var current = _head;
        while(current.Next != null)
        {
            if (current.Next.Data != null && current.Next.Data.Equals(obj))
            {
                current.Next = current.Next.Next;
                Count --;
                return;
            }
        }
    }


    /// <summary>
    /// Dette er en metode som prøver å fjerne en node fra listen vår
    /// returnerer sann eller usann basert på om operasjonen var suksessfull eller ikke. 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public bool TryRemove(T obj)
    {
        if (_head == null) return false;

        if (_head.Data != null && _head.Data.Equals(obj))
        {
            _head = _head.Next;
            Count --;
            return true; 
        } 
        var current = _head;
        while (current.Next != null)
        {
            if (current.Next.Data != null && current.Next.Data.Equals(obj))
            {
                current.Next = current.Next.Next;
                Count --;
                return true;
            }
            current = current.Next;
        }
        
        return false;
    }

    /// <summary>
    /// Dette er en metode som setter en verdi til en spesifikk index.
    /// Thrower en argument out of range exception hvis index er utenfor rekkevidden. 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="index"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private void SetValue(T? obj, int index)
    {
        if (Count < index) throw new ArgumentOutOfRangeException($"index {index} is out of range of list.");
        var innerCount = 0;
        var current = _head;
        while (innerCount < index - 1)
        {
            current = current!.Next;
            innerCount++;
        }
        current!.Next = new Node<T>(obj, current.Next);
    }


    /// <summary>
    /// Dette er en metode som lar oss hente ut en verdi på en spesifikk index. 
    /// Dette er en privat metode, men blir tilgjengeliggjort via indexering i toppen. 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private T? GetValue(int index)
    {
        if (Count < index) throw new ArgumentOutOfRangeException($"Index {index} is out of range of list.");
        var innerCount = 0;
        var current = _head;
        while (innerCount < index)
        {
            current = current!.Next;
            innerCount++;
        }
        return current!.Data;
    }

    /// <summary>
    /// For å tilgjengeliggjøre alle kolleksjonsmetodene som eksisterer
    /// kan vi også implementere IEnumerable interfacen i C#
    /// IEnumeratoren som vi returnerer her, skal bare fortelle IEnumerable<T> interfacen vår
    /// hvordan den skal kunne hente data T fra hvert element i listen vår. 
    /// Så for å implementere den, så kan vi ta i bruk en enkel itereringsmetode,
    /// som itererer gjennom listen vår, og yielder dataen fra hvert element
    /// ut til IEnumerable iteratoren.
    /// </summary>
    /// <returns></returns>
    public IEnumerator<T?> GetEnumerator()
    {
        var current = _head;
        while (current != null)
        {
            /* Yield er et litt spesielt nøkkelord, som i dette tilfellet
            lar oss både returne dataen på punktet vi er, men også bookmarker hvor i iterasjonen vi er,
            slik at IEnumerable interfacen vår kan fortsette der den slapp.
            
            Du kan se for det at hver gang iteratoren treffer yield return statementet returnerer den
            verdien fra current.Data, men husker hvor i iterasjonen den var, slik at når neste element er kallet,
            starter den fra det gjeldende "bookmarket" og ikke helt i fra starten av while loopen. */
            yield return current.Data;
            current = current.Next;
        }
    }

    /// <summary>
    /// Denne generiske IEnumerator metoden er krevd av IEnumerable<T> interfacen
    /// Den kaller bare den interne GetEnumerator() metoden.
    /// </summary>
    /// <returns></returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
