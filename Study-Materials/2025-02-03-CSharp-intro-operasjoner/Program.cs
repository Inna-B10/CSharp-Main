namespace _03_02_2025_intro_operasjoner;

class Program
{
    static void Main(string[] args)
    {
        double num1 = 11;
        double num2 = 32;
        

        /* Aritremiske operatorer */
        double sum = num1  +num2;
        Console.WriteLine(sum);

        double subtract = num1 - num2;
        Console.WriteLine(subtract);

        double multiply = num1 * num2;
        Console.WriteLine(multiply);

        double divide = num1 / num2;
        Console.WriteLine(divide);



        /* Tildelingsoperatorer. */
        num1 += 2;

        num2 -= 4;

        num1 *= 3;

        num2 /= 5;

        Console.WriteLine(num1);
        Console.WriteLine(num2);


        /* Sammenlignings operatorer, resultatet kan lagres som en bool (true/false) */
        bool isLargerThan = num1 > num2;
        Console.WriteLine(isLargerThan);

        bool isSmallerThan = num1 < num2;
        Console.WriteLine(isSmallerThan);

        bool isEqual = num1 == num2;
        Console.WriteLine(isEqual);

        /* Logikkoperatorer, lar oss se på flere forskjellige sammenligninger */

        /* Er begge sann? */
        Console.WriteLine(isLargerThan && isSmallerThan);

        /* er en av de sann? */
        Console.WriteLine(isLargerThan || isSmallerThan);

        /* det motsatte av */
        Console.WriteLine(!isEqual);
    }
}
