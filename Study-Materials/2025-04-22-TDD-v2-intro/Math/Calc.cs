namespace Math;

public class Calc
{
  public float Add(float a, float b)
  {
    var result = a + b;

    if (float.IsInfinity(result))
    {
      throw new ArgumentException("Numbers to big!");
    }

    return a + b;
  }

  public float Divide(float a, float b)
  {
    return a / b;
  }
}
