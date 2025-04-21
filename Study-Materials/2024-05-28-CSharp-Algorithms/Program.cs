using System.Diagnostics;

namespace KH_CS_Uke4;
class Program
{
    static void Main(string[] args)
    {
        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        
        

        Algorithms algorithms = new Algorithms();
        int[] sampleArray = {1,10,9,82, 24, 28, 16, 1028, 512, 256};
        algorithms.BubbleSort(sampleArray);
        Console.WriteLine("Bubble sort output: ");
        Console.WriteLine(string.Join(" ", sampleArray));

        stopwatch.Stop();
        Console.WriteLine($"Bubblesort ran for: {stopwatch.ElapsedMilliseconds} milliseconds");

        stopwatch.Start();

        int[] secondSample = {25, 150, 250, 10, 82, 7, 9, 3, 1};
        algorithms.Quicksort(secondSample, 0, secondSample.Length - 1);
        Console.WriteLine("Sorted array using Quicksort: ");
        Console.WriteLine(string.Join(" ", secondSample));

        stopwatch.Stop();

        Console.WriteLine($"Quicksort ran for: {stopwatch.ElapsedMilliseconds} milliseconds");

        SearchAlgorithms searchAlgorithms = new SearchAlgorithms();

        stopwatch.Start();

        int[] sampleArray2 = {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20};
        int x = 3;
        int result = searchAlgorithms.LinearSearch(sampleArray2, x);
        if(result == -1) 
        {
            Console.WriteLine("Element not found!");
        }
        else 
        {
            Console.WriteLine($"The element was found within the given array on index: {result}");
        }

        stopwatch.Stop();
        Console.WriteLine($"Linear search found the element in {stopwatch.ElapsedMilliseconds} milliseconds!");
    
        stopwatch.Start();

        int[] sampleArray3 = {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20};
        int x1 = 8;    
        int result1 = searchAlgorithms.BinarySearch(sampleArray3, x1);

        if(result1 == -1) 
        {
            Console.WriteLine("The element was not found within the sample array!");
        }
        else 
        {
            Console.WriteLine($"The element was found on index: {result1}");
        }

        stopwatch.Stop();

        Console.WriteLine($"Binary Search Algorithm ran in: {stopwatch.ElapsedMilliseconds} milliseconds!");
        Console.ReadLine();
    }
}
