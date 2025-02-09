public class MyGame
{
    /// <summary>
    /// This is purely a mock/psuedocode class, not functional
    /// </summary>
    public bool GameState { get; set; } // set to true if running
    public required string MainChar { get; set; } // set to a default name if not given any arguments
    public int Lives;
    public double Damage;
    public char X;
    public char Y;
    public char[][]? Map; // 2D-Array of characters
    public List<string>? NPC { get; set; }
}