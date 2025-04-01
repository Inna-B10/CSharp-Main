namespace Core.Classes;

public class Animal(string name, int age, Feed feed)
{
    public string Name {get;set;} = name;
    public int Age {get;set;} = age;
    public Feed Feed {get;set;} = feed;
}