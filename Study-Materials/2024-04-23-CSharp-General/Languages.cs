public class Languages {
    private string? Python;
    private string? JavaScript;

    private string? CSharp;

    public Languages(string? python, string? javascript, string? csharp) {
        Python = python;
        JavaScript = javascript;
        CSharp = csharp;
    }

    private void PrintLanguages() {
        Console.WriteLine($"Lang 1 : {Python}, Lang 2 : {JavaScript}, Lang 3 : {CSharp}");
    }
}