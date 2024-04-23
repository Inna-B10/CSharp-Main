public class Car {
    public string? Model { get; set; }
    public string? Brand { get; set; }
    public int? Year { get; set; }

    private string? registration;

    public Car(string? model, string? brand, int? year) {
        Model = model;
        Brand = brand;
        Year = year;
    }

    public void GetCar() {
        registration = "1341241";
        Console.WriteLine($"Car model: {Model}, Car brand: {Brand}, Year released: {Year}, Registration: {registration}");
    }
}