namespace CS_exercise1;

public interface IRobot {
  string? Name {get; set;}
  int? Weight {get; set;}
  byte[] Coordinates {get; set;}
  int? Height {get; set;}

  void printValues();
}