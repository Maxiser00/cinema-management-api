using CinemaAPI.DTO.Movie;

namespace CinemaAPI.DTO.Country;

public class GetCountryDto
{
    public int Id { get; set; }
    public string? Label { get; set; }
    
    public List<GetLightMovieDto>? Movies { get; set; }
}