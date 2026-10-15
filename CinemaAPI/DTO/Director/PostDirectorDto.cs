using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.DTO.Director;

public class PostDirectorDto
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
