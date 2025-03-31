namespace Generics.Models;

public class Penguin(string name, FoodConsumption food, int age, bool isEmperor) : Animal(name, food, age)
{
    public bool IsEmperor { get; set; } = isEmperor;

    public void Slide() => Console.WriteLine($"{Name} slides on its belly!");
}