namespace Csharp;
class Program
{

    public class Car {
        string model = "Volvo";
    }

    public static int AddNumbers(int a, int b) {
        return a + b;
    }
    public static int VoidAdd(int a, int b) {
        return a + b;
    }

    static void Main(string[] args)
    {
        int x = 5;
        double y = 0.15;
        // char a = 'b';
        // string testChar = "aaa";
        Console.WriteLine(x + y);
        Console.WriteLine(AddNumbers(6, 6)); // -> 6
        string[] stringArray = {"Hello", "From", "A", "Array"};
        Console.WriteLine(VoidAdd(2, 15));
        Console.WriteLine(SayHello());
        Console.WriteLine(stringArray[3]);
        
        for(int i = 0; i < stringArray.Length; i++) {
            Console.WriteLine(stringArray[i]);
        }
    }

    public static string SayHello() {
        return "Hello";
    }
}
