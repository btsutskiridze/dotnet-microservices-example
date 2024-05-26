using System.ComponentModel.DataAnnotations;

namespace CommandService.Dtos;

public record CommandReadDto(
    int Id,
    string HowTo,
    string CommandLine
);