namespace Core.Classes;

public class Bear(string name, string furColor, int age, Feed feed): Animal(name,age,feed)
{
    public string FurColor {get;set;} = furColor;
}