//Namespacet til prosjektet skal reflektere filstrukturen i prosjektet.
//Siden denne fillen eksisterer i Classes mappen i prosjektet vårt,
//skal filen leve i namespacet som reflekterer dette. 

namespace Kodestil_kodestandarer.Interfaces;
//Hvis man vil ha flere namespaces i hver vil, kan namespaces være scoped.
//Det er annbefalt å likevell ha ett namespace per fil. 


//Hvis man ikke bruker scopet namespace, er implicit all kode i
//filen i samme namespace.

//Når vi navngir classes, interfaces og andre typer, bruker vi PascalCase.
//Interfaces sitt navn starter alltid med I, for å markere tydlig at det er en Interface.
public interface IDatabaseModel
{
    //Alle ikke-private fields, properties, methods m.m. som tilhører en class, interface, struct eller andre typer, skal også være i pascal case.
    public List<string> Names {get;set;}

    /// <summary>
    /// Bruk gjerne xml blokk-kommentarer for å lage korte, men beskrivende forklaringer på
    /// Hva en metode gjør.
    /// 
    /// Har du en interface, la gjerne kommentaren ligge i Interfacen, slik at kommentaren er tilgjengelig
    /// uavhengig av hva objekt som arver det. 
    /// </summary>
    /// <param name="databaseType"></param>
    public void GetConnectionString(string databaseType);
}