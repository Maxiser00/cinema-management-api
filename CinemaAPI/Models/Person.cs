using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.Models;

public class Person
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateOnly BirthDate { get; set; }

    public DateOnly? DeathDate { get; set; }

    [Required, MaxLength(255)]
    public string Biography { get; set; } = string.Empty;

    [Required, MaxLength(64)]
    public string PhotoHash { get; set; } = string.Empty;

    [Required]
    public int CountryId { get; set; }

    public Country Country { get; set; } = null!;
}
