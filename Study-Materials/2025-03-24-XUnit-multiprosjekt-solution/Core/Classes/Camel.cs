namespace Core.Classes;

public class Camel(string name, int humpCount, int age, Feed feed): Animal(name, age, feed)
{
    public int HumpCount {get;set;} = humpCount;
}