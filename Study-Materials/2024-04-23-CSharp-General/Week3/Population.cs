using System.Net.NetworkInformation;

public class Population {
    // bruker longint

    public long CalculatePopulation() {
        long population1950 = 2557628654;
        long population2024 = 7921606146;

        long populationdifferance = population2024 - population1950;

        return populationdifferance;
    }

    public double CalculatePopulationDouble() {
        double pop1950 = 2.5;
        double pop2024 = 7.9;

        return pop2024 - pop1950;
    }

    public double CalculateFuturePopulation() {
        double pop1950 = 2.5;
        double pop2024 = 7.9;
        double numberOfYears = 15;
        double popdiff = pop2024 - pop1950;

        return pop2024 + popdiff / (2024-1950) * numberOfYears;
    }

    public double CalculatePopulationWithLoop() {
        double pop1950 = 2.5;
        double pop2024 = 7.9;

        double totalPopulation = pop1950;
        // we use a linear growth model, this is not accurate but for 
        // demonstration purposes.

        // this should return a value of 7.9629...
        for(int year = 1950; year <= 2024; year++) {
            totalPopulation += (pop2024 - pop1950) / (2024-1950);
        }
        return totalPopulation;
    }
}