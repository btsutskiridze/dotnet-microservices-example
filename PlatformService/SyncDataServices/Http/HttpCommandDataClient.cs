using System.Text;
using System.Text.Json;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http;

public class HttpCommandDataClient : ICommandDataClient
{

    private readonly HttpClient _client;
    private readonly IConfiguration _config;

    public HttpCommandDataClient(HttpClient client, IConfiguration config)
    {
        _client = client;
        _config = config;
    }

    public async Task SendPlatformToCommand(PlatformReadDto plat)
    {
        var httpContent = new StringContent(
            JsonSerializer.Serialize(plat),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _client.PostAsync($"{_config["CommandService"]}/api/c/platforms", httpContent);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("--> Sync POST to Command Service was OK!");
        }
        else
        {
            Console.WriteLine("--> Sync POST to Command Service was NOT OK!");
        }
    }
}