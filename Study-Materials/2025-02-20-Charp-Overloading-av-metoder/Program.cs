using System.Numerics;

namespace _20_02_2025_rep_torsdag;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, user. Welcome to the calculator program.");
        Console.WriteLine("Please enter a valid mathematical operation you want done");
        //130 + 58
        //[1,3,0,+,5,8]
        var validOperands = new List<char> { '+', '-', '/', '*' };

        var input = Console.ReadLine();
        //[FIXME]error if 
        // - operand after operand (*/)
        // - not valid operand after valid (%)
        // - string instead of number
        // - number format (. or ,)
        // - dividing by 0 (not error but should process this case)
        while (string.IsNullOrEmpty(input) || !validOperands.Any(op => input.Any(character => character == op)))
        {
            Console.WriteLine("I could not read a valid operation, please try again");
            input = Console.ReadLine();
        }

        var sanitizeInput = input.Where(c => c != ' ').ToList();
        List<double> currentVal = [];
        List<char> currentOperand = [];
        List<char> buffer = [];
        var calc = new Calculator<double>(0);
        for (int i = 0; i < sanitizeInput.Count; i++)
        {
            if (!validOperands.Contains(sanitizeInput[i]))
            {
                buffer.Add(sanitizeInput[i]);
            }
            else
            {
                currentOperand.Add(sanitizeInput[i]);
                currentVal.Add(double.Parse(string.Join("", buffer)));
                buffer = [];
            }
        }
        if (buffer.Count > 0) currentVal.Add(double.Parse(string.Join("", buffer)));
        calc.val = currentVal[0];
        for (int i = 0; i < currentOperand.Count; i++)
        {
            calc = calc.RunOperator(currentOperand[i], currentVal[i + 1]);
        }
        Console.WriteLine($"The result of your operation is: {calc.GetResult()}");
    }

    static int Add(int a, int b)
    {
        return a + b;
    }

    static int Add(int a, int b, int c)
    {
        return a + b + c;
    }

    static int Add(params int[] param)
    {
        return param.Sum();
    }
}
class Calculator<T>(T startVal)
where T : INumber<T>
{
    public T val = startVal;
    public Calculator<T> MultiplyVal(T multiplicationFactor)
    {
        val *= multiplicationFactor;
        return this;
    }
    public Calculator<T> AddToVal(T addition)
    {
        val += addition;
        return this;
    }
    public Calculator<T> SubtractFromVal(T subtraction)
    {
        val -= subtraction;
        return this;
    }

    public Calculator<T> Division(T divider)
    {
        val /= divider;
        return this;
    }
    public T GetResult()
    {
        return val;
    }
    public Calculator<T> RunOperator(char operand, T val)
    {
        return operand switch
        {
            '+' => AddToVal(val),
            '*' => MultiplyVal(val),
            '/' => Division(val),
            '-' => SubtractFromVal(val),
            _ => this
        };
    }
}
