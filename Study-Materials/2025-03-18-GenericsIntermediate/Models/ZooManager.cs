using Generics.Interfaces;

namespace Generics.Models;

public class ZooManager
{
    // Takket være kovarianse kan vi bruke AnimalShow<Lion>, AnimalShow<Elephant>, etc.
    public static void RunShow(IAnimalShow<Animal> show)
    {
        Console.WriteLine("\n=== Animal Show Starting ===");
        for (int i = 0; i < show.PerformerCount; i++)
        {
            var performer = show.GetNextPerformer();
            Console.WriteLine($"{performer.Name} performs for the audience!");
            
            // Vi kan bruke typespesifikk funksjonalitet via casting
            if (performer is Lion lion)
                lion.Roar();
            else if (performer is Elephant elephant)
                elephant.Spray();
            else if (performer is Penguin penguin)
                penguin.Slide();
            else
                Console.WriteLine($"{performer.Name} does a generic trick!");
        }
        Console.WriteLine("=== Animal Show Ended ===\n");
    }
    
    // Takket være kontravarianse kan vi bruke AnimalSorter<Animal> for alle dyretyper
    public static void OrganizeExhibit<T>(IEnumerable<T> animals, AnimalSorter<T> sorter) where T : Animal
    {
        Console.WriteLine("\n=== Organizing New Exhibit ===");
        sorter.SortByAge(animals);
        Console.WriteLine("=== Exhibit Ready ===\n");
    }
    
    // Denne metoden viser både kovarianse og kontravarianse i samme funksjon
    public static void FeedAndTrain<T>(IAnimalPopper<T> source, IPushable<Animal> destination) where T : Animal
    {
        Console.WriteLine("\n=== Feeding and Training Session ===");
        for (int i = 0; i < Math.Min(5, source.Count); i++)
        {
            var animal = source.Pop();
            Console.WriteLine($"Training {animal.Name}...");
            destination.Push(animal); // Dette fungerer fordi Animal er superklasse av T
        }
        Console.WriteLine("=== Session Complete ===\n");
    }
}