using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.DTO.Movie;

public class PutMovieDto
{
    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    public DateOnly ReleaseDate { get; set; }

    [Required]
    public int Duration { get; set; }

    [Required]
    public decimal Budget { get; set; }

    [Required]
    public int StudioId { get; set; }

    [Required]
    public int LanguageId { get; set; }
}
