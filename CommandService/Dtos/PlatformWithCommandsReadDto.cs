namespace CommandService.Dtos;

public class PlatformWithCommandsReadDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<CommandReadDto> Commands { get; set; } = [];
}