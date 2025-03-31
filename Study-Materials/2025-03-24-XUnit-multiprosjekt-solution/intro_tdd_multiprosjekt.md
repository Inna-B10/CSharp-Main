# Testdreven utvikling med xUnit, og multiprosjekt solution.

## Hvorfor TDD?

TDD er nok et verktøy i verktøykassen vår, for å passe på at programmet vårt oppnår det vi vil.<br>

Det lar oss forhåndsbestemme et sett av kriterier programmet vårt skal passe, for at vi kan garantere at programmet vårt fungerer slik som det skal. <br>

## Oppsett:
For å drive TDD, kan vi bruke vår solution som en container for flere .net prosjekter.<br>
Vi starter i en tom mappe, og bruker 

```bash
dotnet new sln
```

for å lage en ny solutionfil i mappen vi er i, solutionfilen får samme navn som mappen.<br>
En solutionfil kan inneholde flere .net prosjekter, som kan linkes sammen via vår solutionfil.<br>

Før vi lager prosjektene kan det være lurt å tenke på følgende:<br>
La solutionfilen ligge i hovedmappen, så lag nye mapper for hvert prosjekt:
- /Projectname
    - /Projectname.sln
    -  /SubProject

Vi oppnår dette via følgende:

```bash
dotnet new classlib -o ./Core
dotnet sln add ./Core
dotnet new xunit -o ./Tests
dotnet sln add ./Tests
dotnet add ./Tests/Tests.csproj reference ./Core/Core.csproj
dotnet new console -o ./App
dotnet sln add ./App
dotnet add ./App/App.csproj reference ./Core/Core.csproj
```

Legg merke til -o flagget som lar oss definere en output for templaten vår. 

Hva gjorde vi nå?<br>
Dere vil se at vi nå har følgende mappestruktur:

- /Prosjektnavn
    - /Prosjektnavn.snl
    - /App
        - /App.csproj
    - /Core
        - /Core.csproj
    - /Tests
        - /Tests.csproj

App er vår standard console app template, som vi begynner å bli godt kjent med.
<br>
classlibrary representerer bare en samling av classes og interfaces og andre modeller, som de andre prosjektene våre nå har tilgjengelige til bruk i sitt namespace.<br>
Det lar oss skrive en klasse en gang, men ta de i bruke i både console appen vår, og i xUnit prosjektet vårt.<br>
Vår sln fil fungerer som en container som styrer referanser mellom prosjektene, men også at vi får korrekt linting og lsp i alle prosjektene i samme solution.<br>
Dette lar oss lage flere .net prosjekter i samme folder, som alle skal sammen være en del av et større prosjekt, og lar oss bygge opp applikasjoner og programmer på en mer modulær og tidsriktig måte.<br>


La oss se på et eksempel:

# Dyrehagen

I forrige uke så vi på en implementasjon av datatypen Stack.<br>
Det er en datatype som fungerer følgende:
- Data skal kunne pushes til toppen av Stacken.
- Data skal kunne poppes av toppen av Stacken. 
- Det skal kunne opperere på en generisk typ T datatype. 

Eller enkelt fortalt, det fungerer på LIFO prinsippet (Last in, first out).<br>
Vi laget en utvidet "stack" for å holde kontroll på forskjellige dyreinnhenginger.<br>
La oss lage det samme prosjektet på nytt, men denne gangen forhåndsbestemme et set med tester vår dyreinnhenging bør kunne oppfylle. <br>

## Kriterier:
Når vi driver TDD, så kan det være lurt å gjøre litt forplanlegging. 
Det kan være lurt som minstekrav å liste opp et sett med kriterier man ønsker koden skal oppfylle, slik at man kan skrive tester som oppfyller disse. 

La oss tenke litt på hva kriterier vi ønsker.<br>

### Dyr:
- Vi ønsker å lage en baseclass(superclass) Animal, som skal kunne ha et navn, alder, og en foringstype.
- Vi ønsker å lage subclasser som skal kunne inherite fra Animal, men kunne ha mer spesifikke karakteristikker for hvert dyr. 
    - Bear:
        - FurColor
    - Camel:
        - HumpCount
    - Lion:
        - ManeColor
    - Penguin:
        - IsEmperor
    - Elephant:
        - TruncLength

### Animal Pen:

- Vi ønsker en generisk Animal Pen, som kan holde alle typer av baseclass Animal via en generisk T.
- Vi må ha et sett med interfaces som tillater operasjoner på T, men som tillater covariance, og contravariance.
- Funksjonalitet:
    - Lage en pen av gitt størrelse int size.
    - Holde en Count av antal dyr i pen.
    - Ha en metode som lar oss Pushe et Animal av typ T til stacken.
    - Ha en metode som lar oss Poppe ut et dyr av typ T fra stacken.

Vi kan implementere alt dette i Core prosjektet vårt. Slik at alle modellene og interfacene er tilgjengeliggjort i de to andre prosjektene våre. 

## Gjennomføring
Når vi gjør TDD følger vi gjerne Red Green Refactor prinsippet:<br>
- Skriv først tester som du vet ikke kan implementeres/som kommer til å krasje. Disse representerer målene vi ønsker at programmet vårt skal oppnå. (red)
- Implementer minstekravet for at testene skal passe. (green)
- Kan du nå refaktore koden for å gjøre den bedre, og mer leslig, men at testene fremdeles passer? (refactor)

Her gjør du det lurt å teste edge-cases i tillegg. <br>
Bør koden din throwe hvis noen kriterier stemmer?
Hvordan håndterer koden din feilbehandling? 

Interfaces lar oss sette krav til hvordan vår kode skal implementeres, mens testing er et verktøy som lar oss sette krav til hva vår kode skal utføre. <br>
Vi kan via tester garantere for at vår kode gjennomfører kravene gitt den.
