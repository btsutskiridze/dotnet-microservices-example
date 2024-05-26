using CommandService.Strategies;

namespace CommandService.Factories;


public class EventStrategyFactory
{
    private static Dictionary<string, IEventStrategy> _strategies;

    public EventStrategyFactory(IServiceProvider provider)
    {
        _strategies = new Dictionary<string, IEventStrategy>
        {
            { "Platform_Published", provider.GetRequiredService<PlatformPublishedEventStrategy>() }
        };
    }

    public IEventStrategy GetStrategy(string eventType)
    {
        return _strategies.TryGetValue(eventType, out var strategy)
            ? strategy
            : new UndeterminedEventStrategy();
    }
}
