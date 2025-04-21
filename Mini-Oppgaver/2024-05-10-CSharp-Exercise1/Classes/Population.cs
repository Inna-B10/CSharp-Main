namespace CS_exercise1;

public class Population{

  static int startYear = 1950;
  static int endYear = 2024;
  static double startPopulation = 2.5e9;
  static double endPopulation = 7.9e9;
  int targetYear {get;set;}

  double growthRate = CalculateGrowthRate(startYear, endYear, startPopulation, endPopulation);


    //Method for calculating the average annual growth factor
    static double CalculateGrowthRate(int startYear, int endYear, double startPopulation, double endPopulation)
    {
        int years = endYear - startYear;
        double growthRate = Math.Pow((endPopulation / startPopulation), (1.0 / years)) - 1;
        return growthRate;
    }

    // Method for calculating population for a given year
    public void CalculatePopulation(int targetYear)
    {
        CalculateGrowthRate(startYear, endYear, startPopulation, endPopulation);
        int years = targetYear - startYear;
        double population = startPopulation * Math.Pow((1 + growthRate), years);
        Console.WriteLine(population);
        string scientificPopulation = population.ToString("0.0e0");
        Console.WriteLine("Approximate population in {0} year is: {1}", targetYear, scientificPopulation);
    }
}