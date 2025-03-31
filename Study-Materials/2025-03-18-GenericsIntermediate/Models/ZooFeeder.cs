using Generics.Interfaces;

namespace Generics.Models;

public class ZooFeeder
{

    //Siden vi alltid her kan garantere, via covariance at vår IAnimalPopper<Animal> 
    //kan trygt og typesikkert hentes ut fra vår AnimalPen, trenger vi bare å hente in interfacen,
    //for så å konsumere bare de metodene som trengs i Feed metoden får.
    public static void Feed(IAnimalPopper<Animal> animals)
    {
        for (int i = 0; i < animals.Count; i++)
        {
            var animal = animals.Pop();
            var food = animal.Consumtion switch
            {
                FoodConsumption.Herbivore => "a fresh salad.",
                FoodConsumption.Carnivore => "a nice, juicy salmon",
                FoodConsumption.Omnivore => "anything we could find, really",
                _ => "this foodtype is not registered"
            };
            Console.WriteLine($"Fed animal {animal.Name} {food}");
        }
    }
}