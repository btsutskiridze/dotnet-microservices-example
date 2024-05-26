using CommandService.Models;

namespace CommandService.Data;


public interface ICommandRepo
{

    bool SaveChanges();

    // Platforms
    IEnumerable<Platform> GetAllPlatforms();

    IEnumerable<Platform> GetAllPlatformsWithCommands();

    Platform GetPlatformById(int platformId);

    void CreatePlatform(Platform platform);

    bool PlatformExists(int platformId);

    bool ExternalPlatformExists(int externalPlatformId);

    // Commands
    IEnumerable<Command> GetCommandsForPlatform(int platformId);

    Command? GetCommand(int platformId, int commandId);

    void CreateCommand(int platformId, Command command);
}