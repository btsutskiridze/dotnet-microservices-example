using System.Text.Json;
using CommandService.Dtos;
using CommandService.Factories;

namespace CommandService.EventProcessing;

public class EventProcessor : IEventProcessor
{
    private readonly EventStrategyFactory _eventStrategyFactory;

    public EventProcessor(EventStrategyFactory eventStrategyFactory)
    {
        _eventStrategyFactory = eventStrategyFactory;
    }

    public void ProcessEvent(string message)
    {
        var eventType = GetEventType(message);

        var eventStrategy = _eventStrategyFactory.GetStrategy(eventType);

        eventStrategy.ProcessEvent(message);
    }

    private string GetEventType(string message)
    {
        var eventType = JsonSerializer.Deserialize<GenericEventDto>(message);

        return eventType?.Event ?? string.Empty;
    }
}
