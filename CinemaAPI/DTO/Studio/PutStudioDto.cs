using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.DTO.Studio;

public class PutStudioDto
{
    [Required]
    public string Label { get; set; } = null!;
}
