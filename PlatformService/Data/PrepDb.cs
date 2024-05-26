using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data;

public class PrepDb
{

    private static IWebHostEnvironment _env;

    public static void PrepPopulation(IApplicationBuilder app, IWebHostEnvironment env)
    {
        var serviceScope = app.ApplicationServices.CreateScope();
        _env = env;


        SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), env.IsProduction());

    }

    private static void SeedData(AppDbContext context, bool isProd)
    {
        if (isProd)
        {
            Console.WriteLine("--> Attempting to apply migrations...");
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not run migrations: {ex.Message}");
            }
        }


        if (!context.Platforms.Any())
        {
            Console.WriteLine("--> Seeding Data...");

            context.Platforms.AddRange(
                new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                new Platform() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                new Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" },
                new Platform() { Name = "Docker", Publisher = "Docker Inc.", Cost = "Free" },
                new Platform() { Name = "Azure", Publisher = "Microsoft", Cost = "Paid" }
            );

            context.SaveChanges();

            Console.WriteLine("--> Data Seeded");
            context.Platforms.ToList().ForEach(p => Console.WriteLine($"--> {p.Name}"));
        }
        else
        {
            Console.WriteLine("--> We already have data");
        }
    }
}
