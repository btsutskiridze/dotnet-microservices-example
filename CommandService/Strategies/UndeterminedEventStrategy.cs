namespace CommandService.Strategies;


public class UndeterminedEventStrategy : IEventStrategy
{
    public void ProcessEvent(string message)
    {
        Console.WriteLine("Undetermined event");
    }
}