namespace CinemaAPI.DTO.Movie;

public class GetLightMovieDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateOnly ReleaseDate { get; set; }   
    public int Duration { get; set; }
    public decimal Budget { get; set; }
    
    public string? Studio { get; set; }
    public string? Language { get; set; }
    
    public List<string>? Directors { get; set; }
    public List<string>? Categories { get; set; }
    public List<string>? Countries { get; set; }
}