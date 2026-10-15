using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.DTO.Movie;

public class PutMovieActorDto
{
    [Required] int ActorId { get; set; }
}