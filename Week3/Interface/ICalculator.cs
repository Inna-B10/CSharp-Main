public interface ICalculator {
    /// <summary>
    /// Add method, does addition
    /// </summary>
    /// <param name="a">number a</param>
    /// <param name="b">number b</param>
    /// <returns>the sum of a + b</returns>
    int Add(int a, int b);

    /// <summary>
    /// Subtract method, does subtraction
    /// </summary>
    /// <param name="a">number a</param>
    /// <param name="b">number b</param>
    /// <returns>the sum of a - b</returns>
    int Subtract(int a, int b);
}