
namespace Generics.Models; 

public class Lion(string name, FoodConsumption food, int age, string maneColor) : Animal(name, food, age)
{
    public string ManeColor { get; set; } = maneColor;

    public void Roar() => Console.WriteLine($"{Name} roars loudly!");
}