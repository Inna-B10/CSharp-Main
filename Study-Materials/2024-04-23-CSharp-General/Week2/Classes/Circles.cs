/*
Implements the ICircleShape interface
*/

public class Circles : ICircleShape
{

    public double Radius { get; set; }

    public Circles(double radius)
    {
        Radius = radius;
    }

    public double CalculateCircleArea()
    {
        return Math.PI * Math.Pow(Radius, 2);
    }

    public double CalculatePerimeter()
    {
        return 2 * Math.PI * Radius;
    }
}