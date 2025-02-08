//  dotnet run

// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
// 
// Random dice = new Random();
// int roll = dice.Next();
// Console.WriteLine(roll);


Random dice = new Random();
int roll1 = dice.Next();
int roll2 = dice.Next(101);
int roll3 = dice.Next(50, 101);

Console.WriteLine($"First roll: {roll1}");
Console.WriteLine($"Second roll: {roll2}");
Console.WriteLine($"Third roll: {roll3}");

