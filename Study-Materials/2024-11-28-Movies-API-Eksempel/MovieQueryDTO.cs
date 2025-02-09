using System;

namespace Kartlegging_API_Eksempel;

//Dette er en modell av de potensielle verdiene vi kan hente ut fra en query.
//Alle er "Nullable" som vil si de trenger ikke eksistere.
public class MovieQueryDTO
{
    public string? Title {get;init;}
    public int? To {get;init;}
    public int? From {get;init;}
    public string? Actor {get;init;}
    public string? Genre {get;init;}
}
