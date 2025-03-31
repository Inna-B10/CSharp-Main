namespace Core.Classes;

public class Elephant(string name, double truncLength, int age, Feed feed): Animal(name, age, feed)
{
    public double TruncLength {get;set;} = truncLength;
}