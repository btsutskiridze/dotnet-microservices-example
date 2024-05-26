using CommandService.Models;
using CommandService.SyncDataServices.Grpc;

namespace CommandService.Data;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        var GrpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();

        var platforms = GrpcClient?.ReturnAllPlatforms();

        SeedData(serviceScope.ServiceProvider.GetService<ICommandRepo>(), platforms);
    }

    private static void SeedData(ICommandRepo? repo, IEnumerable<Platform> platforms)
    {
        if (repo is null) return;

        Console.WriteLine("--> Seeding new platforms...");

        foreach (var plat in platforms)
        {
            if (!repo.ExternalPlatformExists(plat.ExternalId))
            {
                repo.CreatePlatform(plat);
            }
        }

        repo.SaveChanges();
        Console.WriteLine("--> Seeding new platforms... Done!");
    }
}