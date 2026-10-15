using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.DTO.Movie;

public class PutMovieCategoryDto
{
    [Required] int CategoryId { get; set; }
}