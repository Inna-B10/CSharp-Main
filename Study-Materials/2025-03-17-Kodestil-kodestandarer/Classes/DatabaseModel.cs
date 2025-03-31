
//Siden namespaces reflekterer folderstrukturen, er det lett å linke prosjektkode sammen.
using Kodestil_kodestandarer.Interfaces;

namespace Kodestil_kodestandarer.Classes;

//Så mye som mulig, hvis classen din ikke trenger komplisert setup, bruk primary constructor.
public class DatabaseModel(string databaseLocation) : IDatabaseModel
{
    public List<string> Names {get;set;} = [];

    //private fields og properties skrives med _camelCase, legg merke til _

    private string _connectionString {get;set;} = string.Empty;

    public string DatabaseLocation = databaseLocation;
    //Metoder sine parametere, regnes som locally scoped, så skal skrives i camelCase.
    public void GetConnectionString(string databaseType)
    {
        //Har du kode som kan "throwe" en exception, husk å behandle den så godt som mulig.
        try
        {
            _connectionString = GetDatabaseParameters(databaseType);
        }
        //Husk å være så spesifikk i catching som mulig, pass på å behandle exceptions du selv har definert kan skje. 
        catch (NotImplementedException ex)
        {
            Console.WriteLine($"Something went wrong getting the database type: {ex.Message}");
            throw;
        }
    }

    
    private static string GetDatabaseParameters(string databaseType)
    {

        //Husk å alltid legge ved enten en default: case i en switch case statement,
        //Eller en discard param path i en switch expression, for å dekke alle mulige cases som kan skje, i koden.
        return databaseType switch 
        {
            "postgresql" => "Host=localhost;Port=5432;Database=mydatabase;Username=myuser;Password=mypassword;",
            "mySql" => "Server=localhost;Database=mydatabase;User=myuser;Password=mypassword;",
            "sqlite" => "Data Source=mydatabase.db",
            "mongodb" => "mongodb://myusername:mypassword@cluster0-shard-00-00.mongodb.net:27017,cluster0-shard-00-01.mongodb.net:27017,cluster0-shard-00-02.mongodb.net:27017/mydatabase?ssl=true&replicaSet=atlas-cluster0&authSource=admin&retryWrites=true&w=majority",
            _ => throw new NotImplementedException($"The database type {databaseType} is not yet implemented.")
        };
    }
}
