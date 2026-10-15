using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.DTO.Movie;

public class PostMovieCategoryDto
{
    [Required] public int CategoryId { get; set; }
}