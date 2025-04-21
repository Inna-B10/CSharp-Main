using System;
using System.Collections.Generic;

namespace CS_exercise1;

class Program
{
    static void Main(string[] args)
    {
        int a = 16;
        double b = 5.324d;
        string level = "user";

        //------------------------Array
        string[] day = {"Monday", "Wednesday", "Friday"};

        //------------------------List
        List<string> AuthorList = new List<string>();
        AuthorList.Add("Dickens");
        AuthorList.Add("Christie");
        AuthorList.Add("Tolkien");
        AuthorList.Add("Orwell");

        //Output array
        Console.WriteLine("Array:");
        for(int i=0; i<day.Length; i++){
            Console.WriteLine(day[i]);
        }
        Console.WriteLine();

        //Output List
        Console.WriteLine("List:");
        for(int i=0; i<AuthorList.Count; i++){
            Console.WriteLine(AuthorList[i]);
        }
        /*foreach(string author in AuthorList){
            Console.WriteLine(author);
        }*/
        Console.WriteLine();

        //------------------------Objects
        Robot healer = new Robot("Healer", 800, new byte[]{0,10,0}, 2);
        healer.printValues();
        
        Robot killer = new Robot("Killer", 1000, new byte[] {0,0,10}, null);
        killer.printValues();

        Robot bot = new Robot();
        bot.printValues();

        Robot bot1 = new Robot("", null, [], 4);
        bot1.printValues();

        Robot.printCount();
        /*
        bot1.Name = "Bot1";
        bot1.Height = 5;
        Console.WriteLine($"{bot1.Name} height: {bot1.Height}");
        */
        Console.WriteLine();
        //------------------------Methods
        Console.WriteLine($"Sum = {AddNumbers(1.7, 3.2)}");
        //Console.WriteLine($"Sum = {AddNumbers((int)7.2, (int)3.9)}");
         Console.WriteLine();

        double area = AreaCircle();
        Console.WriteLine("Area of circle is: {0}", area);
        Console.WriteLine();

        string text = "Alice,Bob,Chris,Dave,Eric,Fred";

        string[] list = text.Split(",");
        foreach(string str in list) {
            Console.WriteLine(str);
        }
        Console.WriteLine($"Hello, {list[3]}!");
        Console.WriteLine("Goodby, {0} and {1}!", list[0], list[1]);
        Console.WriteLine("Today is: "+day[2]);
        Console.WriteLine(text.Substring(10, 5));
        Console.WriteLine(text.Replace("Eric", "Emil"));
        Console.WriteLine();

        Population population = new Population();
        population.CalculatePopulation(1900);
        Console.WriteLine();

    }
    public static double AreaCircle()
    {
        double radius;
        do {
            Console.WriteLine("Input radius (float num): ");
            string input = Console.ReadLine();

            if (double.TryParse(input, out radius)) {
                double area = Math.PI * Math.Pow(radius, 2);
                return(area);
            }
            else {
                Console.WriteLine("Invalid input! Please enter a valid floating-point number.");
            }
        }
        while (true);    
    }
    
    public static double AddNumbers(double x, double y){
        return x+y;
    }
    /*
    public static int AddNumbers(int x, int y){
        return x+y;
    }
    */

}
