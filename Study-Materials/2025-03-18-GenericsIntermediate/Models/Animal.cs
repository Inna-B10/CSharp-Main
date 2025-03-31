namespace Generics.Models;

public class Animal(string name, FoodConsumption food, int age)
{
    public string Name {get;set;} = name;
    public FoodConsumption Consumtion {get;set;} = food;

    public int Age {get;set;} = age;
}
