using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CinemaAPI.Models;

public class Movie
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(255)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public DateOnly ReleaseDate { get; set; }

    [Required]
    public int Duration { get; set; }

    [Required, Precision(16, 2)]
    public decimal Budget { get; set; }

    [Required, MaxLength(64)]
    public string PosterHash { get; set; } = string.Empty;

    [Required]
    public int StudioId { get; set; }

    public Studio Studio { get; set; } = null!;

    [Required]
    public int LanguageId { get; set; }

    public Language Language { get; set; } = null!;

    public List<MovieCountry> MovieCountries { get; set; } = new();
    public List<MovieActor> MovieActors { get; set; } = new();
    public List<MovieDirector> MovieDirectors { get; set; } = new();
    public List<MovieCategory> MovieCategories { get; set; } = new();
}
