using CinemaAPI.DTO.Director;
using CinemaAPI.DTO.Movie;

namespace CinemaAPI.DTO.Studio;

public class GetStudioDto
{
    public int Id { get; set; }
    public string? Label { get; set; }
    
    public List<GetLightMovieDto>? Movies { get; set; }
    public List<GetLightDirectorDto>? Directors { get; set; }
}