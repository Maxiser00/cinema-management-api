using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.DTO.Movie;

public class PostMovieActorDto
{
    [Required] public int ActorId { get; set; }
}