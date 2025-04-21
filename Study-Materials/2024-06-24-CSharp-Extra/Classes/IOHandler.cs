using System;
using System.IO;

public class IOHandler
{
    public void ReadJSONFile(string filePath)
    {
        try
        {
            string jsonData = File.ReadAllText(filePath);
            Console.WriteLine("Contents of the JSON file:");
            Console.WriteLine(jsonData);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading from the JSON file: {ex.Message}");
        }
    }

    public void ReadCSVFile(string filePath)
    {
        try
        {
            string[] lines = File.ReadAllLines(filePath);
            Console.WriteLine("Content in CSV file:");
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading the CSV file: {ex.Message}");
        }
    }

    public void ReadTextFile(string filePath)
    {
        try
        {
            string[] lines = File.ReadAllLines(filePath);
            Console.WriteLine("Contents of text file:");
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading from the file: {ex.Message}");
        }
    }

    // Write methods

    public void WriteJSONFile(string filePath, string content)
    {
        try
        {
            File.WriteAllText(filePath, content);
            Console.WriteLine("Successfully wrote to the JSON file!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to the JSON file: {ex.Message}");
        }
    }

    public void WriteCSVFile(string filePath, string content)
    {
        try
        {
            File.WriteAllText(filePath, content);
            Console.WriteLine("Successfully wrote data to the CSV file!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to write data to the CSV file: {ex.Message}");
        }
    }
}