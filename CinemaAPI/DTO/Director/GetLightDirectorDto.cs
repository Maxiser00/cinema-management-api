namespace CinemaAPI.DTO.Director;

public class GetLightDirectorDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly? DeathDate { get; set; }
    public string? Biography { get; set; }
    public string? Country { get; set; }
}