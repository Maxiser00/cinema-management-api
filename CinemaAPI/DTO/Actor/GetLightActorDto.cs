namespace CinemaAPI.DTO.Actor;

public class GetLightActorDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly? DeathDate { get; set; }
    public string? Biography { get; set; }
    public string? Country { get; set; }
}