using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.DTO.Movie;

public class PutMovieCountryDto
{
    [Required] int CountryId { get; set; }
}