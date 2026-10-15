using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.DTO.Movie;

public class PostMovieCountryDto
{
    [Required] public int CountryId { get; set; }
}