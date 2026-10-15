using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.DTO.Movie;

public class PutMovieDirectorDto
{
    [Required] int DirectorId { get; set; }
}