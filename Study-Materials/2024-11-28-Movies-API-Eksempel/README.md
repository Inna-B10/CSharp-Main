## Oppsummering

Vi brukte den innebygde templaten dotnet new webapi -controller<br>
for å generere en ny template for en webapi.<br>
vi valgte å bruke controller flagget for å kunne ha litt mer kontroll over dataflyten<br>
i apien vår. <br>
<br>
Vi brukte curl som vi så i går, for å hente ned en utgave av samme movies.json objekt, som vi så i går.<br>
Vi laget en modell av hva data vi ville hente ut fra movies.json<br>
og en context som skal inneholde en representasjon av denne filen i minnet på applikasjonen vår<br>
<br>
I program.cs bruker vi builderen vår,<br>
og initialiserer en utgave av vår context.<br>
Da kunne vi hente inn denne kontexten som en "dependency" i vår MovieController.<br>
MovieController tok imot requests over http, og returnerte innsyn i vår kontext basert på<br>
requestparametere og route.
