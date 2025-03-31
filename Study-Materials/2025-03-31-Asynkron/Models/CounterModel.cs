namespace Asynkron.Models;

public class CounterModel(string name, int maxCount, int delay)
{
    public string Name {get;set;} = name;
    public int MaxCount {get;set;} = maxCount;
    public int DelayMs {get;set;} = delay;
}