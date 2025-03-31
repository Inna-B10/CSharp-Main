# TaskManager

I dette prosjektet skal vi kikke på software arkitektprinsippet som heter MVC, Model, View, Controller. <br>
Dette designprinsippet er en avart av Layered architecture prinsippet, som baserer seg på at hvert "lag" av applikasjonen vår kun har ansvar for sitt lag. <br>
<br>
MVC tar dette separation of Concern elementet av Layered Architecture enda lengre,
ved å separere opp appen i tre distinkte lag, modell, view og controll. <br>
I dette prosjektet skal vi lage en enkel taskmanager app, som skal kunne gjøre "crud" operasjoner (Create, Read, Update, Delete) mot x antall tasks som holdes i minnet til pcen vår. <br>

Vi skal ha et controller lag. Dette controller laget skal kunne være mellomlaget mellom brukerinput, og funksjoner som skal kjøres.<br>
Vi kan se for oss at vår controller skal matche en input mot et output. <br>
f.eks:
- Bruker ber om info om tasks. 
- Controller server metoden som genererer view om tasks. 

Vi kan se for oss at flyten i prosjektet skal fungere som følgende:<br>
<br>
Bruker -> View -> Controller -> Model -> View -> Bruker<br>
Controller skal mota brukerinput, og skal kunne generere et view av modellen til brukeren basert på dette.<br>
La oss gå i detalj om hvordan arkitekturen ser ut.

## Arkitektur

Vi skal følge mvc design patternet. <br>

### Model + ServiceModels:
Har ansvar for datastruktur og business logikk.
- skal representere en task og propertiene til en task.<br>
    Detaljer:
    - Id, en unik identifier til en task.
    - Title, en kort beskrivelse eller et navn til tasken.
    - Description, Detaljert beskrivelse av en task.
    - IsCompleted, status om ferdig eller ikke.
    - DueDate, Når skal tasken være gjennomført.
    - MarkAsComplete, en metode som markerer en task som ferdig. 
- Skal representere henting og lagring av data i en manager eller context class.<br>
- Skal representere regler for validering og inserting og generell businesslogikk.<br>
    Detaljer:
    -   _tasks, intern lagring av tasks objekter.
    -   _nextId, en hjelpeproperty som kan gi hver task en unik autoinkrementerende id.
    -   Count get; -> returnere antall elementer i _tasks.
    -   GetAllTasks() -> returns _tasks
    -   GetTaskById(id) -> returns første fra _task med id.
    -   GetPendingTasks() -> returns _tasks som ikke er complete.
    -   GetCompleteTasks() -> returns _tasks som er complete.
    -   Add(title, description, duedate) -> adds task via constructor og autoincrementor. 
    -   CompleteTask(id) -> MarkAsComplete på task med id.
    -   DeleteTask(id) -> DeleteTask med id.

### View:
Skal representere datasettet via en leslig ui til brukeren.<br>
- Skal vise et enkelt ux/ui med menyer og options.
- Skal kunne håndtere brukerinput.
- Har ingen kontroll over State til Model.
- presenterer data agnostisk basert på inkommende data.<br>
    Detaljer:
    - DisplayMainMenu -> Viser en main menu til bruker.
    - DisplayTasks(liste av tasks, headeroption) -> Rendrer ut en liste av tasks. 
    - DisplayTaskDetails(enkel Task) -> Viser info om en enkel task.
    - GetInput(prompt, errorMessage) -> Henter brukerinput via en prompt. 
    - DisplayMessage(message, color) -> Viser en bedskjed til bruker, kan endre hva type display via display options. 
    - WaitForKey -> Venter til bruker trykker en knapp, i.e. velger en ui option. 

### Controller
Denne delen av applikasjonen skal være coordineringslaget av applikasjonen vår.<br>
Den skal koordinere Modeller mot Views og vise versa. <br>
- Hvis et view henter brukerinput, skal controlleren koordinere og behandle dette mot vår modell.
- Oppdaterer modellen om det trengs.
- Velger hva View som skal kjøres neste gang.<br>
    Detaljer:
    - _taskContext -> informasjon om alle tasks.
    - _viewGenerator -> informasjon om Views.
    - Run -> Kjører applikasjonen vår.
    - ViewAllTasks -> Viser alle tasks til brukeren.
    - ViewPendingTasks -> Viser uferdige tasks til bruker.
    - ViewCompletedTask -> Viser bare ferdige tasks til bruker.
    - AddTask -> Legger til en ny task.
    - ViewTaskDetail -> Se detaljer for en spesifikk Task.
    - CompleteTask -> markerer en task som ferdig.
    - DeleteTask -> fjerner en task.
- Er på mange måter organisatoren eller dirigenten for programflyten i programmet vårt.

La oss kikke litt i detalj hvordan Controlleren behandler Component Interaction:
1. Brukeren velger en option fra View sin MainMenu.
2. View passer input videre til vår Controller.
3. Controlleren behandler input, og velger hvilken metode i modellen/servicemodellen vår som skal kjøres. 
4. Modellen vår oppdaterer staten sin, og returnerer resultatet til controlleren vår.
5. Controlleren bestemmer da hvilket View som skal vises til bruker, og gir Viewet korrekt data for å vise det det trenger.
6. View rendrer informasjon til brukeren. 


## Designprinsipp i MVC:

### Inkapsling (Separation of Concerns)
Hver komponent lever i sitt eget lag, og har kun ansvar for kode og state i sitt lag.
Dette kan gjøre det lettere å forstå hvordan koden fungerer, lettere å maintaine i lang tid, og lettere å utvide med ny funksjonalitet senere. 

### Single Responsibility (SRP)
Hver klasse har en primær rolle:
- Task: representerer en enkel entity.
- TaskContext: Representerer og behandler datastrukturen for Tasks.
- ViewGenerator: Håndterer og genererer brukerinterfacen.
- TaskController: Koordinerer actions mellom Model og View.

### Loose Coupling
Komponenter snakker sammen via Interfaces, men vet ellers ingenting om hverandre.<br> Hvis den underliggende logikken i hvert lag endrer seg, vil godt utformete interfaces la det være mulig å endre logikke i en komponent, uten å måtte endre noe av logikken i de andre. 