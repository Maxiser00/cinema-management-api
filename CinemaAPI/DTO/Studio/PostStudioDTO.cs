using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.DTO.Studio;

public class PostStudioDto
{
    [Required]
    public string Label { get; set; } = null!;
}
