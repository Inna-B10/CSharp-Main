using System;

namespace CS_exercise1 {

  class Robot : IRobot {

    private static int countObj;

    public string? Name {get; set;}="Bot";
    public int? Weight {get; set;}=100;
    public byte[] Coordinates {get; set;}={0,0,0};
    public int? Height {get; set;}=1;

    // default constructor
    public Robot(){}

    // user's constructor
    public Robot(string? name, int? weight, byte[] coordinates, int? height){
      if(String.IsNullOrWhiteSpace(name)!=true || name.Trim().Length != 0)
      Name = name;
      if(weight<200||weight==0) Weight=200; else Weight= weight;
      if(coordinates.Length>0)
      Coordinates = coordinates;
      if (height.HasValue)
      {
        Height = height;
      }
      countObj++;
    }
    
    public void printValues(){
      Console.Write($"Name: {Name}, weight: {Weight}, height: {Height}, coordinates: ");
      foreach (byte el in Coordinates){
        Console.Write(el+" ");
      }
      Console.WriteLine();
    }
    public static void printCount(){
      Console.WriteLine("Total created objects: "+countObj);
    }
  }
}