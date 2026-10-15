using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.DTO.Country;

public class PutCountryDto
{
    [Required]
    public string Label { get; set; } = null!;
}
