namespace _2025_02_27_Lego_database;

using _2025_02_27_Lego_database.Controllers;
using _2025_02_27_Lego_database.Views;

class Program
{
    static void Main(string[] args)
    {
        string setsFilePath = "./sets.csv";
        string themesFilePath = "./themes.csv";

        Console.WriteLine($"{StylesClass.BLUE}{StylesClass.INVERSE} Welcome to Lego database! {StylesClass.RESET_ALL}");
        Console.WriteLine();

        //Load data from files
        BaseController controller = new BaseController(setsFilePath, themesFilePath);

        controller.Start();
    }
}
