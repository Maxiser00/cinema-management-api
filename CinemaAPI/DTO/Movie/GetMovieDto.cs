using CinemaAPI.DTO.Actor;
using CinemaAPI.DTO.Category;
using CinemaAPI.DTO.Director;
using CinemaAPI.DTO.Studio;

namespace CinemaAPI.DTO.Movie;

public class GetMovieDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateOnly ReleaseDate { get; set; }   
    public int Duration { get; set; }
    public decimal Budget { get; set; }
    
    public GetLightStudioDto? Studio { get; set; }
    public string? Language { get; set; }
    
    public List<GetLightActorDto>? Actors { get; set; }
    public List<GetLightDirectorDto>? Directors { get; set; }
    public List<GetLightCategoryDto>? Categories { get; set; }
    public List<string>? Countries { get; set; }
}