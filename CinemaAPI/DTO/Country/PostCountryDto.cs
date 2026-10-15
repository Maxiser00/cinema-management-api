using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.DTO.Country;

public class PostCountryDto
{
    [Required]
    public string Label { get; set; } = null!;
}
