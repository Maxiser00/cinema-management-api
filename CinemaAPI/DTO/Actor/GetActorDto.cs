using CinemaAPI.DTO.Movie;

namespace CinemaAPI.DTO.Actor;

public class GetActorDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly? DeathDate { get; set; }
    public string? Biography { get; set; }
    public string? Country { get; set; }
    
    public List<GetLightMovieDto>? Movies { get; set; }
}