using System.Text.Json;
using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;

namespace CommandService.Strategies;


public class PlatformPublishedEventStrategy : IEventStrategy
{
    private readonly IServiceScopeFactory _scopeFactory;

    private readonly IMapper _mapper;

    public PlatformPublishedEventStrategy(IServiceScopeFactory scopeFactory, IMapper mapper)
    {
        _scopeFactory = scopeFactory;
        _mapper = mapper;
    }

    public void ProcessEvent(string message)
    {

        using var scope = _scopeFactory.CreateScope();

        var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();

        var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(message);

        try
        {
            var plat = _mapper.Map<Platform>(platformPublishedDto);

            if (repo.ExternalPlatformExists(plat.ExternalId))
            {
                Console.WriteLine("--> Platform already exists");
                return;
            }

            repo.CreatePlatform(plat);
            repo.SaveChanges();

            Console.WriteLine("--> Platform added");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Could not add platform to DB: {ex.Message}");
        }
    }
}