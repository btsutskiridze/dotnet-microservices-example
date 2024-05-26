using System.ComponentModel.DataAnnotations;

namespace CommandService.Dtos;


public record CommandCreateDto(
    [Required]
    string HowTo,

    [Required]
    string CommandLine
);