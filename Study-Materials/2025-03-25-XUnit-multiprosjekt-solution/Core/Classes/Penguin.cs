namespace Core.Classes;

public class Penguin(string name, bool isEmperor, int age, Feed feed): Animal(name, age, feed)
{
    public bool IsEmperor {get;set;} = isEmperor;
}