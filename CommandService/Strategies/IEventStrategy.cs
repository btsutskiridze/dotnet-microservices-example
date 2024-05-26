namespace CommandService.Strategies;


public interface IEventStrategy
{
    void ProcessEvent(string message);
}