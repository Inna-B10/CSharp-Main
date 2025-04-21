/*
Define the interface
*/
public interface ICircleShape
{
    /// <summary>
    /// Calculate the area of a circle
    /// </summary>
    /// <returns>PI * the power of Radium times 2</returns>
    double CalculateCircleArea();

    /// <summary>
    /// Calculates the perimeter of a circle
    /// </summary>
    /// <returns>2 * PI * Radius</returns>
    double CalculatePerimeter();

}