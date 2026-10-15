using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.DTO.Movie;

public class PostMovieDirectorDto
{
    [Required] public int DirectorId { get; set; }
}