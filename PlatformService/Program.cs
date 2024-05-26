using Microsoft.EntityFrameworkCore;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.SyncDataServices.Grpc;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.UseInMemoryDatabase("InMem");
        Console.WriteLine($"--> Using InMemory Database");
    }
    else
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn"));
        Console.WriteLine($"--> Using SQL Server");
    }
});


builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

builder.Services.AddGrpc();

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapGrpcService<GrpcPlatformService>();
app.MapGet("protos/platforms.proto", async (context) =>
{
    await context.Response.WriteAsync(File.ReadAllText("Protos/platforms.proto"));

});


PrepDb.PrepPopulation(app, app.Environment);

Console.WriteLine("CommandService endpoint: " + app.Configuration["CommandService"]);

app.Run();


