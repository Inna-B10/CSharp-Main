namespace Generics.Models;

public class Elephant(string name, FoodConsumption food, int age, double trunkLength) : Animal(name, food, age)
{
    public double TrunkLength { get; set; } = trunkLength;

    public void Spray() => Console.WriteLine($"{Name} sprays water with its {TrunkLength}m long trunk!");
}