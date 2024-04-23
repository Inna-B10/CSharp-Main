namespace CSharp_Examples;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        // inherited the Person class with the newPerson object
        Person newPerson = new Person(null, null);
        string name = "John";
        newPerson.Name = name;
        newPerson.SayHello(name);
        Console.WriteLine(newPerson.Name);

        Car car = new Car("Model S", "Tesla", 2014);
        Car car1 = new Car("Polestar 2", "Polestar", 2020);
        car.GetCar();
        car1.GetCar();

        Languages languages = new Languages(null, null, null);

    }
}
