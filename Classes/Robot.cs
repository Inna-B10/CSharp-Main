using System;

namespace CS_exercise1{
  class Robot{
    private static int countObj;
    private string name="Bot";
    private int weight=100;
    private byte[] coordinates={0,0,0};
    public int Height {get; set;} = 1;

    public int Weight{
      get{
        Console.Write("Result: ");
        return this.weight;
        }
        set {
          if(value<200){
            this.weight = 200;
          }
          else if(value>5000){
            this.weight = 5000;
          }
          else{
            this.weight=value;
          }
        }
    }
    
    public Robot(string name, int weight, byte[] coordinates, int height){
      this.setValues(name, weight, coordinates, height);
      countObj++;
    }
    public Robot(string name){
      this.name = name;
      countObj++;
    }

    public Robot(){
      countObj++;
    }

    public void setValues(string name, int weight, byte[] coordinates,int height){
      this.name = name;
      this.weight= weight;
      this.coordinates = coordinates;
      this.Height= height;
    }
    public void printValues(){
      Console.Write($"Name: {this.name}, weight: {this.weight}, height: {this.Height}, coordinates: ");
      foreach (byte el in coordinates){
        Console.Write(el+" ");
      }
      Console.WriteLine();
    }
    public static void printCount(){
      Console.WriteLine("Total created objects: "+countObj);
    }
  }
}