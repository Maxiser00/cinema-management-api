using CinemaAPI.DTO.Director;
using CinemaAPI.DTO.Movie;

namespace CinemaAPI.DTO.Category;

public class GetCategoryDto
{
    public int Id { get; set; }
    public string? Label { get; set; }
    
    public List<GetLightMovieDto>? Movies { get; set; }
    public List<GetLightDirectorDto>? Directors { get; set; }
}