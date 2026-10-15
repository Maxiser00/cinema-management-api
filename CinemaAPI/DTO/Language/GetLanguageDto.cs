using CinemaAPI.DTO.Movie;

namespace CinemaAPI.DTO.Language;

public class GetLanguageDto
{
    public int? Id { get; set; }
    public string? Label { get; set; }
    
    public List<GetLightMovieDto>? Movies { get; set; }
}