namespace Core.Classes;

public class Lion(string name, string maneColor, int age, Feed feed): Animal(name, age, feed)
{
    public string ManeColor{get;set;} = maneColor;
}