using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.DTO.Actor;

public class PutActorDto
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public DateOnly BirthDate { get; set; }

    public DateOnly? DeathDate { get; set; }

    [Required]
    public string Biography { get; set; } = null!;

    [Required]
    public int CountryId { get; set; }
}
